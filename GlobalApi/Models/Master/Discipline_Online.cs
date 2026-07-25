using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Discipline_Online
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int cd_id { get; set; }

        [StringLength(10)]
        public string? cd_code { get; set; }

        [StringLength(50)]
        public string? cd_clinicaldiscipline { get; set; }
        public string? created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public string? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public string? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
        public bool? delete_flag { get; set; }
        public int? record_status { get; set; }
        [StringLength(250)]
        public string? remarks { get; set; }
    }

    public class Discipline_DD_Online
    {
        public int cd_id { get; set; }
        public string? cd_code { get; set; }
        public string? cd_clinicaldiscipline { get; set; }
    }

    public class DisciplineById_Online
    {
        public int cd_id { get; set; }
        public string? cd_code { get; set; }
        public string? cd_clinicaldiscipline { get; set; }
        public bool? delete_flag { get; set; }
        public int? record_status { get; set; }
        public string? sts_name { get; set; }
        public string? remarks { get; set; }

    }

    public class GetAllDiscipline_Online
    {
        public int cd_id { get; set; }
        public string? cd_code { get; set; }
        public string? cd_clinicaldiscipline { get; set; }
        public bool? delete_flag { get; set; }
        public int? record_status { get; set; }
        public string? sts_name { get; set; }
        public string? remarks { get; set; }

    }
    public class ApproveDiscipline_Online
    {
        public int cd_id { get; set; }
        public string? remarks { get; set; }
    }
}
