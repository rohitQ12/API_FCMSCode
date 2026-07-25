using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using System.Data;
using Microsoft.Data.SqlClient;
using GlobalApi.Models.Authentication;
using static GlobalApi.Models.Master.Individual;
using static Slapper.AutoMapper;
using GlobalApi.JsonFile;

namespace GlobalApi.Repository.MasterRepository
{
    public class IndividualRepository : IIndividual
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        private readonly FileUpload fileUpload;

        public IndividualRepository()
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
            fileUpload = new FileUpload();
        }
        public async Task<UserRegisterResult> InsertIndividual(Individual_Images lead, string UserId)
        {
            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {
                    var userPhonenumber = await globalContext.Users.Where(x => x.UserName == lead.Ind_MobileNumber.ToString() || x.PhoneNumber == lead.Ind_MobileNumber.ToString()).FirstOrDefaultAsync();
                    var userEmail = await globalContext.Users.Where(x => x.Email == lead.Ind_Email).FirstOrDefaultAsync();
                    if (userPhonenumber != null)
                    {
                        return new UserRegisterResult { IsSuccess = false, Message = "Mobile number already Exists." };

                    }

                    else if (userEmail != null)
                    {
                        return new UserRegisterResult { IsSuccess = false, Message = "Email already exists." };
                    }

                    int id = await primarykeyvalue.primary_key("Individual");

                    // string Filename = this.fileUpload.ProcessUploadedFile("wwwroot/Images", lead.Ind_Photo);
                    // string COcumentFilename = this.fileUpload.ProcessUploadedFile("wwwroot/IndividualDocuments", lead.Ind_Choose_Document);
                    //string DoctorMOUDoc = this.fileUpload.ProcessUploadedFile("wwwroot/IndividualDocuments", lead.MOUDocument);
                    //string DoctorSignature = this.fileUpload.ProcessUploadedFile("wwwroot/DoctorDocuments", lead.Doc_Signature_Doc);
                    //if (DocumentFilename == "default_user.png")
                    //{
                    //    DocumentFilename = null;
                    //}
                    //if (DoctorMOUDoc == "default_user.png")

                    //{
                    //    DoctorMOUDoc = null;
                    //}
                    //if (CorporateSignature == "default_user.png")
                    //{
                    //    DoctorSignature = null;
                    //}

                    AuthUser objAuthUser = new AuthUser()
                    {
                        UserName = Convert.ToString(lead.Ind_MobileNumber),
                        UserId = 0,
                        FirstName = lead.Ind_Name,
                        LastName = "",
                        PhoneNumber = Convert.ToString(lead.Ind_MobileNumber),
                        //DOB = lead.DO_DOB,
                        //Gender = lead.DO_Gender,
                        Role_Id_FK = "473e54f6-9cde-491f-99b1-e8aa25d9d522",
                        Email = lead.Ind_Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsEnabled = false,
                        Inactive = "N",
                        // Imagename = Filename,
                        Id = Guid.NewGuid().ToString(),
                        PhoneNumberConfirmed = false
                    };

                    var AuthUserResult = await globalContext.Users.AddAsync(objAuthUser);
                    await globalContext.SaveChangesAsync();

                    Individual obj = new Individual()
                    {
                        Ind_Id = id,
                        Ind_UserID = objAuthUser.Id,
                        Ind_code = "IND0" + id,
                        Ind_Name = lead.Ind_Name,
                        Ind_DOB = lead.Ind_DOB,
                        Ind_Gender = lead.Ind_Gender,
                        Ind_Address = lead.Ind_Address,
                        Ind_Country_Id_FK = lead.Ind_Country_Id_FK,
                        Ind_ST_Id_FK = lead.Ind_ST_Id_FK,
                        //Ind_DI_Id_FK = lead.Ind_DI_Id_FK,
                        //taluk_Id_Fk = lead.taluk_Id_Fk,
                        //gram_Id_Fk = lead.Ind_Id_Fk,
                        //Ind_Village = lead.Ind_Village,
                        //Ind_PostalCode = lead.Ind_PostalCode,
                        Ind_MobileNumber = lead.Ind_MobileNumber,
                        // Ind_LandLineNumber = lead.Assi_LandLineNumber,
                        //Ind_AlternativeNumber = lead.Ind_AlternativeNumber,
                        Ind_Email = lead.Ind_Email,
                        Ind_fromDate = lead.Ind_fromDate,
                        Ind_ToDate = lead.Ind_ToDate,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        Ind_status = 1,
                        Remarks = "",

                        //Ind_Skill_Desc = lead.Ind_Skill_Desc

                    };
                    var result = await globalContext.Individual.AddAsync(obj);
                    await globalContext.SaveChangesAsync();

                    //saheb
                    //update Users_Payment table based on users_order_id ->"LMS34566781"
                    var paymentRecord = await db.Users_Payment
                    .FirstOrDefaultAsync(u => u.users_order_id == lead.order_id);

                    if (paymentRecord != null)
                    {
                        paymentRecord.users_id = objAuthUser.Id; // "c384d5c0-7be4-493f-a1c5-2d29b96368b0";
                        paymentRecord.users_code = obj.Ind_code;// "IND0265";
                        await db.SaveChangesAsync();
                    }



                    foreach (StudentCourse cpt in lead.Student_Course)
                    {
                        int scId = await primarykeyvalue.primary_key("student_courses");
                        student_courses objCer = new student_courses()
                        {
                            Cou_Id = scId,
                            Stu_id_fk = id,
                            cp_id = cpt.cp_id,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                        };
                        var ComplaintResult = await globalContext.student_courses.AddAsync(objCer);
                        await globalContext.SaveChangesAsync();
                    }
                    //var dlang = await doctorLanguageRepository.InsertDoctorLanguage(lead.DoctorLanguage,id);
                    await InsertUsers(obj);
                    return new UserRegisterResult { IsSuccess = true, Message = "Individual Added Successfully" };
                }

            }


            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UsersLists> InsertUsers(Individual lead)
        {
            int _id = await primarykeyvalue.primary_key("UsersLists");
            UsersLists insert = new UsersLists()
            {
                Id = _id,
                User_cat = "Individual",
                User_ref_id = lead.Ind_Id,
                created_by = 1,
                created_date = DateTime.Now,
                delete_flag = false,
                status = 1,

            };
            var _new = await db.UsersLists.AddAsync(insert);
            await db.SaveChangesAsync();
            return _new.Entity;

        }

        public async Task<UserRegisterResult> UpdateIndividual(Individual_Images lead)
        {
            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {
                    string Filename = string.Empty;
                    //var Assistant = await globalContext.Assistant.FirstOrDefaultAsync(x => x.Assi_Id == lead.Assi_Id);
                    var Individual = await globalContext.Individual.FirstOrDefaultAsync(x => x.Ind_Id == lead.Ind_Id);

                    var user = await globalContext.Users.FirstOrDefaultAsync(x => x.Id == Individual.Ind_UserID);

                    var objuserPhonenumber = await globalContext.Users.Where(x => x.UserName == lead.Ind_MobileNumber.ToString() || x.PhoneNumber == lead.Ind_MobileNumber.ToString()).FirstOrDefaultAsync();

                    var objuserEmail = await globalContext.Users.Where(x => x.Email == lead.Ind_Email).FirstOrDefaultAsync();

                    if (objuserPhonenumber != null)
                    {
                        if (objuserPhonenumber.PhoneNumber != Convert.ToString(Individual.Ind_MobileNumber))
                        {
                            return new UserRegisterResult { IsSuccess = false, Message = "MobileNumber Already Exists" };
                        }
                    }
                    if (objuserEmail != null)
                    {
                        if (objuserEmail.Email != Individual.Ind_Email)
                        {
                            return new UserRegisterResult { IsSuccess = false, Message = "Email Already Exists" };
                        }
                    }

                    //if (lead.Ind_Photo != null)
                    //{
                    //    if (Individual.Ind_Photo != null && Individual.Ind_Photo != "default_user.png")
                    //    {
                    //        string filepath = Path.Combine("wwwroot/Images", Individual.Ind_Photo);
                    //        System.IO.File.Delete(filepath);
                    //    }
                    //    Filename = this.fileUpload.ProcessUploadedFile("wwwroot/Images", lead.Ind_Photo);
                    //}
                    //else
                    //{
                    //    Filename = Individual.Ind_Photo;
                    //}



                    if (Individual.Ind_UserID.Length > 0)
                    {

                        if (user != null)
                        {
                            user.FirstName = lead.Ind_Name;
                            //  user.LastName = lead.Ind_LastName;
                            user.PhoneNumber = Convert.ToString(lead.Ind_MobileNumber);
                            user.Email = lead.Ind_Email;
                            user.Gender = lead.Ind_Gender;
                            user.DOB = lead.Ind_DOB;
                            user.Imagename = Filename;
                            await globalContext.SaveChangesAsync();
                        }
                    }
                    if (Individual != null)
                    {
                        Individual.Ind_Id = lead.Ind_Id;
                        Individual.Ind_Name = lead.Ind_Name;
                        // Individual.Ind_LastName = lead.Ind_LastName;
                        Individual.Ind_DOB = lead.Ind_DOB;
                        Individual.Ind_Gender = lead.Ind_Gender;
                        Individual.Ind_Email = lead.Ind_Email;
                        Individual.Ind_MobileNumber = lead.Ind_MobileNumber;
                        // Individual.Ind_MotherTongue = lead.Ind_MotherTongue;
                        //Assistant.Assi_Hos_Id_FK = lead.Ind_Hos_Id_FK;
                        //Individual.Ind_Qua_Id_FK = lead.Ind_Qua_Id_FK;
                        //  Assistant.Assi_Des_Id_FK = lead.Ind_Des_Id_FK;
                        // Inddent.Assi_skill_id = lead.Ind_skill_id;
                        //Assistant.Assi_Photo = uniqueFilename;
                        Individual.Ind_Address = lead.Ind_Address;
                        Individual.Ind_Country_Id_FK = lead.Ind_Country_Id_FK;
                        Individual.Ind_ST_Id_FK = lead.Ind_ST_Id_FK;
                        Individual.Ind_DI_Name = lead.Ind_DI_Name;
                        // Individual.taluk_Id_Fk = lead.taluk_Id_Fk;
                        //Assistant.gram_Id_Fk = lead.gram_Id_Fk;
                        // Individual.Ind_Village = lead.Ind_Village;
                        //  Individual.Ind_PostalCode = lead.Ind_PostalCode;
                        // Assistant.Assi_LandLineNumber = lead.Ind_LandLineNumber;
                        // Individual.Ind_AlternativeNumber = lead.Ind_AlternativeNumber;
                        Individual.modified_by = 2;
                        Individual.Ind_fromDate = lead.Ind_fromDate;
                        Individual.Ind_ToDate = lead.Ind_ToDate;
                        Individual.modified_date = DateTime.Now;
                        Individual.delete_flag = false;
                        Individual.Ind_status = 2;
                        Individual.Remarks = "";
                        //  Individual.Ind_Skill_Desc = lead.Ind_Skill_Desc;
                        await globalContext.SaveChangesAsync();

                        globalContext.RemoveRange(globalContext.student_courses.Where(x => x.Stu_id_fk == lead.Ind_Id));
                        await globalContext.SaveChangesAsync();
                        foreach (StudentCourse cpt in lead.Student_Course)
                        {
                            int scId = await primarykeyvalue.primary_key("student_courses");
                            student_courses objCer = new student_courses()
                            {
                                Cou_Id = scId,
                                Stu_id_fk = lead.Ind_Id,
                                cp_id = cpt.cp_id,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var ComplaintResult = await globalContext.student_courses.AddAsync(objCer);
                            await globalContext.SaveChangesAsync();
                        }
                        return new UserRegisterResult { IsSuccess = true, Message = "Individual Updated Successfully" };
                    }
                    return new UserRegisterResult { IsSuccess = false, Message = "Individual Updated Not Successfully" };
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /*
                public async Task<List<GetAllIndividual>> GetAllIndividual(int? Assi_Hos_Id_FK, string roleaction)
                {
                    if (db != null)
                    {
                        var query = (from a in db.Student
                                     //join b in db.Hospital on a.Assi_Hos_Id_FK equals b.Hos_Id into blist
                                     //from b in blist.DefaultIfEmpty()
                                     join c in db.Qualification on a.Stu_Qua_Id_FK equals c.qualification_id into clist
                                     from c in clist.DefaultIfEmpty()
                                     //join d in db.Designation on a.Assi_Des_Id_FK equals d.designation_id into dlist
                                     //from d in dlist.DefaultIfEmpty()
                                     //join e in db.SkillSets on a.Assi_skill_id equals e.Skillset_id into elist
                                     //from e in elist.DefaultIfEmpty()
                                     join f in db.States on a.Stu_ST_Id_FK equals f.stat_id into flist
                                     from f in flist.DefaultIfEmpty()
                                     join g in db.Districts on a.Stu_DI_Id_FK equals g.district_id into glist
                                     from g in glist.DefaultIfEmpty()
                                     join h in db.Countries on a.Stu_Country_Id_FK equals h.cntry_id into hlist
                                     from h in hlist.DefaultIfEmpty()
                                     join i in db.Taluk on a.taluk_Id_Fk equals i.Taluk_id into ilist
                                     from i in ilist.DefaultIfEmpty()
                                     //join j in db.Gram on a.gram_Id_Fk equals j.Gram_id into jlist
                                     //from j in jlist.DefaultIfEmpty()
                                     join k in db.Language_MST on a.Stu_MotherTongue equals k.Id into klist
                                     from k in klist.DefaultIfEmpty()
                                     join l in db.Status on a.Stu_status equals l.sts_id
                                     join m in db.Users on a.Stu_UserID equals m.Id
                                     where a.Stu_Id != 0
                                     //where roleaction == "Hospital" ? a.Assi_Hos_Id_FK == Assi_Hos_Id_FK : a.Stu_Id > 0
                                     orderby a.Stu_Id descending
                                     select new GetAllStudent
                                     {
                                         Stu_Id = a.Stu_Id,
                                         Stu_code = a.Stu_Code,
                                         Stu_FirstName = a.Stu_FirstName,
                                         Stu_LastName = a.Stu_LastName,
                                         Stu_DOB = a.Stu_DOB,
                                         Stu_Gender = a.Stu_Gender,
                                         Stu_MotherTongue = a.Stu_MotherTongue,
                                         Language = k.Language,
                                         //Stu_Hos_Id_FK = a.Assi_Hos_Id_FK,
                                         //Assi_Hos_HospitalName = b.Hos_HospitalName,
                                         Stu_Qua_Id_FK = a.Stu_Qua_Id_FK,
                                         Stu_qualification = c.qualification_Name,
                                        // Stu_Des_Id_FK = a.Assi_Des_Id_FK,
                                         //Stu_Designation = d.designation_desc,
                                         //Stu_skill_id = a.Assi_skill_id,
                                         //Stu_Skill = e.Skillset_name,
                                         Stu_Photo = a.Stu_Photo,
                                         Stu_Image = configurationRoot.GetSection("AppUrl").Value + "Images/" + m.Imagename,
                                         Stu_Address = a.Stu_Address,
                                         Stu_Country_Id_FK = a.Stu_Country_Id_FK,
                                         Stu_Country_name = h.country_name,
                                         Stu_ST_Id_FK = a.Stu_ST_Id_FK,
                                         state_name = f.state_name,
                                         Stu_DI_Id_FK = a.Stu_DI_Id_FK,
                                         district_name = g.district_name,
                                         taluk_Id_Fk = a.taluk_Id_Fk,
                                         taluk_name = i.Taluk_name,
                                         //gram_Id_Fk = a.gram_Id_Fk,
                                        // gram_name = j.Gram_name,
                                         Stu_Village = a.Stu_Village,
                                         Stu_PostalCode = a.Stu_PostalCode,
                                         Stu_MobileNumber = m.PhoneNumber,
                                        // Stu_LandLineNumber = a.Stu_LandLineNumber,
                                         Stu_AlternativeNumber = a.Stu_AlternativeNumber,
                                         Stu_Email = m.Email,
                                         delete_flag = a.delete_flag,
                                         Stu_status = a.Stu_status,
                                         sts_name = l.sts_name,
                                         Stu_Skill_Desc = a.Stu_Skill_Desc,
                                     });
                        return await query.ToListAsync();

                    }
                    return null;

                }
        */
        //public async Task<List<Student_DD>> GetAssistant_DD(int? Assi_Hos_Id_FK, string roleaction)
        //{
        //    if (db != null)
        //    {
        //        var query = (from a in db.Student
        //                         //where a.delete_flag == false && a.status == 1 
        //                         //&& (roleaction == "Hospital" ? a.Assi_Hos_Id_FK == Assi_Hos_Id_FK : a.Stu_Id > 0)
        //                     orderby a.Stu_FirstName
        //                     select new Student_DD
        //                     {
        //                         Stu_Id = a.Stu_Id,
        //                         Stu_code = a.Stu_Code,
        //                         Stu_FirstName = a.Stu_FirstName,
        //                         Stu_LastName = a.Stu_LastName,
        //                     }).ToListAsync();
        //        return await query;
        //    }
        //    return null;
        //}
        public async Task<UserRegisterResult> DeleteIndividual(int Ind_Id)
        {
            try
            {
                var result = await db.Individual.FirstOrDefaultAsync(x => x.Ind_Id == Ind_Id);
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == result.Ind_UserID);
                if (user != null)
                {
                    user.IsEnabled = false;
                    user.Inactive = "Y";
                    await db.SaveChangesAsync();
                }
                if (result != null)
                {
                    result.Ind_Id = Ind_Id;
                    result.delete_flag = true;
                    result.Ind_status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return new UserRegisterResult { IsSuccess = true, Message = "Individual Deleted Successfully" };
                }

                return new UserRegisterResult { IsSuccess = false, Message = "Individual Details Does Not Exists" };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IndividualById> GetIndividualById(int Ind_Id, string roleaction)
        {
            if (db != null)
            {
                var query = (from a in db.Individual
                             join f in db.States on a.Ind_ST_Id_FK equals f.stat_id into flist
                             from f in flist.DefaultIfEmpty()
                             join h in db.Countries on a.Ind_Country_Id_FK equals h.cntry_id into hlist
                             from h in hlist.DefaultIfEmpty()
                             join l in db.Status on a.Ind_status equals l.sts_id
                             join m in db.Users on a.Ind_UserID equals m.Id
                             where a.Ind_Id == Ind_Id && a.Ind_Id != 0
                             select new IndividualById
                             {
                                 Ind_Id = a.Ind_Id,
                                 Ind_code = a.Ind_code,
                                 Ind_Name = a.Ind_Name,
                                 //  Ind_LastName = a.Ind_LastName,
                                 Ind_DOB = a.Ind_DOB,
                                 Ind_Gender = a.Ind_Gender,
                                 // Ind_MotherTongue = a.Ind_MotherTongue,
                                 // Language = k.Language,
                                 // Stu_Hos_Id_FK = a.Stu_Hos_Id_FK,
                                 //Stu_Hos_HospitalName = b.Hos_HospitalName,
                                 // Stu_Qua_Id_FK = a.Stu_Qua_Id_FK,
                                 //  Ind_qualification = c.qualification_Name,
                                 // Stu_Des_Id_FK = a.Stu_Des_Id_FK,
                                 // Stu_Designation = d.designation_desc,
                                 //Stu_skill_id = a.Assi_skill_id,
                                 //Stu_Skill = e.Skillset_name,
                                 //  Ind_Photo = a.Ind_Photo,
                                 // Ind_Image = configurationRoot.GetSection("AppUrl").Value + "Images/" + m.Imagename,
                                 Ind_Address = a.Ind_Address,
                                 Ind_Country_Id_FK = a.Ind_Country_Id_FK,
                                 Ind_Country_name = h.country_name,
                                 Ind_ST_Id_FK = a.Ind_ST_Id_FK,
                                 state_name = f.state_name,
                                 // Ind_DI_Id_FK = a.Ind_DI_Id_FK,
                                 district_name = a.Ind_DI_Name,
                                 Ind_fromDate = a.Ind_fromDate,
                                 Ind_ToDate = a.Ind_ToDate,
                                 //  taluk_Id_Fk = a.taluk_Id_Fk,
                                 // taluk_name = i.Taluk_name,
                                 //gram_Id_Fk = a.gram_Id_Fk,
                                 //gram_name = j.Gram_name,
                                 // Ind_Village = a.Ind_Village,
                                 // Ind_PostalCode = a.Ind_PostalCode,
                                 Ind_MobileNumber = m.PhoneNumber,
                                 // Ind_LandLineNumber = a.Ind_LandLineNumber,
                                 // Ind_AlternativeNumber = a.Ind_AlternativeNumber,
                                 Ind_Email = m.Email,
                                 GetAllStudentCourse = (from k in db.student_courses
                                                        join v in db.Course_Package on k.cp_id equals v.cp_id
                                                        where k.Stu_id_fk == a.Ind_Id
                                                        select new GetAllStudentCourse
                                                        {
                                                            Cou_Id = k.Cou_Id,
                                                            cp_id = k.cp_id,
                                                            cu_name = v.cu_name,
                                                            cp_amount = v.cp_amount

                                                        }).ToList(),
                                 delete_flag = a.delete_flag,
                                 Ind_status = a.Ind_status,
                                 sts_name = l.sts_name,
                                 //  Ind_Skill_Desc = a.Ind_Skill_Desc
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
        //                         PartnerName = string.Concat(a.Stu_FirstName, " - ", d.PhoneNumber),
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






        public async Task<dynamic> GetIndividualDetails()
        {
            var query = (from a in db.Individual
                             // join c in db.Hospital on a.DO_HO_Id_FK equals c.Hos_Id
                         join d in db.Users on a.Ind_UserID equals d.Id
                         where
                         //a.DO_HO_Id_FK == HospitalId &&
                         a.delete_flag == false && a.Ind_status == 3 && a.Ind_Id != 0
                         select new
                         {
                             Id = a.Ind_Id,
                             UserID = d.Id,
                             PartnerName = string.Concat(a.Ind_Name, " - ", d.PhoneNumber),
                             FirstName = a.Ind_Name,
                             //LastName = a.Ind_LastName,
                             Email = a.Ind_Email,
                             MobileNumber = d.PhoneNumber,
                             //HospitalName = c.Hos_HospitalName

                         }).ToListAsync();
            return await query;
        }


        public async Task<UserRegisterResult> ApproveIndividual(ApproveIndividual approveIndividual)
        {
            try
            {

                var result = await db.Individual.Where(x => x.Ind_Id == approveIndividual.Ind_Id).FirstOrDefaultAsync();
                if (result.Ind_status != 3)
                {
                    result.Ind_status = 3;
                    if (approveIndividual.Remarks.Length < 0)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = approveIndividual.Remarks;
                    await db.SaveChangesAsync();
                    return new UserRegisterResult { IsSuccess = true, Message = "Individual Approved Successfully" };
                }
                return new UserRegisterResult { IsSuccess = false, Message = "Individual Details Does Not Exists" };

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<List<GetAllIndividual>> GetAllIndividual()
        {
            if (db != null)
            {
                var query = (from a in db.Individual
                                 //join b in db.Hospital on a.Assi_Hos_Id_FK equals b.Hos_Id into blist
                                 //from b in blist.DefaultIfEmpty()
                             join f in db.States on a.Ind_ST_Id_FK equals f.stat_id into flist
                             from f in flist.DefaultIfEmpty()
                                 //join g in db.Districts on a.Ind_DI_Id_FK equals g.district_id into glist
                                 //from g in glist.DefaultIfEmpty()
                             join h in db.Countries on a.Ind_Country_Id_FK equals h.cntry_id into hlist
                             from h in hlist.DefaultIfEmpty()
                             join l in db.Status on a.Ind_status equals l.sts_id
                             join m in db.Users on a.Ind_UserID equals m.Id
                             where a.Ind_Id != 0
                             //where roleaction == "Hospital" ? a.Assi_Hos_Id_FK == Assi_Hos_Id_FK : a.Ind_Id > 0
                             orderby a.Ind_Id descending
                             select new GetAllIndividual
                             {
                                 Ind_Id = a.Ind_Id,
                                 Ind_code = a.Ind_code,
                                 Ind_Name = a.Ind_Name,
                                 // Ind_LastName = a.Ind_LastName,
                                 Ind_DOB = a.Ind_DOB,
                                 Ind_Gender = a.Ind_Gender,
                                 // Ind_MotherTongue = a.Ind_MotherTongue,
                                 //  Language = k.Language,
                                 //Ind_Hos_Id_FK = a.Assi_Hos_Id_FK,
                                 //Assi_Hos_HospitalName = b.Hos_HospitalName,
                                 // Ind_Qua_Id_FK = a.Ind_Qua_Id_FK,
                                 //  Ind_qualification = c.qualification_Name,
                                 // Ind_Des_Id_FK = a.Assi_Des_Id_FK,
                                 //Ind_Designation = d.designation_desc,
                                 //Ind_skill_id = a.Assi_skill_id,
                                 //Ind_Skill = e.Skillset_name,
                                 // Ind_Photo = a.Ind_Photo,
                                 Ind_Image = configurationRoot.GetSection("AppUrl").Value + "Images/" + m.Imagename,
                                 Ind_Address = a.Ind_Address,
                                 Ind_Country_Id_FK = a.Ind_Country_Id_FK,
                                 Ind_Country_name = h.country_name,
                                 Ind_ST_Id_FK = a.Ind_ST_Id_FK,
                                 state_name = f.state_name,
                                 // Ind_DI_Id_FK = a.Ind_DI_Id_FK,
                                 district_name = a.Ind_DI_Name,
                                 // taluk_Id_Fk = a.taluk_Id_Fk,
                                 // taluk_name = i.Taluk_name,
                                 //gram_Id_Fk = a.gram_Id_Fk,
                                 // gram_name = j.Gram_name,
                                 //  Ind_Village = a.Ind_Village,
                                 // Ind_PostalCode = a.Ind_PostalCode,
                                 Ind_MobileNumber = m.PhoneNumber,
                                 Ind_fromDate = a.Ind_fromDate,
                                 Ind_ToDate = a.Ind_ToDate,
                                 // Ind_LandLineNumber = a.Ind_LandLineNumber,
                                 // Ind_AlternativeNumber = a.Ind_AlternativeNumber,
                                 Ind_Email = m.Email,
                                 GetAllStudentCourse = (from k in db.student_courses
                                                        join v in db.Course_Package on k.cp_id equals v.cp_id
                                                        where k.Stu_id_fk == a.Ind_Id
                                                        select new GetAllStudentCourse
                                                        {
                                                            Cou_Id = k.Cou_Id,
                                                            cp_id = k.cp_id,
                                                            cu_name = v.cu_name,
                                                            cp_amount = v.cp_amount

                                                        }).ToList(),
                                 delete_flag = a.delete_flag,
                                 Ind_status = a.Ind_status,
                                 sts_name = l.sts_name,
                                 // Ind_Skill_Desc = a.Ind_Skill_Desc,
                             });
                return await query.ToListAsync();

            }
            return null;

        }
    }
}

