using GlobalApi.Models.Master;
using GlobalApi.Models.Master.YourNamespace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICustomer
    {
        Task<List<UserCustomer>> GetAllCustomerList();
        Task<UserCustomer?> GetCustomerByID(int custId);
    }
}
