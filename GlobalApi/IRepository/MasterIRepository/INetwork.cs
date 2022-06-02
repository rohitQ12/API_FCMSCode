using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface INetwork
    {
        Task<Network> InsertNetwork(Network lead);
        Task<Network> UpdateNetwork(Network lead);
        Task<List<GetAllNetwork>> GetAllNetwork();
        Task<List<Network_DD>> GetNetwork_DD();
        Task<NetworkById> GetNetworkById(int NE_Id);
        Task<Network> DeleteNetwork(int NE_Id);
        Task<string> ApproveNetwork(int NE_Id, string? Remarks);

    }
}

