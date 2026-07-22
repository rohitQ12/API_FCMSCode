using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDistrict
    {
        Task<string> InsertDistrict(Districts Districts);
        Task<string> UpdateDistrict(Districts Districts);
        Task<List<District_DD>> GetDistrict_DD(int stat_id);
        Task<string> DeleteDistrict(int district_id);
        Task<DistrictById> GetDistrictById(int district_id);
        Task<List<GetDistrictState>> GetAllDistrict();
        Task<string> ApproveDistrict(ApproveDistrict ApproveDistrict);
    }
}
