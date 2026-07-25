using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Discipline
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int CD_Id { get; set; }

        [StringLength(10)]
        public string? CD_Code { get; set; }

        [StringLength(50)]
        public string? CD_ClinicalDiscipline { get; set; }
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

        [StringLength(250)]
        public string? Remarks { get; set; }


    }
    public class Discipline_DD
    {
        public int CD_Id { get; set; }
        public string? CD_Code { get; set; }
        public string? CD_ClinicalDiscipline { get; set; }
    }
    public class DisciplineById
    {
        public int CD_Id { get; set; }
        public string? CD_Code { get; set; }
        public string? CD_ClinicalDiscipline { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }

    }
    public class GetAllDiscipline
    {
        public int CD_Id { get; set; }
        public string? CD_Code { get; set; }
        public string? CD_ClinicalDiscipline { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }

    }
    public class ApproveDiscipline
    {
        public int CD_Id { get; set; }
        public string? Remarks { get; set; }
    }

    public class NoDiscFound
    {
        public int? CD_Id { get; set; }
        public string? CD_ClinicalDiscipline { get; set; }
    }

}
