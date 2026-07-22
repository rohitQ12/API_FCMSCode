using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Customer_Enquiry
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int cus_eq_id { get; set; }
        public string? cus_eq_name { get; set; }
        public string? cus_eq_phoneNo { get; set; }
        public string? cus_eq_email { get; set; }
        public DateTime? cus_eq_DOB { get; set; }
        public string? cus_eq_gender { get; set; }
        public string? cus_eq_Address { get; set; }
        public int? cus_eq_Postalcode { get; set; }
        public int? cus_eq_State{ get; set; }
        public string? cus_eq_city { get; set; }
       // public int? cus_eq_inv { get; set; }
        public string? cus_eq_desc { get; set; }
        public DateTime created_date { get; set; }
        public DateTime? deleted_date { get; set; }
        public int cus_eq_status { get; set; }
        public int? cus_Cm_Fk { get; set; }
        //public int cus_eq_type { get; set; }

    }



    public class Enquiry_details
    {
        public string? cus_eq_name { get; set; }
        public string? cus_eq_phoneNo { get; set; }
        public string? cus_eq_email { get; set; }
        public DateTime? cus_eq_DOB { get; set; }
        public string? cus_eq_gender { get; set; }
        public string? cus_eq_Address { get; set; }
        public int? cus_eq_Postalcode { get; set; }
        public int? cus_eq_State { get; set; }
        public string? cus_eq_city { get; set; }
        //public int? cus_eq_inv { get; set; }
        public string? cus_eq_desc { get; set; }
        //public int cus_eq_type { get; set; }
        public List<StudentCourse>? Student_Course { get; set; } = null!;

        public int? cus_Cm_Fk { get; set; }


    }


    public class GetAllEnquiry_details
    {
        public int? cus_eq_id { get; set; }
        public string? cus_eq_name { get; set; }
        public string? cus_eq_phoneNo { get; set; }
        public string? cus_eq_email { get; set; }
        public DateTime? cus_eq_DOB { get; set; }
        public string? cus_eq_gender { get; set; }
        public string? cus_eq_Address { get; set; }
        public int? cus_eq_Postalcode { get; set; }
        public int? cus_eq_State { get; set; }
        public string? cus_eq_city { get; set; }
        public int? cus_eq_inv { get; set; }
        public string? cus_eq_desc { get; set; }
        public string? sts_name { get; set; }
        public int? St_Cm_Fk { get; set; }
        public string? St_Cm_Name { get; set; }
        public string? state_name { get; set; }
        public List<GetAllStudentCourse>? GetAllStudentCourse { get; set; } = null!;

        public int? cus_Cm_Fk { get; set; }
     
        public int? cus_eq_status { get; set; }

      


    }


    public class GetAllEnquiry_detailsById
    {
        public string? cus_eq_name { get; set; }
        public string? cus_eq_phoneNo { get; set; }
        public string? cus_eq_email { get; set; }
        public DateTime? cus_eq_DOB { get; set; }
        public string? cus_eq_gender { get; set; }
        public string? cus_eq_Address { get; set; }
        public int? cus_eq_Postalcode { get; set; }
        public int? cus_eq_State { get; set; }
        public string? cus_eq_city { get; set; }
        public int? cus_eq_inv { get; set; }
        public string? cus_eq_desc { get; set; }
        public string? sts_name { get; set; }
        public int? St_Cm_Fk { get; set; }
        public string? St_Cm_Name { get; set; }
        public string? state_name { get; set; }
        public int? cus_Cm_Fk { get; set; }
        public List<GetAllStudentCourse>? GetAllStudentCourse { get; set; } = null!;



        public int? cus_eq_status { get; set; }


    }

    public class ApproveEnquire
    {
        public int cus_eq_id { get; set; }
        public string? Remarks { get; set; }

    }
}
