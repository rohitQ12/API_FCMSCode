using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ILAB_Description
    {
        Task<LAB_Description> InsertLab_Description(LAB_Description lead);
        Task<LAB_Description> UpdateLab_Description(LAB_Description lead);
        Task<List<GetAllLAB_Desc>> GetAllLab_Description();
        Task<List<LabDesc_DD>> GetLabDesc_DD();
        Task<List<LabDesc_DD>> LabDesc_DD_ByCat_Id(int Cat_Id);
        Task<LAB_Description> DeleteLab_Description(int LAB_DescId);
        Task<GetLabDesc_ById> GetLabDesc_ById(int Lab_DescId);
        Task<string> ApproveLAB_Description(ApproveLab_Desc lead);
    }
}
