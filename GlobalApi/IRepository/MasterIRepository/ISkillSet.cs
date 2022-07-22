using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISkillSet
    {
        Task<string> InsertSkillSet(SkillSets lead);
        Task<string> UpdateSkillSet(SkillSets lead);
        Task<List<Qual_SkillSet>> GetAllSkillSet();
        Task<List<SkillSet_DD>> GetSkillSet_DD(int qualification_Id);
        Task<string> DeleteSkillSet(int Skillset_id);
        Task<SkillSetById> GetSkillSetById(int Skillset_id);
        Task<string> ApproveSkillSet(ApproveSkillSet lead);
    }
}
