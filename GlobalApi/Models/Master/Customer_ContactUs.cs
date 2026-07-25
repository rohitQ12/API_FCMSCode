using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Customer_ContactUs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int cust_id { get; set; }
        public string? cust_name { get; set; }
        public string? cust_phone_no { get; set; }
        public string? cust_email { get; set; }
        public string? cust_concern_desc { get; set; }
        public DateTime? created_date { get; set; }

        public int cus_status { get; set; }
    }

    public class GetAllCustomer_ContactUs
    {
        public int cust_id { get; set; }
        public string? cust_name { get; set; }
        public string cust_phone_no { get; set; }
        public string? cust_email { get; set; }
        public string? cust_concern_desc { get; set; }
        public DateTime? created_date { get; set; }
        public int cus_status { get; set; }

    }


    public class GetCustomer_ContactUsById
    {
        public int cust_id { get; set; }
        public string? cust_name { get; set; }
        public string cust_phone_no { get; set; }
        public string? cust_email { get; set; }
        public string? cust_concern_desc { get; set; }
        public DateTime? created_date { get; set; }
        public int cus_status { get; set; }

    }

    public class ContactUs
    {
        public string? cust_name { get; set; }
        public string? cust_phone_no { get; set; }
        public string? cust_email { get; set; }
        public string? cust_concern_desc { get; set; }
       
    }


}
