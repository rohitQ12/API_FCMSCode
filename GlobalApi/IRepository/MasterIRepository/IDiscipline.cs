using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiscipline
    {
        Task<string> InsertDiscipline(Discipline lead);
        Task<string> UpdateDiscipline(Discipline lead);
        Task<List<GetAllDiscipline>> GetAllDiscipline();
        Task<List<Discipline_DD>> GetDiscipline_DD();
        Task<DisciplineById> GetDisciplineById(int CD_Id);
        Task<string> DeleteDiscipline(int CD_Id);
        Task<string> ApproveDiscipline(ApproveDiscipline lead);
    }
}
