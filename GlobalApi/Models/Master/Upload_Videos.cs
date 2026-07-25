using Microsoft.AspNetCore.Mvc.Formatters;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace GlobalApi.Models.Master
{
    public class Upload_Videos
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int vi_id { get; set; }
        public string vi_name { get; set; }
        public string vi_url { get; set; }

        public string? vi_file { get; set; }
        public decimal vi_amount { get; set; }
        public string vi_time { get; set; }

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

    public class vedio_Documents
    {
        public string vi_name { get; set; }
        public IFormFile vi_vedio { get; set; }
        public IFormFile? vi_file { get; set; }
        public decimal vi_amount { get; set; }

        public string vi_time { get; set; }


        public class GetAllVedio_Documents
        {
            public int vi_id { get; set; }
            public string vi_name { get; set; }
            public string? vi_url { get; set; }
            public string vi_time { get; set; }
            public string? vi_file { get; set; }
            public decimal? vi_amount { get; set; }
            public int status { get; set; }


        }

        public class GetAllVedio_DocumentsById
        {
            public int vi_id { get; set; }
            public string vi_name { get; set; }
            public string? vi_url { get; set; }
            public string vi_time { get; set; }
            public string? vi_file { get; set; }
            public decimal? vi_amount { get; set; }
            public int status { get; set; }


        }


        public class UpdateVedio_DocumentsById
        {

            public int vi_id { get; set; }
            public string vi_name { get; set; }
            public IFormFile? vi_url { get; set; }

            public string vi_time { get; set; }
            public IFormFile? vi_file { get; set; }
            public decimal? vi_amount { get; set; }

        }
        public class Approvevedio_Documents
        {
            public int vi_id { get; set; }

        }

    }


    }



