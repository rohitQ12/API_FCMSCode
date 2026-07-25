using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GlobalApi.GlobalClasses;


namespace GlobalApi.Models.Authentication
{
    public class UserVerfication
    {
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }

		[StringLength(10)]
		public string? OTP { get; set; }
		public long? Phonenumber { get; set; }
		public string? Email { get; set; }
		public int? ExpiryTime { get; set; }
		public Nullable<System.DateTime> OTPCreatedDate { get; set; }
	}

    public class UserVerfication_Online
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long verify_id { get; set; }

        [StringLength(10)]
        public string? otp { get; set; }
        public string? phone_number { get; set; }
        public string? email { get; set; }
        public int? expiry_time { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
    }
}
