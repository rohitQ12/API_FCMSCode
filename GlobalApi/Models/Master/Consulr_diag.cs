using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Consulr_diag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Con_diag_id { get; set; }
        /**************************************************************/
        [Display(Name = "Consultation")]
        public virtual int? Con_diag_conid_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Con_diag_conid_FK")]
        public virtual Consultation? Consultation { get; set; }
        /**************************************************************/
        public string? Con_diag_descrip { get; set; }
        public string? Con_diag_created_by { get; set; }
        public DateTime? Con_diag_created_date { get; set; }
        public string? Con_diag_modified_by { get; set; }
        public Nullable<System.DateTime> Con_diag_modified_date { get; set; }
        public string? Con_diag_deleted_by { get; set; }
        public Nullable<System.DateTime> Con_diag_deleted_date { get; set; }
        public bool? Con_diag_delete_flag { get; set; }
        public int? Status { get; set; }
        public string? Remarks { get; set; }
    }

    public class Consulr_diag_GetAll
    {
        public int Con_diag_id { get; set; }
        public int? Con_diag_conid_FK { get; set; }
        public string? Con_diag_descrip { get; set; }
        public int? Con_diag_created_by { get; set; }
        public bool? Con_diag_delete_flag { get; set; }
        public int? Status { get; set; }
        public string? status_name { get; set; }
        public string? Remarks { get; set; }
    }
}
