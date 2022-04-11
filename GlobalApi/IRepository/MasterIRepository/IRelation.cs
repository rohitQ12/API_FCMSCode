using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IRelation
    {
        Task<Relation> InsertRelation(Relation lead);
        Task<Relation> UpdateRelation(Relation lead);
        Task<List<Relation>> GetAllRelation();
        Task<List<Relation_DD>> GetRelation_DD();
        Task<RelationById> GetRelationById(int relation_id);
        Task<Relation> DeleteRelation(int relation_id);
    }
}
