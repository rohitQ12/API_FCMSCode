using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICurrency
    {
        Task<Currency> InsertCurrency(Currency lead);
        Task<Currency> UpdateCurrency(Currency lead);
        Task<List<GetCountryCurrency>> GetAllCurrency();
        Task<List<Currency_DD>> GetCurrency_DD();
        Task<Currency> DeleteCurrency(int currency_id);
        Task<CurrencyById> GetCurrencyById(int currency_id);
    }
}
