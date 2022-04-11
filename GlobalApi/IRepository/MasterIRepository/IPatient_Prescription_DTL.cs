using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IPatient_Prescription_DTL
    {
        Task<string> InsertPatient_Prescription_DTL(List<Patient_Prescription_DTL> lead, int Rx_Id_FK);
        Task<Patient_Prescription_DTL> UpdatePatient_Prescription_DTL(Patient_Prescription_DTL lead);
        Task<List<GetAllPPD>> GetAllPatient_Prescription_DTL();
        Task<PPD_By_Id> GetPatient_Prescription_DTLById(int Dtl_Id);
        Task<Patient_Prescription_DTL> DeletePatient_Prescription_DTL(int Dtl_Id);

    }
}
