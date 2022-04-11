using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISubMenuFunctionsRepository
    {
        Task<SubMenuFunctions> InsertAppSubMenuFunctions(SubMenuFunctions subMenuFunctions);
        Task<SubMenuFunctions> UpdateAppSubMenuFunctions(SubMenuFunctions subMenuFunctions);
        Task<List<SubMenuFunctions>> GetAllAppSubMenuFunctions();
        Task<SubMenuFunctions> GetAppSubMenuFunctionsById(int SMF_Id);
        Task<SubMenuFunctions> DeleteAppSubMenuFunctions(int SMF_Id);
        Task<List<SubMenuFunctions>> GetAppSubMenuFunctions();
    }
}
