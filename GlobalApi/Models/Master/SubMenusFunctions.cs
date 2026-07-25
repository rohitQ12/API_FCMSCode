using GlobalApi.Models.AdminClaims;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class SubMenusFunctions
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SMF_Id { get; set; }
        [StringLength(50)]
        public string? SMF_label { get; set; }
        [StringLength(50)]
        public string? SMF_icon { get; set; }
        [StringLength(50)]
        public string? SMF_link { get; set; }
        public int SMF_SM_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("SMF_SM_Id_FK")]
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
    public class SubMenuFunctions_List
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SMF_Id { get; set; }
        [StringLength(50)]
        public string? SMF_label { get; set; }
        [StringLength(50)]
        public string? SMF_icon { get; set; }
        [StringLength(50)]
        public string? SMF_link { get; set; }
        public int SMF_SM_Id_FK { get; set; }
        public List<ClaimsModels> SubMenuFunctionClaim { get; set; }
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
