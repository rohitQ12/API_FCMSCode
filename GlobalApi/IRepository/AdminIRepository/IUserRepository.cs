using GlobalApi.Models.Authentication;
using GlobalApi.Data;
using GlobalApi.Models.AdminClaims;

namespace GlobalApi.IRepository.AdminIRepository
{
    public interface IUserRepository
    {
        Task<List<AuthUser_Details>> GetUser();
        Task<Profile> GetUserByname(string username);
        Task<Profile> UpdateUserProfile(Profile_Image userProfile);
    }
}
