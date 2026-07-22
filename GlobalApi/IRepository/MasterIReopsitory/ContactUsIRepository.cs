using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ContactUsIRepository
    {

        Task<string> InsertContactUs(ContactUs lead);
        Task<List<GetAllCustomer_ContactUs>> GetAllCustomer_ContactUs();
        Task<GetCustomer_ContactUsById> GetCustomer_ContactUsById(string cust_phone_no);
    }
}
