using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections;

namespace GlobalApi.Models.Master
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int DO_Id { get; set; }
        [StringLength(50)]
        public string? UserId { get; set; }
        public string? DO_RegNo { get; set; }

        [StringLength(10)]
        public string? DO_Code { get; set; }

        [StringLength(50)]
        public string? DO_FirstName { get; set; }

        [StringLength(50)]
        public string? DO_LastName { get; set; }
        public DateTime? DO_DOB { get; set; }

        [StringLength(20)]
        public string? DO_Gender { get; set; }

        public int? DO_MotherTongue { get; set; }
        //max
        public string? DO_Address { get; set; }

        
        [Display(Name = "Countries")]
        public virtual int? DO_Country_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DO_Country_Id_FK")]
        public virtual Countries? Countries { get; set; }


        [Display(Name = "States")]
        public virtual int? DO_ST_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DO_ST_Id_FK")]
        public virtual States? States { get; set; }


        [Display(Name = "Districts")]
        public virtual int? DO_DI_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DO_DI_Id_FK")]
        public virtual Districts? Districts { get; set; }


        [Display(Name = "Taluk")]
        public virtual int? DO_Taluk_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("DO_Taluk_Id")]
        public virtual Taluk? Taluk { get; set; }

        [Display(Name = "Gram")]
        public virtual int? DO_Gram_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("DO_Gram_Id")]
        public virtual Gram? Gram { get; set; }

        [StringLength(50)]
        public string? DO_Village { get; set; }

        public int? DO_PostalCode { get; set; }
        public long DO_MobileNumber { get; set; }
        public long? DO_OfficialNumber { get; set; }
        public long? DO_Alernative_Numb { get; set; }


        [StringLength(50)]
        public string? DO_Email { get; set; }


        [Display(Name = "Hospital")]
        public virtual int? DO_HO_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DO_HO_Id_FK")]
        public virtual Hospital? Hospital { get; set; }


        [Display(Name = "Qualification")]
        public virtual int? DO_QU_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DO_QU_Id_FK")]
        public virtual Qualification? Qualification { get; set; }


        [Display(Name = "Designation")]
        public virtual int? DO_DE_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DO_DE_Id_FK")]
        public virtual Designation? Designation { get; set; }


        [Display(Name = "Discipline")]
        public virtual int? DO_CD_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DO_CD_Id_FK")]
        public virtual Discipline? Discipline { get; set; }


        [Display(Name = "Specialization")]
        public virtual int? DO_SP_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DO_SP_Id_FK")]
        public virtual Specialization? Specialization { get; set; }


        [StringLength(255)]
        public string? DO_Photo { get; set; }
        public int? DO_UserId_FK { get; set; }
        
        [StringLength(50)]
        public string? PANno { get; set; }
        
        [StringLength(50)]
        public string? GSTno { get; set; }

        [StringLength(50)]
        public string? Regno { get; set; }

        public int created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int status { get; set; }
        
        [StringLength(250)]
        public string? Remarks { get; set; }


    }
    public class GetAllDoctor
    {
        public int DO_Id { get; set; }
        public string? DO_RegNo { get; set; }
        public string? DO_Code { get; set; }
        public string? DO_FirstName { get; set; }
        public string? DO_LastName { get; set; }
        public DateTime? DO_DOB { get; set; }
        public string? DO_Gender { get; set; }
        public int? DO_MotherTongue { get; set; }
        public string? Language { get; set; }
        public string? DO_Address { get; set; }
        public int? DO_Country_Id_FK { get; set; }
        public string? DO_Country_name { get; set; }
        public int? DO_ST_Id_FK { get; set; }
        public string? DO_StateName { get; set; }
        public int? DO_DI_Id_FK { get; set; }
        public string? DO_DistrictName { get; set; }
        public int? DO_Taluk_Id { get; set; }
        public string? Taluk_name { get; set; }
        public int? DO_Gram_Id { get; set; }
        public string? Gram_name { get; set; }
        public string? DO_Village { get; set; }
        public int? DO_PostalCode { get; set; }
        public long DO_MobileNumber { get; set; }
        public long? DO_OfficialNumber { get; set; }
        public long? DO_Alernative_Numb { get; set; }
        public string? DO_Email { get; set; }
        public int? DO_HO_Id_FK { get; set; }
        public string? DO_Hospital { get; set; }
        public int? DO_QU_Id_FK { get; set; }
        public string? DO_Qualification { get; set; }
        public int? DO_DE_Id_FK { get; set; }
        public string? DO_Designation { get; set; }
        public int? DO_CD_Id_FK { get; set; }
        public string? DO_ClinicalDiscipline { get; set; }
        public int? DO_SP_Id_FK { get; set; }
        public string? DO_Specialization { get; set; }
        public string? DO_Photo { get; set; }
        public byte[]? Imagebyte { get; set; }
        public int? DO_UserId_FK { get; set; }
        public string? PANno { get; set; }
        public string? GSTno { get; set; }
        public string? Regno { get; set; }
        //public DateTime DO_INSTS { get; set; }
        public bool delete_flag { get; set; }
        public int? status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }

    }
    public class DoctorById
    {
        public int DO_Id { get; set; }
        public string? DO_RegNo { get; set; }
        public string? DO_Code { get; set; }
        public string? DO_FirstName { get; set; }
        public string? DO_LastName { get; set; }
        public DateTime? DO_DOB { get; set; }
        public string? DO_Gender { get; set; }
        public int? DO_MotherTongue { get; set; }
        public string? Language { get; set; }
        public string? DO_Address { get; set; }
        public string? DO_Country { get; set; }
        public int? DO_Country_Id_FK { get; set; }
        public string? DO_Country_name { get; set; }
        public int? DO_ST_Id_FK { get; set; }
        public string? DO_StateName { get; set; }
        public int? DO_DI_Id_FK { get; set; }
        public string? DO_DistrictName { get; set; }
        public int? DO_Taluk_Id { get; set; }
        public string? Taluk_name { get; set; }
        public int? DO_Gram_Id { get; set; }
        public string? Gram_name { get; set; }
        public string? DO_Village { get; set; }
        public int? DO_PostalCode { get; set; }
        public long DO_MobileNumber { get; set; }
        public long? DO_OfficialNumber { get; set; }
        public long? DO_Alernative_Numb { get; set; }
        public string? DO_Email { get; set; }
        public int? DO_HO_Id_FK { get; set; }
        public string? DO_Hospital { get; set; }
        public int? DO_QU_Id_FK { get; set; }
        public string? DO_Qualification { get; set; }
        public int? DO_DE_Id_FK { get; set; }
        public string? DO_Designation { get; set; }
        public int? DO_CD_Id_FK { get; set; }
        public string? DO_ClinicalDiscipline { get; set; }
        public int? DO_SP_Id_FK { get; set; }
        public string? DO_Specialization { get; set; }
        public string? DO_Photo { get; set; }
        public byte[]? Imagebyte { get; set; }
        public int? DO_UserId_FK { get; set; }
        public string? PANno { get; set; }
        public string? GSTno { get; set; }
        public string? Regno { get; set; }
        //public DateTime DO_INSTS { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }

    }
    public class Doctor_Images
    {
        public int DO_Id { get; set; }
        public string? DO_RegNo { get; set; }
        public string? DO_Code { get; set; }
        public string? DO_FirstName { get; set; }
        public string? DO_LastName { get; set; }
        public DateTime? DO_DOB { get; set; }
        public string? DO_Gender { get; set; }
        public int? DO_MotherTongue { get; set; }
        public string? DO_Address { get; set; }
        public int? DO_Country_Id_FK { get; set; }
        public int? DO_ST_Id_FK { get; set; }
        public int? DO_DI_Id_FK { get; set; }
        public int? DO_Taluk_Id { get; set; }
        public int? DO_Gram_Id { get; set; }
        public string? DO_Village { get; set; }
        public int? DO_PostalCode { get; set; }
        public long DO_MobileNumber { get; set; }
        public long? DO_OfficialNumber { get; set; }
        public long? DO_Alernative_Numb { get; set; }
        public string? DO_Email { get; set; }
        public int? DO_HO_Id_FK { get; set; }
        public int? DO_QU_Id_FK { get; set; }
        public int? DO_DE_Id_FK { get; set; }
        public int? DO_CD_Id_FK { get; set; }
        public int? DO_SP_Id_FK { get; set; }
        public IFormFile? DO_Photo { get; set; }
        //public string? DO_Languages { get; set; }
        //public int[]? DoctorLanguage { get; set; }
        public int? DO_UserId_FK { get; set; }
        public string? PANno { get; set; }
        public string? GSTno { get; set; }
        public string? Regno { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }


    }
    public class Doctor_ImagesUP
    {
        public int DO_Id { get; set; }
        public string? DO_RegNo { get; set; }
        public string? DO_Code { get; set; }
        public string? DO_FirstName { get; set; }
        public string? DO_LastName { get; set; }
        public DateTime? DO_DOB { get; set; }
        public string? DO_Gender { get; set; }
        public int? DO_MotherTongue { get; set; }
        public string? DO_Address { get; set; }
        public int? DO_Country_Id_FK { get; set; }
        public int? DO_ST_Id_FK { get; set; }
        public int? DO_DI_Id_FK { get; set; }
        public int? DO_Taluk_Id { get; set; }
        public int? DO_Gram_Id { get; set; }
        public string? DO_Village { get; set; }
        public int? DO_PostalCode { get; set; }
        public long DO_MobileNumber { get; set; }
        public long? DO_OfficialNumber { get; set; }
        public long? DO_Alernative_Numb { get; set; }
        public string? DO_Email { get; set; }
        public int? DO_HO_Id_FK { get; set; }
        public int? DO_QU_Id_FK { get; set; }
        public int? DO_DE_Id_FK { get; set; }
        public int? DO_CD_Id_FK { get; set; }
        public int? DO_SP_Id_FK { get; set; }
        public IFormFile? DO_Photo { get; set; }
        //public string? DO_Languages { get; set; }
        //public int[]? DoctorLanguage { get; set; }
        //public List<DoctorLanguage> DoctorLanguage { get; set; } = null!;
        public int? DO_UserId_FK { get; set; }
        public string? PANno { get; set; }
        public string? GSTno { get; set; }
        public string? Regno { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }


    }

    public class Doctor_Imagestesting
    {
        public int DO_Id { get; set; }
        public string? DO_RegNo { get; set; }

        public string? DO_Code { get; set; }
        public string? DO_Languages { get; set; }
        public DoctorLanguage[] DoctorLanguage { get; set; }
        public IFormFile DO_Photo { get; set; }
        //public int DO_UserId_FK { get; set; }
        //public FormFileWrapper IdImage { get; set; }


    }
    public class FormFileWrapper
    {
        public IFormFile File { get; set; }
    }
    public class doctest
    {
        public IFormFile DO_Photo { get; set; }
    }
    public class Edit_ImageModel_DO
    {
        public string? DO_Photo { get; set; }

    }
    public class Doctor_DD
    {
        public int DO_Id { get; set;}
        public string DO_Name { get; set; }
        public string? DO_Photo { get; set; }
        public string? Sp_Name { get; set; }
        public string? Hos_Name { get; set; }
        public string? district { get; set; }

    }
    public class ApproveDoctor
    {
        public int DO_Id { get; set; }
        public string? Remarks { get; set; }
    }

}
