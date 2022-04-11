using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IIMG_SUBINVESTIGATIONS
    {
        Task<IMG_SUBINVESTIGATIONS> InsertIMG_SUBINVESTIGATIONS(IMG_SUBINVESTIGATIONS lead);
        Task<IMG_SUBINVESTIGATIONS> UpdateIMG_SUBINVESTIGATIONS(IMG_SUBINVESTIGATIONS lead);
        Task<List<GetImgSubInsv>> GetIMG_SUBINVESTIGATIONS();
        Task<List<ImgSubInsv_DD>> GetImgSubInsv_DD();
        Task<ImgSubInsvBy_Id> GetImgSubInsvBy_Id(int Id);
        Task<IMG_SUBINVESTIGATIONS> DeleteIMG_SUBINVESTIGATIONS(int Id);

    }
}
