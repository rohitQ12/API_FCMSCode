using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Identity_MSTRepository : IIdentity_MST
    {
        private readonly GlobalContext db;
        private readonly IPrimarykeyvalue primarykeyvalue;
        public Identity_MSTRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<Identity_DOC_MST> InsertIdentity_MST(Identity_DOC_MST lead)
        {
            try
            {
                int id = await primarykeyvalue.primary_key("Identity_DOC_MST");
                Identity_DOC_MST obj = new Identity_DOC_MST()
                {
                    Id = lead.Id,
                    DOC_Name = lead.DOC_Name,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Identity_DOC_MST.AddAsync(obj);
                await db.SaveChangesAsync();
                return result.Entity;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Identity_DOC_MST> UpdateIdentity_MST(Identity_DOC_MST lead)
        {
            try
            {
                var result = await db.Identity_DOC_MST.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.DOC_Name = lead.DOC_Name;
                    result.modified_by = 1;
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
        public async Task<List<Identity_DOC_MST>> GetAllIdentity_MST()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Identity_DOC_MST
                                 where a.Id != 0
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
        public async Task<List<IdentityDD>> GetIdentity_MST_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Identity_DOC_MST
                             where a.delete_flag == false && a.status != 6
                             && a.Id != 0
                             select new IdentityDD
                             {
                                 IdentityProof = a.Id,
                                 DOC_Name = a.DOC_Name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

        public async Task<Identity_DOC_MST> DeleteIdentity_MST(int Id)
        {
            try
            {
                var result = await db.Identity_DOC_MST.FirstOrDefaultAsync(x => x.Id == Id);

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

    }
}
