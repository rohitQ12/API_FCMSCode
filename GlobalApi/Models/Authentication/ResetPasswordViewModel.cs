using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.Authentication
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class ResetPasswordViewModel_Online
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public int? Id { get; set; }
        public string? OTP { get; set; }
        public string? user_type { get; set; }
    }

    public class VerifyOTP_ForgotPwd
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public string OTP { get; set; }
        public string? user_type { get; set; }
    }

    public class VerifyOTP_Online
    {
        [Required]
        public string user_name { get; set; }
        [Required]
        public int verify_id { get; set; }
        [Required]
        public string otp { get; set; }
    }
}
