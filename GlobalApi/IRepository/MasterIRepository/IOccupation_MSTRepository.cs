using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IOccupation_MSTRepository
    {
        Task<List<Occupation_MST>> GetAllOccupation();
    }
}
