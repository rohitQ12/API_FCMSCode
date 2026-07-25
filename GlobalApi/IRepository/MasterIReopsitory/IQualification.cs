using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IQualification
    {
        Task<string> InsertQualification(Qualification Qualification);
        Task<string> UpdateQualification(Qualification Qualification);
        Task<List<GetAllQualification>> GetAllQualification();
        Task<List<GetAllQualification>> GetAllQualification_Skillset_DD();

        Task<List<Qualification_DD>> GetQualification_DD();
        Task<QualificationById> GetQualificationById(int qualification_id);
        Task<string> DeleteQualification(int qualification_id);
        Task<string> ApproveQualification(ApproveQualification ApproveQualification);


        //Online Portals
        Task<string> InsertQualification_Online(Qualification_Online Qualification_Online);
        Task<string> UpdateQualification_Online(Qualification_Online Qualification_Online);
        Task<List<GetAllQualification_Online>> GetAllQualification_Online();
        Task<List<Qualification_DD_Online>> GetQualification_DD_Online();
        Task<QualificationById_Online> GetQualificationById_Online(int qualification_id);
        Task<string> DeleteQualification_Online(int qualification_id);
        Task<string> ApproveQualification_Online(ApproveQualification_Online ApproveQualification_Online);
    }
}
