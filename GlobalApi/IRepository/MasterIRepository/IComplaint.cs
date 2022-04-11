using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IComplaint
    {
        Task<string> InsertComplaint(List<Complaint> lead, int Appt_Id);
        Task<Complaint> UpdateComplaint(Complaint lead);
        Task<List<GetAllComplaint>> GetAllComplaint();
        Task<ComplaintBy_Id> GetComplaintById(int CPT_Id);
        Task<Complaint> DeleteComplaint(int CPT_Id);

    }
}
