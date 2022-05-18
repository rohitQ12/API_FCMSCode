using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class SkillSets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Skillset_id { get; set; }

        [StringLength(50)]
        public string? Skillset_name { get; set; }
        
        [StringLength(3)]
        public string? Skillset_Code { get; set; }

        [Display(Name = "Qualification")]
        public virtual int? qualification_id { get; set; }
        [JsonIgnore]
        [ForeignKey("qualification_id")]
        public virtual Qualification? Qualification { get; set; }
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
    public class SkillSet_DD
    {
        public int Skillset_id { get; set; }
        public string? Skillset_name { get; set; }
    }
    public class SkillSetById
    {
        public int Skillset_id { get; set; }
        public string? Skillset_name { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class Qual_SkillSet
    {
        public int Skillset_id { get; set; }
        public string? Skillset_name { get; set; }
        public int? qualification_id { get; set; }
        public string? qualification_Name { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
}