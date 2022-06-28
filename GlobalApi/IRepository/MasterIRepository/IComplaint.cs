using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IComplaint
    {
        Task<string> InsertComplaint(List<Complaint> lead, int Appt_Id);
        Task<string> InsertPHCComplaint(List<Complaint> lead, int Appt_Id);

        Task<bool> UpdateComplainttest(List<Complaint> lead, int Appt_Id);
        Task<bool> UpdatePHCComplaint(List<Complaint> lead, int Appt_Id);

        Task<List<GetAllComplaint>> GetAllComplaint();
        Task<List<GetAllComplaint>> GetAllPHCComplaint();

        Task<List<ComplaintBy_Id>> GetComplaintById(int CPT_PR_Id_FK);
        Task<Complaint> DeleteComplaint(int CPT_Id);

    }
}
