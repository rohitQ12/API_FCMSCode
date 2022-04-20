using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class IMG_SUBINVESTIGATIONSRepository : IIMG_SUBINVESTIGATIONS
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public IMG_SUBINVESTIGATIONSRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<IMG_SUBINVESTIGATIONS> InsertIMG_SUBINVESTIGATIONS(IMG_SUBINVESTIGATIONS lead)
        {
            try
            {
                var duplicate = await db.IMG_SUBINVESTIGATIONS.FirstOrDefaultAsync(x => x.Sub_Category == lead.Sub_Category);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("IMG_SUBINVESTIGATIONS");
                    IMG_SUBINVESTIGATIONS obj = new IMG_SUBINVESTIGATIONS()
                    {
                        Id = id,
                        Img_Invt_Id = lead.Img_Invt_Id,
                        Sub_Category = lead.Sub_Category,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.IMG_SUBINVESTIGATIONS.AddAsync(obj);
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
        public async Task<IMG_SUBINVESTIGATIONS> UpdateIMG_SUBINVESTIGATIONS(IMG_SUBINVESTIGATIONS lead)
        {
            try
            {
                var result = await db.IMG_SUBINVESTIGATIONS.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.Img_Invt_Id = lead.Img_Invt_Id;
                    result.Sub_Category = lead.Sub_Category;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 1;
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
        public async Task<List<GetImgSubInsv>> GetIMG_SUBINVESTIGATIONS()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.IMG_SUBINVESTIGATIONS
                                 join b in db.IMG_INVESTIGATIONS on a.Img_Invt_Id equals b.Id
                                 orderby a.Id descending
                                 select new GetImgSubInsv
                                 {
                                     Id = a.Id,
                                     Img_Invt_Id = a.Img_Invt_Id,
                                     Category = b.Category,
                                     Sub_Category = a.Sub_Category,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
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
        public async Task<List<ImgSubInsv_DD>> GetImgSubInsv_DD(int Img_Invt_Id)
        {
            if (db != null)
            {
                var query = (from a in db.IMG_SUBINVESTIGATIONS
                             where a.Img_Invt_Id == Img_Invt_Id && a.delete_flag == false && a.status == 1
                             select new ImgSubInsv_DD
                             {
                                 Img_SubInvst_Id = a.Id,
                                 Sub_Category = a.Sub_Category,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<IMG_SUBINVESTIGATIONS> DeleteIMG_SUBINVESTIGATIONS(int Id)
        {
            try
            {
                var result = await db.IMG_SUBINVESTIGATIONS.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
                    result.delete_flag = true;
                    result.status = 0;
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
        public async Task<ImgSubInsvBy_Id> GetImgSubInsvBy_Id(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.IMG_SUBINVESTIGATIONS
                             join b in db.IMG_INVESTIGATIONS on a.Img_Invt_Id equals b.Id
                             where a.Id == Id
                             select new ImgSubInsvBy_Id
                             {
                                 Id = a.Id,
                                 Img_Invt_Id = a.Img_Invt_Id,
                                 Category = b.Category,
                                 Sub_Category = a.Sub_Category,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
