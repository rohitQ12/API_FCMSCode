using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sts_id { get; set; }

        [Required]
        [StringLength(50)]
        public string sts_name { get; set;}
    }
}
