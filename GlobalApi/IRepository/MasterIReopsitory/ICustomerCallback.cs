using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICustomerCallback
    {       
        Task<ApiResponse> InsertCustomer_Callback(Customer_Callback customer_Callback);
       
    }
}
