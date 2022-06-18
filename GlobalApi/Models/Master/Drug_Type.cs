using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Drug_Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Drug_type_Id { get; set; }

        [StringLength(50)]
        public string Drg_type_name { get; set; }
        public string? Drg_type_created_by { get; set; }
        public DateTime Drg_type_created_date { get; set; }
        public string? Drg_type_modified_by { get; set; }
        public DateTime? Drg_type_modified_date { get; set; }
        public string? Drg_type_deleted_by { get; set; }
        public DateTime? Drg_type_deleted_date { get; set; }
        [Required]
        public bool? Drg_type_delete_flag { get; set; }
        [Required]
        public int? Status { get; set; }
        public string? Remarks { get; set; }
    }

    public class Drug_TypeAll
    {
        public int Drug_type_Id { get; set; }
        public string Drg_type_name { get; set; }
        public bool? Drg_type_delete_flag { get; set; }
        public int? Status { get; set; }
        public string status_name { get; set; }
        public string? Remarks { get; set; }
    }
    public class DrugTypeapprove
    {
        public int Drug_type_Id { get; set; }
        public string? Remarks { get; set; }
    }

    public class Drug_TypeDD
    {
        public int Drug_type_Id { get; set; }
        public string Drg_type_name { get; set; }
    }
}
