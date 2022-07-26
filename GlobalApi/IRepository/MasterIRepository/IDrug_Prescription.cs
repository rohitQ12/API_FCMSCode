using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDrug_Prescription
    {
        Task<string> InserDrug_Prescription(Drug_Prescription lead);
        Task<string> UpdateDrug_Prescription(Drug_Prescription lead);
        Task<List<Drug_PrescriptionAll>> GetAllDrug_Prescription();
        Task<string> DeleteDrug_Prescription(int Dtl_Id);
        Task<List<Drug_PrescriptionAll>> GetById_Drug_Prescription(int Cons_Id);
    }
}
