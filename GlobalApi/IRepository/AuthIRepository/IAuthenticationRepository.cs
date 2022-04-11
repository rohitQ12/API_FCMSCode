using GlobalApi.Models.Authentication;
using Google.Apis.Auth;

namespace GlobalApi.IRepository.AuthIRepository
{
    public interface IAuthenticationRepository
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterModel model);
        Task<UserManagerResponse> ExtRegisterUserAsync(RegisterModel model);
        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
        Task<UserManagerResponse> ForgetPasswordAsync(string Username);
        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<UserManagerResponse> ForGoogle(string Token);
        Task<UserManagerResponse> ForFacebook(string accesstoken);
        Task<UserManagerResponse> ChangePasswordAsync(ChangePassword model);
        Task<bool> UpdateUserAsync(RegisterBindingModel model, string userName);
        Task<bool> DeleteUserAsync(string userId);
        Task<string> ActivateInactivate(string userid);
        bool Phonenumber(string phonenumber);
    }
}
