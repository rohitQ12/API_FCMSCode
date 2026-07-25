using GlobalApi.Models.Master;
using System.Collections.Generic;
using static GlobalApi.Models.Master.GetAllCourse_Section;

namespace GlobalApi.IRepository.MasterIReopsitory
{
    public interface Course_MasterIRepository
    {

        Task<string> InsertCourse_Master(InsertCourse_Master insertCourse_Master);
        Task<string> UpdateCourse_Master(UpdateCourse_Master updateCourse_Master);
        Task<List<GetAllCourse_Master>> GetAllCourse_Master();
        Task<List<GetCourse_MasterById>> GetCourse_MasterById(string cu_name);
        Task<List<GetCourse_Master_DD>> GetCourse_Master_DD();
        Task<List<GetCourDD>> GetCoursesDD();
        Task<List<GetSecDD>> GetSecDD();
        Task<List<GetSecDD>> GetSecById(int sec_Id);
        Task<string> DeleteCourse_MasterById(int cu_Id);
        Task<string> ApproveCourse_MasterById(ApproveCourse_Master ApproveCourse_Master);

        Task<List<GetBothDD>> GetAllPackDD();


    }
}
