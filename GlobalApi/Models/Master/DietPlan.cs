using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class DietPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Display(Name = "Consultation")]
        public virtual int DP_CON_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DP_CON_Id_FK")]
        public virtual Consultation? Consultation { get; set; }

        [StringLength(250)]
        public string? BreakFast { get; set; }

        [StringLength(250)]
        public string? Lunch { get; set; }

        [StringLength(250)]
        public string? Dinner { get; set; }
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
    public class GetAllDietPlan
    {
        public int Id { get; set; }
        public int DP_CON_Id_FK { get; set; }
        public int? DP_CON_PR_ID_FK { get; set; }
        public string? DP_CON_PR_Name { get; set; }
        public string? DP_CON_Type { get; set; }
        public string? BreakFast { get; set; }
        public string? Lunch { get; set; }
        public string? Dinner { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class GetById
    {
        public int Id { get; set; }
        public int DP_CON_Id_FK { get; set; }
        public int? DP_CON_PR_ID_FK { get; set; }
        public string? DP_CON_PR_Name { get; set; }
        public string? DP_CON_Type { get; set; }
        public string? BreakFast { get; set; }
        public string? Lunch { get; set; }
        public string? Dinner { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
}
