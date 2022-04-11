using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDesignation
    {
        Task<Designation> InsertDesignation(Designation lead);
        Task<Designation> UpdateDesignation(Designation lead);
        Task<List<Designation>> GetAllDesignation();
        Task<List<Designation_DD>> GetDesignation_DD();
        Task<DesignationById> GetDesignationById(int designation_id);
        Task<Designation> DeleteDesignation(int designation_id);
    }
}
