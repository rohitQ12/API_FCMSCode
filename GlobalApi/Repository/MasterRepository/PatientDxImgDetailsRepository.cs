//using Microsoft.EntityFrameworkCore;
//using GlobalApi.Data;
//using GlobalApi.GlobalClasses;
//using GlobalApi.IRepository.MasterIRepository;
//using GlobalApi.Models.Master;

//namespace GlobalApi.Repository.MasterRepository
//{
//    public class PatientDxImgDetailsRepository : IPatientDxImgDetails
//    {
//        GlobalContext  db;
//        //public readonly string _connectionString;
//        private IPrimarykeyvalue primarykeyvalue;
//        public PatientDxImgDetailsRepository(GlobalContext _db)
//        {
//            db = _db;
//            primarykeyvalue = new Primarykeyvalue(_db);
//        }
//        public async Task<List<GetPatientDxImgDetails>> GetAllPatientDxImgDetails()
//        {
//            try
//            {
//                if (db != null)
//                {
//                    var query = (from a in db.PatientDxImgDetails
//                                 join b in db.Imaging on a.Img_Id_FK equals b.Id
//                                 join c in db.IMG_INVESTIGATIONS on a.Img_Invst_Id_FK equals c.Id
//                                 join d in db.IMG_SUBINVESTIGATIONS on a.Img_SubInvst_Id_FK equals d.Id
//                                 join e in db.Consultation on a.CON_Id_FK equals e.CON_Id
//                                 join f in db.Doctor on e.CON_DO_Id_FK equals f.DO_Id
//                                 join g in db.Patient on e.CON_PR_Id_FK equals g.PR_Id
//                                 orderby a.Id descending
//                                 select new GetPatientDxImgDetails
//                                 {
//                                     Id = a.Id,
//                                     Img_Id_FK = a.Img_Id_FK,
//                                     CON_Id_FK = a.CON_Id_FK,
//                                     Img_CON_DO_Id = e.CON_DO_Id_FK,
//                                     Img_DO_Name = string.Concat(f.DO_FirstName, f.DO_LastName),
//                                     Img_DO_MobNum = f.DO_MobileNumber,
//                                     Img_CON_PR_Id = e.CON_PR_Id_FK,
//                                     Img_PR_Name = string.Concat(g.PR_FirstName, g.PR_LastName),
//                                     Img_PR_Gender = g.PR_Gender,
//                                     Img_PR_Age = g.PR_Age,
//                                     Img_PR_MobNum = g.PR_MobileNumber,
//                                     Img_PR_Email = g.PR_Email,
//                                     Img_PR_Address = g.PR_Address,
//                                     //Img_PR_BloodGroup = g.PR_BloodGroup,
//                                     Img_PR_Photo = g.PR_Photo,
//                                     Img_PR_Taluk = g.PR_Taluk,
//                                     Img_PR_Village = g.PR_Village,
//                                     Img_PR_PostalCode = g.PR_Postalcode,
//                                     //Fasting = a.Fasting,
//                                     //Non_Fasting = a.Non_Fasting,
//                                     Img_Invst_Id_FK = a.Img_Invst_Id_FK,
//                                     Img_Invst_Category = c.Category,
//                                     Img_SubInvst_Id_FK = a.Img_SubInvst_Id_FK,
//                                     Img_SubInvst_Category = d.Sub_Category,
//                                     AcceptTest = a.AcceptTest,
//                                     ImgRemarks = a.ImgRemarks,
//                                     ImgDelivery_status = a.ImgDelivery_status,
//                                     Report = a.Report,
//                                     delete_flag = a.delete_flag,
//                                     status = a.status,
//                                 });
//                    return await query.ToListAsync();
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task<PatientDxImgDetailsBy_Id> GetPatientDxImgDetailsById(int Id)
//        {
//            if (db != null)
//            {
//                var query = (from a in db.PatientDxImgDetails
//                             join b in db.LabTest on a.Img_Id_FK equals b.Id
//                             join c in db.LAB_INVESTIGATIONS on a.Img_Invst_Id_FK equals c.Id
//                             join d in db.LAB_SUBINVESTIGATIONS on a.Img_SubInvst_Id_FK equals d.Id
//                             join e in db.Consultation on a.CON_Id_FK equals e.CON_Id
//                             join f in db.Doctor on e.CON_DO_Id_FK equals f.DO_Id
//                             join g in db.Patient on e.CON_PR_Id_FK equals g.PR_Id
//                             where a.Id == Id
//                             select new PatientDxImgDetailsBy_Id
//                             {
//                                 Id = a.Id,
//                                 Img_Id_FK = a.Img_Id_FK,
//                                 CON_Id_FK = a.CON_Id_FK,
//                                 Img_CON_DO_Id = e.CON_DO_Id_FK,
//                                 Img_DO_Name = string.Concat(f.DO_FirstName, f.DO_LastName),
//                                 Img_DO_MobNum = f.DO_MobileNumber,
//                                 Img_CON_PR_Id = e.CON_PR_Id_FK,
//                                 Img_PR_Name = string.Concat(g.PR_FirstName, g.PR_LastName),
//                                 Img_PR_Gender = g.PR_Gender,
//                                 Img_PR_Age = g.PR_Age,
//                                 Img_PR_MobNum = g.PR_MobileNumber,
//                                 Img_PR_Email = g.PR_Email,
//                                 Img_PR_Address = g.PR_Address,
//                                 //Img_PR_BloodGroup = g.PR_BloodGroup,
//                                 Img_PR_Photo = g.PR_Photo,
//                                 Img_PR_Taluk = g.PR_Taluk,
//                                 Img_PR_Village = g.PR_Village,
//                                 Img_PR_PostalCode = g.PR_Postalcode,
//                                 //Fasting = a.Fasting,
//                                 //Non_Fasting = a.Non_Fasting,
//                                 Img_Invst_Id_FK = a.Img_Invst_Id_FK,
//                                 Img_Invst_Category = c.Category,
//                                 Img_SubInvst_Id_FK = a.Img_SubInvst_Id_FK,
//                                 Img_SubInvst_Category = d.Sub_Category,
//                                 AcceptTest = a.AcceptTest,
//                                 ImgRemarks = a.ImgRemarks,
//                                 ImgDelivery_status = a.ImgDelivery_status,
//                                 Report = a.Report,
//                                 delete_flag = a.delete_flag,
//                                 status = a.status,
//                             }).FirstOrDefaultAsync();
//                return await query;
//            }
//            return null;
//        }

