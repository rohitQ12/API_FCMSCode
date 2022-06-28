using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ILanguage_MSTRepository
    {
        Task<List<Language_MST>> GetAllLanguage();
    }
}
