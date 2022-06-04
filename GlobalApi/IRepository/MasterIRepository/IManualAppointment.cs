using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IManualAppointment
    {
        Task<ManualAppointment> InsertAppointment(InsertManualApptDetails lead, int Appt_PatientId, string UserId);
        Task<UsersLists> InsertUsers(ManualAppointment lead);
        Task<ManualAppointment> ApproveAppointment(ApprovePhcAppointment lead);
        Task<ManualAppointment> RejectAppointment(int MAppt_Id);
        Task<string> UpdateAppointment(InsertManualApptDetails lead);
        Task<Parameters> UpdateParameters(InsertManualApptDetails lead);
        Task<List<GetAllManualAppointment>> GetAllAppointment();
        Task<ManualAppointment> DeleteAppointment(int MAppt_Id);
        Task<List<ManualAppointmentById>> GetAdminAppointmentById(int MAppt_Id);
        //Task<List<GetHosDD>> GetHospital_DD(int PR_Id);
        Task<List<GetHosDD>> GetHospital_DD();

    }
}
