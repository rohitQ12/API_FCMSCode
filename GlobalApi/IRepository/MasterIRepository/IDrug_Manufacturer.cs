using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDrug_Manufacturer
    {
        Task<List<Drug_Manufacturer>> GetAllDrug_Manufacturer();
        Task<List<Drug_ManufacturerDD>> GetDrug_Manufacturer_DD();
    }
}
