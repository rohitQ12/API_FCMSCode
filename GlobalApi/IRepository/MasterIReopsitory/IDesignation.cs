using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDesignation
    {
        Task<string> InsertDesignation(Designation Designation);
        Task<string> UpdateDesignation(Designation Designation);
        Task<List<GetAllDesignation>> GetAllDesignation();
        Task<List<Designation_DD>> GetDesignation_DD();
        Task<DesignationById> GetDesignationById(int designation_id);
        Task<string> DeleteDesignation(int designation_id);
        Task<string> ApproveDesignation(ApproveDesignation ApproveDesignation);


        //Online Portals
        Task<string> InsertDesignation_Online(Designation_Online Designation_Online);
        Task<string> UpdateDesignation_Online(Designation_Online Designation_Online);
        Task<List<GetAllDesignation_Online>> GetAllDesignation_Online();
        Task<List<Designation_DD_Online>> GetDesignation_DD_Online();
        Task<DesignationById_Online> GetDesignationById_Online(int designation_id);
        Task<string> DeleteDesignation_Online(int designation_id);
        Task<string> ApproveDesignation_Online(ApproveDesignation_Online ApproveDesignation_Online);
    }
}
