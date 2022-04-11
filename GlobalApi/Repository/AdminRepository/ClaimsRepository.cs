//using GlobalApi.Controllers.AdminController;
//using GlobalApi.Data;
//using GlobalApi.GlobalClasses;
//using GlobalApi.Models.Authentication;
//using GlobalApi.Models.AdminClaims;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using System.Data;
//using System.Security.Claims;

//namespace GlobalApi.Repository.AuthRepository
//{
//    public class ClaimsRepository
//    {
//        public readonly string _connectionString;
//        private readonly UserManager<AuthUser> userManager;
//        private IEnumerable<Claim> AlreadyExistingClaimsForUser = null;
//        RoleManager<IdentityRole> roleManager;
//        GlobalContext globalcontext;
//        ClaimsHandle claimshandle;
//        public ClaimsRepository(ClaimsHandle claimshandle,IConfiguration configuration,UserManager<AuthUser> userManager, RoleManager<IdentityRole> roleManager, GlobalContext globalcontext)
//        {
//            this.userManager = userManager;
//            this.roleManager = roleManager;
//            this.globalcontext = globalcontext;
//            _connectionString = configuration.GetConnectionString("ConnectionString");
//            claimshandle = this.claimshandle;
//        }
//        public async Task<bool> CreateClaimsForASP_NetUsersBasedOnRole(string roleId, List<Menus> ListMenus)
//        {
//            //Note: Remember we are saving only Claims whose Value is true...

//            List<AuthUser> usersList = GetAllUsersBelongingToTheRole(roleId);
//            foreach (AuthUser user in usersList)
//            {
//                var _user = await userManager.FindByIdAsync(user.Id);
//                //AuthUser _new = new AuthUser();
//                //_new.Id = user.Id;
//                //_new.UserName = "testing131";
//                AlreadyExistingClaimsForUser = await GetClaimsListForUser(user.UserName);
//                foreach (Menus menu in ListMenus)
//                {
//                    foreach (Pages page in menu.PagesList)
//                    {
//                        foreach (ClaimsModels claim in page.PageFunctionClaim)
//                        {
//                            if (!AlreadyExistingClaimsForUser.Any(c => c.Type == claim.ClaimType && c.Value == ConvertBoolToString(true)))
//                            {

//                                if (AlreadyExistingClaimsForUser.Any(c => c.Type == claim.ClaimType))
//                                {

//                                    Claim AlreadyExistingClaim = new Claim(claim.ClaimType, InvertClaimValue(ConvertBoolToString(claim.ClaimValue)));
//                                    await userManager.RemoveClaimAsync(_user, AlreadyExistingClaim);
//                                }
//                                if (claim.ClaimValue == true && claim.IsClaimShown == true)
//                                {
//                                    IdentityResult result = await userManager.AddClaimAsync(_user, new Claim(claim.ClaimType, ConvertBoolToString(claim.ClaimValue)));
//                                    //IdentityResult result = await userManager.AddClaimsAsync(_new, new Claim(claim.ClaimType, ConvertBoolToString(claim.ClaimValue)));
//                                    //var xnew = await userManager.AddClaimAsync(_user, new Claim(claim.ClaimType, ConvertBoolToString(claim.ClaimValue)));
//                                    if (!result.Succeeded)
//                                        return false;
//                                }
//                            }
//                            else
//                            {
//                                if (claim.ClaimValue == false)
//                                {
//                                    Claim AlreadyExistingClaim = new Claim(claim.ClaimType, ConvertBoolToString(true));
//                                    await userManager.RemoveClaimAsync(_user, AlreadyExistingClaim);
//                                }
//                            }
//                        }


//                        //For submenu
//                        foreach (SubPages subpage in page.SubPagesModels)
//                        {
//                            foreach (ClaimsModels claim in subpage.ClaimsModels)
//                            {
//                                //do what ever operation

