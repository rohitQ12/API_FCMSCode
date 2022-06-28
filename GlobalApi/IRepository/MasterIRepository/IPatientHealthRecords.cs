using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IPatientHealthRecords
    {
        Task<string> InsertPatientHealthRecords(PHR_Doc lead);
        //Task<string> InsertPatientHealthRecords(List<PHR_Doc> lead, int Appt_Id);
        Task<string> UpdatePatientHealthRecords(PHR_DocUP lead);
        //Task<string> UpdatePatientHealthRecords(List<PatientHealthRecords> lead , int Appt_Id);

        Task<List<GetAllPHR>> GetAllPatientHealthRecords();
        Task<PatientHealthRecords> DeletePatientHealthRecords(int PHR_Id);
        Task<PHRById> GetPatientHealthRecordsById(int PHR_Id);
    }
}
