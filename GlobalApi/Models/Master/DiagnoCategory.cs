using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class DiagnoCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? TypeId { get; set; }

        [StringLength(100)]
        public string name { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class Diagno_DD
    {
        public int id { get; set; }
        public string name { get; set; }

    }
    public class GetAllDiagnoCat
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? TypeId { get; set; }
        public string? Type_name { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }


    }

}

