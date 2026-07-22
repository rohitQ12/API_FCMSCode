using GlobalApi.IRepository.MasterIRepository;

using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.Models.AdminClaims;
using GlobalApi.GlobalClasses;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class MenuRepository : IMenu
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public MenuRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<Menus> InsertAppMenu(Menus lead)
        {
            try
            {
                int id = await primarykeyvalue.primary_key("Menu");
                Menus obj = new Menus()
                {
                    M_Id = id,
                    M_label = lead.M_label,
                    M_icon = lead.M_icon,
                    M_Title = lead.M_Title,
                    M_Redirect_URL = lead.M_Redirect_URL,
                    M_DisplayOrder = lead.M_DisplayOrder,
                    Created_by = 1,
                    Created_date = DateTime.Now,
                    Delete_flag = false,
                    Status = 1
                };
                var result = await db.Menus.AddAsync(obj);
                await db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Menus> UpdateAppMenu(Menus lead)
        {
            try
            {
                var result = await db.Menus.FirstOrDefaultAsync(x => x.M_Id == lead.M_Id);
                if (result != null)
                {
                    result.M_Id = lead.M_Id;
                    result.M_label = lead.M_label;
                    result.M_icon = lead.M_icon;
                    result.M_Title = lead.M_Title;
                    result.M_Redirect_URL = lead.M_Redirect_URL;
                    result.M_DisplayOrder = lead.M_DisplayOrder;
                    //result.submodule_id = lead.submodule_id;
                    result.Modified_by = 1;
                    result.Modified_date = DateTime.Now;
                    result.Delete_flag = false;
                    result.Status = 1;
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
        
        //public async Task<List<SubModuleMenu>> GetAllAppMenu()
        //{
        //    try
        //    {
        //        if (db != null)
        //        {
        //            var query = (from a in db.SubModule
        //                         from b in db.Menu
        //                         where a.submodule_id == b.submodule_id && b.status == 1 && b.delete_flag == false
        //                         select new SubModuleMenu
        //                         {
        //                             submodule_id = a.submodule_id,
        //                             submodule_name = a.submodule_name,
        //                             app_menu_id = b.app_menu_id,
        //                             app_menu_name = b.app_menu_name,
        //                             menu_id = b.menu_id,
        //                             app_menu_title = b.app_menu_title,
        //                             app_menu_redirect_URL = b.app_menu_redirect_URL,
        //                             app_menu_displayorder = b.app_menu_displayorder,
        //                         });
        //            return await query.ToListAsync();
        //        }
        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //}
        public async Task<Menus> DeleteAppMenu(int M_Id)
        {
            try
            {
                var result = await db.Menus.FirstOrDefaultAsync(x => x.M_Id == M_Id);

                if (result != null)
                {
                    result.Delete_flag = true;
                    result.Status = 0;
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

        public async Task<Menus> GetAppMenuById(int M_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Menus
                             where a.M_Id == M_Id && a.Status == 1 && a.Delete_flag == false
                             select new Menus
                             {
                                 M_label = a.M_label,
                                 M_icon = a.M_icon,
                                 M_Id = a.M_Id,
                                 M_Title = a.M_Title,
                                 M_Redirect_URL = a.M_Redirect_URL,
                                 M_DisplayOrder = a.M_DisplayOrder
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<Menus>> GetAppMenu()
        {
            var result = (from m in db.Menus
                          where m.Delete_flag == false
                          select m).ToListAsync();
            return await result;
        }
    }
}
