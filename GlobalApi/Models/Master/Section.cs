using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.Master
{
    public class Section
    {
        [Key]
        public int sc_id { get; set; }
        public int? cs_id { get; set; }
        public string? sc_name { get; set; }
        public DateTime? created_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
    }


    public class GetSectionDD
    {
  
        public int? cs_id { get; set; }
        public string? sc_name { get; set; }
    }

    
}
