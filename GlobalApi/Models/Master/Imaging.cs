//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;

//namespace GlobalApi.Models.Master
//{
//    public class Imaging
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.None)]
//        [Required]
//        public int Id { get; set; }

//        [Display(Name = "Consultation")]
//        public virtual int Img_CON_Id_FK { get; set; }
//        [JsonIgnore]
//        [ForeignKey("Img_CON_Id_FK")]
//        public virtual Consultation? Consultation { get; set; }


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
//        public string? ImgTestReport { get; set; }
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
//    public class GetImaging
//    {
//        public int Id { get; set; }
//        public int Img_CON_Id_FK { get; set; }
//        public string? Img_CON_Weight { get; set; }
//        public int Img_Invst_Id_FK { get; set; }
//        public string? Img_Invst_Category { get; set; }
//        public int Img_SubInvst_Id_FK { get; set; }
//        public string? Img_SubInvst_Category { get; set; }
//        public string? ImgTestReport { get; set; }
//        public bool delete_flag { get; set; }
//        public int? status { get; set; }

//    }
//    public class ImagingBy_Id
//    {
//        public int Id { get; set; }
//        public int Img_CON_Id_FK { get; set; }
//        public string? Img_CON_Weight { get; set; }
//        public int Img_Invst_Id_FK { get; set; }
//        public string? Img_Invst_Category { get; set; }
//        public int Img_SubInvst_Id_FK { get; set; }
//        public string? Img_SubInvst_Category { get; set; }
//        public string? ImgTestReport { get; set; }
//        public bool delete_flag { get; set; }
//        public int? status { get; set; }

//    }
//}
