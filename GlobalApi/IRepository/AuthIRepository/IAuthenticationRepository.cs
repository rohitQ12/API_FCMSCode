using GlobalApi.Models.Authentication;
using Google.Apis.Auth;

namespace GlobalApi.IRepository.AuthIRepository
{
    public interface IAuthenticationRepository
    {
        Task<UserManagerResponse> RegisterUserAsync(string Firstname, string Lastname, string Phonenumber,
                                                                 string Email, string Password, string Role_Id, int? OfficeId, IFormFile? Image);
        Task<UserManagerResponse> ExtRegisterUserAsync(string Firstname, string Lastname, string Phonenumber, string Email, string Password, string Role_Id);
        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
        Task<UserManagerResponse> ForgetPasswordAsync(string Username);
        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<UserManagerResponse> ForGoogle(string Token);
        Task<UserManagerResponse> ForFacebook(string accesstoken);
        Task<UserManagerResponse> ChangePasswordAsync(ChangePassword model);
        Task<bool> UpdateUserAsync(RegisterBindingModel model, string userName);
        Task<bool> DeleteUserAsync(string userId);
        Task<string> ActivateInactivate(string userid);
        bool Userverification(string data);
        Task<string> ApproveUser(string userid, string? Remarks);
    }
}
