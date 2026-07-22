using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface INetwork
    {
        Task<string> InsertNetwork(Network Network);
        Task<string> UpdateNetwork(Network Network);
        Task<List<GetAllNetwork>> GetAllNetwork();
        Task<List<Network_DD>> GetNetwork_DD();
        Task<NetworkById> GetNetworkById(int NE_Id);
        Task<string> DeleteNetwork(int NE_Id);
        Task<string> ApproveNetwork(ApproveNetwork ApproveNetwork);

    }
}

