using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICaste_MSTRepository
    {
        Task<List<GetAllCasteMst>> GetAllCaste();
        Task<List<Caste_DD>> GetCaste_DD(int Religion_id);
    }
}
