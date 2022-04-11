using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class LabTestingDetailsRepository : ILabTestingDetails
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public LabTestingDetailsRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }

        public async Task<string> InsertLabTestingDetails(List<LabTestingDetails> lead, int LT_Id_FK)
        {
            try
            {
                foreach (LabTestingDetails lab in lead)
                {
                    var duplicate = await db.LabTestingDetails.FirstOrDefaultAsync(x => x.LT_Id_FK == lab.LT_Id_FK && x.Lab_Invst_Id == lab.Lab_Invst_Id
                                    && x.Lab_SubInvst_Id == lab.Lab_SubInvst_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("LabTestingDetails");
                        LabTestingDetails obj = new LabTestingDetails()
                        {
                            Id = id,
                            LT_Id_FK = LT_Id_FK,
                            Lab_Invst_Id = lab.Lab_Invst_Id,
                            Lab_SubInvst_Id = lab.Lab_SubInvst_Id,
                            FastingORNonFasting = lab.FastingORNonFasting,
                            Remarks = lab.Remarks,
                            delete_flag = false,
                        };
                        var result = await db.LabTestingDetails.AddAsync(obj);
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
        public async Task<LabTestingDetails> UpdateLabTestingDetails(TestReport lead)
        {
            try
            {
                var result = await db.LabTestingDetails.FirstOrDefaultAsync(x => x.Id == lead.Id && x.LT_Id_FK == lead.LT_Id_FK);
                if (lead.Report != null)
                {
                    if (result.Report != null)
                    {
                        string filepath = Path.Combine("wwwroot/LabReports", result.Report);
                        System.IO.File.Delete(filepath);
                    }
                }

                string uniqueFilename = ProcessUploadedFile(lead);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.LT_Id_FK = lead.LT_Id_FK;
                    result.Lab_Invst_Id = lead.Lab_Invst_Id;
                    result.Lab_SubInvst_Id = lead.Lab_SubInvst_Id;
                    result.FastingORNonFasting = lead.FastingORNonFasting;
                    result.Remarks = lead.Remarks;
                    result.Report = uniqueFilename;
                    result.modified_by = 2;
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

        private string ProcessUploadedFile(TestReport model)
        {
            string uniqueFileName = null;


            if (model.Report != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/LabReports");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Report.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Report.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        public async Task<LabTestingDetails> DeleteLabTestingDetails(int Id)
        {
            try
            {
                var result = await db.LabTestingDetails.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
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
        public async Task<List<GetLabTestingDetails>> GetAllLabTestingDetails()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.LabTestingDetails
                                 join b in db.LabTesting on a.LT_Id_FK equals b.Id
                                 join c in db.LAB_INVESTIGATIONS on a.Lab_Invst_Id equals c.Id
                                 join d in db.LAB_SUBINVESTIGATIONS on a.Lab_SubInvst_Id equals d.Id
                                 join e in db.Consultation on b.Tst_CON_Id_FK equals e.CON_Id
                                 join f in db.Doctor on e.CON_DO_Id_FK equals f.DO_Id
                                 join g in db.Patient on e.CON_PR_Id_FK equals g.PR_Id
                                 orderby a.Id descending
                                 select new GetLabTestingDetails
                                 {
                                     Id = a.Id,
                                     LT_Id_FK = a.LT_Id_FK,
                                     //CON_Id_FK = a.CON_Id_FK,
                                     Lab_CON_DO_Id = e.CON_DO_Id_FK,
                                     Lab_DO_Name = string.Concat(f.DO_FirstName, f.DO_LastName),
                                     Lab_CON_PR_Id = e.CON_PR_Id_FK,
                                     Lab_PR_Name = string.Concat(g.PR_FirstName, g.PR_LastName),
                                     Lab_PR_Gender = g.PR_Gender,
                                     Lab_PR_Age = g.PR_Age,
                                     Lab_PR_BloodGroup = g.PR_BloodGroup,
                                     Lab_Invst_Id = a.Lab_Invst_Id,
                                     Lab_Invst_Category = c.Category,
                                     Lab_SubInvst_Id = a.Lab_SubInvst_Id,
                                     Lab_SubInvst_Category = d.Sub_Category,
                                     FastingORNonFasting = a.FastingORNonFasting,
                                     Remarks = a.Remarks,
                                     Report = a.Report,
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
        public async Task<LabTestingDetailsById> GetLabTestingDetailsById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.LabTestingDetails
                             join b in db.LabTesting on a.LT_Id_FK equals b.Id
                             join c in db.LAB_INVESTIGATIONS on a.Lab_Invst_Id equals c.Id
                             join d in db.LAB_SUBINVESTIGATIONS on a.Lab_SubInvst_Id equals d.Id
                             join e in db.Consultation on b.Tst_CON_Id_FK equals e.CON_Id
                             join f in db.Doctor on e.CON_DO_Id_FK equals f.DO_Id
                             join g in db.Patient on e.CON_PR_Id_FK equals g.PR_Id
                             where a.Id == Id
                             select new LabTestingDetailsById
                             {
                                 Id = a.Id,
                                 LT_Id_FK = a.LT_Id_FK,
                                 //CON_Id_FK = a.CON_Id_FK,
                                 Lab_CON_DO_Id = e.CON_DO_Id_FK,
                                 Lab_DO_Name = string.Concat(f.DO_FirstName, f.DO_LastName),
                                 Lab_CON_PR_Id = e.CON_PR_Id_FK,
                                 Lab_PR_Name = string.Concat(g.PR_FirstName, g.PR_LastName),
                                 Lab_PR_Gender = g.PR_Gender,
                                 Lab_PR_Age = g.PR_Age,
                                 Lab_PR_BloodGroup = g.PR_BloodGroup,
                                 Lab_Invst_Id = a.Lab_Invst_Id,
                                 Lab_Invst_Category = c.Category,
                                 Lab_SubInvst_Id = a.Lab_SubInvst_Id,
                                 Lab_SubInvst_Category = d.Sub_Category,
                                 FastingORNonFasting = a.FastingORNonFasting,
                                 Remarks = a.Remarks,
                                 Report = a.Report,
                                 delete_flag = a.delete_flag,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
