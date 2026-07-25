using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.Authentication
{
    public class ChangePassword
    {
        //[Required]
        //public string Username { get; set; }


        [Required]
        //[DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

    }
}
