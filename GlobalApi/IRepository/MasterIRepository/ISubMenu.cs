using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISubMenu
    {
        Task<SubMenu> InsertAppSubMenu(SubMenu lead);
        Task<SubMenu> UpdateAppSubMenu(SubMenu lead);
        Task<List<SubMenu>> GetAllAppSubMenu();
        Task<SubMenu> GetAppSubMenuById(int SM_Id);
        Task<SubMenu> DeleteAppSubMenu(int SM_Id);
        Task<List<SubMenu>> GetAppSubMenu();

    }
}
