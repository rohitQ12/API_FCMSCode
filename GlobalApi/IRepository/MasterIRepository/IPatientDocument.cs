using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IPatientDocument
    {
        Task<string> InsertPatientDocument(Patient_Documents lead, int PR_Id_FK);
        //Task<string> ProcessUploadedFile(List<Patient_Documents> lead);
        Task<string> UpdatePatientDocument(Patient_DocumentsUP lead);
        Task<List<GetAllPatientDocument>> GetAllPatientDocument();
        Task<PatientDocumentById> GetPatientDocumentById(int NE_Id);
        Task<PatientDocument> DeletePatientDocument(int NE_Id);

    }
}
