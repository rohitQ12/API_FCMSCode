using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICountry
    {
        Task<string> InsertCountry(Countries countries);
        Task<string> UpdateCountry(Countries countries);
        Task<List<GetAllCountry>> GetAllCountry();
        Task<dynamic> GetAllCountry(int ItemsPerPage, int pageno);
        Task<List<Country_DD>> GetCountry_DD();
        Task<List<Country_DD>> GetCountry_DD_Mobile();
        Task<CountryById> GetCountryById(int Country_id);
        Task<string> DeleteCountry(int Country_id);
        Task<string> ApproveCountry(ApproveCountry approvecountry);

    }
}
