using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Authentication
{
    public class AuthUser : IdentityUser
    {
        [MaxLength(128)]
        public string? Role_Id_FK { get; set; }
        public int? UserId { get; set; }
        [MaxLength(1)]
        public string? Inactive { get; set; }
        [MaxLength(150)]
        public string? FirstName { get; set; }
        [MaxLength(150)]
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? Imagename { get; set; }
        public bool IsEnabled { get; set; }

        [StringLength(250)]
        public string? Remarks { get; set; }
        public string? OTP { get; set; }
    }
    public class AuthUser_Details : AuthUser
    {
        public string? RoleIdFk { get; set; }
        public string? Rolename { get; set; }
        public IFormFile? Image { get; set; }
        public byte[]? Imagebyte { get; set; }
        public string? ProfilePicture { get; set; }

        public List<GetAllCourse_Master>? GetAllCourse_Master { get; set; }

        


    }

    public class AuthUser_Details_New
    {

        public string? id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? email { get; set; }
        public bool emailConfirmed { get; set; }
        public string? phoneNumber { get; set; }
        public bool phoneNumberConfirmed { get; set; }
        public string? Role_Id_FK { get; set; }
        public string? rolename { get; set; }
        public string? userName { get; set; }
        public string? Imagename { get; set; }
        public string? Image_Url { get; set; }

    }
    public class getprofile
    {
        public string? Stu_MobileNumber { get; set; }
        public string? Stu_Name { get; set; }
        public string? cu_name { get; set; }
        public string? vi_time { get; set; }
        public string? vi_name { get; set; }
        public string? ch_name { get; set; }




    }
    public class getdoctorprofile
    {
        public int? Id { get; set; }
        public string? UserId { get; set; }
        public string? RoleID { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Mobilenumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? Age { get; set; }
        public string? Photo { get; set; }
        public string? DO_RegNo { get; set; }
        public int? Country_Id_FK { get; set; }
        public int? State_Id_FK { get; set; }
        public int? District_Id_FK { get; set; }
        public int? Taluk_Id_FK { get; set; }
        public int? Gram_Id_FK { get; set; }
        public string? Regno { get; set; }
        public string? Country_name { get; set; }
        public string? State_name { get; set; }
        public string? District_name { get; set; }
        public string? Taluk_name { get; set; }
        public string? Gram_name { get; set; }
        public int? Postalcode { get; set; }
        public int? Qualification_id { get; set; }
        public int? Designation_id { get; set; }
        public int? Discipline_id { get; set; }
        public int? Specialization_id { get; set; }
        public string? Qualification_name { get; set; }
        public string? Designation_name { get; set; }
        public string? Discipline_name { get; set; }
        public string? Specialization_name { get; set; }
        public string? ClinicName { get; set; }
        public string? DO_Type { get; set; }

    }

}
