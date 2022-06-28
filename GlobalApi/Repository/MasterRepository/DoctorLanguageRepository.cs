using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DoctorLanguageRepository : IDoctorLanguage
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DoctorLanguageRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertDoctorLanguage(int[] Doclang, int DO_Id)
        {
            try
            {
                foreach (var dl in Doclang)
                {
                    var duplicate = await db.DoctorLanguage.FirstOrDefaultAsync(x => x.DO_Id == DO_Id && x.Lang_Id_FK == dl);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("DoctorLanguage");
                        DoctorLanguage obj = new DoctorLanguage()
                        {
                            Id = id,
                            DO_Id = DO_Id,
                            Lang_Id_FK = dl,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                            status = 1,
                        };
                        var result = await db.DoctorLanguage.AddAsync(obj);
                        await db.SaveChangesAsync();
                    }
                    else
                        return "Data already inserted";
                }
                return "Record insert successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //public async Task<DoctorLanguage> UpdateDoctorLanguage(DoctorLanguage lead)
        //{
        //    try
        //    {

        //        var result = await db.DoctorLanguage.FirstOrDefaultAsync(x => x.DO_Id == lead.DO_Id);
        //        if (result != null)
        //        {
        //            result.Id = lead.Id;
        //            result.DO_Id = lead.DO_Id;
        //            result.Lang_Id_FK = lead.Lang_Id_FK;
        //            result.modified_by = 1;
        //            result.modified_date = DateTime.Now;
        //            result.delete_flag = false;
        //            result.status = 2;
        //            await db.SaveChangesAsync();
        //            return result;
        //        }
        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        public async Task<bool> UpdateDoctorLanguage(List<DoctorLanguage> lead, int DO_Id)
        {
            try
            {
                List<DoctorLanguage> AlreadyExistsDoctorLanguage = await GetExistsDoctorLanguage(DO_Id);

                if (AlreadyExistsDoctorLanguage.Count > lead.Count)
                {
                    foreach (var d in AlreadyExistsDoctorLanguage)
                    {
                        if (!lead.Any(x => x.Lang_Id_FK == d.Lang_Id_FK))
                        {
                            //Delete
                            var result = await db.DoctorLanguage.FirstOrDefaultAsync(x => x.Lang_Id_FK == d.Lang_Id_FK && x.DO_Id == DO_Id);
                            if (result != null)
                            {
                                var removedoclang = db.DoctorLanguage.Remove(result);
                                await db.SaveChangesAsync();
                            }

                            //Insert
                            foreach (var a in lead)
                            {
                                var result1 = await db.DoctorLanguage.FirstOrDefaultAsync(x => x.Lang_Id_FK == a.Lang_Id_FK && x.DO_Id == DO_Id);
                                if (result1 == null)
                                {
                                    int id = await primarykeyvalue.primary_key("DoctorLanguage");
                                    DoctorLanguage obj = new DoctorLanguage()
                                    {
                                        Id = id,
                                        DO_Id = DO_Id,
                                        Lang_Id_FK = a.Lang_Id_FK,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                        status = 1,
                                    };
                                    var result_ = await db.DoctorLanguage.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }

                            }

                        }

                        else
                        {
                            var result = await db.DoctorLanguage.FirstOrDefaultAsync(x => x.Id == d.Id);
                            if (result != null)
                            {
                                //result.Id = d.Id;
                                result.DO_Id = DO_Id;
                                result.Lang_Id_FK = d.Lang_Id_FK;
                                result.modified_by = 2;
                                result.modified_date = DateTime.Now;
                                result.delete_flag = false;
                                result.status = 2;
                                await db.SaveChangesAsync();
                                //return result;
                            }
                        }

                    }

                    return true;
                }
                else if (AlreadyExistsDoctorLanguage.Count <= lead.Count)
                {
                    foreach (var d in lead)
                    {
                        //Update
                        if (AlreadyExistsDoctorLanguage.Any(x => x.Lang_Id_FK == d.Lang_Id_FK))
                        {
                            var result = await db.DoctorLanguage.FirstOrDefaultAsync(x => x.Id == d.Id);
                            if (result != null)
                            {
                                //result.Id = d.Id;
                                result.DO_Id = DO_Id;
                                result.Lang_Id_FK = d.Lang_Id_FK;
                                result.modified_by = 2;
                                result.modified_date = DateTime.Now;
                                result.delete_flag = false;
                                result.status = 2;
                                await db.SaveChangesAsync();
                                //return result;
                            }
                        }

                        //Delete and Insert
                        else if (!AlreadyExistsDoctorLanguage.Any(x => x.Lang_Id_FK == d.Lang_Id_FK && x.DO_Id == DO_Id))
                        {
                            //Delete
                            foreach (var a in AlreadyExistsDoctorLanguage)
                            {
                                if (!lead.Any(x => x.Lang_Id_FK == a.Lang_Id_FK))
                                {
                                    var result = await db.DoctorLanguage.FirstOrDefaultAsync(x => x.Lang_Id_FK == a.Lang_Id_FK && x.DO_Id == DO_Id);
                                    if (result != null)
                                    {
                                        var removedoclang = db.DoctorLanguage.Remove(result);
                                        await db.SaveChangesAsync();
                                    }

                                }

                            }

                            //Insert
                            int id = await primarykeyvalue.primary_key("DoctorLanguage");
                            DoctorLanguage obj = new DoctorLanguage()
                            {
                                Id = id,
                                DO_Id = DO_Id,
                                Lang_Id_FK = d.Lang_Id_FK,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                                status = 1,
                            };
                            var result_ = await db.DoctorLanguage.AddAsync(obj);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            int id = await primarykeyvalue.primary_key("DoctorLanguage");
                            DoctorLanguage obj = new DoctorLanguage()
                            {
                                Id = id,
                                DO_Id = DO_Id,
                                Lang_Id_FK = d.Lang_Id_FK,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                                status = 1,
                            };
                            var result = await db.DoctorLanguage.AddAsync(obj);
                            await db.SaveChangesAsync();
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<List<DoctorLanguage>> GetExistsDoctorLanguage(int DO_Id)
        {
            try
            {
                var result = await (from d in db.DoctorLanguage
                                    where d.DO_Id == DO_Id
                                    select new DoctorLanguage()
                                    {
                                        Id = d.Id,
                                        Lang_Id_FK = d.Lang_Id_FK,

                                    }).ToListAsync();
                return result;
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
                                 join b in db.Doctor on a.DO_Id equals b.DO_Id
                                 join c in db.Language on a.Lang_Id_FK equals c.Id
                                 orderby a.Id descending
                                 select new GetDoctorlang
                                 {
                                     Id = a.Id,
                                     DO_Id = a.DO_Id,
                                     DO_Name = string.Concat(b.DO_FirstName, b.DO_LastName),
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
        public async Task<GetDoctorlang> GetDoctorLanguageById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.DoctorLanguage
                             join b in db.Doctor on a.DO_Id equals b.DO_Id
                             join c in db.Language on a.Lang_Id_FK equals c.Id
                             where a.Id == Id
                             select new GetDoctorlang
                             {
                                 Id = a.Id,
                                 DO_Id = a.DO_Id,
                                 DO_Name = string.Concat(b.DO_FirstName, b.DO_LastName),
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
                             where a.delete_flag == false && a.status != 6 && a.Id != 0
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
