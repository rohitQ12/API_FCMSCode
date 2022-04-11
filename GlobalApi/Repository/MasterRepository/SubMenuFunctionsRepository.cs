using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.AdminRepository
{
    public class SubMenuFunctionsRepository: ISubMenuFunctionsRepository
    {
        GlobalContext rolecontext;
        public IPrimarykeyvalue primarykeyvalue;
        public SubMenuFunctionsRepository(GlobalContext rolecontext)
        {
            this.rolecontext = rolecontext;
            primarykeyvalue = new Primarykeyvalue(rolecontext);
        }

        public async Task<SubMenuFunctions> InsertAppSubMenuFunctions(SubMenuFunctions subMenuFunctions)
        {
            try
            {
                int id = await primarykeyvalue.primary_key("SubMenuFunctions");
                SubMenuFunctions obj = new SubMenuFunctions()
                {
                    SMF_Id = id,
                    SMF_label = subMenuFunctions.SMF_label,
                    SMF_icon = subMenuFunctions.SMF_icon,
                    SMF_SM_Id_FK = subMenuFunctions.SMF_SM_Id_FK,
                    Created_by = 1,
                    Created_date = DateTime.Now,
                    Delete_flag = false,
                    Status = 1
                };
                var result = await rolecontext.SubMenusFunctions.AddAsync(obj);
                await rolecontext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<SubMenuFunctions> UpdateAppSubMenuFunctions(SubMenuFunctions subMenuFunctions)
        {
            try
            {
                var result = await rolecontext.SubMenusFunctions.FirstOrDefaultAsync(x => x.SMF_Id == subMenuFunctions.SMF_Id);
                if (result != null)
                {
                    result.SMF_label = subMenuFunctions.SMF_label;
                    result.SMF_icon = subMenuFunctions.SMF_icon;
                    result.SMF_SM_Id_FK = subMenuFunctions.SMF_SM_Id_FK;
                    result.Created_by = 1;
                    result.Created_date = DateTime.Now;
                    result.Delete_flag = false;
                    result.Status = 1;
                    await rolecontext.SaveChangesAsync();
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<SubMenuFunctions>> GetAllAppSubMenuFunctions()
        {
            try
            {
                if (rolecontext != null)
                {
                    var query = (from a in rolecontext.SubMenu
                                 from b in rolecontext.SubMenusFunctions
                                 where a.SM_Id == b.SMF_SM_Id_FK && b.Status == 1 && b.Delete_flag == false
                                 select new SubMenuFunctions
                                 {
                                     SMF_SM_Id_FK = a.SM_Id,
                                     SMF_icon = a.SM_icon,
                                     SMF_Id = b.SMF_Id,
                                     SMF_label = b.SMF_label,
                                     SMF_link = b.SMF_link,
                                 });
                    return await query.ToListAsync();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<SubMenuFunctions> DeleteAppSubMenuFunctions(int SMF_Id)
        {
            try
            {
                var result = await rolecontext.SubMenusFunctions.FirstOrDefaultAsync(x => x.SMF_Id == SMF_Id);
                if (result != null)
                {
                    result.Delete_flag = true;
                    result.Status = 0;
                    result.Deleted_by = 1;
                    result.Deleted_date = DateTime.Now;
                    await rolecontext.SaveChangesAsync();
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<SubMenuFunctions> GetAppSubMenuFunctionsById(int SMF_Id)
        {
            if (rolecontext != null)
            {
                var query = (from a in rolecontext.SubMenusFunctions
                             where a.SMF_Id == SMF_Id && a.Status == 1 && a.Delete_flag == false
                             select new SubMenuFunctions
                             {
                                 SMF_Id = a.SMF_Id,
                                 SMF_label = a.SMF_label,
                                 SMF_icon = a.SMF_icon,
                                 SMF_SM_Id_FK = a.SMF_SM_Id_FK,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<SubMenuFunctions>> GetAppSubMenuFunctions()
        {
            if (rolecontext != null)
            {
                var query = (from a in rolecontext.SubMenusFunctions
                             select a).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
