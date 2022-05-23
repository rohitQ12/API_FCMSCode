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
                    var duplicate = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Id == ddtl.Id && x.Appt_Id == Appt_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("DiseasesDtl");
                        DiseasesDtl obj = new DiseasesDtl()
                        {
                            Ddtl_Id = id,
                            Id = ddtl.Id,
                            Appt_Id = Appt_Id,
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
        public async Task<string> InsertManualDiseasesDtl(List<DiseasesDtl> lead, int MAppt_Id)
        {
            try
            {
                foreach (DiseasesDtl ddtl in lead)
                {
                    var duplicate = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Id == ddtl.Id && x.MAppt_Id == MAppt_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("DiseasesDtl");
                        DiseasesDtl obj = new DiseasesDtl()
                        {
                            Ddtl_Id = id,
                            Id = ddtl.Id,
                            MAppt_Id = MAppt_Id,
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
                        if (!lead.Any(x => x.Id == d.Id))
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
                                var result1 = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Id == a.Id && x.Appt_Id == Appt_Id);
                                if (result1 == null)
                                {
                                    int id = await primarykeyvalue.primary_key("DiseasesDtl");
                                    DiseasesDtl obj = new DiseasesDtl()
                                    {
                                        Ddtl_Id = id,
                                        Id = a.Id,
                                        Appt_Id = Appt_Id,
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
                                result.Id = d.Id;
                                result.Appt_Id = Appt_Id;
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
                        if (AlreadyExistsDiseases.Any(x => x.Id == d.Id))
                        {
                            var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Id = d.Id;
                                result.Appt_Id = Appt_Id;
                                result.Remarks = d.Remarks;
                                result.modified_by = 1;
                                result.modified_date = DateTime.Now;
                                result.delete_flag = false;
                                await db.SaveChangesAsync();
                                //return result;
                            }
                        }
                        //Delete and Insert
                        else if (!AlreadyExistsDiseases.Any(x => x.Id == d.Id && x.Appt_Id == Appt_Id))
                        {
                            //Delete
                            foreach (var a in AlreadyExistsDiseases)
                            {
                                if (!lead.Any(x => x.Id == a.Id))
                                {
                                    var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Id == a.Id && x.Appt_Id == Appt_Id);
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
                                Id = d.Id,
                                Appt_Id = Appt_Id,
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
                                Id = d.Id,
                                Appt_Id = Appt_Id,
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
        public async Task<bool> UpdateManualDiseasesDtl(List<DiseasesDtl> lead, int MAppt_Id)
        {
            try
            {
                List<DiseasesDtl> AlreadyExistsDiseases = await GetExistsManualDiseases(MAppt_Id);
                if (AlreadyExistsDiseases.Count > lead.Count)
                {
                    foreach (var d in AlreadyExistsDiseases)
                    {
                        //Delete
                        if (!lead.Any(x => x.Id == d.Id))
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
                                var result1 = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Id == a.Id && x.MAppt_Id == MAppt_Id);
                                if (result1 == null)
                                {
                                    int id = await primarykeyvalue.primary_key("DiseasesDtl");
                                    DiseasesDtl obj = new DiseasesDtl()
                                    {
                                        Ddtl_Id = id,
                                        Id = a.Id,
                                        MAppt_Id = MAppt_Id,
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
                                result.Id = d.Id;
                                result.MAppt_Id = MAppt_Id;
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
                        if (AlreadyExistsDiseases.Any(x => x.Id == d.Id))
                        {
                            var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Id = d.Id;
                                result.MAppt_Id = MAppt_Id;
                                result.Remarks = d.Remarks;
                                result.modified_by = 1;
                                result.modified_date = DateTime.Now;
                                result.delete_flag = false;
                                await db.SaveChangesAsync();
                                //return result;
                            }
                        }
                        //Delete and Insert
                        else if (!AlreadyExistsDiseases.Any(x => x.Id == d.Id && x.MAppt_Id == MAppt_Id))
                        {
                            //Delete
                            foreach (var a in AlreadyExistsDiseases)
                            {
                                if (!lead.Any(x => x.Id == a.Id))
                                {
                                    var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Id == a.Id && x.MAppt_Id == MAppt_Id);
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
                                Id = d.Id,
                                MAppt_Id = MAppt_Id,
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
                                Id = d.Id,
                                MAppt_Id = MAppt_Id,
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
                                 //join b in db.PatientAppointment on a.Appt_Id equals b.Appt_Id
                                 join c in db.Diseases on a.Id equals c.Id
                                 orderby a.Ddtl_Id descending
                                 select new GetAllDiseasesDtl
                                 {
                                     Ddtl_Id = a.Ddtl_Id,
                                     Id = a.Id,
                                     Diseases_Name = c.Diseases_Name,
                                     Appt_Id = a.Appt_Id,
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
        public async Task<List<GetAllDiseasesDtl>> GetAllManualDiseasesDtl()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DiseasesDtl
                                     //join b in db.PatientAppointment on a.Appt_Id equals b.Appt_Id
                                 join c in db.Diseases on a.Id equals c.Id
                                 orderby a.Ddtl_Id descending
                                 select new GetAllDiseasesDtl
                                 {
                                     Ddtl_Id = a.Ddtl_Id,
                                     Id = a.Id,
                                     Diseases_Name = c.Diseases_Name,
                                     MAppt_Id = a.MAppt_Id,
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
                                    where d.Appt_Id == Appt_Id
                                    select new DiseasesDtl()
                                    {
                                        Ddtl_Id = d.Ddtl_Id,
                                        Id = d.Id

                                    }).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<DiseasesDtl>> GetExistsManualDiseases(int MAppt_Id)
        {
            try
            {
                var result = await (from d in db.DiseasesDtl
                                    where d.MAppt_Id == MAppt_Id
                                    select new DiseasesDtl()
                                    {
                                        Ddtl_Id = d.Ddtl_Id,
                                        Id = d.Id

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
                             join b in db.PatientAppointment on a.Appt_Id equals b.Appt_Id
                             join c in db.Diseases on a.Id equals c.Id
                             where b.Appt_PatientId_FK == Ddtl_PR_Id_FK
                             orderby a.Ddtl_Id descending
                             select new GetDiseaseDtlById
                             {
                                 Ddtl_Id = a.Ddtl_Id,
                                 Id = a.Id,
                                 Diseases_Name = c.Diseases_Name,
                                 Appt_Id = a.Appt_Id,
                                 Remarks = a.Remarks,
                                 delete_flag = a.delete_flag,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
