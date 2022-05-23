using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiseasesDtl
    {
        Task<string> InsertDiseasesDtl(List<DiseasesDtl> lead, int Appt_Id);
        Task<string> InsertManualDiseasesDtl(List<DiseasesDtl> lead, int MAppt_Id);
        Task<bool> UpdateDiseasesDtltest(List<DiseasesDtl> lead, int Appt_Id);
        Task<bool> UpdateManualDiseasesDtl(List<DiseasesDtl> lead, int MAppt_Id);
        Task<List<GetAllDiseasesDtl>> GetAllDiseasesDtl();
        Task<List<GetAllDiseasesDtl>> GetAllManualDiseasesDtl();
        Task<List<GetDiseaseDtlById>> GetDiseasesDtlById(int Ddtl_PR_Id_FK);
        Task<DiseasesDtl> DeleteDiseasesDtl(int Id);

    }
}
