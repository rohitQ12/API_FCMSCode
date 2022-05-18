using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DiagnoCategoryRepository : IDiagnoCategory
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DiagnoCategoryRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<DiagnoCategory> InsertDiagnoCategory(DiagnoCategory lead)
        {
            try
            {
                var duplicate = await db.DiagnoCategory.FirstOrDefaultAsync(x => x.name == lead.name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("DiagnoCategory");
                    DiagnoCategory obj = new DiagnoCategory()
                    {
                        id = id,
                        name = lead.name,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.DiagnoCategory.AddAsync(obj);
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
        public async Task<DiagnoCategory> UpdateDiagnoCategory(DiagnoCategory lead)
        {
            try
            {
                var result = await db.DiagnoCategory.FirstOrDefaultAsync(x => x.id == lead.id);
                if (result != null)
                {
                    result.id = lead.id;
                    result.name = lead.name;
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
        public async Task<List<DiagnoCategory>> GetAllDiagnoCategory()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DiagnoCategory
                                 orderby a.id descending
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
        public async Task<List<Diagno_DD>> GetDiagnoCategory_DD()
        {
            if (db != null)
            {
                var query = (from a in db.DiagnoCategory
                             where a.delete_flag == false && a.status == 1
                             select new Diagno_DD
                             {
                                 id = a.id,
                                 name = a.name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<DiagnoCategory> DeleteDiagnoCategory(int Id)
        {
            try
            {
                var result = await db.DiagnoCategory.FirstOrDefaultAsync(x => x.id == Id);
                if (result != null)
                {
                    result.id = Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 3;
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
