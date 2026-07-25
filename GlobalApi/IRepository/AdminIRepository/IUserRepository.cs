using GlobalApi.Models.Authentication;
using GlobalApi.Data;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.AdminIRepository
{
    public interface IUserRepository
    {
        Task<List<AuthUser_Details>> GetUser(string? roleaction, string? rolename, int? OfficeId);
        Task<AuthUser_Details> GetUserByname(string username);
        Task<string> UpdateUserProfile(string Id, IFormFile Image,
            string Email, string PhoneNumber, string FirstName, string LastName, string Gender, DateTime? DOB);
        //Task<List<getpatientprofile>> GetPatientProfile(string username);
        Task<dynamic> GetPatientProfile(string username);
        Task<dynamic> GetDoctorProfile(string username);
      //  Task<getdoctorprofile_online> GetDoctorProfile_Online(string username);
        Task<AuthUser_Details_New> GetUserByname_New(string username);
        Task<dynamic> GetPatientProfile_Online(string username);

    }
}
