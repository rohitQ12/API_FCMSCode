using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDoctorLocation
    {
        Task<string> InsertDoctorLocation(List<DoctorLocation> lead, int DO_Id);
        Task<DoctorLocation> UpdateDoctorLocation(List<DoctorLocation> lead, int DO_Id);
        Task<List<GetDoctorLoc>> GetAllDoctorLocation();
        Task<GetDoctorLoc> GetDoctorLocationById(int Id);
        Task<DoctorLocation> DeleteDoctorLocation(int Id);

    }
}
