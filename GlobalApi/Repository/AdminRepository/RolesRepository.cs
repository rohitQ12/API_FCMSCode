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
            this._context =new GlobalContext();
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
        public async Task<bool> CheckRoles(string roleId)
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
                var result = (from d in _context.Roles
                              orderby d.RoleId descending 
                              select d).ToListAsync();
                return await result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<AspNetRole>> GetAllRoles_DD(string? roleaction,string? rolename)
        {
            try
            {
                var result = (from d in _context.Roles
                              where d.Inactive == "N" 
                              && roleaction == "All" ? d.Name != null : d.Rolecategory == roleaction
                              orderby d.Name
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
                          select new RolesModels { RoleName=d.Name,RoleId=d.Id ,Inactive=d.Inactive }).ToListAsync();
            return await result;
        }

        public async Task<List<RolesModels>> GetRolesforSelectedOffice()
        {
            var result = (from d in _context.Roles where d.Inactive != "Y" || d.Inactive == null 
                          select new RolesModels { RoleName = d.Name, RoleId = d.Id }).ToListAsync();
            return await result;
        }

        //public async Task<bool> UpdateOfficeRole(RolesModels role)
        //{
        //    IdentityResult result = null;
        //    bool isSameRole = false;
        //    bool isRoleAlreadyExit = await roleManager.RoleExistsAsync(role.RoleName);
        //    if (isRoleAlreadyExit)
        //    {
        //        AspNetRole roleDetails = await roleManager.FindByIdAsync(role.RoleId);
        //        if (roleDetails.Name == role.RoleName)
        //        {
        //            result = new IdentityResult();
        //            isSameRole = true;
        //        }
        //        return false;
        //    }
        //    else
        //    {
        //        //var role_obj = new AspNetRole();
        //        var role_obj = await roleManager.FindByIdAsync(role.RoleId);
        //        role_obj.Name = role.RoleName;
        //        role_obj.Id = role.RoleId;
        //        result = await roleManager.UpdateAsync(role_obj);
        //        return true;
        //    }
        //}

        //public async Task<bool> UpdateOfficeRole(RolesModels role)
        //{
        //    if (role == null || string.IsNullOrEmpty(role.RoleId) || string.IsNullOrEmpty(role.RoleName))
        //        return false;

        //    // Step 1: Find the role by ID
        //    var role_obj = await roleManager.FindByIdAsync(role.RoleId);
        //    if (role_obj == null)
        //        return false; // No role found with that ID

        //    // Step 2: Check if another role with the same name already exists
        //    var existingRole = await roleManager.FindByNameAsync(role.RoleName);
        //    if (existingRole != null && existingRole.Id != role.RoleId)
        //    {
        //        // Another role already has this name
        //        return false;
        //    }

        //    // Step 3: Update the role
        //    role_obj.Name = role.RoleName;
        //    role_obj.NormalizedName = role.RoleName.ToUpper(); // important for Identity

        //    var result = await roleManager.UpdateAsync(role_obj);

        //    // Step 4: Return status
        //    return result.Succeeded;
        //}

        public async Task<string> UpdateOfficeRole(RolesModels role)
        {
            if (role == null || string.IsNullOrEmpty(role.RoleId))
                return "Invalid role data";

            // Step 1: Find role by ID
            var role_obj = await roleManager.FindByIdAsync(role.RoleId);
            if (role_obj == null)
                return "Role not found";

            // Step 2: Check if another role (different ID) already has the same name
            var existingRole = await roleManager.FindByNameAsync(role.RoleName);
            if (existingRole != null && existingRole.Id != role.RoleId)
                return "Role name already exists";

            // Step 3: Update the role properties
            role_obj.Name = role.RoleName;
            role_obj.NormalizedName = role.RoleName.ToUpper();

            // ✅ Add custom field update (if exists)
            if (role_obj is AspNetRole customRole)
                customRole.Inactive = role.Inactive;

            // Step 4: Save the changes
            var result = await roleManager.UpdateAsync(role_obj);

            if (result.Succeeded)
                return "Role updated successfully";
            else
                return "Error while updating role";
        }


    }
}
