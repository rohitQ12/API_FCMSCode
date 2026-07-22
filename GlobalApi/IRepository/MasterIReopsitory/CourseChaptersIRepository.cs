using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface CourseChaptersIRepository
    {
         Task<string> InsertCourse_Chapters(InsertCourse_Chapters insertCourse_Chapters);
        Task<string> UpdateCourse_Chapters(UpdateCourse_Chapters updateCourse_Chapters);
        Task<List<GetAllCourse_Chapters>> GetAllCourse_Chapters();
        Task<GetCourseChaptersById> GetCourseChaptersById(int ch_id);
        Task<List<GetCourse_Chapters_DD>> GetCourse_Chapters_DD();
        Task<string> DeleteCourse_ChaptersById(int ch_id);
        Task<string> ApproveCourse_ChaptersById(ApproveCourse_Chapters approveCourse_Chapters);
        Task<List<GetCourseChaptersById>> GetCourseChaptersByName(string ch_name);


    }
}
