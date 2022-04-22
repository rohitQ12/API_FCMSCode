using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.AdminRepository
{
    public class OfficesRepository: IOfficesRepository
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public OfficesRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<Offices> InsertOffice(Offices offices)
        {
            try
            {
                int id = await primarykeyvalue.primary_key("Offices");
                Offices obj = new Offices()
                {
                    Id = id,
                    OfficeName = offices.OfficeName,
                    Off_Level = offices.Off_Level,
                    Off_Address1 = offices.Off_Address1,
                    Off_District_Id_Fk = offices.Off_District_Id_Fk,
                    Off_Address2 = offices.Off_Address2,
                    Off_Email= offices.Off_Email,
                    Off_PhoneNumber= offices.Off_PhoneNumber,
                    Off_Landline= offices.Off_Landline,
                    Inactive= offices.Inactive,
                    Off_UserId= offices.Off_UserId,
                    Off_TS= offices.Off_TS,
                    Off_LastEdited_UserId= offices.Off_LastEdited_UserId,
                    Off_LastEdited_TS= offices.Off_LastEdited_TS,
                    Off_OfficerName= offices.Off_OfficerName,
                    Off_Designation= offices.Off_Designation,
                    //app_submenu_id = lead.app_submenu_id,
                    Created_by = 1,
                    Created_date = DateTime.Now,
                    Delete_flag = false,
                    Status = 1
                };
                var result = await db.Office.AddAsync(obj);
                await db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<OfficeRoles> AddOfficeRoles(string userid,int OfficeId)
        {
            try
            {
                int id = await primarykeyvalue.primary_key("OfficeRoles");
                OfficeRoles obj = new OfficeRoles()
                {
                    Id = id,
                    RoleId = userid,
                    OfficeId = OfficeId,
                };
                var result = await db.OfficeRoles.AddAsync(obj);
                await db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Offices> UpdateOffice(Offices offices)
        {
            try
            {
                var result = await db.Office.FirstOrDefaultAsync(x => x.Id == offices.Id);
                if (result != null)
                {
                    result.OfficeName = offices.OfficeName;
                    result.Off_Level = offices.Off_Level;
                    result.Off_Address1 = offices.Off_Address1;
                    result.Off_District_Id_Fk = offices.Off_District_Id_Fk;
                    result.Off_Address2 = offices.Off_Address2;
                    result.Off_Email = offices.Off_Email;
                    result.Off_PhoneNumber = offices.Off_PhoneNumber;
                    result.Off_Landline = offices.Off_Landline;
                    result.Inactive = offices.Inactive;
                    result.Off_UserId = offices.Off_UserId;
                    result.Off_TS = offices.Off_TS;
                    result.Off_LastEdited_UserId = offices.Off_LastEdited_UserId;
                    result.Off_LastEdited_TS = offices.Off_LastEdited_TS;
                    result.Off_OfficerName = offices.Off_OfficerName;
                    result.Off_Designation = offices.Off_Designation;
                    //app_submenu_id = lead.app_submenu_id,
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
        
        //public async Task<List<SubMenuPage>> GetAllAppPage()
        //{
        //    try
        //    {
        //        if (db != null)
        //        {
        //            var query = (from a in db.SubMenu
        //                         from b in db.Page
        //                         where a.app_submenu_id == b.app_submenu_id && b.status == 1 && b.delete_flag == false
        //                         select new SubMenuPage
        //                         {
        //                             app_submenu_id = a.app_submenu_id,
        //                             app_submenu_name = a.app_submenu_name,
        //                             app_page_id = b.app_page_id,
        //                             app_page_name = b.app_page_name,
        //                             page_name = b.page_name,
        //                             app_page_icon_title = b.app_page_icon_title,
        //                             app_page_image_URL = b.app_page_image_URL,
        //                             app_page_ridirect_URL = b.app_page_ridirect_URL,
        //                             app_page_icon_id = b.app_page_icon_id,
        //                             app_page_displayorder = b.app_page_displayorder
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
        public async Task<Offices> DeleteOffice(int Id)
        {
            try
            {
                var result = await db.Office.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
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

        public async Task<Offices> GetOfficeById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.Office
                             where a.Id == Id && a.Status == 1 && a.Delete_flag == false
                             select new Offices
                             {
                                 Id = a.Id,
                                 OfficeName = a.OfficeName,
                                 Off_Level = a.Off_Level,
                                 Off_Address1 = a.Off_Address1,
                                 Off_District_Id_Fk = a.Off_District_Id_Fk,
                                 Off_Address2 = a.Off_Address2,
                                 Off_Email = a.Off_Email,
                                 Off_PhoneNumber = a.Off_PhoneNumber,
                                 Off_Landline = a.Off_Landline,
                                 Inactive = a.Inactive,
                                 Off_UserId = a.Off_UserId,
                                 Off_TS = a.Off_TS,
                                 Off_LastEdited_UserId = a.Off_LastEdited_UserId,
                                 Off_LastEdited_TS = a.Off_LastEdited_TS,
                                 Off_OfficerName = a.Off_OfficerName,
                                 Off_Designation = a.Off_Designation,
                                 //app_submenu_id = lead.app_submenu_id,
                                 Delete_flag = a.Delete_flag,
                                 Status = a.Status
                                 //app_submenu_id = lead.app_submenu_id
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<Offices>> GetOffice()
        {
            var result = (from P in db.Office select P).ToListAsync();
            return await result;
        }
    }
}
