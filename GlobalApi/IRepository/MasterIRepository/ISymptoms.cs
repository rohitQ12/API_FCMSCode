using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISymptoms
    {
        Task<string> InsertSymptoms(List<Symptoms> lead, int Appt_Id);
        //Task<Symptoms> UpdateSymptoms(Symptoms lead);
        Task<bool> UpdateSymptomstest(List<Symptoms> lead, int Appt_Id);
        Task<List<GetAllSymptoms>> GetAllSymptoms();
        Task<List<SymptomsBy_Id>> GetSymptomsById(int SYM_APPT_PR_Id_FK);
        Task<Symptoms> DeleteSymptoms(int SYM_Id);
        //Task<Symptoms> InsertApptSymptoms(List<Symptoms> lead, int Appt_Id, int SYM_MST_Id_FK);

    }
}
