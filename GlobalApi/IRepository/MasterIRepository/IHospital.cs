using GlobalApi.Models.Master;
using GlobalApi.Models.Authentication;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IHospital
    {
        Task<Hospital> InsertHospital(Hospital_Images lead);
        Task<Hospital> UpdateHospital(Hospital_Images lead);
        Task<List<GetAllHospital>> GetAllHospital();
        Task<List<Hospital_DD>> GetHospital_DD();
        Task<HospitalById> GetHospitalById(int Hos_Id);
        Task<Hospital> DeleteHospital(int Hos_Id);
        Task<List<Usercategory_DD>> GetHospitalCategory_DD();

    }
}
