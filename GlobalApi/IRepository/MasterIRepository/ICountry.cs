using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICountry
    {
        Task<string> InsertCountry(Countries lead);
        Task<string> UpdateCountry(Countries lead);
        Task<dynamic> GetAllCountry();
        Task<List<Country_DD>> GetCountry_DD();
        Task<CountryById> GetCountryById(int Country_id);
        Task<string> DeleteCountry(int Country_id);
        Task<string> ApproveCountry(ApproveCountry lead);

    }
}
