using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GlobalApi.Models.Master
{
    public class Partner
    {
        public int PartnerId { get; set; }
        public string? PartnerCode { get; set; }
        public string? PartnerName { get; set; }
        public string? PartGender { get; set; }
        public string? PartMobileNumber { get; set; }
        public string? PartEmail { get; set; }
        public string? PartAddress { get; set; }
        public string? PartCity { get; set; }
        public string? PartState { get; set; }
        public string? PartZipcode { get; set; }
        public string? PartLicenseNumber { get; set; }
        public DateTime? PartLicenseExpiryDate { get; set; }
        public string? PartIDNumber { get; set; }
        public string? PartVehicleType { get; set; }
        public string? PartVehicleNumber { get; set; }
        public string? Part_UserId { get; set; }
        public char Inactive { get; set; } = 'N';
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }

    public class PartnerRegister
    {
        public string? PartnerName { get; set; }
        public string PartMobileNumber { get; set; }
        public string? PartEmail { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
      
    }

}
