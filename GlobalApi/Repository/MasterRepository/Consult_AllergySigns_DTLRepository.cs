using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Consult_AllergySigns_DTLRepository : IConsult_AllergySigns_DTL
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Consult_AllergySigns_DTLRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<bool> UpdateConsult_AllergySigns_DTL(List<Consult_AllergySigns_DTL> lead, int CON_Id)
        {
            try
            {
                List<Consult_AllergySigns_DTL> AlreadyExistsDiseases = await GetExistsAllergySigns(CON_Id);
                if (AlreadyExistsDiseases.Count > lead.Count)
                {
                    foreach (var d in AlreadyExistsDiseases)
                    {
                        //Delete
                        if (!lead.Any(x => x.Al_Id == d.Al_Id))
                        {
                            var result = await db.Consult_AllergySigns_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                var removedisease = db.Consult_AllergySigns_DTL.Remove(result);
                                await db.SaveChangesAsync();
                            }
                            //Insert
                            foreach (var a in lead)
                            {
                                var result1 = await db.Consult_AllergySigns_DTL.FirstOrDefaultAsync(x => x.Al_Id == a.Al_Id && x.CON_Id == CON_Id);
                                if (result1 == null)
                                {
                                    int id = await primarykeyvalue.primary_key("Consult_AllergySigns_DTL");
                                    Consult_AllergySigns_DTL obj = new Consult_AllergySigns_DTL()
                                    {
                                        Ddtl_Id = id,
                                        Al_Id = a.Al_Id,
                                        CON_Id = CON_Id,
                                        //Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.Consult_AllergySigns_DTL.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }

                            }

                        }
                        else
                        {
                            var result = await db.Consult_AllergySigns_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Al_Id = d.Al_Id;
                                result.CON_Id = CON_Id;
                                //result.Remarks = d.Remarks;
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
                            var result = await db.Consult_AllergySigns_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Al_Id = d.Al_Id;
                                result.CON_Id = CON_Id;
                                //result.Remarks = d.Remarks;
                                result.modified_by = 1;
                                result.modified_date = DateTime.Now;
                                result.delete_flag = false;
                                await db.SaveChangesAsync();
                                //return result;
                            }
                        }
                        //Delete and Insert
                        else if (!AlreadyExistsDiseases.Any(x => x.Al_Id == d.Al_Id && x.CON_Id == CON_Id))
                        {
                            //Delete
                            foreach (var a in AlreadyExistsDiseases)
                            {
                                if (!lead.Any(x => x.Al_Id == a.Al_Id))
                                {
                                    var result = await db.Consult_AllergySigns_DTL.FirstOrDefaultAsync(x => x.Al_Id == a.Al_Id && x.CON_Id == CON_Id);
                                    if (result != null)
                                    {
                                        var removediseases = db.Consult_AllergySigns_DTL.Remove(result);
                                        await db.SaveChangesAsync();
                                    }

                                }

                            }
                            //Insert
                            int id = await primarykeyvalue.primary_key("Consult_AllergySigns_DTL");
                            Consult_AllergySigns_DTL obj = new Consult_AllergySigns_DTL()
                            {
                                Ddtl_Id = id,
                                Al_Id = d.Al_Id,
                                CON_Id = CON_Id,
                                //Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result_ = await db.Consult_AllergySigns_DTL.AddAsync(obj);
                            await db.SaveChangesAsync();
                        }

                        else
                        {
                            int id = await primarykeyvalue.primary_key("Consult_AllergySigns_DTL");
                            Consult_AllergySigns_DTL obj = new Consult_AllergySigns_DTL()
                            {
                                Ddtl_Id = id,
                                Al_Id = d.Al_Id,
                                CON_Id = CON_Id,
                                //Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result = await db.Consult_AllergySigns_DTL.AddAsync(obj);
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
        public async Task<List<GetAllCASdtl>> GetAllConsult_AllergySigns_DTL()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consult_AllergySigns_DTL
                                 join c in db.AllergySigns on a.Al_Id equals c.Al_Id
                                 orderby a.Ddtl_Id descending
                                 select new GetAllCASdtl
                                 {
                                     Ddtl_Id = a.Ddtl_Id,
                                     Al_Id = a.Al_Id,
                                     Al_Name = c.Al_Name,
                                     CON_Id = a.CON_Id,
                                     //Remarks = a.Remarks,
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
        public async Task<List<GetAllCons_Allergys>> GetAllCons_Allergys()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consult_AllergySigns_DTL
                                 join c in db.AllergySigns on a.Al_Id equals c.Al_Id
                                 orderby a.Ddtl_Id descending
                                 select new GetAllCons_Allergys
                                 {
                                     Al_Id = a.Al_Id,
                                     Al_Code = c.Al_Code,
                                     Acronyms = c.Acronyms,
                                     Al_Name = c.Al_Name,
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

        public async Task<List<Consult_AllergySigns_DTL>> GetExistsAllergySigns(int CON_Id)
        {
            try
            {
                var result = await (from d in db.Consult_AllergySigns_DTL
                                    where d.CON_Id == CON_Id
                                    select new Consult_AllergySigns_DTL()
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
        public async Task<Consult_AllergySigns_DTL> DeleteConsult_AllergySigns_DTL(int Ddtl_Id)
        {
            try
            {
                var result = await db.Consult_AllergySigns_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == Ddtl_Id);
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
        public async Task<List<GetCASdtlById>> GetConsult_AllergySigns_DTLById(int CON_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Consult_AllergySigns_DTL
                             join c in db.AllergySigns on a.Al_Id equals c.Al_Id
                             where a.CON_Id == CON_Id
                             orderby a.Ddtl_Id descending
                             select new GetCASdtlById
                             {
                                 Ddtl_Id = a.Ddtl_Id,
                                 Al_Id = a.Al_Id,
                                 Al_Name = c.Al_Name,
                                 CON_Id = a.CON_Id,
                                 //Remarks = a.Remarks,
                                 delete_flag = a.delete_flag,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
