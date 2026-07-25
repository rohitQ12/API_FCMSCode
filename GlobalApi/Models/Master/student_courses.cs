using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace GlobalApi.Models.Master
{
    public class student_courses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Cou_Id { get; set; }

        [Display(Name = "Student")]
        public virtual int? Stu_id_fk { get; set; } = null!;
        [JsonIgnore]
        [ForeignKey("Stu_id_fk")]
        public virtual Student? Student { get; set; }
        [Display(Name = "Course_Package")]
        public virtual int cp_id { get; set; }
        [ForeignKey("cp_id")]
        public virtual Course_Package? Course_Package { get; set; }

        public int? created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }


        [Display(Name = "Corporate")]
        public virtual int? Co_id_fk { get; set; } = null!;
        [JsonIgnore]
        [ForeignKey("Co_id_fk")]
        public virtual Corporate? Corporate { get; set; }

        [Display(Name = "Institution")]
        public virtual int? Ins_id_fk { get; set; } = null!;
        [JsonIgnore]
        [ForeignKey("Ins_id_fk")]
        public virtual Institution? Institution { get; set; }
    }


    public class GetAllStudentCourse
    {
        public int? Cou_Id { get; set; }
        public int? cp_id { get; set; }
        public string cu_name { get; set; }
        public decimal? cp_amount { get; set; }
    }

    public class StudentCourse
    {
        public int? Cou_Id { get; set; }
        public int cp_id { get; set; }

    }
}
