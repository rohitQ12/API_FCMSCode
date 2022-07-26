using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDrugMaster
    {
        Task<string> InsertDrugMaster(DrugMaster lead);
        Task<string> UpdateDrugMaster(DrugMaster lead);
        Task<List<GetAllDrugMaster>> GetAllDrugMaster();
        Task<GetAllDrugMaster> GetDrugMasterById(int Id);
        Task<string> DeleteDrugMaster(int Id);
        Task<List<DrugMasterDD>> GetDrugMaster_DD();
        Task<string> ApproveDrugMaster(ApproveDrgMst lead);

    }
}
