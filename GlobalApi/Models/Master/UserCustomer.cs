using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace GlobalApi.Models.Master
{
    [Table("Customer")] 
    public class UserCustomer
    {
        [Key]
        public int Cust_Id { get; set; }
        public string Cust_Name { get; set; }
        public string Cust_Email_Id { get; set; }
        public string Cust_Mobile_Number { get; set; }
        public string Cust_Photo { get; set; }
        public string Cust_Address { get; set; }
        public string Cust_Zip_Code { get; set; }
        public string Cust_UserId { get; set; }
        public string Cust_status { get; set; }
        public string create_by { get; set; }
        public DateTime? created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public bool delete_flag { get; set; }
    }
}
