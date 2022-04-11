using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class PatientDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Doc_Id { get; set; }


        [Display(Name = "Patient")]
        public virtual int PR_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("PR_Id_FK")]
        public virtual Patient? Patient { get; set; }


        [Display(Name = "DocumentType")]
        public virtual int Doc_Type_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Doc_Type_Id_FK")]
        public virtual DocumentType? DocumentType { get; set; }

        [StringLength(50)]
        public string Choose_Document { get; set; }
        public int Doc_UserId_FK { get; set; }
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
    public class GetAllPatientDocument
    {
        public int Doc_Id { get; set; }
        public int PR_Id_FK { get; set; }
        public string? PR_Name { get; set; }
        public int Doc_Type_Id_FK { get; set; }
        public string? Doc_Name { get; set; }
        public string Choose_Document { get; set; }
        public int Doc_UserId_FK { get; set; }

    }
    public class PatientDocumentById
    {
        public int Doc_Id { get; set; }
        public int PR_Id_FK { get; set; }
        public string? PR_Name { get; set; }
        public int Doc_Type_Id_FK { get; set; }
        public string? Doc_Name { get; set; }
        public string Choose_Document { get; set; }
        public int Doc_UserId_FK { get; set; }


    }
    public class Patient_Documents
    {
        public int Doc_Id { get; set; }
        public int PR_Id_FK { get; set; }
        public int Doc_Type_Id_FK { get; set; }
        public IFormFile Choose_Document { get; set; }
        public int Doc_UserId_FK { get; set; }
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
