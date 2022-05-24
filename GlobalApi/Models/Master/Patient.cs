using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int PR_Id { get; set; }
        public string? UserId { get; set; }
        public string? SfxPrfxId { get; set; }

        [Display(Name = "Hospital")]
        public virtual int? PR_RemoteHospitalName_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("PR_RemoteHospitalName_Id_FK")]
        public virtual Hospital? Hospital { get; set; }

        [StringLength(255)]
        public string? PR_PatientCode { get; set; }

        [StringLength(50)]
        public string? PR_FirstName { get; set; }

        [StringLength(50)]
        public string? PR_LastName { get; set; }

        [StringLength(20)]
        public string? PR_Gender { get; set; }

        public DateTime? PR_DOB { get; set; }

        [StringLength(100)]
        public string? PR_Age { get; set; }
        public string? PR_LandlineNo { get; set; }
        public string? PR_Alternative_No { get; set; }

        [StringLength(20)]
        public string? PR_MaritalStatus { get; set; }

        [StringLength(50)]
        public string? PR_FatherName { get; set; }
        [StringLength(10)]
        public string? PR_BloodGroup { get; set; }

        [StringLength(20)]
        public string? PR_MotherTongue { get; set; }

        public int? PR_REG_Id_FK { get; set; }
        public int? PR_NAL_Id_FK { get; set; }
        public int? PR_CAT_Id_FK { get; set; }
        public int? PR_IDN_Id_FK { get; set; }
        
        [StringLength(50)]
        public string? PR_Identity_No { get; set; }

        public int? PR_OCU_Id_FK { get; set; }

        [StringLength(50)]
        public string? PR_Income { get; set; }

        [StringLength(255)]
        public string? PR_Insurance { get; set; }
        public int? PR_INU_Id_FK { get; set; }

        public long? PR_Insured_Sum { get; set; }
        //max
        public string? PR_Address { get; set; }

        [Display(Name = "Countries")]
        public virtual int? PR_Country_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("PR_Country_Id_FK")]
        public virtual Countries? Countries { get; set; }


        [Display(Name = "States")]
        public virtual int? PR_S_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("PR_S_Id_FK")]
        public virtual States? States { get; set; }


        [Display(Name = "Districts")]
        public virtual int? PR_D_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("PR_D_Id_FK")]
        public virtual Districts? Districts { get; set; }
        
        [Display(Name = "Taluk")]
        public virtual int? PR_Taluk_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("PR_Taluk_Id")]
        public virtual Taluk? Taluk { get; set; }

        [Display(Name = "Gram")]
        public virtual int? PR_Gram_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("PR_Gram_Id")]
        public virtual Gram? Gram { get; set; }


        [StringLength(50)]
        public string? PR_Village { get; set; }
        public int? PR_Postalcode { get; set; }
        public string? PR_MobileNumber { get; set; }

        [StringLength(50)]
        public string? PR_Email { get; set; }

        [StringLength(50)]
        public string? PR_PassportNo { get; set; }
        public DateTime? PR_RegistrationDateTime { get; set; }

        [StringLength(255)]
        public string? PR_Photo { get; set; }
        public int? PR_UserId_FK { get; set; }
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

    }
    public class GetAllPatient
    {
        public int PR_Id { get; set; }
        public string? UserId { get; set; }
        public string? SfxPrfxId { get; set; }
        public int? PR_RemoteHospitalName_Id_FK { get; set; }
        public string? PR_RemoteHospitalName { get; set; }
        public string? PR_PatientCode { get; set; }
        public string? PR_FirstName { get; set; }
        public string? PR_LastName { get; set; }
        public string? PR_Gender { get; set; }
        public DateTime PR_DOB { get; set; }
        public string? PR_Age { get; set; }
        public string? PR_LandlineNo { get; set; }
        public string? PR_Alternative_No { get; set; }
        public string? PR_MaritalStatus { get; set; }
        public string? PR_FatherName { get; set; }
        public string? PR_BloodGroup { get; set; }
        public string? PR_MotherTongue { get; set; }
        public int? PR_REG_Id_FK { get; set; }
        public string? Religion { get; set; }
        public int? PR_NAL_Id_FK { get; set; }
        public string? Nationality { get; set; }
        public int? PR_CAT_Id_FK { get; set; }
        public string? Caste { get; set; }
        public int? PR_IDN_Id_FK { get; set; }
        public string? DOC_Name { get; set; }
        public string? PR_Identity_No { get; set; }
        public int? PR_OCU_Id_FK { get; set; }
        public string? Occupation { get; set; }
        public string? PR_Income { get; set; }
        public string? PR_Insurance { get; set; }
        public int? PR_INU_Id_FK { get; set; }
        public string? Insurer { get; set; }
        public long? PR_Insured_Sum { get; set; }
        public string? PR_Address { get; set; }
        public int PR_Country_Id_FK { get; set; }
        public string? PR_Country_Name { get; set; }
        public int PR_S_Id_FK { get; set; }
        public string? PR_StateName { get; set; }
        public int PR_D_Id_FK { get; set; }
        public string? PR_District { get; set; }
        public int? PR_Taluk_Id { get; set; }
        public string? Taluk_name { get; set; }
        public int? PR_Gram_Id { get; set; }
        public string? Gram_name { get; set; }
        public string? PR_Village { get; set; }
        public int PR_Postalcode { get; set; }
        public string PR_MobileNumber { get; set; }
        public string? PR_Email { get; set; }
        public string? PR_PassportNo { get; set; }
        public DateTime PR_RegistrationDateTime { get; set; }
        public string? PR_Photo { get; set; }
        public byte[]? PR_Photobyte { get; set; }
        public int? PR_UserId_FK { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class PatientById
    {
        public int PR_Id { get; set; }
        public string? UserId { get; set; }
        public string? SfxPrfxId { get; set; }
        public int? PR_RemoteHospitalName_Id_FK { get; set; }
        public string? PR_RemoteHospitalName { get; set; }
        public string? PR_PatientCode { get; set; }
        public string? PR_FirstName { get; set; }
        public string? PR_LastName { get; set; }
        public string? PR_Gender { get; set; }
        public DateTime PR_DOB { get; set; }
        public string? PR_Age { get; set; }
        public string? PR_LandlineNo { get; set; }
        public string? PR_Alternative_No { get; set; }
        public string? PR_MaritalStatus { get; set; }
        public string? PR_FatherName { get; set; }
        public string? PR_BloodGroup { get; set; }
        public string? PR_MotherTongue { get; set; }
        public int? PR_REG_Id_FK { get; set; }
        public string? Religion { get; set; }
        public int? PR_NAL_Id_FK { get; set; }
        public string? Nationality { get; set; }
        public int? PR_CAT_Id_FK { get; set; }
        public string? Caste { get; set; }
        public int? PR_IDN_Id_FK { get; set; }
        public string? DOC_Name { get; set; }
        public string? PR_Identity_No { get; set; }
        public int? PR_OCU_Id_FK { get; set; }
        public string? Occupation { get; set; }
        public string? PR_Income { get; set; }
        public string? PR_Insurance { get; set; }
        public int? PR_INU_Id_FK { get; set; }
        public string? Insurer { get; set; }
        public long? PR_Insured_Sum { get; set; }
        public string? PR_Address { get; set; }
        public int PR_Country_Id_FK { get; set; }
        public string? PR_Country_Name { get; set; }
        public int PR_S_Id_FK { get; set; }
        public string? PR_StateName { get; set; }
        public int PR_D_Id_FK { get; set; }
        public string? PR_District { get; set; }
        public int? PR_Taluk_Id { get; set; }
        public string? Taluk_name { get; set; }
        public int? PR_Gram_Id { get; set; }
        public string? Gram_name { get; set; }
        public string? PR_Village { get; set; }
        public int PR_Postalcode { get; set; }
        public string PR_MobileNumber { get; set; }
        public string? PR_Email { get; set; }
        public string? PR_PassportNo { get; set; }
        public DateTime PR_RegistrationDateTime { get; set; }
        public string? PR_Photo { get; set; }
        public byte[]? PR_Photobyte { get; set; }
        public int? PR_UserId_FK { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class Patient_Images
    {
        public int? PR_Id { get; set; }
        public string? UserId { get; set; }
        public string? SfxPrfxId { get; set; }
        public virtual int? PR_RemoteHospitalName_Id_FK { get; set; }
        public string? PR_PatientCode { get; set; }
        public string? PR_FirstName { get; set; }
        public string? PR_LastName { get; set; }
        public string? PR_Gender { get; set; }
        public DateTime? PR_DOB { get; set; }
        public string? PR_Age { get; set; }
        public string? PR_LandlineNo { get; set; }
        public string? PR_Alternative_No { get; set; }
        public string? PR_MaritalStatus { get; set; }
        public string? PR_FatherName { get; set; }
        public string? PR_BloodGroup { get; set; }
        public string? PR_MotherTongue { get; set; }
        public int? PR_REG_Id_FK { get; set; }
        public int? PR_NAL_Id_FK { get; set; }
        public int? PR_CAT_Id_FK { get; set; }
        public int? PR_IDN_Id_FK { get; set; }
        public string? PR_Identity_No { get; set; }
        public int? PR_OCU_Id_FK { get; set; }
        public string? PR_Income { get; set; }
        public string? PR_Insurance { get; set; }
        public int? PR_INU_Id_FK { get; set; }
        public long? PR_Insured_Sum { get; set; }
        public string? PR_Address { get; set; }
        public int? PR_Country_Id_FK { get; set; }
        public int? PR_S_Id_FK { get; set; }
        public int? PR_D_Id_FK { get; set; }
        public int? PR_Taluk_Id { get; set; }
        public int? PR_Gram_Id { get; set; }
        public string? PR_Village { get; set; }
        public int? PR_Postalcode { get; set; }
        public string? PR_MobileNumber { get; set; }
        public string? PR_Email { get; set; }
        public string? PR_PassportNo { get; set; }
        public DateTime? PR_RegistrationDateTime { get; set; }
        public IFormFile? PR_Photo { get; set; }
        public int? PR_UserId_FK { get; set; }
        public bool delete_flag { get; set; }
        public int? status { get; set; }

    }
    public class PatientReg: Patient_Images
    {
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
    public class Edit_ImageModel_PR
    {
        public string? PR_Photo { get; set; }
    }
    public class Patient_DD
    {
        public int PR_Id { get; set; }
        public string? PR_PatientCode { get; set; }
        public string? PR_Name { get; set; }

    }
    public class get_Patidautomatic
    {
        public string? automaticgen_patid { get; set; }
    }

}
