using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.Master
{
    public class Course_Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int sc_id { get; set; }
        public string sc_name { get; set; }
        public int sc_ch_Fk { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
    }

    public class InsertCourse_Section
    {
        public string? sc_name { get; set; }
        public int sc_ch_Fk { get; set; }

    }

    public class UpdateCourse_Section
    {
        public int sc_id { get; set; }
        public string? sc_name { get; set; }
        public int sc_ch_Fk { get; set; }

    }

    public class GetCourse_Section_DD
    {
        public int sc_id { get; set; }
        public string? sc_name { get; set; }

    }


    public class GetAllCourse_Section
    {
        public int sc_id { get; set; }
        public string? sc_name { get; set; }
        public int sc_ch_Fk { get; set; }

        public string ch_name { get; set; }
        public string vi_name { get; set; }
        public string? ch_url { get; set; }

        public string? vi_file { get; set; }
        public decimal? vi_amount { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }




        public class GetCourseSectionById
        {
            public int sc_id { get; set; }
            public string? sc_name { get; set; }
            public int sc_ch_Fk { get; set; }

            public string ch_name { get; set; }
            public string vi_name { get; set; }
           
            public string? ch_url { get; set; }

            public string? vi_file { get; set; }
            public decimal? vi_amount { get; set; }
            public int status { get; set; }
            public string? sts_name { get; set; }


        }



        public class ApproveCourse_Section
        {
            public int sc_id { get; set; }
            //public int status { get; set; }

        }


        public class GetCranialSectionDD
        {
            public int sc_id { get; set; }
            public string? sc_name { get; set; }

        }

        //public class GetSpinalSectionDD
        //{
        //    public int sc_id { get; set; }
        //    public string? sc_name { get; set; }

        //}

    }
}
