using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Section_Id { get; set; }
        [StringLength(20)]
        public string? Section_Name { get; set; }
        [Display(Name = "Department")]
        public virtual int Dept_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("Dept_Id")]
        public virtual Department? Department { get; set; }
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
    public class Section_DD
    {
        public int Section_Id { get; set; }
        public string? Section_Name { get; set; }
    }
    public class SectionById
    {
        public int Section_Id { get; set; }
        public string? Section_Name { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class GetAllSection
    {
        public int Section_Id { get; set; }
        public string? Section_Name { get; set; }
        public int Dept_Id { get; set; }
        public string? Dept_name { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
}