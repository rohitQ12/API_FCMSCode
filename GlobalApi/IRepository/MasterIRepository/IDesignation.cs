using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDesignation
    {
        Task<string> InsertDesignation(Designation lead);
        Task<string> UpdateDesignation(Designation lead);
        Task<List<GetAllDesignation>> GetAllDesignation();
        Task<List<Designation_DD>> GetDesignation_DD();
        Task<DesignationById> GetDesignationById(int designation_id);
        Task<string> DeleteDesignation(int designation_id);
        Task<string> ApproveDesignation(ApproveDesignation lead);
    }
}
