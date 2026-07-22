using GlobalApi.Data;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.AdminClaims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using GlobalApi.Models.Master;

namespace GlobalApi.GlobalClasses
{
    public class ClaimsHandle
    {
        private readonly UserManager<AuthUser> userManager;
        private IEnumerable<Claim> AlreadyExistingClaimsForUser = null;
        RoleManager<AspNetRole> roleManager;
        GlobalContext globalcontext;
        RoleHandle roleHandle;
        public IPrimarykeyvalue primarykeyvalue;
        public ClaimsHandle(UserManager<AuthUser> userManager, RoleManager<AspNetRole> roleManager, GlobalContext globalcontext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.globalcontext = globalcontext;
            primarykeyvalue = new Primarykeyvalue();
            this.roleHandle = new RoleHandle(roleManager, userManager);
        }
        public async Task<bool> Create_RoleClaim(string roleId, List<Menus_List> ListMenus)
        {
            try
            {
                //Note: Remember we are saving only Claims whose Value is true...
                List<Menus_List> AlreadyExistsClaimsListOfTheRole = await GetAllClaimsAllocatedToRole(roleId);
                using (GlobalContext db = new GlobalContext())
                {
                    foreach (var menu in ListMenus)
                    {
                        foreach (var submenu in menu.subItems)
                        {
                            foreach (var claim in submenu.SubMenuClaim)
                            {
                                if (!AlreadyExistsClaimsListOfTheRole.Any(c => (c.subItems.Any(d => d.SM_label == submenu.SM_label) && (c.subItems.Any(d => d.SubMenuClaim.Any(e => e.ClaimTypeId == claim.ClaimTypeId && e.ClaimValue == true))))))
                                {
                                    if (AlreadyExistsClaimsListOfTheRole.Any(c => (c.subItems.Any(d => d.SM_label == submenu.SM_label)) && (c.subItems.Any(d => d.SubMenuClaim.Any(e => e.ClaimTypeId == claim.ClaimTypeId)))))
                                    {
                                        var delete = await db.RoleClaims.FirstOrDefaultAsync(x => x.RC_RoleId_FK == roleId && x.RC_SMD_Id_FK == claim.ClaimTypeId);
                                        if (delete != null)
                                        {
                                            var data = db.RoleClaims.Remove(delete);
                                            await db.SaveChangesAsync();
                                        }
                                    }
                                    if (claim.ClaimValue == true && claim.IsClaimShown == true)
                                    {
                                        int id = await primarykeyvalue.primary_key("RoleClaims");
                                        RoleClaims obj = new RoleClaims()
                                        {
                                            RC_Id = id,
                                            RC_RoleId_FK = roleId,
                                            RC_M_Id_FK = menu.M_Id,
                                            RC_SM_Id_FK = submenu.SM_Id,
                                            RC_SMD_Id_FK = claim.ClaimTypeId,
                                            PageFunctionName = claim.ClaimType,
                                            RC_Value = "Y",
                                            RC_UserId_FK = 1,
                                            RC_INSTS = System.DateTime.Now,
                                            Delete_flag = false,
                                            Modified_by = 0,
                                            Modified_date = DateTime.Now,
                                            Created_by = 0,
                                            Created_date = DateTime.Now,
                                            Status = 1,
                                            Deleted_by = 1,
                                            Deleted_date = DateTime.Now,
                                        };
                                        var result = await db.RoleClaims.AddAsync(obj);
                                        await db.SaveChangesAsync();

                                    }
                                }
                                else
                                {
                                    if (claim.ClaimValue == false)
                                    {
                                        var delete = await db.RoleClaims.FirstOrDefaultAsync(x => x.RC_RoleId_FK == roleId && x.RC_SMD_Id_FK == claim.ClaimTypeId);
                                        if (delete != null)
                                        {
                                            var data = db.RoleClaims.Remove(delete);
                                            await db.SaveChangesAsync();
                                        }
                                    }
                                }
                            }
                            //For sub pages
                            foreach (var submenusfunction in submenu.subItemsList)
                            {
                                foreach (var subMenuclaim in submenusfunction.SubMenuFunctionClaim)
                                {
                                    if (subMenuclaim.ClaimValue == true)
                                    {
                                        try
                                        {

                                            var result = await db.SubRoleClaims.FirstOrDefaultAsync(x => x.SRC_RoleId_FK == roleId && x.SRC_SMFD_Id_FK == subMenuclaim.ClaimTypeId);
                                            if (result != null)
                                            {
                                                var data = db.SubRoleClaims.Remove(result);
                                                await db.SaveChangesAsync();
                                            }
                                            int id = await primarykeyvalue.primary_key("SubRoleClaims");
                                            SubRoleClaims sobj = new SubRoleClaims()
                                            {
                                                SRC_Id = id,
                                                SRC_RoleId_FK = roleId,
                                                SRC_SMF_Id_FK = submenusfunction.SMF_Id,
                                                SRC_SMFD_Id_FK = subMenuclaim.ClaimTypeId,
                                                SRC_Value = "Y",
                                                SRC_UserId_FK = 1,
                                                SRC_INSTS = System.DateTime.Now,
                                                Delete_flag = false,
                                                Modified_by = 0,
                                                Modified_date = DateTime.Now,
                                                Created_by = 0,
                                                Created_date = DateTime.Now,
                                                Status = 1,
                                                Deleted_by = 1,
                                                Deleted_date = DateTime.Now,
                                            };
                                            await db.SubRoleClaims.AddAsync(sobj);
                                            await db.SaveChangesAsync();
                                            if (subMenuclaim.ClaimValue == false)
                                            {
                                                var delete = await db.SubRoleClaims.FirstOrDefaultAsync(x => x.SRC_RoleId_FK == roleId && x.SRC_SMFD_Id_FK == submenusfunction.SMF_Id);
                                                if (delete != null)
                                                {
                                                    var data = db.SubRoleClaims.Remove(delete);
                                                    await db.SaveChangesAsync();
                                                }
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            throw new Exception("error");
                                        }
                                    }
                                }

                            }
                        }

                        if (menu.subItems.Count == 0)
                        {
                            if (!AlreadyExistsClaimsListOfTheRole.Any(c => c.M_label == menu.M_label))
                            {
                                if (AlreadyExistsClaimsListOfTheRole.Any(c => c.M_label == menu.M_label))
                                {
                                    var delete = await db.RoleClaims.FirstOrDefaultAsync(x => x.RC_RoleId_FK == roleId && x.RC_M_Id_FK == menu.M_Id);
                                    if (delete != null)
                                    {
                                        var data = db.RoleClaims.Remove(delete);
                                        await db.SaveChangesAsync();
                                    }
                                }
                                int id = await primarykeyvalue.primary_key("RoleClaims");
                                RoleClaims obj = new RoleClaims()
                                {
                                    RC_Id = id,
                                    RC_RoleId_FK = roleId,
                                    RC_M_Id_FK = menu.M_Id,
                                    RC_SM_Id_FK = 0,
                                    RC_SMD_Id_FK = 0,
                                    PageFunctionName = menu.M_label,
                                    RC_Value = "Y",
                                    RC_UserId_FK = 1,
                                    RC_INSTS = System.DateTime.Now,
                                    Delete_flag = false,
                                    Modified_by = 0,
                                    Modified_date = DateTime.Now,
                                    Created_by = 0,
                                    Created_date = DateTime.Now,
                                    Status = 1,
                                    Deleted_by = 1,
                                    Deleted_date = DateTime.Now,
                                };
                                var result = await db.RoleClaims.AddAsync(obj);
                                await db.SaveChangesAsync();
                            }
                            else
                            {
                                if (menu.ClaimValue == false)
                                {
                                    var delete = await db.RoleClaims.FirstOrDefaultAsync(x => x.RC_RoleId_FK == roleId && x.RC_M_Id_FK == menu.M_Id);
                                    if (delete != null)
                                    {
                                        var data = db.RoleClaims.Remove(delete);
                                        await db.SaveChangesAsync();
                                    }

                                }

                            }
                        }
                    }


                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> CreateClaimsForASP_NetUsersBasedOnRole(string roleId, List<Menus_List> ListMenus)
        {
            //Note: Remember we are saving only Claims whose Value is true...
            try
            {
                List<AuthUser> usersList = this.roleHandle.GetAllUsersBelongingToTheRole(roleId);
                foreach (AuthUser user in usersList)
                {
                    var _user = await userManager.FindByIdAsync(user.Id);
                    AlreadyExistingClaimsForUser = await GetClaimsListForUser(user.UserName);
                    foreach (Menus_List menu in ListMenus)
                    {
                        foreach (SubMenu_List submenus in menu.subItems)
                        {
                            foreach (ClaimsModels claim in submenus.SubMenuClaim)
                            {
                                if (!AlreadyExistingClaimsForUser.Any(c => c.Type == claim.ClaimType && c.Value == ConvertBoolToString(true)))
                                {

                                    if (AlreadyExistingClaimsForUser.Any(c => c.Type == claim.ClaimType))
                                    {

                                        Claim AlreadyExistingClaim = new Claim(claim.ClaimType, InvertClaimValue(ConvertBoolToString(claim.ClaimValue)));
                                        await userManager.RemoveClaimAsync(_user, AlreadyExistingClaim);
                                    }
                                    if (claim.ClaimValue == true && claim.IsClaimShown == true)
                                    {
                                        IdentityResult result = await userManager.AddClaimAsync(_user, new Claim(claim.ClaimType, ConvertBoolToString(claim.ClaimValue)));
                                        if (!result.Succeeded)
                                            return false;
                                    }
                                }
                                else
                                {
                                    if (claim.ClaimValue == false)
                                    {
                                        Claim AlreadyExistingClaim = new Claim(claim.ClaimType, ConvertBoolToString(true));
                                        await userManager.RemoveClaimAsync(_user, AlreadyExistingClaim);
                                    }
                                }
                            }


                            //For submenu
                            foreach (SubMenuFunctions_List submenusfunction in submenus.subItemsList)
                            {
                                foreach (ClaimsModels claim in submenusfunction.SubMenuFunctionClaim)
                                {
                                    //do what ever operation

                                    if (!AlreadyExistingClaimsForUser.Any(c => c.Type == claim.ClaimType && c.Value == ConvertBoolToString(true)))
                                    {
                                        if (AlreadyExistingClaimsForUser.Any(c => c.Type == claim.ClaimType))
                                        {
                                            Claim AlreadyExistingClaim = new Claim(claim.ClaimType, InvertClaimValue(ConvertBoolToString(claim.ClaimValue)));
                                            await userManager.RemoveClaimAsync(_user, AlreadyExistingClaim);
                                        }
                                        if (claim.ClaimValue == true && claim.IsClaimShown == true)
                                        {
                                            IdentityResult result = await userManager.AddClaimAsync(_user, new Claim(claim.ClaimType, ConvertBoolToString(claim.ClaimValue)));
                                            if (!result.Succeeded)
                                                return false;
                                        }
                                    }
                                    else
                                    {
                                        if (claim.ClaimValue == false)
                                        {
                                            Claim AlreadyExistingClaim = new Claim(claim.ClaimType, ConvertBoolToString(true));
                                            await userManager.RemoveClaimAsync(_user, AlreadyExistingClaim);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Claim>> GetClaimsListForUser(string userName)
        {
            var _user = await globalcontext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            //var user = await userManager.FindByNameAsync(userName);
            IEnumerable<Claim> _claims = await userManager.GetClaimsAsync(_user);
            return _claims;
        }
        private string ConvertBoolToString(bool value)
        {
            if (value)
                return "Y";
            else
                return "N";
        }
        private string InvertClaimValue(string claimValue)
        {
            if (claimValue == "Y")
                return "N";
            else
                return "Y";
        }
        public async Task<List<Menus_List>> GetAllClaimsAllocatedToRole(string roleId)
        {
            try
            {

                var _result = (from a in globalcontext.Menus
                                   //join b in SubMenus on a.M_Id equals b.SM_M_Id_FK 
                                   //join c in globalcontext.RoleClaims on a.M_Id equals c.RC_M_Id_FK
                                   //group new { a } by new { a.M_Id, a.M_label, a.M_icon, a.M_Title } into grouped
                               select new Menus_List()
                               {
                                   M_Id = a.M_Id,
                                   M_label = a.M_label,
                                   M_icon = a.M_icon,
                                   ClaimValue = ((from x in globalcontext.Menus
                                                  join y in globalcontext.RoleClaims on x.M_Id equals y.RC_M_Id_FK
                                                  where x.M_Id == a.M_Id && y.RC_Value == "Y" && y.RC_RoleId_FK == roleId
                                                  select x).Count()) >= 1 ? true : false,
                                   subItems = ((from d in globalcontext.SubMenu
                                                where d.SM_M_Id_FK == a.M_Id
                                                select new SubMenu_List()
                                                {
                                                    SM_Id = d.SM_Id,
                                                    SM_label = d.SM_label,
                                                    SM_icon = d.SM_icon,
                                                    SM_link = d.SM_link,
                                                    ClaimValue = ((from x in globalcontext.SubMenu
                                                                   join y in globalcontext.RoleClaims on x.SM_Id equals y.RC_SM_Id_FK
                                                                   where x.SM_Id == d.SM_Id && y.RC_Value == "Y" && y.RC_RoleId_FK == roleId
                                                                   select x).Count()) >= 1 ? true : false,
                                                    SubMenuClaim = ((from e in globalcontext.SubMenusDetails
                                                                     join f in globalcontext.SubMenu on e.SMD_SM_Id_FK equals f.SM_Id
                                                                     where f.SM_label == d.SM_label
                                                                     select new ClaimsModels()
                                                                     {
                                                                         ClaimTypeId = e.SMD_Id,
                                                                         IsClaimShown = e.SMD_IsClaimShown_In_UI,
                                                                         ClaimType = e.SMD_SubMenusFunction,
                                                                         ClaimValue = ((from g in globalcontext.SubMenusDetails
                                                                                        join h in globalcontext.RoleClaims on e.SMD_Id equals h.RC_SMD_Id_FK
                                                                                        where g.SMD_SM_Id_FK == f.SM_Id && h.RC_Value == "Y" && h.RC_RoleId_FK == roleId
                                                                                        select g).Count()) >= 1 ? true : false
                                                                     }).ToList()),
                                                    subItemsList = ((from h in globalcontext.SubMenusFunctions
                                                                         //join h in globalcontext.SubMenusFunctions on g.SM_Id equals h.SMF_SM_Id_FK
                                                                         //join i in globalcontext.SubRoleClaims on h.SMF_Id equals i.SRC_SMF_Id_FK
                                                                     where h.SMF_SM_Id_FK == d.SM_Id
                                                                     //group new { h } by new { h.SMF_Id, h.SMF_label, h.SMF_icon, h.SMF_link } into subgroup
                                                                     select new SubMenuFunctions_List()
                                                                     {
                                                                         SMF_Id = h.SMF_Id,
                                                                         SMF_label = h.SMF_label,
                                                                         SMF_icon = h.SMF_icon,
                                                                         SMF_link = h.SMF_link,
                                                                         SubMenuFunctionClaim = ((from j in globalcontext.SubMenusFunctionDetails
                                                                                                  join k in globalcontext.SubMenusFunctions on j.SMFD_SMF_Id_FK equals k.SMF_Id
                                                                                                  select new ClaimsModels()
                                                                                                  {
                                                                                                      ClaimTypeId = j.SMFD_Id,
                                                                                                      IsClaimShown = j.SMFD_IsClaimShown_In_UI,
                                                                                                      ClaimType = j.SMFD_SubMenusFunction,
                                                                                                      ClaimValue = ((from l in globalcontext.SubMenusFunctionDetails
                                                                                                                     join m in globalcontext.SubRoleClaims on l.SMFD_Id equals m.SRC_SMFD_Id_FK
                                                                                                                     where m.SRC_SMF_Id_FK == k.SMF_Id && m.SRC_Value == "Y" && m.SRC_RoleId_FK == roleId
                                                                                                                     select d).Count()) >= 1 ? true : false
                                                                                                  }).ToList())
                                                                     }).ToList())
                                                }).ToList())
                               }).ToListAsync();

                return await _result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Menus>> Gettest()
        {
            var result = (from d in globalcontext.Menus select d).ToListAsync();
            return await result;
        }


        public async Task<bool> CreateClaimsBasedOnRoles(string userId, string roleId)
        {
            List<Menus_List> ClaimsFromTelemedicineRoleClaimsTable = await GetAllClaimsAllocatedToRole(roleId);
            foreach (Menus_List Menu in ClaimsFromTelemedicineRoleClaimsTable)
            {
                await CreateClaimsInAspNetUserClaimsTableForRole(userId, Menu.subItems);
            }
            return true;
        }

        public async Task<bool> CreateClaimsInAspNetUserClaimsTableForRole(string userId, List<SubMenu_List> subMenuList)
        {

            AuthUser AuthUser = new AuthUser();
            var testing = await GetClaimsListForUser(userId);
            var _user = await userManager.FindByNameAsync(userId);

            foreach (SubMenu_List subMenu in subMenuList)
            {
                foreach (ClaimsModels claim in subMenu.SubMenuClaim)
                {
                    if (testing.Any(c => c.Type == claim.ClaimType))
                    {
                        Claim AlreadyExistingClaim = new Claim(claim.ClaimType, "Y");
                        await userManager.RemoveClaimAsync(_user, AlreadyExistingClaim);
                    }

                    if (claim.ClaimValue == true)
                    {
                        IdentityResult result = await userManager.AddClaimAsync(_user, new Claim(claim.ClaimType, "Y"));
                        if (!result.Succeeded)
                            return false;
                    }
                }
            }
            return true;
        }

        public async Task<string> getrolename(string username)
        {
            var result = await userManager.FindByNameAsync(username);
            var result1 = await roleManager.FindByIdAsync(result.Role_Id_FK);
            return result1.Name;
        }
    }
}
