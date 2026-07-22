using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.AdminClaims
{
    public class RolesModels
    {
        //[Required(ErrorMessage = "OfficeTypeId is required")]
        //public int OfficeId { get; set; }
        //public int Off_Level { get; set; }
        //public string? OfficeName { get; set; }
        //public string? OfficeType { get; set; }
        //public int DistrictId { get; set; }
        //public string? DistrictName { get; set; }
        public string? RoleId { get; set; }
        [Required(ErrorMessage = "Rolename is required")]
        [MaxLength(256, ErrorMessage = "Role name can have maximum 256 characters only")]
        public string? RoleName { get; set; }
        public string? Inactive { get; set; }
    }
}
