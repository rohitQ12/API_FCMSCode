using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Drug_Units
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Drg_unit_id { get; set; }
        [Display(Name = "Drug_Type")]
        public virtual int Drg_Type_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Drg_Type_Id_FK")]
        public virtual Drug_Type? Drug_Type { get; set; }
        [StringLength(50)]
        public string Drg_Unit { get; set; }
        public string? Drg_unit_created_by { get; set; }
        public DateTime Drg_unit_created_date { get; set; }
        public string? Drg_unit_modified_by { get; set; }
        public DateTime? Drg_unit_modified_date { get; set; }
        public string? Drg_unit_deleted_by { get; set; }
        public DateTime? Drg_unit_deleted_date { get; set; }
        [Required]
        public bool? Drg_unit_delete_flag { get; set; }
        [Required]
        public int? Status { get; set; }
        public string? Remarks { get; set; }
    }

    public class Drug_UnitsAll
    {
        public int Drg_unit_id { get; set; }
        public int Drg_Type_Id_FK { get; set; }
        public string Drg_Type_Name { get; set; }
        public string Drg_Unit { get; set; }
        public bool? Drg_unit_delete_flag { get; set; }
        public int? Status { get; set; }
        public string status_name { get; set; }
        public string? Remarks { get; set; }
    }
    public class ApproveDrgunit
    {
        public int Drg_unit_id { get; set; }
        public string? Remarks { get; set; }
    }

    public class Drug_UnitDD
    {
        public int Drg_unit_id { get; set; }
        public int Drg_Type_Id_FK { get; set; }
        public string? Drug_type_name { get; set; }
        public string Drg_Unit { get; set; }
    }
}
