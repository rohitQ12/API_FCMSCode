using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IIMG_INVESTIGATIONS
    {
        Task<IMG_INVESTIGATIONS> InsertIMG_INVESTIGATIONS(IMG_INVESTIGATIONS lead);
        Task<IMG_INVESTIGATIONS> UpdateIMG_INVESTIGATIONS(IMG_INVESTIGATIONS lead);
        Task<List<IMG_INVESTIGATIONS>> GetIMG_INVESTIGATIONS();
        Task<List<ImgInsv_DD>> GetImgInsv_DD();
        Task<ImgInsvBy_Id> GetImgInsvBy_Id(int Id);
        Task<IMG_INVESTIGATIONS> DeleteIMG_INVESTIGATIONS(int Id);

    }
}
