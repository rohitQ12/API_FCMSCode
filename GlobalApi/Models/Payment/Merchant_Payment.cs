using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalApi.Models.Payment
{

    public class Users_Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int pay_id { get; set; }
        public string? users_id { get; set; }
        public string? users_code { get; set; }
        public string? users_type { get; set; }
        public string? users_order_id { get; set; }
        public string? razorpay_key { get; set; }
        public string? razorpay_order_id { get; set; }
        public string? razorpay_payment_id { get; set; }
        public string? entity_type { get; set; }
        public int? course_id { get; set; }
        public string? course_fee { get; set; }
        public string? currency_type { get; set; }
        public string? order_status { get; set; }
        public bool? is_international { get; set; }
        public string? payment_method { get; set; }
        public int? amount_refunded { get; set; }
        public string? refund_status { get; set; }
        public bool? is_captured { get; set; }
        public string? description_remarks { get; set; }
        public string? card_id { get; set; }
        public string? card_entity_type { get; set; }
        public string? card_holder_name { get; set; }
        public string? last4 { get; set; }
        public string? card_network { get; set; }
        public string? card_type { get; set; }
        public string? card_issuer { get; set; }
        public bool? is_card_international { get; set; }
        public bool? is_card_emi { get; set; }
        public string? card_sub_type { get; set; }
        public string? bank_name { get; set; }
        public string? wallet { get; set; }
        public string? vpa { get; set; }
        public string? users_name { get; set; }
        public string? users_email { get; set; }
        public string? contact_number { get; set; }
        public string? users_address { get; set; }
        public int? postal_code { get; set; }
        public string? country_name { get; set; }
        public string? state_name { get; set; }
        public string? city_name { get; set; }
        public int? fee { get; set; }
        public int? tax { get; set; }
        public string? error_code { get; set; }
        public string? error_description { get; set; }
        public string? error_source { get; set; }
        public string? error_step { get; set; }
        public string? error_reason { get; set; }
        public string? auth_code { get; set; }
        public DateTime? created_at_server { get; set; }
        public bool? refund_requested { get; set; }
        public DateTime? created_date { get; set; }
        public int? no_of_attempts { get; set; }
        public DateTime? payment_created_at { get; set; }
        public DateTime? payment_date { get; set; }
        public string? payment_gateway { get; set; }
    }

    public class Users_PayReq
    {
        public string users_name { get; set; }
        public int course_id { get; set; }
        public int? no_of_subscription { get; set; }

    }

    public class MerchantOrderModels
    {
        public string? users_id { get; set; }
        public string? users_code { get; set; }
        public string? users_type { get; set; }
        public string? users_order_id { get; set; }
        public string? razorpay_order_id { get; set; }
        public string? razorpay_key { get; set; }
        public int course_id { get; set; }
        public int course_fee { get; set; }
        public string? currency_type { get; set; }
        public string? users_name { get; set; }
        public string? users_email { get; set; }
        public string? contact_number { get; set; }
        public string? users_address { get; set; }
        public int? postal_code { get; set; }
        public string? country_name { get; set; }
        public string? state_name { get; set; }
        public string? city_name { get; set; }
        public string? description_remarks { get; set; }
        public string? payment_gateway { get; set; }
        public string? entity_type { get; set; }
        public string? order_status { get; set; }
        public int no_of_attempts { get; set; }
        public DateTime? created_at_server { get; set; }

    }

    public class CustomerOrderData
    {
        public string? users_id { get; set; }
        public string? users_code { get; set; }
        public string? users_type { get; set; }
        public string? users_order_id { get; set; }
        public string? razorpay_order_id { get; set; }
        public string? razorpay_key { get; set; }
        public int course_id { get; set; }
        public int course_fee { get; set; }
        public string? currency_type { get; set; }
        public string? users_name { get; set; }
        public string? users_email { get; set; }
        public string? contact_number { get; set; }
        public int? postal_code { get; set; }
        public string? users_address { get; set; }
        public string? country_name { get; set; }
        public string? state_name { get; set; }
        public string? city_name { get; set; }
        public string? description_remarks { get; set; }


    }


    public class CustomerOrderData_Ind
    {       
        public string? users_type { get; set; }
        public string users_order_id { get; set; }
        public string? razorpay_order_id { get; set; }
        public string? razorpay_key { get; set; }
        public int course_id { get; set; }
        public int course_fee { get; set; }
        public string? currency_type { get; set; }
        public string? users_name { get; set; }
        public string? users_email { get; set; }
        public string? contact_number { get; set; }
        public int? postal_code { get; set; }
        public string? users_address { get; set; }
        public string? country_name { get; set; }
        public string? state_name { get; set; }
        public string? city_name { get; set; }
        public string? description_remarks { get; set; }


    }

    public class MerchantPaymentModels
    {
        public string? razorpay_payment_id { get; set; }
        public string? users_order_id { get; set; }
        public string? entity_type { get; set; }
        public int course_fee { get; set; }
        public string? payment_status { get; set; }
        public bool? is_international { get; set; }
        public string? payment_method { get; set; }
        public int? amount_refunded { get; set; }
        public string? refund_status { get; set; }
        public bool? is_captured { get; set; }
        public string? card_id { get; set; }
        public string? card_entity_type { get; set; }
        public string? card_holder_name { get; set; }
        public string? last4 { get; set; }
        public string? card_network { get; set; }
        public string? card_type { get; set; }
        public string? card_issuer { get; set; }
        public bool? is_card_international { get; set; }
        public bool? is_card_emi { get; set; }
        public string? card_sub_type { get; set; }
        public string? bank_name { get; set; }
        public string? wallet { get; set; }
        public string? vpa { get; set; }
        public int? fee { get; set; }
        public int? tax { get; set; }
        public string? error_code { get; set; }
        public string? error_description { get; set; }
        public string? error_source { get; set; }
        public string? error_step { get; set; }
        public string? error_reason { get; set; }
        public string? auth_code { get; set; }
        public DateTime? payment_created_at { get; set; }
        public string? currency_type { get; set; }
        public string? payment_description { get; set; }
        public string? users_email { get; set; }
        public string? contact_number { get; set; }

    }

    public class VerifySignature
    {
        public string? razorpay_payment_id { get; set; }
        public string? razorpay_order_id { get; set; }
        public string? razorpay_signature { get; set; }
    }


    public class RazorOrderReqMsg
    {
        public int? msg_code { get; set; }
        public string? msg_status { get; set; }
        public string? msg_desc { get; set; }
    }

    public class RazorPaymentReqMsg
    {
        public int? msg_code { get; set; }
        public string? msg_status { get; set; }
        public List<MerchantPaymentModels>? payment_data { get; set; }
    }



    public class IndOrderData
    {        
        public string? users_type { get; set; }
        public string? users_name { get; set; }
        public string? users_email { get; set; }
        public string? contact_number { get; set; }
        public int course_id { get; set; }    
       

    }

}
