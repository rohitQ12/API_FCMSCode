using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Authentication
{
    public class AspNetRole : IdentityRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleId { get; set; }
        public string Inactive { get; set; } = "N";
        public string? Rolecategory { get; set; }
    }
}
