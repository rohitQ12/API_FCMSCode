using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISymptoms
    {
        Task<string> InsertSymptoms(List<Symptoms> lead, int Appt_Id);
        Task<Symptoms> UpdateSymptoms(Symptoms lead);
        Task<List<GetAllSymptoms>> GetAllSymptoms();
        Task<SymptomsBy_Id> GetSymptomsById(int SYM_Id);
        Task<Symptoms> DeleteSymptoms(int SYM_Id);

    }
}
