using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface Istate
    {
        Task<string> InsertState(States states);
        Task<string> UpdateState(States states);
        Task<List<GetStateCountry>> GetAllState();
        Task<List<State_DD>> GetState_DD(int cntry_id);
        Task<string> DeleteState(int stat_id);
        Task<StateById> GetStateById(int stat_id);
        Task<string> ApproveState(ApproveState approvestate);
    }
}
