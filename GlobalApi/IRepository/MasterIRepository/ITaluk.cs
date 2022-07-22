using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ITaluk
    {
        Task<string> InsertTaluk(Taluk lead);
        Task<string> UpdateTaluk(Taluk lead);
        Task<List<Taluk_DD>> GetTaluk_DD(int district_id);
        Task<string> DeleteTaluk(int Taluk_id);
        Task<List<GetTalukDistricts>> GetAllTaluk();
        Task<string> ApproveTaluk(ApproveTaluk lead);
    }
}
