using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiagnostic_Test
    {
        Task<string> InsertDiagnostic_Test(Diagnostic_Test lead);
        Task<string> UpdateDiagnostic_Test(Diagnostic_Test lead);
        Task<List<GetAllDiagno_Test>> GetAllDiagnostic_Test();
        Task<List<Diagno_TestDD>> GetDiagnostic_Test_DD(int Cat_Id);
        Task<string> DeleteDiagnostic_Test(int DT_Id);
        Task<GetDiagno_TestById> GetDiagnostic_TestById(int DT_Id);
        Task<string> ApproveDiagnostic_Test(ApproveDiagno_Test lead);
    }
}
