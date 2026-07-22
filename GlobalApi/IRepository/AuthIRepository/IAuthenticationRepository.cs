using GlobalApi.JsonFile;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
using Google.Apis.Auth;

namespace GlobalApi.IRepository.AuthIRepository
{
    public interface IAuthenticationRepository
    {
        Task<dynamic> RegisterUserAsync(RegisterModel model);
        Task<dynamic> ExtRegisterUserAsync(string Firstname, string Lastname, string Phonenumber, string Email, string Password, string Role_Id, int Id, string OTP);
        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
        Task<UserManagerResponse> ForgetPasswordAsync(string Username);

        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<UserManagerResponse> ResetPasswordAsync_Online(ResetPasswordViewModel_Online model);
        Task<UserManagerResponse> ForGoogle(string Token);
        Task<UserManagerResponse> ForFacebook(string accesstoken);
        Task<UserManagerResponse> ChangePasswordAsync(ChangePassword model, string userName);
        Task<bool> UpdateUserAsync(RegisterBindingModel model, string userName);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> ActivateInactivate(string userid);
        bool Userverification(string data);
        Task<bool> ApproveUser(string userid, string? Remarks);
        Task<Dictionary<string, object>> SendOTP(string Phonenumber, string TemplateId);
        Task<Dictionary<string, object>> SendOTP_Reg(string Phonenumber, string TemplateId);
        Task<Dictionary<string, object>> SendOTP_ForgotPassword(string Phonenumber, string TemplateId);
        Task<dynamic> VerifyOTP_ForgotPwd(VerifyOTP_ForgotPwd model);
     //   Task<dynamic> DoctorRegister(DoctorReg doctorReg);
      //  Task<dynamic> DoctorRegister_Online(DoctorReg_Online doctorReg);
       //Task<dynamic> PatientRegister(PatientReg patientReg);
     //   Task<dynamic> DiagCenterRegister(DiagnosticReg diagnosticReg);
        //Task<dynamic> PharmacyRegister(PharmacyReg pharmacyReg);
        //Task<dynamic> HospitalRegister(HospitalReg hospitalReg);
        Task<List<AuthUser_Details>> GetUser(string? roleaction, string? rolename, int? OfficeId);
        Task<AuthUser_Details> GetUserByname(string username);
        Task<AuthUser_Details> GetUsersByNumber(string username);
        Task<List<GetAllCoursebyname>> GetUserCourseByname(string username);

        //Task<dynamic> UserRegister(UserRegister userRegister);
        Task<UserRegisterResult> UserRegister(UserRegister userRegister);
        Task<List<getprofile>> GetProfile(string username);

        Task<string> UpdateUserProfile(AuthUser_Details model, string? rolename);
        Task<string> UserLogin(UserLogin login);
        //Online Portals
        // Task<dynamic> PatientRegister_Online(PatientReg_Online patientReg);
        Task<UserRoleCustomResponse> GetRoleId_MobileApp(string Phonenumber);

        Task<UserRegisterResult> PartnerRegister(PartnerRegister partnerRegister);
        Task<List<UserWithRoleViewModel>> GetAllUsersWithRoles();


    }
}
