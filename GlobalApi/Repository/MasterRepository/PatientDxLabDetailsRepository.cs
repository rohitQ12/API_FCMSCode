using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    //public class PatientDxLabDetailsRepository : IPatientDxLabDetails
    //{
    //    GlobalContext  db;
    //    //public readonly string _connectionString;
    //    private IPrimarykeyvalue primarykeyvalue;
    //    public PatientDxLabDetailsRepository(GlobalContext _db)
    //    {
    //        db = _db;
    //        primarykeyvalue = new Primarykeyvalue(_db);
    //    }
    //    public async Task<List<GetPatientDxLabDetails>> GetAllPatientDxLabDetails()
    //    {
    //        try
    //        {
    //            if (db != null)
    //            {
    //                var query = (from a in db.PatientDxLabDetails
    //                             join b in db.LabTest on a.LT_Id_FK equals b.Id
    //                             join c in db.LAB_INVESTIGATIONS on a.Lab_Invst_Id_FK equals c.Id
    //                             join d in db.LAB_SUBINVESTIGATIONS on a.Lab_SubInvst_Id_FK equals d.Id
    //                             join e in db.Consultation on a.CON_Id_FK equals e.CON_Id
    //                             join f in db.Doctor on e.CON_DO_Id_FK equals f.DO_Id
    //                             join g in db.Patient on e.CON_PR_Id_FK equals g.PR_Id
    //                             orderby a.Id descending
    //                             select new GetPatientDxLabDetails
    //                             {
    //                                 Id = a.Id,
    //                                 LT_Id_FK = a.LT_Id_FK,
    //                                 CON_Id_FK = a.CON_Id_FK,
    //                                 Lab_CON_DO_Id = e.CON_DO_Id_FK,
    //                                 Lab_DO_Name = string.Concat(f.DO_FirstName, f.DO_LastName),
    //                                 Lab_DO_MobNum = f.DO_MobileNumber,
    //                                 Lab_CON_PR_Id = e.CON_PR_Id_FK,
    //                                 Lab_PR_Name = string.Concat(g.PR_FirstName, g.PR_LastName),
    //                                 Lab_PR_Gender = g.PR_Gender,
    //                                 Lab_PR_Age = g.PR_Age,
    //                                 Lab_PR_MobNum = g.PR_MobileNumber,
    //                                 Lab_PR_Email = g.PR_Email,
    //                                 Lab_PR_Address = g.PR_Address,
    //                                 //Lab_PR_BloodGroup = g.PR_BloodGroup,
    //                                 Lab_PR_Photo = g.PR_Photo,
    //                                 Lab_PR_Taluk = g.PR_Taluk,
    //                                 Lab_PR_Village = g.PR_Village,
    //                                 Lab_PR_PostalCode = g.PR_Postalcode,
    //                                 //Fasting = a.Fasting,
    //                                 //Non_Fasting = a.Non_Fasting,
    //                                 FastingORNonFasting = a.FastingORNonFasting,
    //                                 SampleTaken = a.SampleTaken,
    //                                 Lab_Invst_Id_FK = a.Lab_Invst_Id_FK,
    //                                 Lab_Invst_Category = c.Category,
    //                                 Lab_SubInvst_Id_FK = a.Lab_SubInvst_Id_FK,
    //                                 Lab_SubInvst_Category = d.Sub_Category,
    //                                 AcceptTest = a.AcceptTest,
    //                                 LabRemarks = a.LabRemarks,
    //                                 LabDelivery_status = a.LabDelivery_status,
    //                                 delete_flag = a.delete_flag,
    //                                 status = a.status,
    //                             });
    //                return await query.ToListAsync();
    //            }
    //            return null;
    //        }
    //        catch (Exception e)
    //        {
    //            throw new Exception(e.Message);
    //        }
    //    }
    //    public async Task<PatientDxLabDetailsBy_Id> GetPatientDxLabDetailsById(int Id)
    //    {
    //        if (db != null)
    //        {
    //            var query = (from a in db.PatientDxLabDetails
    //                         join b in db.LabTest on a.LT_Id_FK equals b.Id
    //                         join c in db.LAB_INVESTIGATIONS on a.Lab_Invst_Id_FK equals c.Id
    //                         join d in db.LAB_SUBINVESTIGATIONS on a.Lab_SubInvst_Id_FK equals d.Id
    //                         join e in db.Consultation on a.CON_Id_FK equals e.CON_Id
    //                         join f in db.Doctor on e.CON_DO_Id_FK equals f.DO_Id
    //                         join g in db.Patient on e.CON_PR_Id_FK equals g.PR_Id
    //                         where a.Id == Id
    //                         select new PatientDxLabDetailsBy_Id
    //                         {
    //                             Id = a.Id,
    //                             LT_Id_FK = a.LT_Id_FK,
    //                             CON_Id_FK = a.CON_Id_FK,
    //                             Lab_CON_DO_Id = e.CON_DO_Id_FK,
    //                             Lab_DO_Name = string.Concat(f.DO_FirstName, f.DO_LastName),
    //                             Lab_DO_MobNum = f.DO_MobileNumber,
    //                             Lab_CON_PR_Id = e.CON_PR_Id_FK,
    //                             Lab_PR_Name = string.Concat(g.PR_FirstName, g.PR_LastName),
    //                             Lab_PR_Gender = g.PR_Gender,
    //                             Lab_PR_Age = g.PR_Age,
    //                             Lab_PR_MobNum = g.PR_MobileNumber,
    //                             Lab_PR_Email = g.PR_Email,
    //                             Lab_PR_Address = g.PR_Address,
    //                             //Lab_PR_BloodGroup = g.PR_BloodGroup,
    //                             Lab_PR_Photo = g.PR_Photo,
    //                             Lab_PR_Taluk = g.PR_Taluk,
    //                             Lab_PR_Village = g.PR_Village,
    //                             Lab_PR_PostalCode = g.PR_Postalcode,
    //                             //Fasting = a.Fasting,
    //                             //Non_Fasting = a.Non_Fasting,
    //                             FastingORNonFasting = a.FastingORNonFasting,
    //                             SampleTaken = a.SampleTaken,
    //                             Lab_Invst_Id_FK = a.Lab_Invst_Id_FK,
    //                             Lab_Invst_Category = c.Category,
    //                             Lab_SubInvst_Id_FK = a.Lab_SubInvst_Id_FK,
    //                             Lab_SubInvst_Category = d.Sub_Category,
    //                             AcceptTest = a.AcceptTest,
    //                             LabRemarks = a.LabRemarks,
    //                             LabDelivery_status = a.LabDelivery_status,
    //                             delete_flag = a.delete_flag,
    //                             status = a.status,
    //                         }).FirstOrDefaultAsync();
    //            return await query;
    //        }
    //        return null;
    //    }

    //    public async Task<bool> AcceptPatientDxLabDetails(int Id, int LT_Id_FK, int AcceptTest)
    //    {
    //        try
    //        {
    //            var result = await db.PatientDxLabDetails.FirstOrDefaultAsync(x => x.Id == Id && x.LT_Id_FK == LT_Id_FK);
    //            if (result != null)
    //            {
    //                result.Id = Id;
    //                result.LT_Id_FK = LT_Id_FK;
    //                //result.LabRemarks = LabRemarks;
    //                result.AcceptTest = AcceptTest;
    //                result.delete_flag = false;
    //                result.status = 1;
    //                await db.SaveChangesAsync();
    //                return true;
    //            }
    //            return false;
    //        }
    //        catch (Exception e)
    //        {
    //            throw new Exception(e.Message);
    //        }
    //    }
    //    public async Task<PatientDxLabDetails> UpdatePatientDxLabDetails(TestReport lead)
    //    {
    //        try
    //        {
    //            var result = await db.PatientDxLabDetails.FirstOrDefaultAsync(x => x.Id == lead.Id && x.LT_Id_FK == lead.LT_Id_FK && x.AcceptTest == 1);
    //            string uniqueFilename = ProcessUploadedFile(lead);
    //            if (result != null)
    //            {
    //                result.Id = lead.Id;
    //                result.LT_Id_FK = lead.LT_Id_FK;
    //                result.LabRemarks = lead.LabRemarks;
    //                result.LabDelivery_status = lead.LabDelivery_status;
    //                result.Report = uniqueFilename;
    //                //result.AcceptPrescription = 1;
    //                result.modified_by = 2;
    //                result.modified_date = DateTime.Now;
    //                result.delete_flag = false;
    //                result.status = 1;
    //                await db.SaveChangesAsync();
    //                return result;
    //            }
    //            return null;
    //        }
    //        catch (Exception e)
    //        {
    //            throw new Exception(e.Message);
    //        }
    //    }

    //    private string ProcessUploadedFile(TestReport model)
    //    {
    //        string uniqueFileName = null;


    //        if (model.Report != null)
    //        {
    //            string uploadsFolder = Path.Combine("wwwroot/LabReports");
    //            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Report.FileName;
    //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
    //            using (var fileStream = new FileStream(filePath, FileMode.Create))
    //            {
    //                model.Report.CopyTo(fileStream);
    //            }
    //        }

    //        return uniqueFileName;
    //    }
    //    public async Task<PatientDxLabDetails> DeletePatientDxLabDetails(int Id)
    //    {
    //        try
    //        {
    //            var result = await db.PatientDxLabDetails.FirstOrDefaultAsync(x => x.Id == Id);
    //            if (result != null)
    //            {
    //                result.Id = Id;
    //                result.delete_flag = true;
    //                result.status = 0;
    //                result.deleted_by = 3;
    //                result.deleted_date = DateTime.Now;
    //                await db.SaveChangesAsync();
    //                return result;
    //            }
    //            return null;
    //        }
    //        catch (Exception e)
    //        {
    //            throw new Exception(e.Message);
    //        }
    //    }

    //}
}
