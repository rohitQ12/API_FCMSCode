using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IMenu
    {
        Task<Menus> InsertAppMenu(Menus lead);
        Task<Menus> UpdateAppMenu(Menus lead);
        //Task<List<SubModuleMenu>> GetAllAppMenu();
        Task<Menus> GetAppMenuById(int app_menu_id);
        Task<Menus> DeleteAppMenu(int app_menu_id);
        Task<List<Menus>> GetAppMenu();
    }
}
