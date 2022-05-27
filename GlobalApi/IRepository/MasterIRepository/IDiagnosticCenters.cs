using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiagnosticCenters
    {
        Task<DiagnosticCenters> InsertDiagnosticCenters(Diagnostic_Images lead);
        Task<DiagnosticCenters> UpdateDiagnosticCenters(Diagnostic_Images lead);
        Task<List<GetAllDiagnosticCenters>> GetAllDiagnosticCenters(int? DGSTC_Id, string roleaction);
        Task<List<DiagnosticCenters_DD>> GetDiagnosticCenters_DD(int? DGSTC_Id, string roleaction);
        Task<DiagnosticCentersById> GetDiagnosticCentersById(int DGSTC_Id, string roleaction);
        Task<List<Usercategory_DD>> GetDiagnosticCategory_DD();
        Task<DiagnosticCenters> DeleteDiagnosticCenters(int DGSTC_Id);
        Task<string> ApproveDiagnosticCenter(int DGSTC_Id, string? Remarks);
    }
}
