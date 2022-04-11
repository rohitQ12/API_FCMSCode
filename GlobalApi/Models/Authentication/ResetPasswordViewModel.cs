using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.Authentication
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string NewPassword { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }
    }
}
