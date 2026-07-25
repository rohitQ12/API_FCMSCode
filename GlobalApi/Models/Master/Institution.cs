using GlobalApi.CustomJson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Institution
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Ins_Id { get; set; }
        [StringLength(20)]
        public string? Ins_InstitutionCode { get; set; }

        [StringLength(50)]
        public string? Ins_InstitutionName { get; set; }

      
        public string? Ins_InstitutionEmail { get; set; }

        [StringLength(50)]
        public string? Ins_InstitutionPhoneNo { get; set; }

        [StringLength(50)]
        public string? Ins_InstitutionAddress { get; set; }

        public DateTime? Ins_Dob { get; set; }

        //public string? Hos_UserID { get; set; }

        [Display(Name = "Countries")]
        public virtual int Ins_Country_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Ins_Country_Id_FK")]
        public virtual Countries? Countries { get; set; }


        [Display(Name = "States")]
        public virtual int Ins_ST_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Ins_ST_Id_FK")]
        public virtual States? States { get; set; }
        public string? Ins_Reg_No { get; set; }

        public string? Ins_Alernative_Numb { get; set; }
        public int? Ins_PostalCode { get; set; }

        //[Display(Name = "Districts")]
        //public virtual int Ins_DI_Id_FK { get; set; }
        //[JsonIgnore]
        //[ForeignKey("Ins_DI_Id_FK")]
        //public virtual Districts? Districts { get; set; }






        [StringLength(250)]

        public int? created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int status { get; set; }


        public int? Ins_no_sub { get; set; }


        [StringLength(50)]
        public string? Ins_GSTno { get; set; }

        [StringLength(50)]
        public string? Ins_PANno { get; set; }


        public string? Remarks { get; set; }
        [StringLength(15)]
        public string? Ins_Landline { get; set; }
        [StringLength(15)]
        public string? Ins_OwnerName { get; set; }
        [StringLength(15)]
        public string? Ins_UserID { get; set; }
        public string? Ins_web_url { get; set; }
        public string? Ins_DI_Name { get; set; }
        public string? Ins_fromDate { get; set; }
        public string? Ins_ToDate { get; set; }


    }

    public class GetAllInstitution
    {
        public int Ins_Id { get; set; }
        public string? Ins_InstitutionCode { get; set; }
        public string? Ins_InstitutionName { get; set; }
        //public int? Hos_Type_Id { get; set; }
        //public string TypeName { get; set; }
        //public int? Hos_cat_Id { get; set; }
        //public string CatName { get; set; }
        //public string? Branch_Name { get; set; }
        public string? Ins_InstitutionEmail { get; set; }
        public string? Ins_InstitutionPhoneNo { get; set; }
        public string? Ins_InstitutionAddress { get; set; }
        public string? PrimaryorBranch { get; set; }

        public string? GSTno { get; set; }
        public string? PANno { get; set; }

        public int Ins_Country_Id_FK { get; set; }
        public string? Ins_Country_name { get; set; }
        public int Ins_ST_Id_FK { get; set; }
        public string? Ins_state_name { get; set; }
        public string? Ins_DI_Name { get; set; }
        public string? Ins_district_name { get; set; }
    
        public string? Taluk_name { get; set; }
        //public int? Hos_Gram_Id { get; set; }
        //public string? Gram_name { get; set; }

        public string? NE_Description { get; set; }

        public string? Ins_Landline { get; set; }
        public string? Ins_NetworkLogo { get; set; }
        public byte[]? Logobyte { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }
        public string? Ins_web_url { get; set; }
        public string?  Ins_Reg_No { get; set; }
        public int? Ins_no_sub { get; set; }
        public string? Ins_fromDate { get; set; }
        public string? Ins_ToDate { get; set; }
        public List<GetAllStudentCourse>? GetAllStudentCourse { get; set; } = null!;

        public string? Ins_Alernative_Numb { get; set; }
        public int? Ins_PostalCode { get; set; }
    }
    public class Institution_Images
    {
        public Institution_Images()
        {
            Ins_Id = 0;
            UserId = string.Empty;
            Ins_InstitutionCode = string.Empty;
            Ins_InstitutionPhoneNo = string.Empty;
            Ins_InstitutionEmail = string.Empty;
            Ins_Country_Id_FK = 0;
            Ins_ST_Id_FK = 0;
            Ins_DI_Name = "";
            Ins_Reg_No = string.Empty;
            Ins_Alernative_Numb = string.Empty;
            Ins_PostalCode = 0;


        }
        public int Ins_Id { get; set; }
        public string? UserId { get; set; }
        public string? Ins_InstitutionCode { get; set; }
        public string? Ins_InstitutionName { get; set; }
        //public int? Hos_Type_Id { get; set; }
        //public int? Hos_cat_Id { get; set; }
        //public int? Hos_Branch { get; set; }
        public string? Ins_InstitutionEmail { get; set; }
        public string? Ins_InstitutionPhoneNo { get; set; }
        public string? Ins_InstitutionAddress { get; set; }
        public string? PrimaryorBranch { get; set; }
        public string? Ins_GSTno { get; set; }
        public string? Ins_PANno { get; set; }
        public int Ins_Country_Id_FK { get; set; }
        public int Ins_ST_Id_FK { get; set; }
        public string? Ins_DI_Name { get; set; }
        public string? Ins_Reg_No { get; set; }
        //public int? Hos_Gram_Id { get; set; }
        public int? Ins_no_sub { get; set; }
        public int Ins_sum { get; set; }


        public string? Ins_Landline { get; set; }
        public int? created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        //public string? OwnerName { get; set; }
        //public IFormFile? MOUDocument { get; set; }
        public string? Ins_web_url { get; set; }
          public string? Ins_Alernative_Numb { get; set; }
        public int? Ins_PostalCode { get; set; }

        public string? Ins_fromDate { get; set; }
        public string? Ins_ToDate { get; set; }

        public List<StudentCourse>? Student_Course { get; set; } = null!;


    }
    public class InstitutionReg : Institution_Images
    {
        public InstitutionReg()
        {
            Role_ID = string.Empty;
        }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public int Id { get; set; }
        public string OTP { get; set; }
        public string Role_ID { get; set; }
    }
   
    public class Institution_DD
    {
        public int Ins_Id { get; set; }
        public string? Ins_InstitutionCode { get; set; }
        public string? Ins_InstitutionName { get; set; }
        //public int Hos_NE_Id_FK { get; set; }
        //public string? NE_Description { get; set; }


    }
    public class NetworkInstitution_DD
    {
        public int INs_Id { get; set; }
        public string? Ins_InstitutionCode { get; set; }
        public string? Ins_InstitutionName { get; set; }


        public string? Ins_Description { get; set; }
    }
    //public class HospitalCategory_DD
    //{
    //    public int Ins_Id { get; set; }
    //    public string? Ins_InstitutionCode { get; set; }
    //    public string? Ins_InstitutionName { get; set; }
    //    public int? Hos_cat_Id { get; set; }
    //    public string name { get; set; }

    //}


    public class InstitutionById
    {
        public int Ins_Id { get; set; }
        public string? Ins_InstitutionCode { get; set; }
        public string? Ins_InstitutionName { get; set; }
        public string? Ins_InstitutionEmail { get; set; }
        public string? Ins_InstitutionPhoneNo { get; set; }
        public string? Ins_InstitutionAddress { get; set; }
        public string? PrimaryorBranch { get; set; }

        public string? GSTno { get; set; }
        public string? PANno { get; set; }

        public int Ins_Country_Id_FK { get; set; }
        public string? Ins_Country_name { get; set; }
        public int Ins_ST_Id_FK { get; set; }
        public string? Ins_state_name { get; set; }
        public string? Ins_DI_Name { get; set; }
        public string? Ins_district_name { get; set; }
        public string? Taluk_name { get; set; }
        public string? Ins_Reg_No { get; set; }
        //public int? Hos_Gram_Id { get; set; }
        //public string? Gram_name { get; set; }

        public string? NE_Description { get; set; }


        public string? Ins_Landline { get; set; }
        public string? Ins_NetworkLogo { get; set; }
        public byte[]? Logobyte { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }
        public string? Ins_web_url { get; set; }
        public int? Ins_no_sub { get; set; }
        public string? Ins_Alernative_Numb { get; set; }
        public int? Ins_PostalCode { get; set; }
        public string? Ins_fromDate { get; set; }
        public string? Ins_ToDate { get; set; }
        public List<GetAllStudentCourse>? GetAllStudentCourse { get; set; } = null!;

    }
    public class ApproveIns
    {
        public int Ins_Id { get; set; }
        public string? Remarks { get; set; }
    }
    //public class UserRegister
    //{

    //    //public UserRegister()
    //    //{
    //    //    Stu_Inscode = string.Empty;
    //    //    Stu_Copcode = string.Empty;

    //    //}
    //    //public string? Stu_Inscode { get; set; } = null;
    //    //public string? Stu_Copcode { get; set; } = null;
    //    public string? Stu_Name { get; set; }

    //    [JsonConverter(typeof(NullableDateTimeConverter))]
    //    //public DateTime? Stu_DOB { get; set; }
    //    public string? Stu_Email { get; set; }
    //    public string? Stu_MobileNumber { get; set; }
    //    // public string? Stu_Gender { get; set; }

    //    //public string? Ind_Name { get; set; }
    //    //public string? Ind_code { get; set; }
    //    //public DateTime? Ind_DOB { get; set; }
    //    //public string? Ind_Gender { get; set; }
    //    //public string? Ind_MobileNumber { get; set; }

    //    //public string? Ind_Email { get; set; }
    //    //public string? Ind_fromDate { get; set; }
    //    //public string? Ind_ToDate { get; set; }

    //    public string? Password { get; set; }
    //    public string? ConfirmPassword { get; set; }
    //    public int? FK_StateId { get; set; }
    //    public int? FK_ZoneId { get; set; }
    //    public int? FK_DistrictId { get; set; }
    //    public int? FK_TestId { get; set; }

    //    //profile picture
    //    public IFormFile? ProfilePicture { get; set; }

    //    //public List<StudentCourse>? Student_Course { get; set; } = null!;


    //}
    public class UserLogin
    {
        public string? User_Email { get; set; }

        public string? User_PhoneNumber { get; set; }

        public string? Password { get; set; }
    }

    public class GetAllInstitutionIds

    {

        public int Ins_Id { get; set; }
        public string? Ins_InstitutionCode { get; set; }
        public string? Ins_InstitutionName { get; set; }
        public string? Ins_Reg_No { get; set; }
    }

    public class UserRegister
    {
       // 4446564678

        public string Cust_Name { get; set; }
        public string Cust_Email_Id { get; set; }
        public string Cust_Mobile_Number { get; set; }
        //public string Cust_Address { get; set; }
        //public string Cust_Zip_Code { get; set; }
        public string Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }


    public class UserWithRoleViewModel
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserId { get; set; }
        public string? Role_Id_FK { get; set; }
        public string? Inactive { get; set; }
        public string? RoleName { get; set; }
    }

}
