using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ITaluk
    {
        Task<string> InsertTaluk(Taluk Taluk);
        Task<string> UpdateTaluk(Taluk Taluk);
        Task<List<Taluk_DD>> GetTaluk_DD(int district_id);
        Task<string> DeleteTaluk(int Taluk_id);
        Task<List<GetTalukDistricts>> GetAllTaluk();
        Task<string> ApproveTaluk(ApproveTaluk ApproveTaluk);
    }
}
