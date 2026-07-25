using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using MaxMind.GeoIP2.Responses;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GlobalApi.Repository.MasterRepository
{
    public class CustomerCallbackRepository : ICustomerCallback
    {

        private readonly GlobalContext db;
        private IEMailService _EMailService;
        private readonly HttpClient _httpClient;

        private readonly IPrimarykeyvalue primarykeyvalue;
        public CustomerCallbackRepository(IEMailService EMailService, HttpClient httpClient)
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            this._EMailService = EMailService;
            this._httpClient = httpClient;
        }


        public async Task<ApiResponse> InsertCustomer_Callback(Customer_Callback custCallback)
        {
            try
            {
                int id = await primarykeyvalue.primary_key("Customer_Callback");

                Customer_Callback objCust = new Customer_Callback()
                {
                    cust_callback_id = id,
                    cust_name = custCallback.cust_name,
                    cust_phone_no = custCallback.cust_phone_no,
                    cust_email = custCallback.cust_email,
                    cust_msg_desc = custCallback.cust_msg_desc,
                    created_date = DateTime.Now
                };

                await db.Customer_Callback.AddAsync(objCust);
                await db.SaveChangesAsync();

                //local api url
                string apiUrl = "http://192.168.1.128/ConVoxCCS/External/external_dialer.php";

                Customer_Call_Object objPast = new Customer_Call_Object()
                {
                    action = "DATAUPLOAD",
                    data = new DataItem[]
                    {
                new DataItem { mobile = objCust.cust_phone_no, info_1 = objCust.cust_name }
                    },
                    refno = id.ToString(),
                    process = "ConVox_Process",
                    batch_id = "457764335557",
                    generate_ticket = "Y",
                    cust_type = "student",
                    cust_name = objCust.cust_name,
                    cust_phone_no = objCust.cust_phone_no,
                    cust_email = objCust.cust_email,
                    cust_concern_id_fk = "79",
                    cust_concern_desc = objCust.cust_msg_desc,
                    list_id = "102"
                };

                string jsonData = JsonConvert.SerializeObject(objPast);
                string apiResponse = await PostDataAsync(apiUrl, jsonData);


                string emailBody = $@"
            <html>
            <body>
                <h3>LMS - Customer Callback Request</h3>
                <table border='1' cellspacing='0' cellpadding='5' style='border-collapse: collapse; width: 100%;'>
                    <tr><th style='background-color:#f2f2f2; text-align:left;'>Field</th><th style='background-color:#f2f2f2; text-align:left;'>Details</th></tr>
                    <tr><td><b>Name</b></td><td>{objCust.cust_name}</td></tr>
                    <tr><td><b>Phone Number</b></td><td>{objCust.cust_phone_no}</td></tr>
                    <tr><td><b>Email</b></td><td>{objCust.cust_email}</td></tr>
                    <tr><td><b>Message</b></td><td>{objCust.cust_msg_desc}</td></tr>
                </table>
            </body>
            </html>";

                //test ALDA
                /*
                await _EMailService.SendEmailAsync_support(objCust.cust_email, "laksman.alda@vikasglobal.net", "LMS - Customer Callback Request", emailBody);
                return "Mail send success";
                */

                //live ALDA
                try
                {
                    await _EMailService.SendEmailAsync_support(objCust.cust_email, "enquiry@neurospineoptica.com", "LMS - Customer Callback Request", emailBody);
                }

                catch (Exception e)
                {

                }
                

                // Return structured API response
                return new ApiResponse
                {
                    STATUS = "DU000",
                    MESSAGE = new Message
                    {
                        inserted_numbers = objCust.cust_phone_no
                    },
                    refno = id.ToString()
                };


            }
            catch (Exception e)
            {
                throw new Exception("Error while inserting customer callback: " + e.Message);
            }
        }


        public async Task<string> PostDataAsync(string apiUrl, string jsonData1)
        {
            try
            {

                var content = new StringContent(jsonData1, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);

                response.EnsureSuccessStatusCode(); // Ensure HTTP 2xx status for success

                if (response.IsSuccessStatusCode)
                {
                    // Additional actions for successful response, if needed
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle unsuccessful response (non-2xx status code)
                    // You can choose to return an error message or throw an exception
                    return $"HTTP request failed with status code: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                // For example, logging or returning a custom error message
                return $"An error occurred: {ex.Message}";
            }
        }

    }
}
