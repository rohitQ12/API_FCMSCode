using GlobalApi.Models.Master;
using static GlobalApi.Models.Master.GetAllCourse_Section;


namespace GlobalApi.IRepository.MasterIReopsitory
{
    public interface Course_SectionIRepository
    {
        Task<string> InsertCourse_Section(InsertCourse_Section insertCourse_Section);
        Task<string> UpdateCourse_Section(UpdateCourse_Section updateCourse_Section);
        Task<List<GetAllCourse_Section>> GetAllCourse_Sections();
        Task<GetCourseSectionById> GetCourseSectionById(int sc_Id);
        Task<List<GetCranialSectionDD>> GetCranialSectionDD();
        Task<List<GetCranialSectionDD>> GetSpinalSectionDD();

        Task<List<GetCourse_Section_DD>> GetCourse_Section_DD();
        Task<string> DeleteCourse_SectionById(int sc_Id);
        Task<string> ApproveCourse_SectionById(ApproveCourse_Section approveCourse_Section);

        Task<List<GetCourseSectionById>> GetCourseSectionByName(string sc_name);

    }

}
