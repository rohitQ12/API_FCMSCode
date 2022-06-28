using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Drug_Frequency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Drg_freq_Id { get; set; }
        public string? Drg_frq_name { get; set; }
        public string? Drg_frq_order { get; set; }
        public string? Drg_frq_created_by { get; set; }
        public DateTime Drg_frq_created_date { get; set; }
        public string? Drg_frq_modified_by { get; set; }
        public DateTime? Drg_frq_modified_date { get; set; }
        public string? Drg_frq_deleted_by { get; set; }
        public DateTime? Drg_frq_deleted_date { get; set; }
        [Required]
        public bool? Drg_frq_delete_flag { get; set; }
        [Required]
        public int? status { get; set; }
        public string? Remarks { get; set; }
    }

    public class Drug_FrequencyAll
    {
        public int Drg_freq_Id { get; set; }
        public string? Drg_frq_name { get; set; }
        public string? Drg_frq_order { get; set; }
        public bool? Drg_frq_delete_flag { get; set; }
        public int? status { get; set; }
        public string status_name { get; set; }
        public string? Remarks { get; set; }
    }
    public class DrugFrequencyapprove
    {
        public int Drg_freq_Id { get; set; }
        public string? Remarks { get; set; }
    }

    public class Drug_FrequencyDD
    {
        public int Drg_freq_Id { get; set; }
        public string? Drg_frq_name { get; set; }
        public string? Drg_frq_order { get; set; }
    }
}
