using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IImgTest
    {
        Task<ImgTest> InsertImgTest(ImgTest_Details lead);
        Task<bool> AcceptImgTest(int Id, int Img_Id_FK, bool AcceptImgTest);
        Task<ImgTest> UpdateImgTest(ImgTest lead);
        Task<List<GetAllImgTest>> GetAllImgTest();
        Task<ImgTestById> GetImgTestById(int Id);
        Task<ImgTest> DeleteImgTest(int Id);

    }
}
