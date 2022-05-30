using GlobalApi.Models.Master;
using GlobalApi.Models.Authentication;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IPharmacy
    {
        Task<Pharmacy> InsertPharmacy(Pharmacy_Images lead);
        Task<Pharmacy> UpdatePharmacy(Pharmacy_Images lead);
        Task<List<GetAllPharmacy>> GetAllPharmacy();
        Task<List<Pharmacy_DD>> GetPharmacy_DD();
        Task<PharmacyById> GetPharmacyById(int Ph_Id);
        Task<Pharmacy> DeletePharmacy(int Ph_Id);
        Task<string> ApprovePharmacy(int Ph_Id, string? Remarks);

    }
}
