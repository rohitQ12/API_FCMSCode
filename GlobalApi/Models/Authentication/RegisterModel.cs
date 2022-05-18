using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.Authentication
{
    public class RegisterModel
    {
        //public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public int? OfficeId { get; set; }
        public string? RoleId { get; set; }
        public string? Phonenumber { get; set; }
        
    }

    public class SelfRegisterModel
    {
        //public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 6)]
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Phonenumber { get; set; }

    }

    public class LoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

    }
}
