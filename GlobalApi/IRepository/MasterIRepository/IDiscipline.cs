using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiscipline
    {
        Task<Discipline> InsertDiscipline(Discipline lead);
        Task<Discipline> UpdateDiscipline(Discipline lead);
        Task<List<Discipline>> GetAllDiscipline();
        Task<List<Discipline_DD>> GetDiscipline_DD();
        Task<DisciplineById> GetDisciplineById(int CD_Id);
        Task<Discipline> DeleteDiscipline(int CD_Id);

    }
}
