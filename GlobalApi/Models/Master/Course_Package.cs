using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.Master
{
    public class Course_Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int cp_id { get; set; }
        public string? cu_name { get; set; }
       // public int? cp_se_id { get; set; }
       // public int? cp_ch_id { get; set; }
        public string? cp_Author { get; set; }
        public string? Discount { get; set; }
        public string? package_name { get; set; }
        public string? Duration { get; set; }
        public int? cu_sc_id_fk { get; set; }
        public int? cu_sec_fk { get; set; }
        public decimal? cp_amount { get; set; }
        public int? created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }

    public class Course_Pack
    {

        public int cp_id { get; set; }
        public string cu_name { get; set; }
        //  public int? cp_se_id { get; set; }
        // public int? cp_ch_id { get; set; }
        public string? cp_Author { get; set; }
       // public int? cu_sec_fk { get; set; }
        public int? cu_sc_id_fk { get; set; }
        public string? sec_name { get; set; }


        public decimal? cp_amount { get; set; }
        public string? Discount { get; set; }
        public string? package_name { get; set; }



    }

    public class GetAllCourse_Pack
    {

        public int? cp_id { get; set; }
       // public int? cu_id { get; set; }

        public string? cu_name { get; set; }
       // public int ch_id { get; set; }
       // public string? ch_name { get; set; }
       // public int? sc_id { get; set; }
        public string? cp_Author { get; set; }
         public int? cu_sc_id_fk { get; set; }
        public string? sec_name { get; set; }
        public decimal? cp_amount { get; set; }

        public string? Discount { get; set; }
        public string? package_name { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }


    }
    public class GetCourse_PackById
    {
        public int? cp_id { get; set; }
        //public int? cu_id { get; set; }

        public string? cu_name { get; set; }
    
        public string? Discount { get; set; }
        public string? Duration { get; set; }

        public int? cu_sc_id_fk { get; set; }
        //public string? sec_name { get; set; }
       public string? package_name { get; set; }
        public string? cp_Author { get; set; }

        public decimal? cp_amount { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
    }
    public class ApproveCourse_Pack
    {
        public int cp_id { get; set; }

    }

    public class GetPackageDD
    {

        public int cp_id { get; set; }
        public string? cu_name { get; set; }
    }
    public class GetCranialDD : GetPackageDD
    {

        public string? Duration { get; set; }
        public decimal? Amount { get; set; }
    }

    public class GetBothDD
    {
        public string? cu_name { get; set; }

        public string? Duration { get; set; }
        public decimal? Amount { get; set; }
    }
   


}
