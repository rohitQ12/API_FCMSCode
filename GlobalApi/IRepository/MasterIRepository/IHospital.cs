using GlobalApi.Models.Master;
using GlobalApi.Models.Authentication;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IHospital
    {
        Task<Hospital> InsertHospital(Hospital_Images lead);
        Task<Hospital> UpdateHospital(Hospital_Images lead);
        Task<List<GetAllHospital>> GetAllHospital();
        Task<List<GetAllHospital>> GetAllHospitaltest(int? Hos_Id, string test);
        Task<List<Hospital_DD>> GetHospital_DD(int? Hos_Id, string roleaction);
        Task<HospitalById> GetHospitalById(int? Hos_Id, string roleaction);
        Task<Hospital> DeleteHospital(int Hos_Id);
        Task<List<Usercategory_DD>> GetHospitalCategory_DD();
        Task<List<NetworkHospital_DD>> GetNetworkHospital_DD(int Hos_id);
        Task<string> ApproveHospital(int Hos_Id, string? Remarks);

    }
}
