using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDietPlan
    {
        Task<string> InsertDietPlan(DietPlan lead);
        Task<string> UpdateDietPlan(DietPlan lead);
        Task<List<GetAllDietPlan>> GetAllDietPlan();
        Task<List<GetAllDietPlan>> GetDietPlanById(int Id);
        Task<string> DeleteDietPlan(int Id);

    }
}