//                                if (!AlreadyExistingClaimsForUser.Any(c => c.Type == claim.ClaimType && c.Value == ConvertBoolToString(true)))
//                                {
//                                    if (AlreadyExistingClaimsForUser.Any(c => c.Type == claim.ClaimType))
//                                    {
//                                        Claim AlreadyExistingClaim = new Claim(claim.ClaimType, InvertClaimValue(ConvertBoolToString(claim.ClaimValue)));
//                                        await userManager.RemoveClaimAsync(_user, AlreadyExistingClaim);
//                                    }
//                                    if (claim.ClaimValue == true && claim.IsClaimShown == true)
//                                    {
//                                        IdentityResult result = await userManager.AddClaimAsync(_user, new Claim(claim.ClaimType, ConvertBoolToString(claim.ClaimValue)));
//                                        if (!result.Succeeded)
//                                            return false;
//                                    }
//                                }
//                                else
//                                {
//                                    if (claim.ClaimValue == false)
//                                    {
//                                        Claim AlreadyExistingClaim = new Claim(claim.ClaimType, ConvertBoolToString(true));
//                                        await userManager.RemoveClaimAsync(_user, AlreadyExistingClaim);
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            return true;
//        }
//        private string ConvertBoolToString(bool value)
//        {
//            if (value)
//                return "Y";
//            else
//                return "N";
//        }
//        private string InvertClaimValue(string claimValue)
//        {
//            if (claimValue == "Y")
//                return "N";
//            else
//                return "Y";
//        }
//        public List<AuthUser> GetAllUsersBelongingToTheRole(string roleId)
//        {
//            List<AuthUser> usersBelongingToTheRole =  new List<AuthUser> { new AuthUser() { Id= "0c0eb50c-a10f-4489-8396-161cc2f1122f",UserName= "kbvarshan" } };
//            return usersBelongingToTheRole;
//        }
//        public async Task<IEnumerable<Claim>> GetClaimsListForUser(string userName)
//        {
//            var EdiaryUserModels = await userManager.FindByNameAsync(userName);
//            var user = await userManager.FindByNameAsync(userName);
//            IEnumerable<Claim> _claims = await userManager.GetClaimsAsync(user);
//            AuthUser authUser = new AuthUser();
//            authUser.Role_Id_FK = EdiaryUserModels.Id.ToString();
//            string role_id_FK = EdiaryUserModels.Id.ToString();
//            IEnumerable<Claim> claims = await (userManager.GetClaimsAsync(authUser));
//            return _claims;
//        }
//        public async Task<bool> Create_RoleClaim(string roleId, List<Menus> CFMSMenus)
//        {
//            //Note: Remember we are saving only Claims whose Value is true...
//            List<Menus> AlreadyExistsClaimsListOfTheRole =await this.claimshandle.GetAllClaimsAllocatedToRole(roleId);
//            //List<Menus> AlreadyExistsClaimsListOfTheRole = await GetAllClaimsAllocatedToRole(roleId);
//            foreach (var menu in CFMSMenus)
//            {
//                foreach (var page in menu.PagesList)
//                {
//                    foreach (var claim in page.PageFunctionClaim)
//                    {
//                        if (!AlreadyExistsClaimsListOfTheRole.Any(c => (c.PagesList.Any(d => d.P_PageName == page.P_PageName) && (c.PagesList.Any(d => d.PageFunctionClaim.Any(e => e.ClaimTypeId == claim.ClaimTypeId && e.ClaimValue == true))))))
//                        {
//                            if (AlreadyExistsClaimsListOfTheRole.Any(c => (c.PagesList.Any(d => d.P_PageName == page.P_PageName) && (c.PagesList.Any(d => d.PageFunctionClaim.Any(e => e.ClaimTypeId == claim.ClaimTypeId))))))
//                            {
//                                var result = await globalcontext.RoleClaims.FirstOrDefaultAsync(x => x.RC_RoleId_FK == roleId);
//                                if (result != null)
//                                {
//                                    result.RC_PF_Id_FK = claim.ClaimTypeId;
//                                    result.Delete_flag = true;
//                                    result.Status = 0;
//                                    result.Deleted_by = 1;
//                                    result.Deleted_date = DateTime.Now;
//                                    await globalcontext.SaveChangesAsync();
//                                }
//                            }
//                            if (claim.ClaimValue == true && claim.IsClaimShown == true)
//                            {
//                                //int KRC_Id = GlobalContext.RoleClaims.Max(u=>u.Id);
//                                RoleClaims obj = new RoleClaims()
//                                {
//                                    RC_Id = 1,
//                                    RC_RoleId_FK = roleId,
//                                    RC_PF_Id_FK = claim.ClaimTypeId,
//                                    RC_Value = "Y",
//                                    RC_UserId_FK = 1,
//                                    RC_INSTS = System.DateTime.Now,
//                                    Delete_flag = true,
//                                    Status = 0,
//                                    Deleted_by = 1,
//                                    Deleted_date = DateTime.Now,
//                                };
//                                var result = await globalcontext.RoleClaims.AddAsync(obj);
//                                await globalcontext.SaveChangesAsync();
//                            }
//                        }
//                        else
//                        {
//                            if (claim.ClaimValue == false)
//                            {
//                                var result = await globalcontext.RoleClaims.FirstOrDefaultAsync(x => x.RC_RoleId_FK == roleId);
//                                if (result != null)
//                                {
//                                    result.RC_PF_Id_FK = claim.ClaimTypeId;
//                                    result.Delete_flag = true;
//                                    result.Status = 0;
//                                    result.Deleted_by = 1;
//                                    result.Deleted_date = DateTime.Now;
//                                    await globalcontext.SaveChangesAsync();
//                                }
//                            }
//                        }

