using GlobalApi.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GlobalApi.GlobalClasses
{
    public class ClaimsAuthorization
    {
        private readonly UserManager<AuthUser> userManager;
        public ClaimsAuthorization(UserManager<AuthUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<Claim>> GetClaimsListForUserAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            IEnumerable<Claim> claims = await userManager.GetClaimsAsync(user);
            return claims;
        }
    }
}
