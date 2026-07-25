using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using System.Data;
using Microsoft.Data.SqlClient;
using GlobalApi.Models.Authentication;
using Dapper;
using System.Linq;
using System.Reflection.Emit;
using System;
using GlobalApi.JsonFile;

namespace GlobalApi.Repository.MasterRepository
{
    public class CorporateRepository : ICorporate
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
      
        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        private readonly FileUpload fileUpload;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CorporateRepository(IWebHostEnvironment webHostEnvironment)
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
          
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
            fileUpload = new FileUpload();
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<UserRegisterResult> InsertCorporate(Corporate_Images lead, string UserId)
        {
            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {
                    var userPhonenumber = await globalContext.Users.Where(x => x.UserName == lead.CO_MobileNumber.ToString() || x.PhoneNumber == lead.CO_MobileNumber.ToString()).FirstOrDefaultAsync();
                    var userEmail = await globalContext.Users.Where(x => x.Email == lead.CO_Email).FirstOrDefaultAsync();
                    var coorporateCode = await globalContext.Corporate.Where(x => x.Co_Reg_No == lead.Co_Reg_No).FirstOrDefaultAsync();
                    if (userPhonenumber != null)
                    {
                        return new UserRegisterResult { IsSuccess = false, Message = "Mobile number already Exists." };

                    }

                    else if (userEmail != null)
                    {
                        return new UserRegisterResult { IsSuccess = false, Message = "Email already exists." };
                    }


                    if(coorporateCode != null)
                    {
                        return new UserRegisterResult { IsSuccess = false, Message = "Corporate Register  already Exists." };
                    }

                    int id = await primarykeyvalue.primary_key("Corporate");

                    //string Filename = this.fileUpload.ProcessUploadedFile("wwwroot/Images", lead.CO_Photo);
                    //string COcumentFilename = this.fileUpload.ProcessUploadedFile("wwwroot/CorporateDocuments", lead.CO_Choose_Document);
                    //string DoctorMOUDoc = this.fileUpload.ProcessUploadedFile("wwwroot/CorporateDocuments", lead.MOUDocument);
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
                        UserName = Convert.ToString(lead.CO_MobileNumber),
                        UserId = 0,
                        FirstName = lead.CO_Name,
                        PhoneNumber = Convert.ToString(lead.CO_MobileNumber),
                        //DOB = lead.DO_DOB,
                        //Gender = lead.DO_Gender,
                        Role_Id_FK = "9b1b5533-f527-4711-8cbb-8f654dcc249f",
                        Email = lead.CO_Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsEnabled = false,
                        Inactive = "N",
                        //Imagename = Filename,
                        Id = Guid.NewGuid().ToString(),
                        PhoneNumberConfirmed = false
                    };

                    var AuthUserResult = await globalContext.Users.AddAsync(objAuthUser);
                    await globalContext.SaveChangesAsync();

                    Corporate obj = new Corporate()
                    {
                        CO_Id = id,
                     
                        CO_UserId = objAuthUser.Id,
                        CO_Code ="COP0" + id,
                        CO_Name = lead.CO_Name,
                        Co_Reg_No=lead.Co_Reg_No,
                        //CO_DOB = lead.DO_DOB,
                        //CO_Gender = lead.DO_Gender,
                        // CO_MotherTongue = lead.DO_MotherTongue,
                        CO_Address = lead.CO_Address,
                        CO_Country_Id_FK = lead.CO_Country_Id_FK,
                        CO_ST_Id_FK = lead.CO_ST_Id_FK,
                        //CO_DI_Id_FK = lead.CO_DI_Id_FK,
                       
                        // CO_Gram_Id = lead.CO_Gram_Id,
                        CO_MobileNumber = lead.CO_MobileNumber,
                        //CO_OfficialNumber = lead.DO_OfficialNumber,
                        CO_Email = lead.CO_Email,
                        // CO_HO_Id_FK = lead.DO_HO_Id_FK,
                        // CO_QU_Id_FK = lead.DO_QU_Id_FK,
                        // CO_DE_Id_FK = lead.DO_DE_Id_FK,
                        // CO_CD_Id_FK = lead.DO_CD_Id_FK,
                        //CO_SP_Id_FK = lead.DO_SP_Id_FK,
                       // CO_Photo = Filename,
                        //CO_UserId_FK = lead.DO_UserId_FK,
                        CO_Alernative_Numb = lead.CO_Alernative_Numb,
                        CO_PANno = lead.CO_PANno,
                        CO_GSTno = lead.CO_GSTno,
                        Co_no_sub=lead.Co_no_sub,
                        Co_FromDate=lead.Co_FromDate,
                        Co_DI_Name=lead.Co_DI_Name,
                        Co_ToDate =lead.Co_ToDate,
                        Co_PostalCode = lead.Co_PostalCode,
                        //CO_RegNo = lead.CO_RegNo,
                       // DO_Type = "Hospital",
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1,
                        // DO_Choose_Document = DocumentFilename,
                       // ClinicName = lead.ClinicName,
                       // MOUDocument = DoctorMOUDoc,
                        //Doc_Signature_Doc = DoctorSignature,
                                                        // CO_SD_Id_FK = lead.DO_SD_Id_FK,
                                                        // CO_Spc_Id_FK = lead.DO_Spc_Id_FK,
                                                        // CO_SC_Id_FK = lead.DO_SC_Id_FK,

                    };
                    var result = await globalContext.Corporate.AddAsync(obj);
                    await globalContext.SaveChangesAsync();
                    //var dlang = await doctorLanguageRepository.InsertDoctorLanguage(lead.DoctorLanguage,id);
                    await InsertUsers(obj);

                    foreach (StudentCourse cpt in lead.Student_Course)
                    {
                        int scId = await primarykeyvalue.primary_key("student_courses");
                        student_courses objCer = new student_courses()
                        {
                            Cou_Id = scId,
                            Co_id_fk = id,
                            cp_id = cpt.cp_id,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                        };
                        var ComplaintResult = await globalContext.student_courses.AddAsync(objCer);
                        await globalContext.SaveChangesAsync();
                    }
                    return new UserRegisterResult { IsSuccess = true, Message = "Corporate added successfully." };
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UsersLists> InsertUsers(Corporate lead)
        {
            int _id = await primarykeyvalue.primary_key("UsersLists");
            UsersLists insert = new UsersLists()
            {
                Id = _id,
                User_cat = "Corporate",
                User_ref_id = lead.CO_Id,
                created_by = 1,
                created_date = DateTime.Now,
                delete_flag = false,
                status = 1

            };
            var _new = await db.UsersLists.AddAsync(insert);
            await db.SaveChangesAsync();
            return _new.Entity;
        }

        public async Task<UserRegisterResult> UpdateCorporate(Corporate_ImagesUP lead)
        {
            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {
                    string Filename = string.Empty;
                    string DocumentFilename = string.Empty;
                    string DoctorMOUDoc = string.Empty;
                    string DoctorSignatureName = string.Empty;
                    var corporate = await globalContext.Corporate.FirstOrDefaultAsync(x => x.CO_Id == lead.CO_Id);
                    var user = await globalContext.Users.FirstOrDefaultAsync(x => x.Id == corporate.CO_UserId);
                    var objuserPhonenumber = await globalContext.Users.Where(x => x.UserName == lead.CO_MobileNumber.ToString() || x.PhoneNumber == lead.CO_MobileNumber.ToString()).FirstOrDefaultAsync();
                    var objuserEmail = await globalContext.Users.Where(x => x.Email == lead.CO_Email).FirstOrDefaultAsync();

                    if (objuserPhonenumber != null)
                    {
                        if (objuserPhonenumber.PhoneNumber != Convert.ToString(lead.CO_MobileNumber))
                        {
                            return new UserRegisterResult { IsSuccess = false, Message = "Mobile number already exists." };
                        }
                    }
                    if (objuserEmail != null)
                    {
                        if (objuserEmail.Email != corporate.CO_Email)
                        {
                            return new UserRegisterResult { IsSuccess = false, Message = "Email already exists." };
                        }
                    }

                    //if (lead.CO_Photo != null)
                    //{
                    //    if (corporate.CO_Photo != null && corporate.CO_Photo != "default_user.png")
                    //    {
                    //        string filepath = Path.Combine("wwwroot/Images", corporate.CO_Photo);
                    //        System.IO.File.Delete(filepath);
                    //    }
                    //    Filename = this.fileUpload.ProcessUploadedFile("wwwroot/Images", lead.CO_Photo);
                    //}
                    //else
                    //{
                    //    Filename = corporate.CO_Photo;
                    //}
                    //if (lead.CO_Choose_Document != null)
                    //{
                    //    if (corporate.CO_Choose_Document != null && corporate.CO_Choose_Document != " ")
                    //    {
                    //        string filepath = Path.Combine("wwwroot/DoctorDocuments", corporate.CO_Choose_Document);
                    //        File.Delete(filepath);
                    //    }
                    //    DocumentFilename = this.fileUpload.ProcessUploadedFile("wwwroot/CorporateDocuments", lead.CO_Choose_Document);
                    //}
                    //else
                    //{
                    //    DocumentFilename = corporate.CO_Choose_Document;
                    //}
                    //if (lead.MOUDocument != null)
                    //{
                    //    if (corporate.MOUDocument != null && corporate.MOUDocument != " ")
                    //    {
                    //        string filepath = Path.Combine("wwwroot/DoctorDocuments", corporate.MOUDocument);
                    //        System.IO.File.Delete(filepath);
                    //    }
                    //    DoctorMOUDoc = this.fileUpload.ProcessUploadedFile("wwwroot/CorporateDocuments", lead.MOUDocument);
                    //}
                    //else
                    //{
                    //    DoctorMOUDoc = corporate.MOUDocument;
                    //}


                    //if (lead.Doc_Signature_Doc != null)
                    //{
                    //    if (Doctor.DO_Choose_Document != null && Doctor.DO_Choose_Document != " ")
                    //    {
                    //        string filepath = Path.Combine("wwwroot/DoctorDocuments", Doctor.DO_Choose_Document);
                    //        File.Delete(filepath);
                    //    }
                    //    DoctorSignatureName = this.fileUpload.ProcessUploadedFile("wwwroot/DoctorDocuments", lead.Doc_Signature_Doc);
                    //}
                    //else
                    //{
                    //    DoctorSignatureName = Corporate.Coc_Signature_Doc;
                    //}


                    if (corporate.CO_UserId.Length > 0)
                    {
                        if (user != null)
                        {
                            user.FirstName = lead.CO_Name;
                            user.PhoneNumber = Convert.ToString(lead.CO_MobileNumber);
                            user.Email = lead.CO_Email;
                            //user.Gender = lead.CO_Gender;
                            //user.DOB = lead.CO_DOB;
                            user.Imagename = Filename;
                            await globalContext.SaveChangesAsync();
                        }
                    }
                    if (corporate != null)
                    {

                        corporate.CO_Id = lead.CO_Id;
                        //Doctor.DO_Code = lead.DO_Code;
                        corporate.CO_Name = lead.CO_Name;
                        corporate.Co_Reg_No = lead.Co_Reg_No;

                        //Doctor.CO_DOB = lead.DO_DOB;
                        //Doctor.CO_Gender = lead.DO_Gender;
                        //Doctor.DO_MotherTongue = lead.DO_MotherTongue;
                       // corporate.CO_Address = lead.CO_Address;
                        corporate.CO_Country_Id_FK = lead.CO_Country_Id_FK;
                        corporate.CO_ST_Id_FK = lead.CO_ST_Id_FK;
                        //corporate.CO_DI_Id_FK = lead.CO_DI_Id_FK;
                        corporate.Co_DI_Name = lead.Co_DI_Name;

                        //Doctor.CO_Taluk_Id = lead.DO_Taluk_Id;
                        //Doctor.CO_Gram_Id = lead.DO_Gram_Id;
                        corporate.CO_MobileNumber = lead.CO_MobileNumber;
                        corporate.CO_Email = lead.CO_Email;
                    
                        corporate.CO_Photo = Filename;
                     
                        corporate.CO_PANno = lead.PANno;
                        corporate.CO_GSTno = lead.GSTno;
                        corporate.Co_no_sub = lead.Co_no_sub;
                        corporate.Co_FromDate = lead.Co_FromDate;
                        corporate.Co_ToDate = lead.Co_ToDate;
                        corporate.modified_by = 2;
                        corporate.modified_date = DateTime.Now;
                        //corporate.ClinicName = lead.ClinicName;
                        corporate.delete_flag = false;
                        corporate.status = 2;
                        corporate.Co_PostalCode = lead.Co_PostalCode;
                        corporate.CO_Alernative_Numb = lead.CO_Alernative_Numb;
                        // Doctor.DO_Choose_Document = DocumentFilename;
                        //corporate.DO_Type = lead.DO_Type;
                       // corporate.CO_SD_Id_FK = lead.DO_SD_Id_FK;
                       //corporate.CO_Spc_Id_FK = lead.DO_Spc_Id_FK;
                       // corporate.CO_SC_Id_FK = lead.DO_SC_Id_FK;
                       // corporate.Coc_Signature_Doc = DoctorSignatureName;
                        await globalContext.SaveChangesAsync();
                        globalContext.RemoveRange(globalContext.student_courses.Where(x => x.Co_id_fk == lead.CO_Id));
                        await globalContext.SaveChangesAsync();
                        foreach (StudentCourse cpt in lead.Student_Course)
                        {
                            int scId = await primarykeyvalue.primary_key("student_courses");
                            student_courses objCer = new student_courses()
                            {
                                Cou_Id = scId,
                                Co_id_fk =lead.CO_Id,
                                cp_id = cpt.cp_id,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var ComplaintResult = await globalContext.student_courses.AddAsync(objCer);
                            await globalContext.SaveChangesAsync();
                        }
                        return new UserRegisterResult { IsSuccess = true, Message = "Corporate updated successfully." };
                    }
                    return new UserRegisterResult { IsSuccess = false, Message = "Corporate details not updated." };
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

       

        public async Task<List<GetAllCorporate>> GetAllCorporate()
        {
            var query = (from a in db.Corporate
                         join b in db.States on a.CO_ST_Id_FK equals b.stat_id into blist
                         from b in blist.DefaultIfEmpty()
                         //join c in db.Districts on a.CO_DI_Id_FK equals c.district_id into clist
                         //from c in clist.DefaultIfEmpty()
                         join i in db.Countries on a.CO_Country_Id_FK equals i.cntry_id into ilist
                         from i in ilist.DefaultIfEmpty()
                         //join j in db.Taluk on a.CO_Taluk_Id equals j.Taluk_id into jlist
                         //from j in jlist.DefaultIfEmpty()
                         join m in db.Status on a.status equals m.sts_id
                         join n in db.Users on a.CO_UserId equals n.Id
                       
                         //join z in db.Network on a.CO_NE_Id_FK equals z.NE_Id into zlist
                         //from z in zlist.DefaultIfEmpty()
                         where a.CO_Id != 0
                         orderby a.CO_Id descending
                         select new GetAllCorporate
                         {

                             CO_Id =a.CO_Id,
                             CO_Code=a.CO_Code,
                             CO_CorporateName=a.CO_Name,
                             Co_Reg_No = a.Co_Reg_No,
                             CO_Email = a.CO_Email,
                             CO_CorporatePhoneNo=a.CO_MobileNumber,
                             CO_Alernative_Numb=a.CO_Alernative_Numb,
                             CO_Address=a.CO_Address,
                             GSTno=a.CO_GSTno,
                             PANno=a.CO_PANno,
                             CO_Country_Id_FK=a.CO_Country_Id_FK,
                             CO_ST_Id_FK =a.CO_ST_Id_FK,
                             //CO_DI_Id_FK=a.CO_DI_Id_FK,
                             Co_DI_Name = a.Co_DI_Name,
                             Co_PostalCode = a.Co_PostalCode,
                            


                             GetAllStudentCourse = (from k in db.student_courses
                                                    join v in db.Course_Package on k.cp_id equals v.cp_id
                                                    where k.Co_id_fk == a.CO_Id
                                                    select new GetAllStudentCourse
                                                    {
                                                        Cou_Id = k.Cou_Id,
                                                        cp_id = k.cp_id,
                                                        cu_name = v.cu_name,
                                                        cp_amount = v.cp_amount

                                                    }).ToList(),
                             delete_flag =a.delete_flag,
                             status=a.status,
                             Remarks=a.Remarks,
                             CO_Country_name = i.country_name,
                             CO_state_name = b.state_name,
                             //CO_district_name = c.district_name,
                            // Taluk_name = j.Taluk_name,
                             CO_CorporateLogo = a.CO_Choose_Document,
                            // CO_Landline=a.CO_Alernative_Numb,
                             sts_name = m.sts_name,
                             Co_no_sub = a.Co_no_sub,
                             Co_FromDate = a.Co_FromDate,
                             Co_ToDate = a.Co_ToDate,

                         });
            return await query.ToListAsync();
        }

        public async Task<UserRegisterResult> DeleteCorporate(int CO_Id)
        {
            try
            {
                var result = await db.Corporate.FirstOrDefaultAsync(x => x.CO_Id == CO_Id);
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == result.CO_UserId);
                if (user != null)
                {
                    user.IsEnabled = false;
                    user.Inactive = "Y";
                }
                if (result != null)
                {
                    result.CO_Id = CO_Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return new UserRegisterResult { IsSuccess = true, Message = "Corporate Deleted Successfully" };
                }
                return new UserRegisterResult { IsSuccess = false, Message = "Corporate Details Does Not Exists" };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<CorporateById> GetCorporateById(int CO_Id)
        {
            var query = (from a in db.Corporate
                         join b in db.States on a.CO_ST_Id_FK equals b.stat_id into blist
                         from b in blist.DefaultIfEmpty()
                         //join c in db.Districts on a.CO_DI_Id_FK equals c.district_id into clist
                         //from c in clist.DefaultIfEmpty()
                            
                         join i in db.Countries on a.CO_Country_Id_FK equals i.cntry_id into ilist
                         from i in ilist.DefaultIfEmpty()
                         //join j in db.Taluk on a.CO_Taluk_Id equals j.Taluk_id into jlist
                         //from j in jlist.DefaultIfEmpty()
                             
                         join m in db.Status on a.status equals m.sts_id
                         join n in db.Users on a.CO_UserId equals n.Id
                         where a.CO_Id == CO_Id

                         select new CorporateById
                         {
                             CO_Id = a.CO_Id,
                             CO_Code = a.CO_Code,
                             CO_CorporateName = a.CO_Name,
                             Co_Reg_No = a.Co_Reg_No,

                             CO_Email = a.CO_Email,
                             CO_CorporatePhoneNo = a.CO_MobileNumber,
                             CO_Alernative_Numb = a.CO_Alernative_Numb,
                             CO_Address = a.CO_Address,
                             GSTno = a.CO_GSTno,
                             PANno = a.CO_PANno,
                             CO_Country_Id_FK = a.CO_Country_Id_FK,
                             CO_ST_Id_FK = a.CO_ST_Id_FK,
                             Co_DI_Name = a.Co_DI_Name,
                             Co_PostalCode = a.Co_PostalCode,
                             //CO_DI_Id_FK = a.CO_DI_Id_FK,
                             GetAllStudentCourse = (from k in db.student_courses
                                                    join v in db.Course_Package on k.cp_id equals v.cp_id
                                                    where k.Co_id_fk == a.CO_Id
                                                    select new GetAllStudentCourse
                                                    {
                                                        Cou_Id = k.Cou_Id,
                                                        cp_id = k.cp_id,
                                                        cu_name = v.cu_name,
                                                        cp_amount = v.cp_amount

                                                    }).ToList(),
                             delete_flag = a.delete_flag,
                             status = a.status,
                             Remarks = a.Remarks,
                             CO_Country_name = i.country_name,
                             CO_state_name = b.state_name,
                            // CO_district_name = c.district_name,
                            // Taluk_name = j.Taluk_name,
                             CO_CorporateLogo = a.CO_Choose_Document,
                             sts_name = m.sts_name,
                             Co_no_sub = a.Co_no_sub,
                             Co_FromDate = a.Co_FromDate,
                             Co_ToDate = a.Co_ToDate,

                         }).FirstOrDefaultAsync();
            return await query;
        }


        public async Task<dynamic> GetCorporateDetails(int corporateId)
        {
            var query = (from a in db.Corporate
                         //join c in db.Hospital on a.DO_HO_Id_FK equals c.Hos_Id
                         join d in db.Users on a.CO_UserId equals d.Id
                         // where a.DO_HO_Id_FK == HospitalId &&
                         where a.delete_flag == false && a.status == 3 && a.CO_Id != 0
                         select new
                         {
                             Id = a.CO_Id,
                             UserID = d.Id,
                             PartnerName = string.Concat(a.CO_Name),
                             FirstName = a.CO_Name,
                             Email = a.CO_Email,
                             MobileNumber = d.PhoneNumber,
                           //  HospitalName = c.Hos_HospitalName

                         }).ToListAsync();
            return await query;

        }

        public async Task<UserRegisterResult> ApproveCorporate(ApproveCorporate lead)
        {
            try
            {

                var result = await db.Corporate.Where(x => x.CO_Id == lead.CO_Id).FirstOrDefaultAsync();
                if (result.status != 3)
                {
                    //result.cntry_id = lead.cntry_id;
                    result.status = 3;
                    if (lead.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = lead.Remarks;
                    await db.SaveChangesAsync();
                    return new UserRegisterResult { IsSuccess = true, Message = "Corporate Approved Successfully" };
                }
                return new UserRegisterResult { IsSuccess = false, Message = "Corporate Details Does Not Exists" };

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllCorporateIds>> GetAllCorporateIds()
        {
            var query = (from a in db.Corporate
                         join b in db.States on a.CO_ST_Id_FK equals b.stat_id into blist
                         from b in blist.DefaultIfEmpty()
                         join m in db.Status on a.status equals m.sts_id
                         join n in db.Users on a.CO_UserId equals n.Id
                         where a.CO_Id != 0
                         orderby a.CO_Id descending
                         select new GetAllCorporateIds
                         {

                             CO_Id = a.CO_Id,
                             CO_Code = a.CO_Code,
                             Co_Reg_No = a.Co_Reg_No,

                             CO_Name = a.CO_Name,  

                         });
            return await query.ToListAsync();
        }


    }
    
  

        
    }

