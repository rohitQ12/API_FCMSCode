using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class SymptomsRepository : ISymptoms
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public SymptomsRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }
        public async Task<string> InsertSymptoms(List<Symptoms> lead , int Appt_Id)
        {
            try
            {
                foreach(Symptoms sym in lead)
                {
                    var duplicate = await db.Symptoms.FirstOrDefaultAsync(x => x.SYM_MST_Id_FK == sym.SYM_MST_Id_FK && x.SYM_APPT_Id_FK == Appt_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("Symptoms");
                        Symptoms obj = new Symptoms()
                        {
                            SYM_Id = id,
                            SYM_MST_Id_FK = sym.SYM_MST_Id_FK,
                            SYM_APPT_Id_FK = Appt_Id,
                            Remarks = sym.Remarks,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                        };
                        var result = await db.Symptoms.AddAsync(obj);
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
        public async Task<Symptoms> UpdateSymptoms(Symptoms lead)
        {
            try
            {
                var result = await db.Symptoms.FirstOrDefaultAsync(x => x.SYM_Id == lead.SYM_Id);
                if (result != null)
                {
                    result.SYM_Id = lead.SYM_Id;
                    result.SYM_MST_Id_FK = lead.SYM_MST_Id_FK;
                    result.SYM_APPT_Id_FK = lead.SYM_APPT_Id_FK;
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
        public async Task<List<GetAllSymptoms>> GetAllSymptoms()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Symptoms
                                 join b in db.PatientAppointment on a.SYM_APPT_Id_FK equals b.Appt_Id
                                 orderby a.SYM_Id descending
                                 select new GetAllSymptoms
                                 {
                                     SYM_Id = a.SYM_Id,
                                     SYM_MST_Id_FK = a.SYM_MST_Id_FK,
                                     SYM_APPT_Id_FK = a.SYM_APPT_Id_FK,
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
        public async Task<Symptoms> DeleteSymptoms(int SYM_Id)
        {
            try
            {
                var result = await db.Symptoms.FirstOrDefaultAsync(x => x.SYM_Id == SYM_Id);
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
        public async Task<SymptomsBy_Id> GetSymptomsById(int SYM_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Symptoms
                             join b in db.PatientAppointment on a.SYM_APPT_Id_FK equals b.Appt_Id
                             where a.SYM_Id == SYM_Id
                             select new SymptomsBy_Id
                             {
                                 SYM_Id = a.SYM_Id,
                                 SYM_MST_Id_FK = a.SYM_MST_Id_FK,
                                 SYM_APPT_Id_FK = a.SYM_APPT_Id_FK,
                                 Remarks = a.Remarks,
                                 delete_flag = a.delete_flag,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
