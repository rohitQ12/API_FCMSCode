using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.AdminRepository
{
    public class SubMenuFunctionsRepository: ISubMenusFunctionsRepository
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public SubMenuFunctionsRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<SubMenusFunctions> InsertAppSubMenuFunctions(SubMenusFunctions subMenuFunctions)
        {
            try
            {
                int id = await primarykeyvalue.primary_key("SubMenuFunctions");
                SubMenusFunctions obj = new SubMenusFunctions()
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
                var result = await db.SubMenusFunctions.AddAsync(obj);
                await db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<SubMenusFunctions> UpdateAppSubMenuFunctions(SubMenusFunctions subMenuFunctions)
        {
            try
            {
                var result = await db.SubMenusFunctions.FirstOrDefaultAsync(x => x.SMF_Id == subMenuFunctions.SMF_Id);
                if (result != null)
                {
                    result.SMF_label = subMenuFunctions.SMF_label;
                    result.SMF_icon = subMenuFunctions.SMF_icon;
                    result.SMF_SM_Id_FK = subMenuFunctions.SMF_SM_Id_FK;
                    result.Created_by = 1;
                    result.Created_date = DateTime.Now;
                    result.Delete_flag = false;
                    result.Status = 2;
                    await db.SaveChangesAsync();
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<SubMenusFunctions>> GetAllAppSubMenuFunctions()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.SubMenu
                                 from b in db.SubMenusFunctions
                                 where a.SM_Id == b.SMF_SM_Id_FK && b.Status == 1 && b.Delete_flag == false
                                 select new SubMenusFunctions
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
        public async Task<SubMenusFunctions> DeleteAppSubMenuFunctions(int SMF_Id)
        {
            try
            {
                var result = await db.SubMenusFunctions.FirstOrDefaultAsync(x => x.SMF_Id == SMF_Id);
                if (result != null)
                {
                    result.Delete_flag = true;
                    result.Status = 6;
                    result.Deleted_by = 1;
                    result.Deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<SubMenusFunctions> GetAppSubMenuFunctionsById(int SMF_Id)
        {
            if (db != null)
            {
                var query = (from a in db.SubMenusFunctions
                             where a.SMF_Id == SMF_Id && a.Status == 1 && a.Delete_flag == false
                             select new SubMenusFunctions
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
        public async Task<List<SubMenusFunctions>> GetAppSubMenuFunctions()
        {
            if (db != null)
            {
                var query = (from a in db.SubMenusFunctions
                             select a).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
