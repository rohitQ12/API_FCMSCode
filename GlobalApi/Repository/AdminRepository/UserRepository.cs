using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
using GlobalApi.Models.AdminClaims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Dapper;

namespace GlobalApi.Repository.AdminRepository
{
    public class UserRepository: IUserRepository
    {
        private readonly UserManager<AuthUser> userManager =null!;
        private readonly RoleManager<AspNetRole> roleManager ;
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private FindUserId findUserId;
        private readonly ADO_Configrations ado_Configurations;
        private readonly IConfigurationRoot configurationRoot = null!;
        public UserRepository(UserManager<AuthUser> userManager, RoleManager<AspNetRole> roleManager,
               GlobalContext globalContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = globalContext;
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            this.findUserId = new FindUserId();
            configurationRoot = configurationBuilder.Build();
            ado_Configurations = new ADO_Configrations();
        }

        //user ctrl
        public async Task<List<AuthUser_Details>> GetUser(string? roleaction, string? rolename,int? OfficeId)
        {
            try
            {
                if(roleaction == "All")
                {
                    var resultAll = (from d in db.Users
                                  join e in db.Roles on d.Role_Id_FK equals e.Id
                                  //join f in db.OfficeRoles on d.Id equals f.UserId
                                  where roleaction == "All" && e.Name!= "Patient"
                                  && d.IsEnabled == true && d.PhoneNumberConfirmed == true
                                  orderby d.UserId descending
                                  select new AuthUser_Details
                                  {
                                      Id = d.Id,
                                      UserId = d.UserId,
                                      Role_Id_FK = d.Role_Id_FK,
                                      Rolename = e.Name,
                                      Inactive = d.Inactive,
                                      FirstName = d.FirstName,
                                      LastName = d.LastName,
                                      DOB = d.DOB,
                                      Gender = d.Gender,
                                      IsEnabled = d.IsEnabled,
                                      UserName = d.UserName,
                                      Email = d.Email,
                                      PhoneNumber = d.PhoneNumber
                                  }).ToListAsync();

                    return await resultAll;

                }
                var result = (from d in db.Users
                              join e in db.Roles on d.Role_Id_FK equals e.Id
                              join f in db.OfficeRoles on d.Id equals f.UserId
                              where roleaction == "All" ? f.Id != 0 : f.OfficeId == OfficeId
                              && d.IsEnabled == true && d.PhoneNumberConfirmed == true
                              orderby d.UserId descending
                              select new AuthUser_Details
                              {
                                  Id = d.Id,
                                  UserId=d.UserId,
                                  Role_Id_FK = d.Role_Id_FK,
                                  Rolename = e.Name,
                                  Inactive = d.Inactive,
                                  FirstName = d.FirstName,
                                  LastName = d.LastName,
                                  DOB=d.DOB,
                                  Gender=d.Gender,
                                  IsEnabled = d.IsEnabled,
                                  UserName = d.UserName,
                                  Email = d.Email,
                                  PhoneNumber = d.PhoneNumber
                              }).ToListAsync();

                return await result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<string> UpdateUserProfile(string Id, IFormFile Image,
            string Email, string PhoneNumber, string FirstName, string LastName, string Gender, DateTime? DOB)
        {
            try
            {
                string Username = null;
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == Id);
                var role = await db.Roles.FirstOrDefaultAsync(x => x.Id == user.Role_Id_FK);
                var User_PhoneNumber= await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == PhoneNumber);
                var User_Email = await db.Users.FirstOrDefaultAsync(x => x.Email == Email);
                if (User_PhoneNumber == null || user.PhoneNumber== PhoneNumber)
                {
                    if (User_Email == null || user.Email== Email)
                    {
                        if (Image != null)
                        {
                            if (user.Imagename != null && user.Imagename != "default_user.png")
                            {
                                string filepath = Path.Combine("wwwroot/Images", user.Imagename);
                                System.IO.File.Delete(filepath);
                            }

                        }

                        string image = Image == null ? user.Imagename : ProcessUploadedFile(Image,PhoneNumber);

                        /*
                        string[] EmailSeparators = user.UserName.Split("@");
                        for (int i = 0; i < EmailSeparators.Length; i++)
                        {
                            if (EmailSeparators[i].ToLower() == "gmail.com")
                            {
                                Username = Email;
                            }
                        }
                        */

                        if (user != null)
                        {
                            user.UserName = Username == "gmail.com" ? Email : PhoneNumber;
                            user.FirstName = FirstName;
                            user.LastName = LastName;
                            user.PhoneNumber = PhoneNumber;
                            user.Email = Email;
                            user.Gender = Gender;
                            user.DOB = DOB;
                            //user.Imagename = image;
                            //user.Imagename = PhoneNumber + System.IO.Path.GetExtension(Image.FileName);
                            await db.SaveChangesAsync();
                            return "User Updated successfully";
                        }
                        //if (role.Name == "Doctor")
                        //{
                        //    var DoctorDetails = await this.db.Doctor.FirstOrDefaultAsync(x => x.DO_UserId == user.Id);
                        //    DoctorDetails.DO_FirstName= FirstName;
                        //    DoctorDetails.DO_LastName= LastName;
                        //    DoctorDetails.DO_Email = Email;
                        //    DoctorDetails.DO_MobileNumber = Convert.ToInt64(PhoneNumber);
                        //    DoctorDetails.DO_Gender = Gender;
                        //    DoctorDetails.DO_DOB = DOB;
                        //    //DoctorDetails.DO_Photo = image;
                        //    //DoctorDetails.DO_Photo = PhoneNumber + System.IO.Path.GetExtension(Image.FileName);
                        //}
                        //else if (role.Name == "Medical Assistant")
                        //{
                        //    var MedicalAssistantDetails = await this.db.Assistant.FirstOrDefaultAsync(x => x.Asssi_UserID == user.Id);
                        //    MedicalAssistantDetails.Assi_FirstName = FirstName;
                        //    MedicalAssistantDetails.Assi_LastName = LastName;
                        //    MedicalAssistantDetails.Assi_Email = Email;
                        //    MedicalAssistantDetails.Assi_MobileNumber = Convert.ToInt64(PhoneNumber);
                        //    MedicalAssistantDetails.Assi_Gender = Gender;
                        //    MedicalAssistantDetails.Assi_DOB = DOB;
                        //    //MedicalAssistantDetails.Assi_Photo = image;
                        //    //MedicalAssistantDetails.Assi_Photo = PhoneNumber + System.IO.Path.GetExtension(Image.FileName);
                        //}
                    }
                    return "User Email Already Exists";
                }
                return "User PhoneNumber Already Exists";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private string ProcessUploadedFile(IFormFile image, string phonenumber)
        {
            string uniqueFileName=null;
            string file_ext = System.IO.Path.GetExtension(image.FileName);

            if (image != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/Images");
                //uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                uniqueFileName = phonenumber + file_ext;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        public async Task<Profile> InsertUserProfile(string Email,string Firstname,string Lastname,string PhoneNumber)
        {
            try
            {
                var id = await primarykeyvalue.primary_key("UserProfile");
                Profile obj = new Profile()
                {
                    Id = id,
                    EmailID = Email,
                    UserName = Firstname+""+ Lastname,
                    Firstname= Firstname,
                    Lastname= Lastname,
                    Phonenumber = PhoneNumber,
                    Image = "default_user.png"

                };
                var result = await db.Profiles.AddAsync(obj);
                await db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AuthUser_Details> GetUserByname(string username)
        {
            try 
            {

                var profile = await db.Users.FirstOrDefaultAsync(b => b.Email == username || b.PhoneNumber == username);
                var role = await db.Roles.FirstOrDefaultAsync(c => c.Id == profile.Role_Id_FK);
                AuthUser_Details obj = new AuthUser_Details();
                obj.Id = profile.Id;
                obj.UserName = profile.UserName;
                obj.FirstName = profile.FirstName;
                obj.LastName = profile.LastName;
                obj.Email = profile.Email;
                obj.Gender = profile.Gender;
                obj.PhoneNumber = profile.PhoneNumber;
                obj.DOB = profile.DOB;
                obj.Rolename = role.Name == null ? "" : role.Name;
                obj.Role_Id_FK = profile.Role_Id_FK;
                obj.Imagebyte = File.Exists("wwwroot/Images/" + profile.Imagename) == true ? System.IO.File.ReadAllBytes(("wwwroot/Images/" + profile.Imagename)) :
                    System.IO.File.ReadAllBytes(("wwwroot/Images/" + "default_user.png"));
                obj.ProfilePicture = profile.Imagename;

                obj.Imagename = profile.Imagename;
                return obj;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<AuthUser_Details_New> GetUserByname_New(string username)
        {
            try
            {

                var profile = await db.Users.FirstOrDefaultAsync(b => b.Email == username || b.PhoneNumber == username);
                var role = await db.Roles.FirstOrDefaultAsync(c => c.Id == profile.Role_Id_FK);
                AuthUser_Details_New obj = new AuthUser_Details_New();
                obj.id = profile.Id;
                obj.FirstName = profile.FirstName;
                obj.LastName = profile.LastName;
                obj.Gender = profile.Gender;
                obj.DOB = profile.DOB;
                obj.email = profile.Email;
                obj.emailConfirmed = profile.EmailConfirmed;
                obj.phoneNumber = profile.PhoneNumber;
                obj.phoneNumberConfirmed = profile.PhoneNumberConfirmed;
                obj.Role_Id_FK = profile.Role_Id_FK;
                obj.rolename = role.Name == null ? "" : role.Name.ToUpper();
                obj.userName = profile.UserName;
                obj.Imagename = profile.Imagename;
                //saheb check with dynamic url
                if (profile.Imagename == null || profile.Imagename == "")
                {
                    //obj.Image_Url = "https://telemedicinephcapi.esdinfra.com/wwwroot/Images/default_user.png";
                    obj.Image_Url = "https://telemedicinephcapi.esdinfra.com/wwwroot/Images/default_user.png";
                }
                else
                {

                    //obj.Image_Url = "https://telemedicinephcapi.esdinfra.com/wwwroot/Images/" + profile.Imagename;
                    obj.Image_Url = "https://telemedicinephcapi.esdinfra.com/wwwroot/Images/" + profile.Imagename;
                    
                }

                return obj;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<dynamic> GetPatientProfile(string username)
        {
            try
            {

                using (IDbConnection con = ado_Configurations.connection())
                {
                    var param = new DynamicParameters();
                    param.Add("@UserName", username);
                    var PatientProfile = await con.QueryAsync("Patient_Profile", param, null, null, CommandType.StoredProcedure);
                    return PatientProfile.FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        //public async Task<List<getpatientprofile>> GetPatientProfile(string username)
        //{
        //    using (SqlConnection sql = ado_Configurations.connection())
        //    {
        //        using (SqlCommand cmd = new SqlCommand("Patient_Profile", sql))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@UserName", username);
        //            var response = new List<getpatientprofile>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapTopatientprofile(reader));
        //                }
        //            }
        //            return response;
        //        }
        //    }
        //}
        //public getpatientprofile MapTopatientprofile(SqlDataReader reader)
        //{
        //    return new getpatientprofile()
        //    {

        //        //Photo = configurationRoot.GetSection("AppUrl").Value + "Images/" + (Convert.ToString(reader["DO_Photo"])),
        //        Id = Convert.ToInt32(reader["Id"]),
        //        UserId = Convert.ToString(reader["UserId"]),
        //        RoleID = Convert.ToString(reader["RoleID"]),
        //        Firstname = Convert.ToString(reader["Firstname"]),
        //        Lastname = Convert.ToString(reader["Lastname"]),
        //        Email = Convert.ToString(reader["Email"]),
        //        Mobilenumber = Convert.ToString(reader["Mobilenumber"]),
        //        Gender = Convert.ToString(reader["Gender"]),
        //        DOB = Convert.ToDateTime(reader["DOB"]),
        //        Age = Convert.ToString(reader["Age"]),
        //        Bloodgroup = Convert.ToString(reader["Bloodgroup"]),
        //        Photo = configurationRoot.GetSection("AppUrl").Value + "Images/" + (Convert.ToString(reader["Photo"])),
        //        Country_Id_FK = Convert.ToInt32(reader["Country_Id_FK"]),
        //        State_Id_FK = Convert.ToInt32(reader["State_Id_FK"]),
        //        District_Id_FK = Convert.ToInt32(reader["District_Id_FK"]),
        //        Taluk_Id_FK = Convert.ToInt32(reader["Taluk_Id_FK"]),
        //        Gram_Id_FK = Convert.ToInt32(reader["Gram_Id_FK"]),
        //        Country_name = Convert.ToString(reader["Country_name"]),
        //        State_name = Convert.ToString(reader["State_name"]),
        //        District_name = Convert.ToString(reader["District_name"]),
        //        Taluk_name = Convert.ToString(reader["Taluk_name"]),
        //        Gram_name = Convert.ToString(reader["Gram_name"]),
        //        Postalcode = Convert.ToInt32(reader["Postalcode"])
        //    };
        //}
        public async Task<dynamic> GetDoctorProfile(string username)
        {
            try
            {
                using (IDbConnection con = ado_Configurations.connection())
                {
                    var param = new DynamicParameters();
                    param.Add("@UserName", username);
                    var DoctorProfile = await con.QueryAsync("Doctor_Profile", param, null, null, CommandType.StoredProcedure);
                    return DoctorProfile.FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        //public async Task<getdoctorprofile_online> GetDoctorProfile_Online(string username)
        //{
        //    try
        //    {
        //        var query = (from a in db.Users
        //                     join b in db.Doctor_Online on a.Id equals b.do_user_id
        //                     join c in db.Countries on b.do_country_id_fk equals c.cntry_id into clist
        //                     from c in clist.DefaultIfEmpty()
        //                     join d in db.States on b.do_st_id_fk equals d.stat_id into dlist
        //                     from d in dlist.DefaultIfEmpty()
        //                     join e in db.Districts on b.do_di_id_fk equals e.district_id into elist
        //                     from e in elist.DefaultIfEmpty()
        //                     join f in db.Qualification_Online on b.do_qu_id_fk equals f.qualification_id into flist
        //                     from f in flist.DefaultIfEmpty()
        //                     join g in db.Designation_Online on b.do_de_id_fk equals g.designation_id into glist
        //                     from g in glist.DefaultIfEmpty()
        //                     join h in db.Discipline_Online on b.do_di_id_fk equals h.cd_id into hlist
        //                     from h in hlist.DefaultIfEmpty()
        //                     join i in db.Specialization_Online on b.do_sp_id_fk equals i.sp_id into ilist
        //                     from i in ilist.DefaultIfEmpty()
        //                     join j in db.Hospital_Online on b.do_ho_id_fk equals j.hos_id into jlist
        //                     from j in jlist.DefaultIfEmpty()
        //                     join k in db.Language_MST_Online on b.mother_tongue_fk equals k.language_id into klist
        //                     from k in klist.DefaultIfEmpty()
        //                     where a.UserName==username
        //                     select new getdoctorprofile_online
        //                     {
        //                         do_id = b.do_id,
        //                         do_user_id = b.do_user_id,
        //                         Role_Id_FK = a.Role_Id_FK,
        //                         do_first_name = b.do_first_name,
        //                         do_last_name = b.do_last_name,
        //                         do_email = b.do_email,
        //                         do_mobile_no = b.do_mobile_no,
        //                         do_gender = b.do_gender,
        //                         do_dob = b.do_dob,
        //                         do_photo = configurationRoot.GetSection("AppUrl").Value + "Images/" + b.do_photo, //check condition if else
        //                         do_reg_no = b.do_reg_no,
        //                         do_country_id_fk = b.do_country_id_fk,
        //                         do_st_id_fk = b.do_st_id_fk,
        //                         do_di_id_fk = b.do_di_id_fk,
        //                         country_name = c.country_name,
        //                         state_name = d.state_name,
        //                         district_name = e.district_name,
        //                         do_postal_code = b.do_postal_code,
        //                         do_qu_id_fk = b.do_qu_id_fk,
        //                         do_de_id_fk = b.do_de_id_fk,
        //                         do_cd_id_fk = b.do_cd_id_fk,
        //                         do_sp_id_fk = b.do_sp_id_fk,
        //                         qualification_name = f.qualification_name,
        //                         designation_desc = g.designation_desc,
        //                         cd_clinicaldiscipline = h.cd_clinicaldiscipline,
        //                         sp_specialization = i.sp_specialization,
        //                         clinic_name = b.clinic_name,
        //                         do_type = b.do_type,
        //                         others_hospital_name = b.others_hospital_name,
        //                         do_ho_id_fk = b.do_ho_id_fk,
        //                         hos_name = j.hos_name,
        //                         do_exp_document = configurationRoot.GetSection("AppUrl").Value + "DoctorDocuments/" + b.do_exp_document,
        //                         do_qualification_document = configurationRoot.GetSection("AppUrl").Value + "DoctorDocuments/" + b.do_qualification_document,
        //                         do_exp_yr = b.do_exp_yr,
        //                         consulation_fee = b.consulation_fee,
        //                         do_alernative_no = b.do_alernative_no,
        //                         do_address = b.do_address,
        //                         mother_tongue_fk = b.mother_tongue_fk,
        //                         language_name = k.language_name,
        //                         other_language_known_fk = b.other_language_known_fk,
        //                         pan_no = b.pan_no,
        //                         gst_no = b.gst_no,
        //                         remarks = b.remarks,
        //                         delete_flag = b.delete_flag,
        //                         record_status = b.record_status,
        //                         Getallknownlang_doctor = (from l in db.Doctor_Language_Online
        //                                                   join m in db.Language_MST_Online on l.language_id_fk equals m.language_id
        //                                                   where l.do_id_fk == b.do_id
        //                                                   select new Getallknownlang_doctor()
        //                                                   {
        //                                                       do_lan_id = l.do_lan_id,
        //                                                       do_id_fk = l.do_id_fk,
        //                                                       language_id_fk = l.language_id_fk,
        //                                                       language_name = m.language_name
        //                                                   }).ToList(),
        //                     }).FirstOrDefaultAsync();
        //        return await query;

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //}
        //public async Task<List<getdoctorprofile>> GetDoctorProfile(string username)
        //{
        //    using (SqlConnection sql = ado_Configurations.connection())
        //    {
        //        using (SqlCommand cmd = new SqlCommand("Doctor_Profile", sql))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@UserName", username);
        //            var response = new List<getdoctorprofile>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapTodoctorprofile(reader));
        //                }
        //            }
        //            return response;
        //        }
        //    }
        //}
        //public getdoctorprofile MapTodoctorprofile(SqlDataReader reader)
        //{
        //    return new getdoctorprofile()
        //    {

        //        Photo = configurationRoot.GetSection("AppUrl").Value + "Images/" + (Convert.ToString(reader["DO_Photo"])),
        //        Id = Convert.ToInt32(reader["Id"]),
        //        UserId = Convert.ToString(reader["UserId"]),
        //        RoleID = Convert.ToString(reader["RoleID"]),
        //        Firstname = Convert.ToString(reader["Firstname"]),
        //        Lastname = Convert.ToString(reader["Lastname"]),
        //        Email = Convert.ToString(reader["Email"]),
        //        Mobilenumber = Convert.ToString(reader["Mobilenumber"]),
        //        Gender = Convert.ToString(reader["Gender"]),
        //        DOB = Convert.ToDateTime(reader["DOB"]),
        //        Age = Convert.ToString(reader["Age"]),
        //        Photo = configurationRoot.GetSection("AppUrl").Value + "Images/" + (Convert.ToString(reader["Photo"])),
        //        DO_RegNo = Convert.ToString(reader["DO_RegNo"]),
        //        Country_Id_FK = Convert.ToInt32(reader["Country_Id_FK"]),
        //        State_Id_FK = Convert.ToInt32(reader["State_Id_FK"]),
        //        District_Id_FK = Convert.ToInt32(reader["District_Id_FK"]),
        //        Taluk_Id_FK = Convert.ToInt32(reader["Taluk_Id_FK"]),
        //        Gram_Id_FK = Convert.ToInt32(reader["Gram_Id_FK"]),
        //        Regno = Convert.ToString(reader["Regno"]),
        //        Country_name = Convert.ToString(reader["Country_name"]),
        //        State_name = Convert.ToString(reader["State_name"]),
        //        District_name = Convert.ToString(reader["District_name"]),
        //        Taluk_name = Convert.ToString(reader["Taluk_name"]),
        //        Gram_name = Convert.ToString(reader["Gram_name"]),
        //        Postalcode = Convert.ToInt32(reader["Postalcode"]),
        //        Qualification_id = Convert.ToInt32(reader["Designation_id"]),
        //        Designation_id = Convert.ToInt32(reader["Designation_id"]),
        //        Discipline_id = Convert.ToInt32(reader["Discipline_id"]),
        //        Specialization_id = Convert.ToInt32(reader["Specialization_id"]),
        //        Qualification_name = Convert.ToString(reader["Qualification_name"]),
        //        Designation_name = Convert.ToString(reader["Designation_name"]),
        //        Discipline_name = Convert.ToString(reader["Discipline_name"]),
        //        Specialization_name = Convert.ToString(reader["Specialization_name"]),
        //        ClinicName = Convert.ToString(reader["ClinicName"]),
        //        DO_Type = Convert.ToString(reader["DO_Type"]),
        //    };
        //}

        //Online Portals
        public async Task<dynamic> GetPatientProfile_Online(string username)
        {
            try
            {

                using (IDbConnection con = ado_Configurations.connection())
                {
                    var param = new DynamicParameters();
                    param.Add("@UserName", username);
                    var PatientProfile = await con.QueryAsync("Get_Patient_Profile_Online", param, null, null, CommandType.StoredProcedure);
                    return PatientProfile.FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
