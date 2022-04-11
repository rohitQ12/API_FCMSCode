using GlobalApi.Data;
using GlobalApi.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GlobalApi.GlobalClasses
{
    public class FindUserId
    {
        private readonly UserManager<AuthUser> userManager;
        private readonly RoleManager<AspNetRole> roleManager;
        GlobalContext authDb;
        public FindUserId(UserManager<AuthUser> userManager, RoleManager<AspNetRole> roleManager, GlobalContext authDb)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.authDb = authDb;
        }

        public async Task<string> FindRole_Id_FKFromUserName(string userName)
        {
            AuthUser userDetails = await userManager.FindByNameAsync(userName);
            return userDetails.Role_Id_FK;
        }

        public async Task<string> FindRoleNameFromUserName(string userName)
        {
            AuthUser userDetails = await userManager.FindByNameAsync(userName);
            return await FindRoleNameFromRole_Id_FK(userDetails.Role_Id_FK);
        }

        public async Task<string> FindRoleNameFromRole_Id_FK(string roleId)
        {
            IdentityRole role = await roleManager.FindByIdAsync(roleId);
            string roleName = role.Name.ToString();
            return roleName;
        }
        public async Task<string> FindUserIdFromUserName(string userName)
        {
            AuthUser userDetails = await userManager.FindByNameAsync(userName);
            return userDetails.Id;
        }
        public async Task<string> FindUserIdFromUserNames(string userName)
        {
            AuthUser userDetails = await userManager.FindByNameAsync(userName);

            return userDetails.Id;
        }
        public async Task<string> FindIs_TestUserFromUserName(string userName)
        {
            AuthUser userDetails = await userManager.FindByNameAsync(userName);
            return userDetails.Id;
        }
        public async Task<string> FindIdFromUserName(string userName)
        {
            AuthUser userDetails = await userManager.FindByNameAsync(userName);
            return userDetails.Id;
        }
        public async Task<List<AuthUser_Details>> FindUser()
        {
            List<AuthUser> userDetails = await authDb.Users.OrderByDescending(d => d.UserId).ToListAsync();

            var result = (from d in authDb.Users
                          join e in authDb.Roles on d.Role_Id_FK equals e.Id
                          orderby d.UserId descending
                          select new AuthUser_Details {
                              Id= d.Id,
                              UserId= d.UserId,
                              RoleIdFk= d.Role_Id_FK,
                              Rolename= e.Name,
                              Inactive= d.Inactive,
                              FirstName= d.FirstName,
                              LastName= d.LastName,
                              imagename= d.imagename,
                              IsEnabled= d.IsEnabled,
                              UserName= d.UserName,
                              Email= d.Email,
                              PhoneNumber= d.PhoneNumber
                          }).ToListAsync();

            
            return await result;
        }
        public async Task<AuthUser> FindUser(string username)
        {
            return await authDb.Users.FirstOrDefaultAsync(x=>x.UserName == username);
        }

        public async Task<bool> CheckRoles(string roleId)
        {
            //var result = await gbcontext.AspNetRoles.FirstOrDefaultAsync(d => d.Id == roleId);
            await authDb.Users.OrderByDescending(d=>d.Id).ToListAsync();
            var result = await authDb.Roles.FirstOrDefaultAsync(d => d.Id == roleId);
            if (result.Inactive != "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
            return true;
        }

    }
}
