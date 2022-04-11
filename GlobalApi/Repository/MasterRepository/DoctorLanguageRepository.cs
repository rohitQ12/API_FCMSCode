using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DoctorLanguageRepository : IDoctorLanguage
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public DoctorLanguageRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }
        public async Task<DoctorLanguage> UpdateDoctorLanguage(DoctorLanguage lead)
        {
            try
            {

                var result = await db.DoctorLanguage.FirstOrDefaultAsync(x => x.doc_Id_FK == lead.doc_Id_FK);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.doc_Id_FK = lead.doc_Id_FK;
                    result.Lang_Id_FK = lead.Lang_Id_FK;
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
        public async Task<List<GetDoctorlang>> GetAllDoctorLanguage()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DoctorLanguage
                                 join b in db.Doctor on a.doc_Id_FK equals b.DO_Id
                                 join c in db.Language on a.Lang_Id_FK equals c.Id
                                 orderby a.Id descending
                                 select new GetDoctorlang
                                 {
                                     Id = a.Id,
                                     doc_Id_FK = a.doc_Id_FK,
                                     doc_name = string.Concat(b.DO_FirstName, b.DO_LastName),
                                     Lang_Id_FK = a.Lang_Id_FK,
                                     lang = c.Languages,
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
        public async Task<DoctorLanguage> DeleteDoctorLanguage(int Id)
        {
            try
            {
                var result = await db.DoctorLanguage.FirstOrDefaultAsync(x => x.Id == Id);
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
        public async Task<GetDoctorlang> GetDoctorLanguageById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.DoctorLanguage
                             join b in db.Doctor on a.doc_Id_FK equals b.DO_Id
                             join c in db.Language on a.Lang_Id_FK equals c.Id
                             where a.Id == Id
                             select new GetDoctorlang
                             {
                                 Id = a.Id,
                                 doc_Id_FK = a.doc_Id_FK,
                                 doc_name = string.Concat(b.DO_FirstName, b.DO_LastName),
                                 Lang_Id_FK = a.Lang_Id_FK,
                                 lang = c.Languages,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<Language_DD>> GetLanguage_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Language
                             where a.delete_flag == false && a.status == 1
                             select new Language_DD
                             {
                                 Id = a.Id,
                                 Languages = a.Languages,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
    }
}
