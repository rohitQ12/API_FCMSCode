using GlobalApi.Models.Master;
using GlobalApi.Models.Master.YourNamespace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GlobalApi.IRepository.MasterIReopsitory
{
    public interface IPartnerRepository
    {
        Task<List<Partner>> GetAllPartnersList();
        Task<string> UpdatePartnerDetails(Partner partner);
        Task<Partner?> GetPartnerDetailsByID(int partnerId);
    }
}
