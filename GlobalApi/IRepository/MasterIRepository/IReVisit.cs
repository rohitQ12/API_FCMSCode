using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IReVisit
    {
        Task<ReVisit> InsertReVisit(ReVisit lead);
        Task<List<GetAllReVisit>> GetAllReVisit();
        Task<List<GetAllReVisit>> GetReVisitByCON_Id(int CON_Id);
        Task<List<GetAllReVisit>> GetReVisitById(int RV_Id);
        Task<ReVisit> DeleteReVisit(int RV_Id);
        //Task<ReVisit> ApproveReVisit(int? AssistantId, string roleaction,ApprvReferrals lead);
        Task<ReVisit> ApproveReVisit(ApprvReVisit lead);

    }
}
