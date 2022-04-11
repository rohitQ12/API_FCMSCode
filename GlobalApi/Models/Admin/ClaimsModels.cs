using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.AdminClaims
{
    public class ClaimsModels
    {
        [Key]
        [Required]
        public int ClaimTypeId { get; set; }

        [Required]
        public bool IsClaimShown { get; set; }

        [Required]
        public string? ClaimType { get; set; }

        [Required]
        public bool ClaimValue { get; set; }
    }
}
