using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Designation_Online
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int designation_id { get; set; }

        [StringLength(10)]
        public string? designation_code { get; set; }

        [StringLength(50)]
        public string? designation_desc { get; set; }
        public string? created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public string? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public string? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
        public bool? delete_flag { get; set; }
        public int? record_status { get; set; }

        [StringLength(250)]
        public string? remarks { get; set; }

    }

    public class Designation_DD_Online
    {
        public int designation_id { get; set; }
        public string? designation_code { get; set; }
        public string? designation_desc { get; set; }
    }

    public class DesignationById_Online
    {
        public int designation_id { get; set; }
        public string? designation_code { get; set; }
        public string? designation_desc { get; set; }
        public bool? delete_flag { get; set; }
        public int? record_status { get; set; }
        public string? sts_name { get; set; }
        public string? remarks { get; set; }

    }

    public class GetAllDesignation_Online
    {
        public int designation_id { get; set; }
        public string? designation_code { get; set; }
        public string? designation_desc { get; set; }
        public bool? delete_flag { get; set; }
        public int? record_status { get; set; }
        public string? sts_name { get; set; }
        public string? remarks { get; set; }


    }
    public class ApproveDesignation_Online
    {
        public int designation_id { get; set; }
        public string? remarks { get; set; }
    }


}
