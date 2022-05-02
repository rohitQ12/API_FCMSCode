using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DiseasesDtlRepository : IDiseasesDtl
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DiseasesDtlRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertDiseasesDtl(List<DiseasesDtl> lead , int Appt_Id)
        {
            try
            {
                foreach(DiseasesDtl ddtl in lead)
                {
                    var duplicate = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Dis_Id_FK == ddtl.Dis_Id_FK && x.Ddtl_APPT_Id_FK == Appt_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("DiseasesDtl");
                        DiseasesDtl obj = new DiseasesDtl()
                        {
                            Ddtl_Id = id,
                            Dis_Id_FK = ddtl.Dis_Id_FK,
                            Ddtl_APPT_Id_FK = Appt_Id,
                            Remarks = ddtl.Remarks,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                        };
                        var result = await db.DiseasesDtl.AddAsync(obj);
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
        //public async Task<DiseasesDtl> UpdateDiseasesDtl(DiseasesDtl lead)
        //{
        //    try
        //    {
        //        var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Ddtl_Id == lead.Ddtl_Id);
        //        if (result != null)
        //        {
        //            result.Ddtl_Id = lead.Ddtl_Id;
        //            result.Dis_Id_FK = lead.Dis_Id_FK;
        //            result.Ddtl_APPT_Id_FK = lead.Ddtl_APPT_Id_FK;
        //            result.Remarks = lead.Remarks;
        //            result.modified_by = 1;
        //            result.modified_date = DateTime.Now;
        //            result.delete_flag = false;
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
        public async Task<bool> UpdateDiseasesDtltest(List<DiseasesDtl> lead, int Appt_Id)
        {
            try
            {
                List<DiseasesDtl> AlreadyExistsDiseases = await GetExistsDiseases(Appt_Id);
                if (AlreadyExistsDiseases.Count > lead.Count)
                {
                    foreach (var d in AlreadyExistsDiseases)
                    {
                        //Delete
                        if (!lead.Any(x => x.Dis_Id_FK == d.Dis_Id_FK))
                        {
                            var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                var removedisease = db.DiseasesDtl.Remove(result);
                                await db.SaveChangesAsync();
                            }
                            //Insert
                            foreach (var a in lead)
                            {
                                var result1 = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Dis_Id_FK == a.Dis_Id_FK && x.Ddtl_APPT_Id_FK == Appt_Id);
                                if (result1 == null)
                                {
                                    int id = await primarykeyvalue.primary_key("DiseasesDtl");
                                    DiseasesDtl obj = new DiseasesDtl()
                                    {
                                        Ddtl_Id = id,
                                        Dis_Id_FK = a.Dis_Id_FK,
                                        Ddtl_APPT_Id_FK = Appt_Id,
                                        Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.DiseasesDtl.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }

                            }

                        }
                        else
                        {
                            var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Dis_Id_FK = d.Dis_Id_FK;
                                result.Ddtl_APPT_Id_FK = Appt_Id;
                                result.Remarks = d.Remarks;
                                result.modified_by = 1;
                                result.modified_date = DateTime.Now;
                                result.delete_flag = false;
                                await db.SaveChangesAsync();
                                //return result;
                            }
                        }

                    }
                    return true;
                }
                else if (AlreadyExistsDiseases.Count <= lead.Count)
                {
                    foreach (var d in lead)
                    {
                        //Update
                        if (AlreadyExistsDiseases.Any(x => x.Dis_Id_FK == d.Dis_Id_FK))
                        {
                            var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Dis_Id_FK = d.Dis_Id_FK;
                                result.Ddtl_APPT_Id_FK = Appt_Id;
                                result.Remarks = d.Remarks;
                                result.modified_by = 1;
                                result.modified_date = DateTime.Now;
                                result.delete_flag = false;
                                await db.SaveChangesAsync();
                                //return result;
                            }
                        }
                        //Delete and Insert
                        else if (!AlreadyExistsDiseases.Any(x => x.Dis_Id_FK == d.Dis_Id_FK && x.Ddtl_APPT_Id_FK == Appt_Id))
                        {
                            //Delete
                            foreach (var a in AlreadyExistsDiseases)
                            {
                                if (!lead.Any(x => x.Dis_Id_FK == a.Dis_Id_FK))
                                {
                                    var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Dis_Id_FK == a.Dis_Id_FK && x.Ddtl_APPT_Id_FK == Appt_Id);
                                    if (result != null)
                                    {
                                        var removediseases = db.DiseasesDtl.Remove(result);
                                        await db.SaveChangesAsync();
                                    }

                                }

                            }
                            //Insert
                            int id = await primarykeyvalue.primary_key("DiseasesDtl");
                            DiseasesDtl obj = new DiseasesDtl()
                            {
                                Ddtl_Id = id,
                                Dis_Id_FK = d.Dis_Id_FK,
                                Ddtl_APPT_Id_FK = Appt_Id,
                                Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result_ = await db.DiseasesDtl.AddAsync(obj);
                            await db.SaveChangesAsync();
                        }

                        else
                        {
                            int id = await primarykeyvalue.primary_key("DiseasesDtl");
                            DiseasesDtl obj = new DiseasesDtl()
                            {
                                Ddtl_Id = id,
                                Dis_Id_FK = d.Dis_Id_FK,
                                Ddtl_APPT_Id_FK = Appt_Id,
                                Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result = await db.DiseasesDtl.AddAsync(obj);
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

        public async Task<List<GetAllDiseasesDtl>> GetAllDiseasesDtl()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DiseasesDtl
                                 join b in db.PatientAppointment on a.Ddtl_APPT_Id_FK equals b.Appt_Id
                                 join c in db.Diseases on a.Dis_Id_FK equals c.Id
                                 orderby a.Ddtl_Id descending
                                 select new GetAllDiseasesDtl
                                 {
                                     Ddtl_Id = a.Ddtl_Id,
                                     Dis_Id_FK = a.Dis_Id_FK,
                                     Dis_Name = c.Diseases_Name,
                                     Ddtl_APPT_Id_FK = a.Ddtl_APPT_Id_FK,
                                     Remarks = a.Remarks,
                                     delete_flag = a.delete_flag,
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
        public async Task<List<DiseasesDtl>> GetExistsDiseases(int Appt_Id)
        {
            try
            {
                var result = await (from d in db.DiseasesDtl
                                    where d.Ddtl_APPT_Id_FK == Appt_Id
                                    select new DiseasesDtl()
                                    {
                                        Ddtl_Id = d.Ddtl_Id,
                                        Dis_Id_FK = d.Dis_Id_FK

                                    }).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<DiseasesDtl> DeleteDiseasesDtl(int Ddtl_Id)
        {
            try
            {
                var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Ddtl_Id == Ddtl_Id);
                if (result != null)
                {
                    result.Ddtl_Id = Ddtl_Id;
                    result.delete_flag = true;
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
        public async Task<List<GetDiseaseDtlById>> GetDiseasesDtlById(int Ddtl_PR_Id_FK)
        {
            if (db != null)
            {
                var query = (from a in db.DiseasesDtl
                             join b in db.PatientAppointment on a.Ddtl_APPT_Id_FK equals b.Appt_Id
                             join c in db.Diseases on a.Dis_Id_FK equals c.Id
                             where b.Appt_PatientId_FK == Ddtl_PR_Id_FK
                             orderby a.Ddtl_Id descending
                             select new GetDiseaseDtlById
                             {
                                 Ddtl_Id = a.Ddtl_Id,
                                 Dis_Id_FK = a.Dis_Id_FK,
                                 Dis_Name = c.Diseases_Name,
                                 Ddtl_APPT_Id_FK = a.Ddtl_APPT_Id_FK,
                                 Remarks = a.Remarks,
                                 delete_flag = a.delete_flag,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
