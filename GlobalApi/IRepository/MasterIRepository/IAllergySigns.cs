using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IAllergySigns
    {
        Task<AllergySigns> InsertAllergySigns(AllergySigns lead);
        Task<AllergySigns> UpdateAllergySigns(AllergySigns lead);
        Task<List<AllergySigns>> GetAllAllergySigns();
        Task<List<AllergySigns_DD>> GetAllergySigns_DD();
        Task<AllergySignsBy_Id> GetAllergySignsById(int Al_Id);
        Task<AllergySigns> DeleteAllergySigns(int Al_Id);

    }
}
