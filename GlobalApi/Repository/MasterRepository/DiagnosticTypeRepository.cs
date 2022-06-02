using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DiagnosticTypeRepository : IDiagnosticType
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DiagnosticTypeRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<DiagnosticType> InsertDiagnosticType(DiagnosticType lead)
        {
            try
            {
                var duplicate = await db.DiagnosticType.FirstOrDefaultAsync(x => x.Type == lead.Type);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("DiagnosticType");
                    DiagnosticType obj = new DiagnosticType()
                    {
                        Id = id,
                        Type = lead.Type,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.DiagnosticType.AddAsync(obj);
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
        public async Task<DiagnosticType> UpdateDiagnosticType(DiagnosticType lead)
        {
            try
            {
                var result = await db.DiagnosticType.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.Type = lead.Type;
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
        public async Task<List<GetAllDiagnoType>> GetAllDiagnosticType()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DiagnosticType
                                 join b in db.Status on a.status equals b.sts_id
                                 orderby a.Id descending
                                 select new GetAllDiagnoType
                                 {
                                     Id =a.Id,
                                     Type = a.Type,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = b.sts_name,
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
        public async Task<List<DiagnoType_DD>> GetDiagnosticType_DD()
        {
            if (db != null)
            {
                var query = (from a in db.DiagnosticType
                             where a.delete_flag == false && a.status != 6 && a.Id != 0
                             select new DiagnoType_DD
                             {
                                 Id = a.Id,
                                 Type = a.Type,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<DiagnosticType> DeleteDiagnosticType(int Id)
        {
            try
            {
                var result = await db.DiagnosticType.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
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
