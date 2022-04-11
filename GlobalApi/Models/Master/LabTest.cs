//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;

//namespace GlobalApi.Models.Master
//{
//    public class LabTest
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.None)]
//        [Required]
//        public int Id { get; set; }

//        [Display(Name = "Consultation")]
//        public virtual int Tst_CON_Id_FK { get; set; }
//        [JsonIgnore]
//        [ForeignKey("Tst_CON_Id_FK")]
//        public virtual Consultation? Consultation { get; set; }

//        //[StringLength(1)]
//        //public string? Fasting { get; set; }

//        //[StringLength(1)]
//        //public string? Non_Fasting { get; set; }

//        [StringLength(2)]
//        public string? FastingORNonFasting { get; set; }

//        [Display(Name = "LAB_INVESTIGATIONS")]
//        public virtual int Lab_Invst_Id_FK { get; set; }
//        [JsonIgnore]
//        [ForeignKey("Lab_Invst_Id_FK")]
//        public virtual LAB_INVESTIGATIONS? LAB_INVESTIGATIONS { get; set; }


//        [Display(Name = "LAB_SUBINVESTIGATIONS")]
//        public virtual int Lab_SubInvst_Id_FK { get; set; }
//        [JsonIgnore]
//        [ForeignKey("Lab_SubInvst_Id_FK")]
//        public virtual LAB_SUBINVESTIGATIONS? LAB_SUBINVESTIGATIONS { get; set; }
//        public string? LabTestReport { get; set; }
//        public int? created_by { get; set; }
//        public Nullable<System.DateTime> created_date { get; set; }
//        public int? modified_by { get; set; }
//        public Nullable<System.DateTime> modified_date { get; set; }
//        public int? deleted_by { get; set; }
//        public Nullable<System.DateTime> deleted_date { get; set; }

//        [Required]
//        public bool delete_flag { get; set; }

//        [Required]
//        public int? status { get; set; }

//    }
//    public class GetLabTest
//    {
//        public int Id { get; set; }
//        public int Tst_CON_Id_FK { get; set; }
//        //public int Tst_CON_DO_Id { get; set; }
//        //public string? Tst_DO_Name { get; set; }
//        //public string? Tst_DO_MobNum { get; set; }
//        //public int Tst_CON_PR_Id { get; set; }
//        //public string? Tst_PR_Name { get; set; }
//        //public string? Tst_PR_Gender { get; set; }
//        //public string? Tst_PR_Age { get; set; }
//        //public long Tst_PR_MobNum { get; set; }
//        //public string? Tst_PR_Email { get; set; }
//        //public string? Tst_PR_Address { get; set; }
//        //public string? Tst_PR_BloodGroup { get; set; }
//        //public string? Tst_PR_Photo { get; set; }
//        //public string? Tst_PR_Taluk { get; set; }
//        //public string? Tst_PR_Village { get; set; }
//        //public string? Tst_PR_PostalCode { get; set; }
//        //public string? Fasting { get; set; }
//        //public string? Non_Fasting { get; set; }

//        public string? FastingORNonFasting { get; set; }
//        public int Lab_Invst_Id_FK { get; set; }
//        public string? Lab_Invst_Category { get; set; }
//        public int Lab_SubInvst_Id_FK { get; set; }
//        public string? Lab_SubInvst_Category { get; set; }
//        public string? LabTestReport { get; set; }
//        public bool delete_flag { get; set; }
//        public int? status { get; set; }

//    }
//    public class LabTestBy_Id
//    {
//        public int Id { get; set; }
//        public int Tst_CON_Id_FK { get; set; }
//        //public int Tst_CON_DO_Id { get; set; }
//        //public string? Tst_DO_Name { get; set; }
//        //public string? Tst_DO_MobNum { get; set; }
//        //public int Tst_CON_PR_Id { get; set; }
//        //public string? Tst_PR_Name { get; set; }
//        //public string? Tst_PR_Gender { get; set; }
//        //public string? Tst_PR_Age { get; set; }
//        //public long Tst_PR_MobNum { get; set; }
//        //public string? Tst_PR_Email { get; set; }
//        //public string? Tst_PR_Address { get; set; }
//        //public string? Tst_PR_BloodGroup { get; set; }
//        //public string? Tst_PR_Photo { get; set; }
//        //public string? Tst_PR_Taluk { get; set; }
//        //public string? Tst_PR_Village { get; set; }
//        //public string? Tst_PR_PostalCode { get; set; }
//        //public string? Fasting { get; set; }
//        //public string? Non_Fasting { get; set; }
//        public string? FastingORNonFasting { get; set; }
//        public int Lab_Invst_Id_FK { get; set; }
//        public string? Lab_Invst_Category { get; set; }
//        public int Lab_SubInvst_Id_FK { get; set; }
//        public string? Lab_SubInvst_Category { get; set; }
//        public string? LabTestReport { get; set; }
//        public bool delete_flag { get; set; }
//        public int? status { get; set; }

//    }
//}
