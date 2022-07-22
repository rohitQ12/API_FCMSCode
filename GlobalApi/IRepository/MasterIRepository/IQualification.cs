using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IQualification
    {
        Task<string> InsertQualification(Qualification lead);
        Task<string> UpdateQualification(Qualification lead);
        Task<List<GetAllQualification>> GetAllQualification();
        Task<List<Qualification_DD>> GetQualification_DD();
        Task<QualificationById> GetQualificationById(int qualification_id);
        Task<string> DeleteQualification(int qualification_id);
        Task<string> ApproveQualification(ApproveQualification lead);
    }
}
