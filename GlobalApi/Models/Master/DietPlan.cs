using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class DietPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Dp_Id { get; set; }

        [Display(Name = "Consultation")]
        public virtual int DP_CON_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DP_CON_Id_FK")]
        public virtual Consultation? Consultation { get; set; }

        [StringLength(100)]
        public string? Dp_intake { get; set; }
        public int? Dp_duration { get; set; }

        [StringLength(100)]
        public string? Dp_dura_interof { get; set; }
        public string? Dp_instruction { get; set; }
        public int? created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int Status { get; set; }

    }
    public class GetAllDietPlan
    {
        public int Dp_Id { get; set; }
        public int DP_CON_Id_FK { get; set; }
        public string? Dp_intake { get; set; }
        public int? Dp_duration { get; set; }
        public string? Dp_dura_interof { get; set; }
        public string? Dp_instruction { get; set; }
        public string? Status_name { get; set; }
        public bool delete_flag { get; set; }
        public int Status { get; set; }

    }
}
