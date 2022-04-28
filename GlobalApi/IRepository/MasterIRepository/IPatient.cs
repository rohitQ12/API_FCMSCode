using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IPatient
    {
        Task<Patient> InsertPatient(Patient_Images lead, string UserId);
        Task<Patient> UpdatePatient(Patient_Images lead);
        Task<List<GetAllPatient>> GetAllPatient();
        Task<List<PatientById>> GetPatientById(int PR_Id);
        Task<Patient> DeletePatient(int PR_Id);
    }
}
