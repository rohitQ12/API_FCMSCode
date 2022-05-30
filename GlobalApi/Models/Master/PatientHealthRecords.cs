using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class PatientHealthRecords
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int PHR_Id { get; set; }
        public int? Appt_Id { get; set; }
        public string? Choose_Document { get; set; }
        public int? Doc_UserId_FK { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int status { get; set; }
    }
    public class GetAllPHR
    {
        public int PHR_Id { get; set; }
        public int? Appt_Id { get; set; }
        public string? Choose_Document { get; set; }
        public int? Doc_UserId_FK { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
    }
    public class PHRById
    {
        public int PHR_Id { get; set; }
        public int? Appt_Id { get; set; }
        public string? Choose_Document { get; set; }
        public int? Doc_UserId_FK { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
    }
    public class PHR_Doc
    {
        public int PHR_Id { get; set; }
        public int? Appt_Id { get; set; }
        public IFormFile[]? Choose_Document { get; set; }
        public int? Doc_UserId_FK { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
    }
    public class PHR_DocUP
    {
        public int PHR_Id { get; set; }
        public int Appt_Id { get; set; }
        public IFormFile[]? Choose_Document { get; set; }
        public int? Doc_UserId_FK { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
    }

    public class Edit_ImageModel_PHR
    {
        public string? Choose_Document { get; set; }
    }

}
