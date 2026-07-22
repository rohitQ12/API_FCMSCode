using GlobalApi.Models.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.AdminClaims
{
    public class SubRoleClaims
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SRC_Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string? SRC_RoleId_FK { get; set; }
        [Required]
        public int SRC_SMF_Id_FK { get; set; }
        [ForeignKey("SRC_SMF_Id_FK")]
        public SubMenusFunctions SubMenuFunctions { get; set; }
        public int? SRC_SMFD_Id_FK { get; set; }
        public string? SRC_Value { get; set; }
        public int SRC_UserId_FK { get; set; }
        public DateTime SRC_INSTS { get; set; }
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
