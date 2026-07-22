using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiscipline
    {
        Task<string> InsertDiscipline(Discipline Discipline);
        Task<string> UpdateDiscipline(Discipline Discipline);
        Task<List<GetAllDiscipline>> GetAllDiscipline();
        Task<List<Discipline_DD>> GetDiscipline_DD();
        Task<DisciplineById> GetDisciplineById(int CD_Id);
        Task<string> DeleteDiscipline(int CD_Id);
        Task<string> ApproveDiscipline(ApproveDiscipline ApproveDiscipline);

        //online portals
        Task<string> InsertDiscipline_Online(Discipline_Online Discipline_Online);
        Task<string> UpdateDiscipline_Online(Discipline_Online Discipline_Online);
        Task<List<GetAllDiscipline_Online>> GetAllDiscipline_Online();
        Task<List<Discipline_DD_Online>> GetDiscipline_DD_Online();
        Task<DisciplineById_Online> GetDisciplineById_Online(int cd_id);
        Task<string> DeleteDiscipline_Online(int cd_id);
        Task<string> ApproveDiscipline_Online(ApproveDiscipline_Online ApproveDiscipline_Online);

    }

}