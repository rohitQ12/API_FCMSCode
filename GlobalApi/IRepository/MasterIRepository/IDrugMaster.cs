using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDrugMaster
    {
        Task<DrugMaster> InsertDrugMaster(DrugMaster lead);
        Task<DrugMaster> UpdateDrugMaster(DrugMaster lead);
        Task<List<GetAllDrugMaster>> GetAllDrugMaster();
        Task<GetDrugById> GetDrugMasterById(int Id);
        Task<DrugMaster> DeleteDrugMaster(int Id);
        Task<List<DrugTypeDD>> GetDrugTypeDD();
        Task<List<UnitDD>> GetUnitDD(int DT_Id_FK);

    }
}
