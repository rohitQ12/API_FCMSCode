using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDrugMaster
    {
        Task<DrugMaster> InsertDrugMaster(DrugMaster lead);
        Task<DrugMaster> UpdateDrugMaster(DrugMaster lead);
        Task<List<GetAllDrugMaster>> GetAllDrugMaster();
        Task<GetAllDrugMaster> GetDrugMasterById(int Id);
        Task<DrugMaster> DeleteDrugMaster(int Id);
        Task<List<DrugMasterDD>> GetDrugMaster_DD();
        Task<bool> ApproveDrugMaster(ApproveDrgMst lead);

    }
}