//                    }
//                    //For sub pages
//                    foreach (var subpage in page.SubPagesModels)
//                    {
//                        foreach (var subpageclaim in subpage.ClaimsModels)
//                        {
//                            if (subpageclaim.ClaimValue == true)
//                            {
//                                //delete add

//                                try
//                                {
//                                    var result = await globalcontext.SubRoleClaims.FirstOrDefaultAsync(x => x.SRC_RoleId_FK == roleId);
//                                    if (result != null)
//                                    {
//                                        result.SRC_KSPF_Id_FK = subpageclaim.ClaimTypeId;
//                                        result.Delete_flag = true;
//                                        result.Status = 0;
//                                        result.Deleted_by = 1;
//                                        result.Deleted_date = DateTime.Now;
//                                        await globalcontext.SaveChangesAsync();
//                                    }


//                                    //int KRC_Id = globalcontext.CFMS_SubMenuRoleClaims.Max(u => u.CRC_Id);
//                                    // con = ado_configrations.connection();
//                                    SubRoleClaims sobj = new SubRoleClaims()
//                                    {
//                                        SRC_Id =1,
//                                        SRC_RoleId_FK = roleId,
//                                        SRC_KSPF_Id_FK = subpageclaim.ClaimTypeId,
//                                        SRC_Value = "Y",
//                                        SRC_UserId_FK = 1,
//                                        SRC_INSTS = System.DateTime.Now,
//                                        Delete_flag = true,
//                                        Status = 0,
//                                        Deleted_by = 1,
//                                        Deleted_date = DateTime.Now,
//                                    };
//                                    await globalcontext.SubRoleClaims.AddAsync(sobj);
//                                    await globalcontext.SaveChangesAsync();
//                                }

//                                catch (Exception e)
//                                {
//                                    throw new Exception("error");
//                                }
//                            }


//                            else
//                            {
//                                //delete

