using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IQualification
    {
        Task<Qualification> InsertQualification(Qualification lead);
        Task<Qualification> UpdateQualification(Qualification lead);
        Task<List<Qualification>> GetAllQualification();
        Task<List<Qualification_DD>> GetQualification_DD();
        Task<QualificationById> GetQualificationById(int qualification_id);
        Task<Qualification> DeleteQualification(int qualification_id);
    }
}
