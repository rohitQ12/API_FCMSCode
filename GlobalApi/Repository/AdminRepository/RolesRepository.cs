using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Data;
using System.Linq;
using GlobalApi.Models.Authentication;

namespace GlobalApi.Repository.AdminRepository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly GlobalContext _context;
        private readonly RoleManager<AspNetRole> roleManager;
        public RolesRepository(RoleManager<AspNetRole> roleManager,GlobalContext context)
        {
            this.roleManager = roleManager;
            this._context = context;
        }
        public async Task<bool> ActivateInactivate(string id)
        {
                var result = await _context.Roles.FirstOrDefaultAsync(d => d.Id == id);
                if (result.Inactive == "N" || result.Inactive == null)
                {
                    if(result!=null)
                    {
                        result.Inactive = "Y";
                        await _context.SaveChangesAsync();
                        return false;
                    }
                    return false;
                }
                else {
                    if (result != null)
                    {
                        result.Inactive = "N";
                        await _context.SaveChangesAsync();
                        
                    }
                    return true;
                }
        }
        public async Task<Boolean> CheckRoles(string roleId)
        {
            var result= await _context.Roles.FirstOrDefaultAsync(d => d.Id == roleId);
            if (result.Inactive != "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CreateRoles(RolesModels role)
        {
            IdentityResult result = null;
            AspNetRole obj = new AspNetRole()
            {
                RoleId= roleManager.Roles.Max(u => u.RoleId) + 1,
                Name =role.RoleName,
                Inactive="N",
            };
            if (!await roleManager.RoleExistsAsync(role.RoleName))
            {
                result = await roleManager.CreateAsync(obj);
                return true;
            }
            else
                return false;
        }

        public async Task<List<AspNetRole>> GetAllRoles()
        {
            try
            {
                var result = (from d in _context.Roles where d.Inactive=="N"
                              orderby d.RoleId descending 
                              select d).ToListAsync();
                return await result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<AspNetRole>> GetAllRoles_DD()
        {
            try
            {
                var result = (from d in _context.Roles
                              where d.Inactive == "N" && (d.RoleId <= 4 || d.RoleId == 10)
                              orderby d.RoleId descending
                              select d).ToListAsync();
                return await result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<RolesModels>> GetRoleId(string id)
        {
            var result = (from d in _context.Roles where d.Id == id 
                          select new RolesModels { RoleName=d.Name,RoleId=d.Id}).ToListAsync();
            return await result;
        }

        public async Task<List<RolesModels>> GetRolesforSelectedOffice()
        {
            var result = (from d in _context.Roles where d.Inactive != "Y" || d.Inactive == null 
                          select new RolesModels { RoleName = d.Name, RoleId = d.Id }).ToListAsync();
            return await result;
        }

        public async Task<bool> UpdateOfficeRole(string rolename, string Id)
        {
            IdentityResult result = null;
            bool isSameRole = false;
            bool isRoleAlreadyExit = await roleManager.RoleExistsAsync(rolename);
            if (isRoleAlreadyExit)
            {
                AspNetRole roleDetails = await roleManager.FindByIdAsync(Id); 
                if (roleDetails.Name == rolename)
                {
                    result = new IdentityResult();
                    isSameRole = true;
                }
                return false;
            }
            else
            {
                var role = new AspNetRole();
                role.Name = rolename;
                role.Id = Id;
                result = await roleManager.UpdateAsync(role);
                return true;
            }
        }
    }
}
