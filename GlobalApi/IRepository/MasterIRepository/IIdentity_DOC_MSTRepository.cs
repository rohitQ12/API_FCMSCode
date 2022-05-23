using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IIdentity_DOC_MSTRepository
    {
        Task<List<Identity_DOC_MST>> GetAllIdentity();
        Task<List<IdentityDD>> GetIdentityDD();
    }
}
