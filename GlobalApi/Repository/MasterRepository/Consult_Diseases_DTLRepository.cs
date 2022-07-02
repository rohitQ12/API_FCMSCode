using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Consult_Diseases_DTLRepository : IConsult_Diseases_DTL
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Consult_Diseases_DTLRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<bool> UpdateConsult_Diseases_DTL(List<Consult_Diseases_DTL> lead, int CON_Id)
        {
            try
            {
                List<Consult_Diseases_DTL> AlreadyExistsDiseases = await GetExistsConsult_Diseases_DTL(CON_Id);
                if (AlreadyExistsDiseases.Count > lead.Count)
                {
                    foreach (var d in AlreadyExistsDiseases)
                    {
                        //Delete
                        if (!lead.Any(x => x.Id == d.Id))
                        {
                            var result = await db.Consult_Diseases_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                var removedisease = db.Consult_Diseases_DTL.Remove(result);
                                await db.SaveChangesAsync();
                            }
                            //Insert
                            foreach (var a in lead)
                            {
                                var result1 = await db.Consult_Diseases_DTL.FirstOrDefaultAsync(x => x.Id == a.Id && x.CON_Id == CON_Id);
                                if (result1 == null)
                                {
                                    int id = await primarykeyvalue.primary_key("Consult_Diseases_DTL");
                                    Consult_Diseases_DTL obj = new Consult_Diseases_DTL()
                                    {
                                        Ddtl_Id = id,
                                        Id = a.Id,
                                        CON_Id = CON_Id,
                                        //Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.Consult_Diseases_DTL.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }

                            }

                        }
                        else
                        {
                            var result = await db.Consult_Diseases_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Id = d.Id;
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
                        if (AlreadyExistsDiseases.Any(x => x.Id == d.Id))
                        {
                            var result = await db.Consult_Diseases_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == d.Ddtl_Id);
                            if (result != null)
                            {
                                //result.Ddtl_Id = d.Ddtl_Id;
                                result.Id = d.Id;
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
                        else if (!AlreadyExistsDiseases.Any(x => x.Id == d.Id && x.CON_Id == CON_Id))
                        {
                            //Delete
                            foreach (var a in AlreadyExistsDiseases)
                            {
                                if (!lead.Any(x => x.Id == a.Id))
                                {
                                    var result = await db.Consult_Diseases_DTL.FirstOrDefaultAsync(x => x.Id == a.Id && x.CON_Id == CON_Id);
                                    if (result != null)
                                    {
                                        var removediseases = db.Consult_Diseases_DTL.Remove(result);
                                        await db.SaveChangesAsync();
                                    }

                                }

                            }
                            //Insert
                            int id = await primarykeyvalue.primary_key("Consult_Diseases_DTL");
                            Consult_Diseases_DTL obj = new Consult_Diseases_DTL()
                            {
                                Ddtl_Id = id,
                                Id = d.Id,
                                CON_Id = CON_Id,
                                //Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result_ = await db.Consult_Diseases_DTL.AddAsync(obj);
                            await db.SaveChangesAsync();
                        }

                        else
                        {
                            int id = await primarykeyvalue.primary_key("Consult_Diseases_DTL");
                            Consult_Diseases_DTL obj = new Consult_Diseases_DTL()
                            {
                                Ddtl_Id = id,
                                Id = d.Id,
                                CON_Id = CON_Id,
                                //Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result = await db.Consult_Diseases_DTL.AddAsync(obj);
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
        public async Task<List<GetAllCDDtl>> GetAllConsult_Diseases_DTL()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consult_Diseases_DTL
                                 join c in db.Diseases on a.Id equals c.Id
                                 orderby a.Ddtl_Id descending
                                 select new GetAllCDDtl
                                 {
                                     Ddtl_Id = a.Ddtl_Id,
                                     Id = a.Id,
                                     Diseases_Name = c.Diseases_Name,
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
        public async Task<List<GetAllCons_Diseases>> GetAllCons_Diseases()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consult_Diseases_DTL
                                 join c in db.Diseases on a.Id equals c.Id
                                 orderby a.Ddtl_Id descending
                                 select new GetAllCons_Diseases
                                 {
                                     Id = a.Id,
                                     Diseases_Code = c.Diseases_Code,
                                     Acronyms = c.Acronyms,
                                     Diseases_Name = c.Diseases_Name,
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

        public async Task<List<Consult_Diseases_DTL>> GetExistsConsult_Diseases_DTL(int CON_Id)
        {
            try
            {
                var result = await (from d in db.Consult_Diseases_DTL
                                    where d.CON_Id == CON_Id
                                    select new Consult_Diseases_DTL()
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
        public async Task<Consult_Diseases_DTL> DeleteConsult_Diseases_DTL(int Ddtl_Id)
        {
            try
            {
                var result = await db.Consult_Diseases_DTL.FirstOrDefaultAsync(x => x.Ddtl_Id == Ddtl_Id);
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
        public async Task<List<GetCDDtlById>> GetConsult_Diseases_DTLById(int CON_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Consult_Diseases_DTL
                             //join b in db.Consultation on a.CON_Id equals b.CON_Id
                             join c in db.Diseases on a.Id equals c.Id
                             where a.CON_Id == CON_Id
                             orderby a.Ddtl_Id descending
                             select new GetCDDtlById
                             {
                                 Ddtl_Id = a.Ddtl_Id,
                                 Id = a.Id,
                                 Diseases_Name = c.Diseases_Name,
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
