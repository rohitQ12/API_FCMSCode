using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class ImgTestDetailsRepository : IImgTestDetails
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public ImgTestDetailsRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }

        public async Task<string> InsertImgTestDetails(List<ImgTestDetails> lead, int Img_Id_FK)
        {
            try
            {
                foreach (ImgTestDetails lab in lead)
                {
                    var duplicate = await db.ImgTestDetails.FirstOrDefaultAsync(x => x.Id == lab.Id && x.Img_Invst_Id == lab.Img_Invst_Id
                    && x.Img_SubInvst_Id == lab.Img_SubInvst_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("ImgTestDetails");
                        ImgTestDetails obj = new ImgTestDetails()
                        {
                            Id = id,
                            Img_Id_FK = Img_Id_FK,
                            Img_Invst_Id = lab.Img_Invst_Id,
                            Img_SubInvst_Id = lab.Img_SubInvst_Id,
                            ImgRemarks = lab.ImgRemarks,
                            delete_flag = false,
                        };
                        var result = await db.ImgTestDetails.AddAsync(obj);
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
        public async Task<ImgTestDetails> UpdateImgTestDetails(ImgReport lead)
        {
            try
            {
                var result = await db.ImgTestDetails.FirstOrDefaultAsync(x => x.Id == lead.Id && x.Img_Id_FK == lead.Img_Id_FK);
                if (lead.Report != null)
                {
                    if (result.Report != null)
                    {
                        string filepath = Path.Combine("wwwroot/ImgReports", result.Report);
                        System.IO.File.Delete(filepath);
                    }
                }

                string uniqueFilename = ProcessUploadedFile(lead);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.Img_Id_FK = lead.Img_Id_FK;
                    result.Img_Invst_Id = lead.Img_Invst_Id;
                    result.Img_SubInvst_Id = lead.Img_SubInvst_Id;
                    result.ImgRemarks = lead.ImgRemarks;
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
        private string ProcessUploadedFile(ImgReport model)
        {
            string uniqueFileName = null;
            if (model.Report != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/ImgReports");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Report.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Report.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }


        public async Task<ImgTestDetails> DeleteImgTestDetails(int Id)
        {
            try
            {
                var result = await db.ImgTestDetails.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
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
        public async Task<List<GetAllImgTestDetails>> GetAllImgTestDetails()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.ImgTestDetails
                                 join b in db.ImgTest on a.Img_Id_FK equals b.Id
                                 join c in db.IMG_INVESTIGATIONS on a.Img_Invst_Id equals c.Id
                                 join d in db.IMG_SUBINVESTIGATIONS on a.Img_SubInvst_Id equals d.Id
                                 join e in db.Consultation on b.Img_CON_Id_FK equals e.CON_Id
                                 join f in db.Doctor on e.CON_DO_Id_FK equals f.DO_Id
                                 join g in db.Patient on e.CON_PR_Id_FK equals g.PR_Id
                                 orderby a.Id descending
                                 select new GetAllImgTestDetails
                                 {
                                     Id = a.Id,
                                     Img_Id_FK = a.Img_Id_FK,
                                     CON_Id_FK = b.Img_CON_Id_FK,
                                     Img_CON_DO_Id = e.CON_DO_Id_FK,
                                     Img_DO_Name = string.Concat(f.DO_FirstName, f.DO_LastName),
                                     Img_DO_MobNum = f.DO_MobileNumber,
                                     Img_CON_PR_Id = e.CON_PR_Id_FK,
                                     Img_PR_Name = string.Concat(g.PR_FirstName, g.PR_LastName),
                                     Img_PR_Gender = g.PR_Gender,
                                     Img_PR_Age = g.PR_Age,
                                     //Img_PR_MobNum = g.PR_MobileNumber,
                                     //Img_PR_Email = g.PR_Email,
                                     //Img_PR_Address = g.PR_Address,
                                     Img_PR_BloodGroup = g.PR_BloodGroup,
                                     //Img_PR_Photo = g.PR_Photo,
                                     //Img_PR_Taluk = g.PR_Taluk,
                                     //Img_PR_Village = g.PR_Village,
                                     //Img_PR_PostalCode = g.PR_Postalcode,
                                     //Fasting = a.Fasting,
                                     //Non_Fasting = a.Non_Fasting,
                                     Img_Invst_Id = a.Img_Invst_Id,
                                     Img_Invst_Category = c.Category,
                                     Img_SubInvst_Id = a.Img_SubInvst_Id,
                                     Img_SubInvst_Category = d.Sub_Category,
                                     ImgRemarks = a.ImgRemarks,
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
        public async Task<ImgTestDetailsById> GetImgTestDetailsById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.ImgTestDetails
                             join b in db.ImgTest on a.Img_Id_FK equals b.Id
                             join c in db.LAB_INVESTIGATIONS on a.Img_Invst_Id equals c.Id
                             join d in db.LAB_SUBINVESTIGATIONS on a.Img_SubInvst_Id equals d.Id
                             join e in db.Consultation on b.Img_CON_Id_FK equals e.CON_Id
                             join f in db.Doctor on e.CON_DO_Id_FK equals f.DO_Id
                             join g in db.Patient on e.CON_PR_Id_FK equals g.PR_Id
                             where a.Id == Id
                             select new ImgTestDetailsById
                             {
                                 Id = a.Id,
                                 Img_Id_FK = a.Img_Id_FK,
                                 CON_Id_FK = b.Img_CON_Id_FK,
                                 Img_CON_DO_Id = e.CON_DO_Id_FK,
                                 Img_DO_Name = string.Concat(f.DO_FirstName, f.DO_LastName),
                                 Img_DO_MobNum = f.DO_MobileNumber,
                                 Img_CON_PR_Id = e.CON_PR_Id_FK,
                                 Img_PR_Name = string.Concat(g.PR_FirstName, g.PR_LastName),
                                 Img_PR_Gender = g.PR_Gender,
                                 Img_PR_Age = g.PR_Age,
                                 //Img_PR_MobNum = g.PR_MobileNumber,
                                 //Img_PR_Email = g.PR_Email,
                                 //Img_PR_Address = g.PR_Address,
                                 Img_PR_BloodGroup = g.PR_BloodGroup,
                                 //Img_PR_Photo = g.PR_Photo,
                                 //Img_PR_Taluk = g.PR_Taluk,
                                 //Img_PR_Village = g.PR_Village,
                                 //Img_PR_PostalCode = g.PR_Postalcode,
                                 Img_Invst_Id = a.Img_Invst_Id,
                                 Img_Invst_Category = c.Category,
                                 Img_SubInvst_Id = a.Img_SubInvst_Id,
                                 Img_SubInvst_Category = d.Sub_Category,
                                 ImgRemarks = a.ImgRemarks,
                                 Report = a.Report,
                                 delete_flag = a.delete_flag,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
