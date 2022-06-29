using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class AllergySigns_DTLRepository : IAllergySigns_DTL
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public AllergySigns_DTLRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertAllergySigns_DTL(List<AllergySigns_DTL> lead, int Appt_Id)
        {
            try
            {
                foreach (AllergySigns_DTL ddtl in lead)
                {
                    var duplicate = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Al_Id == ddtl.Al_Id && x.Appt_Id == Appt_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("AllergySigns_DTL");
                        AllergySigns_DTL obj = new AllergySigns_DTL()
                        {
                            Ddtl_Id = id,
                            Al_Id = ddtl.Al_Id,
                            Appt_Id = Appt_Id,
                            Remarks = ddtl.Remarks,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                        };
                        var result = await db.AllergySigns_DTL.AddAsync(obj);
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
        public async Task<string> InsertPHCAllergySigns_DTL(List<AllergySigns_DTL> lead, int Appt_Id)
        {
            try
            {
                foreach (AllergySigns_DTL ddtl in lead)
                {
                    var duplicate = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Al_Id == ddtl.Al_Id && x.Phc_Appt_Id == Appt_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("AllergySigns_DTL");
                        AllergySigns_DTL obj = new AllergySigns_DTL()
                        {
                            Ddtl_Id = id,
                            Al_Id = ddtl.Al_Id,
                            Phc_Appt_Id = Appt_Id,
                            Remarks = ddtl.Remarks,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                        };
                        var result = await db.AllergySigns_DTL.AddAsync(obj);
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

        public async Task<bool> UpdateAllergySigns_DTLtest(List<AllergySigns_DTL> lead, int Appt_Id)
        {
            try
            {
                List<AllergySigns_DTL> AlreadyExistsDiseases = await GetExistsAllergySigns(Appt_Id);
                if (AlreadyExistsDiseases.Count > lead.Count)
                {
                    foreach (var d in AlreadyExistsDiseases)
                    {
                        //Delete
                        if (!lead.Any(x => x.Al_Id == d.Al_Id))
                        {
                            var result = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                var removedisease = db.AllergySigns_DTL.Remove(result);
                                await db.SaveChangesAsync();
                            }
                            //Insert
                            foreach (var a in lead)
                            {
                                var result1 = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Al_Id == a.Al_Id && x.Appt_Id == Appt_Id);
                                if (result1 == null)
                                {
                                    int id = await primarykeyvalue.primary_key("AllergySigns_DTL");
                                    AllergySigns_DTL obj = new AllergySigns_DTL()
                                    {
                                        Ddtl_Id = id,
                                        Al_Id = a.Al_Id,
                                        Appt_Id = Appt_Id,
                                        Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.AllergySigns_DTL.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }

                            }

                        }
                        else
                        {
                            var result = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Al_Id = d.Al_Id;
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
                        if (AlreadyExistsDiseases.Any(x => x.Al_Id == d.Al_Id))
                        {
                            var result = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Al_Id = d.Al_Id;
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
                        else if (!AlreadyExistsDiseases.Any(x => x.Al_Id == d.Al_Id && x.Appt_Id == Appt_Id))
                        {
                            //Delete
                            foreach (var a in AlreadyExistsDiseases)
                            {
                                if (!lead.Any(x => x.Al_Id == a.Al_Id))
                                {
                                    var result = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Al_Id == a.Al_Id && x.Appt_Id == Appt_Id);
                                    if (result != null)
                                    {
                                        var removediseases = db.AllergySigns_DTL.Remove(result);
                                        await db.SaveChangesAsync();
                                    }

                                }

                            }
                            //Insert
                            int id = await primarykeyvalue.primary_key("AllergySigns_DTL");
                            AllergySigns_DTL obj = new AllergySigns_DTL()
                            {
                                Ddtl_Id = id,
                                Al_Id = d.Al_Id,
                                Appt_Id = Appt_Id,
                                Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result_ = await db.AllergySigns_DTL.AddAsync(obj);
                            await db.SaveChangesAsync();
                        }

                        else
                        {
                            int id = await primarykeyvalue.primary_key("AllergySigns_DTL");
                            AllergySigns_DTL obj = new AllergySigns_DTL()
                            {
                                Ddtl_Id = id,
                                Al_Id = d.Al_Id,
                                Appt_Id = Appt_Id,
                                Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result = await db.AllergySigns_DTL.AddAsync(obj);
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
        public async Task<bool> UpdatePHCAllergySigns_DTL(List<AllergySigns_DTL> lead, int Appt_Id)
        {
            try
            {
                List<AllergySigns_DTL> AlreadyExistsPHCAllergySigns = await GetExistsPHCAllergySigns(Appt_Id);
                if (AlreadyExistsPHCAllergySigns.Count > lead.Count)
                {
                    foreach (var d in AlreadyExistsPHCAllergySigns)
                    {
                        //Delete
                        if (!lead.Any(x => x.Al_Id == d.Al_Id))
                        {
                            var result = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                var removedisease = db.AllergySigns_DTL.Remove(result);
                                await db.SaveChangesAsync();
                            }
                            //Insert
                            foreach (var a in lead)
                            {
                                var result1 = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Al_Id == a.Al_Id && x.Phc_Appt_Id == Appt_Id);
                                if (result1 == null)
                                {
                                    int id = await primarykeyvalue.primary_key("AllergySigns_DTL");
                                    AllergySigns_DTL obj = new AllergySigns_DTL()
                                    {
                                        Ddtl_Id = id,
                                        Al_Id = a.Al_Id,
                                        Phc_Appt_Id = Appt_Id,
                                        Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.AllergySigns_DTL.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }

                            }

                        }
                        else
                        {
                            var result = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Al_Id = d.Al_Id;
                                result.Phc_Appt_Id = Appt_Id;
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
                else if (AlreadyExistsPHCAllergySigns.Count <= lead.Count)
                {
                    foreach (var d in lead)
                    {
                        //Update
                        if (AlreadyExistsPHCAllergySigns.Any(x => x.Al_Id == d.Al_Id))
                        {
                            var result = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Al_Id = d.Al_Id;
                                result.Phc_Appt_Id = Appt_Id;
                                result.Remarks = d.Remarks;
                                result.modified_by = 1;
                                result.modified_date = DateTime.Now;
                                result.delete_flag = false;
                                await db.SaveChangesAsync();
                                //return result;
                            }
                        }
                        //Delete and Insert
                        else if (!AlreadyExistsPHCAllergySigns.Any(x => x.Al_Id == d.Al_Id && x.Phc_Appt_Id == Appt_Id))
                        {
                            //Delete
                            foreach (var a in AlreadyExistsPHCAllergySigns)
                            {
                                if (!lead.Any(x => x.Al_Id == a.Al_Id))
                                {
                                    var result = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Al_Id == a.Al_Id && x.Phc_Appt_Id == Appt_Id);
                                    if (result != null)
                                    {
                                        var removediseases = db.AllergySigns_DTL.Remove(result);
                                        await db.SaveChangesAsync();
                                    }

                                }

                            }
                            //Insert
                            int id = await primarykeyvalue.primary_key("AllergySigns_DTL");
                            AllergySigns_DTL obj = new AllergySigns_DTL()
                            {
                                Ddtl_Id = id,
                                Al_Id = d.Al_Id,
                                Phc_Appt_Id = Appt_Id,
                                Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result_ = await db.AllergySigns_DTL.AddAsync(obj);
                            await db.SaveChangesAsync();
                        }

                        else
                        {
                            int id = await primarykeyvalue.primary_key("AllergySigns_DTL");
                            AllergySigns_DTL obj = new AllergySigns_DTL()
                            {
                                Ddtl_Id = id,
                                Al_Id = d.Al_Id,
                                Phc_Appt_Id = Appt_Id,
                                Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result = await db.AllergySigns_DTL.AddAsync(obj);
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


        public async Task<List<GetAllAllergySigns_DTL>> GetAllAllergySigns_DTL()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.AllergySigns_DTL
                                     //join b in db.PatientAppointment on a.Appt_Id equals b.Appt_Id
                                 join c in db.AllergySigns on a.Al_Id equals c.Al_Id
                                 orderby a.Ddtl_Id descending
                                 select new GetAllAllergySigns_DTL
                                 {
                                     Ddtl_Id = a.Ddtl_Id,
                                     Al_Id = a.Al_Id,
                                     Al_Name = c.Al_Name,
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
        public async Task<List<GetAllAllergySigns_DTL>> GetAllPHCAllergySigns_DTL()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.AllergySigns_DTL
                                     //join b in db.PatientAppointment on a.Appt_Id equals b.Appt_Id
                                 join c in db.AllergySigns on a.Al_Id equals c.Al_Id
                                 orderby a.Ddtl_Id descending
                                 select new GetAllAllergySigns_DTL
                                 {
                                     Ddtl_Id = a.Ddtl_Id,
                                     Al_Id = a.Al_Id,
                                     Al_Name = c.Al_Name,
                                     Phc_Appt_Id = a.Phc_Appt_Id,
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

        public async Task<List<AllergySigns_DTL>> GetExistsAllergySigns(int Appt_Id)
        {
            try
            {
                var result = await (from d in db.AllergySigns_DTL
                                    where d.Appt_Id == Appt_Id
                                    select new AllergySigns_DTL()
                                    {
                                        Ddtl_Id = d.Ddtl_Id,
                                        Al_Id = d.Al_Id

                                    }).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<AllergySigns_DTL>> GetExistsPHCAllergySigns(int Appt_Id)
        {
            try
            {
                var result = await (from d in db.AllergySigns_DTL
                                    where d.Phc_Appt_Id == Appt_Id
                                    select new AllergySigns_DTL()
                                    {
                                        Ddtl_Id = d.Ddtl_Id,
                                        Al_Id = d.Al_Id

                                    }).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AllergySigns_DTL> DeleteAllergySigns_DTL(int Ddtl_Id)
        {
            try
            {
                var result = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == Ddtl_Id);
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
        public async Task<List<GetAllergySigns_DTLById>> GetAllergySigns_DTLById(int Ddtl_PR_Id_FK)
        {
            if (db != null)
            {
                var query = (from a in db.AllergySigns_DTL
                             join b in db.PatientAppointment on a.Appt_Id equals b.Appt_Id
                             join c in db.AllergySigns on a.Al_Id equals c.Al_Id
                             where b.Appt_PatientId_FK == Ddtl_PR_Id_FK
                             orderby a.Ddtl_Id descending
                             select new GetAllergySigns_DTLById
                             {
                                 Ddtl_Id = a.Ddtl_Id,
                                 Al_Id = a.Al_Id,
                                 Al_Name = c.Al_Name,
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
