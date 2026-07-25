using GlobalApi.GlobalClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using GlobalApi.Repository.PaymentRepository;
using GlobalApi.IRepository.PaymentIRepository;
using GlobalApi.Models.Payment;
using GlobalApi.Data;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Controllers.PaymentController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public readonly IPayment _repository;
        public readonly FindUserId findUserId;
        HttpClient client = null;
        private IConfiguration Configuration;
        ISMSService objSMSService;
        private readonly GlobalContext db;

        public PaymentController(IConfiguration _configuration, ISMSService objSMSService)
        {
            this._repository = new PaymentRepository();
            this.findUserId = new FindUserId();
            client = new HttpClient();
            Configuration = _configuration;
            this.objSMSService = objSMSService;
            this.db = new GlobalContext();
        }

        [HttpPost, Route("ProcessRequestOrder")]
        [AllowAnonymous]
        public async Task<ActionResult> ProcessRequestOrder([FromBody] Users_PayReq payReq)
        {
            try
            {
                if (payReq.users_name == null)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "User Name is required." });
                }

                var user_id = await this.findUserId.FindUserIdFromUserName(payReq.users_name);
                if (user_id == null)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 404, msg_status = "User Id is not registered." });
                }

                var role_name = await this.findUserId.FindRoleNameFromUserId(user_id);
                if (role_name == null)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 404, msg_status = "Role Name not found with User Name :" + payReq.users_name });
                }

                if (payReq.course_id == 0)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Course Id is required." });
                }
                //instituion
                if (role_name != "Institition" && role_name != "Corporate" && role_name != "Individual")
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 404, msg_status = "User Id not registered with any of the role names: Institition, Corporate, Individual." });
                }

                var cp_charges = await (from c in this.db.Course_Package
                                        where c.cp_id == payReq.course_id
                                        select c.cp_amount)
                      .FirstOrDefaultAsync();

                if (cp_charges == null || cp_charges == 0)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Minimum amount must be 1 rupees." });
                }

                decimal finalAmountFee;
                int no_of_subs = 0;
                no_of_subs = Convert.ToInt32(payReq.no_of_subscription);
                if (no_of_subs == 0)
                {
                    no_of_subs = 1;
                }

                int cp_amount = await _repository.RoundCourseAmount(Convert.ToString(cp_charges));

                if (cp_amount < 1)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Minimum course amount must be 1 rupees." });
                }
                if (payReq.course_id == 305 && role_name == "Institition" && no_of_subs >= 10)
                {
                    // Apply a discount for Both Course
                    decimal discount = 36.66m;
                    decimal discountAmount = cp_amount - (cp_amount * discount / 100);
                    int finalAmount = (int)Math.Round(discountAmount, MidpointRounding.AwayFromZero);

                    int totalDiscount_course_fee = finalAmount * no_of_subs;
                    finalAmountFee = totalDiscount_course_fee;
                }
                else if (payReq.course_id == 305 && role_name == "Corporate" && no_of_subs == 10)
                {

                    decimal CorpDiscount = 20m;
                    decimal CorpDiscountAmount = cp_amount - (cp_amount * CorpDiscount / 100);
                    int CorpFinalAmount = (int)Math.Round(CorpDiscountAmount, MidpointRounding.AwayFromZero);
                    int CorpTotalDiscount_fee = CorpFinalAmount * no_of_subs;
                    finalAmountFee = CorpTotalDiscount_fee;

                }
                else
                {
                    int total_course_fee = cp_amount * no_of_subs;
                    finalAmountFee = total_course_fee;
                }



                string razorpay_key = this.Configuration.GetSection("PayAPISettings")["razorpay_key_id"];
                string razorpay_secret = this.Configuration.GetSection("PayAPISettings")["razorpay_key_secret"];
                CustomerOrderData customerOrderData = null; // Initialize the variable to null

                //start here Institition role users
                if (role_name == "Institition")
                {
                    var customer_data = from u in db.Users
                                        join i in db.Institution on u.Id equals i.Ins_UserID
                                        join c in db.Countries on i.Ins_Country_Id_FK equals c.cntry_id
                                        join s in db.States on i.Ins_ST_Id_FK equals s.stat_id
                                        where u.Inactive == "Y" && u.UserName == payReq.users_name
                                        select new
                                        {
                                            users_id = u.Id,
                                            users_code = i.Ins_InstitutionCode,
                                            users_type = role_name,
                                            customer_name = i.Ins_InstitutionName,
                                            customer_email = i.Ins_InstitutionEmail,
                                            contact_number = i.Ins_InstitutionPhoneNo,
                                            customer_address = i.Ins_InstitutionAddress,
                                            postal_code = i.Ins_PostalCode,
                                            customer_country = c.country_name,
                                            customer_state = s.state_name
                                        };
                    // Fetch the single record
                    var customer = await customer_data.FirstOrDefaultAsync(); // Safely fetch a single row or null

                    if (customer == null)
                    {
                        return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Customer data could not be created for the provided role :" + role_name });

                    }

                    // Directly access the properties
                    customerOrderData = new CustomerOrderData
                    {
                        users_id = customer.users_id,
                        users_code = customer.users_code,
                        users_type = customer.users_type,
                        users_name = customer.customer_name,
                        users_email = customer.customer_email,
                        contact_number = customer.contact_number,
                        users_address = customer.customer_address,
                        postal_code = customer.postal_code,
                        country_name = customer.customer_country,
                        state_name = customer.customer_state,
                        city_name = "Bengaluru", //pass here dynamic city
                        course_id = payReq.course_id,
                        course_fee = Convert.ToInt32(finalAmountFee) // cp_amount
                    };


                }

                //end here Institition role users

                //start here Corporate role users
                if (role_name == "Corporate")
                {
                    var customer_data = from u in db.Users
                                        join i in db.Corporate on u.Id equals i.CO_UserId
                                        join c in db.Countries on i.CO_Country_Id_FK equals c.cntry_id
                                        join s in db.States on i.CO_ST_Id_FK equals s.stat_id
                                        where u.Inactive == "N" && u.UserName == payReq.users_name
                                        select new
                                        {
                                            users_id = u.Id,
                                            users_code = i.CO_Code,
                                            users_type = role_name,
                                            customer_name = i.CO_Name,
                                            customer_email = i.CO_Email,
                                            contact_number = i.CO_MobileNumber,
                                            customer_address = i.CO_Address,
                                            postal_code = i.Co_PostalCode,
                                            customer_country = c.country_name,
                                            customer_state = s.state_name
                                        };
                    // Fetch the single record
                    var customer = await customer_data.FirstOrDefaultAsync(); // Safely fetch a single row or null

                    if (customer == null)
                    {
                        return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Customer data could not be created for the provided role :" + role_name });

                    }

                    // Directly access the properties
                    customerOrderData = new CustomerOrderData
                    {
                        users_id = customer.users_id,
                        users_code = customer.users_code,
                        users_type = customer.users_type,
                        users_name = customer.customer_name,
                        users_email = customer.customer_email,
                        contact_number = customer.contact_number,
                        users_address = customer.customer_address,
                        postal_code = customer.postal_code,
                        country_name = customer.customer_country,
                        state_name = customer.customer_state,
                        city_name = "Bengaluru", //pass here dynamic city
                        course_id = payReq.course_id,
                        course_fee = Convert.ToInt32(finalAmountFee) // cp_amount
                    };
                }
                //end here Corporate role users

                //here implement others user's role if required
                if (role_name == "Individual")
                {
                    var customer_data = from u in db.Users
                                        join i in db.Individual on u.Id equals i.Ind_UserID
                                        join c in db.Countries on i.Ind_Country_Id_FK equals c.cntry_id
                                        join s in db.States on i.Ind_ST_Id_FK equals s.stat_id
                                        where u.Inactive == "N" && u.UserName == payReq.users_name
                                        select new
                                        {
                                            users_id = u.Id,
                                            users_code = i.Ind_code,
                                            users_type = role_name,
                                            customer_name = i.Ind_Name,
                                            customer_email = i.Ind_Email,
                                            contact_number = i.Ind_MobileNumber,
                                            customer_address = "",
                                            postal_code = 0,
                                            customer_country = c.country_name,
                                            customer_state = s.state_name
                                        };
                    // Fetch the single record
                    var customer = await customer_data.FirstOrDefaultAsync(); // Safely fetch a single row or null

                    if (customer == null)
                    {
                        return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Customer data could not be created for the provided role :" + role_name });

                    }

                    // Directly access the properties
                    customerOrderData = new CustomerOrderData
                    {
                        users_id = customer.users_id,
                        users_code = customer.users_code,
                        users_type = customer.users_type,
                        users_name = customer.customer_name,
                        users_email = customer.customer_email,
                        contact_number = customer.contact_number,
                        users_address = customer.customer_address,
                        postal_code = customer.postal_code,
                        country_name = customer.customer_country,
                        state_name = customer.customer_state,
                        city_name = "Bengaluru", //pass here dynamic city
                        course_id = payReq.course_id,
                        course_fee = Convert.ToInt32(finalAmountFee) // cp_amount
                    };
                }

                //Process the merchant order
                var merchantOrder = await _repository.ProcessMerchantOrder(customerOrderData, razorpay_key, razorpay_secret);

                if (merchantOrder != null)
                {
                    return Ok(merchantOrder);

                }

                return BadRequest(new RazorOrderReqMsg
                {
                    msg_code = 400,
                    msg_status = "Failed to fetch the order data."
                });

            }

            catch (Exception ex)
            {
                return BadRequest(new RazorOrderReqMsg { msg_code = 500, msg_status = "Exception: " + ex.Message });
            }


        }


        //process request order for individual -> saheb
        [HttpPost, Route("ProcessRequestOrder_Ind")]
        [AllowAnonymous]
        public async Task<ActionResult> ProcessRequestOrder_Ind([FromBody] IndOrderData payReq)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(payReq.users_type))
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "User Type is required." });
                }


                if (string.IsNullOrWhiteSpace(payReq.users_name))
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "User Name is required." });
                }

                if (string.IsNullOrWhiteSpace(payReq.users_email))
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Email is required." });
                }

                if (string.IsNullOrWhiteSpace(payReq.contact_number))
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Phone Number is required." });
                }

                if (payReq.course_id <= 0)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Course Id is required." });
                }

                var cp_charges = await this.db.Course_Package
                                              .Where(c => c.cp_id == payReq.course_id)
                                              .Select(c => (decimal?)c.cp_amount) // Ensuring nullable decimal handling
                                              .FirstOrDefaultAsync();

                if (!cp_charges.HasValue || cp_charges.Value <= 0)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Minimum amount must be 1 rupee." });
                }

                int cp_amount = await _repository.RoundCourseAmount(Convert.ToString(cp_charges));

                if (cp_amount <= 0)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Minimum course amount must be 1 rupees." });
                }


                string razorpay_key = this.Configuration.GetSection("PayAPISettings")["razorpay_key_id"];
                string razorpay_secret = this.Configuration.GetSection("PayAPISettings")["razorpay_key_secret"];
                CustomerOrderData_Ind customerOrderData = null; // Initialize the variable to null               

                //Generate random unique receipt number for order
                Random randomNum = new Random();
                string cust_order_id = "LMS" + randomNum.Next(10000000, 100000000).ToString();
               
                customerOrderData = new CustomerOrderData_Ind
                {                   
                    users_type = payReq.users_type,
                    users_order_id = cust_order_id,
                    users_name = payReq.users_name,
                    users_email = payReq.users_email,
                    contact_number = payReq.contact_number,
                    users_address = "JP Nagar", //pass here dynamic data
                    postal_code = 560078, //pass here dynamic data
                    country_name = "India", //pass here dynamic data
                    state_name = "Karnataka", //pass here dynamic data
                    city_name = "Bengaluru", //pass here dynamic data
                    course_id = payReq.course_id,
                    course_fee = Convert.ToInt32(cp_amount) // cp_amount
                };


                //Process the merchant order -> Individual
                var merchantOrder = await _repository.ProcessMerchantOrder_Ind(customerOrderData, razorpay_key, razorpay_secret);

                if (merchantOrder != null)
                {
                    return Ok(merchantOrder);

                }

                return BadRequest(new RazorOrderReqMsg
                {
                    msg_code = 400,
                    msg_status = "Failed to fetch the order data."
                });

            }

            catch (Exception ex)
            {
                return BadRequest(new RazorOrderReqMsg { msg_code = 500, msg_status = "Exception: " + ex.Message });
            }


        }

        [HttpGet]
        [Route("GetUsersOrderDetailById/{users_order_id}")]
        public async Task<ActionResult> GetCustOrderDetailById(string users_order_id)
        {
            try
            {
                if (users_order_id == null)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Users order id parameter is required" });
                }
                var result = await _repository.GetUsersOrderDetailById(users_order_id);
                if (result == null)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Order data not found" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new RazorOrderReqMsg { msg_code = 500, msg_status = "Exception: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("CancelOrderProcess/{users_order_id}")]
        public async Task<ActionResult> CancelOrderProcess(string users_order_id)
        {
            try
            {
                if (users_order_id == null)
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Users order id parameter is required" });
                }
                var result = await _repository.CancelOrderProcess(users_order_id);
                return Ok(new RazorOrderReqMsg { msg_code = 200, msg_status = "Failed", msg_desc = result });
            }
            catch (Exception ex)
            {

                return BadRequest(new RazorOrderReqMsg { msg_code = 500, msg_status = "Exception: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("CompleteOrderProcess")]
        public async Task<ActionResult> CompleteOrderProcess(VerifySignature verifysignature)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(verifysignature.razorpay_payment_id))
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Failed", msg_desc = "Razorpay payment id is required." });
                }
                string razorpay_key = this.Configuration.GetSection("PayAPISettings")["razorpay_key_id"];
                string razorpay_secret = this.Configuration.GetSection("PayAPISettings")["razorpay_key_secret"];
                string PaymentMessage = await _repository.CompleteOrderProcess(verifysignature, razorpay_key, razorpay_secret);

                if (PaymentMessage == "captured")
                {

                    return Ok(new RazorOrderReqMsg { msg_code = 200, msg_status = "Success", msg_desc = "razor_pay :" + verifysignature.razorpay_order_id + " pay_id: " + verifysignature.razorpay_payment_id + " sig: " + verifysignature.razorpay_signature });
                }
                else
                {
                    return Ok(new RazorOrderReqMsg { msg_code = 200, msg_status = "Failed", msg_desc = PaymentMessage });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new RazorOrderReqMsg { msg_code = 500, msg_status = "Exception: " + ex.Message });
            }


        }


        //complete order process -> saheb
        [HttpPost]
        [Route("CompleteOrderProcess_Ind")]
        public async Task<ActionResult> CompleteOrderProcess_Ind(VerifySignature verifysignature)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(verifysignature.razorpay_payment_id))
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Failed", msg_desc = "Razorpay payment id is required." });
                }
                string razorpay_key = this.Configuration.GetSection("PayAPISettings")["razorpay_key_id"];
                string razorpay_secret = this.Configuration.GetSection("PayAPISettings")["razorpay_key_secret"];
                string PaymentMessage = await _repository.CompleteOrderProcess(verifysignature, razorpay_key, razorpay_secret);

                if (PaymentMessage == "captured")
                {

                    return Ok(new RazorOrderReqMsg { msg_code = 200, msg_status = "Success", msg_desc = "razor_pay :" + verifysignature.razorpay_order_id + " pay_id: " + verifysignature.razorpay_payment_id + " sig: " + verifysignature.razorpay_signature });
                }
                else
                {
                    return Ok(new RazorOrderReqMsg { msg_code = 200, msg_status = "Failed", msg_desc = PaymentMessage });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new RazorOrderReqMsg { msg_code = 500, msg_status = "Exception: " + ex.Message });
            }


        }



        [HttpGet]
        [Route("GetPaymentDetailById/{razorpay_payment_id}")]
        public async Task<ActionResult> GetCustPaymentDetailById(string razorpay_payment_id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(razorpay_payment_id))
                {
                    return BadRequest(new RazorOrderReqMsg { msg_code = 400, msg_status = "Razorpay payment id parameter is required" });
                }
                string razorpay_key = this.Configuration.GetSection("PayAPISettings")["razorpay_key_id"];
                string razorpay_secret = this.Configuration.GetSection("PayAPISettings")["razorpay_key_secret"];
                MerchantPaymentModels merchantPayment = await _repository.GetUsersPaymentDetailById(razorpay_payment_id, razorpay_key, razorpay_secret);

                if (merchantPayment != null)
                {
                    return Ok(new RazorPaymentReqMsg
                    {
                        msg_code = 200,
                        msg_status = "Payment data fetched successfully.",
                        payment_data = new List<MerchantPaymentModels> { merchantPayment }
                    });
                }

                return BadRequest(new RazorPaymentReqMsg
                {
                    msg_code = 400,
                    msg_status = "Failed to fetch the payment data.",
                    payment_data = null
                });
            }
            catch (Exception ex)
            {
                // Log the exception details if needed
                // Example: _logger.LogError(ex, "Exception occurred while fetching payment details");

                return BadRequest(new RazorPaymentReqMsg { msg_code = 500, msg_status = "Exception: " + ex.Message, payment_data = null });
            }
        }

    }


}
