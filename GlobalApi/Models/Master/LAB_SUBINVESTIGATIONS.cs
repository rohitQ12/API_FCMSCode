using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class LAB_SUBINVESTIGATIONS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }

        [Display(Name = "LAB_INVESTIGATIONS")]
        public virtual int Lab_Invt_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Lab_Invt_Id_FK")]
        public virtual LAB_INVESTIGATIONS? LAB_INVESTIGATIONS { get; set; }


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
    public class GetLabSubInsv
    {
        public int Id { get; set; }
        public int Lab_Invt_Id_FK { get; set; }
        public string? Category { get; set; }
        public string? Sub_Category { get; set; }
        public bool delete_flag { get; set; }
        public int? status { get; set; }

    }
    public class LabSubInsvBy_Id
    {
        public int Id { get; set; }
        public int Lab_Invt_Id_FK { get; set; }
        public string? Category { get; set; }
        public string? Sub_Category { get; set; }
        public bool delete_flag { get; set; }
        public int? status { get; set; }

    }
    public class LabSubInsv_DD
    {
        public int Lab_SubInvst_Id { get; set; }
        //public int Lab_Invt_Id_FK { get; set; }
        //public string? Category { get; set; }
        public string? Sub_Category { get; set; }

    }

}
