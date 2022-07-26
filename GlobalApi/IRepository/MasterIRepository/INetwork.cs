using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface INetwork
    {
        Task<string> InsertNetwork(Network lead);
        Task<string> UpdateNetwork(Network lead);
        Task<List<GetAllNetwork>> GetAllNetwork();
        Task<List<Network_DD>> GetNetwork_DD();
        Task<NetworkById> GetNetworkById(int NE_Id);
        Task<string> DeleteNetwork(int NE_Id);
        Task<string> ApproveNetwork(ApproveNetwork lead);

    }
}

