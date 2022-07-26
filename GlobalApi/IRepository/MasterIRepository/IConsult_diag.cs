using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IConsult_diag
    {
        Task<string> Insert_Consult_diag(Consulr_diag diagData);
        Task<string> Update_Consult_diag(Consulr_diag UpdConDiag);
        Task<List<Consulr_diag_GetAll>> GetAll_Consult_diag();
        Task<string> Delete_Consult_diag(int Dlt_Id);
        Task<List<Consulr_diag_GetAll>> GetById_Consult_diag(int DiagId);
    }
}
