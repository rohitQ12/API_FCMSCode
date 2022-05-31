using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class PatientDocumentRepository : IPatientDocument
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public PatientDocumentRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertPatientDocument(Patient_Documents lead , int PR_Id_FK)
        {
            try
            {
                foreach (var PDoc in lead.Choose_Document)
                {
                    var duplicate = await db.PatientDocument.FirstOrDefaultAsync(x => x.Doc_Id == lead.Doc_Id && x.PR_Id_FK == PR_Id_FK
                        && x.Doc_Type_Id_FK == lead.Doc_Type_Id_FK );
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("PatientDocument");
                        string uniqueFilename = ProcessUploadedFile(PDoc);
                        PatientDocument obj = new PatientDocument()
                        {
                            Doc_Id = id,
                            PR_Id_FK = PR_Id_FK,
                            Doc_Type_Id_FK = 1,//modify
                            Choose_Document = uniqueFilename,
                            Doc_UserId_FK = 1,//modify
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.PatientDocument.AddAsync(obj);
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

        //Inserting PatientDocuments
        private string ProcessUploadedFile(IFormFile Choose_Document)
        {
            string uniqueFileName = null;


            if (Choose_Document != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/PatientDocuments");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Choose_Document.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                   Choose_Document.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        public async Task<string> UpdatePatientDocument(Patient_DocumentsUP lead)
        {
            try
            {
                List<PatientDocument> AlreadyExistsPRDocs = await GetExistsPRDocs(lead.PR_Id_FK);
                if (AlreadyExistsPRDocs.Count > 0)
                {
                    foreach (var d in AlreadyExistsPRDocs)
                    {
                        //Delete
                        var result = await db.PatientDocument.FirstOrDefaultAsync(x => x.Choose_Document == d.Choose_Document && x.PR_Id_FK == lead.PR_Id_FK);
                        if (result != null)
                        {
                            var removephr = db.PatientDocument.Remove(result);
                            await db.SaveChangesAsync();
                            string filepath = Path.Combine("wwwroot/PatientDocuments", result.Choose_Document);
                            System.IO.File.Delete(filepath);

                        }

                    }

                }
                else
                    return "There are no records";

                foreach (var PDoc in lead.Choose_Document)
                {
                    var duplicate = await db.PatientDocument.FirstOrDefaultAsync(x => x.Doc_Id == lead.Doc_Id && x.PR_Id_FK == lead.PR_Id_FK
                        && x.Doc_Type_Id_FK == lead.Doc_Type_Id_FK);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("PatientDocument");
                        string uniqueFilename = ProcessUploadedFile(PDoc);
                        PatientDocument obj = new PatientDocument()
                        {
                            Doc_Id = id,
                            PR_Id_FK = lead.PR_Id_FK,
                            Doc_Type_Id_FK = 1,//modify
                            Choose_Document = uniqueFilename,
                            Doc_UserId_FK = 1,//modify
                            created_by = 1,
                            created_date = DateTime.Now,
                            modified_by = 2,
                            modified_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.PatientDocument.AddAsync(obj);
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
        public async Task<List<PatientDocument>> GetExistsPRDocs(int PR_Id_FK)
        {
            try
            {
                var result = await (from d in db.PatientDocument
                                    where d.PR_Id_FK == PR_Id_FK
                                    select new PatientDocument()
                                    {
                                        Doc_Id = d.Doc_Id,
                                        Doc_Type_Id_FK = d.Doc_Type_Id_FK,
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

        public async Task<List<GetAllPatientDocument>> GetAllPatientDocument()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.PatientDocument
                                 join b in db.Patient on a.PR_Id_FK equals b.PR_Id
                                 join c in db.DocumentType on a.Doc_Type_Id_FK equals c.doctype_id
                                 orderby a.Doc_Id descending
                                 select new GetAllPatientDocument
                                 {
                                     Doc_Id = a.Doc_Id,
                                     PR_Id_FK = a.PR_Id_FK,
                                     //Appt_Id_Fk = a.Appt_Id_Fk,
                                     PR_Name = String.Concat(b.PR_FirstName,b.PR_LastName),
                                     Doc_Type_Id_FK = a.Doc_Type_Id_FK,
                                     Doc_Name = c.doctype_name,
                                     Choose_Document = a.Choose_Document,

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

        public async Task<PatientDocument> DeletePatientDocument(int Doc_Id)
        {
            try
            {
                var result = await db.PatientDocument.FirstOrDefaultAsync(x => x.Doc_Id == Doc_Id);
                if (result != null)
                {
                    result.Doc_Id = Doc_Id;
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
        public async Task<PatientDocumentById> GetPatientDocumentById(int Doc_Id)
        {
            if (db != null)
            {
                var query = (from a in db.PatientDocument
                             join b in db.Patient on a.PR_Id_FK equals b.PR_Id
                             join c in db.DocumentType on a.Doc_Type_Id_FK equals c.doctype_id
                             where a.Doc_Id == Doc_Id
                             select new PatientDocumentById
                             {
                                 Doc_Id = a.Doc_Id,
                                 PR_Id_FK = a.PR_Id_FK,
                                 //Appt_Id_Fk = a.Appt_Id_Fk,
                                 PR_Name = String.Concat(b.PR_FirstName, b.PR_LastName),
                                 Doc_Type_Id_FK = a.Doc_Type_Id_FK,
                                 Doc_Name = c.doctype_name,
                                 Choose_Document = a.Choose_Document,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
