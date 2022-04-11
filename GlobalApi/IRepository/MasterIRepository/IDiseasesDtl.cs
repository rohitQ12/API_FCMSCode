using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiseasesDtl
    {
        Task<string> InsertDiseasesDtl(List<DiseasesDtl> lead, int Appt_Id);
        Task<DiseasesDtl> UpdateDiseasesDtl(DiseasesDtl lead);
        Task<List<GetAllDiseasesDtl>> GetAllDiseasesDtl();
        Task<GetDiseaseDtlById> GetDiseasesDtlById(int Id);
        Task<DiseasesDtl> DeleteDiseasesDtl(int Id);

    }
}
