using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class ComplaintRepository : IComplaint
    {
        GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public ComplaintRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }
        public async Task<string> InsertComplaint(List<Complaint> lead , int Appt_Id)
        {
            try
            {
                foreach (Complaint cpt in lead)
                {
                    var duplicate = await db.Complaint.FirstOrDefaultAsync(x => x.CPT_MST_Id_FK == cpt.CPT_MST_Id_FK && x.CPT_APPT_Id_FK == Appt_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("Complaint");
                        Complaint obj = new Complaint()
                        {
                            CPT_Id = id,
                            CPT_MST_Id_FK = cpt.CPT_MST_Id_FK,
                            CPT_APPT_Id_FK = Appt_Id,
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
        public async Task<Complaint> UpdateComplaint(Complaint lead)
        {
            try
            {
                var result = await db.Complaint.FirstOrDefaultAsync(x => x.CPT_Id == lead.CPT_Id);
                if (result != null)
                {
                    result.CPT_Id = lead.CPT_Id;
                    result.CPT_MST_Id_FK = lead.CPT_MST_Id_FK;
                    result.CPT_APPT_Id_FK = lead.CPT_APPT_Id_FK;
                    result.Remarks = lead.Remarks;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
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
        public async Task<List<GetAllComplaint>> GetAllComplaint()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Complaint
                                 join b in db.PatientAppointment on a.CPT_APPT_Id_FK equals b.Appt_Id
                                 join c in db.ComplaintMst on a.CPT_MST_Id_FK equals c.Cmst_Id
                                 orderby a.CPT_Id descending
                                 select new GetAllComplaint
                                 {
                                     CPT_Id = a.CPT_Id,
                                     CPT_MST_Id_FK = a.CPT_MST_Id_FK,
                                     CPT_MST_Name = c.Cmst_Name,
                                     CPT_APPT_Id_FK = a.CPT_APPT_Id_FK,
                                     CPT_APPT_PR_Id_FK = b.Appt_PatientId_FK,
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
        public async Task<ComplaintBy_Id> GetComplaintById(int CPT_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Complaint
                             join b in db.PatientAppointment on a.CPT_APPT_Id_FK equals b.Appt_Id
                             where a.CPT_Id == CPT_Id
                             select new ComplaintBy_Id
                             {
                                 CPT_Id = a.CPT_Id,
                                 CPT_MST_Id_FK = a.CPT_MST_Id_FK,
                                 CPT_APPT_Id_FK = a.CPT_APPT_Id_FK,
                                 Remarks = a.Remarks,
                                 delete_flag = a.delete_flag,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
