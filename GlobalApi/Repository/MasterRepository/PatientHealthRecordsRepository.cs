using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class PatientHealthRecordsRepository : IPatientHealthRecords
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public PatientHealthRecordsRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertPatientHealthRecords(PHR_Doc lead)
        {
            try
            {
                foreach (var PDoc in lead.Choose_Document)
                {
                    var duplicate = await db.PatientHealthRecords.FirstOrDefaultAsync(x => x.PHR_Id == lead.PHR_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("PatientHealthRecords");
                        string uniqueFilename = ProcessUploadedFile(PDoc);
                        PatientHealthRecords obj = new PatientHealthRecords()
                        {
                            PHR_Id = id,
                            //PR_Id_FK = PR_Id_FK,
                            Appt_Id = lead.Appt_Id,
                            Choose_Document = uniqueFilename,
                            Doc_UserId_FK = 1,//modify
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.PatientHealthRecords.AddAsync(obj);
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


        //public async Task<string> InsertPatientHealthRecords(List<PHR_Doc> lead , int Appt_Id)
        //{
        //    try
        //    {
        //        foreach (PHR_Doc PDoc in lead)
        //        {
        //            foreach (var phr in PDoc.Choose_Document)
        //            {
        //                var duplicate = await db.PatientHealthRecords.FirstOrDefaultAsync(x => x.PHR_Id == PDoc.PHR_Id);
        //                if (duplicate == null)
        //                {
        //                    int id = await primarykeyvalue.primary_key("PatientHealthRecords");
        //                    string uniqueFilename = ProcessUploadedFile(phr);
        //                    PatientHealthRecords obj = new PatientHealthRecords()
        //                    {
        //                        PHR_Id = id,
        //                        //PR_Id_FK = PR_Id_FK,
        //                        Appt_Id = Appt_Id,
        //                        Choose_Document = uniqueFilename,
        //                        Doc_UserId_FK = 1,//modify
        //                        created_by = 1,
        //                        created_date = DateTime.Now,
        //                        delete_flag = false,
        //                        status = 1
        //                    };
        //                    var result = await db.PatientHealthRecords.AddAsync(obj);
        //                    await db.SaveChangesAsync();
        //                }
        //                else
        //                    return "Data already inserted";
        //            }

        //        }
        //        return "Record insert successfully";
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}


        //Inserting PatientHealthRecordss

        private string ProcessUploadedFile(IFormFile Choose_Document)
        {
            string uniqueFileName = null;


            if (Choose_Document != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/PatientHealthRecords");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Choose_Document.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Choose_Document.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        //public async Task<string> UpdatePatientHealthRecords(PHR_Doc lead)
        //{
        //    try
        //    {
        //        foreach (var PDoc in lead.Choose_Document)
        //        {
        //            var result = await db.PatientHealthRecords.FirstOrDefaultAsync(x => x.PHR_Id == lead.PHR_Id);
        //            string uniqueFilename = lead.Choose_Document != null ? ProcessUploadedFile(PDoc) : result.Choose_Document;

        //            if (result != null)
        //            {
        //                result.PHR_Id = lead.PHR_Id;
        //                result.Appt_Id = lead.Appt_Id;
        //                result.Choose_Document = uniqueFilename;
        //                result.Doc_UserId_FK = lead.Doc_UserId_FK;
        //                result.modified_by = 2;
        //                result.modified_date = DateTime.Now;
        //                result.delete_flag = false;
        //                result.status = 2;
        //                await db.SaveChangesAsync();
        //                //return "Sucessfull";
        //            }
        //            return "Please enter a Valid Id";

        //        }
        //        return "Health Records updated Successfully";
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        public async Task<string> UpdatePatientHealthRecords(PHR_DocUP lead)
        {
            try
            {
                List<PatientHealthRecords> AlreadyExistsPHR = await GetExistsPHR(lead.Appt_Id);
                if (AlreadyExistsPHR.Count > 0)
                {
                    foreach (var d in AlreadyExistsPHR)
                    {
                        //Delete
                        var result = await db.PatientHealthRecords.FirstOrDefaultAsync(x => x.Choose_Document == d.Choose_Document && x.Appt_Id == lead.Appt_Id);
                        if (result != null)
                        {
                            var removephr = db.PatientHealthRecords.Remove(result);
                            await db.SaveChangesAsync();
                            string filepath = Path.Combine("wwwroot/PatientHealthRecords", result.Choose_Document);
                            System.IO.File.Delete(filepath);

                        }

                    }

                }
                else
                    return "There are no records";

                foreach (var PDoc in lead.Choose_Document)
                {
                    var duplicate = await db.PatientHealthRecords.FirstOrDefaultAsync(x => x.PHR_Id == lead.PHR_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("PatientHealthRecords");
                        string uniqueFilename = ProcessUploadedFile(PDoc);
                        //string uniqueFilename = lead.Choose_Document != null ? ProcessUploadedFile(PDoc) : lead.Choose_Document;
                        PatientHealthRecords obj = new PatientHealthRecords()
                        {
                            PHR_Id = id,
                            //PR_Id_FK = PR_Id_FK,
                            Appt_Id = lead.Appt_Id,
                            Choose_Document = uniqueFilename,
                            Doc_UserId_FK = 1,//modify
                            created_by = 1,
                            created_date = DateTime.Now,
                            modified_by = 2,
                            modified_date = DateTime.Now,
                            delete_flag = false,
                            status = 2
                        };
                        var result = await db.PatientHealthRecords.AddAsync(obj);
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


        public async Task<List<PatientHealthRecords>> GetExistsPHR(int Appt_Id)
        {
            try
            {
                var result = await (from d in db.PatientHealthRecords
                                    where d.Appt_Id == Appt_Id
                                    select new PatientHealthRecords()
                                    {
                                        PHR_Id = d.PHR_Id,
                                        Choose_Document = d.Choose_Document,
                                        Doc_UserId_FK = d.Doc_UserId_FK,

                                    }).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllPHR>> GetAllPatientHealthRecords()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.PatientHealthRecords
                                 orderby a.PHR_Id descending
                                 select new GetAllPHR
                                 {
                                     PHR_Id = a.PHR_Id,
                                     Appt_Id = a.Appt_Id,
                                     Choose_Document = a.Choose_Document,
                                     Doc_UserId_FK = a.Doc_UserId_FK,
                                     delete_flag = a.delete_flag,
                                     status = a.status,

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

        public async Task<PatientHealthRecords> DeletePatientHealthRecords(int PHR_Id)
        {
            try
            {
                var result = await db.PatientHealthRecords.FirstOrDefaultAsync(x => x.PHR_Id == PHR_Id);
                if (result != null)
                {
                    result.PHR_Id = PHR_Id;
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
        public async Task<PHRById> GetPatientHealthRecordsById(int PHR_Id)
        {
            if (db != null)
            {
                var query = (from a in db.PatientHealthRecords
                             where a.PHR_Id == PHR_Id
                             select new PHRById
                             {
                                 PHR_Id = a.PHR_Id,
                                 Appt_Id = a.Appt_Id,
                                 Choose_Document = a.Choose_Document,
                                 Doc_UserId_FK = a.Doc_UserId_FK,
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
