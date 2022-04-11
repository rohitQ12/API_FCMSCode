using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class LAB_INVESTIGATIONS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Category { get; set; }
        public int? created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int? status { get; set; }

    }
    public class LabInsvBy_Id
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public bool delete_flag { get; set; }
        public int? status { get; set; }

    }
    public class LabInsv_DD
    {
        public int Id { get; set; }
        public string? Category { get; set; }

    }
}
