using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class DoctorDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int DDoc_Id { get; set; }


        [Display(Name = "Doctor")]
        public virtual int? DO_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("DO_Id")]
        public virtual Doctor? Doctor { get; set; }


        [Display(Name = "DocumentType")]
        public virtual int? doctype_id { get; set; }
        [JsonIgnore]
        [ForeignKey("doctype_id")]
        public virtual DocumentType? DocumentType { get; set; }

        public string? Choose_Document { get; set; }
        public int? Doc_UserId_FK { get; set; }
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
    public class GetAllDoctorDocument
    {
        public int? DDoc_Id { get; set; }
        public int? DO_Id { get; set; }
        public string? DO_Name { get; set; }
        public int? doctype_id { get; set; }
        public string? Doc_Name { get; set; }
        public string? Choose_Document { get; set; }
        public int? Doc_UserId_FK { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }

    }
    public class DoctorDocumentById
    {
        public int? DDoc_Id { get; set; }
        public int? DO_Id { get; set; }
        public string? DO_Name { get; set; }
        public int? doctype_id { get; set; }
        public string? Doc_Name { get; set; }
        public string? Choose_Document { get; set; }
        public int? Doc_UserId_FK { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }

    }
    public class Doctor_Documents
    {
        public int? DDoc_Id { get; set; }
        public int DO_Id { get; set; }
        public int? doctype_id { get; set; }
        public IFormFile[]? Choose_Document { get; set; }
        public int? Doc_UserId_FK { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
    }
    //public class Doctor_DocumentsUP
    //{
    //    public int? DDoc_Id { get; set; }
    //    public int? DO_Id { get; set; }
    //    public int? doctype_id { get; set; }
    //    public IFormFile[]? Choose_Document { get; set; }
    //    public int? Doc_UserId_FK { get; set; }
    //    public int? created_by { get; set; }
    //    public DateTime? created_date { get; set; }
    //    public int? modified_by { get; set; }
    //    public Nullable<System.DateTime> modified_date { get; set; }
    //    public int? deleted_by { get; set; }
    //    public Nullable<System.DateTime> deleted_date { get; set; }
    //    public bool delete_flag { get; set; }
    //    public int status { get; set; }
    //}

    public class Doctor_Documentstest
    {
        public int? DDoc_Id { get; set; }
        public int? DO_Id { get; set; }
        public int? doctype_id { get; set; }
        public int? Doc_UserId_FK { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }

    public class MyFileUploadsClass
    {
        public IFormFile[] Files { get; set; }
        public Doctor_Documentstest[] Doctor_Documentstests { get; set; }
        // other properties
    }

    public class Doctor_Documentsdemotet
    {
        public int? DDoc_Id { get; set; }
        public int DO_Id { get; set; }
        public int[]? doctype_id { get; set; }
        public IList<IFormFile>? Choose_Document { get; set; }
        public int? Doc_UserId_FK { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }

    //public class Doctor_Doc_File : Doctor_Documentsdemotet
    //{       
        
    //    public int? doctype_id { get; set; }
    //    public string? Choose_Document { get; set; }       
    //}

}
