using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IAppointment
    {
        Task<AppointmentModel> InsertAppointment(InsertDetails lead, int Appt_PatientId);
        Task<AppointmentModel> UpdateAppointment(AppointmentModel lead);
        Task<List<GetAllAppointmentModel>> GetAllAppointment();
        Task<List<AppointmentModelById>> GetAppointmentById(int Appt_PatientId_FK);
        Task<AppointmentModel> DeleteAppointment(int Appt_Id);
        Task<List<GetDocDD>> GetDoctorDD(string Select_day, string Select_FrmTime, string Select_toTime);
        Task<AppointmentModel> ApproveAppointment(int Appt_Id);
        Task<AppointmentModel> InsertApptBasedOnSymptoms(ApptonDiffCategory lead, int Appt_PatientId, int SYM_MST_Id_FK);
        Task<AppointmentModel> InsertApptBasedOnDisease(ApptonDiffCategory lead, int Appt_PatientId, int Dis_Id_FK);
        Task<AppointmentModel> InsertApptBasedOnDoctor(ApptonDoctor lead, int Appt_PatientId, int DO_Id);
        Task<AppointmentModel> InsertApptBasedOnSpecalization(ApptonSpecalization lead, int Appt_PatientId, int SP_Id);
    }
}
