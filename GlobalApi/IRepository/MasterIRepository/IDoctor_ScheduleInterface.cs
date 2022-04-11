using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDoctor_ScheduleInterface
    {
        Task<List<Doctor_ScheduleModule>> GetDoctor_Schedule();

        Task<string> Insert_DoctorSchedule(Doctor_ScheduleModule Sc);

        Task<string> UpdateDoctor_Schedule(Doctor_ScheduleModule Su);
        Task<Doctor_ScheduleModule> DeleteDoctor_Schedule(int Id);
        Task<List<Doctor_ScheduleModule>> GetDoctor_ScheduleById(int id);
    }
}
