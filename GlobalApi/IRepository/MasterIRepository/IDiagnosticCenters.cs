using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiagnosticCenters
    {
        Task<DiagnosticCenters> InsertDiagnosticCenters(DiagnosticCenters lead);
        Task<DiagnosticCenters> UpdateDiagnosticCenters(DiagnosticCenters lead);
        Task<List<GetAllDiagnosticCenters>> GetAllDiagnosticCenters();
        Task<List<DiagnosticCenters_DD>> GetDiagnosticCenters_DD();
        Task<DiagnosticCentersById> GetDiagnosticCentersById(int DGSTC_Id);
        Task<DiagnosticCenters> DeleteDiagnosticCenters(int DGSTC_Id);

    }
}
