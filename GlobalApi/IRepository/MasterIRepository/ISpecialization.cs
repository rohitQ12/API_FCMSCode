using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISpecialization
    {
        Task<string> InsertSpecialization(Specialization lead);
        Task<string> UpdateSpecialization(Specialization lead);
        Task<List<GetAllSpecialization>> GetAllSpecialization();
        Task<List<Specialization_DD>> GetSpecialization_DD(int CD_Id);
        Task<SpecializationById> GetSpecializationById(int SP_Id);
        Task<string> DeleteSpecialization(int SP_Id);
        Task<string> ApproveSpecialization(ApproveSpecialization lead);
    }
}
