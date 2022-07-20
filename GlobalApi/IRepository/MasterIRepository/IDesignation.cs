using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDesignation
    {
        Task<bool> InsertDesignation(Designation lead);
        Task<bool> UpdateDesignation(Designation lead);
        Task<List<GetAllDesignation>> GetAllDesignation();
        Task<List<Designation_DD>> GetDesignation_DD();
        Task<DesignationById> GetDesignationById(int designation_id);
        Task<bool> DeleteDesignation(int designation_id);
        Task<bool> ApproveDesignation(ApproveDesignation lead);
    }
}
