using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GlobalApi.Models.Master
{
    public class Course_Chapters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int ch_id { get; set; }

        [StringLength(100)]
        public string? ch_name { get; set; }

        public int ch_vi_FK { get; set; }

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
    public class InsertCourse_Chapters
    {
        public string? ch_name { get; set; }
        public int ch_vi_FK { get; set; }

    }

    public class UpdateCourse_Chapters
    {
        public int ch_id { get; set; }
        public string? ch_name { get; set; }
        public int ch_vi_FK { get; set; }

    }

    public class GetCourse_Chapters_DD
    {
        public int ch_id { get; set; }
        public string? ch_name { get; set; }
        
    }


    public class GetAllCourse_Chapters
    {
        public int ch_id { get; set; }
        public string? ch_name { get; set; }
        public int ch_vi_FK { get; set; }

        public string vi_name { get; set; }
        public string? vi_url { get; set; }

        public string? vi_file { get; set; }
        public decimal? vi_amount { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }


    }

    public class GetCourseChaptersById
    {
        public int ch_id { get; set; }
        public string? ch_name { get; set; }
        public int ch_vi_FK { get; set; }

        public string vi_name { get; set; }
        public string? vi_url { get; set; }

        public string? vi_file { get; set; }
        public decimal? vi_amount { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }


    }
    public class ApproveCourse_Chapters
    {
      
        public int ch_id { get; set; }

    }







}
