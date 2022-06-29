using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISymptoms
    {
        Task<string> InsertSymptoms(List<Symptoms> lead, int Appt_Id);
        Task<string> InsertPHCSymptoms(List<Symptoms> lead, int Appt_Id);

        Task<bool> UpdateSymptomstest(List<Symptoms> lead, int Appt_Id);
        Task<bool> UpdatePHCSymptoms(List<Symptoms> lead, int Appt_Id);

        Task<List<GetAllSymptoms>> GetAllSymptoms();
        Task<List<GetAllSymptoms>> GetAllPHCSymptoms();

        Task<List<SymptomsBy_Id>> GetSymptomsById(int SYM_APPT_PR_Id_FK);
        Task<Symptoms> DeleteSymptoms(int SYM_Id);

    }
}
