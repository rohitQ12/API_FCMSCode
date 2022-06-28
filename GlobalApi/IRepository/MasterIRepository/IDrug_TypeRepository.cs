using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDrug_TypeRepository
    {
        Task<Drug_Type> InsertDrug_Type(Drug_Type lead);
        Task<Drug_Type> UpdateDrug_Type(Drug_Type lead);
        Task<List<Drug_TypeAll>> GetAllDrug_Type();
        Task<Drug_Type> DeleteDrug_Type(int Id);
        Task<List<Drug_TypeDD>> GetDrug_Type_DD();
        Task<string> ApproveDrug_Type(DrugTypeapprove lead);

    }
}
