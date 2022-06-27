// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using GlobalApi.Models.Authentication;

namespace GlobalApi
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<AuthUser> _claimsFactory;

        public ProfileService(UserManager<AuthUser> userManager, IUserClaimsPrincipalFactory<AuthUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }


        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);


            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            //if (user.JobTitle != null)
            //    claims.Add(new Claim("",""));

            if (user.FirstName != null) 
                claims.Add(new Claim(PropertyConstants.FullName, user.FirstName));

            //if (user.Configuration != null)
            //    claims.Add(new Claim(PropertyConstants.Configuration, user.Configuration));

            context.IssuedClaims = claims;
        }


        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);

            context.IsActive = (user != null) && user.IsEnabled;
        }
    }
}