//                                var result = await globalcontext.SubRoleClaims.FirstOrDefaultAsync(x => x.SRC_RoleId_FK == roleId);
//                                if (result != null)
//                                {
//                                    result.SRC_KSPF_Id_FK = subpageclaim.ClaimTypeId;
//                                    result.Delete_flag = true;
//                                    result.Status = 0;
//                                    result.Deleted_by = 1;
//                                    result.Deleted_date = DateTime.Now;
//                                    await globalcontext.SaveChangesAsync();
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            return true;
//        }
//        public async Task<List<Menus>> GetAllClaimsAllocatedToRole(string roleId)
//        {

//            try
//            {
//                var _result1 = (from r in globalcontext.RoleClaims
//                                join p in globalcontext.PageFunctions on r.RC_PF_Id_FK equals p.PF_Id
//                                where r.RC_RoleId_FK == roleId
//                                select new RoleClaims
//                                {
//                                    PageFunctions = r.PageFunctions,
//                                    RC_PF_Id_FK = r.RC_PF_Id_FK,
//                                    RC_Value = r.RC_Value
//                                }).ToList<RoleClaims>();
//                var _result2 = (from s in globalcontext.SubRoleClaims
//                                join p in globalcontext.SubPageFunctions on s.SRC_KSPF_Id_FK equals p.SPF_Id
//                                where s.SRC_RoleId_FK == roleId
//                                select new SubPageFunctions
//                                {
//                                    SPF_PageFunction = p.SPF_PageFunction,
//                                    SPF_KP_Id_FK = s.SRC_KSPF_Id_FK,
//                                }).ToList<SubPageFunctions>();

//                var _result3 = (from m in globalcontext.Menus
//                                select new Menus()
//                                {
//                                    label = m.label,
//                                    PagesList = ((from z in globalcontext.Pages
//                                                  where z.P_KM_Id_FK == m.M_Id
//                                                  select new Pages
//                                                  {
//                                                      P_Id = z.P_Id,
//                                                      P_PageName = z.P_PageName,
//                                                      PageFunctionClaim = ((from r in globalcontext.PageFunctions
//                                                                            join p in globalcontext.Pages on r.PF_KP_Id_FK equals p.P_Id
//                                                                            where p.P_PageName == z.P_PageName
//                                                                            select new ClaimsModels
//                                                                            {
//                                                                                ClaimTypeId = r.PF_Id,
//                                                                                IsClaimShown = true,
//                                                                                ClaimType = "",
//                                                                                ClaimValue = (from d in globalcontext.RoleClaims where d.RC_PF_Id_FK == r.PF_Id && d.RC_Value == "Y" select d).Any()
//                                                                            }).ToList()),
//                                                      SubPagesModels = ((from r in globalcontext.SubPageFunctions
//                                                                         join p in globalcontext.Pages on r.SPF_KP_Id_FK equals p.P_Id
//                                                                         where z.P_PageName == z.P_PageName
//                                                                         select new SubPages
//                                                                         {
//                                                                             SP_KP_Id_FK = r.SPF_KP_Id_FK,
//                                                                             SP_PageName = r.SPF_PageFunction,
//                                                                             SP_Name = r.SPF_PageFunction,
//                                                                             ClaimsModels = ((from y in globalcontext.SubPageFunctions
//                                                                                             join p in globalcontext.SubPages on r.SPF_KP_Id_FK equals p.SP_Id
//                                                                                             where p.SP_PageName == r.SPF_PageFunction
//                                                                                             select new ClaimsModels()
//                                                                                             {
//                                                                                                 ClaimTypeId = y.SPF_Id,
//                                                                                                 IsClaimShown = y.SPF_IsClaimShown_In_UI,
//                                                                                                 ClaimType = y.SPF_PageFunction,
//                                                                                                 ClaimValue = (from d in globalcontext.RoleClaims where d.RC_PF_Id_FK == r.SPF_Id && d.RC_Value == "Y" select d).Any(),
//                                                                                             }).ToList())
//                                                                         }).ToList())
//                                                  }).ToList())
//                                }).ToListAsync();

