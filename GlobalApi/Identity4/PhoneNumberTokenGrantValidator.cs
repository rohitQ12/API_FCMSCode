using System.Linq;
using System.Threading.Tasks;
using GlobalApi.Models.Authentication;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;

namespace GlobalApi
{
    public class PhoneNumberTokenGrantValidator : IExtensionGrantValidator
    {
        private readonly PhoneNumberTokenProvider<AuthUser> _phoneNumberTokenProvider;
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IEventService _events;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public PhoneNumberTokenGrantValidator(
            PhoneNumberTokenProvider<AuthUser> phoneNumberTokenProvider,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            IEventService events
            //ILogger<PhoneNumberTokenGrantValidator> logger
            )
        {
            _phoneNumberTokenProvider = phoneNumberTokenProvider;
            _userManager = userManager;
            _signInManager = signInManager;
            _events = events;
            
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

            var test = from d in _userManager.Users select d;

            var user = await _userManager.Users.SingleOrDefaultAsync(x =>
                x.PhoneNumber == _userManager.NormalizeName(username) ||
                x.Email == username);

            if (user == null)
            {
                logger.Info("Authentication failed for user: {username}, reason: invalid username",
                   username);
                await _events.RaiseAsync(new UserLoginFailureEvent(username,
                    "invalid PhoneNumber or Email", false));
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
                    "User PhoneNumber or Email does not exits");
                return;
            }

            var testing = _userManager.CheckPasswordAsync(user, password);
            if (!testing.Result)
            {
                logger.Info("Authentication failed for user: {password}, reason: invalid username",
                  password);
                await _events.RaiseAsync(new UserLoginFailureEvent(password,
                    "invalid PhoneNumber or Email", false));
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
                     "invalid password");
                return;
            }

            logger.Info("Credentials validated for username: {username}", username);
            await _events.RaiseAsync(new UserLoginSuccessEvent(username, user.Id, username, false));
            await _signInManager.SignInAsync(user, true);
            context.Result = new GrantValidationResult(user.Id, OidcConstants.AuthenticationMethods.Password);
        }

        public string GrantType => "phone_number_token";
    }
}