using GlobalApi.Models.Master;
using GlobalApi.Models.Master.YourNamespace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IStudent
    {
        //Task<string> InsertStudent(Student_Images lead, string UserId);

        Task<string> InsertStudent(Student_Images lead);

        Task<string> UpdateStudent(Student_Images lead);
        Task<List<GetAllStudent>> GetAllStudent();
        //Task<List<Assistant_DD>> GetAssistant_DD(int? Assi_Hos_Id_FK, string roleaction);
        Task<StudentById> GetStudentById(int Stu_Id, string roleaction);
        Task<string> DeleteStudent(int Stu_id);
        Task<dynamic> GetStudentDetails();
        Task<string> ApproveStudent(ApproveStudent approveStudent);
        //Task GetAllMCQByTestIdAsync(int testId);




        //// new MCQ APIs
        ///

        Task<List<QuestionDto>> GetAllMCQQuestions();
        Task<List<TestDto>> GetAllMCQTests();
        Task<TestDto> GetAllMCQByTestId(int testId);
        Task<TestResultDetailDto> SubmitMCQTest(SubmitTestRequest request);
        Task<List<TestResultDto>> GetAllTestResultsByStudentId(string studentId);

        Task<List<TestResultDto>> GetAllStudentResultsAsync(string loggedInUserId);

        Task<StudentTestAttemptDto> CheckMultipleTestAttemptsAsync(string studentId);

        //Task<AdminDashboardDto> GetAdminDashboardStatus(string loggedInUserId);
        Task<AdminDashboardDto> GetAdminDashboardStatus(string loggedInUserId, int TestId);

        Task<List<ExamMasterDto>> GetAllExams();
        Task<List<ExamCategoryDto>> GetExamCategoriesByExamId(int ExamId);

        Task<List<ExamSubCategoryDto>> GetExamSubCategoriesByCategoryId(int ExamCateId);
        Task<List<TestDtos>> GetTestsByExamSubCateId(int ExamSubCateId);

        Task<List<TestDtos_Category>> GetTestsByExamCateId(int ExamCateId);

        Task<List<DetailedTestResultDto>> GetDetailedTestResults(string StudentId);

        //Task<StudentWarningDto> UpdateStudentWarningCount(string StudentId, int TestId);
        Task<List<DetailedTestResultModel>> GetDetailedTestResultsByAdmin(string loggedInUserId);
        Task<List<CertificateInfoDto>> GetCertificateInfoByStudentId(string StudentId);
        Task<TestsCountDto> GetTestsCountByStudentId(string StudentId);

        Task<List<StateDto>> GetAllStates();
        Task<List<ZoneDto>> GetZonesByStateId(int StateId);
        Task<List<DistrictDto>> GetDistrictsByZoneId(int ZoneId);
        Task<List<ExamCategoryTestDto>> GetExamCategoryTestList();
        Task<List<QuestionViewModel>> GetQuestionsByStudentIdAsync(string studentUserId);

        Task<(List<SectionSummaryViewModel> Sections, OverallSectionSummaryViewModel Overall)> GetSectionWiseSummaryAsync(string loggedInUserId, int testId, string studentId);

        //Task<List<OverallSummaryViewModel>> GetOverallSummaryAsync(string loggedInUserId, int testId, string studentId);
        Task<OverallSummaryResponse> GetOverallSummaryAsync(string loggedInUserId, int testId, string studentId);
        //Task<List<SectionSummaryViewModel>> GetSectionWiseSummaryAsync(string loggedInUserId, int testId, string studentId);
        Task<List<DifficultyLevelSummaryViewModel>> GetDifficultyLevelSummaryAsync(string loggedInUserId, int testId, string studentId);
        Task<List<QuestionWiseViewModel>> GetQuestionWiseDetailsAsync(string loggedInUserId, int testId, string studentId);
        Task<List<TopStudentDto>> GetTop10Students(string loggedInUserId, int? stateId = null, int? zoneId = null, int? districtId = null);

        Task<List<StudentDetailsDto>> GetStudentDetailsByUserId(string stuUserId);

        Task<List<TestRankedResultDto>> GetRankedTestResultsByTestId(int testId);
        Task<StudentRankDto?> GetStudentRankByTestId(int testId, string studentId);
        Task<List<ExamCategoryTestViewModel>> GetExamCategoryByStudId(string StudentId);



    }
}
