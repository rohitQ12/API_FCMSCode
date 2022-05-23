using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IInsurer_MSTRepository
    {
        Task<List<Insurer_MST>> GetAllInsurer();
    }
}
