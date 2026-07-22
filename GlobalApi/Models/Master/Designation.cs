using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Designation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int designation_id { get; set; }

        [StringLength(10)]
        public string designation_code { get; set; }

        [StringLength(50)]
        public string designation_desc { get; set; }
        public int? created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        
        [StringLength(250)]
        public string? Remarks { get; set; }

    }
    public class Designation_DD
    {
        public int designation_id { get; set; }
        public string designation_code { get; set; }
        public string designation_desc { get; set; }
    }

    public class DesignationById
    {
        public int designation_id { get; set; }
        public string designation_code { get; set; }
        public string designation_desc { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }


    }
    public class GetAllDesignation
    {
        public int designation_id { get; set; }
        public string designation_code { get; set; }
        public string designation_desc { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }


    }
    public class ApproveDesignation
    {
        public int designation_id { get; set; }
        public string? Remarks { get; set; }
    }

    public class NoDesigFound
    {
        public int? designation_id { get; set; }
        public string? designation_desc { get; set; }
    }
}