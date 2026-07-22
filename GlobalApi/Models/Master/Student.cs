using GlobalApi.CustomJson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class Student
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int Stu_Id { get; set; }
		public string? Stu_Inscode { get; set; }
		public string? Stu_Copcode { get; set; }

		public string Stu_UserID { get; set; }

		//public string? ASISfxPrfxId { get; set; }

		[StringLength(50)]
		public string? Stu_Name { get; set; }


		public DateTime? Stu_DOB { get; set; }

		[StringLength(20)]
		public string? Stu_Gender { get; set; }
		[StringLength(20)]
		//public string? Assi_Village { get; set; }


		//[Display(Name = "Hospital")]
		//public virtual int? Assi_Hos_Id_FK { get; set; }
		[JsonIgnore]
		//[ForeignKey("Assi_Hos_Id_FK")]
		//public virtual Hospital? Hospital { get; set; }



		//[Display(Name = "Designation")]
		//public virtual int? Assi_Des_Id_FK { get; set; }
		//[JsonIgnore]
		//[ForeignKey("Assi_Des_Id_FK")]
		//public virtual Designation? Designation { get; set; }


		//public int? Assi_skill_id { get; set; }


		public string? Stu_Address { get; set; }

		[Display(Name = "Countries")]
		public virtual int? Stu_Country_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Stu_Country_Id_FK")]
		public virtual Countries? Countries { get; set; }

		[Display(Name = "States")]
		public virtual int? Stu_ST_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Stu_ST_Id_FK")]
		public virtual States? States { get; set; }


		//[Display(Name = "Districts")]
		//public virtual int? Stu_DI_Id_FK { get; set; }
		//[JsonIgnore]
		//[ForeignKey("Stu_DI_Id_FK")]
		//public virtual Districts? Districts { get; set; }

		public string? Stu_DI_Name { get; set; }

		//public int? gram_Id_Fk { get; set; }

		public string? Stu_MobileNumber { get; set; }

		[StringLength(15)]
		//public string? Assi_LandLineNumber { get; set; }

		//[StringLength(10)]

		//[StringLength(50)]
		public string Stu_Email { get; set; }
		public int? created_by { get; set; }
		public Nullable<System.DateTime> created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }

		[Required]
		public bool delete_flag { get; set; }

		[Required]
		public int Stu_status { get; set; }

		[StringLength(250)]
		public string? Remarks { get; set; }
		public int? FK_StateId { get; set; }
		public int? FK_ZoneId { get; set; }
		public int? FK_DistrictId { get; set; }
		public int? FK_TestId { get; set; }

        //profile picture
		public string? Stu_Photo { get; set; } = "default_user.png";




    }
	public class GetAllStudent
	{
		public int Stu_Id { get; set; }
		//public string? ASISfxPrfxId { get; set; }
		public string? Stu_Name { get; set; }
		public string? Stu_Inscode { get; set; }
		public string Stu_Copcode { get; set; }
		public string? Stu_LastName { get; set; }
		public DateTime? Stu_DOB { get; set; }
		public string? Stu_Gender { get; set; }
		public string? Language { get; set; }
		//public int? Stu_Hos_Id_FK { get; set; }
		//public string? Assi_Hos_HospitalName { get; set; }
		public string? Stu_qualification { get; set; }
		public int? Stu_Des_Id_FK { get; set; }
		public string? Stu_Designation { get; set; }
		public int? Stu_skill_id { get; set; }
		public string? Stu_Skill { get; set; }
		public string? Stu_Image { get; set; }
		public byte[]? Imagebyte { get; set; }
		public string? Stu_Address { get; set; }
		public int? Stu_Country_Id_FK { get; set; }
		public string? Stu_Country_name { get; set; }
		public int? Stu_ST_Id_FK { get; set; }
		public string? state_name { get; set; }
		public string? Stu_DI_Name { get; set; }
		public string? district_name { get; set; }
		public string? taluk_name { get; set; }
		//public int? gram_Id_Fk { get; set; }
		//public string? gram_name { get; set; }

		public string Stu_MobileNumber { get; set; }
		//public string? Assi_LandLineNumber { get; set; }
		public string? Stu_Email { get; set; }
		public bool delete_flag { get; set; }
		public int Stu_status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }
		public string? Ins_fromDate { get; set; }
		public string? Ins_ToDate { get; set; }

		public string? Co_FromDate { get; set; }
		public string? Co_ToDate { get; set; }


		public List<GetAllStudentCourse>? GetAllStudentCourse { get; set; } = null!;

	}

	public class Student_DD
	{
		public int Stu_Id { get; set; }
		//public string? ASISfxPrfxId { get; set; }
		public string? Stu_Name { get; set; }
		public string? Stu_LastName { get; set; }
	}
	public class StudentById
	{
		public int Stu_Id { get; set; }
		//public string? ASISfxPrfxId { get; set; }
		public string? Stu_Name { get; set; }

		public Nullable<System.DateTime> Stu_DOB { get; set; }
		public string? Stu_Gender { get; set; }
		public string? Language { get; set; }
		//public int? Stu_Hos_Id_FK { get; set; }
		//public string? Stu_Hos_HospitalName { get; set; }

		//public int? Stu_Des_Id_FK { get; set; }


		public string? Stu_Address { get; set; }
		public int? Stu_Country_Id_FK { get; set; }
		public string? Stu_Country_name { get; set; }
		public int? Stu_ST_Id_FK { get; set; }
		public string? state_name { get; set; }
		public string? Stu_DI_Name { get; set; }

		public string Stu_MobileNumber { get; set; }
		//public string? Assi_LandLineNumber { get; set; }
		public string? Stu_Email { get; set; }
		public bool delete_flag { get; set; }
		public int Stu_status { get; set; }
		public string? sts_name { get; set; }
		public string? Stu_UserID { get; set; }
		public string? Remarks { get; set; }
		//  public List<GetAllStudentCourse>? GetAllStudentCourse { get; set; } = null!;
	}
	public class Student_Images
	{

		public int Stu_Id { get; set; }
		public string? Stu_Inscode { get; set; }
		public string? Stu_Copcode { get; set; }
		public string? Stu_Name { get; set; }

		public Nullable<System.DateTime> Stu_DOB { get; set; }
		public string? Stu_Gender { get; set; }
		public string? Stu_Address { get; set; }
		public int? Stu_Country_Id_FK { get; set; }
		public int? Stu_ST_Id_FK { get; set; }
		public string? Stu_DI_Name { get; set; }
		public string? Stu_MobileNumber { get; set; }
		public string Stu_Email { get; set; }
		public int? created_by { get; set; }
		public Nullable<System.DateTime> created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }
		public bool delete_flag { get; set; }
		public int Stu_status { get; set; }
		//  public List<StudentCourse>? Student_Course { get; set; } = null!;


	}
	//public class Edit_ImageModel_Ass
	//{
	//	public string? Stu_Photo { get; set; }
	//}
	public class ApproveStudent
	{
		public int Stu_Id { get; set; }
		public string? Remarks { get; set; }
	}




	//////New MCQ APIs
	/// <summary>
	/// 
	/// </summary>
	public class QuestionDto
	{
		public int QuestionId { get; set; }
		public string QuestionText { get; set; }
		public int TestId { get; set; }
		public string OptionA { get; set; }
		public string OptionB { get; set; }
		public string OptionC { get; set; }
		public string OptionD { get; set; }
	}

	public class TestDto
	{
		public int TestId { get; set; }
		public string TestName { get; set; }
		public string TestDescription { get; set; }
		public int TotalQuestions { get; set; }
		public int DurationMinutes { get; set; }
		public List<QuestionDto> Questions { get; set; }
	}

	//In MCQ VGSL Test API we are not using
	public class StudentAnswerDto
	{
		public int AnswerId { get; set; }
		public string StudentId { get; set; } // from AspNetUsers
		public int TestId { get; set; }
		public int QuestionId { get; set; }
		public char SelectedOption { get; set; } // 'A', 'B', 'C', 'D'
		public bool IsCorrect { get; set; }
		public DateTime CreatedDate { get; set; }
	}

	public class TestResultDto
	{
		public int ResultId { get; set; }
		public string StudentId { get; set; }

		public string UserName { get; set; }

		public string Stu_Name { get; set; }
		public int TestId { get; set; }

		public String TestName { get; set; }

		public int TotalQuestions { get; set; }
		public int Attempted { get; set; }
		public int CorrectAnswers { get; set; }
		public int WrongAnswers { get; set; }
		public decimal Percentage { get; set; }
		public string Grade { get; set; }
		public DateTime CreatedDate { get; set; }
        public string IsDisqualified { get; set; }
        public string ReasonForDisqualification { get; set; }

    }

    //public class SubmitTestRequest
    //{
    //	public string StudentId { get; set; }
    //	public int TestId { get; set; }
    //	public List<StudentAnswerInputDto> Answers { get; set; }
    //}

    //public class StudentAnswerInputDto
    //{
    //	public int QuestionId { get; set; }
    //	public string SelectedOption { get; set; }
    //}
    public class SubmitTestRequest
    {
        public string StudentId { get; set; }
        public int TestId { get; set; }

        public string IsDisqualified { get; set; }

        public string ReasonForDisqualification { get; set; }
        public string TestVideoBase64 { get; set; }  // ✅ Base64 video string
        public List<StudentAnswerInputDto> Answers { get; set; }
    }

    public class StudentAnswerInputDto
    {
        public int QuestionId { get; set; }
        public string SelectedOption { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TimeTakenSeconds { get; set; }
        public string AnswerImage { get; set; }
        public DateTime? ImageCapturedTime { get; set; }
    }


    public class TestResultResponseDto
	{
		public string Message { get; set; }
		public TestResultDetailDto Result { get; set; }
	}

    //public class TestResultDetailDto
    //{
    //	public int TotalQuestions { get; set; }
    //	public int Attempted { get; set; }
    //	public int CorrectAnswers { get; set; }
    //	public int WrongAnswers { get; set; }
    //	public decimal Percentage { get; set; }
    //	public string Grade { get; set; }
    //}

    public class TestResultDetailDto
    {
        public int TotalQuestions { get; set; }
        public int Attempted { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public decimal Percentage { get; set; }
        public string Grade { get; set; }
        public string TotalTimeTaken { get; set; }  // ✅ formatted (e.g., 1 min 29 secs)
        public int NotAttempted { get; set; }
        public string IsDisqualified { get; set; }
        public string ReasonForDisqualification { get; set; }
        public string TestVideoPath { get; set; }
    }

    public class StudentTestAttemptDto
	{
		public string Message { get; set; }
		public int TotalAttempts { get; set; }

		// Optional list of tests attempted
		public List<TestAttemptDetailDto> Attempts { get; set; } = new List<TestAttemptDetailDto>();
	}

	public class TestAttemptDetailDto
	{
		public int TestId { get; set; }
		public int TotalQuestions { get; set; }
		public int Attempted { get; set; }
		public int CorrectAnswers { get; set; }
		public int WrongAnswers { get; set; }
		public decimal Percentage { get; set; }
		public string Grade { get; set; }
		public DateTime CreatedDate { get; set; }
	}

    // DTO for per-test attempt count
    //public class TestAttemptDto
    //{
    //	public int TestId { get; set; }
    //	public string TestName { get; set; }
    //	public int StudentsAttempted { get; set; }
    //}

    //// DTO for dashboard result
    //public class AdminDashboardDto
    //{
    //	public int TotalStudentsAttempted { get; set; }
    //	public List<TestAttemptDto> TestAttempts { get; set; } = new();
    //	public string Message { get; set; } // For access denied
    //}

    public class TestAttemptDto
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int StudentsAttempted { get; set; }
    }

    public class StudentResultDto
    {
        public long Rank { get; set; }
        public int ResultId { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public decimal? Percentage { get; set; }
        public string Grade { get; set; }
    }

    // Dashboard DTO
    public class AdminDashboardDto
    {
        public int TotalStudentsAttempted { get; set; }
        public List<TestAttemptDto> TestAttempts { get; set; } = new();
        public List<StudentResultDto> TopStudents { get; set; } = new();
        public List<StudentResultDto> AllStudents { get; set; } = new();
        public string Message { get; set; } // For access denied
    }

    public class ExamMasterDto
	{
		public int ExamId { get; set; }
		public string ExamName { get; set; }
		public string ExamDescription { get; set; }
		public bool IsActive { get; set; }
		//public DateTime CreatedDate { get; set; }
	}


	public class ExamCategoryDto
	{
		public int ExamCateId { get; set; }
		public int FK_ExamId { get; set; }
		public string CategoryName { get; set; }
		public string CategoryDescription { get; set; }

		//public DateTime CreatedDate { get; set; }
	}

	public class ExamSubCategoryDto
	{
		public int ExamSubCateId { get; set; }
		public int FK_ExamCateId { get; set; }
		public string SubCategoryName { get; set; }
		public string SubCategoryDescription { get; set; }
		//public DateTime CreatedDate { get; set; }
	}


	public class TestDtos
	{
		public int TestId { get; set; }
		public int FK_ExamSubCateId { get; set; }
		public string TestName { get; set; }
		public string TestDescription { get; set; }
		public int TotalQuestions { get; set; }
		public int DurationMinutes { get; set; }
	}


    public class TestDtos_Category
    {
        public int TestId { get; set; }
        public int FK_ExamCateId { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public int TotalQuestions { get; set; }
        public int DurationMinutes { get; set; }
    }

    public class DetailedTestResultDto
	{
		public int ResultId { get; set; }
		public string StudentId { get; set; }
		public string Stu_Name { get; set; }
		public int TestId { get; set; }
		public string TestName { get; set; }
		public int TotalQuestions { get; set; }
		public int Attempted { get; set; }
		public int CorrectAnswers { get; set; }
		public int WrongAnswers { get; set; }
		public decimal Percentage { get; set; }
		public string Grade { get; set; }
		public DateTime CreatedDate { get; set; }
		public int ExamId { get; set; }
		public string ExamName { get; set; }
		public int ExamCateId { get; set; }
		public string CategoryName { get; set; }
		public int ExamSubCateId { get; set; }
		public string SubCategoryName { get; set; }
	}

    //public class StudentWarningDto
    //{
    //	public int WarningCount { get; set; }
    //	public bool IsBlocked { get; set; }
    //	public string Stu_Name { get; set; }
    //	public string TestName { get; set; }
    //}

    //   public class WarningRequest
    //   {
    //       public string StudentId { get; set; }
    //       public int TestId { get; set; }
    //   }


    public class DetailedTestResultModel
    {
        public int ResultId { get; set; }
        public string StudentId { get; set; }
        public int TestId { get; set; }
        public string Stu_Name { get; set; }
        public string ExamName { get; set; }
        public string CategoryName { get; set; }
        public string TestName { get; set; }
        public int TotalQuestions { get; set; }
        public int Attempted { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public decimal Percentage { get; set; }
        public string Grade { get; set; }
        public DateTime TestDate { get; set; }
        public string Message { get; set; }  // for access denied case
    }




    public class CertificateInfoDto
    {
        public int ResultId { get; set; }
        public string StudentId { get; set; }
        public string Stu_Name { get; set; }
        public string TestName { get; set; }
        public decimal Percentage { get; set; }
        public string Grade { get; set; }
        public DateTime TestDate { get; set; }
        public string ExamName { get; set; }
        public string CategoryName { get; set; }
    }


    public class TestsCountDto
    {
        public int TotalStudentTests { get; set; }
        public int TotalAvailableTests { get; set; }
    }


    public class StateDto
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = string.Empty;
    }

    public class ZoneDto
    {
        public int ZoneId { get; set; }
        public string ZoneName { get; set; } = string.Empty;
    }
    public class DistrictDto
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = string.Empty;
    }


    namespace YourNamespace.Models
    {
        public class ExamCategoryTestDto
        {
            public int ExamId { get; set; }
            public string ExamName { get; set; }
            public int ExamCateId { get; set; }
            public string CategoryName { get; set; }
            public int TestId { get; set; }
            public string TestName { get; set; }
        }
    }

    public class QuestionViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectOption { get; set; }
        public int Marks { get; set; }
        public string SectionName { get; set; }
        public string DifficultyLevel { get; set; }
        public int? TimeAllowedSeconds { get; set; }
    }


    // Models/SummaryViewModels.cs
    //public class OverallSummaryViewModel
    //{
    //    public string TestName { get; set; }
    //    public int TotalQuestions { get; set; }
    //    public int Attempted { get; set; }
    //    public int CorrectAnswers { get; set; }
    //    public int WrongAnswers { get; set; }
    //    public decimal Percentage { get; set; }
    //    public string Grade { get; set; }
    //    public string Status { get; set; }
    //    public DateTime? TestFinishTime { get; set; }
    //    public string TimeTaken { get; set; }
    //}

    // New
    public class OverallSummaryViewModel
    {
        public string TestName { get; set; }
        public int TotalQuestions { get; set; }
        public int Attempted { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int NotAttempted { get; set; }
        public decimal Percentage { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public DateTime? TestFinishTime { get; set; }
        public string TimeTaken { get; set; }
        public DateTime? TestStartTime { get; set; }
        public DateTime? TestEndTime { get; set; }
        public string IsDisqualified { get; set; }
        public string ReasonForDisqualification { get; set; }
    }





    //New
    public class OverallSummaryResponse
    {
        public OverallSummaryViewModel Summary { get; set; }
        public List<StudentImageViewModel> Images { get; set; }
        public StudentProfileViewModel StudentProfile { get; set; } // ✅ New

        public string VideoPath { get; set; }      // ✅ relative path
        public string VideoBase64 { get; set; }    // ✅ base64 (optional for frontend preview)
    }





    //New
    public class StudentImageViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerImagePath { get; set; }
        public DateTime? ImageCapturedTime { get; set; }
        public string ImageBase64 { get; set; } // optional
    }


    // New 

    public class StudentProfileViewModel
    {
        public string Stu_Name { get; set; }
        public string Stu_Photo { get; set; }
        public string Stu_PhotoBase64 { get; set; } // optional for sending inline image
    }



    public class SectionSummaryViewModel
    {
        public string SectionName { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalQuestionsAttempted { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int NotAttempted { get; set; }
        public decimal MarksScored { get; set; }
        public decimal TimeTakenSeconds { get; set; }
        public string TimeTakenFormatted { get; set; }
        public decimal Percentage { get; set; }
        //public string SectionStartTime { get; set; }    // Optional (if included later)
        public string SectionEndTime { get; set; }      // Now returned by SP
    }

    public class DifficultyLevelSummaryViewModel
    {
        public string DifficultyLevel { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalQuestionsAttempted { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int NotAttempted { get; set; }
        public decimal MarksScored { get; set; }
        public decimal TimeTakenSeconds { get; set; }
        public decimal Percentage { get; set; }
    }

    public class QuestionWiseViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string SectionName { get; set; }
        public string DifficultyLevel { get; set; }
        public string SelectedOption { get; set; }
        public string CorrectOption { get; set; }
        public bool? IsCorrect { get; set; }
        public decimal TimeTakenSeconds { get; set; }
    }


    public class OverallSectionSummaryViewModel
    {
        public int TotalQuestions { get; set; }
        public int TotalAttempted { get; set; }
        public int TotalNotAttempted { get; set; }
        public int TotalCorrect { get; set; }
        public int TotalWrong { get; set; }
        public decimal TotalMarksScored { get; set; }
        public decimal OverallPercentage { get; set; }
    }

    public class TopStudentDto
    {
        public int RankNo { get; set; }
        public int ResultId { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string StateName { get; set; }
        public string ZoneName { get; set; }
        public string DistrictName { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public decimal Percentage { get; set; }
        public string Message { get; set; } // for Access Denied
    }


    public class StudentDetailsDto
    {
        public string Stu_UserID { get; set; }
        public string Stu_Name { get; set; }
        public string Stu_MobileNumber { get; set; }
        public string Stu_Email { get; set; }
        public string State_Name { get; set; }
        public string ZoneName { get; set; }
        public string District_Name { get; set; }
        public string TestName { get; set; }
    }

    public class TestRankedResultDto
    {
        public long RankPosition { get; set; } // changed from int → long
        public string StudentId { get; set; }
        public string Stu_Name { get; set; }
        public string State_Name { get; set; }
        public string ZoneName { get; set; }
        public string District_Name { get; set; }
        public decimal Percentage { get; set; }
    }

    public class StudentRankDto
    {
        public int StudentRank { get; set; }
        public string StudentId { get; set; }
        public string Stu_Name { get; set; }
        public decimal Percentage { get; set; }
        public int TotalTimeTakenSeconds { get; set; }
    }


    public class ExamCategoryTestViewModel
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public int ExamCateId { get; set; }
        public string CategoryName { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }

    }





}
