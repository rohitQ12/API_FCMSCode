using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class SymptomsMstRepository : ISymptomsMst
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public SymptomsMstRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }
        public async Task<SymptomsMst> InsertSymptomsMst(SymptomsMst lead)
        {
            try
            {
                var duplicate = await db.SymptomsMst.FirstOrDefaultAsync(x => x.Smst_Name == lead.Smst_Name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("SymptomsMst");
                    SymptomsMst obj = new SymptomsMst()
                    {
                        Smst_Id = id,
                        Smst_Code = lead.Smst_Code,
                        Smst_Name = lead.Smst_Name,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.SymptomsMst.AddAsync(obj);
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
        public async Task<SymptomsMst> UpdateSymptomsMst(SymptomsMst lead)
        {
            try
            {
                var result = await db.SymptomsMst.FirstOrDefaultAsync(x => x.Smst_Id == lead.Smst_Id);
                if (result != null)
                {
                    result.Smst_Id = lead.Smst_Id;
                    result.Smst_Code = lead.Smst_Code;
                    result.Smst_Name = lead.Smst_Name;
                    result.modified_by = 1;
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
        public async Task<List<SymptomsMst>> GetAllSymptomsMst()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.SymptomsMst
                                 orderby a.Smst_Id descending
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
        public async Task<List<SymptomsMst_DD>> GetSymptomsMst_DD()
        {
            if (db != null)
            {
                var query = (from a in db.SymptomsMst
                             where a.delete_flag == false && a.status == 1
                             select new SymptomsMst_DD
                             {
                                 Smst_Id = a.Smst_Id,
                                 Smst_Code = a.Smst_Code,
                                 Smst_Name = a.Smst_Name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<SymptomsMst> DeleteSymptomsMst(int Smst_Id)
        {
            try
            {
                var result = await db.SymptomsMst.FirstOrDefaultAsync(x => x.Smst_Id == Smst_Id);
                if (result != null)
                {
                    result.Smst_Id = Smst_Id;
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
        public async Task<SymptomsMst> GetSymptomsMstById(int Smst_Id)
        {
            if (db != null)
            {
                var query = (from a in db.SymptomsMst
                             where a.Smst_Id == Smst_Id
                             select new SymptomsMst
                             {
                                 Smst_Id = a.Smst_Id,
                                 Smst_Code = a.Smst_Code,
                                 Smst_Name = a.Smst_Name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
