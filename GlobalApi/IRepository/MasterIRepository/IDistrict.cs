using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDistrict
    {
        Task<bool> InsertDistrict(Districts lead);
        Task<bool> UpdateDistrict(Districts lead);
        Task<List<District_DD>> GetDistrict_DD(int stat_id);
        Task<bool> DeleteDistrict(int district_id);
        Task<DistrictById> GetDistrictById(int district_id);
        Task<List<GetDistrictState>> GetAllDistrict();
        Task<bool> ApproveDistrict(ApproveDistrict lead);
    }
}
