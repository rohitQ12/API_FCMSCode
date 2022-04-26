using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Authentication
{
    public class AuthUser : IdentityUser
    {
        [MaxLength(128)]
        public string? Role_Id_FK { get; set; }
        [MaxLength(1)]
        public string? Inactive { get; set; }
        [MaxLength(150)]
        public string? FirstName { get; set; }
        [MaxLength(150)]
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? Imagename { get; set; }
        public bool IsEnabled { get; set; } 
    }
    public class AuthUser_Details:AuthUser
    {
        //public string? RoleIdFk { get; set; }
        public string? Rolename { get; set; }
        public IFormFile? Image { get; set; }
        public byte[]? Imagebyte { get; set; }
 
    }

}
