using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.Models.Authentication;
using GlobalApi.Repository.AdminRepository;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GlobalApi.Models.AdminClaims;
using Microsoft.EntityFrameworkCore;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.AuthRepository
{
    public class AuthenticationRepository: IAuthenticationRepository
    {
        private readonly UserManager<AuthUser> userManager;
        private readonly RoleManager<AspNetRole> roleManager;
        private readonly GlobalContext auth=null!;
        private readonly FindUserId obj_FindUserId;
        private readonly IConfiguration _configuration;
        private IEMailService _EMailService;
        private readonly IConfigurationSection _goolgeSettings;
        private readonly FacebookAuthSetting _facebookAuthSetting;
        private readonly IHttpClientFactory _httpClientfactory;
        UserRepository userRepository;
        private SignInManager<AuthUser> signInManager;
        private const string TokenvalidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string UserInfo = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";
        public AuthenticationRepository(GlobalContext auth,
            IHttpClientFactory httpClientfactory, UserManager<AuthUser> userManager, 
            RoleManager<AspNetRole> roleManager, IConfiguration configuration, 
            IEMailService EMailService, FacebookAuthSetting facebookAuthSetting, 
            FindUserId obj_FindUserId, UserRepository userRepository, SignInManager<AuthUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._configuration = configuration;
            this._EMailService = EMailService;
            this._goolgeSettings = _configuration.GetSection("GoogleAuthSettings");
            this._facebookAuthSetting = facebookAuthSetting;
            this._httpClientfactory = httpClientfactory;
            this.auth = auth;
            this.obj_FindUserId = obj_FindUserId;
            this.userRepository=userRepository;
            this.signInManager = signInManager;

        }
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterModel model)
        {
            var userExist = auth.Users.FirstOrDefaultAsync(x => x.UserName == model.Email || x.UserName == model.Phonenumber);
            if (userExist.Result != null)
            {
                return new UserManagerResponse
                {
                    Message = "User Already Exist",
                    IsSuccess = false,
                };
            }
            AuthUser user = new AuthUser()
            {
                UserName = model.Phonenumber==null? model.Email: model.Phonenumber,
                FirstName = model.Firstname,
                LastName = model.Lastname,
                PhoneNumber = model.Phonenumber,
                Role_Id_FK = model.RoleId,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                IsEnabled = true,
                Imagename = "user-1633249__340 (1).png",
            };
            var result = await userManager.CreateAsync(user, model.Password);
            string userid = user.Id;
            if (result.Succeeded)
            {
                //var confrmEmailtoken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                //var encodedEmailToken = Encoding.UTF8.GetBytes(confrmEmailtoken);
                //var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                //string url = $"{_configuration["AppUrl"]}/api/Authentication/ConfirmEmail?userId={user.Id}&token={validEmailToken}";
                //await _EMailService.SendEmailAsync(user.UserName, user.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
                //    $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
                //var profile = await this.userRepository.InsertUserProfile(user.Email, model.Firstname, model.Lastname, user.PhoneNumber);
                //await this.officesRepository.AddOfficeRoles(userid, model.OfficeId);
                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        public async Task<UserManagerResponse> ExtRegisterUserAsync(string Firstname,string Lastname,string Phonenumber,string Email,string Password,string Role_Id)
        {
            try 
            {
                var userExist = auth.Users.FirstOrDefaultAsync(x => x.UserName==Email || x.UserName == Phonenumber);
                if (userExist.Result != null)
                {
                    return new UserManagerResponse
                    {
                        Message = "User Already Exist",
                        IsSuccess = false,
                    };
                }
                AuthUser user = new AuthUser()
                {
                    UserName = Phonenumber == null ? Email : Phonenumber,
                    FirstName = Firstname,
                    LastName = Lastname,
                    PhoneNumber = Phonenumber,
                    Imagename = "user-1633249__340 (1).png",
                    Role_Id_FK = Role_Id,
                    Email = Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    IsEnabled = true,
                    Inactive="N"
                };
                var result = await userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {
                    //var profile = await this.userRepository.InsertUserProfile(Email, Firstname, Lastname, Phonenumber);
                    return new UserManagerResponse
                    {
                        Message = "User created successfully!",
                        IsSuccess = true,
                    };
                }

                return new UserManagerResponse
                {
                    Message = "User did not create",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "User not found"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Email did not confirm",
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        public async Task<UserManagerResponse> ForgetPasswordAsync(string Username)
        {
            var user = await userManager.FindByEmailAsync(Username);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with email",
                };

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["AppUrl"]}/ResetPassword?username={Username}&token={validToken}";

            await _EMailService.SendEmailAsync(user.UserName, user.Email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password <a href='{url}'>Click here</a></p>");

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Reset password URL has been sent to the email successfully!"
            };
        }
        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = userManager.Users.FirstOrDefault(x => x.Email == model.Username || x.PhoneNumber == model.Username);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with"+model.Username,
                };

            if (model.NewPassword != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Password doesn't match its confirmation",
                };
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            //var _dec = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //var decodedToken = WebEncoders.Base64UrlDecode(token);
            //string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                Message = "Something went wrong",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string tooken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _goolgeSettings.GetSection("clientId").Value }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(tooken, settings);
                return payload;
            }
            catch (Exception ex)
            {
                //log an exception
                throw new Exception(ex.Message);
            }
        }
        public async Task<FacebookTookenvalidationResult> VerifyFacebookToken(string accesstoken)
        {
            var formattedUrl = string.Format(TokenvalidationUrl, accesstoken, _facebookAuthSetting.AppId, _facebookAuthSetting.AppSecret);
            var result = await _httpClientfactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsstring = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookTookenvalidationResult>(responseAsstring);
        }
        public async Task<FacebookUserInfoResult> GetUserInfoAsync(string accesstoken)
        {
            var formattedUrl = string.Format(UserInfo, accesstoken);
            var result = await _httpClientfactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsstring = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsstring);
        }
        public async Task<UserManagerResponse> ForGoogle(string Token)
        {
            var payload = await VerifyGoogleToken(Token);
            if (payload == null)
                return new UserManagerResponse
                {
                    Message = "Invalid External Authentication.",
                    IsSuccess = false,
                };

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await userManager.Users.FirstOrDefaultAsync(x=>x.UserName == payload.Email || x.Email == payload.Email);
                if (user == null)
                {
                    user = new AuthUser { Email = payload.Email, UserName = payload.Email };
                    await userManager.CreateAsync(user);
                    await userManager.AddLoginAsync(user, info);
                }
                else

                    await userManager.AddLoginAsync(user, info);
            }
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "Invalid External Authentication.",
                    IsSuccess = false,
                };
            var token = await userManager.CreateSecurityTokenAsync(user);
            //TokenHandler._configuration = _configuration;
            return new UserManagerResponse
            {
                Message = "Google Login successfully!",
                IsSuccess = true,
                ExpireDate = DateTime.Now.AddHours(5),
                token = await CreateAccessToken(payload.Email)
            };
        }
        public async Task<UserManagerResponse> ForFacebook(string accesstoken)
        {
            var validateresult = await VerifyFacebookToken(accesstoken);
            var userInfo = await GetUserInfoAsync(accesstoken);
            var info = new UserLoginInfo("FACEBOOK", _facebookAuthSetting.AppId, "FACEBOOK");
            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await userManager.Users.FirstOrDefaultAsync(x=>x.UserName == userInfo.email || x.Email == userInfo.email);
                if (user == null)
                {
                    user = new AuthUser { Email = userInfo.email, UserName = userInfo.email };
                    var createdResult = await userManager.CreateAsync(user);
                    await userManager.AddLoginAsync(user, info);
                    if (!createdResult.Succeeded)
                    {
                        return new UserManagerResponse
                        {
                            Message = "Something went wrong",
                            IsSuccess = false,
                        };

                    }
                }
                else
                    await userManager.AddLoginAsync(user, info);
            }
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "Invalid External Authentication.",
                    IsSuccess = false,
                };
            var token = await userManager.CreateSecurityTokenAsync(user);
            //TokenHandler._configuration = _configuration;
            return new UserManagerResponse
            {
                Message = "Facebook Login successfully!",
                IsSuccess = true,
                ExpireDate = DateTime.Now.AddHours(5),
                token = await CreateAccessToken(userInfo.email)
            };

        }
        public async Task<UserManagerResponse> ChangePasswordAsync(ChangePassword model)
        {
            if (model != null)
            {
                var user = await userManager.FindByNameAsync(model.Username);
                
                if (user == null)
                {
                    return new UserManagerResponse
                    {
                        IsSuccess = false,
                        Message = "No user find with" + model.Username,
                    };
                }
                // ChangePasswordAsync changes the user password
                var result = await userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    return new UserManagerResponse
                    {
                        IsSuccess = false,
                        Message = "This " + model.NewPassword + "password is not valid",
                    };
                }

                // Upon successfully changing the password refresh sign-in cookie
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Your ChangePasswordConfirmation successfuly",
                };

            }
            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Your sending data are not valid",
            };
        }
        public async Task<string> CreateAccessToken(string username)
        {
            var user = await userManager.FindByEmailAsync(username);
            var userRoles = await userManager.GetRolesAsync(user);
            var authClims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                };
            foreach (var userrole in userRoles)
            {
                authClims.Add(new Claim(ClaimTypes.Role, userrole));
            }
            var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            DateTime now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:iss"],
                audience: _configuration["JWT:aud"],
                claims: authClims,
                notBefore: now,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256));
            var validtoken = new JwtSecurityTokenHandler().WriteToken(token);
            return validtoken;
        }
        public async Task<bool> UpdateUserAsync(RegisterBindingModel model,string userName)
        {

            string roleName = await obj_FindUserId.FindRoleNameFromUserName(userName);

            if (roleName != "")
            {
                AuthUser user = new AuthUser();
                UserStore<AuthUser> store = new UserStore<AuthUser>(auth);
                user = await userManager.FindByNameAsync(model.UserName);
                String hashedNewPassword = userManager.PasswordHasher.HashPassword(user,model.Password);
                AuthUser cUser = await store.FindByIdAsync(user.Id);
                await store.SetPasswordHashAsync(cUser, hashedNewPassword);
                await store.UpdateAsync(cUser);
                return true;
            }
            else
                return false;
        }
        public async Task<bool> DeleteUserAsync(string userId)
        {

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return true;
                }

                return false;
            }
        }
        public async Task<string> ActivateInactivate(string userid)
        {
            var result = userManager.Users.FirstOrDefault(x=>x.Id==userid);
            AuthUser user = new AuthUser();
            UserStore<AuthUser> store = new UserStore<AuthUser>(auth);
            if (result.IsEnabled==true)
            {
                result.IsEnabled = false;
                await store.UpdateAsync(result);
                return "Inactive succefuly";
            }
            else
                result.IsEnabled = true;
                await store.UpdateAsync(result);
                return "Active succefuly";
        }

        public bool Userverification(string data)
        {
            var result = userManager.Users.FirstOrDefault(x => x.PhoneNumber == data || x.Email== data);
            if (result != null)
            {
                return true;
            }
            else
            return false;
        }
        

    }
}
