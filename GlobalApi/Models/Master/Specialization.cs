using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Specialization
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int SP_Id { get; set; }

        [StringLength(3)]
        public string? SP_Code { get; set; }


        [Display(Name = "Discipline")]
        public virtual int SP_CD_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("SP_CD_Id")]
        public virtual Discipline? Discipline { get; set; }


        [StringLength(50)]
        public string? SP_Specialization { get; set; }
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
    public class GetAllSpecialization
    {
        public int SP_Id { get; set; }
        public string? SP_Code { get; set; }
        public int SP_CD_Id { get; set; }
        public string? SP_CD_ClinicalDiscipline { get; set; }
        public string? SP_Specialization { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class Specialization_DD
    {
        public int SP_Id { get; set; }
        public string? SP_Code { get; set; }
        public string? SP_Specialization { get; set; }

    }
    public class SpecializationById
    {
        public int SP_Id { get; set; }
        public string? SP_Code { get; set; }
        public int SP_CD_Id { get; set; }
        public string? SP_CD_ClinicalDiscipline { get; set; }
        public string? SP_Specialization { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
}
