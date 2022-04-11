using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IComplaintMst
    {
        Task<ComplaintMst> InsertComplaintMst(ComplaintMst lead);
        Task<ComplaintMst> UpdateComplaintMst(ComplaintMst lead);
        Task<List<ComplaintMst>> GetAllComplaintMst();
        Task<List<ComplaintMst_DD>> GetComplaintMst_DD();
        Task<ComplaintMst> GetComplaintMstById(int Id);
        Task<ComplaintMst> DeleteComplaintMst(int Id);

    }
}
