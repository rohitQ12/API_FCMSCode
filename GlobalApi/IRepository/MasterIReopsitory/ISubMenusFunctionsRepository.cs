using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISubMenusFunctionsRepository
    {
        Task<SubMenusFunctions> InsertAppSubMenuFunctions(SubMenusFunctions subMenuFunctions);
        Task<SubMenusFunctions> UpdateAppSubMenuFunctions(SubMenusFunctions subMenuFunctions);
        Task<List<SubMenusFunctions>> GetAllAppSubMenuFunctions();
        Task<SubMenusFunctions> GetAppSubMenuFunctionsById(int SMF_Id);
        Task<SubMenusFunctions> DeleteAppSubMenuFunctions(int SMF_Id);
        Task<List<SubMenusFunctions>> GetAppSubMenuFunctions();
    }
}