//                return await _result3;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }


//        }
//        private List<Pages> Create_Pages(int MenuId, List<RoleClaims> ExistingClaimsOfUser, List<SubPageFunctions> subClaimList)
//        {

//            var TotalClaimsList = (from p in globalcontext.Pages
//                                   where p.P_KM_Id_FK == MenuId
//                                   select new Pages
//                                   {
//                                       P_Id = p.P_Id,
//                                       P_PageName = p.P_PageName,
//                                       PageFunctionClaim = Create_PageFunctionsClaim(p.P_PageName, ExistingClaimsOfUser),
//                                       SubPagesModels = CreateSub_Pages(p.P_PageName, subClaimList)
//                                   }).ToList();
//            return TotalClaimsList;
//        }
//        private List<ClaimsModels> Create_PageFunctionsClaim(string PageName, List<RoleClaims> claimListThatRoleHas)
//        {
//            var _new = (from r in globalcontext.PageFunctions
//                        join p in globalcontext.Pages on r.PF_KP_Id_FK equals p.P_Id
//                        where p.P_PageName == PageName
//                        select new ClaimsModels
//                        {
//                            ClaimTypeId = r.PF_Id,
//                            IsClaimShown = true,
//                            ClaimType = "",
//                            ClaimValue = CheckClaimExistForRole(claimListThatRoleHas, r.PF_Id)
//                        }).ToList();

//            return _new;
//        }
//        private bool CheckClaimExistForRole(List<RoleClaims> claimList, int pageFunctionId)
//        {
//            if (claimList.Any(x => x.RC_PF_Id_FK == pageFunctionId && x.RC_Value == "Y"))
//                return true;
//            else
//                return false;
//        }
//        public List<SubPages> CreateSub_Pages(string Pagename, List<SubPageFunctions> subClaimList)
//        {
//            var Subpagelist = (from r in globalcontext.SubPageFunctions
//                               join p in globalcontext.Pages on r.SPF_KP_Id_FK equals p.P_Id
//                               where p.P_PageName == Pagename
//                               select new SubPages
//                               {
//                                   SP_KP_Id_FK = r.SPF_KP_Id_FK,
//                                   SP_PageName = r.SPF_PageFunction,
//                                   SP_Name = r.SPF_PageFunction,
//                                   ClaimsModels = CreateKHBSub_PagesFunctions_test(r.SPF_PageFunction, subClaimList)
//                               }).ToList();
//            return Subpagelist;
//        }
//        public List<ClaimsModels> CreateSub_PagesFunctions(string? SubPageName, List<SubPageFunctions> subClaimList)
//        {
//            List<ClaimsModels> clm = new List<ClaimsModels>();
//            ClaimsModels clims = new ClaimsModels();
            
