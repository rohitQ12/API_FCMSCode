using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.Master
{
    public class feedback
    {
        [Key]
        public int feedback_id { get; set; }
        public int? section_id { get; set; }
        public string? section_name { get; set; }
        public string? comment { get; set; }
   
    }
    public class GetAllfeedback
    {
        public int feedback_id { get; set; }
        public string? comment { get; set; }


    }

}
