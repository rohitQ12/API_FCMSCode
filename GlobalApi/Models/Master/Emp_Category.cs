using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Emp_Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int emp_cat_id { get; set; }
        public string? emp_cat_name { get; set; }
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
    public class Emp_Category_DD
    {
        public int emp_cat_id { get; set; }
        public string? emp_cat_name { get; set; }
    }

    public class Emp_CategoryById
    {
        public int emp_cat_id { get; set; }
        public string? emp_cat_name { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
}
