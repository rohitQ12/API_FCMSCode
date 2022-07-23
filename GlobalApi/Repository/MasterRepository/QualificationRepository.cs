using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class QualificationRepository : IQualification
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public QualificationRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<string> InsertQualification(Qualification lead)
        {
            try
            {
                var qualification_code = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_code == lead.qualification_code);
                var qualification_Name = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_Name == lead.qualification_Name);
                if (qualification_code == null)
                {
                    if (qualification_Name == null)
                    {
                        int id = await primarykeyvalue.primary_key("Qualification");
                        Qualification obj = new Qualification()
                        {
                            qualification_id = id,
                            //qualification_code = "Q" + Convert.ToString(id),
                            qualification_code = lead.qualification_code,
                            qualification_Name = lead.qualification_Name,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.Qualification.AddAsync(obj);
                        await db.SaveChangesAsync();
                        return "Qualification Added Successfully";
                    }
                    return "Qualification Name Already Exists";
                }
                return "Qualification Code Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateQualification(Qualification lead)
        {
            try
            {
                var Qualification = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_id == lead.qualification_id);
                var qualification_code = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_code == lead.qualification_code);
                var qualification_Name = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_Name == lead.qualification_Name);
                if (qualification_code == null || Qualification.qualification_code == lead.qualification_code)
                {
                    if (qualification_Name == null || Qualification.qualification_Name == lead.qualification_Name)
                    {
                        if (Qualification != null)
                        {
                            Qualification.qualification_id = lead.qualification_id;
                            Qualification.qualification_code = lead.qualification_code;
                            Qualification.qualification_Name = lead.qualification_Name;
                            Qualification.modified_by = 1;
                            Qualification.modified_date = DateTime.Now;
                            Qualification.delete_flag = false;
                            Qualification.status = 2;
                            await db.SaveChangesAsync();
                            return "Qualification Updated Successfully";
                        }
                    }
                    return "Qualification Name Already Exists";
                }
                return "Qualification Code Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<GetAllQualification>> GetAllQualification()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Qualification
                                 join b in db.Status on a.status equals b.sts_id
                                 where a.qualification_id != 0 
                                 orderby a.qualification_id descending
                                 select new GetAllQualification
                                 {
                                     qualification_id = a.qualification_id,
                                     qualification_code = a.qualification_code,
                                     qualification_Name = a.qualification_Name,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = b.sts_name,
                                     Remarks = a.Remarks,
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
        public async Task<List<Qualification_DD>> GetQualification_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Qualification
                             where a.delete_flag == false && a.status == 3
                             && a.qualification_id != 0
                             select new Qualification_DD
                             {
                                 qualification_id = a.qualification_id,
                                 qualification_Name = a.qualification_Name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> DeleteQualification(int qualification_id)
        {
            try
            {
                var result = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_id == qualification_id);
                if (result != null)
                {
                    result.qualification_id = qualification_id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Qualification Deleted Successfully";
                }
                return "Qualification Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<QualificationById> GetQualificationById(int qualification_id)
        {
            if (db != null)
            {
                var query = (from a in db.Qualification
                             join b in db.Status on a.status equals b.sts_id
                             where a.qualification_id == qualification_id && a.qualification_id != 0
                             select new QualificationById
                             {
                                 qualification_id = a.qualification_id,
                                 qualification_code = a.qualification_code,
                                 qualification_Name = a.qualification_Name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = b.sts_name,
                                 Remarks = a.Remarks,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> ApproveQualification(ApproveQualification lead)
        {
            try
            {

                    var result = await db.Qualification.Where(x => x.qualification_id == lead.qualification_id).FirstOrDefaultAsync();
                    if (result.status != 3)
                    {
                        //result.qualification_id = lead.qualification_id;
                        result.status = 3;
                        if (lead.Remarks == null)
                        {
                            result.Remarks = "OK";
                        }
                        else
                            result.Remarks = lead.Remarks;
                        await db.SaveChangesAsync();
                    return "Qualification Approved Successfully";
                }
                return "Qualification Details Does Not Exists";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
