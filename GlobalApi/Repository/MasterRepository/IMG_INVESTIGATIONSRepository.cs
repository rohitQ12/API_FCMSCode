                      using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class IMG_INVESTIGATIONSRepository : IIMG_INVESTIGATIONS
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public IMG_INVESTIGATIONSRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<IMG_INVESTIGATIONS> InsertIMG_INVESTIGATIONS(IMG_INVESTIGATIONS lead)
        {
            try
            {
                var duplicate = await db.IMG_INVESTIGATIONS.FirstOrDefaultAsync(x => x.Category == lead.Category);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("IMG_INVESTIGATIONS");
                    IMG_INVESTIGATIONS obj = new IMG_INVESTIGATIONS()
                    {
                        Id = id,
                        Category = lead.Category,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.IMG_INVESTIGATIONS.AddAsync(obj);
                    await db.SaveChangesAsync();
                    return result.Entity;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IMG_INVESTIGATIONS> UpdateIMG_INVESTIGATIONS(IMG_INVESTIGATIONS lead)
        {
            try
            {
                var result = await db.IMG_INVESTIGATIONS.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.Category = lead.Category;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
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
        public async Task<List<IMG_INVESTIGATIONS>> GetIMG_INVESTIGATIONS()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.IMG_INVESTIGATIONS
                                 orderby a.Id descending
                                 select a);
                    return await query.ToListAsync();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<ImgInsv_DD>> GetImgInsv_DD()
        {
            if (db != null)
            {
                var query = (from a in db.IMG_INVESTIGATIONS
                             where a.delete_flag == false && a.status != 6 && a.Id != 0
                             select new ImgInsv_DD
                             {
                                 Img_Invst_Id = a.Id,
                                 Category = a.Category,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<IMG_INVESTIGATIONS> DeleteIMG_INVESTIGATIONS(int Id)
        {
            try
            {
                var result = await db.IMG_INVESTIGATIONS.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
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
        public async Task<ImgInsvBy_Id> GetImgInsvBy_Id(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.IMG_INVESTIGATIONS
                             where a.Id == Id
                             select new ImgInsvBy_Id
                             {
                                 Id = a.Id,
                                 Category = a.Category,
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
