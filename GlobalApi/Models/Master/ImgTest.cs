using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class ImgTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }
        public DateTime ImgRefDate { get; set; }

        [Display(Name = "Consultation")]
        public virtual int Img_CON_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Img_CON_Id_FK")]
        public virtual Consultation? Consultation { get; set; }

        [StringLength(50)]
        public string? Delivery_status { get; set; }
        public bool? AcceptImgTest { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }
        [Required]
        public int status { get; set; }


    }
    public class GetAllImgTest
    {
        public int Id { get; set; }
        public DateTime ImgRefDate { get; set; }
        public int Img_CON_Id_FK { get; set; }
        public int? Tst_CON_DO_Id { get; set; }
        public string? Tst_DO_Name { get; set; }
        public long? Tst_DO_MobNum { get; set; }
        public int? Tst_CON_PR_Id { get; set; }
        public string? Tst_PR_Name { get; set; }
        public string? Tst_PR_Gender { get; set; }
        public string? Tst_PR_Age { get; set; }
        public string? Tst_PR_BloodGroup { get; set; }
        public string? Delivery_status { get; set; }
        public bool? AcceptImgTest { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class ImgTestById
    {
        public int Id { get; set; }
        public DateTime ImgRefDate { get; set; }
        public int Img_CON_Id_FK { get; set; }
        public int? Tst_CON_DO_Id { get; set; }
        public string? Tst_DO_Name { get; set; }
        public long? Tst_DO_MobNum { get; set; }
        public int? Tst_CON_PR_Id { get; set; }
        public string? Tst_PR_Name { get; set; }
        public string? Tst_PR_Gender { get; set; }
        public string? Tst_PR_Age { get; set; }
        public string? Tst_PR_BloodGroup { get; set; }
        public string? Delivery_status { get; set; }
        public bool? AcceptImgTest { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class ImgTest_Details
    {
        public int Id { get; set; }
        public DateTime ImgRefDate { get; set; }
        public int Img_CON_Id_FK { get; set; }
        public List<ImgTestDetails> ImgTestDetails { get; set; } = null!;
        public string? Delivery_status { get; set; }
        public bool? AcceptImgTest { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }


    }
}
