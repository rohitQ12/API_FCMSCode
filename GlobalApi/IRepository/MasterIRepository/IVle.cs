using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IVle
    {
        Task<Vle> InsertVle(VleModel_Image lead);
        Task<Vle> UpdateVle(VleModel_Image lead);
        Task<List<GetAllVle>> GetAllVle();
        Task<List<Vle_DD>> GetVle_DD();
        Task<VleBy_Id> GetVleById(int VL_Id);
        Task<Vle> DeleteVle(int VL_Id);
        Task<string> ApproveVle(int VL_Id, string? Remarks);

    }
}
