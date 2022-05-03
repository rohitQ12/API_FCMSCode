using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ITaluk
    {
        Task<Taluk> InsertTaluk(Taluk lead);
        Task<Taluk> UpdateTaluk(Taluk lead);
        Task<List<Taluk_DD>> GetTaluk_DD(int district_id);
        Task<Taluk> DeleteTaluk(int Taluk_id);
        Task<List<GetTalukDistricts>> GetAllTaluk();
    }
}
