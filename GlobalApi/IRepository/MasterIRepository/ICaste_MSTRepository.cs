using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICaste_MSTRepository
    {
        Task<List<Caste_MST>> GetAllCaste();
    }
}
