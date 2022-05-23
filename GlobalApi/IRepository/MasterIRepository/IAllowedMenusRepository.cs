using GlobalApi.Models.Authentication;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IAllowedMenusRepository
    {
        Task<List<Menus_List>> Get(string roleId);
        Task<List<ClaimsModels>> GetClims(int submenuid, string roleId);
    }
}
