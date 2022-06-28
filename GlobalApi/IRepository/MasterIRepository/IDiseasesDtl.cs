using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiseasesDtl
    {
        Task<string> InsertDiseasesDtl(List<DiseasesDtl> lead, int Appt_Id);
        Task<string> InsertPHCDiseasesDtl(List<DiseasesDtl> lead, int Appt_Id);
        Task<bool> UpdateDiseasesDtltest(List<DiseasesDtl> lead, int Appt_Id);
        Task<bool> UpdatePHCDiseasesDtl(List<DiseasesDtl> lead, int Appt_Id);
        Task<List<GetAllDiseasesDtl>> GetAllDiseasesDtl();
        Task<List<GetAllDiseasesDtl>> GetAllPHCDiseasesDtl();
        Task<List<GetDiseaseDtlById>> GetDiseasesDtlById(int Ddtl_PR_Id_FK);
        Task<DiseasesDtl> DeleteDiseasesDtl(int Id);

    }
}
