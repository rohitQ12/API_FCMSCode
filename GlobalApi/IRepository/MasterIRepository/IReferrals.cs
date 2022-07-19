using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IReferrals
    {
        Task<Referrals> InsertReferrals(Referrals lead);
        Task<List<GetReferrals>> GetAllReferrals();
        Task<List<GetReferrals>> GetReferralsByCON_Id(int CON_Id);
        Task<List<GetReferrals>> GetReferralsById(int Ref_Id);
        Task<Referrals> DeleteReferrals(int Ref_Id);
        //Task<Referrals> ApproveReferrals(int? AssistantId, string roleaction,ApprvReferrals lead);
        Task<Referrals> ApproveReferrals(ApprvReferrals lead);
    }
}
