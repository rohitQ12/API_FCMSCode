using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Emp_Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int emptype_id { get; set; }
        public string? emptype_name { get; set; }
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
    public class Emp_Type_DD
    {
        public int emptype_id { get; set; }
        public string? emptype_name { get; set; }
    }

    public class Emp_TypeById
    {
        public int emptype_id { get; set; }
        public string? emptype_name { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
    }
}
