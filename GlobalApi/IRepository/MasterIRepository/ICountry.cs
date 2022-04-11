using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICountry
    {
        Task<Countries> InsertCountry(Countries lead);
        Task<Countries> UpdateCountry(Countries lead);
        Task<List<Countries>> GetAllCountry();
        Task<List<Country_DD>> GetCountry_DD();
        Task<CountryById> GetCountryById(int Country_id);
        Task<Countries> DeleteCountry(int Country_id);

    }
}
