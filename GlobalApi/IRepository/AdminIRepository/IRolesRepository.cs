using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
using GlobalApi.Models.AdminClaims;

namespace GlobalApi.IRepository.AdminIRepository
{
    public interface IRolesRepository
    {
        Task<bool> CreateRoles(RolesModels role);
        Task<List<AspNetRole>> GetAllRoles();
        Task<List<AspNetRole>> GetAllRoles_DD(string? roleaction, string? rolename);
        Task<List<RolesModels>> GetRoleId(string id);
        Task<bool> CheckRoles(string roleId);
        Task<bool> ActivateInactivate(string id);
       // Task<bool> UpdateOfficeRole(RolesModels role);

       Task<string> UpdateOfficeRole(RolesModels role);

    }
}
