using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface INationality_MSTRepository
    {
        Task<List<Nationality_MST>> GetAllNationality();
    }
}
