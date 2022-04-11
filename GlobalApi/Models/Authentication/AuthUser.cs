using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Authentication
{
    public class AuthUser : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        [Required]
        [MaxLength(128)]
        public string? Role_Id_FK { get; set; }

        [MaxLength(1)]
        public string? Inactive { get; set; }
        [MaxLength(150)]
        public string? FirstName { get; set; }
        [MaxLength(150)]
        public string? LastName { get; set; }
        public string? imagename { get; set; }
        public bool IsEnabled { get; set; } 
        //public string Phonenumber { get; set; }

    }
    public class AuthUser_Details
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public string? RoleIdFk { get; set; }
        public string? Rolename { get; set; }
        public string? Inactive { get; set; }
        [MaxLength(150)]
        public string? FirstName { get; set; }
        [MaxLength(150)]
        public string? LastName { get; set; }
        public string? imagename { get; set; }
        public bool IsEnabled { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }    
        //public string Phonenumber { get; set; }

    }
}
