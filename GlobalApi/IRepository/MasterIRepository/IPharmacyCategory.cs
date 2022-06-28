using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IPharmacyCategory
    {
        Task<PharmacyCategory> InsertPharmacyCategory(PharmacyCategory lead);
        Task<PharmacyCategory> UpdatePharmacyCategory(PharmacyCategory lead);
        Task<List<PharmacyCategory>> GetAllPharmacyCategory();
        Task<List<Pharma_DD>> GetPharmacyCategory_DD();
        //Task<PharmacyCategoryBy_Id> GetPharmacyCategoryById(int Id);
        Task<PharmacyCategory> DeletePharmacyCategory(int Id);

    }
}
