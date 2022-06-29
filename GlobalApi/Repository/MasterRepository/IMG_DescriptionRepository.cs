using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class IMG_DescriptionRepository : IIMG_Description
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public IMG_DescriptionRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<IMG_Description> InsertIMG_Description(IMG_Description lead)
        {
            try
            {
                var duplicate = await db.IMG_Description.FirstOrDefaultAsync(x => x.Img_Invt_Id == lead.Img_Invt_Id && x.Img_SubInvt_Id == lead.Img_SubInvt_Id);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("IMG_Description");
                    IMG_Description obj = new IMG_Description()
                    {
                        Img_DescId = id,
                        Img_Invt_Id = lead.Img_Invt_Id,
                        Img_SubInvt_Id = lead.Img_SubInvt_Id,
                        Img_Description = lead.Img_Description,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.IMG_Description.AddAsync(obj);
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
        public async Task<IMG_Description> UpdateIMG_Description(IMG_Description lead)
        {
            try
            {
                var result = await db.IMG_Description.FirstOrDefaultAsync(x => x.Img_DescId == lead.Img_DescId);
                if (result != null)
                {
                    //result.Img_DescId = lead.Img_DescId;
                    result.Img_Invt_Id = lead.Img_Invt_Id;
                    result.Img_SubInvt_Id = lead.Img_SubInvt_Id;
                    result.Img_Description = lead.Img_Description;
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
        public async Task<List<GetAllIMG_Desc>> GetAllIMG_Description()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.IMG_Description
                                 join b in db.IMG_INVESTIGATIONS on a.Img_Invt_Id equals b.Id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.IMG_SUBINVESTIGATIONS on a.Img_SubInvt_Id equals c.Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 orderby a.Img_DescId descending
                                 select new GetAllIMG_Desc
                                 {
                                     Img_DescId = a.Img_DescId,
                                     Img_Invt_Id = a.Img_Invt_Id,
                                     Category = b.Category,
                                     Img_SubInvt_Id = a.Img_SubInvt_Id,
                                     Sub_Category = c.Sub_Category,
                                     Img_Description = a.Img_Description,
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
        public async Task<List<Img_Desc_DD>> GetImgDesc_DD()
        {
            if (db != null)
            {
                var query = (from a in db.IMG_Description
                             where a.delete_flag == false && a.status != 6
                             && a.Img_DescId != 0
                             select new Img_Desc_DD
                             {
                                 Img_DescId = a.Img_DescId,
                                 Img_Description = a.Img_Description,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<IMG_Description> DeleteIMG_Description(int Img_DescId)
        {
            try
            {
                var result = await db.IMG_Description.FirstOrDefaultAsync(x => x.Img_DescId == Img_DescId);
                if (result != null)
                {
                    result.Img_DescId = Img_DescId;
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
        public async Task<GetImgDesc_ById> GetImgDesc_ById(int Img_DescId)
        {
            if (db != null)
            {
                var query = (from a in db.IMG_Description
                             join b in db.IMG_INVESTIGATIONS on a.Img_Invt_Id equals b.Id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.IMG_SUBINVESTIGATIONS on a.Img_SubInvt_Id equals c.Id into clist
                             from c in clist.DefaultIfEmpty()
                             where a.Img_DescId == Img_DescId
                             select new GetImgDesc_ById
                             {
                                 Img_DescId = a.Img_DescId,
                                 Img_Invt_Id = a.Img_Invt_Id,
                                 Category = b.Category,
                                 Img_SubInvt_Id = a.Img_SubInvt_Id,
                                 Sub_Category = c.Sub_Category,
                                 Img_Description = a.Img_Description,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
