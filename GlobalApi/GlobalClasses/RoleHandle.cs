using GlobalApi.Data;
using GlobalApi.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GlobalApi.GlobalClasses
{
    public class RoleHandle
    {
        private readonly RoleManager<AspNetRole> roleManager;
        private readonly UserManager<AuthUser> userManager;


        public RoleHandle(RoleManager<AspNetRole> roleManager, UserManager<AuthUser> userManager)
        {
            this.roleManager= roleManager;
            this.userManager= userManager;
        }
        public async Task<string> FindRoleIdFromRoleName(string roleName)
        {
            string roleId = "";
            IdentityRole role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return roleId;
            }
            roleId = role.Id;
            return roleId;
        }
        public IEnumerable<IdentityRole> GetAllRoleNames()
        {
            IEnumerable<IdentityRole> rolesList = roleManager.Roles;
            return rolesList;
        }
        public List<AuthUser> GetAllUsersBelongingToTheRole(string roleId)
        {
            List<AuthUser> usersBelongingToTheRole = userManager.Users.Where(x => x.Role_Id_FK.Contains(roleId)).ToList();
            return usersBelongingToTheRole;
        }
        public async Task<bool> DeleteRole(string roleName)
        {
            AspNetRole role = await roleManager.FindByNameAsync(roleName);
            IdentityResult result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return true;
            else
                return false;

        }
        public async Task<bool> CheckRoleExist(string roleId)
        {
            IdentityRole role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
                return false;
            else
                return true;
        }
    }
}
