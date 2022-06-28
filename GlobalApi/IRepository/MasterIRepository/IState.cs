using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface Istate
    {
        Task<bool> InsertState(States lead);
        Task<bool> UpdateState(States lead);
        Task<List<GetStateCountry>> GetAllState();
        Task<List<State_DD>> GetState_DD(int cntry_id);
        Task<bool> DeleteState(int stat_id);
        Task<StateById> GetStateById(int stat_id);
        Task<bool> ApproveState(ApproveState lead);
    }
}
