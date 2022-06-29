using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IIMG_Description
    {
        Task<IMG_Description> InsertIMG_Description(IMG_Description lead);
        Task<IMG_Description> UpdateIMG_Description(IMG_Description lead);
        Task<List<GetAllIMG_Desc>> GetAllIMG_Description();
        Task<List<Img_Desc_DD>> GetImgDesc_DD();
        Task<IMG_Description> DeleteIMG_Description(int Img_DescId);
        Task<GetImgDesc_ById> GetImgDesc_ById(int Img_DescId);
    }
}
