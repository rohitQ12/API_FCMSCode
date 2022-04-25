using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDoctor
    {
        Task<Doctor> InsertDoctor(Doctor_Images lead);
        Task<Doctor> UpdateDoctor(Doctor_Images lead);
        Task<List<GetAllDoctor>> GetAllDoctor();
        Task<DoctorById> GetDoctorById(int DO_Id);
        Task<Doctor> DeleteDoctor(int DO_Id);
        Task<List<Doctor_DD>> Doctor_DD(int SP_Id);

    }
}
