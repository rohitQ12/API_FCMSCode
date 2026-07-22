using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.PaymentIRepository;
using GlobalApi.Models.Payment;
using GlobalApi.Repository.MasterRepository;
using System.Data;
using GlobalApi.Models.Master;
using System.Net;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Drawing;
using NLog.Fluent;
using System;
using Razorpay.Api;

namespace GlobalApi.Repository.PaymentRepository
{
    public class PaymentRepository : IPayment
    {

        private ADO_Configrations ado_Configurations;
        HttpClient client = null;

        private readonly IConfigurationRoot configurationRoot = null!;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly GlobalContext db;
        public PaymentRepository()
        {

            ado_Configurations = new ADO_Configrations();
            client = new HttpClient();
            primarykeyvalue = new Primarykeyvalue();
            this.db = new GlobalContext();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
        }


        //okay
        public async Task<MerchantOrderModels> ProcessMerchantOrder(CustomerOrderData customerOrderData, string razorpay_key, string razorpay_secret)
        {

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //Generate random receipt number for order
                Random randomNum = new Random();
                string cust_order_id = "LMS" + randomNum.Next(10000000, 100000000).ToString();
                string currency = "INR";
                string payment_gateway_name = "razorpay";
                string description_remarks = "Order request for buying course and package";

                //LMS
                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(razorpay_key, razorpay_secret);

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", customerOrderData.course_fee * 100);
                options.Add("currency", currency);
                options.Add("receipt", cust_order_id);
                options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
                //options.Add("Notes", "Test Payment of Merchant");
                Razorpay.Api.Order orderResponse = client.Order.Create(options);
                //string orderId = orderResponse["id"].ToString();

                // Convert Unix timestamp to DateTime
                long timestamp = Convert.ToInt32(orderResponse.Attributes["created_at"]);// 1723031342;
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                DateTime created_at_server = dateTimeOffset.DateTime;


                string entity = orderResponse.Attributes["entity"];
                string status = orderResponse.Attributes["status"];
                int attempts = Convert.ToInt32(orderResponse.Attributes["attempts"]);
                string razor_order_id = orderResponse.Attributes["id"];

                MerchantOrderModels merchant_order = new MerchantOrderModels
                {
                    users_id = customerOrderData.users_id,
                    users_code = customerOrderData.users_code,
                    users_type = customerOrderData.users_type,
                    users_order_id = cust_order_id,
                    razorpay_order_id = razor_order_id,
                    razorpay_key = razorpay_key,
                    course_id = customerOrderData.course_id,
                    course_fee = customerOrderData.course_fee,
                    currency_type = currency,
                    users_name = customerOrderData.users_name,
                    users_email = customerOrderData.users_email,
                    contact_number = customerOrderData.contact_number,
                    users_address = customerOrderData.users_address,
                    postal_code = customerOrderData.postal_code,
                    country_name = customerOrderData.country_name,
                    state_name = customerOrderData.state_name,
                    city_name = customerOrderData.city_name,
                    description_remarks = description_remarks, // "Order by Merchant"
                    payment_gateway = payment_gateway_name,
                    entity_type = await CapitalizeFirstLetter(entity),
                    order_status = await CapitalizeFirstLetter(status),
                    no_of_attempts = attempts,
                    created_at_server = created_at_server
                };

                var result = await SaveMerchantOrderData(merchant_order);

                return await Task.FromResult(merchant_order);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MerchantOrderModels> ProcessMerchantOrder_Ind(CustomerOrderData_Ind customerOrderData, string razorpay_key, string razorpay_secret)
        {

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;               

                string currency = "INR";
                string payment_gateway_name = "razorpay";
                string description_remarks = "Order request for buying course and package.";
                
                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(razorpay_key, razorpay_secret);

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", customerOrderData.course_fee * 100);
                options.Add("currency", currency);
                options.Add("receipt", customerOrderData.users_order_id);
                options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
                //options.Add("Notes", "Test Payment of Merchant");
                Razorpay.Api.Order orderResponse = client.Order.Create(options);
                //string orderId = orderResponse["id"].ToString();

                // Convert Unix timestamp to DateTime
                long timestamp = Convert.ToInt32(orderResponse.Attributes["created_at"]);// 1723031342;
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                DateTime created_at_server = dateTimeOffset.DateTime;

                string entity = orderResponse.Attributes["entity"];
                string status = orderResponse.Attributes["status"];
                int attempts = Convert.ToInt32(orderResponse.Attributes["attempts"]);
                string razor_order_id = orderResponse.Attributes["id"];

                MerchantOrderModels merchant_order = new MerchantOrderModels
                {
                    //users_id = customerOrderData.users_id,
                    //users_code = customerOrderData.users_code,
                    users_type = customerOrderData.users_type,
                    users_order_id = customerOrderData.users_order_id,
                    razorpay_order_id = razor_order_id,
                    razorpay_key = razorpay_key,
                    course_id = customerOrderData.course_id,
                    course_fee = customerOrderData.course_fee,
                    currency_type = currency,
                    users_name = customerOrderData.users_name,
                    users_email = customerOrderData.users_email,
                    contact_number = customerOrderData.contact_number,
                    users_address = customerOrderData.users_address,
                    postal_code = customerOrderData.postal_code,
                    country_name = customerOrderData.country_name,
                    state_name = customerOrderData.state_name,
                    city_name = customerOrderData.city_name,
                    description_remarks = description_remarks, // "Order by Merchant"
                    payment_gateway = payment_gateway_name,
                    entity_type = await CapitalizeFirstLetter(entity),
                    order_status = await CapitalizeFirstLetter(status),
                    no_of_attempts = attempts,
                    created_at_server = created_at_server
                };

                var result = await SaveMerchantOrderData(merchant_order);

                return await Task.FromResult(merchant_order);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SaveMerchantOrderData(MerchantOrderModels merchant_order)
        {
            try
            {

                Microsoft.Data.SqlClient.SqlConnection con = ado_Configurations.connection();
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = "usp_SavePayOrderData";
                cmd.Parameters.AddWithValue("@users_id", merchant_order.users_id);
                cmd.Parameters.AddWithValue("@users_code", merchant_order.users_code);
                cmd.Parameters.AddWithValue("@users_type", merchant_order.users_type);
                cmd.Parameters.AddWithValue("@users_order_id", merchant_order.users_order_id);
                cmd.Parameters.AddWithValue("@razorpay_key", merchant_order.razorpay_key);
                cmd.Parameters.AddWithValue("@razorpay_order_id", merchant_order.razorpay_order_id);
                cmd.Parameters.AddWithValue("@entity_type", merchant_order.entity_type);
                cmd.Parameters.AddWithValue("@course_id", merchant_order.course_id);
                cmd.Parameters.AddWithValue("@course_fee", merchant_order.course_fee);
                cmd.Parameters.AddWithValue("@currency_type", merchant_order.currency_type);
                cmd.Parameters.AddWithValue("@users_name", merchant_order.users_name);
                cmd.Parameters.AddWithValue("@users_email", merchant_order.users_email);
                cmd.Parameters.AddWithValue("@contact_number", merchant_order.contact_number);
                cmd.Parameters.AddWithValue("@users_address", merchant_order.users_address);
                cmd.Parameters.AddWithValue("@postal_code", merchant_order.postal_code);
                cmd.Parameters.AddWithValue("@country_name", merchant_order.country_name);
                cmd.Parameters.AddWithValue("@state_name", merchant_order.state_name);
                cmd.Parameters.AddWithValue("@city_name", merchant_order.city_name);
                cmd.Parameters.AddWithValue("@no_of_attempts", merchant_order.no_of_attempts);
                cmd.Parameters.AddWithValue("@created_at_server", merchant_order.created_at_server);
                cmd.Parameters.AddWithValue("@order_status", merchant_order.order_status);
                cmd.Parameters.AddWithValue("@description_remarks", merchant_order.description_remarks);
                cmd.Parameters.AddWithValue("@payment_gateway", merchant_order.payment_gateway);


                await con.OpenAsync();
                int i = cmd.ExecuteNonQuery();
                await con.CloseAsync();

                if (i >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //okay
        public async Task<CustomerOrderData> GetUsersOrderDetailById(string users_order_id)
        {
            try
            {

                Microsoft.Data.SqlClient.SqlConnection con = ado_Configurations.connection();
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand();
                Microsoft.Data.SqlClient.SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = "usp_GetUsersOrderDetail_ById";
                cmd.Parameters.AddWithValue("@users_order_id", users_order_id);

                CustomerOrderData orderDetail = null;
                con.Open();
                dr = cmd.ExecuteReader(); //Execute query individual data..
                while (dr.Read())
                {
                    orderDetail = new CustomerOrderData();
                    orderDetail.users_id = Convert.ToString(dr["users_id"]);
                    orderDetail.users_code = Convert.ToString(dr["users_code"]);
                    orderDetail.users_type = Convert.ToString(dr["users_type"]);
                    orderDetail.users_order_id = Convert.ToString(dr["users_order_id"]);
                    orderDetail.razorpay_order_id = Convert.ToString(dr["razorpay_order_id"]);
                    orderDetail.razorpay_key = Convert.ToString(dr["razorpay_key"]);
                    orderDetail.course_id = Convert.ToInt32(dr["course_id"]);
                    orderDetail.course_fee = Convert.ToInt32(dr["course_fee"]);
                    orderDetail.currency_type = Convert.ToString(dr["currency_type"]);
                    orderDetail.users_name = Convert.ToString(dr["users_name"]);
                    orderDetail.users_email = Convert.ToString(dr["users_email"]);
                    orderDetail.contact_number = Convert.ToString(dr["contact_number"]);
                    orderDetail.postal_code = Convert.ToInt32(dr["postal_code"]);
                    orderDetail.users_address = Convert.ToString(dr["users_address"]);
                    orderDetail.country_name = Convert.ToString(dr["country_name"]);
                    orderDetail.state_name = Convert.ToString(dr["state_name"]);
                    orderDetail.city_name = Convert.ToString(dr["city_name"]);
                    orderDetail.description_remarks = Convert.ToString(dr["description_remarks"]);

                }
                con.Close();
                return orderDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<string> CancelOrderProcess(string users_order_id)
        {

            //MerchantPaymentModels paymentData = new MerchantPaymentModels();
            try
            {
                //start here               
                    //delete all the datas
                    //fail -> revert back make reject status

                    var userData = db.Users_Payment
                    .Where(u => u.users_order_id == users_order_id)
                    .Select(u => new { u.users_id, u.users_type })
                    .FirstOrDefault();

                    if (userData.users_type == "Institition")
                    {


                        var insId = db.Institution
                        .Where(i => i.Ins_UserID == userData.users_id)
                        .Select(i => i.Ins_Id)
                        .FirstOrDefault();

                        //delete Student_Courses table based on Ins_id_fk = 432
                        var ins_stud = db.student_courses
                       .FirstOrDefault(a => a.Ins_id_fk == insId);

                        if (ins_stud != null)
                        {
                            db.student_courses.Remove(ins_stud);
                            db.SaveChanges();
                        }

                        //delete institution table based on Ins_UserID = 'e8a1bfc8-5f84-4742-b08a-00c054452ca2'
                        var inst = db.Institution
                       .FirstOrDefault(i => i.Ins_UserID == userData.users_id);

                        if (inst != null)
                        {
                            db.Institution.Remove(inst);
                            db.SaveChanges();
                        }


                    }

                    if (userData.users_type == "Corporate")
                    {

                        var corpId = db.Corporate
                       .Where(co => co.CO_UserId == userData.users_id)
                       .Select(co => co.CO_Id)
                       .FirstOrDefault();

                        //delete Student_Courses table based on Co_id_fk = 432
                        var corp_stud = db.student_courses
                       .FirstOrDefault(b => b.Co_id_fk == corpId);

                        if (corp_stud != null)
                        {
                            db.student_courses.Remove(corp_stud);
                            db.SaveChanges();
                        }

                        //delete Corporate table based on CO_UserId = 'e8a1bfc8-5f84-4742-b08a-00c054452ca2'
                        var corp = db.Corporate
                       .FirstOrDefault(c => c.CO_UserId == userData.users_id);

                        if (corp != null)
                        {
                            db.Corporate.Remove(corp);
                            db.SaveChanges();
                        }


                    }

                    if (userData.users_type == "Individual")
                    {


                        var indId = db.Individual
                      .Where(nd => nd.Ind_UserID == userData.users_id)
                      .Select(nd => nd.Ind_Id)
                      .FirstOrDefault();

                        //delete Student_Courses table based on Stu_id_fk = 432
                        var indv_stud = db.student_courses
                       .FirstOrDefault(t => t.Stu_id_fk == indId);

                        if (indv_stud != null)
                        {
                            db.student_courses.Remove(indv_stud);
                            db.SaveChanges();
                        }


                        //delete Corporate table based on CO_UserId = 'e8a1bfc8-5f84-4742-b08a-00c054452ca2'
                        var ind = db.Individual
                       .FirstOrDefault(n => n.Ind_UserID == userData.users_id);

                        if (ind != null)
                        {
                            db.Individual.Remove(ind);
                            db.SaveChanges();
                        }

                    }


                    //delete aspnet user table based on id = 'e8a1bfc8-5f84-4742-b08a-00c054452ca2'
                    var user = db.Users
                    .FirstOrDefault(u => u.Id == userData.users_id);

                    if (user != null)
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                    }


                    //delete Users_Payment table based on users_id = 'e8a1bfc8-5f84-4742-b08a-00c054452ca2'
                    var upaymt = db.Users_Payment
                   .FirstOrDefault(p => p.users_id == userData.users_id);

                    if (upaymt != null)
                    {
                        db.Users_Payment.Remove(upaymt);
                        db.SaveChanges();
                    }


                //end here   





                return "Cancelled Payment";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        //okay
        public async Task<string> CompleteOrderProcess(VerifySignature verifysignature, string razorpay_key, string razorpay_secret)
        {

            MerchantPaymentModels paymentData = new MerchantPaymentModels();
            try
            {
                string paymentId = verifysignature.razorpay_payment_id;

                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(razorpay_key, razorpay_secret);
                Razorpay.Api.Payment ObjPay = client.Payment.Fetch(paymentId);

                // This code is for capture the payment 
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", ObjPay.Attributes["amount"]);
                Razorpay.Api.Payment paymentCaptured = ObjPay.Capture(options);
                int pay_amt = Convert.ToInt32(paymentCaptured.Attributes["amount"]);
                pay_amt = pay_amt / 100;

                paymentData.razorpay_payment_id = Convert.ToString(paymentCaptured.Attributes["id"]);
                paymentData.users_order_id = Convert.ToString(paymentCaptured.Attributes["notes"]["merchant_order_id"]);
                paymentData.course_fee = pay_amt;

                string entity_type = Convert.ToString(paymentCaptured.Attributes["entity"]);
                paymentData.entity_type = await CapitalizeFirstLetter(entity_type);
                paymentData.is_international = Convert.ToBoolean(paymentCaptured.Attributes["international"]);
                paymentData.payment_method = Convert.ToString(paymentCaptured.Attributes["method"]);

                string pay_status = Convert.ToString(paymentCaptured.Attributes["status"]);
                if (pay_status == "captured")
                {
                    pay_status = "success";
                    //success: call here save consulation data 
                }               

                paymentData.payment_status = await CapitalizeFirstLetter(pay_status);

                paymentData.amount_refunded = Convert.ToInt32(paymentCaptured.Attributes["amount_refunded"]);
                paymentData.refund_status = Convert.ToString(paymentCaptured.Attributes["refund_status"]);
                paymentData.is_captured = Convert.ToBoolean(paymentCaptured.Attributes["captured"]);
                paymentData.card_id = Convert.ToString(paymentCaptured.Attributes["card"]["id"]);
                paymentData.card_entity_type = Convert.ToString(paymentCaptured.Attributes["card"]["entity"]);
                paymentData.card_holder_name = Convert.ToString(paymentCaptured.Attributes["card"]["name"]);
                paymentData.last4 = Convert.ToString(paymentCaptured.Attributes["card"]["last4"]);
                paymentData.card_network = Convert.ToString(paymentCaptured.Attributes["card"]["network"]);
                paymentData.card_type = Convert.ToString(paymentCaptured.Attributes["card"]["type"]);
                paymentData.card_issuer = Convert.ToString(paymentCaptured.Attributes["card"]["card_issuer"]);
                paymentData.is_card_international = Convert.ToBoolean(paymentCaptured.Attributes["card"]["international"]);
                paymentData.is_card_emi = Convert.ToBoolean(paymentCaptured.Attributes["card"]["emi"]);
                paymentData.card_sub_type = Convert.ToString(paymentCaptured.Attributes["card"]["sub_type"]);
                paymentData.bank_name = Convert.ToString(paymentCaptured.Attributes["bank"]);
                paymentData.wallet = Convert.ToString(paymentCaptured.Attributes["wallet"]);
                paymentData.vpa = Convert.ToString(paymentCaptured.Attributes["vpa"]);

                string c_fee = Convert.ToString(paymentCaptured.Attributes["fee"]);
                if (string.IsNullOrEmpty(c_fee))
                {
                    c_fee = "0";
                }
                paymentData.fee = Convert.ToInt32(c_fee);

                string c_tax = Convert.ToString(paymentCaptured.Attributes["tax"]);
                if (string.IsNullOrEmpty(c_fee))
                {
                    c_fee = "0";
                }
                paymentData.tax = Convert.ToInt32(c_tax);
                paymentData.error_code = Convert.ToString(paymentCaptured.Attributes["error_code"]);
                paymentData.error_description = Convert.ToString(paymentCaptured.Attributes["error_description"]);
                paymentData.error_source = Convert.ToString(paymentCaptured.Attributes["error_source"]);
                paymentData.error_step = Convert.ToString(paymentCaptured.Attributes["error_step"]);
                paymentData.error_reason = Convert.ToString(paymentCaptured.Attributes["error_reason"]);
                paymentData.auth_code = Convert.ToString(paymentCaptured.Attributes["acquirer_data"]["auth_code"]);

                // Convert Unix timestamp to DateTime
                long timestamp = Convert.ToInt32(paymentCaptured.Attributes["created_at"]);// 1723031342;
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                DateTime payment_created_at_server = dateTimeOffset.DateTime;
                paymentData.payment_created_at = payment_created_at_server;

                // here save payment details call funcation
                var result = await SaveMerchantPayment(paymentData);

                return paymentCaptured.Attributes["status"];
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }


        public async Task<bool> SaveMerchantPayment(MerchantPaymentModels payment)
        {
            try
            {
                Microsoft.Data.SqlClient.SqlConnection con = ado_Configurations.connection();
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = "SaveUsersPaymentDetails";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@users_order_id", payment.users_order_id);
                cmd.Parameters.AddWithValue("@razorpay_payment_id", payment.razorpay_payment_id);
                cmd.Parameters.AddWithValue("@entity_type", payment.entity_type);
                cmd.Parameters.AddWithValue("@course_fee", payment.course_fee);
                cmd.Parameters.AddWithValue("@payment_status", payment.payment_status);
                cmd.Parameters.AddWithValue("@is_international", payment.is_international);
                cmd.Parameters.AddWithValue("@payment_method", payment.payment_method);
                cmd.Parameters.AddWithValue("@amount_refunded", payment.amount_refunded);
                cmd.Parameters.AddWithValue("@refund_status", payment.refund_status);
                cmd.Parameters.AddWithValue("@is_captured", payment.is_captured);
                cmd.Parameters.AddWithValue("@card_id", payment.card_id);
                cmd.Parameters.AddWithValue("@card_entity_type", payment.card_entity_type);
                cmd.Parameters.AddWithValue("@card_holder_name", payment.card_holder_name);
                cmd.Parameters.AddWithValue("@last4", payment.last4);
                cmd.Parameters.AddWithValue("@card_network", payment.card_network);
                cmd.Parameters.AddWithValue("@card_type", payment.card_type);
                cmd.Parameters.AddWithValue("@card_issuer", payment.card_issuer);
                cmd.Parameters.AddWithValue("@is_card_international", payment.is_card_international);
                cmd.Parameters.AddWithValue("@is_card_emi", payment.is_card_emi);
                cmd.Parameters.AddWithValue("@card_sub_type", payment.card_sub_type);
                cmd.Parameters.AddWithValue("@bank_name", payment.bank_name);
                cmd.Parameters.AddWithValue("@wallet", payment.wallet);
                cmd.Parameters.AddWithValue("@vpa", payment.vpa);
                cmd.Parameters.AddWithValue("@fee", payment.fee);
                cmd.Parameters.AddWithValue("@tax", payment.tax);
                cmd.Parameters.AddWithValue("@error_code", payment.error_code);
                cmd.Parameters.AddWithValue("@error_description", payment.error_description);
                cmd.Parameters.AddWithValue("@error_source", payment.error_source);
                cmd.Parameters.AddWithValue("@error_step", payment.error_step);
                cmd.Parameters.AddWithValue("@error_reason", payment.error_reason);
                cmd.Parameters.AddWithValue("@auth_code", payment.auth_code);
                cmd.Parameters.AddWithValue("@payment_created_at", payment.payment_created_at);

                con.Open();
                var result = await cmd.ExecuteNonQueryAsync();
                con.Close();

                if (result >= 1)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw new Exception("error");
            }

        }


        public async Task<MerchantPaymentModels> GetUsersPaymentDetailById(string razorpay_payment_id, string razorpay_key, string razorpay_secret)
        {

            MerchantPaymentModels paymentData = null; // Initialize as null
            try
            {
                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(razorpay_key, razorpay_secret);
                Razorpay.Api.Payment payment = client.Payment.Fetch(razorpay_payment_id);
                int pay_amt = Convert.ToInt32(payment.Attributes["amount"]);
                pay_amt = pay_amt / 100;
                string pay_status = Convert.ToString(payment.Attributes["status"]);
                if (pay_status == "captured")
                {
                    pay_status = "success";
                }


                pay_status = await CapitalizeFirstLetter(pay_status);

                //Convert Unix timestamp to DateTime                
                long timestamp = Convert.ToInt32(payment.Attributes["created_at"]);// 1723031342;
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                DateTime payment_created_at_server = dateTimeOffset.DateTime;

                paymentData = new MerchantPaymentModels
                {
                    razorpay_payment_id = razorpay_payment_id,
                    users_order_id = Convert.ToString(payment.Attributes["notes"]["merchant_order_id"]),
                    course_fee = pay_amt,
                    currency_type = Convert.ToString(payment.Attributes["currency"]),
                    entity_type = Convert.ToString(payment.Attributes["entity"]),
                    is_international = Convert.ToBoolean(payment.Attributes["international"]),
                    payment_method = Convert.ToString(payment.Attributes["method"]),
                    amount_refunded = Convert.ToInt32(payment.Attributes["amount_refunded"]),
                    refund_status = Convert.ToString(payment.Attributes["refund_status"]),
                    is_captured = Convert.ToBoolean(payment.Attributes["is_captured"]),
                    payment_status = pay_status,
                    payment_description = Convert.ToString(payment.Attributes["description"]),
                    card_id = Convert.ToString(payment.Attributes["card_id"]),
                    //card_id = Convert.ToString(payment.Attributes["card"]["id"]),
                    //card_entity_type = Convert.ToString(payment.Attributes["card"]["entity"]),
                    //card_holder_name = Convert.ToString(payment.Attributes["card"]["name"]),
                    //card_network = Convert.ToString(payment.Attributes["card"]["network"]),
                    //card_type = Convert.ToString(payment.Attributes["card"]["type"]),
                    //card_issuer = Convert.ToString(payment.Attributes["card"]["card_issuer"]),
                    //card_sub_type = Convert.ToString(payment.Attributes["card"]["sub_type"]),
                    //last4 = Convert.ToString(payment.Attributes["card"]["last4"]),
                    
                    bank_name = Convert.ToString(payment.Attributes["bank"]),
                    wallet = Convert.ToString(payment.Attributes["wallet"]),
                    vpa = Convert.ToString(payment.Attributes["vpa"]),
                    users_email = Convert.ToString(payment.Attributes["email"]),
                    contact_number = Convert.ToString(payment.Attributes["contact"]),
                    fee = Convert.ToInt32(payment.Attributes["fee"]),
                    tax = Convert.ToInt32(payment.Attributes["tax"]),
                    error_code = Convert.ToString(payment.Attributes["error_code"]),
                    error_description = Convert.ToString(payment.Attributes["error_description"]),
                    error_source = Convert.ToString(payment.Attributes["error_source"]),
                    error_step = Convert.ToString(payment.Attributes["error_step"]),
                    error_reason = Convert.ToString(payment.Attributes["error_reason"]),
                    auth_code = Convert.ToString(payment.Attributes["acquirer_data"]["auth_code"]),
                    payment_created_at = payment_created_at_server


                };
            }
            catch (Exception ex)
            {
                // Log the exception details if needed
                // Consider logging the error here
                // Example: _logger.LogError(ex, "Error fetching payment details");

                // Return null to indicate that no data was retrieved due to an error
                paymentData = null;
            }
            return paymentData;
        }


        //okay
        public async Task<int> RoundCourseAmount(string course_amount)
        {
            // Convert string to decimal
            if (decimal.TryParse(course_amount, out decimal fee))
            {
                // Round to nearest integer
                //int cons_amt= (int)Math.Round(fee);
                return (int)Math.Round(fee);
            }
            else
            {
                // Handle invalid input
                throw new ArgumentException("Invalid course amount value.");
            }
        }

        //okay
        public async Task<string> CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Capitalize the first letter and keep the rest of the string unchanged
            return char.ToUpper(input[0]) + input.Substring(1);
        }

    }
}
