using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Drug_Prescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Prc_id { get; set; }
        /****************************************************/
        [Display(Name = "Consultation")]
        public virtual int? Prc_CONS_id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Prc_CONS_id_FK")]
        public virtual Consultation? Consultation { get; set; }
        /****************************************************/
        [Display(Name = "DrugMaster")]
        public virtual int? Prc_drug_id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Prc_drug_id_FK")]
        public virtual DrugMaster? DrugMaster { get; set; }
        public int? Prc_dosage_qty { get; set; }
        /****************************************************/
        [Display(Name = "Drug_Type")]
        public virtual int? Prc_drug_type_id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Prc_drug_type_id_FK")]
        public virtual Drug_Type? Drug_Type { get; set; }
        /****************************************************/
        [Display(Name = "Drug_Units")]
        public virtual int? Prc_Unit_id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Prc_Unit_id_FK")]
        public virtual Drug_Units? Drug_Units { get; set; }
        /****************************************************/
        [Display(Name = "Drug_Frequency")]
        public virtual int? Prc_drg_frequency_id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Prc_drg_frequency_id_FK")]
        public virtual Drug_Frequency? Drug_Frequency { get; set; }
        /****************************************************/
        public string? Prc_custom_freuency { get; set; }
        public string? Prc_intake { get; set; }
        public string? Prc_intake_instaruction { get; set; }
        public int? Prc_drug_duration { get; set; }
        public string? Prc_duration_intermof { get; set; }
        public string? Prc_created_by { get; set; }
        public DateTime? Prc_created_date { get; set; }
        public string? Prc_modified_by { get; set; }
        public DateTime? Prc_modified_date { get; set; }
        public string? Prc_deleted_by { get; set; }
        public DateTime? Prc_deleted_date { get; set; }
        public bool? Prc_delete_flag { get; set; }
        public int? Status { get; set; }
        public string? Remarks { get; set; }
    }

    public class Drug_PrescriptionAll
    {
        public int Prc_id { get; set; }
        public  int? Prc_CONS_id_FK { get; set; }
        public  int? Prc_drug_id_FK { get; set; }
        public string? Prc_Drg_name { get; set; }
        public int? Prc_drug_type_id_FK { get; set; }
        public string? Prc_drug_type_name { get; set; }
        public int? Prc_dosage_qty { get; set; }
        public string? Drg_Unit { get; set; }
        public  int? Prc_Unit_id_FK { get; set; }
        public string? Drg_frq_name { get; set; }
        public string? Drg_frq_order { get; set; }
        public  int? Prc_drg_frequency_id_FK { get; set; }
        public string? Prc_custom_freuency { get; set; }
        public string? Prc_intake { get; set; }
        public string? Prc_intake_instaruction { get; set; }
        public int? Prc_drug_duration { get; set; }
        public string? Prc_duration_intermof { get; set; }
        public bool? Prc_delete_flag { get; set; }
        public int? Status { get; set; }
        public string? status_name { get; set; }
        public string? Remarks { get; set; }
    }
}
