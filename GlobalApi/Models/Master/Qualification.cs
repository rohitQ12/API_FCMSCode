using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Qualification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int qualification_id { get; set; }

        [StringLength(10)]
        public string qualification_code { get; set; }

        [StringLength(50)]
        public string qualification_Name { get; set; }
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
        
        [StringLength(250)]
        public string? Remarks { get; set; }

    }
    public class Qualification_DD
    {
        public int qualification_id { get; set; }
        public string qualification_Name { get; set; }
    }
    public class QualificationById
    {
        public int qualification_id { get; set; }
        public string qualification_Name { get; set; }
        public string qualification_code { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }


    }
    public class GetAllQualification
    {
        public int qualification_id { get; set; }
        public string qualification_Name { get; set; }
        public string qualification_code { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }


    }
    public class ApproveQualification
    {
        public int qualification_id { get; set; }
        public string? Remarks { get; set; }
    }

    public class NoQualFound
    {
        public int? qualification_id { get; set; }
        public string? qualification_Name { get; set; }
    }
}