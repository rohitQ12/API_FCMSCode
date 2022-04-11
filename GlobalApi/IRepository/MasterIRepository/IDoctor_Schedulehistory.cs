using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDoctor_Schedulehistory
    {
        Task<List<Schedule_historyModel>> GetDoctor_Schedulehistory();

        Task<List<Schedule_historyModel>> GetDoctor_Schedulehistory(int id);
    }
}
