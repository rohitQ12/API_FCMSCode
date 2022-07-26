using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Consult_diagRepository : IConsult_diag
    {
        private readonly GlobalContext db;
        private readonly IPrimarykeyvalue primarykeyvalue;
        public Consult_diagRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> Insert_Consult_diag(Consulr_diag diagData)
        {
            try
            {
                var checkval = await db.Consult_Diagnosis.FirstOrDefaultAsync(x => x.Con_diag_id == diagData.Con_diag_id);
                if (checkval == null)
                {
                    int id = await primarykeyvalue.primary_key("ConsultDiagnosis");
                    Consulr_diag obj = new Consulr_diag()
                    {
                        Con_diag_id = id,
                        Con_diag_conid_FK = diagData.Con_diag_conid_FK,
                        Con_diag_descrip = diagData.Con_diag_descrip,
                        Con_diag_created_by = "1",
                        Con_diag_created_date = DateTime.Now,
                        Con_diag_delete_flag = false,
                        Status = 1
                    };
                    var result = await db.Consult_Diagnosis.AddAsync(obj);
                    await db.SaveChangesAsync();
                    return "Diagnosis inserted successfully";

                }
                return "Diagnosis already exits";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> Update_Consult_diag(Consulr_diag UpdConDiag)
        {
            try
            {
                var result = await db.Consult_Diagnosis.FirstOrDefaultAsync(x => x.Con_diag_id == UpdConDiag.Con_diag_id);
                if (result != null)
                {
                    result.Con_diag_id = UpdConDiag.Con_diag_id;
                    result.Con_diag_conid_FK = UpdConDiag.Con_diag_conid_FK;
                    result.Con_diag_descrip = UpdConDiag.Con_diag_descrip;
                    result.Con_diag_modified_by = "1";
                    result.Con_diag_modified_date = DateTime.Now;
                    result.Status = 2;
                    result.Con_diag_delete_flag = false;
                    await db.SaveChangesAsync();
                    return "Diagnosis updated successfully";
                }
                return "Diagnosis does not exits";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Consulr_diag_GetAll>> GetAll_Consult_diag()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consult_Diagnosis
                                 join b in db.Status on a.Status equals b.sts_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 where a.Con_diag_id != 0
                                 orderby a.Con_diag_id descending
                                 select new Consulr_diag_GetAll
                                 {
                                     Con_diag_id = a.Con_diag_id,
                                     Con_diag_conid_FK = a.Con_diag_conid_FK,
                                     Con_diag_descrip = a.Con_diag_descrip,
                                     Status = a.Status,
                                     status_name = b.sts_name,
                                     Remarks = a.Remarks
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

        public async Task<string> Delete_Consult_diag(int Dlt_Id)
        {
            try
            {
                var result = await db.Consult_Diagnosis.FirstOrDefaultAsync(x => x.Con_diag_id == Dlt_Id);
                if (result != null)
                {
                    result.Con_diag_id = Dlt_Id;
                    result.Con_diag_delete_flag = true;
                    result.Con_diag_deleted_by = "1";
                    result.Con_diag_deleted_date = DateTime.Now;
                    result.Status = 6;
                    await db.SaveChangesAsync();
                    return "Diagnosis deleted successfully";
                }
                return "Diagnosis does not exits";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Consulr_diag_GetAll>> GetById_Consult_diag(int Conslt_id)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consult_Diagnosis
                                 join b in db.Status on a.Status equals b.sts_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 where a.Con_diag_conid_FK == Conslt_id
                                 orderby a.Con_diag_id descending
                                 select new Consulr_diag_GetAll
                                 {
                                     Con_diag_id = a.Con_diag_id,
                                     Con_diag_conid_FK = a.Con_diag_conid_FK,
                                     Con_diag_descrip = a.Con_diag_descrip,
                                     Status = a.Status,
                                     status_name = b.sts_name,
                                     Remarks = a.Remarks
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
    }
}
