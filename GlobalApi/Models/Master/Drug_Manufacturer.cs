using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Drug_Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Drg_manuf_id { get; set; }
        [StringLength(10)]
        public string? Drg_manuf_code { get; set; }
        [StringLength(100)]
        public string? Drg_manuf_name { get; set; }
        public DateTime? Drg_manuf_created_date { get; set; }
        public string? Drg_manuf_created_by { get; set; }
        public DateTime? Drg_manuf_modified_date { get; set; }
        public string? Drg_manuf_modified_by { get; set; }
        public DateTime? Drg_manuf_delete_date { get; set; }
        public string? Drg_manuf_deleted_by { get; set; }
        public bool? Drg_manuf_delete_flag { get; set; }
        public int? Status { get; set; }
        public string? Remarks { get; set; }

    }

    public class Drug_ManufacturerDD
    {
        public int Drg_manuf_id { get; set; }
        public string? Drg_manuf_code { get; set; }
        public string? Drg_manuf_name { get; set; }
        public int? Status { get; set; }

    }
}
