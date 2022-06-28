using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDrug_FrequencyRepository
    {
        Task<Drug_Frequency> InsertDrug_Frequency(Drug_Frequency lead);
        Task<Drug_Frequency> UpdateDrug_Frequency(Drug_Frequency lead);
        Task<List<Drug_FrequencyAll>> GetAllDrug_Frequency();
        Task<Drug_Frequency> DeleteDrug_Frequency(int Id);
        Task<List<Drug_FrequencyDD>> GetADrug_Frequency_DD();
        Task<string> ApproveDrug_Frequency(DrugFrequencyapprove lead);
    }
}
