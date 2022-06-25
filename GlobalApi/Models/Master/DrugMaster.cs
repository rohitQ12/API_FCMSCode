using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class DrugMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Drg_mst_id { get; set; }
        public string? Drug_code { get; set; }
        public string Drg_name { get; set; }

        [Display(Name = "Drug_Type")]
        public virtual int Drg_type_id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Drg_type_id_FK")]
        public virtual Drug_Type? Drug_Type { get; set; }
        public int? Drg_strength { get; set; }

        [Display(Name = "Drug_Units")]
        public virtual int Drg_unit_id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Drg_unit_id_FK")]
        public virtual Drug_Units? Drug_Units { get; set; }
        [Display(Name = "Drug_Manufacturer")]
        public virtual int? Drg_manufacturer_id_FK { get; set; } 
        [JsonIgnore]
        [ForeignKey("Drg_manufacturer_id_FK")]
        public virtual Drug_Manufacturer? Drug_Manufacturer { get; set; }
        public string? Drg_warnings { get; set; }
        public string? Discription { get; set; } 
        public string? Instruction { get; set; }
        [StringLength(50)]
        public string? Drg_medcine_type { get; set; }
        public string? Drg_mst_created_by { get; set; }
        public DateTime Drg_mst_created_date { get; set; }
        public string? Drg_mst_modified_by { get; set; }
        public DateTime? Drg_mst_modified_date { get; set; }
        public string? Drg_mst_deletd_by { get; set; }
        public DateTime? Drg_mst_deleted_date { get; set; }
        public bool? Drg_mst_delete_flag { get; set; }
        public int? Status { get; set; }
        public string? Remarks { get; set; }

    }
    public class GetAllDrugMaster
    {
        public int Drg_mst_id { get; set; }
        public string Drg_name { get; set; }
        public string? Drug_code { get; set; }
        public int? Drg_type_id_FK { get; set; }
        public string? Drg_type_name { get; set; }
        public int? Drg_strength { get; set; }
        public int? Drg_unit_id_FK { get; set; }
        public string? Drg_Unit { get; set; }
        public string? Discription { get; set; }
        public string? Drg_warnings { get; set; }
        public int? Drg_manufacturer_id_FK { get; set; }
        public string? Drg_manuf_name { get; set; }
        public string? Drg_medcine_type { get; set; }
        public string? Instruction { get; set; }
        public bool? Drg_mst_delete_flag { get; set; }
        public int? Status { get; set; }
        public string? status_name { get; set; }
        public string? Remarks { get; set; }

    }

    public class ApproveDrgMst
    {
        public int Drg_mst_id { get; set; }
        public string? Remarks { get; set; }
    }

    public class DrugMasterDD
    {
        public int Drg_mst_id { get; set; }
        public string Drg_name { get; set; }
        public string? Drug_code { get; set; }
        public int Drg_type_id_FK { get; set; }
        public string Drg_type_name { get; set; }
        public int? Drg_strength { get; set; }
        public int Drg_unit_id_FK { get; set; }
        public string Drg_Unit { get; set; }
        public string? Drg_warnings { get; set; }
        public int? Drg_manufacturer_id_FK { get; set; }
        public string? Drg_manuf_name { get; set; }
        public string? Drg_medcine_type { get; set; }
    }
}
