using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.Models.Authentication;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using System.Security.Cryptography;
using static IdentityServer4.Events.TokenIssuedSuccessEvent;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi
{
    public class PhoneNumberTokenGrantValidator : IExtensionGrantValidator
    {
        private readonly PhoneNumberTokenProvider<AuthUser> _phoneNumberTokenProvider;
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IEventService _events;
        //private FindUserId obj_FindUserId = new FindUserId();
        private readonly GlobalContext db;

        public PhoneNumberTokenGrantValidator(
            PhoneNumberTokenProvider<AuthUser> phoneNumberTokenProvider,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            IEventService events

         )
        {
            _phoneNumberTokenProvider = phoneNumberTokenProvider;
            _userManager = userManager;
            _signInManager = signInManager;
            _events = events;
            db = new GlobalContext();

        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var createUser = false;
            var raw = context.Request.Raw;
            var credential = raw.Get(OidcConstants.TokenRequest.GrantType);

            if (credential == null || credential != "phone_number_token")
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
                    "invalid verify_username credential");
                return;
            }

            var username = raw.Get("username");

            var password = raw.Get("password");
            var Rolename = raw.Get("role");
            if (password == null || password == "" || Rolename == null || Rolename == "")
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
                    "invalid verify_password/rolename credential");
                return;
            }
           // var Roles = await db.Roles.Where(x => x.Name == Rolename).Select(x => x.Id).FirstOrDefaultAsync();
           
            
          //  var roleid = await db.RoleClaims.Where(x => x.RC_RoleId_FK == Roles).Select(x => x.RC_RoleId_FK).FirstOrDefaultAsync();


            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == _userManager.NormalizeName(username) || x.Email == username);

            if (user == null)
            {
                //logger.Info("Authentication failed for user: {username}, reason: Invalid username",username);
                await _events.RaiseAsync(new UserLoginFailureEvent(username,
                    "invalid PhoneNumber or Email", false));
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient,
                    "User is not registered.");
                return;
            }
            //deactivated user
            if (user.Inactive == "Y")
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient,
                    "User is Deactivated.");
                return;
            }

            //var user =await _userManager.Users.Include(x => x.UserName).FirstOrDefaultAsync(x => x.UserName == _userManager.NormalizeName(username));

            var rolepermission = await db.RoleClaims.Where(x => x.RC_RoleId_FK == user.Role_Id_FK).Select(x => x.RC_RoleId_FK).CountAsync();

            var Role = await db.Roles.FirstOrDefaultAsync(x => x.Id == user.Role_Id_FK);

            //if (Role.Id == Roles)
            //{
            var Password = await _userManager.CheckPasswordAsync(user, password);

            if (Password == false)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient,
                    "Invalid Password.");
                return;
            }


            if (rolepermission == 0)
            {
                //logger.Info("Authentication failed for user: {username}, reason: invalid username",username);
                await _events.RaiseAsync(new UserLoginFailureEvent(username,
                    "invalid Claims", false));
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient,
                    "Authorization denied");
                return;
            }

            if (Rolename == "Doctor (Online)" || Rolename == "Patient")
            //if (Rolename == "Patient")
            {
                if (Rolename != Role.Name)
                {
                    //logger.Info("Authentication failed for user: {username}, reason: Invalid Username",username);
                    await _events.RaiseAsync(new UserLoginFailureEvent(username,
                        "invalid Role", false));
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient,
                        "User is not registered.");
                    return;
                }
                else if (!Password)
                {
                    // logger.Info("Authentication failed for user: {password}, reason: Invalid username", password);
                    await _events.RaiseAsync(new UserLoginFailureEvent(password,
                        "Invalid PhoneNumber or Email", false));
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
                         "Invalid password");
                    return;
                }


            }
            else if (Rolename == "Admin" || Rolename == "Super Admin")
            //else
            {
                if (Role.Name == "Doctor (Online)" || Role.Name == "Patient")
                //else if (Role.Name == "Patient")
                {
                    //logger.Info("Authentication failed for user: {username}, reason: Invalid Username",username);
                    await _events.RaiseAsync(new UserLoginFailureEvent(username,
                        "invalid Role", false));
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient,
                        "User is not registered.");
                    return;
                }
                else if (!Password)
                {
                    //logger.Info("Authentication failed for user: {password}, reason: Invalid username", password);
                    await _events.RaiseAsync(new UserLoginFailureEvent(password,
                        "Invalid PhoneNumber or Email", false));
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
                         "Invalid password");
                    return;
                }
            }
            await _events.RaiseAsync(new UserLoginSuccessEvent(username, user.Id, username, false));
            await _signInManager.SignInAsync(user, true);

            Dictionary<string, object> customData = new Dictionary<string, object>
            {
                {"Id", user.Id},
                {"firstName", user.FirstName},
                {"lastName", user.LastName},
                {"userName", user.UserName},
                {"roleId", user.Role_Id_FK},
                {"roleName", Role.Name},
                {"emailId", user.Email},
                {"emailConfirmed", user.EmailConfirmed},
                {"phoneNumber", user.PhoneNumber},
                {"phoneNumberConfirmed", user.PhoneNumberConfirmed},
                {"userId", user.UserId}
            };

            context.Result = new GrantValidationResult(
            subject: user.Id,
            authenticationMethod: OidcConstants.AuthenticationMethods.Password,
            customResponse: customData
            );

            // }
            //else
            //{
            //    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
            //        "invalid verify_password/rolename credential");
            //    return;
            //}
        }
        public string GrantType => "phone_number_token";
    }
}