//            var result = (from r in globalcontext.SubPageFunctions
//                         join p in globalcontext.SubPages on r.SPF_KP_Id_FK equals p.SP_Id
//                         where p.SP_PageName == SubPageName
//                         select r).ToList();
//            foreach (SubPageFunctions subpagelist in result)
//            {
//                clims.ClaimTypeId = subpagelist.SPF_Id;
//                clims.IsClaimShown = subpagelist.SPF_IsClaimShown_In_UI;
//                clims.ClaimType = subpagelist.SPF_PageFunction;
//                clims.ClaimValue = ChecksubClaimExistForRole(subClaimList, subpagelist.SPF_Id); 
//            }
//            clm.Add(clims);
//            var Subpagelist = from r in globalcontext.SubPageFunctions
//                              join p in globalcontext.SubPages on r.SPF_KP_Id_FK equals p.SP_Id
//                              where p.SP_PageName == SubPageName
//                              select new ClaimsModels()
//                              {
//                                  ClaimTypeId = r.SPF_Id,
//                                  IsClaimShown = r.SPF_IsClaimShown_In_UI,
//                                  ClaimType = r.SPF_PageFunction,
//                                  ClaimValue = ChecksubClaimExistForRole(subClaimList, r.SPF_Id)
//                              };
//            //var result1 = globalcontext.SubPageFunctions.Join(globalcontext.SubPages,
//            //                            x => x.SPF_Id, y => y.SP_Id, (x, y) =>
//            //                            new ClaimsModels {
//            //                                ClaimTypeId = x.SPF_Id,
//            //                                IsClaimShown = x.SPF_IsClaimShown_In_UI,
//            //                                ClaimType = x.SPF_PageFunction,
//            //                                //ClaimValue = (subClaimList.Any(z => z.SPF_Id == x.SPF_Id && x.SRC_Value == "Y") == true ? true : false),
//            //                                ///ClaimValue = subClaimList.Where(item => { if (item.SPF_Id== x.SPF_Id) return true; else return false; }),
//            //                                ClaimValue =((from d in subClaimList where d.SPF_Id== x.SPF_Id select d).Any())==true ? true :false,
//            //                            }).ToList();

//            return clm;
//            //testing data 
            

//        }
//        public List<ClaimsModels> CreateKHBSub_PagesFunctions_test(string SubPageName, List<SubPageFunctions> subClaimList)
//        {
//            List<ClaimsModels> Subpagelist = new List<ClaimsModels>();
//            Microsoft.Data.SqlClient.SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
//            Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand();
//            cmd1.CommandType = CommandType.StoredProcedure;
//            cmd1.Connection = con;
//            cmd1.CommandText = "GetSubPagesFunctionsForPurticularPage";
//            cmd1.Parameters.AddWithValue("@SubPageName", SubPageName);
//            SqlDataAdapter da1 = new SqlDataAdapter();
//            DataTable dt1 = new DataTable();
//            da1.SelectCommand = cmd1;
//            con.Open();
//            da1.Fill(dt1);
//            con.Close();

//            //Bind EmpModel generic list using LINQ 
//            Subpagelist = (from DataRow drr in dt1.Rows
//                           select new ClaimsModels()
//                           {
//                               ClaimTypeId = (int)drr["SPF_Id"],
//                               IsClaimShown = (bool)drr["SPF_IsClaimShown_In_UI"],
//                               ClaimType = (string)drr["SPF_PageFunction"],
//                               ClaimValue = ChecksubClaimExistForRole(subClaimList, (int)drr["SPF_Id"])


//                           }).ToList();

//            return Subpagelist;
//        }
//        public  bool ChecksubClaimExistForRole(List<SubPageFunctions> subClaimList, int subpageFunctionId)
//        {

//            if (subClaimList.Any(x => x.SPF_Id == subpageFunctionId && x.SRC_Value == "Y"))
//                return true;
//            else
//                return false;
//        }
//        public async Task<bool> manageuserclims(UserClaimsViewModel CFMSMenus)
//        {
//            var user = await userManager.FindByIdAsync(CFMSMenus.UserId);
//            List<Claim> userClaims = new List<Claim>();
//            if (user == null)
//            {
//                return false;
//            }
//            var userRoles = await userManager.GetRolesAsync(user);
//            foreach (var policy in userRoles)
//            {
//                userClaims.Add(new Claim(policy, string.Empty, ClaimValueTypes.String));
//            }

//            // Get all the user existing claims and delete them
//            var claims = await userManager.GetClaimsAsync(user);
//            var result = await userManager.RemoveClaimsAsync(user, claims);

//            if (!result.Succeeded)
//            {
//                return false;
//            }

//            // Add all the claims that are selected on the UI
//            result = await userManager.AddClaimsAsync(user,
//            CFMSMenus.Cliams.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)));
//            if (!result.Succeeded)
//            {
//                return false;
//            }

//            return true;

//        }
       
//    }
//}
