using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class ImgTestDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }

        [Display(Name = "ImgTest")]
        public virtual int Img_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Img_Id_FK")]
        public virtual ImgTest? ImgTest { get; set; }


        [Display(Name = "IMG_INVESTIGATIONS")]
        public virtual int Img_Invst_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("Img_Invst_Id")]
        public virtual IMG_INVESTIGATIONS? IMG_INVESTIGATIONS { get; set; }


        [Display(Name = "IMG_SUBINVESTIGATIONS")]
        public virtual int Img_SubInvst_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("Img_SubInvst_Id")]
        public virtual IMG_SUBINVESTIGATIONS? IMG_SUBINVESTIGATIONS { get; set; }

        [StringLength(300)]
        public string? ImgRemarks { get; set; }
        public string? Report { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

    }
    public class ImgTestDetailsById
    {
        public int Id { get; set; }
        public int Img_Id_FK { get; set; }
        public int? CON_Id_FK { get; set; }
        public int? Img_CON_DO_Id { get; set; }
        public string? Img_DO_Name { get; set; }
        public long Img_DO_MobNum { get; set; }
        public int? Img_CON_PR_Id { get; set; }
        public string? Img_PR_Name { get; set; }
        public string? Img_PR_Gender { get; set; }
        public string? Img_PR_Age { get; set; }
        //public long Img_PR_MobNum { get; set; }
        //public string? Img_PR_Email { get; set; }
        //public string? Img_PR_Address { get; set; }
        public string? Img_PR_BloodGroup { get; set; }
        //public string? Img_PR_Photo { get; set; }
        //public string? Img_PR_Taluk { get; set; }
        //public string? Img_PR_Village { get; set; }
        //public int Img_PR_PostalCode { get; set; }
        //public string? Fasting { get; set; }
        //public string? Non_Fasting { get; set; }
        public int? Img_Invst_Id { get; set; }
        public string? Img_Invst_Category { get; set; }
        public int? Img_SubInvst_Id { get; set; }
        public string? Img_SubInvst_Category { get; set; }
        public string? ImgRemarks { get; set; }
        public string? Report { get; set; }
        public bool delete_flag { get; set; }

    }
    public class GetAllImgTestDetails
    {
        public int Id { get; set; }
        public int Img_Id_FK { get; set; }
        public int? CON_Id_FK { get; set; }
        public int? Img_CON_DO_Id { get; set; }
        public string? Img_DO_Name { get; set; }
        public long Img_DO_MobNum { get; set; }
        public int? Img_CON_PR_Id { get; set; }
        public string? Img_PR_Name { get; set; }
        public string? Img_PR_Gender { get; set; }
        public string? Img_PR_Age { get; set; }
        //public long Img_PR_MobNum { get; set; }
        //public string? Img_PR_Email { get; set; }
        //public string? Img_PR_Address { get; set; }
        public string? Img_PR_BloodGroup { get; set; }
        //public string? Img_PR_Photo { get; set; }
        //public string? Img_PR_Taluk { get; set; }
        //public string? Img_PR_Village { get; set; }
        //public int Img_PR_PostalCode { get; set; }
        public int Img_Invst_Id { get; set; }
        public string? Img_Invst_Category { get; set; }
        public int Img_SubInvst_Id { get; set; }
        public string? Img_SubInvst_Category { get; set; }
        public string? ImgRemarks { get; set; }
        public string? Report { get; set; }
        public bool delete_flag { get; set; }
    }
    public class ImgReport
    {
        public int Id { get; set; }
        public int Img_Id_FK { get; set; }
        public int Img_Invst_Id { get; set; }
        public int Img_SubInvst_Id { get; set; }
        public string? ImgRemarks { get; set; }
        public IFormFile? Report { get; set; }
        public bool delete_flag { get; set; }

    }
    public class Edit_FileModel_ImgReport
    {
        public string? ImgReport { get; set; }
    }
}
