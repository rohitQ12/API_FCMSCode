using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISkillSet
    {
        Task<SkillSets> InsertSkillSet(SkillSets lead);
        Task<SkillSets> UpdateSkillSet(SkillSets lead);
        Task<List<Qual_SkillSet>> GetAllSkillSet();
        Task<List<SkillSet_DD>> GetSkillSet_DD(int qualification_Id);
        Task<SkillSets> DeleteSkillSet(int Skillset_id);
        Task<SkillSetById> GetSkillSetById(int Skillset_id);
        Task<string> ApproveSkillSet(int Skillset_id, string? Remarks);
    }
}
