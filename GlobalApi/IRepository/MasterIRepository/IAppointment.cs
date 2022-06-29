using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IAppointment
    {
        Task<AppointmentModel> InsertAppointment(InsertDetails lead, int Appt_PatientId,string UserId);
        //Task<AppointmentModel> UpdateAppointment(InsertDetails lead);
        Task<string> UpdateAppointment(InsertDetails lead);
        Task<List<GetAllAppointmentModel>> GetAllAppointment(int? HospitalId, string roleaction);
        Task<List<AppointmentModelById>> GetAppointmentById(int Appt_PatientId_FK);
        //Task<List<AppointmentModelById>> GetAdminAppointmentById(int Appt_Id);
        Task<AppointmentModel> DeleteAppointment(int Appt_Id);
        Task<List<GetDocDD>> GetDoctorDD(string Select_day, string Select_FrmTime, string Select_toTime);
        Task<AppointmentModel> ApproveAppointment(ApproveAppointment lead);
        Task<AppointmentModel> RejectAppointment(int Appt_Id);
        Task<AppointmentModel> InsertApptBasedOnSymptoms(ApptonDiffCategory lead, int Appt_PatientId, int Smst_Id);
        Task<AppointmentModel> InsertApptBasedOnDisease(ApptonDiffCategory lead, int Appt_PatientId, int Id);
        Task<AppointmentModel> InsertApptBasedOnDoctor(ApptonDoctor lead, int Appt_PatientId, int DO_Id);
        Task<AppointmentModel> InsertApptBasedOnSpecalization(ApptonSpecalization lead, int Appt_PatientId, int SP_Id);
        Task<List<GetDocDD>> GetDoctorDDOnSpec(int Sp_Id, string Select_day, string Select_FrmTime, string Select_toTime);




    }
}
