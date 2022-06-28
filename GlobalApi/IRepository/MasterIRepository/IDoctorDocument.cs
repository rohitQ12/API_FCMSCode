using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDoctorDocument
    {
        Task<string> InsertDoctorDocument(Doctor_Documents lead, int DO_Id);
        //Task<string> InsertDoctorDocument(Doctor_Doc_File lead, int DO_Id);
        Task<string> UpdateDoctorDocument(Doctor_Documents lead);
        Task<List<DoctorDocument>> GetExistsDDocs(int DO_Id);
        Task<List<GetAllDoctorDocument>> GetAllDoctorDocument();
        Task<DoctorDocument> DeleteDoctorDocument(int DDoc_Id);
        Task<DoctorDocumentById> GetDoctorDocumentById(int DDoc_Id);
    }
}
