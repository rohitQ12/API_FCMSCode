using GlobalApi.Data;
using GlobalApi.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace GlobalApi.GlobalClasses
{
    public class ClaimsAuthorization
    {
        //private readonly UserManager<AuthUser> userManager;
        //public ClaimsAuthorization(UserManager<AuthUser> userManager)
        //{
        //    this.userManager = userManager;
        //}
        private readonly GlobalContext db;
        public ClaimsAuthorization()
        {
            db = new GlobalContext();
        }

        //public async Task<IEnumerable<Claim>> GetClaimsListForUserAsync(string userName)
        //{
        //    var user = await userManager.FindByNameAsync(userName);
        //    IEnumerable<Claim> claims = await userManager.GetClaimsAsync(user);
        //    return claims;
        //}
        public async Task<List<IdentityUserClaim<string>>> GetClaimsListForUserAsync(string userName)
        {
            var user = db.Users.SingleOrDefault(x=>x.UserName==userName);
            var claims = await (from d in db.UserClaims select d).ToListAsync();
            //IEnumerable<Claim> claims = await userManager.GetClaimsAsync(user);
            return claims;
        }
    }
}
