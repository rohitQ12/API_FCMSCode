using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDistrict
    {
        Task<Districts> InsertDistrict(Districts lead);
        Task<Districts> UpdateDistrict(Districts lead);
        Task<List<District_DD>> GetDistrict_DD(int stat_id);
        Task<Districts> DeleteDistrict(int district_id);
        Task<DistrictById> GetDistrictById(int district_id);
        Task<List<GetDistrictState>> GetAllDistrict();
        Task<string> ApproveDistrict(ApproveDistrict lead);
    }
}
