using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class LabTestingDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }


        [Display(Name = "LabTesting")]
        public virtual int LT_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("LT_Id_FK")]
        public virtual LabTesting? LabTesting { get; set; }


        [Display(Name = "LAB_INVESTIGATIONS")]
        public virtual int Lab_Invst_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("Lab_Invst_Id")]
        public virtual LAB_INVESTIGATIONS? LAB_INVESTIGATIONS { get; set; }


        [Display(Name = "LAB_SUBINVESTIGATIONS")]
        public virtual int Lab_SubInvst_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("Lab_SubInvst_Id")]
        public virtual LAB_SUBINVESTIGATIONS? LAB_SUBINVESTIGATIONS { get; set; }

        [StringLength(2)]
        public string? FastingORNonFasting { get; set; }

        [StringLength(300)]
        public string? Remarks { get; set; }
        public string? Report { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

    }
    public class LabTestingDetailsById
    {
        public int Id { get; set; }
        public int LT_Id_FK { get; set; }
        public int? Lab_CON_DO_Id { get; set; }
        public string? Lab_DO_Name { get; set; }
        public int? Lab_CON_PR_Id { get; set; }
        public string? Lab_PR_Name { get; set; }
        public string? Lab_PR_Gender { get; set; }
        public string? Lab_PR_Age { get; set; }
        public string? Lab_PR_BloodGroup { get; set; }
        public int Lab_Invst_Id { get; set; }
        public string? Lab_Invst_Category { get; set; }
        public int Lab_SubInvst_Id { get; set; }
        public string? Lab_SubInvst_Category { get; set; }
        public string? FastingORNonFasting { get; set; }
        public string? Remarks { get; set; }
        public string? Report { get; set; }
        public bool delete_flag { get; set; }

    }
    public class GetLabTestingDetails
    {
        public int Id { get; set; }
        public int LT_Id_FK { get; set; }
        public int? Lab_CON_DO_Id { get; set; }
        public string? Lab_DO_Name { get; set; }
        public int? Lab_CON_PR_Id { get; set; }
        public string? Lab_PR_Name { get; set; }
        public string? Lab_PR_Gender { get; set; }
        public string? Lab_PR_Age { get; set; }
        public string? Lab_PR_BloodGroup { get; set; }
        public int Lab_Invst_Id { get; set; }
        public string? Lab_Invst_Category { get; set; }
        public int Lab_SubInvst_Id { get; set; }
        public string? Lab_SubInvst_Category { get; set; }
        public string? FastingORNonFasting { get; set; }
        public string? Remarks { get; set; }
        public string? Report { get; set; }
        public bool delete_flag { get; set; }
    }
    public class TestReport
    {
        public int Id { get; set; }
        public int LT_Id_FK { get; set; }
        public int Lab_Invst_Id { get; set; }
        public int Lab_SubInvst_Id { get; set; }
        public string? FastingORNonFasting { get; set; }
        public string? Remarks { get; set; }
        public IFormFile? Report { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public bool delete_flag { get; set; }

    }
    public class Edit_FileModel_Report
    {
        public string? Report { get; set; }
    }
}
