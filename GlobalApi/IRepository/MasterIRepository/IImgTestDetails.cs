using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IImgTestDetails
    {
        Task<string> InsertImgTestDetails(List<ImgTestDetails> lead, int Img_Id_FK);
        Task<ImgTestDetails> UpdateImgTestDetails(ImgReport lead);
        Task<ImgTestDetails> DeleteImgTestDetails(int Id);
        Task<List<GetAllImgTestDetails>> GetAllImgTestDetails();
        Task<ImgTestDetailsById> GetImgTestDetailsById(int Id);

    }
}
