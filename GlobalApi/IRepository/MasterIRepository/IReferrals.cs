using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IReferrals
    {
        Task<Referrals> InsertReferrals(Referrals lead);
        Task<List<GetReferrals>> GetAllReferrals();
        Task<GetReferrals> GetReferralsByCON_Id(int CON_Id);
        Task<GetReferrals> GetReferralsById(int Ref_Id);
        Task<Referrals> DeleteReferrals(int Ref_Id);
        //Task<ApprvReferrals> ApproveReferrals(int? AssistantId, string roleaction,ApprvReferrals lead);
        Task<ApprvReferrals> ApproveReferrals(ApprvReferrals lead);
    }
}
