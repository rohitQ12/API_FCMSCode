using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IIdentity_MST
    {
        Task<Identity_DOC_MST> InsertIdentity_MST(Identity_DOC_MST lead);
        Task<Identity_DOC_MST> UpdateIdentity_MST(Identity_DOC_MST lead);
        Task<List<Identity_DOC_MST>> GetAllIdentity_MST();
        Task<List<IdentityDD>> GetIdentity_MST_DD();
        Task<Identity_DOC_MST> DeleteIdentity_MST(int Country_id);

    }
}
