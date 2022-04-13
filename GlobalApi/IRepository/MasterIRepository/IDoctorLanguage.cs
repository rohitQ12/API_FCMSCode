using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDoctorLanguage
    {
        Task<string> InsertDoctorLanguage(List<DoctorLanguage> lead, int DO_Id);
        Task<DoctorLanguage> UpdateDoctorLanguage(DoctorLanguage lead);
        Task<List<GetDoctorlang>> GetAllDoctorLanguage();
        Task<List<Language_DD>> GetLanguage_DD();
        Task<GetDoctorlang> GetDoctorLanguageById(int Id);
        Task<DoctorLanguage> DeleteDoctorLanguage(int Id);

    }
}
