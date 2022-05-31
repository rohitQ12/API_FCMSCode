using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DoctorDocumentRepository : IDoctorDocument
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DoctorDocumentRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertDoctorDocument(Doctor_Documents lead, int DO_Id)
        {
            try
            {
                foreach (var DDoc in lead.Choose_Document)
                {
                    var duplicate = await db.DoctorDocument.FirstOrDefaultAsync(x => x.DDoc_Id == lead.DDoc_Id && x.DO_Id == DO_Id
                        && x.doctype_id == lead.doctype_id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("DoctorDocument");
                        string uniqueFilename = ProcessUploadedFile(DDoc);
                        DoctorDocument obj = new DoctorDocument()
                        {
                            DDoc_Id = id,
                            DO_Id = DO_Id,
                            doctype_id = lead.doctype_id,
                            Choose_Document = uniqueFilename,
                            Doc_UserId_FK = 1,//modify
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.DoctorDocument.AddAsync(obj);
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

        //Inserting DoctorDocuments

        //public async Task<string> InsertDoctorDocument(Doctor_Doc_File lead, int DO_Id)
        //{
        //    try
        //    {
        //        foreach (var det in lead)
        //        {
        //            string uniqueFilename = String.Empty;
        //            string IdFilename = String.Empty;
        //            int id = await primarykeyvalue.primary_key("DoctorDocument");
                    
        //            uniqueFilename = await ProcessUploadedFile(det.Choose_Document);
        //            DoctorDocument obj = new DoctorDocument()
        //            {
        //                DDoc_Id = id,
        //                DO_Id = DO_Id,
        //                doctype_id = det.doctype_id,
        //                Choose_Document = uniqueFilename,
        //                Doc_UserId_FK = 1,//modify
        //                created_by = 1,
        //                created_date = DateTime.Now,
        //                delete_flag = false,
        //                status = 1
        //            };
        //            var result = await db.DoctorDocument.AddAsync(obj);
        //            await db.SaveChangesAsync();
        //            break;

        //        }


        //            /*
        //            foreach (int doctp in lead.doctype_id)
        //            {
        //                foreach (var DDoc in lead.Choose_Document)
        //                {

        //                    var duplicate = await db.DoctorDocument.FirstOrDefaultAsync(x => x.DDoc_Id == lead.DDoc_Id && x.DO_Id == DO_Id);
        //                    if (duplicate == null)
        //                    {
        //                        string uniqueFilename = String.Empty;
        //                        string IdFilename = String.Empty;
        //                        int id = await primarykeyvalue.primary_key("DoctorDocument");
        //                        IdFilename = lead.doctype_id + " " DDoc.FileName;
        //                        uniqueFilename = await ProcessUploadedFile(DDoc);
        //                        DoctorDocument obj = new DoctorDocument()
        //                        {
        //                            DDoc_Id = id,
        //                            DO_Id = DO_Id,
        //                            doctype_id = doctp,
        //                            Choose_Document = uniqueFilename,
        //                            Doc_UserId_FK = 1,//modify
        //                            created_by = 1,
        //                            created_date = DateTime.Now,
        //                            delete_flag = false,
        //                            status = 1
        //                        };
        //                        var result = await db.DoctorDocument.AddAsync(obj);
        //                        await db.SaveChangesAsync();
        //                        break;
        //                    }
        //                    else
        //                     return "Data already inserted";

        //                }


        //            }
        //            */
        //            return "Record insert successfully";
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        private string ProcessUploadedFile(IFormFile Choose_Document)
        {
            //string uniqueFileName = null;
            string uniqueFileName = String.Empty;

            if (Choose_Document != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/DoctorDocuments");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Choose_Document.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                   Choose_Document.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        public async Task<string> UpdateDoctorDocument(Doctor_Documents lead)
        {
            try
            {
                List<DoctorDocument> AlreadyExistsDDocs = await GetExistsDDocs(lead.DO_Id);
                if (AlreadyExistsDDocs.Count > 0)
                {
                    foreach (var d in AlreadyExistsDDocs)
                    {
                        //Delete
                        var result = await db.DoctorDocument.FirstOrDefaultAsync(x => x.Choose_Document == d.Choose_Document && x.DO_Id == lead.DO_Id);
                        if (result != null)
                        {
                            var removephr = db.DoctorDocument.Remove(result);
                            await db.SaveChangesAsync();
                            string filepath = Path.Combine("wwwroot/DoctorDocuments", result.Choose_Document);
                            System.IO.File.Delete(filepath);

                        }

                    }

                }
                else
                    return "There are no records";

                foreach (var DDoc in lead.Choose_Document)
                {
                    var duplicate = await db.DoctorDocument.FirstOrDefaultAsync(x => x.DDoc_Id == lead.DDoc_Id && x.DO_Id == lead.DO_Id
                        && x.doctype_id == lead.doctype_id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("DoctorDocument");
                        string uniqueFilename = ProcessUploadedFile(DDoc);
                        DoctorDocument obj = new DoctorDocument()
                        {
                            DDoc_Id = id,
                            DO_Id = lead.DO_Id,
                            doctype_id = lead.doctype_id,//modify
                            Choose_Document = uniqueFilename,
                            Doc_UserId_FK = 1,//modify
                            created_by = 1,
                            created_date = DateTime.Now,
                            modified_by = 2,
                            modified_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.DoctorDocument.AddAsync(obj);
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
        public async Task<List<DoctorDocument>> GetExistsDDocs(int DO_Id)
        {
            try
            {
                var result = await (from d in db.DoctorDocument
                                    where d.DO_Id == DO_Id
                                    select new DoctorDocument()
                                    {
                                        DDoc_Id = d.DDoc_Id,
                                        doctype_id = d.doctype_id,
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

        public async Task<List<GetAllDoctorDocument>> GetAllDoctorDocument()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DoctorDocument
                                 join b in db.Doctor on a.DO_Id equals b.DO_Id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.DocumentType on a.doctype_id equals c.doctype_id into clist
                                 from c in clist.DefaultIfEmpty()
                                 orderby a.DDoc_Id descending
                                 select new GetAllDoctorDocument
                                 {
                                     DDoc_Id = a.DDoc_Id,
                                     DO_Id = a.DO_Id,
                                     DO_Name = String.Concat(b.DO_FirstName, b.DO_LastName),
                                     doctype_id = a.doctype_id,
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

        public async Task<DoctorDocument> DeleteDoctorDocument(int DDoc_Id)
        {
            try
            {
                var result = await db.DoctorDocument.FirstOrDefaultAsync(x => x.DDoc_Id == DDoc_Id);
                if (result != null)
                {
                    result.DDoc_Id = DDoc_Id;
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
        public async Task<DoctorDocumentById> GetDoctorDocumentById(int DDoc_Id)
        {
            if (db != null)
            {
                var query = (from a in db.DoctorDocument
                             join b in db.Doctor on a.DO_Id equals b.DO_Id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.DocumentType on a.doctype_id equals c.doctype_id into clist
                             from c in clist.DefaultIfEmpty()
                             where a.DDoc_Id == DDoc_Id
                             select new DoctorDocumentById
                             {
                                 DDoc_Id = a.DDoc_Id,
                                 DO_Id = a.DO_Id,
                                 DO_Name = String.Concat(b.DO_FirstName, b.DO_LastName),
                                 doctype_id = a.doctype_id,
                                 Doc_Name = c.doctype_name,
                                 Choose_Document = a.Choose_Document,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
