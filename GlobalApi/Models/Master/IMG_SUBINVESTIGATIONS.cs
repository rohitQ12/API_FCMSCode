using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class IMG_SUBINVESTIGATIONS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }

        [Display(Name = "IMG_INVESTIGATIONS")]
        public virtual int Img_Invt_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("Img_Invt_Id")]
        public virtual IMG_INVESTIGATIONS? IMG_INVESTIGATIONS { get; set; }


        [StringLength(100)]
        public string? Sub_Category { get; set; }
        public int? created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int? status { get; set; }

    }
    public class GetImgSubInsv
    {
        public int Id { get; set; }
        public int Img_Invt_Id { get; set; }
        public string? Category { get; set; }
        public string? Sub_Category { get; set; }
        public bool delete_flag { get; set; }
        public int? status { get; set; }

    }
    public class ImgSubInsvBy_Id
    {
        public int Id { get; set; }
        public int Img_Invt_Id { get; set; }
        public string? Category { get; set; }
        public string? Sub_Category { get; set; }
        public bool delete_flag { get; set; }
        public int? status { get; set; }

    }
    public class ImgSubInsv_DD
    {
        public int Id { get; set; }
        //public int Lab_Invt_Id_FK { get; set; }
        //public string? Category { get; set; }
        public string? Sub_Category { get; set; }

    }
}
