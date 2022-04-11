using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDocumentType
    {
        Task<DocumentType> InsertDocumentType(DocumentType lead);
        Task<DocumentType> UpdateDocumentType(DocumentType lead);
        Task<List<DocumentType>> GetAllDocumentType();
        Task<List<DocumentType_DD>> GetDocumentType_DD();
        Task<DocumentTypeById> GetDocumentTypeById(int doctype_id);
        Task<DocumentType> DeleteDocumentType(int doctype_id);
    }
}
