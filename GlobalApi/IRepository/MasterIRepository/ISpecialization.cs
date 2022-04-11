using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISpecialization
    {
        Task<Specialization> InsertSpecialization(Specialization lead);
        Task<Specialization> UpdateSpecialization(Specialization lead);
        Task<List<GetAllSpecialization>> GetAllSpecialization();
        Task<List<Specialization_DD>> GetSpecialization_DD();
        Task<SpecializationById> GetSpecializationById(int SP_Id);
        Task<Specialization> DeleteSpecialization(int SP_Id);

    }
}
