using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IConsult_Complaint_DTL
    {
        Task<List<Consult_Complaint_DTL>> GetExistsConsult_Complaint_DTL(int CON_Id);
        Task<bool> UpdateConsult_Complaint_DTL(List<Consult_Complaint_DTL> lead, int CON_Id);
        Task<List<GetAllCCdtl>> GetAllConsult_Complaint_DTL();
        Task<List<GetAllCons_Complaints>> GetAllCons_Complaints();
        Task<Consult_Complaint_DTL> DeleteConsult_Complaint_DTL(int CPT_Id);
        Task<List<CCdtlBy_Id>> GetConsult_Complaint_DTLById(int CON_Id);
    }
}
