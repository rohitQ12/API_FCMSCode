using GlobalApi.Models.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.AdminClaims
{
    public class RoleClaims
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RC_Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string RC_RoleId_FK { get; set; }
        public string? PageFunctionName { get; set; }
        [Required]
        public int RC_SM_Id_FK { get; set; }
        [ForeignKey("RC_SM_Id_FK")]
        //public SubMenu SubMenu { get; set; }
        public int? RC_M_Id_FK { get; set; }
        public int? RC_SMD_Id_FK { get; set; }
        [MaxLength(5)]
        public string? RC_Value { get; set; }
        public int RC_UserId_FK { get; set; }
        public DateTime RC_INSTS { get; set; }
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
