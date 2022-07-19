using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IManualAppointment
    {
        Task<AppointmentModel> InsertAppointment(InsertDetails lead, int Appt_PatientId, string UserId);
        Task<string> UpdateAppointment(InsertDetails lead);
        Task<List<GetAllAppointmentModel>> GetAllAppointment(int HospitalId, int DoctorId, string roleaction, string rolename);
        Task<List<AppointmentModelById>> GetAppointmentById(int Appt_Id);
        Task<AppointmentModel> DeleteAppointment(int Appt_Id);
        Task<List<GetDocDD>> GetDoctorDD(string Select_day, string Select_FrmTime, string Select_toTime);
        Task<string> ApproveAppointment(ApproveAppointment lead);
        Task<AppointmentModel> RejectAppointment(int Appt_Id);

    }
}
