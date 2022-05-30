using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DocumentTypeRepository : IDocumentType
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DocumentTypeRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<DocumentType> InsertDocumentType(DocumentType lead)
        {
            try
            {
                var duplicate = await db.DocumentType.FirstOrDefaultAsync(x => x.doctype_name == lead.doctype_name || x.doc_description == lead.doc_description);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("DocumentType");
                    DocumentType obj = new DocumentType()
                    {
                        doctype_id = id,
                        doctype_name = lead.doctype_name,
                        doc_description = lead.doc_description,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.DocumentType.AddAsync(obj);
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
        public async Task<DocumentType> UpdateDocumentType(DocumentType lead)
        {
            try
            {
                var result = await db.DocumentType.FirstOrDefaultAsync(x => x.doctype_id == lead.doctype_id);
                if (result != null)
                {
                    result.doctype_id = lead.doctype_id;
                    result.doctype_name = lead.doctype_name;
                    result.doc_description = lead.doc_description;
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
        public async Task<List<DocumentType>> GetAllDocumentType()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DocumentType
                                 orderby a.doctype_id descending
                                 select a);
                    return await query.ToListAsync();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<DocumentType_DD>> GetDocumentType_DD()
        {
            if (db != null)
            {
                var query = (from a in db.DocumentType
                             where a.delete_flag == false && a.status != 6 && a.doctype_id != 0
                             select new DocumentType_DD
                             {
                                 doctype_id = a.doctype_id,
                                 doctype_name = a.doctype_name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<DocumentType> DeleteDocumentType(int doctype_id)
        {
            try
            {
                var result = await db.DocumentType.FirstOrDefaultAsync(x => x.doctype_id == doctype_id);
                if (result != null)
                {
                    result.doctype_id = doctype_id;
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
        public async Task<DocumentTypeById> GetDocumentTypeById(int doctype_id)
        {
            if (db != null)
            {
                var query = (from a in db.DocumentType
                             where a.doctype_id == doctype_id
                             select new DocumentTypeById
                             {
                                 doctype_id = a.doctype_id,
                                 doctype_name = a.doctype_name,
                                 doc_description = a.doc_description,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
