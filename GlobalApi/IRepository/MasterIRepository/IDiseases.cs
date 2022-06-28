using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiseases
    {
        Task<Diseases> InsertDiseases(Diseases lead);
        Task<Diseases> UpdateDiseases(Diseases lead);
        Task<List<GetAllDiseases>> GetAllDiseases();
        Task<List<Diseases_DD>> GetDiseases_DD();
        Task<DiseasesBy_Id> GetDiseasesById(int Id);
        Task<Diseases> DeleteDiseases(int Id);

    }
}

