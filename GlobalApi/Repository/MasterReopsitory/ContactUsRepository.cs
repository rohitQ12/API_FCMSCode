using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Data;
using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using Microsoft.Data.SqlClient;
using GlobalApi.Models.Authentication;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Configuration;


namespace GlobalApi.Repository.MasterRepository
{
    public class ContactUsRepository : ContactUsIRepository
    {

        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContactUsRepository()
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
           
        }


        public async Task<string> InsertContactUs(ContactUs lead)
        {
            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {
                    
                    int cust_id = await primarykeyvalue.primary_key("Customer_ContactUs");

                    Customer_ContactUs obj = new Customer_ContactUs()
                    {
                        cust_id = cust_id,
                        cust_name = lead.cust_name,
                        cust_phone_no = lead.cust_phone_no,
                        cust_email = lead.cust_email,
                        cust_concern_desc = lead.cust_concern_desc,
                        created_date = DateTime.Now,
                        cus_status = 1
                    };
                    var result = await globalContext.Customer_ContactUs.AddAsync(obj);
                    await globalContext.SaveChangesAsync();
                    return "Message sended Successfully";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<List<GetAllCustomer_ContactUs>> GetAllCustomer_ContactUs()
        {
            var query = (from a in db.Customer_ContactUs
                         where a.cust_id != 0
                         orderby a.cust_id descending
                         select new GetAllCustomer_ContactUs
                         {
                             cust_id = a.cust_id,
                             cust_name = a.cust_name,
                             cust_phone_no = a.cust_phone_no,
                             cust_email = a.cust_email,
                             cust_concern_desc = a.cust_concern_desc,
                             cus_status = a.cus_status,
                         });
            return await query.ToListAsync();
        }


        public async Task<GetCustomer_ContactUsById> GetCustomer_ContactUsById(string cust_phone_no)
        {
            var query = (from a in db.Customer_ContactUs
                         where a.cust_id != 0 && a.cust_phone_no == cust_phone_no
                         orderby a.cust_id descending
                         select new GetCustomer_ContactUsById
                         {
                             cust_id = a.cust_id,
                             cust_name = a.cust_name,
                             cust_phone_no = a.cust_phone_no,
                             cust_email = a.cust_email,
                             cust_concern_desc = a.cust_concern_desc,
                             cus_status = a.cus_status,
                         }).FirstOrDefaultAsync();
            return await query;
        }




    }
}
