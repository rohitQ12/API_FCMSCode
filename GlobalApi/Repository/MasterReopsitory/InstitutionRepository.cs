using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Models.Authentication;
using System;
using static Slapper.AutoMapper;
using GlobalApi.JsonFile;

namespace GlobalApi.Repository.MasterRepository
{
    public class InstitutionRepository : IInstitution
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        private readonly FileUpload fileUpload;
        public InstitutionRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
            fileUpload = new FileUpload();
        }
        public async Task<UserRegisterResult> InsertInstitution(Institution_Images lead)
        {
            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {
                    var userPhonenumber = await globalContext.Users.Where(x => x.UserName == lead.Ins_InstitutionPhoneNo || x.PhoneNumber == lead.Ins_InstitutionPhoneNo).FirstOrDefaultAsync();
                    var userEmail = await globalContext.Users.Where(x => x.Email == lead.Ins_InstitutionEmail).FirstOrDefaultAsync();
                    var InstitiutionCode = await globalContext.Institution.Where(x => x.Ins_Reg_No == lead.Ins_Reg_No).FirstOrDefaultAsync();
                    if (userPhonenumber != null)
                    {
                        return new UserRegisterResult { IsSuccess = false, Message = "MobileNumber Already Exists" };
                       
                    }

                    else if (userEmail != null)
                    {
                        return new UserRegisterResult { IsSuccess = false, Message = "Email Already Exists" };

                       
                    }

                    int InstitutionId = await primarykeyvalue.primary_key("Institution");
                    // string Filename = this.fileUpload.ProcessUploadedFile("wwwroot/Images", lead.Ins_InstitutionLogo);
                    // string InstitutionMOUDoc = this.fileUpload.ProcessUploadedFile("wwwroot/Hospital", lead.MOUDocument);

                    if (InstitiutionCode != null)
                    {
                        return new UserRegisterResult { IsSuccess = false, Message = "Institution Register Already Exists" };


                    }

                    AuthUser objAuthUser = new AuthUser()
                    {
                        UserName = lead.Ins_InstitutionPhoneNo,
                        UserId = 0,
                        FirstName = lead.Ins_InstitutionName,
                        LastName = "",
                        PhoneNumber = lead.Ins_InstitutionPhoneNo,
                        NormalizedUserName = lead.Ins_InstitutionPhoneNo,
                        Role_Id_FK = "1ea5ecc1-d1df-42c7-be93-5c3549a69209",
                        Email = lead.Ins_InstitutionEmail,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsEnabled = false,
                        Inactive = "Y",
                        // Imagename = Filename,
                        Id = Guid.NewGuid().ToString(),
                        PhoneNumberConfirmed = false
                    };

                    var AuthUserResult = await globalContext.Users.AddAsync(objAuthUser);
                    await globalContext.SaveChangesAsync();

                    Institution obj = new Institution()
                    {
                        Ins_Id =InstitutionId,
                        Ins_InstitutionCode = "INS0" + InstitutionId,
                        Ins_UserID = objAuthUser.Id,
                        Ins_Reg_No = lead.Ins_Reg_No,
                        //Ins_InstitutionCode = lead.Ins_InstitutionCode,
                        Ins_InstitutionName = lead.Ins_InstitutionName,
                        //Hos_Type_Id = lead.Hos_Type_Id,
                        //Hos_cat_Id = lead.Hos_cat_Id,
                        //Hos_Branch = lead.Hos_Branch != null ? lead.Hos_Branch : 0,
                        Ins_InstitutionEmail = lead.Ins_InstitutionEmail,
                        Ins_InstitutionPhoneNo = lead.Ins_InstitutionPhoneNo,
                        Ins_InstitutionAddress = lead.Ins_InstitutionAddress,
                        Ins_Country_Id_FK = lead.Ins_Country_Id_FK,
                        Ins_ST_Id_FK = lead.Ins_ST_Id_FK,
                        Ins_DI_Name = lead.Ins_DI_Name,
                        //Ins_DI_Id_FK = lead.Ins_DI_Id_FK,
                        //Hos_Gram_Id = lead.Ins_Gram_Id,
                        Ins_no_sub = lead.Ins_no_sub,
                        Ins_Landline = lead.Ins_Landline,
                        // Ins_InstitutionLogo = Filename,
                        Ins_fromDate = lead.Ins_fromDate,
                        Ins_ToDate = lead.Ins_ToDate,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1,
                        //OwnerName = lead.Ins_OwnerName,
                        Ins_GSTno = lead.Ins_GSTno,
                        Ins_PANno = lead.Ins_PANno,
                        Ins_PostalCode = lead.Ins_PostalCode,
                        Ins_Alernative_Numb = lead.Ins_Alernative_Numb,
                        // MOUDocument = InstitutionMOUDoc,
                        Ins_web_url = lead.Ins_web_url,

                    };
                    var result = await globalContext.Institution.AddAsync(obj);
                    await InsertUsers(obj);
                    await globalContext.SaveChangesAsync();

                    foreach (StudentCourse cpt in lead.Student_Course)
                    {
                        int scId = await primarykeyvalue.primary_key("student_courses");
                        student_courses objCer = new student_courses()
                        {
                            Cou_Id = scId,
                            Ins_id_fk = InstitutionId,
                            cp_id = cpt.cp_id,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                        };
                        var ComplaintResult = await globalContext.student_courses.AddAsync(objCer);
                        await globalContext.SaveChangesAsync();
                    }
                    return new UserRegisterResult { IsSuccess = true, Message = "Institution Added Successfully" };

                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<UsersLists> InsertUsers(Institution lead)
        {
            try
            {
                int _id = await primarykeyvalue.primary_key("UsersLists");
                UsersLists insert = new UsersLists()
                {
                    Id = _id,
                    User_cat = "Institution",
                    User_ref_id = lead.Ins_Id,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1,

                };
                var _new = await db.UsersLists.AddAsync(insert);
                await db.SaveChangesAsync();
                return _new.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserRegisterResult> UpdateInstitution(Institution_Images lead)
        {
            try
            {

                using (GlobalContext globalContext = new GlobalContext())
                {
                    string Filename = string.Empty;
                    string HospitalMOUDoc = string.Empty;
                    var Institution = await globalContext.Institution.FirstOrDefaultAsync(x => x.Ins_Id == lead.Ins_Id);

                    var user = await globalContext.Users.FirstOrDefaultAsync(x => x.Id == Institution.Ins_UserID);

                    var objuserPhonenumber = await globalContext.Users.Where(x => x.UserName == lead.Ins_InstitutionPhoneNo || x.PhoneNumber == lead.Ins_InstitutionPhoneNo).FirstOrDefaultAsync();

                    var objuserEmail = await globalContext.Users.Where(x => x.Email == lead.Ins_InstitutionEmail).FirstOrDefaultAsync();

                    if (objuserPhonenumber != null)
                    {
                        if (objuserPhonenumber.PhoneNumber != Institution.Ins_InstitutionPhoneNo)
                        {
                            return new UserRegisterResult { IsSuccess = false, Message = "MobileNumber Already Exists" };
                        }
                    }
                    if (objuserEmail != null)
                    {
                        if (objuserEmail.Email != Institution.Ins_InstitutionEmail)
                        {
                            return new UserRegisterResult { IsSuccess = false, Message = "Email Already Exists" };

                          
                        }
                    }
                    //if (lead.Ins_InstitutionLogo != null)
                    //{
                    //    if (Institution.Ins_InstitutionLogo != null && Institution.Ins_InstitutionLogo != "default_user.png")
                    //    {
                    //        string filepath = Path.Combine("wwwroot/Images", Institution.Ins_InstitutionLogo);
                    //        System.IO.File.Delete(filepath);
                    //    }
                    //    Filename = this.fileUpload.ProcessUploadedFile("wwwroot/Images", lead.Ins_InstitutionLogo);
                    //}
                    //else
                    //{
                    //    Filename = Institution.Ins_InstitutionLogo;
                    //}
                    //if (lead.MOUDocument != null)
                    //{
                    //    if (Institution.MOUDocument != null && Institution.MOUDocument != " ")
                    //    {
                    //        string filepath = Path.Combine("wwwroot/Hospital", Institution.MOUDocument);
                    //        System.IO.File.Delete(filepath);
                    //    }
                    //    HospitalMOUDoc = this.fileUpload.ProcessUploadedFile("wwwroot/Hospital", lead.MOUDocument);
                    //}
                    //else
                    //{
                    //    HospitalMOUDoc = Institution.MOUDocument;
                    //}

                    if (Institution.Ins_UserID.Length > 0)
                    {

                        if (user != null)
                        {
                            user.FirstName = "";
                            user.LastName = "";
                            user.PhoneNumber = lead.Ins_InstitutionPhoneNo;
                            user.Email = lead.Ins_InstitutionEmail;
                            user.Imagename = Filename;
                            await globalContext.SaveChangesAsync();
                        }
                    }

                    if (Institution != null)
                    {
                        Institution.Ins_InstitutionCode = lead.Ins_InstitutionCode;
                        Institution.Ins_InstitutionName = lead.Ins_InstitutionName;
                        Institution.Ins_no_sub = lead.Ins_no_sub;
                        Institution.Ins_Reg_No = lead.Ins_Reg_No;
                        //Institution.Ins_cat_Id = lead.Hos_cat_Id;
                        //Institution.Ins_Branch = lead.Ins_Branch != null ? lead.Ins_Branch : 0;
                        Institution.Ins_InstitutionEmail = lead.Ins_InstitutionEmail;
                        Institution.Ins_InstitutionPhoneNo = lead.Ins_InstitutionPhoneNo;
                        Institution.Ins_InstitutionAddress = lead.Ins_InstitutionAddress;
                        Institution.Ins_GSTno = lead.Ins_GSTno;
                        Institution.Ins_PANno = lead.Ins_PANno;
                        Institution.Ins_Country_Id_FK = lead.Ins_Country_Id_FK;
                        Institution.Ins_ST_Id_FK = lead.Ins_ST_Id_FK;
                        Institution.Ins_DI_Name = lead.Ins_DI_Name;
                        //Institution.Ins_DI_Id_FK = lead.Ins_DI_Id_FK;
                        //Institution.Hos_Gram_Id = lead.Hos_Gram_Id;
                        Institution.Ins_PostalCode = lead.Ins_PostalCode;
                        Institution.Ins_Alernative_Numb = lead.Ins_Alernative_Numb;
                        //result.Hos_village = lead.Hos_village;
                        Institution.Ins_Landline = lead.Ins_Landline;
                        Institution.modified_by = 1;
                        Institution.modified_date = DateTime.Now;
                        Institution.delete_flag = false;
                        Institution.status = 2;
                        Institution.Ins_web_url = lead.Ins_web_url;
                        Institution.Ins_fromDate = lead.Ins_fromDate;
                        Institution.Ins_ToDate = lead.Ins_ToDate;
                        await globalContext.SaveChangesAsync();
                        globalContext.RemoveRange(globalContext.student_courses.Where(x => x.Ins_id_fk == lead.Ins_Id));
                        await globalContext.SaveChangesAsync();
                        foreach (StudentCourse cpt in lead.Student_Course)
                        {
                            int scId = await primarykeyvalue.primary_key("student_courses");
                            student_courses objCer = new student_courses()
                            {
                                Cou_Id = scId,
                                Ins_id_fk = lead.Ins_Id,
                                cp_id = cpt.cp_id,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                            };
                            var ComplaintResult = await globalContext.student_courses.AddAsync(objCer);
                            await globalContext.SaveChangesAsync();
                        }
                        return new UserRegisterResult { IsSuccess = true, Message = "Institution Updated Successfully" };

                    }

                    return new UserRegisterResult { IsSuccess = false, Message = "Institution Not Updated" };

                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //public async Task<List<GetAllHospital>> GetAllHospital()
        //{
        //    try
        //    {
        //        var query = (from a in db.Hospital
        //                     join b in db.States on a.Hos_ST_Id_FK equals b.stat_id into blist
        //                     from b in blist.DefaultIfEmpty()
        //                     join c in db.Districts on a.Hos_DI_Id_FK equals c.district_id into clist
        //                     from c in clist.DefaultIfEmpty()
        //                     join d in db.Network on a.Hos_NE_Id_FK equals d.NE_Id into dlist
        //                     from d in dlist.DefaultIfEmpty()
        //                     join e in db.Countries on a.Hos_Country_Id_FK equals e.cntry_id into elist
        //                     from e in elist.DefaultIfEmpty()
        //                     join f in db.Hos_Type on a.Hos_Type_Id equals f.Id into flist
        //                     from f in flist.DefaultIfEmpty()
        //                     join g in db.Category on a.Hos_cat_Id equals g.id into glist
        //                     from g in glist.DefaultIfEmpty()
        //                     join h in db.Taluk on a.Hos_Taluk_Id equals h.Taluk_id into hlist
        //                     from h in hlist.DefaultIfEmpty()
        //                     join i in db.Gram on a.Hos_Gram_Id equals i.Gram_id into ilist
        //                     from i in ilist.DefaultIfEmpty()
        //                     join k in db.Status on a.status equals k.sts_id
        //                     join l in db.Users on a.Hos_UserID equals l.Id
        //                     where a.Ins_Id != 0
        //                     orderby a.Ins_Id descending
        //                     select new GetAllInstitution
        //                     {
        //                         Hos_Id = a.Hos_Id,
        //                         Hos_HospitalCode = a.Hos_HospitalCode,
        //                         Hos_HospitalName = a.Hos_HospitalName,
        //                         Hos_Type_Id = a.Hos_Type_Id,
        //                         TypeName = f.Type,
        //                         Hos_cat_Id = a.Hos_cat_Id,
        //                         CatName = g.name,
        //                         Hos_Branch = a.Hos_Branch,
        //                         Hos_BranchName = (from j in db.Hospital
        //                                           where j.Hos_Id == (a.Hos_Branch == null ? 1 : a.Hos_Branch)
        //                                           select j.Hos_HospitalName).FirstOrDefault(),
        //                         Hos_HospitalEmail = l.Email,
        //                         Hos_HospitalPhoneNo = l.PhoneNumber,
        //                         Hos_HospitalAddress = a.Hos_HospitalAddress,
        //                         PrimaryorBranch = a.PrimaryorBranch,
        //                         GSTno = a.GSTno,
        //                         PANno = a.PANno,
        //                         RegNo = a.RegNo,
        //                         Hos_Country_Id_FK = a.Hos_Country_Id_FK,
        //                         Hos_Country_name = e.country_name,
        //                         Hos_ST_Id_FK = a.Hos_ST_Id_FK,
        //                         Hos_state_name = b.state_name,
        //                         Hos_DI_Id_FK = a.Hos_DI_Id_FK,
        //                         Hos_district_name = c.district_name,
        //                         Hos_Taluk_Id = a.Hos_Taluk_Id,
        //                         Taluk_name = h.Taluk_name,
        //                         Hos_Gram_Id = a.Hos_Gram_Id,
        //                         Hos_Village = a.Hos_Village,
        //                         Gram_name = i.Gram_name,
        //                         Hos_PostalCode = a.Hos_PostalCode,
        //                         Hos_NE_Id_FK = a.Hos_NE_Id_FK,
        //                         NE_Description = d.NE_Description,
        //                         Hos_Alterno = a.Hos_Alterno,
        //                         Hos_Landline = a.Hos_Landline,
        //                         Hos_HospitalLogo = configurationRoot.GetSection("AppUrl").Value + "Images/" + a.Hos_HospitalLogo, //a.Hos_HospitalLogo,
        //                         Hos_NetworkLogo = configurationRoot.GetSection("AppUrl").Value + "Images/" + "network_logo.jpg",
        //                         //Hos_HospitalLogo = a.Hos_HospitalLogo,
        //                         //Hos_Image = configurationRoot.GetSection("AppUrl").Value + "Images/" + l.Imagename,
        //                         delete_flag = a.delete_flag,
        //                         status = a.status,
        //                         sts_name = k.sts_name,
        //                         Remarks = a.Remarks,
        //                         MOUDocument = configurationRoot.GetSection("AppUrl").Value + "Hospital/" + a.MOUDocument,
        //                         Hos_web_url = a.Hos_web_url,
        //                     });
        //        return await query.ToListAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        public async Task<List<GetAllInstitution>> GetAllInstitution()
        {
            try
            {
                var query = (from a in db.Institution
                             join b in db.States on a.Ins_ST_Id_FK equals b.stat_id into blist
                             from b in blist.DefaultIfEmpty()
                                 //join c in db.Districts on a.Ins_DI_Id_FK equals c.district_id into clist
                                 //from c in clist.DefaultIfEmpty()
                             join e in db.Countries on a.Ins_Country_Id_FK equals e.cntry_id into elist
                             from e in elist.DefaultIfEmpty()
                             join j in db.Status on a.status equals j.sts_id
                             join l in db.Users on a.Ins_UserID equals l.Id
                             where a.Ins_Id != 0
                             orderby a.Ins_Id descending
                             select new GetAllInstitution
                             {
                                 Ins_Id = a.Ins_Id,
                                 Ins_InstitutionCode = a.Ins_InstitutionCode,
                                 Ins_InstitutionName = a.Ins_InstitutionName,
                                 Ins_no_sub = a.Ins_no_sub,
                                 Ins_Reg_No = a.Ins_Reg_No,
                //TypeName = f.Type,
                //Hos_cat_Id = a.Hos_cat_Id,
                //CatName = g.name,
                //Ins_BranchName = (from l in db.Hospital
                //                  where l.Hos_Id == (a.Hos_Branch == null ? 1 : a.Hos_Branch)
                //                  select l.Hos_HospitalName).FirstOrDefault(),
                Ins_InstitutionEmail = l.Email,
                                 Ins_InstitutionPhoneNo = l.PhoneNumber,
                                 Ins_InstitutionAddress = a.Ins_InstitutionAddress,
                                 //PrimaryorBranch = a.PrimaryorBranch,
                                 GSTno = a.Ins_GSTno,
                                 PANno = a.Ins_PANno,
                                 Ins_Country_Id_FK = a.Ins_Country_Id_FK,
                                 Ins_Country_name = e.country_name,
                                 Ins_ST_Id_FK = a.Ins_ST_Id_FK,
                                 Ins_state_name = b.state_name,
                                 Ins_DI_Name = a.Ins_DI_Name,
                                 Ins_Alernative_Numb = a.Ins_Alernative_Numb,
                                 Ins_PostalCode = a.Ins_PostalCode,
                                 Ins_Landline = a.Ins_Landline,
                                 //Ins_DI_Id_FK = a.Ins_DI_Id_FK,
                                 //Ins_district_name = c.district_name,
                                 //Hos_Gram_Id = a.Hos_Gram_Id,
                                 //Gram_name = i.Gram_name,

                                 //Ins_InstitutionLogo = configurationRoot.GetSection("AppUrl").Value + "Images/" + a.Ins_InstitutionLogo, //a.Hos_HospitalLogo,
                                 //Ins_NetworkLogo = configurationRoot.GetSection("AppUrl").Value + "Images/" + "network_logo.jpg",
                                 GetAllStudentCourse = (from k in db.student_courses
                                                        join v in db.Course_Package on k.cp_id equals v.cp_id
                                                        where k.Ins_id_fk == a.Ins_Id
                                                        select new GetAllStudentCourse
                                                        {
                                                            Cou_Id = k.Cou_Id,
                                                            cp_id = k.cp_id,
                                                            cu_name = v.cu_name,
                                                            cp_amount = v.cp_amount

                                                        }).ToList(),
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = j.sts_name,
                                 Remarks = a.Remarks,
                                // MOUDocument = configurationRoot.GetSection("AppUrl").Value + "Institution/" + a.MOUDocument,
                                 Ins_web_url = a.Ins_web_url,
                                 Ins_fromDate = a.Ins_fromDate,
                                 Ins_ToDate = a.Ins_ToDate,

                             });
                return await query.ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Institution_DD>> GetInstitution_DD()
        {
            var query = (from a in db.Institution
                         where a.delete_flag == false
                         && a.status == 3 && a.Ins_Id != 0
                         orderby a.Ins_InstitutionName
                         select new Institution_DD
                         {
                             Ins_Id = a.Ins_Id,
                             Ins_InstitutionCode = a.Ins_InstitutionCode,
                             Ins_InstitutionName = a.Ins_InstitutionName,
                         }).ToListAsync();
            return await query;
        }

        //public async Task<List<Hospital_DD>> GetHospital_DD(int? Hos_Id, string roleaction)
        //{
        //    var query = (from a in db.Hospital
        //                 where a.delete_flag == false && a.status != 6 && a.status == 3
        //                 && (roleaction == "Hospital" ? a.Hos_Id == Hos_Id : a.Hos_Id > 0)
        //                 orderby a.Hos_HospitalName
        //                 select new Hospital_DD
        //                 {
        //                     Hos_Id = a.Hos_Id,
        //                     Hos_HospitalCode = a.Hos_HospitalCode,
        //                     Hos_HospitalName = a.Hos_HospitalName,
        //                 }).ToListAsync();
        //    return await query;
        //}


        //public async Task<List<Hospital_DD>> GetPrimaryHospitalDD(int? Hos_Id, string roleaction)
        //{
        //    var query = (from a in db.Hospital
        //                 where a.delete_flag == false && a.status != 6 && a.status == 3
        //                 && (roleaction == "Hospital" ? a.Hos_Id == Hos_Id : a.Hos_Id > 0) && a.Hos_Branch == 0
        //                 orderby a.Hos_HospitalName
        //                 select new Hospital_DD
        //                 {
        //                     Hos_Id = a.Hos_Id,
        //                     Hos_HospitalCode = a.Hos_HospitalCode,
        //                     Hos_HospitalName = a.Hos_HospitalName,
        //                 }).ToListAsync();
        //    return await query;
        //}
        //public async Task<List<NetworkHospital_DD>> GetNetworkHospital_DD(int NE_Id)
        //{
        //    var query = (from a in db.Hospital
        //                 join b in db.Network on a.Hos_NE_Id_FK equals b.NE_Id
        //                 where a.delete_flag == false && a.status == 1 && a.Hos_Id != 0 && b.NE_Id != 0
        //                 && b.delete_flag == false && b.status != 6
        //                 && a.Hos_NE_Id_FK == NE_Id
        //                 orderby a.Hos_HospitalName
        //                 select new NetworkHospital_DD
        //                 {
        //                     Hos_Id = a.Hos_Id,
        //                     Hos_HospitalCode = a.Hos_HospitalCode,
        //                     Hos_HospitalName = a.Hos_HospitalName,
        //                     Hos_NE_Id_FK = b.NE_Id,
        //                     Hos_Description = b.NE_Description,
        //                 }).ToListAsync();
        //    return await query;
        //}

        //public async Task<List<Usercategory_DD>> GetHospitalCategory_DD(int? Hos_Id, string roleaction)
        //{
        //    var query = (from a in db.Hospital
        //                 where a.delete_flag == false && a.status != 6 && a.status == 3
        //                 && (roleaction == "Hospital" ? a.Hos_Id == Hos_Id : a.Hos_Id > 0)
        //                 orderby a.Hos_HospitalName
        //                 select new Usercategory_DD
        //                 {
        //                     Cat_Id = a.Hos_Id,
        //                     Code = a.Hos_HospitalCode,
        //                     Name = a.Hos_HospitalName,

        //                 }).ToListAsync();
        //    return await query;
        //}
        //public async Task<dynamic> GetHospitalSpeciality()
        //{
        //    var query = (from a in db.Hospital
        //                 where a.delete_flag == false && a.status == 3
        //                 orderby a.Hos_HospitalName ascending
        //                 select new
        //                 {
        //                     Hos_Id = a.Hos_Id,
        //                     Hos_HospitalCode = a.Hos_HospitalCode,
        //                     Hos_HospitalName = a.Hos_HospitalName,

        //                 }).ToListAsync();
        //    return await query;
        //}
        public async Task<UserRegisterResult> DeleteInstitution(int Ins_Id)
        {
            try
            {
                var result = await db.Institution.FirstOrDefaultAsync(x => x.Ins_Id == Ins_Id);
                if (result != null)
                {
                    result.Ins_Id = Ins_Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return new UserRegisterResult { IsSuccess = true, Message = "Institution Deleted Successfully" };
                }
                return new UserRegisterResult { IsSuccess = false, Message = "Institution Details Does Not Exists" };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<InstitutionById> GetInstitutionById(int? Ins_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Institution
                             join b in db.States on a.Ins_ST_Id_FK equals b.stat_id into blist
                             from b in blist.DefaultIfEmpty()
                                 //join c in db.Districts on a.Ins_DI_Id_FK equals c.district_id into clist
                                 //from c in clist.DefaultIfEmpty()
                             join e in db.Countries on a.Ins_Country_Id_FK equals e.cntry_id into elist
                             from e in elist.DefaultIfEmpty()
                                 //join f in db.Hos_Type on a.Hos_Type_Id equals f.Id into flist
                                 //from f in flist.DefaultIfEmpty()
                                 //join g in db.Category on a.Hos_cat_Id equals g.id into glist
                                 //from g in glist.DefaultIfEmpty()
                            
                                 //join i in db.Gram on a.Hos_Gram_Id equals i.Gram_id into ilist
                                 //from i in ilist.DefaultIfEmpty()
                             join j in db.Status on a.status equals j.sts_id
                             join l in db.Users on a.Ins_UserID equals l.Id
                             where a.Ins_Id == Ins_Id && a.status != 0
                             select new InstitutionById
                             {
                                 Ins_Id = a.Ins_Id,
                                 Ins_InstitutionCode = a.Ins_InstitutionCode,
                                 Ins_InstitutionName = a.Ins_InstitutionName,
                                 Ins_no_sub = a.Ins_no_sub,
                                 Ins_Reg_No = a.Ins_Reg_No,
                //TypeName = f.Type,
                //Hos_cat_Id = a.Hos_cat_Id,
                //CatName = g.name,
                //Ins_BranchName = (from l in db.Hospital
                //                  where l.Hos_Id == (a.Hos_Branch == null ? 1 : a.Hos_Branch)
                //                  select l.Hos_HospitalName).FirstOrDefault(),
                Ins_InstitutionEmail = l.Email,
                                 Ins_InstitutionPhoneNo = l.PhoneNumber,
                                 Ins_InstitutionAddress = a.Ins_InstitutionAddress,
                                 //PrimaryorBranch = a.PrimaryorBranch,
                                 GSTno = a.Ins_GSTno,
                                 PANno = a.Ins_PANno,
                                 Ins_Country_Id_FK = a.Ins_Country_Id_FK,
                                 Ins_Country_name = e.country_name,
                                 Ins_ST_Id_FK = a.Ins_ST_Id_FK,
                                 Ins_state_name = b.state_name,
                                 Ins_DI_Name = a.Ins_DI_Name,
                                 Ins_Alernative_Numb = a.Ins_Alernative_Numb,
                                 Ins_PostalCode = a.Ins_PostalCode,
                                 // Ins_district_name = c.district_name,

                                 //Hos_Gram_Id = a.Hos_Gram_Id,
                                 //Gram_name = i.Gram_name,

                                 Ins_Landline = a.Ins_Landline,
                                 //Ins_InstitutionLogo = configurationRoot.GetSection("AppUrl").Value + "Images/" + a.Ins_InstitutionLogo, //
                                 //Ins_NetworkLogo = configurationRoot.GetSection("AppUrl").Value + "Images/" + "network_logo.jpg",

                                 GetAllStudentCourse = (from k in db.student_courses
                                                        join v in db.Course_Package on k.cp_id equals v.cp_id
                                                        where k.Ins_id_fk == a.Ins_Id
                                                        select new GetAllStudentCourse
                                                        {
                                                            Cou_Id = k.Cou_Id,
                                                            cp_id = k.cp_id,
                                                            cu_name = v.cu_name,
                                                            cp_amount = v.cp_amount

                                                        }).ToList(),
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = j.sts_name,
                                 Remarks = a.Remarks,
                                 //MOUDocument = configurationRoot.GetSection("AppUrl").Value + "Hospital/" + a.MOUDocument,
                                 Ins_web_url = a.Ins_web_url,
                                 Ins_fromDate = a.Ins_fromDate,
                                 Ins_ToDate = a.Ins_ToDate,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<UserRegisterResult> ApproveInstitution(ApproveIns lead)
        {
            try
            {
                var result = await db.Institution.Where(x => x.Ins_Id == lead.Ins_Id).FirstOrDefaultAsync();
                if (result.status != 3)
                {
                    //result.Hos_Id = lead.Hos_Id;
                    result.status = 3;
                    if (lead.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = lead.Remarks;
                    await db.SaveChangesAsync();
                    return new UserRegisterResult { IsSuccess = true, Message = "Institution Approved Successfully" };
                }
                return new UserRegisterResult { IsSuccess = false, Message = "Institution Details Does Not Exists" };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //public async Task<dynamic> GetGeneralInstitutionDetails()
        //{
        //    var query = (from a in db.Institution
        //                 where a.Hos_Type_Id == 1 &&
        //                 a.delete_flag == false && a.status == 3 && a.Ins_Id != 0
        //                 select new
        //                 {
        //                     Id = a.Hos_Id,
        //                     HospitalName = a.Hos_HospitalName,
        //                     Email = a.Hos_HospitalEmail,
        //                     MobileNumber = a.Hos_HospitalPhoneNo,
        //                     NetWorkName = "Karnataka Telemedicine Network..",
        //                     Address = a.Hos_HospitalAddress

        //                 }).ToListAsync();
        //    return await query;
        //}

        //public async Task<dynamic> GetSpecialityHospitalDetails()
        //{
        //    var query = (from a in db.Hospital
        //                 where a.Hos_Type_Id == 2 &&
        //                 a.delete_flag == false && a.status == 3 && a.Hos_Id != 0
        //                 select new
        //                 {
        //                     Id = a.Hos_Id,
        //                     HospitalName = a.Hos_HospitalName,
        //                     Email = a.Hos_HospitalEmail,
        //                     MobileNumber = a.Hos_HospitalPhoneNo,
        //                     NetWorkName = "Karnataka Telemedicine Network..",
        //                     Address = a.Hos_HospitalAddress

        //                 }).ToListAsync();
        //    return await query;
        //}

        //public async Task<dynamic> GetMultiSpecialityHospitalDetails()
        //{
        //    var query = (from a in db.Hospital
        //                 where a.Hos_Type_Id == 3 &&
        //                 a.delete_flag == false && a.status == 3 && a.Hos_Id != 0
        //                 select new
        //                 {
        //                     Id = a.Hos_Id,
        //                     HospitalName = a.Hos_HospitalName,
        //                     Email = a.Hos_HospitalEmail,
        //                     MobileNumber = a.Hos_HospitalPhoneNo,
        //                     NetWorkName = "Karnataka Telemedicine Network..",
        //                     Address = a.Hos_HospitalAddress

        //                 }).ToListAsync();
        //    return await query;
        //}

        public async Task<List<GetAllInstitutionIds>> GetAllInstitutionIds()
        {
            try
            {
                var query = (from a in db.Institution
                             join b in db.States on a.Ins_ST_Id_FK equals b.stat_id into blist
                             from b in blist.DefaultIfEmpty() 
                             join j in db.Status on a.status equals j.sts_id
                             join l in db.Users on a.Ins_UserID equals l.Id
                             //where roleaction == "Hospital" ? a.Hos_Id == Hos_Id : a.Hos_Id > 0
                             orderby a.Ins_Id descending
                             select new GetAllInstitutionIds
                             {
                                 Ins_Id = a.Ins_Id,
                                 Ins_InstitutionCode = a.Ins_InstitutionCode,
                                 Ins_InstitutionName = a.Ins_InstitutionName,
                                 Ins_Reg_No=a.Ins_Reg_No
                             });
                return await query.ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
    
}
