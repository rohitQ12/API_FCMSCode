using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiagnostic_Test
    {
        Task<Diagnostic_Test> InsertDiagnostic_Test(Diagnostic_Test lead);
        Task<Diagnostic_Test> UpdateDiagnostic_Test(Diagnostic_Test lead);
        Task<List<GetAllDiagno_Test>> GetAllDiagnostic_Test();
        Task<List<Diagno_TestDD>> GetDiagnostic_Test_DD();
        Task<Diagnostic_Test> DeleteDiagnostic_Test(int DT_Id);
        Task<GetDiagno_TestById> GetDiagnostic_TestById(int DT_Id);
        Task<bool> ApproveDiagnostic_Test(ApproveDiagno_Test lead);
    }
}
