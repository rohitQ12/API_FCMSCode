using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class ComplaintMst
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Cmst_Id { get; set; }

        [StringLength(10)]
        public string Cmst_Code { get; set; }
        
        [StringLength(100)]
        public string Cmst_Name { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int status { get; set; }

    }
    public class ComplaintMst_DD
    {
        public int Cmst_Id { get; set; }
        public string Cmst_Code { get; set; }
        public string Cmst_Name { get; set; }

    }
}
