using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IQualification
    {
        Task<bool> InsertQualification(Qualification lead);
        Task<bool> UpdateQualification(Qualification lead);
        Task<List<GetAllQualification>> GetAllQualification();
        Task<List<Qualification_DD>> GetQualification_DD();
        Task<QualificationById> GetQualificationById(int qualification_id);
        Task<bool> DeleteQualification(int qualification_id);
        Task<bool> ApproveQualification(ApproveQualification lead);
    }
}
