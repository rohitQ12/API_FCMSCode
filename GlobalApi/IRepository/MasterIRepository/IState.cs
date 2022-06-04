using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface Istate
    {
        Task<bool> InsertState(States lead);
        Task<States> UpdateState(States lead);
        Task<List<GetStateCountry>> GetAllState();
        Task<List<State_DD>> GetState_DD(int cntry_id);
        Task<States> DeleteState(int stat_id);
        Task<StateById> GetStateById(int stat_id);
        Task<string> ApproveState(ApproveState lead);
    }
}
