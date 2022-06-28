using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICountry
    {
        Task<bool> InsertCountry(Countries lead);
        Task<bool> UpdateCountry(Countries lead);
        Task<List<GetAllCountry>> GetAllCountry();
        Task<List<Country_DD>> GetCountry_DD();
        Task<CountryById> GetCountryById(int Country_id);
        Task<bool> DeleteCountry(int Country_id);
        Task<bool> ApproveCountry(ApproveCountry lead);

    }
}
