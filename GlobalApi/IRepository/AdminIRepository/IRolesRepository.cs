using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
using GlobalApi.Models.AdminClaims;

namespace GlobalApi.IRepository.AdminIRepository
{
    public interface IRolesRepository
    {
        Task<bool> CreateRoles(RolesModels role);
        Task<List<AspNetRole>> GetAllRoles();
        Task<List<AspNetRole>> GetAllRoles_DD();
        Task<List<RolesModels>> GetRoleId(string id);
        Task<Boolean> CheckRoles(string roleId);
        Task<bool> ActivateInactivate(string id);
        Task<bool> UpdateOfficeRole(string rolename, string Id);

    }
}
