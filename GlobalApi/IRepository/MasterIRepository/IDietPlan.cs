using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDietPlan
    {
        Task<DietPlan> InsertDietPlan(DietPlan lead);
        Task<DietPlan> UpdateDietPlan(DietPlan lead);
        Task<List<GetAllDietPlan>> GetAllDietPlan();
        Task<GetById> GetDietPlanById(int SYM_Id);
        Task<DietPlan> DeleteDietPlan(int SYM_Id);

    }
}
