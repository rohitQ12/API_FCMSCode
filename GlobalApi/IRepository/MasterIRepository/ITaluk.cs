using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ITaluk
    {
        Task<bool> InsertTaluk(Taluk lead);
        Task<bool> UpdateTaluk(Taluk lead);
        Task<List<Taluk_DD>> GetTaluk_DD(int district_id);
        Task<bool> DeleteTaluk(int Taluk_id);
        Task<List<GetTalukDistricts>> GetAllTaluk();
        Task<bool> ApproveTaluk(ApproveTaluk lead);
    }
}
