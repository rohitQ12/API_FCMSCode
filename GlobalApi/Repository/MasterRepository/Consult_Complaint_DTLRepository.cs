using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Consult_Complaint_DTLRepository : IConsult_Complaint_DTL
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Consult_Complaint_DTLRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<List<Consult_Complaint_DTL>> GetExistsConsult_Complaint_DTL(int CON_Id)
        {
            try
            {
                var result = await (from d in db.Consult_Complaint_DTL
                                    where d.CON_Id == CON_Id
                                    select new Consult_Complaint_DTL()
                                    {
                                        CPT_Id = d.CPT_Id,
                                        Cmst_Id = d.Cmst_Id

                                    }).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> UpdateConsult_Complaint_DTL(List<Consult_Complaint_DTL> lead, int CON_Id)
        {
            try
            {
                List<Consult_Complaint_DTL> AlreadyExistsComplaint = await GetExistsConsult_Complaint_DTL(CON_Id);

                if (AlreadyExistsComplaint.Count > lead.Count)
                {
                    foreach (var d in AlreadyExistsComplaint)
                    {
                        if (!lead.Any(x => x.Cmst_Id == d.Cmst_Id))
                        {
                            //Delete
                            var result = await db.Consult_Complaint_DTL.FirstOrDefaultAsync(x => x.Cmst_Id == d.Cmst_Id && x.CON_Id == CON_Id);
                            if (result != null)
                            {
                                var removecomplaint = db.Consult_Complaint_DTL.Remove(result);
                                await db.SaveChangesAsync();
                            }
                            //Insert
                            foreach (var a in lead)
                            {
                                var result1 = await db.Consult_Complaint_DTL.FirstOrDefaultAsync(x => x.Cmst_Id == a.Cmst_Id && x.CON_Id == CON_Id);
                                if (result1 == null)
                                {
                                    int id = await primarykeyvalue.primary_key("Consult_Complaint_DTL");
                                    Consult_Complaint_DTL obj = new Consult_Complaint_DTL()
                                    {
                                        CPT_Id = id,
                                        Cmst_Id = a.Cmst_Id,
                                        CON_Id = CON_Id,
                                        //Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.Consult_Complaint_DTL.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }

                            }

                        }

                        else
                        {
                            var result = await db.Consult_Complaint_DTL.FirstOrDefaultAsync(x => x.CPT_Id == d.CPT_Id);
                            if (result != null)
                            {
                                //result.CPT_Id = d.CPT_Id;
                                result.Cmst_Id = d.Cmst_Id;
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
                else if (AlreadyExistsComplaint.Count <= lead.Count)
                {
                    foreach (var d in lead)
                    {
                        //Update
                        if (AlreadyExistsComplaint.Any(x => x.Cmst_Id == d.Cmst_Id))
                        {
                            var result = await db.Consult_Complaint_DTL.FirstOrDefaultAsync(x => x.CPT_Id == d.CPT_Id);
                            if (result != null)
                            {
                                //result.CPT_Id = d.CPT_Id;
                                result.Cmst_Id = d.Cmst_Id;
                                result.CON_Id = CON_Id;
                                //result.Remarks = d.Remarks;
                                result.modified_by = 2;
                                result.modified_date = DateTime.Now;
                                result.delete_flag = false;
                                await db.SaveChangesAsync();
                                //return result;
                            }
                        }
                        //Delete and Insert
                        else if (!AlreadyExistsComplaint.Any(x => x.Cmst_Id == d.Cmst_Id && x.CON_Id == CON_Id))
                        {
                            //Delete
                            foreach (var a in AlreadyExistsComplaint)
                            {
                                if (!lead.Any(x => x.Cmst_Id == a.Cmst_Id))
                                {
                                    var result = await db.Consult_Complaint_DTL.FirstOrDefaultAsync(x => x.Cmst_Id == a.Cmst_Id && x.CON_Id == CON_Id);
                                    if (result != null)
                                    {
                                        var removecomplaint = db.Consult_Complaint_DTL.Remove(result);
                                        await db.SaveChangesAsync();
                                    }

                                }

                            }
                            //Insert
                            int id = await primarykeyvalue.primary_key("Consult_Complaint_DTL");
                            Consult_Complaint_DTL obj = new Consult_Complaint_DTL()
                            {
                                CPT_Id = id,
                                Cmst_Id = d.Cmst_Id,
                                CON_Id = CON_Id,
                                //Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result_ = await db.Consult_Complaint_DTL.AddAsync(obj);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            int id = await primarykeyvalue.primary_key("Consult_Complaint_DTL");
                            Consult_Complaint_DTL obj = new Consult_Complaint_DTL()
                            {
                                CPT_Id = id,
                                Cmst_Id = d.Cmst_Id,
                                CON_Id = CON_Id,
                                //Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result = await db.Consult_Complaint_DTL.AddAsync(obj);
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
        public async Task<List<GetAllCCdtl>> GetAllConsult_Complaint_DTL()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consult_Complaint_DTL
                                 join c in db.ComplaintMst on a.Cmst_Id equals c.Cmst_Id
                                 orderby a.CPT_Id descending
                                 select new GetAllCCdtl
                                 {
                                     CPT_Id = a.CPT_Id,
                                     Cmst_Id = a.Cmst_Id,
                                     Cmst_Name = c.Cmst_Name,
                                     CON_Id = a.CON_Id,
                                     //CPT_APPT_PR_Id_FK = b.Appt_PatientId_FK,
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
        public async Task<Consult_Complaint_DTL> DeleteConsult_Complaint_DTL(int CPT_Id)
        {
            try
            {
                var result = await db.Consult_Complaint_DTL.FirstOrDefaultAsync(x => x.CPT_Id == CPT_Id);
                if (result != null)
                {
                    result.CPT_Id = CPT_Id;
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
        public async Task<List<CCdtlBy_Id>> GetConsult_Complaint_DTLById(int CON_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Consult_Complaint_DTL
                             where a.CON_Id == CON_Id
                             orderby a.CPT_Id descending
                             select new CCdtlBy_Id
                             {
                                 CPT_Id = a.CPT_Id,
                                 Cmst_Id = a.Cmst_Id,
                                 CON_Id = a.CON_Id,
                                 //CPT_APPT_PR_Id_FK = b.Appt_PatientId_FK,
                                 //Remarks = a.Remarks,
                                 delete_flag = a.delete_flag,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
