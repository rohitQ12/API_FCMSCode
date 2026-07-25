using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
using GlobalApi.Models.Master.YourNamespace.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using GlobalContext = GlobalApi.Data.GlobalContext;

namespace GlobalApi.Repository.MasterRepository
{
    public class StudentRepository : IStudent
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        private readonly FileUpload fileUpload;
        public StudentRepository()
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
            fileUpload = new FileUpload();

        }
        public async Task<string> InsertStudent(Student_Images lead)
        {
            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {
                    var userPhonenumber = await globalContext.Users
                        .Where(x => x.UserName == lead.Stu_MobileNumber.ToString() || x.PhoneNumber == lead.Stu_MobileNumber.ToString())
                        .FirstOrDefaultAsync();
                    var userEmail = await globalContext.Users
                        .Where(x => x.Email == lead.Stu_Email)
                        .FirstOrDefaultAsync();

                    if (userPhonenumber != null)
                    {
                        return "MobileNumber Already Exists";
                    }
                    else if (userEmail != null)
                    {
                        return "Email Already Exists";
                    }

                    int stuId = await primarykeyvalue.primary_key("Student");

                    AuthUser objAuthUser = new AuthUser()
                    {
                        UserName = Convert.ToString(lead.Stu_MobileNumber),
                        UserId = 0,
                        FirstName = lead.Stu_Name,
                        //LastName = lead.Stu_LastName,
                        DOB = lead.Stu_DOB,
                        Gender = lead.Stu_Gender,
                        PhoneNumber = Convert.ToString(lead.Stu_MobileNumber),
                        Role_Id_FK = "40ea3dcb-e728-4e1b-a42f-934977114b1a",
                        Email = lead.Stu_Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsEnabled = false,
                        Inactive = "N",
                        Id = Guid.NewGuid().ToString(),
                        PhoneNumberConfirmed = false
                    };

                    var AuthUserResult = await globalContext.Users.AddAsync(objAuthUser);
                    await globalContext.SaveChangesAsync();

                    // Process the uploaded photo
                    FileUpload fileUpload = new FileUpload();
                    //string photoFileName = fileUpload.ProcessUploadedFile("wwwroot/images/students",lead.Stu_Photo);

                    Student obj = new Student()
                    {
                        Stu_Id = stuId,
                        Stu_UserID = objAuthUser.Id,
                        Stu_Name = lead.Stu_Name,
                        Stu_Inscode = lead.Stu_Inscode,
                        Stu_Copcode = lead.Stu_Copcode,
                        Stu_DOB = lead.Stu_DOB,
                        Stu_Gender = lead.Stu_Gender,
                        Stu_Address = lead.Stu_Address,
                        Stu_Country_Id_FK = lead.Stu_Country_Id_FK,
                        Stu_ST_Id_FK = lead.Stu_ST_Id_FK,
                        Stu_DI_Name = lead.Stu_DI_Name,
                        Stu_MobileNumber = lead.Stu_MobileNumber,
                        Stu_Email = lead.Stu_Email,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        Stu_status = 1,
                        Remarks = "",
                    };
                    var result = await globalContext.Student.AddAsync(obj);
                    await InsertUsers(obj);
                    await globalContext.SaveChangesAsync();

                    //foreach (StudentCourse cpt in lead.Student_Course)
                    //{
                    //    int scId = await primarykeyvalue.primary_key("student_courses");
                    //    student_courses objCer = new student_courses()
                    //    {
                    //        Cou_Id = scId,
                    //        Stu_id_fk = stuId,
                    //        cp_id = cpt.cp_id,
                    //        created_by = 1,
                    //        created_date = DateTime.Now,
                    //        delete_flag = false,
                    //    };
                    //    var ComplaintResult = await globalContext.student_courses.AddAsync(objCer);
                    //    await globalContext.SaveChangesAsync();
                    //}

                    return "Student registration successfully";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UsersLists> InsertUsers(Student lead)
        {
            int _id = await primarykeyvalue.primary_key("UsersLists");
            UsersLists insert = new UsersLists()
            {
                Id = _id,
                User_cat = "Student",
                User_ref_id = lead.Stu_Id,
                created_by = 1,
                created_date = DateTime.Now,
                delete_flag = false,
                status = 1,

            };
            var _new = await db.UsersLists.AddAsync(insert);
            await db.SaveChangesAsync();
            return _new.Entity;

        }

        public async Task<string> UpdateStudent(Student_Images lead)
        {
            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {
                    string Filename = string.Empty;
                    //var Assistant = await globalContext.Assistant.FirstOrDefaultAsync(x => x.Assi_Id == lead.Assi_Id);
                    var Student = await globalContext.Student.FirstOrDefaultAsync(x => x.Stu_Id == lead.Stu_Id);

                    var user = await globalContext.Users.FirstOrDefaultAsync(x => x.Id == Student.Stu_UserID);

                    var objuserPhonenumber = await globalContext.Users.Where(x => x.UserName == lead.Stu_MobileNumber.ToString() || x.PhoneNumber == lead.Stu_MobileNumber.ToString()).FirstOrDefaultAsync();

                    var objuserEmail = await globalContext.Users.Where(x => x.Email == lead.Stu_Email).FirstOrDefaultAsync();

                    if (objuserPhonenumber != null)
                    {
                        if (objuserPhonenumber.PhoneNumber != Convert.ToString(Student.Stu_MobileNumber))
                        {
                            return "MobileNumber Already Exists";
                        }
                    }
                    if (objuserEmail != null)
                    {
                        if (objuserEmail.Email != Student.Stu_Email)
                        {
                            return "Email Already Exists";
                        }
                    }

                    //if (lead.Stu_Photo != null)
                    //{
                    //    if (Student.Stu_Photo != null && Student.Stu_Photo != "default_user.png")
                    //    {
                    //        string filepath = Path.Combine("wwwroot/Images", Student.Stu_Photo);
                    //        System.IO.File.Delete(filepath);
                    //    }
                    //    Filename = this.fileUpload.ProcessUploadedFile("wwwroot/Images", lead.Stu_Photo);
                    //}
                    //else
                    //{
                    //    Filename = Student.Stu_Photo;
                    //}



                    if (Student.Stu_UserID.Length > 0)
                    {

                        if (user != null)
                        {
                            user.FirstName = lead.Stu_Name;
                            // user.LastName = lead.Stu_LastName;
                            user.PhoneNumber = Convert.ToString(lead.Stu_MobileNumber);
                            user.Email = lead.Stu_Email;
                            user.Gender = lead.Stu_Gender;
                            user.DOB = lead.Stu_DOB;
                            user.Imagename = Filename;
                            await globalContext.SaveChangesAsync();
                        }
                    }
                    if (Student != null)
                    {
                        Student.Stu_Id = lead.Stu_Id;
                        Student.Stu_Name = lead.Stu_Name;
                        Student.Stu_Inscode = lead.Stu_Inscode;
                        Student.Stu_Copcode = lead.Stu_Copcode;
                        Student.Stu_DOB = lead.Stu_DOB;
                        Student.Stu_Gender = lead.Stu_Gender;
                        Student.Stu_Email = lead.Stu_Email;
                        Student.Stu_MobileNumber = lead.Stu_MobileNumber;
                        Student.Stu_Address = lead.Stu_Address;
                        Student.Stu_Country_Id_FK = lead.Stu_Country_Id_FK;
                        Student.Stu_ST_Id_FK = lead.Stu_ST_Id_FK;
                        Student.Stu_DI_Name = lead.Stu_DI_Name;
                        Student.modified_by = 2;
                        Student.modified_date = DateTime.Now;
                        Student.delete_flag = false;
                        Student.Stu_status = 2;
                        Student.Remarks = "";
                        await globalContext.SaveChangesAsync();
                        return "Student Updated Successfully";
                    }
                    return "Student Updated Not Successfully";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllStudent>> GetAllStudent()
        {
            if (db != null)
            {
                var query = (from a in db.Student
                             join c in db.Institution on a.Stu_Inscode equals c.Ins_Reg_No into clist
                             from c in clist.DefaultIfEmpty()
                             join f in db.States on a.Stu_ST_Id_FK equals f.stat_id into flist
                             from f in flist.DefaultIfEmpty()
                             join g in db.Corporate on a.Stu_Copcode equals g.Co_Reg_No into glist
                             from g in glist.DefaultIfEmpty()
                             join h in db.Countries on a.Stu_Country_Id_FK equals h.cntry_id into hlist
                             from h in hlist.DefaultIfEmpty()
                                 //join i in db.Taluk on a.taluk_Id_Fk equals i.Taluk_id into ilist
                                 //from i in ilist.DefaultIfEmpty()
                             join l in db.Status on a.Stu_status equals l.sts_id
                             join m in db.Users on a.Stu_UserID equals m.Id
                             where a.Stu_Id != 0
                             orderby a.Stu_Id descending
                             select new GetAllStudent
                             {
                                 Stu_Id = a.Stu_Id,
                                 Stu_Name = a.Stu_Name,
                                 Stu_Inscode = a.Stu_Inscode,
                                 Stu_Copcode = a.Stu_Copcode,
                                 Stu_DOB = a.Stu_DOB,
                                 Stu_Gender = a.Stu_Gender,
                                 // Stu_qualification = c.qualification_Name,
                                 Stu_Image = configurationRoot.GetSection("AppUrl").Value + "Images/" + m.Imagename,
                                 Stu_Address = a.Stu_Address,
                                 Stu_Country_Id_FK = a.Stu_Country_Id_FK,
                                 Stu_Country_name = h.country_name,
                                 Stu_ST_Id_FK = a.Stu_ST_Id_FK,
                                 state_name = f.state_name,
                                 Stu_DI_Name = a.Stu_DI_Name,
                                 //  district_name = a.Stu_DI_Name,
                                 //taluk_name = i.Taluk_name,
                                 Stu_MobileNumber = m.PhoneNumber,
                                 Stu_Email = m.Email,

                                 GetAllStudentCourse = (from k in db.student_courses
                                                        join v in db.Course_Package on k.cp_id equals v.cp_id
                                                        where k.Stu_id_fk == a.Stu_Id
                                                        select new GetAllStudentCourse
                                                        {
                                                            Cou_Id = k.Cou_Id,
                                                            cp_id = k.cp_id,
                                                            cu_name = v.cu_name,
                                                            cp_amount = v.cp_amount

                                                        }).ToList(),
                                 delete_flag = a.delete_flag,
                                 Stu_status = a.Stu_status,
                                 sts_name = l.sts_name,
                                 Co_FromDate = g.Co_FromDate,
                                 Co_ToDate = g.Co_ToDate,
                                 Ins_fromDate = c.Ins_fromDate,
                                 Ins_ToDate = c.Ins_ToDate,


                             });
                return await query.ToListAsync();

            }
            return null;

        }
        public async Task<List<Student_DD>> GetAssistant_DD(int? Assi_Hos_Id_FK, string roleaction)
        {
            if (db != null)
            {
                var query = (from a in db.Student
                                 //where a.delete_flag == false && a.status == 1 
                                 //&& (roleaction == "Hospital" ? a.Assi_Hos_Id_FK == Assi_Hos_Id_FK : a.Stu_Id > 0)
                             orderby a.Stu_Name
                             select new Student_DD
                             {
                                 Stu_Id = a.Stu_Id,
                                 Stu_Name = a.Stu_Name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> DeleteStudent(int Stu_Id)
        {
            try
            {
                var result = await db.Student.FirstOrDefaultAsync(x => x.Stu_Id == Stu_Id);
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == result.Stu_UserID);
                if (user != null)
                {
                    user.IsEnabled = false;
                    user.Inactive = "Y";
                    await db.SaveChangesAsync();
                }
                if (result != null)
                {
                    result.Stu_Id = Stu_Id;
                    result.delete_flag = true;
                    result.Stu_status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Student Deleted Successfully";
                }

                return "Student Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<StudentById> GetStudentById(int Stu_Id, string roleaction)
        {
            if (db != null)
            {
                var query = (from a in db.Student
                                 //join b in db.Hospital on a.Assi_Hos_Id_FK equals b.Hos_Id into blist
                                 //from b in blist.DefaultIfEmpty()
                                 //join c in db.Qualification on a.Stu_Qua_Id_FK equals c.qualification_id into clist
                                 //from c in clist.DefaultIfEmpty()
                                 //join d in db.Designation on a.Assi_Des_Id_FK equals d.designation_id into dlist
                                 //from d in dlist.DefaultIfEmpty()
                                 //join e in db.SkillSets on a.Assi_skill_id equals e.Skillset_id into elist
                                 //from e in elist.DefaultIfEmpty()
                             join f in db.States on a.Stu_ST_Id_FK equals f.stat_id into flist
                             from f in flist.DefaultIfEmpty()
                                 //join g in db.Districts on a.Stu_DI_Name equals g.district_id into glist
                                 //from g in glist.DefaultIfEmpty()
                             join h in db.Countries on a.Stu_Country_Id_FK equals h.cntry_id into hlist
                             from h in hlist.DefaultIfEmpty()
                                 //join i in db.Taluk on a.taluk_Id_Fk equals i.Taluk_id into ilist
                                 //from i in ilist.DefaultIfEmpty()
                                 //join j in db.Gram on a.gram_Id_Fk equals j.Gram_id into jlist
                                 //from j in jlist.DefaultIfEmpty()

                             join l in db.Status on a.Stu_status equals l.sts_id
                             join m in db.Users on a.Stu_UserID equals m.Id
                             where a.Stu_Id == Stu_Id && a.Stu_Id != 0
                             //where roleaction == "Hospital" ? a.Assi_Hos_Id_FK == Stu_Id : a.Stu_Id > 0
                             select new StudentById
                             {
                                 Stu_Id = a.Stu_Id,
                                 Stu_Name = a.Stu_Name,
                                 Stu_DOB = a.Stu_DOB,
                                 Stu_Gender = a.Stu_Gender,
                                 // Stu_qualification = c.qualification_Name,
                                 //Stu_Image = configurationRoot.GetSection("AppUrl").Value + "Images/" + m.Imagename,
                                 Stu_Address = a.Stu_Address,
                                 Stu_Country_Id_FK = a.Stu_Country_Id_FK,
                                 Stu_Country_name = h.country_name,
                                 Stu_ST_Id_FK = a.Stu_ST_Id_FK,
                                 state_name = f.state_name,
                                 Stu_DI_Name = a.Stu_DI_Name,
                                 // district_name = g.district_name,
                                 //taluk_name = i.Taluk_name,
                                 Stu_MobileNumber = m.PhoneNumber,
                                 Stu_Email = m.Email,

                                 //GetAllStudentCourse = (from k in db.student_courses
                                 //                       join v in db.Course_Package on k.cp_id equals v.cp_id
                                 //                       where k.Stu_id_fk == a.Stu_Id
                                 //                       select new GetAllStudentCourse
                                 //                       {
                                 //                           Cou_Id = k.Cou_Id,
                                 //                           cp_id = k.cp_id,
                                 //                           cp_co_name = v.cp_co_name,
                                 //                           cp_amount=v.cp_amount
                                 //                       }).ToList(),
                                 delete_flag = a.delete_flag,
                                 Stu_status = a.Stu_status,
                                 sts_name = l.sts_name,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        //public async Task<dynamic> GetStudentDetails(int HospitalId)
        //{
        //    if (db != null)
        //    {
        //        var query = (from a in db.Student
        //                     //join c in db.Hospital on a.Assi_Hos_Id_FK equals c.Hos_Id
        //                     join d in db.Users on a.Stu_UserID equals d.Id
        //                     //where a.Assi_Hos_Id_FK == HospitalId &&
        //                      where a.delete_flag == false && a.status == 3
        //                     select d new
        //                     {
        //                         Id = a.Stu_Id,
        //                         UserID = d.Id,
        //                         PartnerName = string.Concat(a.Stu_FirstName, "-", d.PhoneNumber),
        //                         FirstName= a.Stu_FirstName,
        //                         LastName=a.Stu_LastName,
        //                         Email = d.Email,
        //                         MobileNumber = d.PhoneNumber,
        //                       //  HospitalName = c.Hos_HospitalName

        //                     }).ToListAsync();
        //        return await query;
        //    }
        //    return null;
        //}






        public async Task<dynamic> GetStudentDetails()
        {
            var query = (from a in db.Student
                             // join c in db.Hospital on a.DO_HO_Id_FK equals c.Hos_Id
                         join d in db.Users on a.Stu_UserID equals d.Id
                         where
                         //a.DO_HO_Id_FK == HospitalId &&
                         a.delete_flag == false && a.Stu_status == 3 && a.Stu_Id != 0
                         select new
                         {
                             Id = a.Stu_Id,
                             UserID = d.Id,
                             PartnerName = string.Concat(a.Stu_Name, " - ", d.PhoneNumber),
                             FirstName = a.Stu_Name,
                             Email = a.Stu_Email,
                             MobileNumber = d.PhoneNumber,
                             //HospitalName = c.Hos_HospitalName

                         }).ToListAsync();
            return await query;
        }


        public async Task<string> ApproveStudent(ApproveStudent approveStudent)
        {
            try
            {

                var result = await db.Student.Where(x => x.Stu_Id == approveStudent.Stu_Id).FirstOrDefaultAsync();
                if (result.Stu_status != 3)
                {
                    //result.cntry_id = lead.cntry_id;
                    result.Stu_status = 3;
                    if (approveStudent.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = approveStudent.Remarks;
                    await db.SaveChangesAsync();
                    return "Student Approved Successfully";
                }

                return "Student Details Does Not Exists";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /////////// NEW MCQ APIs

        public async Task<List<QuestionDto>> GetAllMCQQuestions()
        {
            var questions = new List<QuestionDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetAllQuestions"; // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            questions.Add(new QuestionDto
                            {
                                QuestionId = reader.GetInt32(reader.GetOrdinal("QuestionId")),
                                TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
                                QuestionText = reader.GetString(reader.GetOrdinal("QuestionText")),
                                OptionA = reader.GetString(reader.GetOrdinal("OptionA")),
                                OptionB = reader.GetString(reader.GetOrdinal("OptionB")),
                                OptionC = reader.GetString(reader.GetOrdinal("OptionC")),
                                OptionD = reader.GetString(reader.GetOrdinal("OptionD")),
                            });
                        }
                    }
                }
            }

            return questions;
        }

        public async Task<List<TestDto>> GetAllMCQTests()
        {
            var tests = new List<TestDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetAllTests"; // Stored Procedure
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tests.Add(new TestDto
                            {
                                TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
                                TestName = reader.GetString(reader.GetOrdinal("TestName")),
                                TestDescription = reader.GetString(reader.GetOrdinal("TestDescription")),
                                TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                DurationMinutes = reader.GetInt32(reader.GetOrdinal("DurationMinutes"))
                            });
                        }
                    }
                }
            }

            return tests;
        }



        public async Task<TestDto> GetAllMCQByTestId(int testId)
        {
            var testDto = new TestDto();
            var questions = new List<QuestionDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetTestWithQuestions";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = new SqlParameter("@TestId", testId);
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // First result set: Test info
                        if (await reader.ReadAsync())
                        {
                            testDto.TestId = reader.GetInt32(reader.GetOrdinal("TestId"));
                            testDto.TestName = reader.GetString(reader.GetOrdinal("TestName"));
                            testDto.TestDescription = reader.GetString(reader.GetOrdinal("TestDescription"));
                            testDto.TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions"));
                            testDto.DurationMinutes = reader.GetInt32(reader.GetOrdinal("DurationMinutes"));
                        }
                        else
                        {
                            return null; // Test not found
                        }

                        // Second result set: Questions
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                questions.Add(new QuestionDto
                                {
                                    QuestionId = reader.GetInt32(reader.GetOrdinal("QuestionId")),
                                    QuestionText = reader.GetString(reader.GetOrdinal("QuestionText")),
                                    OptionA = reader.GetString(reader.GetOrdinal("OptionA")),
                                    OptionB = reader.GetString(reader.GetOrdinal("OptionB")),
                                    OptionC = reader.GetString(reader.GetOrdinal("OptionC")),
                                    OptionD = reader.GetString(reader.GetOrdinal("OptionD")),
                                });
                            }
                        }
                    }
                }
            }

            testDto.Questions = questions;
            return testDto;
        }


        public async Task<List<QuestionViewModel>> GetQuestionsByStudentIdAsync(string studentUserId)
        {
            var questions = new List<QuestionViewModel>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "usp_GetQuestionsByStudentId"; // Stored Procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@Stu_UserID";
                    param.Value = studentUserId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            questions.Add(new QuestionViewModel
                            {
                                TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
                                TestName = reader.GetString(reader.GetOrdinal("TestName")),
                                QuestionId = reader.GetInt32(reader.GetOrdinal("QuestionId")),
                                QuestionText = reader.GetString(reader.GetOrdinal("QuestionText")),
                                OptionA = reader.IsDBNull(reader.GetOrdinal("OptionA")) ? null : reader.GetString(reader.GetOrdinal("OptionA")),
                                OptionB = reader.IsDBNull(reader.GetOrdinal("OptionB")) ? null : reader.GetString(reader.GetOrdinal("OptionB")),
                                OptionC = reader.IsDBNull(reader.GetOrdinal("OptionC")) ? null : reader.GetString(reader.GetOrdinal("OptionC")),
                                OptionD = reader.IsDBNull(reader.GetOrdinal("OptionD")) ? null : reader.GetString(reader.GetOrdinal("OptionD")),
                                CorrectOption = reader.IsDBNull(reader.GetOrdinal("CorrectOption")) ? null : reader.GetString(reader.GetOrdinal("CorrectOption")),
                                Marks = reader.IsDBNull(reader.GetOrdinal("Marks")) ? 0 : reader.GetInt32(reader.GetOrdinal("Marks")),
                                SectionName = reader.IsDBNull(reader.GetOrdinal("SectionName")) ? null : reader.GetString(reader.GetOrdinal("SectionName")),
                                DifficultyLevel = reader.IsDBNull(reader.GetOrdinal("DifficultyLevel")) ? null : reader.GetString(reader.GetOrdinal("DifficultyLevel")),
                                TimeAllowedSeconds = reader.IsDBNull(reader.GetOrdinal("TimeAllowedSeconds")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("TimeAllowedSeconds"))
                            });
                        }
                    }
                }
            }

            return questions;
        }


        //public async Task<TestResultDetailDto> SubmitMCQTest(SubmitTestRequest request)
        //{
        //    TestResultDetailDto result = null;

        //    using (var connection = db.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        // Step 1: Save each student answer
        //        foreach (var ans in request.Answers)
        //        {
        //            using (var command = connection.CreateCommand())
        //            {
        //                command.CommandText = @"
        //            INSERT INTO StudentAnswers (StudentId, TestId, QuestionId, SelectedOption, IsCorrect)
        //            SELECT 
        //                @StudentId, 
        //                @TestId, 
        //                @QuestionId, 
        //                @SelectedOption, 
        //                CASE WHEN Q.CorrectOption = @SelectedOption THEN 1 ELSE 0 END
        //            FROM Questions Q
        //            WHERE Q.QuestionId = @QuestionId";

        //                command.CommandType = System.Data.CommandType.Text;

        //                var studentIdParam = command.CreateParameter();
        //                studentIdParam.ParameterName = "@StudentId";
        //                studentIdParam.Value = request.StudentId;
        //                command.Parameters.Add(studentIdParam);

        //                var testIdParam = command.CreateParameter();
        //                testIdParam.ParameterName = "@TestId";
        //                testIdParam.Value = request.TestId;
        //                command.Parameters.Add(testIdParam);

        //                var questionIdParam = command.CreateParameter();
        //                questionIdParam.ParameterName = "@QuestionId";
        //                questionIdParam.Value = ans.QuestionId;
        //                command.Parameters.Add(questionIdParam);

        //                var selectedOptionParam = command.CreateParameter();
        //                selectedOptionParam.ParameterName = "@SelectedOption";
        //                selectedOptionParam.Value = ans.SelectedOption;
        //                command.Parameters.Add(selectedOptionParam);

        //                await command.ExecuteNonQueryAsync();
        //            }
        //        }

        //        // Step 2: Call stored procedure to calculate and save test results
        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "SubmitStudentTest"; // Stored Procedure name
        //            command.CommandType = System.Data.CommandType.StoredProcedure;

        //            var studentIdParam = command.CreateParameter();
        //            studentIdParam.ParameterName = "@StudentId";
        //            studentIdParam.Value = request.StudentId;
        //            command.Parameters.Add(studentIdParam);

        //            var testIdParam = command.CreateParameter();
        //            testIdParam.ParameterName = "@TestId";
        //            testIdParam.Value = request.TestId;
        //            command.Parameters.Add(testIdParam);

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    result = new TestResultDetailDto
        //                    {
        //                        TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
        //                        Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
        //                        CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
        //                        WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
        //                        Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
        //                        Grade = reader.GetString(reader.GetOrdinal("Grade"))
        //                    };
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}
        //  Without image capturing
        //public async Task<TestResultDetailDto> SubmitMCQTest(SubmitTestRequest request)
        //{
        //    TestResultDetailDto result = null;

        //    using (var connection = db.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        // ✅ Step 1: Save each student answer with timing info
        //        foreach (var ans in request.Answers)
        //        {
        //            using (var command = connection.CreateCommand())
        //            {
        //                command.CommandText = @"
        //            INSERT INTO StudentAnswers (
        //                StudentId, TestId, QuestionId, SelectedOption, IsCorrect,
        //                Test_StartTime, Test_EndTime, TimeTakenSeconds, CreatedDate
        //            )
        //            SELECT 
        //                @StudentId, 
        //                @TestId, 
        //                @QuestionId, 
        //                @SelectedOption, 
        //                CASE WHEN Q.CorrectOption = @SelectedOption THEN 1 ELSE 0 END,
        //                @StartTime,
        //                @EndTime,
        //                @TimeTakenSeconds,
        //                GETDATE()
        //            FROM Questions Q
        //            WHERE Q.QuestionId = @QuestionId";

        //                command.CommandType = System.Data.CommandType.Text;

        //                command.Parameters.Add(new SqlParameter("@StudentId", request.StudentId));
        //                command.Parameters.Add(new SqlParameter("@TestId", request.TestId));
        //                command.Parameters.Add(new SqlParameter("@QuestionId", ans.QuestionId));
        //                command.Parameters.Add(new SqlParameter("@SelectedOption", ans.SelectedOption ?? (object)DBNull.Value));
        //                command.Parameters.Add(new SqlParameter("@StartTime", ans.StartTime));
        //                command.Parameters.Add(new SqlParameter("@EndTime", ans.EndTime));
        //                command.Parameters.Add(new SqlParameter("@TimeTakenSeconds", ans.TimeTakenSeconds));

        //                await command.ExecuteNonQueryAsync();
        //            }
        //        }

        //        // ✅ Step 2: Call stored procedure to calculate result and total test duration
        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "SubmitStudentTest";
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add(new SqlParameter("@StudentId", request.StudentId));
        //            command.Parameters.Add(new SqlParameter("@TestId", request.TestId));

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    result = new TestResultDetailDto
        //                    {
        //                        TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
        //                        Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
        //                        CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
        //                        WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
        //                        NotAttempted = reader.GetInt32(reader.GetOrdinal("NotAttempted")), // ✅ New
        //                        Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
        //                        Grade = reader.GetString(reader.GetOrdinal("Grade")),
        //                        TotalTimeTaken = reader["TotalTimeTaken"].ToString()
        //                    };
        //                }


        //            }
        //        }
        //    }

        //    return result;
        //}




        //public async Task<TestResultDetailDto> SubmitMCQTest(SubmitTestRequest request)
        //{
        //    TestResultDetailDto result = null;

        //    using (var connection = db.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        // ✅ Step 1: Save each student answer with image path & timing info
        //        foreach (var ans in request.Answers)
        //        {
        //            string imagePath = null;

        //            // ✅ Handle image upload
        //            if (ans.AnswerImage != null && ans.AnswerImage.Length > 0)
        //            {
        //                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", "StudentFacePictures");

        //                if (!Directory.Exists(uploadsFolder))
        //                    Directory.CreateDirectory(uploadsFolder);

        //                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(ans.AnswerImage.FileName)}";
        //                string fullPath = Path.Combine(uploadsFolder, fileName);

        //                using (var stream = new FileStream(fullPath, FileMode.Create))
        //                {
        //                    await ans.AnswerImage.CopyToAsync(stream);
        //                }

        //                // ✅ Store relative path (recommended)
        //                imagePath = $"/Images/StudentFacePictures/{fileName}";
        //            }

        //            using (var command = connection.CreateCommand())
        //            {
        //                command.CommandText = @"
        //            INSERT INTO StudentAnswers (
        //                StudentId, TestId, QuestionId, SelectedOption, IsCorrect,
        //                Test_StartTime, Test_EndTime, TimeTakenSeconds,
        //                AnswerImagePath, ImageCapturedTime, CreatedDate
        //            )
        //            SELECT 
        //                @StudentId, 
        //                @TestId, 
        //                @QuestionId, 
        //                @SelectedOption, 
        //                CASE WHEN Q.CorrectOption = @SelectedOption THEN 1 ELSE 0 END,
        //                @StartTime,
        //                @EndTime,
        //                @TimeTakenSeconds,
        //                @AnswerImagePath,
        //                @ImageCapturedTime,
        //                GETDATE()
        //            FROM Questions Q
        //            WHERE Q.QuestionId = @QuestionId";

        //                command.CommandType = CommandType.Text;

        //                command.Parameters.Add(new SqlParameter("@StudentId", request.StudentId));
        //                command.Parameters.Add(new SqlParameter("@TestId", request.TestId));
        //                command.Parameters.Add(new SqlParameter("@QuestionId", ans.QuestionId));
        //                command.Parameters.Add(new SqlParameter("@SelectedOption", ans.SelectedOption ?? (object)DBNull.Value));
        //                command.Parameters.Add(new SqlParameter("@StartTime", ans.StartTime));
        //                command.Parameters.Add(new SqlParameter("@EndTime", ans.EndTime));
        //                command.Parameters.Add(new SqlParameter("@TimeTakenSeconds", ans.TimeTakenSeconds));
        //                command.Parameters.Add(new SqlParameter("@AnswerImagePath", imagePath ?? (object)DBNull.Value));
        //                command.Parameters.Add(new SqlParameter("@ImageCapturedTime", ans.ImageCapturedTime ?? (object)DBNull.Value));

        //                await command.ExecuteNonQueryAsync();
        //            }
        //        }

        //        // ✅ Step 2: Call stored procedure to calculate result
        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "SubmitStudentTest";
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add(new SqlParameter("@StudentId", request.StudentId));
        //            command.Parameters.Add(new SqlParameter("@TestId", request.TestId));

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    result = new TestResultDetailDto
        //                    {
        //                        TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
        //                        Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
        //                        CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
        //                        WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
        //                        NotAttempted = reader.GetInt32(reader.GetOrdinal("NotAttempted")),
        //                        Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
        //                        Grade = reader.GetString(reader.GetOrdinal("Grade")),
        //                        TotalTimeTaken = reader["TotalTimeTaken"].ToString()
        //                    };
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}


        //public async Task<TestResultDetailDto> SubmitMCQTest(SubmitTestRequest request)
        //{
        //    TestResultDetailDto result = null;

        //    using (var connection = db.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        // ✅ Step 1: Save each student answer with image path & timing info
        //        foreach (var ans in request.Answers)
        //        {
        //            string imagePath = null;

        //            // ✅ Handle Base64 image upload
        //            if (!string.IsNullOrEmpty(ans.AnswerImage))
        //            {
        //                try
        //                {
        //                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", "StudentFacePictures");

        //                    if (!Directory.Exists(uploadsFolder))
        //                        Directory.CreateDirectory(uploadsFolder);

        //                    string fileName = $"{Guid.NewGuid()}.jpg";
        //                    string fullPath = Path.Combine(uploadsFolder, fileName);

        //                    // Remove Base64 prefix if present (e.g., "data:image/jpeg;base64,")
        //                    string base64Data = ans.AnswerImage;
        //                    if (base64Data.Contains(","))
        //                        base64Data = base64Data.Split(',')[1];

        //                    byte[] imageBytes = Convert.FromBase64String(base64Data);
        //                    await File.WriteAllBytesAsync(fullPath, imageBytes);

        //                    // ✅ Store relative path in DB (recommended)
        //                    imagePath = $"/Images/StudentFacePictures/{fileName}";
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine($"❌ Image save failed for QuestionId {ans.QuestionId}: {ex.Message}");
        //                }
        //            }

        //            if (ans.TimeTakenSeconds > 15.000m)
        //                ans.TimeTakenSeconds = 15.000m;

        //            using (var command = connection.CreateCommand())
        //            {
        //                command.CommandText = @"
        //            INSERT INTO StudentAnswers (
        //                StudentId, TestId, QuestionId, SelectedOption, IsCorrect,
        //                Test_StartTime, Test_EndTime, TimeTakenSeconds,
        //                AnswerImagePath, ImageCapturedTime, CreatedDate
        //            )
        //            SELECT 
        //                @StudentId, 
        //                @TestId, 
        //                @QuestionId, 
        //                @SelectedOption, 
        //                CASE WHEN Q.CorrectOption = @SelectedOption THEN 1 ELSE 0 END,
        //                @StartTime,
        //                @EndTime,
        //                @TimeTakenSeconds,
        //                @AnswerImagePath,
        //                @ImageCapturedTime,
        //                GETDATE()
        //            FROM Questions Q
        //            WHERE Q.QuestionId = @QuestionId";

        //                command.CommandType = CommandType.Text;

        //                command.Parameters.Add(new SqlParameter("@StudentId", request.StudentId));
        //                command.Parameters.Add(new SqlParameter("@TestId", request.TestId));
        //                command.Parameters.Add(new SqlParameter("@QuestionId", ans.QuestionId));
        //                command.Parameters.Add(new SqlParameter("@SelectedOption", ans.SelectedOption ?? (object)DBNull.Value));
        //                command.Parameters.Add(new SqlParameter("@StartTime", ans.StartTime));
        //                command.Parameters.Add(new SqlParameter("@EndTime", ans.EndTime));
        //                command.Parameters.Add(new SqlParameter("@TimeTakenSeconds", ans.TimeTakenSeconds));
        //                command.Parameters.Add(new SqlParameter("@AnswerImagePath", imagePath ?? (object)DBNull.Value));
        //                command.Parameters.Add(new SqlParameter("@ImageCapturedTime", ans.ImageCapturedTime ?? (object)DBNull.Value));

        //                await command.ExecuteNonQueryAsync();
        //            }
        //        }






        //        // ✅ Step 2: Call stored procedure to calculate result
        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "SubmitStudentTest";
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add(new SqlParameter("@StudentId", request.StudentId));
        //            command.Parameters.Add(new SqlParameter("@TestId", request.TestId));
        //            command.Parameters.Add(new SqlParameter("@IsDisqualified", request.IsDisqualified));
        //            command.Parameters.Add(new SqlParameter("@ReasonForDisqualification", request.ReasonForDisqualification));

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    result = new TestResultDetailDto
        //                    {
        //                        TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
        //                        Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
        //                        CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
        //                        WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
        //                        NotAttempted = reader.GetInt32(reader.GetOrdinal("NotAttempted")),
        //                        Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
        //                        Grade = reader.GetString(reader.GetOrdinal("Grade")),
        //                        TotalTimeTaken = reader["TotalTimeTaken"].ToString(),
        //                        IsDisqualified = reader.GetString(reader.GetOrdinal("IsDisqualified")),
        //                        //ReasonForDisqualification  = reader.GetString(reader.GetOrdinal("ReasonForDisqualification"))
        //                        ReasonForDisqualification = reader["ReasonForDisqualification"] == DBNull.Value ? null: reader["ReasonForDisqualification"].ToString()

        //                    };
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}


        public async Task<TestResultDetailDto> SubmitMCQTest(SubmitTestRequest request)
        {
            TestResultDetailDto result = null;

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                // ✅ Step 1: Save each student answer (same as before)
                foreach (var ans in request.Answers)
                {
                    string imagePath = null;

                    // ✅ Handle Base64 image upload
                    if (!string.IsNullOrEmpty(ans.AnswerImage))
                    {
                        try
                        {
                            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", "StudentFacePictures");
                            if (!Directory.Exists(uploadsFolder))
                                Directory.CreateDirectory(uploadsFolder);

                            string fileName = $"{Guid.NewGuid()}.jpg";
                            string fullPath = Path.Combine(uploadsFolder, fileName);

                            string base64Data = ans.AnswerImage;
                            if (base64Data.Contains(",")) base64Data = base64Data.Split(',')[1];

                            byte[] imageBytes = Convert.FromBase64String(base64Data);
                            await File.WriteAllBytesAsync(fullPath, imageBytes);

                            imagePath = $"/Images/StudentFacePictures/{fileName}";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Image save failed for QuestionId {ans.QuestionId}: {ex.Message}");
                        }
                    }

                    if (ans.TimeTakenSeconds > 15.000m)
                        ans.TimeTakenSeconds = 15.000m;

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                    INSERT INTO StudentAnswers (
                        StudentId, TestId, QuestionId, SelectedOption, IsCorrect,
                        Test_StartTime, Test_EndTime, TimeTakenSeconds,
                        AnswerImagePath, ImageCapturedTime, CreatedDate
                    )
                    SELECT 
                        @StudentId, 
                        @TestId, 
                        @QuestionId, 
                        @SelectedOption, 
                        CASE WHEN Q.CorrectOption = @SelectedOption THEN 1 ELSE 0 END,
                        @StartTime,
                        @EndTime,
                        @TimeTakenSeconds,
                        @AnswerImagePath,
                        @ImageCapturedTime,
                        GETDATE()
                    FROM Questions Q
                    WHERE Q.QuestionId = @QuestionId";

                        command.Parameters.Add(new SqlParameter("@StudentId", request.StudentId));
                        command.Parameters.Add(new SqlParameter("@TestId", request.TestId));
                        command.Parameters.Add(new SqlParameter("@QuestionId", ans.QuestionId));
                        command.Parameters.Add(new SqlParameter("@SelectedOption", ans.SelectedOption ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@StartTime", ans.StartTime));
                        command.Parameters.Add(new SqlParameter("@EndTime", ans.EndTime));
                        command.Parameters.Add(new SqlParameter("@TimeTakenSeconds", ans.TimeTakenSeconds));
                        command.Parameters.Add(new SqlParameter("@AnswerImagePath", imagePath ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@ImageCapturedTime", ans.ImageCapturedTime ?? (object)DBNull.Value));

                        await command.ExecuteNonQueryAsync();
                    }
                }

                // ✅ Step 2: Handle Test Video Upload
                string videoPath = null;

                if (!string.IsNullOrEmpty(request.TestVideoBase64))
                {
                    try
                    {
                        //string videoFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/TestVideos");
                        string videoFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", "TestVideos");
                        if (!Directory.Exists(videoFolder))
                            Directory.CreateDirectory(videoFolder);

                        string fileName = $"{Guid.NewGuid()}.mp4";
                        string fullPath = Path.Combine(videoFolder, fileName);

                        string base64Data = request.TestVideoBase64;
                        if (base64Data.Contains(",")) base64Data = base64Data.Split(',')[1];

                        byte[] videoBytes = Convert.FromBase64String(base64Data);
                        await File.WriteAllBytesAsync(fullPath, videoBytes);

                        videoPath = $"/Images/TestVideos/{fileName}";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Video save failed: {ex.Message}");
                    }
                }

                // ✅ Step 3: Call stored procedure
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SubmitStudentTest";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@StudentId", request.StudentId));
                    command.Parameters.Add(new SqlParameter("@TestId", request.TestId));
                    command.Parameters.Add(new SqlParameter("@IsDisqualified", request.IsDisqualified));
                    command.Parameters.Add(new SqlParameter("@ReasonForDisqualification", (object?)request.ReasonForDisqualification ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@TestVideoPath", (object?)videoPath ?? DBNull.Value));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            result = new TestResultDetailDto
                            {
                                TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
                                CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
                                WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
                                NotAttempted = reader.GetInt32(reader.GetOrdinal("NotAttempted")),
                                Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
                                Grade = reader.GetString(reader.GetOrdinal("Grade")),
                                TotalTimeTaken = reader["TotalTimeTaken"].ToString(),
                                IsDisqualified = reader.GetString(reader.GetOrdinal("IsDisqualified")),
                                ReasonForDisqualification = reader["ReasonForDisqualification"] == DBNull.Value ? null : reader["ReasonForDisqualification"].ToString(),
                                TestVideoPath = reader["TestVideoPath"] == DBNull.Value ? null : reader["TestVideoPath"].ToString()
                            };
                        }
                    }
                }
            }

            return result;
        }


























        public async Task<List<TestResultDto>> GetAllTestResultsByStudentId(string studentId)
        {
            var results = new List<TestResultDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetAllTestResultsByStudentId"; // Stored Procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@StudentId";
                    param.Value = studentId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            results.Add(new TestResultDto
                            {
                                ResultId = reader.GetInt32(reader.GetOrdinal("ResultId")),
                                StudentId = reader.GetString(reader.GetOrdinal("StudentId")),
                                Stu_Name = reader.GetString(reader.GetOrdinal("Stu_Name")),
                                TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
                                TestName = reader.GetString(reader.GetOrdinal("TestName")),
                                TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
                                CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
                                WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
                                Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
                                Grade = reader.GetString(reader.GetOrdinal("Grade")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                IsDisqualified = reader.GetString(reader.GetOrdinal("IsDisqualified")),
                                //ReasonForDisqualification  = reader.GetString(reader.GetOrdinal("ReasonForDisqualification"))
                                ReasonForDisqualification = reader["ReasonForDisqualification"] == DBNull.Value ? null : reader["ReasonForDisqualification"].ToString()
                            });
                        }
                    }
                }
            }

            return results;
        }

        public async Task<List<TestResultDto>> GetAllStudentResultsAsync(string loggedInUserId)
        {
            var results = new List<TestResultDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetAllStudentResults"; // Stored Procedure name
                    command.CommandType = CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@LoggedInUserId";
                    param.Value = loggedInUserId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Check if the SP returned Access Denied message
                            if (reader.FieldCount == 1 && reader.GetName(0) == "Message")
                            {
                                break; // return empty list or handle in controller
                            }

                            results.Add(new TestResultDto
                            {
                                ResultId = reader.GetInt32(reader.GetOrdinal("ResultId")),
                                StudentId = reader.GetString(reader.GetOrdinal("StudentId")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                Stu_Name = reader.GetString(reader.GetOrdinal("Stu_Name")),
                                TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
                                TestName = reader.GetString(reader.GetOrdinal("TestName")),
                                TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
                                CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
                                WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
                                Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
                                Grade = reader.GetString(reader.GetOrdinal("Grade")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                            });
                        }
                    }
                }
            }

            return results;
        }






        //public async Task<List<GetAllStudent>> GetAllMCQ()
        //{
        //    if (db != null)
        //    {
        //        var query = (from a in db.Student
        //                     join c in db.Institution on a.Stu_Inscode equals c.Ins_Reg_No into clist
        //                     from c in clist.DefaultIfEmpty()
        //                     join f in db.States on a.Stu_ST_Id_FK equals f.stat_id into flist
        //                     from f in flist.DefaultIfEmpty()
        //                     join g in db.Corporate on a.Stu_Copcode equals g.Co_Reg_No into glist
        //                     from g in glist.DefaultIfEmpty()
        //                     join h in db.Countries on a.Stu_Country_Id_FK equals h.cntry_id into hlist
        //                     from h in hlist.DefaultIfEmpty()
        //                         //join i in db.Taluk on a.taluk_Id_Fk equals i.Taluk_id into ilist
        //                         //from i in ilist.DefaultIfEmpty()
        //                     join l in db.Status on a.Stu_status equals l.sts_id
        //                     join m in db.Users on a.Stu_UserID equals m.Id
        //                     where a.Stu_Id != 0
        //                     orderby a.Stu_Id descending
        //                     select new GetAllStudent
        //                     {
        //                         Stu_Id = a.Stu_Id,
        //                         Stu_Name = a.Stu_Name,
        //                         Stu_Inscode = a.Stu_Inscode,
        //                         Stu_Copcode = a.Stu_Copcode,
        //                         Stu_DOB = a.Stu_DOB,
        //                         Stu_Gender = a.Stu_Gender,
        //                         // Stu_qualification = c.qualification_Name,
        //                         Stu_Image = configurationRoot.GetSection("AppUrl").Value + "Images/" + m.Imagename,
        //                         Stu_Address = a.Stu_Address,
        //                         Stu_Country_Id_FK = a.Stu_Country_Id_FK,
        //                         Stu_Country_name = h.country_name,
        //                         Stu_ST_Id_FK = a.Stu_ST_Id_FK,
        //                         state_name = f.state_name,
        //                         Stu_DI_Name = a.Stu_DI_Name,
        //                         //  district_name = a.Stu_DI_Name,
        //                         //taluk_name = i.Taluk_name,
        //                         Stu_MobileNumber = m.PhoneNumber,
        //                         Stu_Email = m.Email,

        //                         GetAllStudentCourse = (from k in db.student_courses
        //                                                join v in db.Course_Package on k.cp_id equals v.cp_id
        //                                                where k.Stu_id_fk == a.Stu_Id
        //                                                select new GetAllStudentCourse
        //                                                {
        //                                                    Cou_Id = k.Cou_Id,
        //                                                    cp_id = k.cp_id,
        //                                                    cu_name = v.cu_name,
        //                                                    cp_amount = v.cp_amount

        //                                                }).ToList(),
        //                         delete_flag = a.delete_flag,
        //                         Stu_status = a.Stu_status,
        //                         sts_name = l.sts_name,
        //                         Co_FromDate = g.Co_FromDate,
        //                         Co_ToDate = g.Co_ToDate,
        //                         Ins_fromDate = c.Ins_fromDate,
        //                         Ins_ToDate = c.Ins_ToDate,


        //                     });
        //        return await query.ToListAsync();

        //    }
        //    return null;

        //}

        //public async Task<TestDto> GetAllMCQByTestIdAsync(int testId)
        //{
        //    // Fetch test details
        //    var testInfo = await db.Tests
        //        .Where(t => t.TestId == testId)
        //        .Select(t => new
        //        {
        //            t.TestId,
        //            t.TestName,
        //            t.TestDescription,
        //            t.TotalQuestions,
        //            t.DurationMinutes
        //        })
        //        .FirstOrDefaultAsync();

        //    if (testInfo == null)
        //        return null;

        //    // Fetch all questions for that test
        //    var questions = await db.Questions
        //        .Where(q => q.TestId == testId)
        //        .Select(q => new QuestionDto
        //        {
        //            QuestionId = q.QuestionId,
        //            QuestionText = q.QuestionText,
        //            OptionA = q.OptionA,
        //            OptionB = q.OptionB,
        //            OptionC = q.OptionC,
        //            OptionD = q.OptionD
        //        })
        //        .ToListAsync();

        //    return new TestDto
        //    {
        //        TestId = testInfo.TestId,
        //        TestName = testInfo.TestName,
        //        TestDescription = testInfo.TestDescription,
        //        TotalQuestions = testInfo.TotalQuestions,
        //        DurationMinutes = testInfo.DurationMinutes,
        //        Questions = questions
        //    };
        //}


        public async Task<StudentTestAttemptDto> CheckMultipleTestAttemptsAsync(string studentId)
        {
            var resultDto = new StudentTestAttemptDto();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "CheckMultipleTestAttempts";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@StudentId";
                    param.Value = studentId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // First result set: message and total attempts
                        if (await reader.ReadAsync())
                        {
                            resultDto.Message = reader.GetString(reader.GetOrdinal("Message"));
                            resultDto.TotalAttempts = reader.GetInt32(reader.GetOrdinal("TotalAttempts"));
                        }

                        // Move to next result set (list of tests)
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                resultDto.Attempts.Add(new TestAttemptDetailDto
                                {
                                    TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
                                    TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                    Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
                                    CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
                                    WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
                                    Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
                                    Grade = reader.GetString(reader.GetOrdinal("Grade")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                                });
                            }
                        }
                    }
                }
            }

            return resultDto;
        }

        //public async Task<AdminDashboardDto> GetAdminDashboardStatus(string loggedInUserId)
        //{
        //    var result = new AdminDashboardDto();

        //    using (var connection = db.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "GetAdminDashboardStatus";
        //            command.CommandType = System.Data.CommandType.StoredProcedure;

        //            var param = command.CreateParameter();
        //            param.ParameterName = "@LoggedInUserId";
        //            param.Value = loggedInUserId;
        //            command.Parameters.Add(param);

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                // First result set: total students attempted or access denied
        //                if (await reader.ReadAsync())
        //                {
        //                    if (reader.GetSchemaTable().Columns.Contains("Message"))
        //                    {
        //                        result.Message = reader.GetString(reader.GetOrdinal("Message"));
        //                        return result; // Access denied, return immediately
        //                    }
        //                    result.TotalStudentsAttempted = reader.GetInt32(reader.GetOrdinal("TotalStudentsAttempted"));
        //                }

        //                // Move to next result set: test attempt details
        //                if (await reader.NextResultAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        result.TestAttempts.Add(new TestAttemptDto
        //                        {
        //                            TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
        //                            TestName = reader.GetString(reader.GetOrdinal("TestName")),
        //                            StudentsAttempted = reader.GetInt32(reader.GetOrdinal("StudentsAttempted"))
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}

        //public async Task<AdminDashboardDto> GetAdminDashboardStatus(string loggedInUserId)
        //{
        //    var result = new AdminDashboardDto();

        //    using (var connection = db.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "GetAdminDashboardStatus";
        //            command.CommandType = System.Data.CommandType.StoredProcedure;

        //            var param = command.CreateParameter();
        //            param.ParameterName = "@LoggedInUserId";
        //            param.Value = loggedInUserId;
        //            command.Parameters.Add(param);

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                // ✅ Detect if stored procedure returned an error message
        //                bool hasMessageColumn = Enumerable.Range(0, reader.FieldCount)
        //                    .Any(i => reader.GetName(i).Equals("Message", StringComparison.OrdinalIgnoreCase));

        //                if (hasMessageColumn && await reader.ReadAsync())
        //                {
        //                    result.Message = reader.GetString(reader.GetOrdinal("Message"));
        //                    return result; // Return immediately with the error message
        //                }

        //                // ✅ Otherwise, proceed with normal dashboard data
        //                if (await reader.ReadAsync())
        //                {
        //                    result.TotalStudentsAttempted = reader.GetInt32(reader.GetOrdinal("TotalStudentsAttempted"));
        //                }

        //                if (await reader.NextResultAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        result.TestAttempts.Add(new TestAttemptDto
        //                        {
        //                            TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
        //                            TestName = reader.GetString(reader.GetOrdinal("TestName")),
        //                            StudentsAttempted = reader.GetInt32(reader.GetOrdinal("StudentsAttempted"))
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}

        //public async Task<AdminDashboardDto> GetAdminDashboardStatus(string loggedInUserId, int TestId)
        //{
        //    var result = new AdminDashboardDto();

        //    using (var connection = db.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "GetAdminDashboardStatus";
        //            command.CommandType = System.Data.CommandType.StoredProcedure;

        //            var paramUser = command.CreateParameter();
        //            paramUser.ParameterName = "@LoggedInUserId";
        //            paramUser.Value = loggedInUserId;
        //            command.Parameters.Add(paramUser);

        //            var paramTest = command.CreateParameter();
        //            paramTest.ParameterName = "@TestId";
        //            paramTest.Value = TestId;
        //            command.Parameters.Add(paramTest);

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                // Check if stored procedure returned an error message
        //                bool hasMessageColumn = Enumerable.Range(0, reader.FieldCount)
        //                    .Any(i => reader.GetName(i).Equals("Message", StringComparison.OrdinalIgnoreCase));

        //                if (hasMessageColumn && await reader.ReadAsync())
        //                {
        //                    result.Message = reader.GetString(reader.GetOrdinal("Message"));
        //                    return result; // Return immediately with the error message
        //                }

        //                // 1️⃣ Total students attempted
        //                if (await reader.ReadAsync())
        //                {
        //                    result.TotalStudentsAttempted = reader.GetInt32(reader.GetOrdinal("TotalStudentsAttempted"));
        //                }

        //                // 2️⃣ Test-wise student attempts
        //                if (await reader.NextResultAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        result.TestAttempts.Add(new TestAttemptDto
        //                        {
        //                            TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
        //                            TestName = reader.GetString(reader.GetOrdinal("TestName")),
        //                            StudentsAttempted = reader.GetInt32(reader.GetOrdinal("StudentsAttempted"))
        //                        });
        //                    }
        //                }

        //                // 3️⃣ Top 3 students
        //                if (await reader.NextResultAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        result.TopStudents.Add(new StudentResultDto
        //                        {
        //                            ResultId = reader.GetInt32(reader.GetOrdinal("ResultId")),
        //                            StudentId = reader.GetString(reader.GetOrdinal("StudentId")),
        //                            StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
        //                            TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
        //                            TestName = reader.GetString(reader.GetOrdinal("TestName")),
        //                            Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
        //                            Grade = reader.GetString(reader.GetOrdinal("Grade"))
        //                        });
        //                    }
        //                }

        //                // 4️⃣ All students
        //                if (await reader.NextResultAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        result.AllStudents.Add(new StudentResultDto
        //                        {
        //                            ResultId = reader.GetInt32(reader.GetOrdinal("ResultId")),
        //                            StudentId = reader.GetString(reader.GetOrdinal("StudentId")),
        //                            StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
        //                            TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
        //                            TestName = reader.GetString(reader.GetOrdinal("TestName")),
        //                            Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
        //                            Grade = reader.GetString(reader.GetOrdinal("Grade"))
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}

        public async Task<AdminDashboardDto> GetAdminDashboardStatus(string loggedInUserId, int testId)
        {
            var result = new AdminDashboardDto();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetAdminDashboardStatus";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var paramUser = command.CreateParameter();
                    paramUser.ParameterName = "@LoggedInUserId";
                    paramUser.Value = loggedInUserId;
                    command.Parameters.Add(paramUser);

                    var paramTest = command.CreateParameter();
                    paramTest.ParameterName = "@TestId";
                    paramTest.Value = testId;
                    command.Parameters.Add(paramTest);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // 1️⃣ Access denied message
                        bool hasMessageColumn = Enumerable.Range(0, reader.FieldCount)
                            .Any(i => reader.GetName(i).Equals("Message", StringComparison.OrdinalIgnoreCase));

                        if (hasMessageColumn && await reader.ReadAsync())
                        {
                            result.Message = reader.GetString(reader.GetOrdinal("Message"));
                            return result;
                        }

                        // 2️⃣ Total students attempted
                        if (await reader.ReadAsync())
                        {
                            result.TotalStudentsAttempted = reader.IsDBNull(reader.GetOrdinal("TotalStudentsAttempted"))
                                ? 0
                                : reader.GetInt32(reader.GetOrdinal("TotalStudentsAttempted"));
                        }

                        // 3️⃣ Test-wise student attempts
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                result.TestAttempts.Add(new TestAttemptDto
                                {
                                    TestId = reader.IsDBNull(reader.GetOrdinal("TestId")) ? 0 : reader.GetInt32(reader.GetOrdinal("TestId")),
                                    TestName = reader.IsDBNull(reader.GetOrdinal("TestName")) ? string.Empty : reader.GetString(reader.GetOrdinal("TestName")),
                                    StudentsAttempted = reader.IsDBNull(reader.GetOrdinal("StudentsAttempted")) ? 0 : reader.GetInt32(reader.GetOrdinal("StudentsAttempted"))
                                });
                            }
                        }

                        // 4️⃣ Top 3 students
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                result.TopStudents.Add(ReadStudentResult(reader));
                            }
                        }

                        // 5️⃣ All students
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                result.AllStudents.Add(ReadStudentResult(reader));
                            }
                        }
                    }
                }
            }

            return result;
        }

        // Helper method for reading StudentResultDto safely
        private StudentResultDto ReadStudentResult(DbDataReader reader)
        {
            return new StudentResultDto
            {
                ResultId = reader.IsDBNull(reader.GetOrdinal("ResultId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ResultId")),
                StudentId = reader.IsDBNull(reader.GetOrdinal("StudentId")) ? string.Empty : reader.GetString(reader.GetOrdinal("StudentId")),
                StudentName = reader.IsDBNull(reader.GetOrdinal("StudentName")) ? string.Empty : reader.GetString(reader.GetOrdinal("StudentName")),
                TestId = reader.IsDBNull(reader.GetOrdinal("TestId")) ? 0 : reader.GetInt32(reader.GetOrdinal("TestId")),
                TestName = reader.IsDBNull(reader.GetOrdinal("TestName")) ? string.Empty : reader.GetString(reader.GetOrdinal("TestName")),
                Percentage = reader.IsDBNull(reader.GetOrdinal("Percentage")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Percentage")),
                Grade = reader.IsDBNull(reader.GetOrdinal("Grade")) ? string.Empty : reader.GetString(reader.GetOrdinal("Grade")),
                Rank = reader.IsDBNull(reader.GetOrdinal("Rank")) ? 0 : reader.GetInt64(reader.GetOrdinal("Rank"))  // ✅ Added

                // CorrectAnswers = reader.IsDBNull(reader.GetOrdinal("CorrectAnswers")) ? 0 : reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
              //WrongAnswers = reader.IsDBNull(reader.GetOrdinal("WrongAnswers")) ? 0 : reader.GetInt32(reader.GetOrdinal("WrongAnswers"))
            };
        }





        public async Task<List<ExamMasterDto>> GetAllExams()
        {
            var exams = new List<ExamMasterDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetAllExamMaster";  // Stored Procedure Name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            exams.Add(new ExamMasterDto
                            {
                                ExamId = reader.GetInt32(reader.GetOrdinal("ExamId")),
                                ExamName = reader.GetString(reader.GetOrdinal("ExamName")),
                                ExamDescription = reader.GetString(reader.GetOrdinal("ExamDescription")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                //CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                            });
                        }
                    }
                }
            }

            return exams;
        }


        public async Task<List<ExamCategoryDto>> GetExamCategoriesByExamId(int ExamId)
        {
            var categories = new List<ExamCategoryDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetExamCategoryByExamId";  // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@ExamId";
                    param.Value = ExamId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categories.Add(new ExamCategoryDto
                            {
                                ExamCateId = reader.GetInt32(reader.GetOrdinal("ExamCateId")),
                                FK_ExamId = reader.GetInt32(reader.GetOrdinal("FK_ExamId")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                                CategoryDescription = reader.GetString(reader.GetOrdinal("CategoryDescription")),
                                //CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                            });
                        }
                    }
                }
            }

            return categories;
        }

        public async Task<List<ExamSubCategoryDto>> GetExamSubCategoriesByCategoryId(int ExamCateId)
        {
            var subCategories = new List<ExamSubCategoryDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetExamSubCategoryByExamCateId";  // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@ExamCateId";
                    param.Value = ExamCateId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            subCategories.Add(new ExamSubCategoryDto
                            {
                                ExamSubCateId = reader.GetInt32(reader.GetOrdinal("ExamSubCateId")),
                                FK_ExamCateId = reader.GetInt32(reader.GetOrdinal("FK_ExamCateId")),
                                SubCategoryName = reader.GetString(reader.GetOrdinal("SubCategoryName")),
                                SubCategoryDescription = reader.GetString(reader.GetOrdinal("SubCategoryDescription")),
                                //CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                            });
                        }
                    }
                }
            }

            return subCategories;
        }

        public async Task<List<TestDtos>> GetTestsByExamSubCateId(int ExamSubCateId)
        {
            var tests = new List<TestDtos>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetTestsByExamSubCateId";  // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@ExamSubCateId";
                    param.Value = ExamSubCateId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tests.Add(new TestDtos
                            {
                                TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
                                FK_ExamSubCateId = reader.GetInt32(reader.GetOrdinal("FK_ExamSubCateId")),
                                TestName = reader.GetString(reader.GetOrdinal("TestName")),
                                TestDescription = reader.GetString(reader.GetOrdinal("TestDescription")),
                                TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                DurationMinutes = reader.GetInt32(reader.GetOrdinal("DurationMinutes"))
                            });
                        }
                    }
                }
            }

            return tests;
        }

        public async Task<List<TestDtos_Category>> GetTestsByExamCateId(int ExamCateId)
        {
            var tests = new List<TestDtos_Category>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetTestsByExamCateId";  // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@ExamCateId";
                    param.Value = ExamCateId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tests.Add(new TestDtos_Category
                            {
                                TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
                                FK_ExamCateId = reader.GetInt32(reader.GetOrdinal("FK_ExamCateId")),
                                TestName = reader.GetString(reader.GetOrdinal("TestName")),
                                TestDescription = reader.GetString(reader.GetOrdinal("TestDescription")),
                                TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                DurationMinutes = reader.GetInt32(reader.GetOrdinal("DurationMinutes"))
                            });
                        }
                    }
                }
            }

            return tests;
        }

        public async Task<List<DetailedTestResultDto>> GetDetailedTestResults(string StudentId)
        {
            var testResults = new List<DetailedTestResultDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetDetailedTestResultsByStudentId";  // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@StudentId";
                    param.Value = StudentId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var dto = new DetailedTestResultDto
                            {
                                ResultId = reader.GetInt32(reader.GetOrdinal("ResultId")),
                                StudentId = reader.GetString(reader.GetOrdinal("StudentId")),
                                Stu_Name = reader.GetString(reader.GetOrdinal("Stu_Name")),
                                TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
                                TestName = reader.GetString(reader.GetOrdinal("TestName")),
                                TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
                                CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
                                WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
                                Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
                                Grade = reader.GetString(reader.GetOrdinal("Grade")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                ExamId = reader.GetInt32(reader.GetOrdinal("ExamId")),
                                ExamName = reader.GetString(reader.GetOrdinal("ExamName")),
                                ExamCateId = reader.GetInt32(reader.GetOrdinal("ExamCateId")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                                ExamSubCateId = reader.GetInt32(reader.GetOrdinal("ExamSubCateId")),
                                SubCategoryName = reader.GetString(reader.GetOrdinal("SubCategoryName"))
                            };

                            testResults.Add(dto);
                        }
                    }
                }
            }

            return testResults;
        }

        //public async Task<StudentWarningDto> UpdateStudentWarningCount(string StudentId, int TestId)
        //{
        //    var result = new StudentWarningDto();

        //    using (var connection = db.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "UpdateStudentWarningCount";
        //            command.CommandType = CommandType.StoredProcedure;

        //            var paramStudent = command.CreateParameter();
        //            paramStudent.ParameterName = "@StudentId";
        //            paramStudent.Value = StudentId;
        //            command.Parameters.Add(paramStudent);

        //            var paramTest = command.CreateParameter();
        //            paramTest.ParameterName = "@TestId";
        //            paramTest.Value = TestId;
        //            command.Parameters.Add(paramTest);

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    result.WarningCount = reader.GetInt32(reader.GetOrdinal("WarningCount"));
        //                    result.IsBlocked = reader.GetBoolean(reader.GetOrdinal("IsBlocked"));
        //                    result.Stu_Name = reader["Stu_Name"] != DBNull.Value ? reader["Stu_Name"].ToString() : "N/A";
        //                    result.TestName = reader["TestName"] != DBNull.Value ? reader["TestName"].ToString() : "N/A";
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}

        public async Task<List<DetailedTestResultModel>> GetDetailedTestResultsByAdmin(string loggedInUserId)
        {
            var testResults = new List<DetailedTestResultModel>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetDetailedTestResultsByAdmin";   // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@LoggedInUserId";
                    param.Value = loggedInUserId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // ✅ Check if stored procedure returned only "Message" column (Access Denied)
                        bool hasMessageColumn = false;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.GetName(i).Equals("Message", StringComparison.OrdinalIgnoreCase))
                            {
                                hasMessageColumn = true;
                                break;
                            }
                        }

                        if (hasMessageColumn && await reader.ReadAsync())
                        {
                            var dto = new DetailedTestResultModel
                            {
                                Message = reader["Message"].ToString()
                            };
                            testResults.Add(dto);
                            return testResults;
                        }

                        // ✅ Otherwise read actual results
                        while (await reader.ReadAsync())
                        {
                            var dto = new DetailedTestResultModel
                            {
                                ResultId = reader.GetInt32(reader.GetOrdinal("ResultId")),
                                StudentId = reader.GetString(reader.GetOrdinal("StudentId")),
                                Stu_Name = reader.GetString(reader.GetOrdinal("Stu_Name")),
                                TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
                                ExamName = reader.GetString(reader.GetOrdinal("ExamName")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                                TestName = reader.GetString(reader.GetOrdinal("TestName")),
                                TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
                                CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
                                WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
                                Percentage = Convert.ToDecimal(reader["Percentage"]),
                                Grade = reader.GetString(reader.GetOrdinal("Grade")),
                                TestDate = Convert.ToDateTime(reader["TestDate"])
                            };

                            testResults.Add(dto);
                        }
                    }
                }
            }

            return testResults;
        }


        public async Task<List<CertificateInfoDto>> GetCertificateInfoByStudentId(string StudentId)
        {
            var certificates = new List<CertificateInfoDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetCertificateInfoByStudentId"; // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@StudentId";
                    param.Value = StudentId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            certificates.Add(new CertificateInfoDto
                            {
                                ResultId = reader.GetInt32(reader.GetOrdinal("ResultId")),
                                StudentId = reader.GetString(reader.GetOrdinal("StudentId")),
                                Stu_Name = reader.IsDBNull(reader.GetOrdinal("Stu_Name")) ? "N/A" : reader.GetString(reader.GetOrdinal("Stu_Name")),
                                TestName = reader.IsDBNull(reader.GetOrdinal("TestName")) ? "N/A" : reader.GetString(reader.GetOrdinal("TestName")),
                                Percentage = reader.IsDBNull(reader.GetOrdinal("Percentage")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Percentage")),
                                Grade = reader.IsDBNull(reader.GetOrdinal("Grade")) ? "N/A" : reader.GetString(reader.GetOrdinal("Grade")),
                                TestDate = reader.IsDBNull(reader.GetOrdinal("TestDate")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("TestDate")),
                                ExamName = reader.IsDBNull(reader.GetOrdinal("ExamName")) ? "N/A" : reader.GetString(reader.GetOrdinal("ExamName")),
                                CategoryName = reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? "N/A" : reader.GetString(reader.GetOrdinal("CategoryName"))
                            });
                        }
                    }
                }
            }

            return certificates;
        }
        public async Task<TestsCountDto> GetTestsCountByStudentId(string StudentId)
        {
            var result = new TestsCountDto();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetTestsCountByStudentId"; // Stored Procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@StudentId";
                    param.Value = StudentId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            result.TotalStudentTests = reader.IsDBNull(reader.GetOrdinal("TotalStudentTests"))
                                ? 0 : reader.GetInt32(reader.GetOrdinal("TotalStudentTests"));

                            result.TotalAvailableTests = reader.IsDBNull(reader.GetOrdinal("TotalAvailableTests"))
                                ? 0 : reader.GetInt32(reader.GetOrdinal("TotalAvailableTests"));
                        }
                    }
                }
            }

            return result;
        }


        public async Task<List<StateDto>> GetAllStates()
        {
            var states = new List<StateDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetAllStates";  // Stored Procedure Name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            states.Add(new StateDto
                            {
                                StateId = reader.GetInt32(reader.GetOrdinal("StateId")),
                                StateName = reader.GetString(reader.GetOrdinal("StateName"))
                            });
                        }
                    }
                }
            }

            return states;
        }


        public async Task<List<ZoneDto>> GetZonesByStateId(int StateId)
        {
            var zones = new List<ZoneDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetZonesByStateId";  // Stored Procedure Name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@StateId";
                    param.Value = StateId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            zones.Add(new ZoneDto
                            {
                                ZoneId = reader.GetInt32(reader.GetOrdinal("ZoneId")),
                                ZoneName = reader.GetString(reader.GetOrdinal("ZoneName"))
                            });
                        }
                    }
                }
            }

            return zones;
        }


        public async Task<List<DistrictDto>> GetDistrictsByZoneId(int ZoneId)
        {
            var districts = new List<DistrictDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetDistrictsByZoneId";  // Stored Procedure Name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@ZoneId";
                    param.Value = ZoneId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            districts.Add(new DistrictDto
                            {
                                DistrictId = reader.GetInt32(reader.GetOrdinal("district_id")),
                                DistrictName = reader.GetString(reader.GetOrdinal("district_name"))
                            });
                        }
                    }
                }
            }

            return districts;
        }

        public async Task<List<ExamCategoryTestDto>> GetExamCategoryTestList()
        {
            var result = new List<ExamCategoryTestDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetExamCategoryTestList";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new ExamCategoryTestDto
                            {
                                ExamId = reader.GetInt32(0),
                                ExamName = reader.GetString(1),
                                ExamCateId = reader.GetInt32(2),
                                CategoryName = reader.GetString(3),
                                TestId = reader.GetInt32(4),
                                TestName = reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return result;
        }



        //public async Task<List<OverallSummaryViewModel>> GetOverallSummaryAsync(string loggedInUserId, int testId, string studentId)
        //{
        //    var result = new List<OverallSummaryViewModel>();
        //    using (var connection = db.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();
        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "GetOverallSummary";
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add(new SqlParameter("@LoggedInUserId", loggedInUserId));
        //            command.Parameters.Add(new SqlParameter("@TestId", testId));
        //            command.Parameters.Add(new SqlParameter("@studentid", studentId));

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    if (reader.FieldCount == 1 && reader.GetName(0) == "Message")
        //                        return new List<OverallSummaryViewModel>();

        //                    result.Add(new OverallSummaryViewModel
        //                    {
        //                        TestName = reader["TestName"].ToString(),
        //                        TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
        //                        Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
        //                        CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
        //                        WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
        //                        Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
        //                        Grade = reader["Grade"].ToString(),
        //                        Status = reader["Status"].ToString(),
        //                        TestFinishTime = reader.IsDBNull(reader.GetOrdinal("TestFinishTime")) ? null : reader.GetDateTime(reader.GetOrdinal("TestFinishTime"))
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}


        //public async Task<List<OverallSummaryViewModel>> GetOverallSummaryAsync(string loggedInUserId, int testId, string studentId)
        //{
        //    var result = new List<OverallSummaryViewModel>();

        //    using (var connection = db.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "GetOverallSummary";
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add(new SqlParameter("@LoggedInUserId", loggedInUserId));
        //            command.Parameters.Add(new SqlParameter("@TestId", testId));
        //            command.Parameters.Add(new SqlParameter("@studentid", studentId));

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    if (reader.FieldCount == 1 && reader.GetName(0) == "Message")
        //                        return new List<OverallSummaryViewModel>();

        //                    result.Add(new OverallSummaryViewModel
        //                    {
        //                        TestName = reader["TestName"].ToString(),
        //                        TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
        //                        Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
        //                        CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
        //                        WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
        //                        Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
        //                        Grade = reader["Grade"].ToString(),
        //                        Status = reader["Status"].ToString(),
        //                        TestFinishTime = reader.IsDBNull(reader.GetOrdinal("TestFinishTime"))
        //                            ? null
        //                            : reader.GetDateTime(reader.GetOrdinal("TestFinishTime")),
        //                        TimeTaken = reader["TimeTaken"].ToString() // ✅ New property
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}

        //New
        public async Task<OverallSummaryResponse> GetOverallSummaryAsync(string loggedInUserId, int testId, string studentId)
        {
            var response = new OverallSummaryResponse();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetOverallSummary";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@LoggedInUserId", loggedInUserId));
                    command.Parameters.Add(new SqlParameter("@TestId", testId));
                    command.Parameters.Add(new SqlParameter("@studentid", studentId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // ✅ Result Set 1: Summary
                        if (await reader.ReadAsync())
                        {
                            if (reader.FieldCount == 1 && reader.GetName(0) == "Message")
                                throw new Exception(reader["Message"].ToString());

                            response.Summary = new OverallSummaryViewModel
                            {
                                TestName = reader["TestName"].ToString(),
                                TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                Attempted = reader.GetInt32(reader.GetOrdinal("Attempted")),
                                CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
                                WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
                                NotAttempted = reader.GetInt32(reader.GetOrdinal("NotAttempted")),
                                Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
                                Grade = reader["Grade"].ToString(),
                                Status = reader["Status"].ToString(),
                                TestFinishTime = reader.IsDBNull(reader.GetOrdinal("TestFinishTime"))
                                    ? null
                                    : reader.GetDateTime(reader.GetOrdinal("TestFinishTime")),
                                TimeTaken = reader["TimeTaken"].ToString(),
                                TestStartTime = reader.IsDBNull(reader.GetOrdinal("TestStartTime"))
                            ? null
                            : reader.GetDateTime(reader.GetOrdinal("TestStartTime")),
                                TestEndTime = reader.IsDBNull(reader.GetOrdinal("TestEndTime"))
                            ? null
                            : reader.GetDateTime(reader.GetOrdinal("TestEndTime")),
                              IsDisqualified = reader["IsDisqualified"].ToString(),
                              ReasonForDisqualification = reader["ReasonForDisqualification"].ToString()

                            };
                        }

                        // ✅ Result Set 2: Images
                        if (await reader.NextResultAsync())
                        {
                            response.Images = new List<StudentImageViewModel>();

                            while (await reader.ReadAsync())
                            {
                                var img = new StudentImageViewModel
                                {
                                    QuestionId = reader.GetInt32(reader.GetOrdinal("QuestionId")),
                                    QuestionText = reader["QuestionText"].ToString(),
                                    AnswerImagePath = reader["AnswerImagePath"].ToString(),
                                    ImageCapturedTime = reader.IsDBNull(reader.GetOrdinal("ImageCapturedTime"))
                                        ? null
                                        : reader.GetDateTime(reader.GetOrdinal("ImageCapturedTime"))
                                };

                                // 🧩 Load image file as base64
                                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", img.AnswerImagePath.TrimStart('/'));
                                if (File.Exists(fullPath))
                                {
                                    byte[] imageBytes = await File.ReadAllBytesAsync(fullPath);
                                    img.ImageBase64 = Convert.ToBase64String(imageBytes);
                                }

                                response.Images.Add(img);
                            }
                        }

                        // ✅ Result Set 3: Student Registration Photo
                        if (await reader.NextResultAsync() && await reader.ReadAsync())
                        {
                            var profile = new StudentProfileViewModel
                            {
                                Stu_Name = reader["Stu_Name"].ToString(),
                                Stu_Photo = reader["Stu_Photo"].ToString()
                            };

                            // 🧩 Convert student photo to base64 if exists
                            string profilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", "ProfilePictures", profile.Stu_Photo);
                           // string profilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "ProfilePictures", profile.Stu_Photo);
                            if (File.Exists(profilePath))
                            {
                                byte[] profileBytes = await File.ReadAllBytesAsync(profilePath);
                                profile.Stu_PhotoBase64 = Convert.ToBase64String(profileBytes);
                            }

                            response.StudentProfile = profile;
                        }


                        if (await reader.NextResultAsync() && await reader.ReadAsync())
                        {
                            string videoPath = reader["TestVideoPath"]?.ToString();

                            if (!string.IsNullOrEmpty(videoPath))
                            {
                                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", videoPath.TrimStart('/'));
                                if (File.Exists(fullPath))
                                {
                                    byte[] videoBytes = await File.ReadAllBytesAsync(fullPath);
                                    response.VideoBase64 = Convert.ToBase64String(videoBytes);
                                    response.VideoPath = videoPath;
                                }
                            }
                        }







                    }
                }
            }

            return response;
        }





        public async Task<(List<SectionSummaryViewModel> Sections, OverallSectionSummaryViewModel Overall)>
    GetSectionWiseSummaryAsync(string loggedInUserId, int testId, string studentId)
        {
            var sectionResults = new List<SectionSummaryViewModel>();
            var overallResult = new OverallSectionSummaryViewModel();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetSectionWiseSummary";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@LoggedInUserId", loggedInUserId));
                    command.Parameters.Add(new SqlParameter("@TestId", testId));
                    command.Parameters.Add(new SqlParameter("@StudentId", studentId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // 🔹 First result set: Section-wise summary
                        while (await reader.ReadAsync())
                        {
                            if (reader.FieldCount == 1 && reader.GetName(0) == "Message")
                                return (new List<SectionSummaryViewModel>(), null);

                            sectionResults.Add(new SectionSummaryViewModel
                            {
                                SectionName = reader["SectionName"].ToString(),
                                TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                TotalQuestionsAttempted = reader.GetInt32(reader.GetOrdinal("TotalQuestionsAttempted")),
                                CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
                                WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
                                NotAttempted = reader.GetInt32(reader.GetOrdinal("NotAttempted")),
                                MarksScored = Convert.ToDecimal(reader["MarksScored"]),
                                //TimeTakenSeconds = reader.GetInt32(reader.GetOrdinal("TimeTakenSeconds")),
                                TimeTakenSeconds = reader["TimeTakenSeconds"] == DBNull.Value
    ? 0m
    : reader.GetDecimal(reader.GetOrdinal("TimeTakenSeconds")),
                                TimeTakenFormatted = reader["TimeTakenFormatted"].ToString(), // ✅ new line
                                Percentage = Convert.ToDecimal(reader["Percentage"]),
                                SectionEndTime = reader.IsDBNull(reader.GetOrdinal("SectionEndTime")) ? null : reader["SectionEndTime"].ToString()
                            
                        });


                        }

                        // 🔹 Move to next result set (overall summary)
                        if (await reader.NextResultAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                overallResult = new OverallSectionSummaryViewModel
                                {
                                    TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                    TotalAttempted = reader.GetInt32(reader.GetOrdinal("TotalAttempted")),
                                    TotalNotAttempted = reader.GetInt32(reader.GetOrdinal("TotalNotAttempted")),
                                    TotalCorrect = reader.GetInt32(reader.GetOrdinal("TotalCorrect")),
                                    TotalWrong = reader.GetInt32(reader.GetOrdinal("TotalWrong")),
                                    TotalMarksScored = Convert.ToDecimal(reader["TotalMarksScored"]),
                                    OverallPercentage = Convert.ToDecimal(reader["OverallPercentage"])
                                };
                            }
                        }
                    }
                }
            }

            return (sectionResults, overallResult);
        }




        // 3️⃣ Difficulty Level Summary
        public async Task<List<DifficultyLevelSummaryViewModel>> GetDifficultyLevelSummaryAsync(string loggedInUserId, int testId, string studentId)
        {
            var result = new List<DifficultyLevelSummaryViewModel>();
            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetDifficultyLevelSummary";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@LoggedInUserId", loggedInUserId));
                    command.Parameters.Add(new SqlParameter("@TestId", testId));
                    command.Parameters.Add(new SqlParameter("@StudentId", studentId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (reader.FieldCount == 1 && reader.GetName(0) == "Message")
                                return new List<DifficultyLevelSummaryViewModel>();

                            result.Add(new DifficultyLevelSummaryViewModel
                            {
                                DifficultyLevel = reader["DifficultyLevel"].ToString(),
                                TotalQuestions = reader.GetInt32(reader.GetOrdinal("TotalQuestions")),
                                TotalQuestionsAttempted = reader.GetInt32(reader.GetOrdinal("TotalQuestionsAttempted")),
                                CorrectAnswers = reader.GetInt32(reader.GetOrdinal("CorrectAnswers")),
                                WrongAnswers = reader.GetInt32(reader.GetOrdinal("WrongAnswers")),
                                NotAttempted = reader.GetInt32(reader.GetOrdinal("NotAttempted")),
                                MarksScored = Convert.ToDecimal(reader["MarksScored"]),
                                // TimeTakenSeconds = reader.GetInt32(reader.GetOrdinal("TimeTakenSeconds")),
                                TimeTakenSeconds = reader["TimeTakenSeconds"] == DBNull.Value
    ? 0m
    : reader.GetDecimal(reader.GetOrdinal("TimeTakenSeconds")),

                                Percentage = Convert.ToDecimal(reader["Percentage"])
                            });
                        }
                    }
                }
            }
            return result;
        }

        // 4️⃣ Question Wise Details
        public async Task<List<QuestionWiseViewModel>> GetQuestionWiseDetailsAsync(string loggedInUserId, int testId, string studentId)
        {
            var result = new List<QuestionWiseViewModel>();
            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetQuestionWiseDetails";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@LoggedInUserId", loggedInUserId));
                    command.Parameters.Add(new SqlParameter("@TestId", testId));
                    command.Parameters.Add(new SqlParameter("@studentId", studentId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (reader.FieldCount == 1 && reader.GetName(0) == "Message")
                                return new List<QuestionWiseViewModel>();

                            result.Add(new QuestionWiseViewModel
                            {
                                QuestionId = Convert.ToInt32(reader["QuestionId"]),
                                QuestionText = reader["QuestionText"].ToString(),
                                OptionA = reader["OptionA"].ToString(),
                                OptionB = reader["OptionB"].ToString(),
                                OptionC = reader["OptionC"].ToString(),
                                OptionD = reader["OptionD"].ToString(),
                                SectionName = reader["SectionName"].ToString(),
                                DifficultyLevel = reader["DifficultyLevel"].ToString(),
                                SelectedOption = reader["SelectedOption"].ToString(),
                                CorrectOption = reader["CorrectOption"].ToString(),
                                IsCorrect = reader.IsDBNull(reader.GetOrdinal("IsCorrect"))
         ? (bool?)null
         : reader.GetBoolean(reader.GetOrdinal("IsCorrect")),
                                //TimeTakenSeconds = Convert.ToInt32(reader["TimeTakenSeconds"])
                                TimeTakenSeconds = reader["TimeTakenSeconds"] == DBNull.Value
    ? 0m
    : reader.GetDecimal(reader.GetOrdinal("TimeTakenSeconds")),

                            });

                        }
                    }
                }
            }
            return result;
        }


        //public async Task<List<TopStudentDto>> GetTop10Students(string loggedInUserId, int? stateId = null, int? zoneId = null, int? districtId = null)
        //{
        //    var result = new List<TopStudentDto>();

        //    using (var connection = db.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();
        //        using (var command = connection.CreateCommand())
        //        {
        //            command.CommandText = "GetTop10Students_Admin";
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add(new SqlParameter("@LoggedInUserId", loggedInUserId));
        //            command.Parameters.Add(new SqlParameter("@StateId", stateId.HasValue ? (object)stateId.Value : DBNull.Value));
        //            command.Parameters.Add(new SqlParameter("@ZoneId", zoneId.HasValue ? (object)zoneId.Value : DBNull.Value));
        //            command.Parameters.Add(new SqlParameter("@DistrictId", districtId.HasValue ? (object)districtId.Value : DBNull.Value));

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    // Access Denied message
        //                    if (reader.FieldCount == 1 && reader.GetName(0) == "Message")
        //                        return new List<TopStudentDto>();

        //                    result.Add(new TopStudentDto
        //                    {
        //                        ResultId = Convert.ToInt32(reader["ResultId"]),
        //                        StudentId = reader["StudentId"].ToString(),
        //                        StudentName = reader["StudentName"].ToString(),
        //                        StateName = reader["StateName"].ToString(),
        //                        ZoneName = reader["ZoneName"].ToString(),
        //                        DistrictName = reader["DistrictName"].ToString(),
        //                        TestId = Convert.ToInt32(reader["TestId"]),
        //                        TestName = reader["TestName"].ToString(),
        //                        Percentage = reader["Percentage"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Percentage"])
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}

        public async Task<List<TopStudentDto>> GetTop10Students(string loggedInUserId, int? stateId = null, int? zoneId = null, int? districtId = null)
        {
            var result = new List<TopStudentDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetTop10Students_Admin";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@LoggedInUserId", loggedInUserId));
                    command.Parameters.Add(new SqlParameter("@StateId", stateId.HasValue ? (object)stateId.Value : DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@ZoneId", zoneId.HasValue ? (object)zoneId.Value : DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@DistrictId", districtId.HasValue ? (object)districtId.Value : DBNull.Value));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var dto = new TopStudentDto();

                            // Handle "Access Denied" message
                            if (reader["Message"] != DBNull.Value && !string.IsNullOrEmpty(reader["Message"].ToString()))
                            {
                                dto.Message = reader["Message"].ToString();
                                result.Add(dto);
                                break; // Stop reading further
                            }

                            dto.RankNo = reader["RankNo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RankNo"]);
                            dto.ResultId = reader["ResultId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ResultId"]);
                            dto.StudentId = reader["StudentId"]?.ToString();
                            dto.StudentName = reader["StudentName"]?.ToString();
                            dto.StateName = reader["StateName"]?.ToString();
                            dto.ZoneName = reader["ZoneName"]?.ToString();
                            dto.DistrictName = reader["DistrictName"]?.ToString();
                            dto.TestId = reader["TestId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TestId"]);
                            dto.TestName = reader["TestName"]?.ToString();
                            dto.Percentage = reader["Percentage"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Percentage"]);
                            dto.Message = reader["Message"] == DBNull.Value ? null : reader["Message"].ToString();

                            result.Add(dto);
                        }
                    }
                }
            }

            return result;
        }


        public async Task<List<StudentDetailsDto>> GetStudentDetailsByUserId(string stuUserId)
        {
            var result = new List<StudentDetailsDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetStudentDetailsByUserId";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Stu_UserID", stuUserId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new StudentDetailsDto
                            {
                                Stu_UserID = reader["Stu_UserID"]?.ToString(),
                                Stu_Name = reader["Stu_Name"]?.ToString(),
                                Stu_MobileNumber = reader["Stu_MobileNumber"]?.ToString(),
                                Stu_Email = reader["Stu_Email"]?.ToString(),
                                State_Name = reader["State_Name"]?.ToString(),
                                ZoneName = reader["ZoneName"]?.ToString(),
                                District_Name = reader["District_Name"]?.ToString(),
                                TestName = reader["TestName"]?.ToString()
                            });
                        }
                    }
                }
            }

            return result;
        }



        public async Task<List<TestRankedResultDto>> GetRankedTestResultsByTestId(int testId)
        {
            var rankedResults = new List<TestRankedResultDto>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetRankedTestResultsByTestId"; // Stored Procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@TestId";
                    param.Value = testId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            rankedResults.Add(new TestRankedResultDto
                            {
                                RankPosition = Convert.ToInt32(reader["RankPosition"]),
                                StudentId = reader.GetString(reader.GetOrdinal("StudentId")),
                                Stu_Name = reader.GetString(reader.GetOrdinal("Stu_Name")),
                                State_Name = reader.GetString(reader.GetOrdinal("state_name")),
                                ZoneName = reader.GetString(reader.GetOrdinal("ZoneName")),
                                District_Name = reader.GetString(reader.GetOrdinal("district_name")),
                                Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage"))
                            });
                        }
                    }
                }
            }

            return rankedResults;
        }


        public async Task<StudentRankDto?> GetStudentRankByTestId(int testId, string studentId)
        {
            StudentRankDto? studentRank = null;

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetStudentRankByTestId"; // Stored Procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var paramTestId = command.CreateParameter();
                    paramTestId.ParameterName = "@TestId";
                    paramTestId.Value = testId;
                    command.Parameters.Add(paramTestId);

                    var paramStudentId = command.CreateParameter();
                    paramStudentId.ParameterName = "@StudentId";
                    paramStudentId.Value = studentId;
                    command.Parameters.Add(paramStudentId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            studentRank = new StudentRankDto
                            {
                                StudentRank = Convert.ToInt32(reader["StudentRank"]),
                                StudentId = reader.GetString(reader.GetOrdinal("StudentId")),
                                Stu_Name = reader.GetString(reader.GetOrdinal("Stu_Name")),
                                Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage")),
                                TotalTimeTakenSeconds = Convert.ToInt32(reader["TotalTimeTakenSeconds"])
                            };
                        }
                    }
                }
            }

            return studentRank;
        }



        public async Task<List<ExamCategoryTestViewModel>> GetExamCategoryByStudId(string StudentId)
        {
            var tests = new List<ExamCategoryTestViewModel>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "usp_GetExamCategoryByStudId";  // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@Stu_UserID";
                    param.Value = StudentId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tests.Add(new ExamCategoryTestViewModel
                            {
                                ExamId = reader.GetInt32(reader.GetOrdinal("ExamId")),
                                ExamName = reader.GetString(reader.GetOrdinal("ExamName")),
                                ExamCateId = reader.GetInt32(reader.GetOrdinal("ExamCateId")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                                TestId = reader.GetInt32(reader.GetOrdinal("TestId")),
                                TestName = reader.GetString(reader.GetOrdinal("TestName"))

                            });
                        }
                    }
                }
            }

            return tests;
        }







    }

}

