using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IPatient
    {
        Task<string> InsertPatient(Patient_Images lead, string UserId,string Create_by);
        Task<string> UpdatePatient(Patient_Images lead);
        Task<List<GetAllPatient>> GetAllPatient(int OfficeRoleId, string Roleaction);
        Task<List<PatientById>> GetPatientById(int PR_Id);
        Task<string> DeletePatient(int PR_Id);
        Task<List<Patient_DD>> GetPatient_DD();
        Task<List<PatientById>> GetPatientByCode(string PR_PatientCode);
    }
}
