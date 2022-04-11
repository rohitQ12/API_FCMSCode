using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IPatientRxDetails
    {
        Task<PatientRxDetails> InsertPatientRxDetails(Prescription_Details lead);
        Task<bool> AcceptPatientRxDetails(int Rx_Id, int Rx_CON_Id_FK, int AcceptPrescription);
        Task<PatientRxDetails> UpdatePatientRxDetails(PatientRxDetails lead);
        Task<List<GetAllPatientRxDetails>> GetAllPatientRxDetails();
        Task<PatientRxDetailsById> GetPatientRxDetailsById(int Rx_Id);
        Task<PatientRxDetails> DeletePatientRxDetails(int Rx_Id);
        Task<List<GetDrugForSpeedSearch>> GetDrugForSpeedSearch(string EnteredText);

    }
}
