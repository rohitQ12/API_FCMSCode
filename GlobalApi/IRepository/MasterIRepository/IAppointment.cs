using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IAppointment
    {
        Task<AppointmentModel> InsertAppointment(InsertDetails lead);
        Task<AppointmentModel> UpdateAppointment(AppointmentModel lead);
        Task<List<GetAllAppointmentModel>> GetAllAppointment();
        Task<AppointmentModelById> GetAppointmentById(int Appt_Id);
        Task<AppointmentModel> DeleteAppointment(int Appt_Id);
        Task<List<GetDocDD>> GetDoctorDD(string Select_day, string Select_FrmTime, string Select_toTime);

    }
}
