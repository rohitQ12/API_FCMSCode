using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDrug_Prescription
    {
        Task<Drug_Prescription> InserDrug_Prescription(Drug_Prescription lead);
        Task<Drug_Prescription> UpdateDrug_Prescription(Drug_Prescription lead);
        Task<List<Drug_PrescriptionAll>> GetAllDrug_Prescription();
        Task<Drug_Prescription> DeleteDrug_Prescription(int Dtl_Id);
        Task<Drug_PrescriptionAll> GetById_Drug_Prescription(int Prsc_Id);
    }
}
