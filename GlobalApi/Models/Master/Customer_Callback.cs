using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    
    public class Customer_Callback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cust_callback_id { get; set; }       
        public string? cust_name { get; set; }
        public string? cust_phone_no { get; set; }
        public string? cust_email { get; set; }
        public string? cust_msg_desc { get; set; }
        public DateTime? created_date { get; set; } = DateTime.Now;    


    }

    public class Customer_Call_Object
    {
        public string? action { get; set; }
        public DataItem[] data { get; set; }
        public string? refno { get; set; }
        public string? process { get; set; }
        public string? batch_id { get; set; }
        public string? generate_ticket { get; set; }
        public string? cust_type { get; set; }
        public string? cust_name { get; set; }
        public string? cust_phone_no { get; set; }
        public string? cust_email { get; set; }
        public string? hospital_name { get; set; }
        public string? cust_concern_id_fk { get; set; }
        public string? cust_concern_desc { get; set; }
        public string? list_id { get; set; }
    }

    public class DataItem
    {
        public string? mobile { get; set; }
        public string? info_1 { get; set; }
    }

    public class ApiResponse
    {
        public string? STATUS { get; set; }
        public Message? MESSAGE { get; set; }
        public string? refno { get; set; }
    }

    public class Message
    {
        public string? inserted_numbers { get; set; }
    }

}
