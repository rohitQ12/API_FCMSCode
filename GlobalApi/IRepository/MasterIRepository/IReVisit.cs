using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IReVisit
    {
        Task<ReVisit> InsertReVisit(ReVisit lead);
        Task<List<GetAllReVisit>> GetAllReVisit();
        Task<GetAllReVisit> GetReVisitByCON_Id(int CON_Id);
        Task<GetAllReVisit> GetReVisitById(int RV_Id);
        Task<ReVisit> DeleteReVisit(int RV_Id);

    }
}
