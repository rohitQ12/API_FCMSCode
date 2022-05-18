using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IPharmacyType
    {
        Task<PharmacyType> InsertPharmacyType(PharmacyType lead);
        Task<PharmacyType> UpdatePharmacyType(PharmacyType lead);
        Task<List<PharmacyType>> GetAllPharmacyType();
        Task<List<PhType_DD>> GetPharmacyType_DD();
        //Task<PharmacyTypeBy_Id> GetPharmacyTypeById(int Id);
        Task<PharmacyType> DeletePharmacyType(int Id);

    }
}
