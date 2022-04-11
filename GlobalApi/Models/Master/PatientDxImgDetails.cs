//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;

//namespace GlobalApi.Models.Master
//{
//    public class PatientDxImgDetails
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.None)]
//        [Required]
//        public int Id { get; set; }


//        [Display(Name = "Consultation")]
//        public virtual int? CON_Id_FK { get; set; }
//        [JsonIgnore]
//        [ForeignKey("CON_Id_FK")]
//        public virtual Consultation? Consultation { get; set; }


//        [Display(Name = "Imaging")]
//        public virtual int Img_Id_FK { get; set; }
//        [JsonIgnore]
//        [ForeignKey("Img_Id_FK")]
//        public virtual Imaging? Imaging { get; set; }

//        //[StringLength(1)]
//        //public string? Fasting { get; set; }

//        //[StringLength(1)]
//        //public string? Non_Fasting { get; set; }

//        [Display(Name = "IMG_INVESTIGATIONS")]
//        public virtual int Img_Invst_Id_FK { get; set; }
//        [JsonIgnore]
//        [ForeignKey("Img_Invst_Id_FK")]
//        public virtual IMG_INVESTIGATIONS? IMG_INVESTIGATIONS { get; set; }


//        [Display(Name = "IMG_SUBINVESTIGATIONS")]
//        public virtual int Img_SubInvst_Id_FK { get; set; }
//        [JsonIgnore]
//        [ForeignKey("Img_SubInvst_Id_FK")]
//        public virtual IMG_SUBINVESTIGATIONS? IMG_SUBINVESTIGATIONS { get; set; }

//        public int? AcceptTest { get; set; }

//        [StringLength(300)]
//        public string? ImgRemarks { get; set; }

//        [StringLength(1)]
//        public string? ImgDelivery_status { get; set; }
//        public string? Report { get; set; }
//        public int? created_by { get; set; }
//        public Nullable<System.DateTime> created_date { get; set; }
//        public int? modified_by { get; set; }
//        public Nullable<System.DateTime> modified_date { get; set; }
//        public int? deleted_by { get; set; }
//        public Nullable<System.DateTime> deleted_date { get; set; }

//        [Required]
//        public bool delete_flag { get; set; }

//        [Required]
//        public int status { get; set; }

//    }
//    public class PatientDxImgDetailsBy_Id
//    {
//        public int Id { get; set; }
//        public int Img_Id_FK { get; set; }
//        public int? CON_Id_FK { get; set; }
//        public int? Img_CON_DO_Id { get; set; }
//        public string? Img_DO_Name { get; set; }
//        public long Img_DO_MobNum { get; set; }
//        public int? Img_CON_PR_Id { get; set; }
//        public string? Img_PR_Name { get; set; }
//        public string? Img_PR_Gender { get; set; }
//        public string? Img_PR_Age { get; set; }
//        public long Img_PR_MobNum { get; set; }
//        public string? Img_PR_Email { get; set; }
//        public string? Img_PR_Address { get; set; }
//        //public string? Img_PR_BloodGroup { get; set; }
//        public string? Img_PR_Photo { get; set; }
//        public string? Img_PR_Taluk { get; set; }
//        public string? Img_PR_Village { get; set; }
//        public int Img_PR_PostalCode { get; set; }
//        //public string? Fasting { get; set; }
//        //public string? Non_Fasting { get; set; }
//        public int? Img_Invst_Id_FK { get; set; }
//        public string? Img_Invst_Category { get; set; }
//        public int? Img_SubInvst_Id_FK { get; set; }
//        public string? Img_SubInvst_Category { get; set; }
//        public int? AcceptTest { get; set; }
//        public string? ImgRemarks { get; set; }
//        public string? ImgDelivery_status { get; set; }
//        public string? Report { get; set; }
//        public bool delete_flag { get; set; }
//        public int status { get; set; }

//    }
//    public class GetPatientDxImgDetails
//    {
//        public int Id { get; set; }
//        public int Img_Id_FK { get; set; }
//        public int? CON_Id_FK { get; set; }
//        public int? Img_CON_DO_Id { get; set; }
//        public string? Img_DO_Name { get; set; }
//        public long Img_DO_MobNum { get; set; }
//        public int? Img_CON_PR_Id { get; set; }
//        public string? Img_PR_Name { get; set; }
//        public string? Img_PR_Gender { get; set; }
//        public string? Img_PR_Age { get; set; }
//        public long Img_PR_MobNum { get; set; }
//        public string? Img_PR_Email { get; set; }
//        public string? Img_PR_Address { get; set; }
//        //public string? Img_PR_BloodGroup { get; set; }
//        public string? Img_PR_Photo { get; set; }
//        public string? Img_PR_Taluk { get; set; }
//        public string? Img_PR_Village { get; set; }
//        public int Img_PR_PostalCode { get; set; }
//        //public string? Fasting { get; set; }
//        //public string? Non_Fasting { get; set; }
//        public int? Img_Invst_Id_FK { get; set; }
//        public string? Img_Invst_Category { get; set; }
//        public int? Img_SubInvst_Id_FK { get; set; }
//        public string? Img_SubInvst_Category { get; set; }
//        public int? AcceptTest { get; set; }
//        public string? ImgRemarks { get; set; }
//        public string? ImgDelivery_status { get; set; }
//        public string? Report { get; set; }
//        public bool delete_flag { get; set; }
//        public int status { get; set; }
//    }
//    public class ImgReport
//    {
//        public int Id { get; set; }
//        public int? CON_Id_FK { get; set; }
//        public int Img_Id_FK { get; set; }
//        //public string? Fasting { get; set; }
//        //public string? Non_Fasting { get; set; }
//        public int? Img_Invst_Id_FK { get; set; }
//        public int? Img_SubInvst_Id_FK { get; set; }
//        public string? ImgRemarks { get; set; }
//        public string? ImgDelivery_status { get; set; }
//        public IFormFile? Report { get; set; }
//        public int? modified_by { get; set; }
//        public Nullable<System.DateTime> modified_date { get; set; }
//        public bool delete_flag { get; set; }
//        public int status { get; set; }

//    }
//    public class Edit_FileModel_ImgReport
//    {
//        public string? ImgReport { get; set; }
//    }
//}
