using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Enquire_Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int enq_id { get; set; }
        public string enq_name { get; set; }

    }

    public class GetAllEnquire_Type
    {
        public int? enq_id { get; set; }
        public string? enq_name { get; set; }

    }
}
