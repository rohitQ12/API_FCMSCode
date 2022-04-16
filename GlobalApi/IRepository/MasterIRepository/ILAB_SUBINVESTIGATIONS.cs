using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ILAB_SUBINVESTIGATIONS
    {
        Task<LAB_SUBINVESTIGATIONS> InsertLAB_SUBINVESTIGATIONS(LAB_SUBINVESTIGATIONS lead);
        Task<LAB_SUBINVESTIGATIONS> UpdateLAB_SUBINVESTIGATIONS(LAB_SUBINVESTIGATIONS lead);
        Task<List<GetLabSubInsv>> GetLAB_SUBINVESTIGATIONS();
        Task<List<LabSubInsv_DD>> GetLabSubInsv_DD(int Lab_Invst_Id);
        Task<LabSubInsvBy_Id> GetLabSubInsvBy_Id(int Id);
        Task<LAB_SUBINVESTIGATIONS> DeleteLAB_SUBINVESTIGATIONS(int Id);

    }
}
