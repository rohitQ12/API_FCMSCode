using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface CoursePackageIRepository
    {
        Task<string> InsertCourse_Pack(Course_Pack course_Pack);
        Task<string> UpdateCourse_Pack(Course_Pack updateCourse_Pack);
        Task<List<GetAllCourse_Pack>> GetAllCourse_Pack();
       // Task<List<GetAllCoursePackage>> GetAllCoursePackage();
        Task<List<GetSectionDD>> GetSectionDD();
        Task<List<GetPackageDD>> GetPackageDD();
        Task<List<GetPackageDD>> GetHide();
        Task<GetCourse_PackById> GetCourse_PackById(int cp_id);
        Task<string> DeleteCourse_PackById(int cp_id);
        Task<string> ApproveCourse_PackById(ApproveCourse_Pack approveCourse_Pack);

       // Task<List<GetCranialDD>> GetCardDD(int id);
        Task<List<GetCranialDD>> GetCardDD(int id);
     


    }
}
