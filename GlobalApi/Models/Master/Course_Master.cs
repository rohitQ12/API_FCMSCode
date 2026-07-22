using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Cms;

namespace GlobalApi.Models.Master
{
    public class Course_Master
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int cu_id { get; set; }
        public string cu_name { get; set; }
        public string? cu_ownername { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? cu_author { get; set; }
        public int cu_sc_id_fk { get; set; }
    }
    public class InsertCourse_Master
    {
        public string? cu_name { get; set; }
        public int cu_sc_id_fk { get; set; }
        public string? cu_author { get; set; }

    }

    public class UpdateCourse_Master
    {
        public int cu_id { get; set; }
        public string? cu_name { get; set; }
        public int cu_sc_id_fk { get; set; }
        public string? cu_author { get; set; }
    }
    public class GetCourse_Master_DD
    {
        public int cu_id { get; set; }
        public string? sc_name { get; set; }

    }

    public class GetAllCourse_Master
    {
        public int cu_id { get; set; }

        public int? ch_id { get; set; }

        public int? sc_id { get; set; }
        public string? ch_name { get; set; }
        public string? vi_name { get; set; }
        public int? vi_id { get; set; }
        public string? cu_name { get; set; }

        public string? number_of_chapters { get; set; }

        public string? cu_author { get; set; }
        //public int cu_sc_id_fk { get; set; }
        public string sc_name { get; set; }
        public string? ch_url { get; set; }
        public string? vi_file { get; set; }
        public decimal? vi_amount { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? vi_time { get; set; }

    }

    public class GetAllCoursebyname
    {
        public int cu_id { get; set; }

        public int ch_id { get; set; }

        public int sc_id { get; set; }
        public string? ch_name { get; set; }
        public string? vi_name { get; set; }
        public int? vi_id { get; set; }
        public string? cu_name { get; set; }
        public string? ins_name { get; set; }
        public string? co_name { get; set; }
        public string? ind_name { get; set; }
        public string? stu_name { get; set; }


        public string? number_of_chapters { get; set; }

        public string? cu_author { get; set; }
        //public int cu_sc_id_fk { get; set; }
        public string sc_name { get; set; }
        public string? ch_url { get; set; }
        public string? vi_file { get; set; }
        public decimal? vi_amount { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? vi_time { get; set; }

    }
    public class GetCourse_MasterById
    {
        public int cu_id { get; set; }
        public string? cu_name { get; set; }
        //public int cu_sc_id_fk { get; set; }

        public string? ch_url { get; set; }

        public int ch_id { get; set; }

        public int sc_id { get; set; }
        public string? ch_name { get; set; }
        public string? cu_author { get; set; }
        public int vi_id { get; set; }
        public string? vi_name { get; set; }

        public string sc_name { get; set; }
        public string? vi_url { get; set; }

        public string? vi_file { get; set; }
        public decimal? vi_amount { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? vi_time { get; set; }



    }



    public class ApproveCourse_Master
    {
        public int cu_id { get; set; }
        //public int status { get; set; }

    }

    public class GetCourDD
    {
        public string cu_name { get; set; }
        public decimal tot_vi_amount { get; set; }
        public string? cu_author { get; set; }
        public string total_time { get; set; }

    }

    public class GetSecDD
    {

        public int? cs_id { get; set; }
        public string? sc_name { get; set; }
        public decimal tot_vi_amount { get; set; }
      public string? cu_author { get; set; }
        public string total_time { get; set; }

    }
}