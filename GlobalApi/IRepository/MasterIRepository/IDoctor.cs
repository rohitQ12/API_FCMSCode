using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDoctor
    {
        Task<Doctor> InsertDoctor(Doctor_Images lead, string UserId);
        Task<Doctor> UpdateDoctor(Doctor_ImagesUP lead);
        Task<List<GetAllDoctor>> GetAllDoctor(int? DO_HO_Id_FK, string roleaction);
        Task<DoctorById> GetDoctorById(int DO_Id);
        Task<Doctor> DeleteDoctor(int DO_Id);
        Task<List<Doctor_DD>> Doctor_DD(int SP_Id);
        Task<string> ApproveDoctor(ApproveDoctor lead);

    }
}
