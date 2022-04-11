using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class PatientRxDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Rx_Id { get; set; }
        public DateTime Prescription_date { get; set; }

        [Display(Name = "Consultation")]
        public virtual int Rx_CON_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Rx_CON_Id_FK")]
        public virtual Consultation? Consultation { get; set; }

        [StringLength(50)]
        public string? Delivery_status { get; set; }
        public int? AcceptPrescription { get; set; }
        public int? created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int status { get; set; }
    }
    public class PatientRxDetailsById
    {
        public int Rx_Id { get; set; }
        public DateTime Prescription_date { get; set; }
        public int Rx_CON_Id_FK { get; set; }
        public int? Rx_CON_PR_ID_FK { get; set; }
        public string? Rx_CON_PR_Name { get; set; }
        public string? Rx_CON_Type { get; set; }
        public string? Delivery_status { get; set; }
        public int? AcceptPrescription { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class GetAllPatientRxDetails
    {
        public int Rx_Id { get; set; }
        public DateTime Prescription_date { get; set; }
        public int Rx_CON_Id_FK { get; set; }
        public int? Rx_CON_PR_ID_FK { get; set; }
        public string? Rx_CON_PR_Name { get; set; }
        public string? Rx_CON_Type { get; set; }
        public string? Delivery_status { get; set; }
        public int? AcceptPrescription { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class Prescription_Details
    {
        public int Rx_Id { get; set; }
        public DateTime Prescription_date { get; set; }
        public int Rx_CON_Id_FK { get; set; }
        public string? Delivery_status { get; set; }
        public int? AcceptPrescription { get; set; }
        public List<Patient_Prescription_DTL> Patient_Prescription_DTL { get; set; } = null!;
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class GetDrugForSpeedSearch
    {
        public int Id { get; set; }
        public string DrugName { get; set; }
        public int DT_Id_FK { get; set; }
        public string Strength { get; set; }
        public int UT_Id_FK { get; set; }
        public string Description { get; set; }
        public string? Instruction { get; set; }

    }
}
