using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.AdminClaims
{
    public class Userprofile
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } 
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string EmailID { get; set; }
        [StringLength(20)]
        public string Gender { get; set; }
        [StringLength(15)]
        public string Phonenumber { get; set; }

        
        public DateTime DOB { get; set; }
        [StringLength(100)]
        public string Image { get; set; }
    }
    public class UserProfile_Image
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailID { get; set; }
        public string Gender { get; set; }
        public string Phonenumber { get; set; }
        public DateTime DOB { get; set; }
        public IFormFile Image { get; set; }
    }
}
