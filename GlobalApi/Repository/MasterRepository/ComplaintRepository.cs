using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class ComplaintRepository : IComplaint
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public ComplaintRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertComplaint(List<Complaint> lead , int Appt_Id)
        {
            try
            {
                foreach (Complaint cpt in lead)
                {
                    var duplicate = await db.Complaint.FirstOrDefaultAsync(x => x.Cmst_Id == cpt.Cmst_Id && x.Appt_Id == Appt_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("Complaint");
                        Complaint obj = new Complaint()
                        {
                            CPT_Id = id,
                            Cmst_Id = cpt.Cmst_Id,
                            Appt_Id = Appt_Id,
                            Remarks = cpt.Remarks,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                        };
                        var result = await db.Complaint.AddAsync(obj);
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
        public async Task<string> InsertPHCComplaint(List<Complaint> lead, int Appt_Id)
        {
            try
            {
                foreach (Complaint cpt in lead)
                {
                    var duplicate = await db.Complaint.FirstOrDefaultAsync(x => x.Cmst_Id == cpt.Cmst_Id && x.Phc_Appt_Id == Appt_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("Complaint");
                        Complaint obj = new Complaint()
                        {
                            CPT_Id = id,
                            Cmst_Id = cpt.Cmst_Id,
                            Phc_Appt_Id = Appt_Id,
                            Remarks = cpt.Remarks,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                        };
                        var result = await db.Complaint.AddAsync(obj);
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


        public async Task<bool> UpdateComplainttest(List<Complaint> lead, int Appt_Id)
        {
            try
            {
                List<Complaint> AlreadyExistsComplaint = await GetExistsComplaint(Appt_Id);

                if (AlreadyExistsComplaint.Count > lead.Count)
                {
                    foreach (var d in AlreadyExistsComplaint)
                    {
                        if (!lead.Any(x => x.Cmst_Id == d.Cmst_Id))
                        {
                            //Delete
                            var result = await db.Complaint.FirstOrDefaultAsync(x => x.Cmst_Id == d.Cmst_Id && x.Appt_Id == Appt_Id);
                            if (result != null)
                            {
                                var removecomplaint = db.Complaint.Remove(result);
                                await db.SaveChangesAsync();
                            }
                            //Insert
                            foreach (var a in lead)
                            {
                                 var result1 = await db.Complaint.FirstOrDefaultAsync(x => x.Cmst_Id == a.Cmst_Id && x.Appt_Id == Appt_Id);
                                 if (result1 == null)
                                 {
                                     int id = await primarykeyvalue.primary_key("Complaint");
                                     Complaint obj = new Complaint()
                                     {
                                            CPT_Id = id,
                                            Cmst_Id = a.Cmst_Id,
                                            Appt_Id = Appt_Id,
                                            Remarks = a.Remarks,
                                            created_by = 1,
                                            created_date = DateTime.Now,
                                            delete_flag = false,
                                     };
                                     var result_ = await db.Complaint.AddAsync(obj);
                                     await db.SaveChangesAsync();
                                 }

                            }
                            
                        }

                        else
                        {
                            var result = await db.Complaint.FirstOrDefaultAsync(x => x.CPT_Id == d.CPT_Id);
                            if (result != null)
                            {
                                //result.CPT_Id = d.CPT_Id;
                                result.Cmst_Id = d.Cmst_Id;
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
                else if(AlreadyExistsComplaint.Count <= lead.Count)
                {
                    foreach (var d in lead)
                    {
                        //Update
                        if (AlreadyExistsComplaint.Any(x => x.Cmst_Id == d.Cmst_Id))
                        {
                            var result = await db.Complaint.FirstOrDefaultAsync(x => x.CPT_Id == d.CPT_Id);
                            if (result != null)
                            {
                                //result.CPT_Id = d.CPT_Id;
                                result.Cmst_Id = d.Cmst_Id;
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
                        else if(!AlreadyExistsComplaint.Any(x => x.Cmst_Id == d.Cmst_Id && x.Appt_Id == Appt_Id))
                        {
                            //Delete
                            foreach(var a in AlreadyExistsComplaint)
                            {
                                if (!lead.Any(x => x.Cmst_Id == a.Cmst_Id))
                                {
                                    var result = await db.Complaint.FirstOrDefaultAsync(x => x.Cmst_Id == a.Cmst_Id && x.Appt_Id== Appt_Id);
                                    if (result != null)
                                    {
                                        var removecomplaint = db.Complaint.Remove(result);
                                        await db.SaveChangesAsync();
                                    }
                                    
                                }
                                   
                            }
                            //Insert
                            int id = await primarykeyvalue.primary_key("Complaint");
                            Complaint obj = new Complaint()
                            {
                                CPT_Id = id,
                                Cmst_Id = d.Cmst_Id,
                                Appt_Id = Appt_Id,
                                Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result_ = await db.Complaint.AddAsync(obj);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            int id = await primarykeyvalue.primary_key("Complaint");
                            Complaint obj = new Complaint()
                            {
                                CPT_Id = id,
                                Cmst_Id = d.Cmst_Id,
                                Appt_Id = Appt_Id,
                                Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result = await db.Complaint.AddAsync(obj);
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
        public async Task<bool> UpdatePHCComplaint(List<Complaint> lead, int Appt_Id)
        {
            try
            {
                List<Complaint> AlreadyExistsComplaint = await GetExistsPHCComplaint(Appt_Id);

                if (AlreadyExistsComplaint.Count > lead.Count)
                {
                    foreach (var d in AlreadyExistsComplaint)
                    {
                        if (!lead.Any(x => x.Cmst_Id == d.Cmst_Id))
                        {
                            //Delete
                            var result = await db.Complaint.FirstOrDefaultAsync(x => x.Cmst_Id == d.Cmst_Id && x.Phc_Appt_Id == Appt_Id);
                            if (result != null)
                            {
                                var removecomplaint = db.Complaint.Remove(result);
                                await db.SaveChangesAsync();
                            }
                            //Insert
                            foreach (var a in lead)
                            {
                                var result1 = await db.Complaint.FirstOrDefaultAsync(x => x.Cmst_Id == a.Cmst_Id && x.Phc_Appt_Id == Appt_Id);
                                if (result1 == null)
                                {
                                    int id = await primarykeyvalue.primary_key("Complaint");
                                    Complaint obj = new Complaint()
                                    {
                                        CPT_Id = id,
                                        Cmst_Id = a.Cmst_Id,
                                        Phc_Appt_Id = Appt_Id,
                                        Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.Complaint.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }

                            }

                        }

                        else
                        {
                            var result = await db.Complaint.FirstOrDefaultAsync(x => x.CPT_Id == d.CPT_Id);
                            if (result != null)
                            {
                                //result.CPT_Id = d.CPT_Id;
                                result.Cmst_Id = d.Cmst_Id;
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
                else if (AlreadyExistsComplaint.Count <= lead.Count)
                {
                    foreach (var d in lead)
                    {
                        //Update
                        if (AlreadyExistsComplaint.Any(x => x.Cmst_Id == d.Cmst_Id))
                        {
                            var result = await db.Complaint.FirstOrDefaultAsync(x => x.CPT_Id == d.CPT_Id);
                            if (result != null)
                            {
                                //result.CPT_Id = d.CPT_Id;
                                result.Cmst_Id = d.Cmst_Id;
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
                        else if (!AlreadyExistsComplaint.Any(x => x.Cmst_Id == d.Cmst_Id && x.Phc_Appt_Id == Appt_Id))
                        {
                            //Delete
                            foreach (var a in AlreadyExistsComplaint)
                            {
                                if (!lead.Any(x => x.Cmst_Id == a.Cmst_Id))
                                {
                                    var result = await db.Complaint.FirstOrDefaultAsync(x => x.Cmst_Id == a.Cmst_Id && x.Phc_Appt_Id == Appt_Id);
                                    if (result != null)
                                    {
                                        var removecomplaint = db.Complaint.Remove(result);
                                        await db.SaveChangesAsync();
                                    }

                                }

                            }
                            //Insert
                            int id = await primarykeyvalue.primary_key("Complaint");
                            Complaint obj = new Complaint()
                            {
                                CPT_Id = id,
                                Cmst_Id = d.Cmst_Id,
                                Phc_Appt_Id = Appt_Id,
                                Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result_ = await db.Complaint.AddAsync(obj);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            int id = await primarykeyvalue.primary_key("Complaint");
                            Complaint obj = new Complaint()
                            {
                                CPT_Id = id,
                                Cmst_Id = d.Cmst_Id,
                                Phc_Appt_Id = Appt_Id,
                                Remarks = d.Remarks,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var result = await db.Complaint.AddAsync(obj);
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

        public async Task<List<GetAllComplaint>> GetAllComplaint()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Complaint
                                 join c in db.ComplaintMst on a.Cmst_Id equals c.Cmst_Id
                                 orderby a.CPT_Id descending
                                 select new GetAllComplaint
                                 {
                                     //CPT_Id = a.CPT_Id,
                                     Cmst_Id = a.Cmst_Id,
                                     Cmst_Code = c.Cmst_Code,
                                     Cmst_Name = c.Cmst_Name,
                                     //Appt_Id = a.Appt_Id,
                                     //CPT_APPT_PR_Id_FK = b.Appt_PatientId_FK,
                                     //Remarks = a.Remarks,
                                     //delete_flag = a.delete_flag,
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
        public async Task<List<GetAllComplaint>> GetAllPHCComplaint()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Complaint
                                 join c in db.ComplaintMst on a.Cmst_Id equals c.Cmst_Id
                                 orderby a.CPT_Id descending
                                 select new GetAllComplaint
                                 {
                                     //CPT_Id = a.CPT_Id,
                                     Cmst_Id = a.Cmst_Id,
                                     Cmst_Code = c.Cmst_Code,
                                     Cmst_Name = c.Cmst_Name,
                                     //Phc_Appt_Id = a.Phc_Appt_Id,
                                     //CPT_APPT_PR_Id_FK = b.Appt_PatientId_FK,
                                     //Remarks = a.Remarks,
                                     //delete_flag = a.delete_flag,
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

        public async Task<List<Complaint>> GetExistsComplaint(int Appt_Id)
        {
            try
            {
                var result =await (from d in db.Complaint
                              where d.Appt_Id == Appt_Id
                              select new Complaint()
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
        public async Task<List<Complaint>> GetExistsPHCComplaint(int Appt_Id)
        {
            try
            {
                var result = await (from d in db.Complaint
                                    where d.Phc_Appt_Id == Appt_Id
                                    select new Complaint()
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

        public async Task<Complaint> DeleteComplaint(int CPT_Id)
        {
            try
            {
                var result = await db.Complaint.FirstOrDefaultAsync(x => x.CPT_Id == CPT_Id);
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
        public async Task<List<ComplaintBy_Id>> GetComplaintById(int CPT_PR_Id_FK)
        {
            if (db != null)
            {
                var query = (from a in db.Complaint
                             join b in db.PatientAppointment on a.Appt_Id equals b.Appt_Id
                             where b.Appt_PatientId_FK == CPT_PR_Id_FK
                             orderby a.CPT_Id descending
                             select new ComplaintBy_Id
                             {
                                 CPT_Id = a.CPT_Id,
                                 Cmst_Id = a.Cmst_Id,
                                 Appt_Id = a.Appt_Id,
                                 //CPT_APPT_PR_Id_FK = b.Appt_PatientId_FK,
                                 Remarks = a.Remarks,
                                 delete_flag = a.delete_flag,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
