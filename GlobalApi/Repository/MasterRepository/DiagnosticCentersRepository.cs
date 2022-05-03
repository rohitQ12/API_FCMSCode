using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class DiagnosticCentersRepository : IDiagnosticCenters
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DiagnosticCentersRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<DiagnosticCenters> InsertDiagnosticCenters(DiagnosticCenters lead)
        {
            try
            {
                var duplicate = await db.DiagnosticCenters.FirstOrDefaultAsync(x => x.DGSTC_Code == lead.DGSTC_Code || x.DGSTC_Name == lead.DGSTC_Name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("DiagnosticCenter");
                    DiagnosticCenters obj = new DiagnosticCenters()
                    {
                        DGSTC_Id = id,
                        //DGSTC_Code = "DGSTC-" + Convert.ToString(id),                        DGSTC_Code = "DGSTC-" + Convert.ToString(id),
                        DGSTC_Code = lead.DGSTC_Code,
                        DGSTC_Name = lead.DGSTC_Name,
                        DGSTC_Address = lead.DGSTC_Address,
                        DGSTC_ST_Id_FK = lead.DGSTC_ST_Id_FK,
                        DGSTC_DI_Id_FK = lead.DGSTC_DI_Id_FK,
                        DGSTC_Village = lead.DGSTC_Village,
                        DGSTC_PostalCode = lead.DGSTC_PostalCode,
                        DGSTC_MobileNumber = lead.DGSTC_MobileNumber,
                        DGSTC_AlterNumber = lead.DGSTC_AlterNumber,
                        DGSTC_LandLineNo = lead.DGSTC_LandLineNo,
                        DGSTC_Email = lead.DGSTC_Email,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.DiagnosticCenters.AddAsync(obj);
                    await InsertUsers(obj);
                    await db.SaveChangesAsync();
                    return result.Entity;

                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<UsersLists> InsertUsers(DiagnosticCenters lead)
        {
            int _id = await primarykeyvalue.primary_key("DiagnosticCenters");
            UsersLists obj = new UsersLists()
            {
                Id = _id,
                User_cat = "DiagnosticCenters",
                User_ref_id = lead.DGSTC_Id,
            };
            var result = await db.UsersLists.AddAsync(obj);
            await db.SaveChangesAsync();
            return result.Entity;

        }
        public async Task<DiagnosticCenters> UpdateDiagnosticCenters(DiagnosticCenters lead)
        {
            try
            {
                var result = await db.DiagnosticCenters.FirstOrDefaultAsync(x => x.DGSTC_Id == lead.DGSTC_Id);
                if (result != null)
                {
                    result.DGSTC_Id = lead.DGSTC_Id;
                    result.DGSTC_Code = lead.DGSTC_Code;
                    result.DGSTC_Name = lead.DGSTC_Name;
                    result.DGSTC_Address = lead.DGSTC_Address;
                    result.DGSTC_ST_Id_FK = lead.DGSTC_ST_Id_FK;
                    result.DGSTC_DI_Id_FK = lead.DGSTC_DI_Id_FK;
                    result.DGSTC_Village = lead.DGSTC_Village;
                    result.DGSTC_PostalCode = lead.DGSTC_PostalCode;
                    result.DGSTC_MobileNumber = lead.DGSTC_MobileNumber;
                    result.DGSTC_AlterNumber = lead.DGSTC_AlterNumber;
                    result.DGSTC_LandLineNo = lead.DGSTC_LandLineNo;
                    result.DGSTC_Email = lead.DGSTC_Email;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
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
        public async Task<List<GetAllDiagnosticCenters>> GetAllDiagnosticCenters()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DiagnosticCenters
                                 join b in db.States on a.DGSTC_Id equals b.stat_id
                                 join c in db.Districts on a.DGSTC_DI_Id_FK equals c.district_id
                                 orderby a.DGSTC_Id descending
                                 select new GetAllDiagnosticCenters
                                 {
                                     DGSTC_Id = a.DGSTC_Id,
                                     DGSTC_Code = a.DGSTC_Code,
                                     DGSTC_Name = a.DGSTC_Name,
                                     DGSTC_Address = a.DGSTC_Address,
                                     DGSTC_ST_Id_FK = a.DGSTC_ST_Id_FK,
                                     DGSTC_state_name = b.state_name,
                                     DGSTC_DI_Id_FK = a.DGSTC_DI_Id_FK,
                                     DGSTC_district_name = c.district_name,
                                     DGSTC_Village = a.DGSTC_Village,
                                     DGSTC_PostalCode = a.DGSTC_PostalCode,
                                     DGSTC_MobileNumber = a.DGSTC_MobileNumber,
                                     DGSTC_AlterNumber = a.DGSTC_AlterNumber,
                                     DGSTC_LandLineNo = a.DGSTC_LandLineNo,
                                     DGSTC_Email = a.DGSTC_Email,
                                     delete_flag = a.delete_flag,
                                     status = a.status

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
        public async Task<List<DiagnosticCenters_DD>> GetDiagnosticCenters_DD()
        {
            if (db != null)
            {
                var query = (from a in db.DiagnosticCenters
                             where a.delete_flag == false && a.status == 1
                             select new DiagnosticCenters_DD
                             {
                                 DGSTC_Id = a.DGSTC_Id,
                                 DGSTC_Code = a.DGSTC_Code,
                                 DGSTC_Name = a.DGSTC_Name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<DiagnosticCenters> DeleteDiagnosticCenters(int DGSTC_Id)
        {
            try
            {
                var result = await db.DiagnosticCenters.FirstOrDefaultAsync(x => x.DGSTC_Id == DGSTC_Id);
                if (result != null)
                {
                    result.DGSTC_Id = DGSTC_Id;
                    result.delete_flag = true;
                    result.status = 6;
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
        public async Task<DiagnosticCentersById> GetDiagnosticCentersById(int DGSTC_Id)
        {
            if (db != null)
            {
                var query = (from a in db.DiagnosticCenters
                             join b in db.States on a.DGSTC_Id equals b.stat_id
                             join c in db.Districts on a.DGSTC_DI_Id_FK equals c.district_id
                             where a.DGSTC_Id == DGSTC_Id
                             select new DiagnosticCentersById
                             {
                                 DGSTC_Id = a.DGSTC_Id,
                                 DGSTC_Code = a.DGSTC_Code,
                                 DGSTC_Name = a.DGSTC_Name,
                                 DGSTC_Address = a.DGSTC_Address,
                                 DGSTC_ST_Id_FK = a.DGSTC_ST_Id_FK,
                                 DGSTC_state_name = b.state_name,
                                 DGSTC_DI_Id_FK = a.DGSTC_DI_Id_FK,
                                 DGSTC_district_name = c.district_name,
                                 DGSTC_Village = a.DGSTC_Village,
                                 DGSTC_PostalCode = a.DGSTC_PostalCode,
                                 DGSTC_MobileNumber = a.DGSTC_MobileNumber,
                                 DGSTC_AlterNumber = a.DGSTC_AlterNumber,
                                 DGSTC_LandLineNo = a.DGSTC_LandLineNo,
                                 DGSTC_Email = a.DGSTC_Email,
                                 delete_flag = a.delete_flag,
                                 status = a.status

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
