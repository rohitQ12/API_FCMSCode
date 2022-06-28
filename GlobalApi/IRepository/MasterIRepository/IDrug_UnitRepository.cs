using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDrug_UnitRepository
    {
        Task<Drug_Units> InsertDrug_Unit(Drug_Units lead);
        Task<Drug_Units> UpdateDrug_Unit(Drug_Units lead);
        Task<List<Drug_UnitsAll>> GetAllDrug_Unit();
        Task<Drug_Units> DeleteDrug_Unit(int Id);
        Task<List<Drug_UnitDD>> GetDD_Drug_Unit();
        Task<string> ApproveDrug_Unit(ApproveDrgunit lead);
    }
}
