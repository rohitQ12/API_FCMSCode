using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface Istate
    {
        Task<string> InsertState(States lead);
        Task<string> UpdateState(States lead);
        Task<List<GetStateCountry>> GetAllState();
        Task<List<GetStateCountry>> GetAllState_test(int ItemsPerPage, int pageno);
        Task<List<State_DD>> GetState_DD(int cntry_id);
        Task<string> DeleteState(int stat_id);
        Task<StateById> GetStateById(int stat_id);
        Task<string> ApproveState(ApproveState lead);
    }
}
