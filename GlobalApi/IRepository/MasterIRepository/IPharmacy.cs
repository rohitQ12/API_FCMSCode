using GlobalApi.Models.Master;
using GlobalApi.Models.Authentication;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IPharmacy
    {
        Task<Pharmacy> InsertPharmacy(Pharmacy_Images lead);
        Task<Pharmacy> UpdatePharmacy(Pharmacy_Images lead);
        Task<List<GetAllPharmacy>> GetAllPharmacy(int? PharmacyId, string roleaction);
        Task<List<Pharmacy_DD>> GetPharmacy_DD(int? PharmacyId, string roleaction);
        Task<PharmacyById> GetPharmacyById(int Ph_Id, string roleaction);
        Task<Pharmacy> DeletePharmacy(int Ph_Id);
        Task<List<Usercategory_DD>> GetPharmacyCategory_DD();

    }
}
