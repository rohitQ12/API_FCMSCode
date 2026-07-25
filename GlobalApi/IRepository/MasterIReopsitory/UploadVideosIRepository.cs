using GlobalApi.Models.Master;
using static GlobalApi.Models.Master.vedio_Documents;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface UploadVideosIRepository
    {
        Task<string> InsertCourseVideos(vedio_Documents fileupload);
        Task<List<GetAllVedio_Documents>> GetAllVideo_Documents();
        Task<GetAllVedio_DocumentsById> GetAllVideo_DocumentsById(int vi_id);
        Task<string> DeleteVideo_DocumentsById(int vi_id);
        Task<string> UpdateVideo_DocumentsById(UpdateVedio_DocumentsById lead);
        Task<string> ApproveVideo(Approvevedio_Documents approvevedio_Documents);
    }
}
