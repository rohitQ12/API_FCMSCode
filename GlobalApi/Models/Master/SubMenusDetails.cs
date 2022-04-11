using GlobalApi.Models.AdminClaims;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class SubMenusDetails
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SMD_Id { get; set; }
        [StringLength(50)]
        public string? SMD_SubMenusFunction { get; set; }
        public bool SMD_IsClaimShown_In_UI { get; set; }
        public int SMD_SM_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("SMD_SM_Id_FK")]
        public virtual SubMenu? SubMenu { get; set; }
        public int Created_by { get; set; }
        public Nullable<System.DateTime> Created_date { get; set; }
        public int Modified_by { get; set; }
        public Nullable<System.DateTime> Modified_date { get; set; }
        public int Deleted_by { get; set; }
        public Nullable<System.DateTime> Deleted_date { get; set; }
        public bool Delete_flag { get; set; }
        public int Status { get; set; }
    }
}
