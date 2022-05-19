using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Diseases
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        public string? Diseases_Code { get; set; }

        public string? Diseases_Name { get; set; }

        [StringLength(50)]
        public string? Acronyms { get; set; }
        
        [Display(Name = "Specialization")]
        public virtual int? Dis_SP_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Dis_SP_Id_FK")]
        public virtual Specialization? Specialization { get; set; }

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

    public class DiseasesBy_Id
    {
        public int Id { get; set; }
        public string? Diseases_Code { get; set; }
        public string? Diseases_Name { get; set; }
        public string? Acronyms { get; set; }
        public int? Dis_SP_Id_FK { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }

    public class Diseases_DD
    {
        public int Id { get; set; }
        public string? Diseases_Code { get; set; }
        public string? Diseases_Name { get; set; }
        public int? Dis_SP_Id_FK { get; set; }


    }

}
