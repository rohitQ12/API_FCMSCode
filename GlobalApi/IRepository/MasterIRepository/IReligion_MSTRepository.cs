using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IReligion_MSTRepository
    {
        Task<List<Religion_MST>> GetAllReligion();
    }
}
