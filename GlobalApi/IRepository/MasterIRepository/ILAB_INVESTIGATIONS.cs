using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ILAB_INVESTIGATIONS
    {
        Task<LAB_INVESTIGATIONS> InsertLAB_INVESTIGATIONS(LAB_INVESTIGATIONS lead);
        Task<LAB_INVESTIGATIONS> UpdateLAB_INVESTIGATIONS(LAB_INVESTIGATIONS lead);
        Task<List<LAB_INVESTIGATIONS>> GetLAB_INVESTIGATIONS();
        Task<List<LabInsv_DD>> GetLabInsv_DD();
        Task<LabInsvBy_Id> GetLabInsvBy_Id(int Id);
        Task<LAB_INVESTIGATIONS> DeleteLAB_INVESTIGATIONS(int Id);

    }
}
