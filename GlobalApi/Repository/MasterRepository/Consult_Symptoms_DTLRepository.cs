using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Consult_Symptoms_DTLRepository : IConsult_Symptoms_DTL
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Consult_Symptoms_DTLRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<List<Consult_Symptoms_DTL>> GetExistsConsult_Symptoms_DTL(int CON_Id)
        {
            try
            {
                var result = await (from d in db.Consult_Symptoms_DTL
                                    where d.CON_Id == CON_Id
                                    select new Consult_Symptoms_DTL()
                                    {
                                        SYM_Id = d.SYM_Id,
                                        Smst_Id = d.Smst_Id

                                    }).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> UpdateConsult_Symptoms_DTL(List<Consult_Symptoms_DTL> lead, int CON_Id)
        {
            try
            {
                List<Consult_Symptoms_DTL> AlreadyExistsConsult_Symptoms_DTL = await GetExistsConsult_Symptoms_DTL(CON_Id);
                if (AlreadyExistsConsult_Symptoms_DTL.Count > lead.Count)
                {
                    foreach (var d in AlreadyExistsConsult_Symptoms_DTL)
                    {
                        if (!lead.Any(x => x.Smst_Id == d.Smst_Id))
                        {
                            var result = await db.Consult_Symptoms_DTL.FirstOrDefaultAsync(x => x.SYM_Id == d.SYM_Id);
                            if (result != null)
                            {
                                var removeConsult_Symptoms_DTL = db.Consult_Symptoms_DTL.Remove(result);
                                await db.SaveChangesAsync();
                            }
                            //Insert
                            foreach (var a in lead)
                            {
                                var result1 = await db.Consult_Symptoms_DTL.FirstOrDefaultAsync(x => x.Smst_Id == a.Smst_Id && x.CON_Id == CON_Id);
                                if (result1 == null)
                                {
                                    int id = await primarykeyvalue.primary_key("Consult_Symptoms_DTL");
                                    Consult_Symptoms_DTL obj = new Consult_Symptoms_DTL()
                                    {
                                        SYM_Id = id,
                                        Smst_Id = a.Smst_Id,
                                        CON_Id = CON_Id,
                                        //Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.Consult_Symptoms_DTL.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }

                            }

                        }
                        else
                        {
                            var result = await db.Consult_Symptoms_DTL.FirstOrDefaultAsync(x => x.SYM_Id == d.SYM_Id);
                            if (result != null)
                            {
                                //result.CPT_Id = d.CPT_Id;
                                result.Smst_Id = d.Smst_Id;
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
                else if (AlreadyExistsConsult_Symptoms_DTL.Count <= lead.Count)
                {
                    foreach (var d in lead)
                    {
                        if (AlreadyExistsConsult_Symptoms_DTL.Any(x => x.Smst_Id == d.Smst_Id))
                        {
                            var result = await db.Consult_Symptoms_DTL.FirstOrDefaultAsync(x => x.SYM_Id == d.SYM_Id);
                            if (result != null)
                            {
                                //result.CPT_Id = d.CPT_Id;
                                result.Smst_Id = d.Smst_Id;
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
                        else if (!AlreadyExistsConsult_Symptoms_DTL.Any(x => x.Smst_Id == d.Smst_Id && x.CON_Id == CON_Id))
                        {
                            //Delete
                            foreach (var a in AlreadyExistsConsult_Symptoms_DTL)
                            {
                                if (!lead.Any(x => x.Smst_Id == a.Smst_Id))
                                {
                                    var result = await db.Consult_Symptoms_DTL.FirstOrDefaultAsync(x => x.Smst_Id == a.Smst_Id && x.CON_Id == CON_Id);
                                    if (result != null)
                                    {
                                        var removeConsult_Symptoms_DTL = db.Consult_Symptoms_DTL.Remove(result);
                                        await db.SaveChangesAsync();
                                    }

                                }

                            }
                            //Insert
                            int id = await primarykeyvalue.primary_key("Consult_Symptoms_DTL");
                            Consult_Symptoms_DTL obj = new Consult_Symptoms_DTL()
                            {
                                SYM_Id = id,
                                Smst_Id = d.Smst_Id,
                                CON_Id = CON_Id,
                                //Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result_ = await db.Consult_Symptoms_DTL.AddAsync(obj);
                            await db.SaveChangesAsync();
                        }

                        else
                        {
                            int id = await primarykeyvalue.primary_key("Consult_Symptoms_DTL");
                            Consult_Symptoms_DTL obj = new Consult_Symptoms_DTL()
                            {
                                SYM_Id = id,
                                Smst_Id = d.Smst_Id,
                                CON_Id = CON_Id,
                                //Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result = await db.Consult_Symptoms_DTL.AddAsync(obj);
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
        public async Task<List<GetAllCSdtl>> GetAllConsult_Symptoms_DTL()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consult_Symptoms_DTL
                                 join c in db.SymptomsMst on a.Smst_Id equals c.Smst_Id
                                 orderby a.SYM_Id descending
                                 select new GetAllCSdtl
                                 {
                                     SYM_Id = a.SYM_Id,
                                     Smst_Id = a.Smst_Id,
                                     Smst_Name = c.Smst_Name,
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
        public async Task<List<GetAllCons_Symptoms>> GetAllCons_Symptoms()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consult_Symptoms_DTL
                                 join c in db.SymptomsMst on a.Smst_Id equals c.Smst_Id
                                 orderby a.SYM_Id descending
                                 select new GetAllCons_Symptoms
                                 {
                                     Smst_Id = a.Smst_Id,
                                     Smst_Code = c.Smst_Code,
                                     Smst_Name = c.Smst_Name,
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

        public async Task<Consult_Symptoms_DTL> DeleteConsult_Symptoms_DTL(int SYM_Id)
        {
            try
            {
                var result = await db.Consult_Symptoms_DTL.FirstOrDefaultAsync(x => x.SYM_Id == SYM_Id);
                if (result != null)
                {
                    result.SYM_Id = SYM_Id;
                    result.delete_flag = true;
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
        public async Task<List<CSdtlBy_Id>> GetConsult_Symptoms_DTLById(int CON_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Consult_Symptoms_DTL
                             join c in db.SymptomsMst on a.Smst_Id equals c.Smst_Id
                             where a.CON_Id == CON_Id
                             orderby a.SYM_Id descending
                             select new CSdtlBy_Id
                             {
                                 SYM_Id = a.SYM_Id,
                                 Smst_Id = a.Smst_Id,
                                 Smst_Name = c.Smst_Name,
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
