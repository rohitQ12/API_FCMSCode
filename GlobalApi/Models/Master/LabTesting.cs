using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class LabTesting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }
        public DateTime TstRefDate { get; set; }

        [Display(Name = "Consultation")]
        public virtual int Tst_CON_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Tst_CON_Id_FK")]
        public virtual Consultation? Consultation { get; set; }

        public bool? AcceptLabTest { get; set; }
        public DateTime? SampleTaken { get; set; }

        [StringLength(50)]
        public string? Delivery_status { get; set; }

        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int status { get; set; }

    }
    public class GetLabTestings
    {
        public int Id { get; set; }
        public DateTime TstRefDate { get; set; }
        public int Tst_CON_Id_FK { get; set; }
        //public int? Tst_CON_DO_Id { get; set; }
        public string? Tst_DO_Name { get; set; }
        public long? Tst_DO_MobNum { get; set; }
        //public int? Tst_CON_PR_Id { get; set; }
        public string? Tst_PR_Name { get; set; }
        public string? Tst_PR_Gender { get; set; }
        public string? Tst_PR_Age { get; set; }
        public string? Tst_PR_BloodGroup { get; set; }
        public bool? AcceptLabTest { get; set; }
        public DateTime? SampleTaken { get; set; }
        public string? Delivery_status { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class LabTestingsById
    {
        public int Id { get; set; }
        public DateTime TstRefDate { get; set; }
        public int Tst_CON_Id_FK { get; set; }
        //public int? Tst_CON_DO_Id { get; set; }
        public string? Tst_DO_Name { get; set; }
        public long? Tst_DO_MobNum { get; set; }
        //public int? Tst_CON_PR_Id { get; set; }
        public string? Tst_PR_Name { get; set; }
        public string? Tst_PR_Gender { get; set; }
        public string? Tst_PR_Age { get; set; }
        public string? Tst_PR_BloodGroup { get; set; }
        public bool? AcceptLabTest { get; set; }
        public DateTime? SampleTaken { get; set; }
        public string? Delivery_status { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }

    public class LabTesting_Details
    {
        public int Id { get; set; }
        public DateTime TstRefDate { get; set; }
        public int Tst_CON_Id_FK { get; set; }
        public List<LabTestingDetails> LabTestingDetails { get; set; } = null!;
        public bool? AcceptLabTest { get; set; }
        public DateTime? SampleTaken { get; set; }
        public string? Delivery_status { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
}
