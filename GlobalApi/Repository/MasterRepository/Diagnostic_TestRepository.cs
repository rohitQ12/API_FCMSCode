using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Diagnostic_TestRepository : IDiagnostic_Test
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Diagnostic_TestRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<string> InsertDiagnostic_Test(Diagnostic_Test lead)
        {
            try
            {
                var duplicate = await db.Diagnostic_Test.FirstOrDefaultAsync(x => x.DT_Type == lead.DT_Type
                    && x.DT_Category == lead.DT_Category && x.DT_Desc == lead.DT_Desc);
                var DT_name = await db.Diagnostic_Test.FirstOrDefaultAsync(x => x.DT_Desc == lead.DT_Desc);
                var DT_code = await db.Diagnostic_Test.FirstOrDefaultAsync(x => x.DT_Code == lead.DT_Code);
                if (DT_code == null)
                {
                    if (DT_name == null)
                    {
                        if (duplicate == null)
                        {
                            int id = await primarykeyvalue.primary_key("Diagnostic_Test");
                            Diagnostic_Test obj = new Diagnostic_Test()
                            {
                                DT_Id = id,
                                DT_Code = lead.DT_Code,
                                DT_Type = lead.DT_Type,
                                DT_Category = lead.DT_Category,
                                DT_Desc = lead.DT_Desc,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                                status = 1
                            };
                            var result = await db.Diagnostic_Test.AddAsync(obj);
                            await db.SaveChangesAsync();
                            return "DiagnoTest Added Successfully";
                        }
                        return "DiagnoTest Details Already Exists";
                    }
                    return "DiagnoTest Name Already Exists";
                }
                return "DiagnoTest Code Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateDiagnostic_Test(Diagnostic_Test lead)
        {
            try
            {
                var result = await db.Diagnostic_Test.FirstOrDefaultAsync(x => x.DT_Id == lead.DT_Id);
                var DT_name = await db.Diagnostic_Test.FirstOrDefaultAsync(x => x.DT_Desc == lead.DT_Desc);
                var DT_code = await db.Diagnostic_Test.FirstOrDefaultAsync(x => x.DT_Code == lead.DT_Code);
                if (result.DT_Code != lead.DT_Code)
                {
                    if (result.DT_Desc != lead.DT_Desc)
                    {
                        if (result != null)
                        {
                            result.DT_Id = lead.DT_Id;
                            result.DT_Code = lead.DT_Code;
                            result.DT_Type = lead.DT_Type;
                            result.DT_Category = lead.DT_Category;
                            result.DT_Desc = lead.DT_Desc;
                            result.modified_by = 1;
                            result.modified_date = DateTime.Now;
                            result.delete_flag = false;
                            result.status = 2;
                            await db.SaveChangesAsync();
                            return "DiagnoTest Updated Successfully";
                        }
                        return "DiagnoTest Details Doesn't Exists";
                    }
                    return "DiagnoTest Desc Already Exists";
                }
                return "DiagnoTest Code Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<GetAllDiagno_Test>> GetAllDiagnostic_Test()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Diagnostic_Test
                                 join b in db.Status on a.status equals b.sts_id
                                 join c in db.DiagnosticType on a.DT_Type equals c.Id
                                 join d in db.DiagnoCategory on a.DT_Category equals d.id
                                 orderby a.DT_Id descending
                                 select new GetAllDiagno_Test
                                 {
                                     DT_Id = a.DT_Id,
                                     DT_Code = a.DT_Code,
                                     DT_Type = a.DT_Type,
                                     Type_Name = c.Type,
                                     DT_Category = a.DT_Category,
                                     Cat_Name = d.name,
                                     DT_Desc = a.DT_Desc,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = b.sts_name,
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
        public async Task<List<Diagno_TestDD>> GetDiagnostic_Test_DD(int Cat_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Diagnostic_Test
                             where a.delete_flag == false || a.status == 3 || a.DT_Id != 0 || a.DT_Category == Cat_Id
                             select new Diagno_TestDD
                             {
                                 DT_Id = a.DT_Id,
                                 DT_Code = a.DT_Code,
                                 DT_Desc = a.DT_Desc,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

        public async Task<string> DeleteDiagnostic_Test(int DT_Id)
        {
            try
            {
                var result = await db.Diagnostic_Test.FirstOrDefaultAsync(x => x.DT_Id == DT_Id);
                if (result != null)
                {
                    result.DT_Id = DT_Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "DiagnoTest Deleted Successfully";
                }
                return "DiagnoTest Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<GetDiagno_TestById> GetDiagnostic_TestById(int DT_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Diagnostic_Test
                             join b in db.Status on a.status equals b.sts_id
                             join c in db.DiagnosticType on a.DT_Type equals c.Id
                             join d in db.DiagnoCategory on a.DT_Category equals d.id
                             where a.DT_Id == DT_Id
                             select new GetDiagno_TestById
                             {
                                 DT_Id = a.DT_Id,
                                 DT_Code = a.DT_Code,
                                 DT_Type = a.DT_Type,
                                 Type_Name = c.Type,
                                 DT_Category = a.DT_Category,
                                 Cat_Name = d.name,
                                 DT_Desc = a.DT_Desc,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = b.sts_name,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> ApproveDiagnostic_Test(ApproveDiagno_Test lead)
        {
            try
            {
                var result = await db.Diagnostic_Test.FirstOrDefaultAsync(x => x.DT_Id == lead.DT_Id);
                if (result != null)
                {
                    result.DT_Id = lead.DT_Id;
                    result.status = 3;
                    await db.SaveChangesAsync();
                    return "DiagnoTest Approved Successfully";
                }
                return "DiagnoTest Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
