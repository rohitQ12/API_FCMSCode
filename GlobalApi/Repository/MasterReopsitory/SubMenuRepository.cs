using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.AdminClaims;
using Microsoft.EntityFrameworkCore;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class SubMenuRepository : ISubMenu
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public SubMenuRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<SubMenu> InsertAppSubMenu(SubMenu subMenu)
        {
            try
            {
                int id = await primarykeyvalue.primary_key("SubMenu");
                SubMenu obj = new SubMenu()
                {
                    SM_Id = id,
                    SM_label = subMenu.SM_label,
                    SM_link = subMenu.SM_link,
                    SM_Title = subMenu.SM_Title,
                    SM_Redirect_URL = subMenu.SM_Redirect_URL,
                    SM_DisplayOrder = subMenu.SM_DisplayOrder,
                    SM_M_Id_FK = subMenu.SM_M_Id_FK,
                    Created_by = 1,
                    Created_date = DateTime.Now,
                    Delete_flag = false,
                    Status = 1
                };
                var result = await db.SubMenu.AddAsync(obj);
                await db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<SubMenu> UpdateAppSubMenu(SubMenu subMenu)
        {
            try
            {
                var result = await db.SubMenu.FirstOrDefaultAsync(x => x.SM_Id == subMenu.SM_Id);
                if (result != null)
                {
                    result.SM_label = subMenu.SM_label;
                    result.SM_link = subMenu.SM_link;
                    result.SM_Title = subMenu.SM_Title;
                    result.SM_Redirect_URL = subMenu.SM_Redirect_URL;
                    result.SM_DisplayOrder = subMenu.SM_DisplayOrder;
                    result.SM_M_Id_FK = subMenu.SM_M_Id_FK;
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
        public async Task<List<SubMenu>> GetAllAppSubMenu()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Menus
                                 from b in db.SubMenu
                                 where a.M_Id == b.SM_M_Id_FK && b.Status == 1 && b.Delete_flag == false
                                 select new SubMenu
                                 {
                                     SM_M_Id_FK = a.M_Id,
                                     M_MenuName = a.M_label,
                                     SM_Id = b.SM_Id,
                                     SM_label = b.SM_label,
                                     SM_link = b.SM_link,
                                     SM_Title = b.SM_Title,
                                     SM_Redirect_URL = b.SM_Redirect_URL,
                                     SM_DisplayOrder = b.SM_DisplayOrder
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
        public async Task<SubMenu> DeleteAppSubMenu(int SM_Id)
        {
            try
            {
                var result = await db.SubMenu.FirstOrDefaultAsync(x => x.SM_Id == SM_Id);
                if (result != null)
                {
                    result.SM_Id = SM_Id;
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

        public async Task<SubMenu> GetAppSubMenuById(int SM_Id)
        {
            if (db != null)
            {
                var query = (from a in db.SubMenu
                             where a.SM_Id == SM_Id && a.Status == 1 && a.Delete_flag == false
                             select new SubMenu
                             {
                                 SM_Id = a.SM_Id,
                                 SM_label = a.SM_label,
                                 SM_link = a.SM_link,
                                 SM_M_Id_FK = a.SM_M_Id_FK,
                                 SM_Title = a.SM_Title,
                                 SM_Redirect_URL = a.SM_Redirect_URL,
                                 SM_DisplayOrder = a.SM_DisplayOrder
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<SubMenu>> GetAppSubMenu()
        {
            if (db != null)
            {
                var query = (from a in db.SubMenu
                             select a).ToListAsync();
                return await query;
            }
            return null;
        }
    }
}
