using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiagnosticType
    {
        Task<DiagnosticType> InsertDiagnosticType(DiagnosticType lead);
        Task<DiagnosticType> UpdateDiagnosticType(DiagnosticType lead);
        Task<List<GetAllDiagnoType>> GetAllDiagnosticType();
        Task<List<DiagnoType_DD>> GetDiagnosticType_DD();
        //Task<DiagnosticTypeBy_Id> GetDiagnosticTypeById(int Id);
        Task<DiagnosticType> DeleteDiagnosticType(int Id);

    }
}
