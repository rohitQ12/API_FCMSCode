using GlobalApi.Models.Authentication;
using GlobalApi.Data;
using GlobalApi.Models.AdminClaims;

namespace GlobalApi.IRepository.AdminIRepository
{
    public interface IUserRepository
    {
        Task<List<AuthUser_Details>> GetUser();
        Task<AuthUser_Details> GetUserByname(string username);
        Task<AuthUser> UpdateUserProfile(AuthUser_Details userProfile);
    }
}
