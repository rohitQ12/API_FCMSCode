using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections;
using System.Reflection.Emit;

namespace GlobalApi.Models.Master
{
    public class Corporate
    {

        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]

        public int CO_Id { get; set; }
        public string? CO_Code { get; set; }
        public string? CO_Name { get; set; }
        public string? CO_Address { get; set; }
        

        [Display(Name = "Countries")]
        public virtual int CO_Country_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("CO_Country_Id_FK")]
        public virtual Countries? Countries { get; set; }


        [Display(Name = "States")]
        public virtual int CO_ST_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("CO_ST_Id_FK")]
        public virtual States? States { get; set; }

        //[Display(Name = "Districts")]
        //public virtual int CO_DI_Id_FK { get; set; }
        //[JsonIgnore]
        //[ForeignKey("CO_DI_Id_FK")]
        //public virtual Districts? Districts { get; set; }


        public string? CO_MobileNumber { get; set; }
        public string? CO_Alernative_Numb { get; set; }
        public string? CO_Email { get; set; }
        public DateTime? CO_Dob { get; set; }
        public string? CO_Photo { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? CO_UserId { get; set; }
        public string? CO_PANno { get; set; }
        public string? Remarks { get; set; }
        public string? CO_GSTno { get; set; }
        public string? CO_Choose_Document { get; set; }


        public int? Co_no_sub { get; set; }
        public string? Co_FromDate { get; set; }
        public string? Co_ToDate { get; set; }
        public string? Co_DI_Name { get; set; }
        public string? Co_Reg_No { get; set; }
        public int? Co_PostalCode { get; set; }

    }
    public class GetAllCorporate
    {
 

        public int CO_Id { get; set; }
        public string? CO_Code { get; set; }
        public string? CO_CorporateName { get; set; }
        public string? CO_Branch { get; set; }
        public string? CO_Email { get; set; }
        public string? CO_CorporatePhoneNo { get; set; }
        public string? CO_Alernative_Numb { get; set; }
        public string? CO_Address { get; set; }


        public string? GSTno { get; set; }
        public string? PANno { get; set; }

       // public string? RegNo { get; set; }
        public int CO_Country_Id_FK { get; set; }
        public string? CO_Country_name { get; set; }
        public int CO_ST_Id_FK { get; set; }
        public string? CO_state_name { get; set; }
        public string? Co_DI_Name { get; set; }
        public string? CO_district_name { get; set; }
        public string? Taluk_name { get; set; }
        public string? CO_Alterno { get; set; }
        public string? CO_Landline { get; set; }
        public string? CO_CorporateLogo { get; set; }
        public string? CO_NetworkLogo { get; set; }
        public byte[]? Logobyte { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }
        public string? CO_web_url { get; set; }
        public int? Co_no_sub { get; set; }
        public string? Co_FromDate { get; set; }
        public string? Co_ToDate { get; set; }
        public string? Co_Reg_No { get; set; }
        public int? Co_PostalCode { get; set; }
        public List<GetAllStudentCourse>? GetAllStudentCourse { get; set; } = null!;

    }
    public class CorporateById
    {

        public int CO_Id { get; set; }
        public string? CO_Code { get; set; }
        public string? CO_CorporateName { get; set; }
        public string? CO_Branch { get; set; }
        public string? CO_Email { get; set; }
        public string? CO_CorporatePhoneNo { get; set; }
        public string? CO_Alernative_Numb { get; set; }
        public string? CO_Address { get; set; }


        public string? GSTno { get; set; }
        public string? PANno { get; set; }

        //public string? RegNo { get; set; }
        public int CO_Country_Id_FK { get; set; }
        public string? CO_Country_name { get; set; }
        public int CO_ST_Id_FK { get; set; }
        public string? CO_state_name { get; set; }
        public string? Co_DI_Name { get; set; }
        public string? CO_district_name { get; set; }
        public string? Taluk_name { get; set; }
        public string? CO_Alterno { get; set; }
        public string? CO_Landline { get; set; }
        public string? CO_CorporateLogo { get; set; }
        public string? CO_NetworkLogo { get; set; }
        public byte[]? Logobyte { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }
        public string? CO_web_url { get; set; }
        public int? Co_no_sub { get; set; }
        public string? Co_FromDate { get; set; }
        public string? Co_ToDate { get; set; }
        public string? Co_Reg_No { get; set; }
        public int? Co_PostalCode { get; set; }
        public List<GetAllStudentCourse>? GetAllStudentCourse { get; set; } = null!;

    }
    public class Corporate_Images
    {
        public Corporate_Images()
        {
            CO_Id = 0;
            CO_UserId = string.Empty;
            CO_Code = string.Empty;
            Co_Reg_No = string.Empty;
            CO_MobileNumber = "0";
            CO_Email = string.Empty;
            CO_Country_Id_FK = 0;
            CO_ST_Id_FK = 0;
            Co_DI_Name = "";
            CO_Alernative_Numb = string.Empty;
            Co_PostalCode = 0;

        }
        public int CO_Id { get; set; }
        public string? CO_Code { get; set; }
        public string? CO_Name { get; set; }
        public string? CO_Address { get; set; }
        public int CO_Country_Id_FK { get; set; }
        public int CO_ST_Id_FK { get; set; }
        public string? Co_DI_Name { get; set; }

        public string CO_MobileNumber { get; set; }
        public string? CO_Alernative_Numb { get; set; }
        public string? CO_Email { get; set; }
      //  public IFormFile? CO_Photo { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool? delete_flag { get; set; }
        public int status { get; set; }
        public string? CO_UserId { get; set; }
        public string? CO_PANno { get; set; }
        public string? Remarks { get; set; }
        public string? CO_GSTno { get; set; }
      //  public IFormFile? CO_Choose_Document { get; set; }


      //  public IFormFile? MOUDocument { get; set; }
        public string? CO_Type { get; set; }
        public int? Co_no_sub { get; set; }
        public string? Co_FromDate { get; set; }
        public string? Co_ToDate { get; set; }
        public string? Co_Reg_No { get; set; }
     
        public int? Co_PostalCode { get; set; }


        public List<StudentCourse>? Student_Course { get; set; } = null!;


    }
    public class CorporaterReg : Corporate_Images
    {
        public CorporaterReg()
        {
            Role_ID = string.Empty;
            OTP = string.Empty;
        }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public int Id { get; set; }
        public string OTP { get; set; }
        public string Role_ID { get; set; }
    }


    public class Corporate_ImagesUP
    {
        public int CO_Id { get; set; }
        public string? UserId { get; set; }
        public string? CO_Code { get; set; }
        public string? CO_Name { get; set; }
        //public DateTime? CO_COB { get; set; }
        //public string? CO_Gender { get; set; }
         public int? CO_MotherTongue { get; set; }
        public string? CO_Address { get; set; }
        public int CO_Country_Id_FK { get; set; }
        public int CO_ST_Id_FK { get; set; }
        public string? Co_DI_Name { get; set; }
        public int? CO_Gram_Id { get; set; }

        public string CO_MobileNumber { get; set; }
        public string? CO_Alernative_Numb { get; set; }
        public string? CO_Email { get; set; }
        public int? CO_HO_Id_FK { get; set; }
        public int? CO_QU_Id_FK { get; set; }
        public int? CO_DE_Id_FK { get; set; }
        public int? CO_CD_Id_FK { get; set; }
        public int? CO_SP_Id_FK { get; set; }
       // public IFormFile? CO_Photo { get; set; }
        //public string? CO_Languages { get; set; }
        //public int[]? COctorLanguage { get; set; }
        //public List<COctorLanguage> COctorLanguage { get; set; } = null!;
        public int? CO_UserId_FK { get; set; }
        public string? PANno { get; set; }
        public string? GSTno { get; set; }
        //public string? Regno { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        //public IFormFile? CO_Choose_Document { get; set; }
        public string? ClinicName { get; set; }


        public string? CO_Type { get; set; }
        public int? CO_SD_Id_FK { get; set; }
        public int? CO_Spc_Id_FK { get; set; }
        public int? CO_SC_Id_FK { get; set; }
        public int? Co_no_sub { get; set; }
        public string? Co_FromDate { get; set; }
        public string? Co_ToDate { get; set; }
        public string? Co_Reg_No { get; set; }
        public int? Co_PostalCode { get; set; }

        //public IFormFile? COc_Signature_COc { get; set; }
        public List<StudentCourse>? Student_Course { get; set; } = null!;

    }

    public class COctor_Imagestesting
    {
        public int CO_Id { get; set; }

        public string? CO_Code { get; set; }
        public string? CO_Languages { get; set; }
       // public CorporateLanguage[] COctorLanguage { get; set; }
        public IFormFile CO_Photo { get; set; }
        //public int CO_UserId_FK { get; set; }
   


    }
    //public class FormFileWrapper
    //{
    //    public IFormFile File { get; set; }
    //}
    public class Corporatetest
    {
        public IFormFile CO_Photo { get; set; }
    }
    public class Edit_ImageModel_CO
    {
        public string? CO_Photo { get; set; }

    }
    public class Corporate_DD
    {
        public int CO_Id { get; set; }
        public string CO_Name { get; set; }
        public string? CO_Photo { get; set; }
        public string? Sp_Name { get; set; }
        public string? Hos_Name { get; set; }
        public string? district { get; set; }

    }
    public class ApproveCorporate
    {
        public int CO_Id { get; set; }
        public string? Remarks { get; set; }
    }
    public class gethoslogo_Corporate
    {
        public int? CO_Id { get; set; }
        public string? Role { get; set; }
        public int? Hos_Id { get; set; }
        public string? Hos_hospitalname { get; set; }
        public string? Hos_HospitalLogo { get; set; }
    }
    public class getdisplineCorporate
    {
        public int? CO_Id { get; set; }
        public string? CO_Name { get; set; }
        public int? Discipline_Id { get; set; }
        public string? CO_Displine { get; set; }
        public int? CO_QualificationId { get; set; }
        public string? CO_Qualification { get; set; }
        public int? CO_LanguageId { get; set; }
        public string? CO_Lanaguage { get; set; }
        public string? CO_photo { get; set; }
        public decimal? CO_Charge { get; set; }
        public string? CO_Document { get; set; }
    }
    public class getclinicCorporate
    {
        public int? Id { get; set; }
        //public string? COctorName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public long? MobileNumber { get; set; }
        public string? HospitalName { get; set; }
        public string? NetWorkName { get; set; }
        public int? Discipline_Id { get; set; }
        public string? Discipline { get; set; }
        public string? Specialization { get; set; }
        public int? CO_QualificationId { get; set; }
        public string? CO_Qualification { get; set; }
        public string? Address { get; set; }
        public int? CO_LanguageId { get; set; }
        public string? CO_Lanaguage { get; set; }
        public string? CO_photo { get; set; }
        public decimal? CO_Charge { get; set; }
        public string? DO_Document { get; set; }
    }
    public class avilableCOctor_today
    {
        public int? CO_Id_FK { get; set; }
        public string? CO_Name { get; set; }
        public string? CO_Photo { get; set; }


        public int? qualification_id { get; set; }
        public string? qualification_Name { get; set; }

    }
     public class GetAllCorporateIds
    {
        public int CO_Id { get; set; }
        public string? CO_Code { get; set; }
        public string? Co_Reg_No { get; set; }

        public string? CO_Name { get; set; }
    }



}
