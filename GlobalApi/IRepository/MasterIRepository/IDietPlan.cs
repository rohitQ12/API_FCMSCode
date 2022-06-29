using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDietPlan
    {
        Task<DietPlan> InsertDietPlan(DietPlan lead);
        Task<DietPlan> UpdateDietPlan(DietPlan lead);
        Task<List<GetAllDietPlan>> GetAllDietPlan();
        Task<List<GetAllDietPlan>> GetDietPlanById(int Id);
        Task<DietPlan> DeleteDietPlan(int Id);

    }
}
