using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IPHC_Appointment
    {
        Task<PHC_Appointment> InsertPHCAppointment(InsertPHCApptDetails lead, int Appt_PatientId, string UserId);
        Task<UsersLists> InsertUsers(PHC_Appointment lead);
        Task<string> ApprovePHCAppointment(ApprovePhcAppointment lead);
        Task<PHC_Appointment> RejectPHCAppointment(int Appt_Id);
        Task<string> UpdatePHCAppointment(InsertPHCApptDetails lead);
        Task<Parameters> UpdateParameters(InsertPHCApptDetails lead);
        Task<List<GetAllPHC_Appointment>> GetAllPHCAppointment(int HospitalId, int DoctorId, string roleaction, string rolename);
        Task<PHC_Appointment> DeletePHCAppointment(int Phc_Appt_Id);
        Task<List<PHC_AppointmentById>> GetPHCAppointmentById(int Phc_Appt_Id);
        //Task<List<GetHosDD>> GetHospital_DD(int PR_Id);
        Task<List<GetHosDD>> GetHospital_DD();

    }
}
