using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.AdminIRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly IStudent _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public readonly IUserRepository userRepository;
        public readonly IAuthenticationRepository authrepository;
        public StudentController(IAuthenticationRepository authrepository, IUserRepository userRepository)
        {
            this._repository = new StudentRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
            this.authrepository = authrepository;
            this.userRepository = userRepository;
        }

        [HttpPost, Route("InsertStudent")]
        public async Task<IActionResult> Post([FromBody] Student_Images lead)
        {
            //var username = "8073043647";
            //  var username = User.Identity.Name;
            // var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            // IfClaimExists = claims.Any(x => x.ClaimType == "StudentAdd" && x.ClaimValue == "Y");
            //if (IfClaimExists)
            //{
            var change = await _repository.InsertStudent(lead);
            if (change == "Student registration successfully")
            {
                return Ok();
            }
            //var delete = await this.findUserId.Deleteuser(result.userid);
            return BadRequest(change);
            //}
            //return Unauthorized();

        }








        [HttpPut, Route("UpdateStudent")]
        public async Task<IActionResult> Put([FromBody] Student_Images lead)
        {
            //var username = "8073043647";
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "StudentEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateStudent(lead);
                if (change == "Student Updated Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }


        [HttpGet, Route("GetAllStudent")]
        public async Task<IActionResult> GetAllstudent()
        {
            var userName = "8778650328";
            //var userName = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
            IfClaimExists = claims.Any(x => x.ClaimType == "StudentView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {

                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var result = await this._repository.GetAllStudent();
                //return Ok(result);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound("student data not found");

            }
            return Unauthorized();

        }


        //[HttpGet, Route("GetAssistant_DD")]
        //public async Task<IActionResult> GetAssistant_DD()
        //{

        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "AssistantView" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var userName = User.Identity.Name.ToString();
        //        var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
        //        var Assi_Hos_Id_FK = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
        //        var result = await this._repository.GetAssistant_DD(Assi_Hos_Id_FK, roleaction);
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound("Assistant data not found");
        //    }
        //    return Unauthorized();


        //}


        [HttpDelete, Route("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int Student_id)
        {
            //var userName = "8073043647";
            var userName = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
            IfClaimExists = claims.Any(x => x.ClaimType == "StudentDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteStudent(Student_id);
                if (change == "Student Deleted Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }


        [HttpGet, Route("GetStudentById")]
        public async Task<IActionResult> GetStudentById(int student_id)
        {
            //var userName = "8073043647";
            var userName = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
            IfClaimExists = claims.Any(x => x.ClaimType == "StudentView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {

                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var result = await this._repository.GetStudentById(student_id, roleaction);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Student data not found");
            }
            return Unauthorized();
        }

        [HttpGet, Route("GetStudentDetails")]
        public async Task<IActionResult> GetStudentDetails()
        {
            //var userName = "8073043647";
            var userName = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
            IfClaimExists = claims.Any(x => x.ClaimType == "StudentView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var result = await this._repository.GetStudentDetails();
                return Ok(result);

            }
            return Unauthorized();
        }

        [HttpPut, Route("ApproveStudent")]
        public async Task<IActionResult> ApproveStudent([FromBody] ApproveStudent approveStudent)
        {
            //var userName = "8073043647";
            var userName = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
            IfClaimExists = claims.Any(x => x.ClaimType == "StudentApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveStudent(approveStudent);
                if (change == "Student Approved Successfully")
                {
                    return Ok();
                }
            }
            return Unauthorized();
        }





        //// New MCQ APIs
        ///

        //[HttpGet, Route("GetAllMCQ/{testId}")]
        //public async Task<IActionResult> GetAllMCQ(int testId)
        //{
        //   // var userName = "8778650328";
        //    //var userName = User.Identity.Name;
        //    //var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
        //    //IfClaimExists = claims.Any(x => x.ClaimType == "StudentView" && x.ClaimValue == "Y");
        //    //if (IfClaimExists)
        //    //{

        //        //var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
        //        var result = await this._repository.GetAllStudent();
        //        //return Ok(result);
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }
        //        return NotFound("Data not found");

        //    //}
        //    //return Unauthorized();

        //}


        [HttpGet("GetAllMCQQuestions")]
        public async Task<IActionResult> GetAllMCQQuestions()
        {
            var result = await _repository.GetAllMCQQuestions();

            if (result == null || !result.Any())
                return NotFound("No questions found");

            return Ok(result);
        }



        [HttpGet("GetAllMCQTests")]
        public async Task<IActionResult> GetAllMCQTests()
        {
            var tests = await _repository.GetAllMCQTests();

            if (tests == null || tests.Count == 0)
                return NotFound("No tests found.");

            return Ok(tests);
        }




        [HttpGet, Route("GetAllMCQByTestId/{testId}")]
        public async Task<IActionResult> GetAllMCQByTestId(int testId)
        {
            var result = await _repository.GetAllMCQByTestId(testId);

            if (result == null)
                return NotFound("No questions found for this Test ID");

            return Ok(result);
        }


        //Get Test Questions based on loggedin student id
        [HttpGet("Questions/ByStudent/{studentId}")]
        public async Task<IActionResult> GetQuestionsByStudent(string studentId)
        {
            var questions = await _repository.GetQuestionsByStudentIdAsync(studentId);

            if (questions == null || !questions.Any())
                return NotFound("No questions found for this student.");

            var grouped = questions
                .GroupBy(q => q.SectionName)
                .Select(g => new
                {
                    SectionName = g.Key,
                    Questions = g.Select(q => new
                    {
                        q.QuestionId,
                        q.QuestionText,
                        Options = new[] { q.OptionA, q.OptionB, q.OptionC, q.OptionD },
                        q.TimeAllowedSeconds,
                        q.DifficultyLevel
                    })
                });

            return Ok(new
            {
                TestId = questions.First().TestId,
                TestName = questions.First().TestName,
                Sections = grouped
            });
        }



        [HttpPost("SubmitMCQTest")]
        public async Task<IActionResult> SubmitMCQTest([FromBody] SubmitTestRequest request)
        {
            try
            {
                var result = await _repository.SubmitMCQTest(request);

                if (result == null)
                    return BadRequest(new { message = "Test submission failed." });

                string message = result.Percentage >= 50
                    ? $"🎉 Congratulations! You scored {result.Percentage}%"
                    : $"Better luck next time. You scored {result.Percentage}%";

                var response = new TestResultResponseDto
                {
                    Message = message,
                    Result = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error submitting test", error = ex.Message });
            }
        }

        [HttpGet("GetAllTestResultsByStudentId/{studentId}")]
        public async Task<IActionResult> GetAllTestResultsByStudentId(string studentId)
        {
            try
            {
                var results = await _repository.GetAllTestResultsByStudentId(studentId);

                if (results == null || results.Count == 0)
                    return NotFound(new { message = "No test results found for this student." });

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching test results", error = ex.Message });
            }
        }

        // GET: api/TestResults/{loggedInUserId}
        [HttpGet("{loggedInUserId}")]
        public async Task<IActionResult> GetAllStudentResults(string loggedInUserId)
        {
            if (string.IsNullOrEmpty(loggedInUserId))
                return BadRequest("LoggedInUserId is required.");

            var results = await _repository.GetAllStudentResultsAsync(loggedInUserId);

            if (results.Count == 0)
                return Unauthorized(new { Message = "Access Denied: Only Super Admin can view all student results." });

            return Ok(results);
        }

        [HttpGet("CheckMultipleTestAttempts/{studentId}")]
        public async Task<IActionResult> CheckMultipleTestAttempts(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
                return BadRequest("StudentId is required.");

            var result = await _repository.CheckMultipleTestAttemptsAsync(studentId);

            if (result == null)
                return NotFound("No data found.");

            return Ok(result);
        }

        //[HttpGet("GetDashboardStatus/{loggedInUserId}")]
        //public async Task<IActionResult> GetDashboardStatus(string loggedInUserId)
        //{
        //    var dashboard = await _repository.GetAdminDashboardStatus(loggedInUserId);

        //    if (!string.IsNullOrEmpty(dashboard.Message))
        //        return Unauthorized(new { dashboard.Message });

        //    return Ok(dashboard);
        //}

        [HttpGet("GetDashboardStatus/{loggedInUserId}/{TestId}")]
        public async Task<IActionResult> GetDashboardStatus(string loggedInUserId, int TestId)
        {
            var dashboard = await _repository.GetAdminDashboardStatus(loggedInUserId, TestId);

            if (!string.IsNullOrEmpty(dashboard.Message))
                return Unauthorized(new { dashboard.Message });

            return Ok(dashboard);
        }



        [HttpGet("GetAllExams")]
        public async Task<IActionResult> GetAllExams()
        {
            var result = await _repository.GetAllExams();

            if (result == null || result.Count == 0)
                return NotFound("No exams found");

            return Ok(result);
        }

        [HttpGet("GetExamCategoriesByExamId/{ExamId}")]
        public async Task<IActionResult> GetExamCategoriesByExamId(int ExamId)
        {
            var result = await _repository.GetExamCategoriesByExamId(ExamId);

            if (result == null || result.Count == 0)
                return NotFound($"No categories found for ExamId = {ExamId}");

            return Ok(result);
        }

        [HttpGet("GetExamSubCategoriesByCategoryId/{ExamCateId}")]
        public async Task<IActionResult> GetExamSubCategoriesByCategoryId(int ExamCateId)
        {
            var result = await _repository.GetExamSubCategoriesByCategoryId(ExamCateId);

            if (result == null || result.Count == 0)
                return NotFound($"No subcategories found for ExamCateId = {ExamCateId}");

            return Ok(result);
        }

        [HttpGet("GetTestsByExamSubCateId/{ExamSubCateId}")]
        public async Task<IActionResult> GetTestsByExamSubCateId(int ExamSubCateId)
        {
            var result = await _repository.GetTestsByExamSubCateId(ExamSubCateId);

            if (result == null || result.Count == 0)
                return NotFound($"No tests found for ExamSubCateId = {ExamSubCateId}");

            return Ok(result);
        }

        //New Saheb
        [HttpGet("GetTestsByExamCateId/{ExamCateId}")]
        public async Task<IActionResult> GetTestsByExamCateId(int ExamCateId)
        {
            var result = await _repository.GetTestsByExamCateId(ExamCateId);

            if (result == null || result.Count == 0)
                return NotFound($"No tests found for ExamCateId = {ExamCateId}");

            return Ok(result);
        }


        [HttpGet("GetDetailedTestResults/{StudentId}")]
        public async Task<IActionResult> GetDetailedTestResults(string StudentId)
        {
            var results = await _repository.GetDetailedTestResults(StudentId);
            if (results == null || !results.Any())
                return NotFound("No results found for the given Student ID.");

            return Ok(results);
        }

        //[HttpPost("UpdateStudentWarning")]
        //public async Task<IActionResult> UpdateStudentWarning([FromBody] WarningRequest request)
        //{
        //    if (request == null || string.IsNullOrEmpty(request.StudentId) || request.TestId <= 0)
        //        return BadRequest("Invalid input parameters.");

        //    var result = await _repository.UpdateStudentWarningCount(request.StudentId, request.TestId);

        //    string message;

        //    if (result.IsBlocked)
        //    {
        //        message = $"🚫 Student {result.Stu_Name} is blocked from the test '{result.TestName}' after 3 violations.";
        //    }
        //    else
        //    {
        //        switch (result.WarningCount)
        //        {
        //            case 1:
        //                message = $"⚠️ Warning 1: Please stay on the test window, {result.Stu_Name}.";
        //                break;
        //            case 2:
        //                message = $"⚠️ Warning 2: Switching tabs again will block you, {result.Stu_Name}.";
        //                break;
        //            default:
        //                message = $"⚠️ Warning {result.WarningCount}: Please focus on the test.";
        //                break;
        //        }
        //    }

        //    return Ok(new
        //    {
        //        result.WarningCount,
        //        result.IsBlocked,
        //        Message = message,
        //        result.Stu_Name,
        //        result.TestName
        //    });
        //}

        [HttpGet("GetDetailedTestResultsByAdmin/{loggedInUserId}")]
        public async Task<IActionResult> GetDetailedTestResultsByAdmin(string loggedInUserId)
        {
            var results = await _repository.GetDetailedTestResultsByAdmin(loggedInUserId);

            if (results.Count == 1 && !string.IsNullOrEmpty(results[0].Message))
                return Unauthorized(new { Message = results[0].Message });

            return Ok(results);
        }

        [HttpGet("GetStudentCertificate/{StudentId}")]
        public async Task<IActionResult> GetCertificateInfoByStudentId(string StudentId)
        {
            if (string.IsNullOrWhiteSpace(StudentId))
                return BadRequest("StudentId is required.");

            var result = await _repository.GetCertificateInfoByStudentId(StudentId);

            if (result == null || result.Count == 0)
                return NotFound("No Studet information found for this student id.");

            return Ok(result);
        }

        [HttpGet("GetTestsCount/{StudentId}")]
        public async Task<IActionResult> GetTestsCountByStudentId(string StudentId)
        {
            if (string.IsNullOrWhiteSpace(StudentId))
                return BadRequest("StudentId is required.");

            var result = await _repository.GetTestsCountByStudentId(StudentId);

            if (result == null)
                return NotFound("No data found for this student.");

            return Ok(result);
        }


        [HttpGet("GetAllStates")]
        public async Task<IActionResult> GetAllStates()
        {
            var states = await _repository.GetAllStates();

            if (states == null || states.Count == 0)
                return NotFound(new { Message = "No states found." });

            return Ok(states);
        }

        [HttpGet("GetZonesByStateId/{StateId}")]
        public async Task<IActionResult> GetZonesByStateId(int StateId)
        {
            var zones = await _repository.GetZonesByStateId(StateId);

            if (zones == null || zones.Count == 0)
                return NotFound(new { Message = "No zones found for the given state." });

            return Ok(zones);
        }


        [HttpGet("GetDistrictsByZoneId/{ZoneId}")]
        public async Task<IActionResult> GetDistrictsByZoneId(int ZoneId)
        {
            var districts = await _repository.GetDistrictsByZoneId(ZoneId);

            if (districts == null || districts.Count == 0)
                return NotFound(new { Message = "No districts found for the given zone." });

            return Ok(districts);
        }

        [HttpGet("GetExamCategoryTestList")]
        public async Task<IActionResult> GetExamCategoryTestList()
        {
            var result = await _repository.GetExamCategoryTestList();

            if (result == null || result.Count == 0)
                return NotFound("No active exams found.");

            return Ok(result);
        }



        //[HttpGet("OverallSummary")]
        //public async Task<IActionResult> GetOverallSummary(string loggedInUserId, int testId, string studentId)
        //{
        //    var result = await _repository.GetOverallSummaryAsync(loggedInUserId, testId, studentId);
        //    if (result == null || result.Count == 0)
        //        return Unauthorized("Access Denied or No data found.");
        //    return Ok(result);
        //}
        [HttpGet("OverallSummary")]
        public async Task<IActionResult> GetOverallSummary(string loggedInUserId, int testId, string studentId)
        {
            var result = await _repository.GetOverallSummaryAsync(loggedInUserId, testId, studentId);

            if (result == null || result.Summary == null)
                return Unauthorized("Access Denied or No data found.");

            return Ok(result);
        }

        [HttpGet("SectionSummary")]
        public async Task<IActionResult> GetSectionSummary(string loggedInUserId, int testId, string studentId)
        {
            var (sections, overall) = await _repository.GetSectionWiseSummaryAsync(loggedInUserId, testId, studentId);

            if (sections == null || sections.Count == 0)
                return Unauthorized("Access Denied or No data found.");

            return Ok(new
            {
                SectionWiseSummary = sections,
                OverallSummary = overall
            });
        }

        [HttpGet("DifficultyLevelSummary")]
        public async Task<IActionResult> GetDifficultyLevelSummary(string loggedInUserId, int testId, string studentId)
        {
            var result = await _repository.GetDifficultyLevelSummaryAsync(loggedInUserId, testId, studentId);
            if (result == null || result.Count == 0)
                return Unauthorized("Access Denied or No data found.");
            return Ok(result);
        }

        [HttpGet("QuestionWiseDetails")]
        public async Task<IActionResult> GetQuestionWiseDetails(string loggedInUserId, int testId, string studentId)
        {
            var result = await _repository.GetQuestionWiseDetailsAsync(loggedInUserId, testId, studentId);
            if (result == null || result.Count == 0)
                return Unauthorized("Access Denied or No data found.");
            return Ok(result);
        }
        [HttpGet("Top10")]
        public async Task<IActionResult> GetTop10Students(string loggedInUserId, int? stateId = null, int? zoneId = null, int? districtId = null)
        {
            var result = await _repository.GetTop10Students(loggedInUserId, stateId, zoneId, districtId);
            if (result == null || result.Count == 0)
                return Unauthorized("Access Denied or No data found.");

            return Ok(result);
        }

        [HttpGet("GetStudentDetailsByUserId/{stuUserId}")]
        public async Task<IActionResult> GetStudentDetailsByUserId(string stuUserId)
        {
            var data = await _repository.GetStudentDetailsByUserId(stuUserId);

            if (data == null || data.Count == 0)
                return NotFound(new { Message = "Student not found" });

            return Ok(data);
        }


        //ranking result

        [HttpGet("GetRankedTestResultsByTestId/{testId}")]
        public async Task<IActionResult> GetRankedTestResultsByTestId(int testId)
        {
            try
            {
                var results = await _repository.GetRankedTestResultsByTestId(testId);

                if (results == null || results.Count == 0)
                    return NotFound(new { message = "No ranked results found for this test." });

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching ranked test results", error = ex.Message });
            }
        }


        [HttpGet("GetStudentRankByTestId/{testId}/{studentId}")]
        public async Task<IActionResult> GetStudentRankByTestId(int testId, string studentId)
        {
            try
            {
                var result = await _repository.GetStudentRankByTestId(testId, studentId);

                if (result == null)
                    return NotFound(new { message = "No rank data found for this student and test." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching student rank", error = ex.Message });
            }
        }


        //New Saheb
        [HttpGet("GetExamCategoryByStudId/{StudentId}")]
        public async Task<IActionResult> GetExamCategoryByStudId(string StudentId)
        {
            var result = await _repository.GetExamCategoryByStudId(StudentId);

            if (result == null || result.Count == 0)
                return NotFound($"No Exam Category Tests found for StudentId = {StudentId}");

            return Ok(result);
        }












    }
    }
