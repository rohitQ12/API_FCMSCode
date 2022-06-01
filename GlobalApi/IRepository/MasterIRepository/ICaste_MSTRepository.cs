using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICaste_MSTRepository
    {
        Task<List<Caste_MST>> GetAllCaste();
        Task<List<Caste_DD>> GetCaste_DD(int Religion_id);
    }
}
