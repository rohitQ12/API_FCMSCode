using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class DiseasesRepository : IDiseases
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DiseasesRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Diseases> InsertDiseases(Diseases lead)
        {
            try
            {
                var duplicate = await db.Diseases.FirstOrDefaultAsync(x => x.Diseases_Name == lead.Diseases_Name || x.Acronyms == lead.Acronyms);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Diseases");
                    Diseases obj = new Diseases()
                    {
                        Id = id,
                        Diseases_Code = lead.Diseases_Code,
                        Diseases_Name = lead.Diseases_Name,
                        Acronyms = lead.Acronyms,
                        Dis_SP_Id_FK = lead.Dis_SP_Id_FK,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Diseases.AddAsync(obj);
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
        public async Task<Diseases> UpdateDiseases(Diseases lead)
        {
            try
            {
                var result = await db.Diseases.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.Diseases_Code = lead.Diseases_Code;
                    result.Diseases_Name = lead.Diseases_Name;
                    result.Acronyms = lead.Acronyms;
                    result.Dis_SP_Id_FK = lead.Dis_SP_Id_FK;
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
        public async Task<List<GetAllDiseases>> GetAllDiseases()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Diseases
                                 join b in db.Status on a.status equals b.sts_id
                                 orderby a.Id descending
                                 select new GetAllDiseases
                                 {
                                     Id = a.Id,
                                     Diseases_Code = a.Diseases_Code,
                                     Diseases_Name = a.Diseases_Name,
                                     Acronyms = a.Acronyms,
                                     Dis_SP_Id_FK = a.Dis_SP_Id_FK,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = b.sts_name
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
        public async Task<List<Diseases_DD>> GetDiseases_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Diseases
                             where a.delete_flag == false && a.status != 6 && a.Id != 0
                             select new Diseases_DD
                             {
                                 Id = a.Id,
                                 Diseases_Code = a.Diseases_Code,
                                 Diseases_Name = a.Diseases_Name,
                                 Acronyms = a.Acronyms,
                                 //Dis_SP_Id_FK = a.Dis_SP_Id_FK,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Diseases> DeleteDiseases(int Id)
        {
            try
            {
                var result = await db.Diseases.FirstOrDefaultAsync(x => x.Id == Id);
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
        public async Task<DiseasesBy_Id> GetDiseasesById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.Diseases
                             join b in db.Status on a.status equals b.sts_id
                             where a.Id == Id
                             select new DiseasesBy_Id
                             {
                                 Id = a.Id,
                                 Diseases_Code = a.Diseases_Code,
                                 Diseases_Name = a.Diseases_Name,
                                 Acronyms = a.Acronyms,
                                 Dis_SP_Id_FK = a.Dis_SP_Id_FK,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = b.sts_name,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
