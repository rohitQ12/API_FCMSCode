using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.Authentication
{
    public class RegisterBindingModel
    {
        public string? RoleId { get; set; }
        public string? UserId { get; set; }
        public int? OfficeId { get; set; }
        //[Required]
        [Display(Name = "User name")]
        public string? UserName { get; set; }
        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string? Password { get; set; }
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