//        public async Task<bool> AcceptPatientDxImgDetails(int Id, int Img_Id_FK, int AcceptTest)
//        {
//            try
//            {
//                var result = await db.PatientDxImgDetails.FirstOrDefaultAsync(x => x.Id == Id && x.Img_Id_FK == Img_Id_FK);
//                if (result != null)
//                {
//                    result.Id = Id;
//                    result.Img_Id_FK = Img_Id_FK;
//                    //result.ImgRemarks = ImgRemarks;
//                    result.AcceptTest = AcceptTest;
//                    result.delete_flag = false;
//                    result.status = 1;
//                    await db.SaveChangesAsync();
//                    return true;
//                }
//                return false;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task<PatientDxImgDetails> UpdatePatientDxImgDetails(ImgReport lead)
//        {
//            try
//            {
//                var result = await db.PatientDxImgDetails.FirstOrDefaultAsync(x => x.Id == lead.Id && x.Img_Id_FK == lead.Img_Id_FK && x.AcceptTest == 1);
//                string uniqueFilename = ProcessUploadedFile(lead);
//                if (result != null)
//                {
//                    result.Id = lead.Id;
//                    result.Img_Id_FK = lead.Img_Id_FK;
//                    result.ImgRemarks = lead.ImgRemarks;
//                    result.ImgDelivery_status = lead.ImgDelivery_status;
//                    result.Report = uniqueFilename;
//                    //result.AcceptPrescription = 1;
//                    result.modified_by = 2;
//                    result.modified_date = DateTime.Now;
//                    result.delete_flag = false;
//                    result.status = 1;
//                    await db.SaveChangesAsync();
//                    return result;
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        private string ProcessUploadedFile(ImgReport model)
//        {
//            string uniqueFileName = null;


//            if (model.Report != null)
//            {
//                string uploadsFolder = Path.Combine("wwwroot/ImgReports");
//                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Report.FileName;
//                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
//                using (var fileStream = new FileStream(filePath, FileMode.Create))
//                {
//                    model.Report.CopyTo(fileStream);
//                }
//            }

//            return uniqueFileName;
//        }


//        public async Task<PatientDxImgDetails> DeletePatientDxImgDetails(int Id)
//        {
//            try
//            {
//                var result = await db.PatientDxImgDetails.FirstOrDefaultAsync(x => x.Id == Id);
//                if (result != null)
//                {
//                    result.Id = Id;
//                    result.delete_flag = true;
//                    result.status = 0;
//                    result.deleted_by = 1;
//                    result.deleted_date = DateTime.Now;
//                    await db.SaveChangesAsync();
//                    return result;
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }

//    }
//}
