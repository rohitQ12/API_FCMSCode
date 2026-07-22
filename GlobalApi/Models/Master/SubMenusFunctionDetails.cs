using GlobalApi.Models.AdminClaims;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class SubMenusFunctionDetails
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SMFD_Id { get; set; }
        [StringLength(50)]
        public string? SMFD_SubMenusFunction { get; set; }
        public bool SMFD_IsClaimShown_In_UI { get; set; }
        public int SMFD_SMF_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("SMFD_SMF_Id_FK")]
        public virtual SubMenusFunctions? SubMenuFunctions { get; set; }
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
