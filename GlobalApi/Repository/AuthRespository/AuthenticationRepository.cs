using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.JsonFile;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
using GlobalApi.Repository.AdminRepository;
using GlobalApi.Repository.MasterRepository;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Razorpay.Api;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Web;
using static Slapper.AutoMapper;
using CustomerModel = GlobalApi.Models.Master.UserCustomer;

namespace GlobalApi.Repository.AuthRepository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AuthUser> userManager;
        private readonly RoleManager<AspNetRole> roleManager;
        private readonly GlobalContext auth = null!;
        private readonly FindUserId obj_FindUserId;
        private readonly IConfiguration _configuration;
        //private readonly GlobalContext db;
        private IEMailService _EMailService;
        private readonly IConfigurationSection _goolgeSettings;
        private readonly FacebookAuthSetting _facebookAuthSetting;
        private readonly IHttpClientFactory _httpClientfactory;
        //private readonly OfficesRepository officesRepository;
        UserRepository userRepository;
        private SignInManager<AuthUser> signInManager;
        private const string TokenvalidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string UserInfo = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";
        string url = "";
        ISMSService objSMSService;
        private readonly IPrimarykeyvalue primarykeyvalue;
        private readonly FileUpload fileUpload;
        private readonly IConfigurationRoot configurationRoot = null!;

        public AuthenticationRepository(GlobalContext auth,
            IHttpClientFactory httpClientfactory, UserManager<AuthUser> userManager,
            RoleManager<AspNetRole> roleManager, IConfiguration configuration,
            IEMailService EMailService, FacebookAuthSetting facebookAuthSetting,
            FindUserId obj_FindUserId, UserRepository userRepository, SignInManager<AuthUser> signInManager, ISMSService objSMSService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._configuration = configuration;
            this._EMailService = EMailService;
            this._goolgeSettings = _configuration.GetSection("GoogleAuthSettings");
            this._facebookAuthSetting = facebookAuthSetting;
            this._httpClientfactory = httpClientfactory;
            this.auth = auth;
            this.obj_FindUserId = obj_FindUserId;
            this.userRepository = userRepository;
            this.signInManager = signInManager;
            // this.officesRepository = new OfficesRepository();
            this.objSMSService = objSMSService;
            primarykeyvalue = new Primarykeyvalue();
            fileUpload = new FileUpload();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
            //db = new GlobalContext();

        }
        public async Task<dynamic> RegisterUserAsync(RegisterModel model)
        {

            var objUser = await userManager.FindByIdAsync(model.UserId);

            if (objUser != null)
            {
                if (objUser.IsEnabled == false && objUser.PhoneNumberConfirmed == false)
                {
                    UserStore<AuthUser> store = new UserStore<AuthUser>(auth);
                    String hashedNewPassword = userManager.PasswordHasher.HashPassword(objUser, model.Password);
                    AuthUser cUser = await store.FindByIdAsync(objUser.Id);
                    cUser.IsEnabled = true;
                    cUser.Role_Id_FK = model.RoleId;
                    cUser.PhoneNumberConfirmed = true;
                    cUser.UserId = userManager.Users.Max(x => x.UserId) + 1;
                    await store.SetPasswordHashAsync(cUser, hashedNewPassword);
                    await store.UpdateAsync(cUser);
                    //var officedetails = await this.officesRepository.AddOfficeRoles(objUser.Id, model.OfficeId);
                    return new
                    {
                        Message = "User Update successfully!",
                        IsSuccess = true,
                        userid = objUser.Id
                    };

                }
                else
                {
                    return new
                    {
                        Message = "User Already Registed!",
                        IsSuccess = false,
                        userid = ""
                    };
                }
            }
            else
            {

                var userExist = await auth.Users.FirstOrDefaultAsync(x => x.UserName == model.Email || x.UserName == model.Phonenumber || x.Email == model.Email || x.PhoneNumber == model.Phonenumber);
                if (userExist != null)
                {
                    if (userExist.PhoneNumber == model.Phonenumber)
                    {
                        return new
                        {
                            Message = "MobileNumber Already Exists",
                            IsSuccess = false,
                        };
                    }
                    else if (userExist.Email == model.Email)
                    {
                        return new
                        {
                            Message = "Email Already Exists",
                            IsSuccess = false,
                        };
                    }
                }
                var Imagename = "default_user.png";
                AuthUser user = new AuthUser()
                {
                    UserName = model.Phonenumber,
                    UserId = userManager.Users.Max(x => x.UserId) + 1,
                    FirstName = model.Firstname,
                    LastName = model.Lastname,
                    PhoneNumber = model.Phonenumber,
                    Role_Id_FK = model.RoleId,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    IsEnabled = true,
                    Inactive = "N",
                    Imagename = Imagename,
                    Id = Guid.NewGuid().ToString(),
                    PhoneNumberConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, model.Password);
                string userid = user.Id;
                if (result.Succeeded)
                {
                    //var confrmEmailtoken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var encodedEmailToken = Encoding.UTF8.GetBytes(confrmEmailtoken);
                    //var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                    //string url = $"{_configuration["AppUrl"]}api/Authentication/ConfirmEmail?userId={user.Id}&token={validEmailToken}";
                    //await _EMailService.SendEmailAsync(user.UserName, user.Email, "Confirm your email", $"<h1>Welcome to Medetel Health</h1>" +
                    //    $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
                    // var officedetails = await this.officesRepository.AddOfficeRoles(userid, model.OfficeId);

                    return new
                    {
                        Message = "User created successfully!",
                        IsSuccess = true,
                        userid = userid
                    };
                }

                return new
                {
                    Message = "User did not create",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

        }


        public async Task<dynamic> ExtRegisterUserAsync(string Firstname, string Lastname,
            string Phonenumber, string Email, string Password, string Role_Id, int Id, string OTP)
        {
            try
            {
                var objUserVerfiy = await this.auth.UserVerfication.FirstOrDefaultAsync(x => x.Id == Id && ((DateTime.Now.Second - x.OTPCreatedDate.Value.Second) + ((DateTime.Now.Minute - x.OTPCreatedDate.Value.Minute) * 60) <= ((x.ExpiryTime * 60) - 10) && x.OTP == OTP));
                if (objUserVerfiy == null)
                {
                    return new
                    {
                        Message = "Invalid OTP Expired",
                        IsSuccess = false,

                    };
                }

                AuthUser user = new AuthUser()
                {
                    UserName = Phonenumber,
                    UserId = userManager.Users.Max(x => x.UserId) + 1,
                    FirstName = Firstname,
                    LastName = Lastname,
                    PhoneNumber = Phonenumber,
                    Imagename = "user-1633249__340 (1).png",
                    Role_Id_FK = Role_Id,
                    Email = Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    IsEnabled = true,
                    Inactive = "N",
                    OTP = "",
                    PhoneNumberConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, Password);
                string userid = user.Id;
                if (result.Succeeded)
                {
                    string RequestBody = "{"
                                             + "\"Header\": \"" + "RgMDTL" + "\","
                                             + "\"Target\": \"" + Phonenumber + "\","
                                             + "\"Is_Unicode\": \"" + "0" + "\","
                                             + "\"Is_Flash\": \"" + "0" + "\","
                                             + "\"Message_Type\": \"" + _configuration["SMSSettings:Message_Type"] + "\","
                                             + "\"Entity_Id\": \"" + _configuration["SMSSettings:Entity_Id"] + "\","
                                             + "\"Content_Template_Id\": \"" + "1407167937909688733" + "\","
                                             + "\"Consent_Template_Id\": \"" + "" + "\","
                                             + "\"Template_Keys_and_Values\": " + "[{"
                                                   + "\"Key\": \"" + "" + "\","
                                                   + "\"Value\": \"" + "" + "\""
                                             + "}]"
                                             + "}";
                    bool SendMessage = this.objSMSService.SendMessage(RequestBody);
                    return new
                    {
                        Message = "User created successfully!",
                        IsSuccess = true,
                        userid = userid
                    };
                }

                return new
                {
                    Message = "User did not create",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<dynamic> PatientRegister(PatientReg patientReg)
        //{
        //    try
        //    {
        //        using (GlobalContext globalContext = new GlobalContext())
        //        {
        //            var objUserVerfiy = await this.auth.UserVerfication.FirstOrDefaultAsync(x => x.Id == patientReg.Id && ((DateTime.Now.Second - x.OTPCreatedDate.Value.Second) + ((DateTime.Now.Minute - x.OTPCreatedDate.Value.Minute) * 60) <= ((x.ExpiryTime * 60) - 10)) && x.OTP == patientReg.OTP);
        //            if (objUserVerfiy == null)
        //            {
        //                return new
        //                {
        //                    Message = "Invalid OTP Expired",
        //                    IsSuccess = false,
        //                };
        //            }

        //            int id = await primarykeyvalue.primary_key("Patient");
        //            string Filename = "default_user.png";

        //            AuthUser objAuthUser = new AuthUser()
        //            {
        //                UserName = Convert.ToString(patientReg.PR_MobileNumber),
        //                UserId = globalContext.Users.Max(x => x.UserId) + 1,
        //                FirstName = patientReg.PR_FirstName,
        //                LastName = patientReg.PR_LastName,
        //                PhoneNumber = Convert.ToString(patientReg.PR_MobileNumber),
        //                Role_Id_FK = patientReg.Role_ID,
        //                Email = patientReg.PR_Email,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                IsEnabled = true,
        //                Inactive = "N",
        //                Imagename = Filename,
        //                Id = Guid.NewGuid().ToString(),
        //                PhoneNumberConfirmed = true,
        //            };
        //            var result = await userManager.CreateAsync(objAuthUser, patientReg.Password);

        //            Patient obj = new Patient()
        //            {
        //                PR_Id = id,
        //                PR_UserId = objAuthUser.Id,
        //                PR_RemoteHospitalName_Id_FK = patientReg.PR_RemoteHospitalName_Id_FK,
        //                PR_RegNo = "",
        //                PR_PatientCode = "P-" + Convert.ToString(id),
        //                PR_FirstName = patientReg.PR_FirstName,
        //                PR_LastName = patientReg.PR_LastName,
        //                PR_Gender = patientReg.PR_Gender,
        //                PR_DOB = patientReg.PR_DOB,
        //                PR_Age = patientReg.PR_Age,
        //                PR_LandlineNo = patientReg.PR_LandlineNo,
        //                PR_Alternative_No = patientReg.PR_Alternative_No,
        //                PR_MaritalStatus = patientReg.PR_MaritalStatus,
        //                PR_FatherName = patientReg.PR_FatherName,
        //                PR_BloodGroup = patientReg.PR_BloodGroup,
        //                PR_MotherTongue = patientReg.PR_MotherTongue,
        //                PR_REG_Id_FK = patientReg.PR_REG_Id_FK,
        //                PR_NAL_Id_FK = patientReg.PR_NAL_Id_FK,
        //                PR_CAT_Id_FK = patientReg.PR_CAT_Id_FK,
        //                PR_IDN_Id_FK = patientReg.PR_IDN_Id_FK,
        //                PR_Identity_No = patientReg.PR_Identity_No,
        //                National_Health_Id = patientReg.National_Health_Id,
        //                PR_OCU_Id_FK = patientReg.PR_OCU_Id_FK,
        //                PR_Income = patientReg.PR_Income,
        //                PR_Insurance = patientReg.PR_Insurance,
        //                PR_INU_Id_FK = patientReg.PR_INU_Id_FK,
        //                PR_Insured_Sum = patientReg.PR_Insured_Sum,
        //                PR_Address = patientReg.PR_Address,
        //                PR_Country_Id_FK = patientReg.PR_Country_Id_FK != null ? patientReg.PR_Country_Id_FK : 0,
        //                PR_S_Id_FK = patientReg.PR_S_Id_FK != null ? patientReg.PR_S_Id_FK : 0,
        //                PR_D_Id_FK = patientReg.PR_D_Id_FK != null ? patientReg.PR_D_Id_FK : 0,
        //                PR_Taluk_Id = patientReg.PR_Taluk_Id,
        //                PR_Gram_Id = patientReg.PR_Gram_Id,
        //                PR_Village = "",
        //                PR_Postalcode = patientReg.PR_Postalcode,
        //                PR_MobileNumber = patientReg.PR_MobileNumber != null ? patientReg.PR_MobileNumber : "0",
        //                PR_Email = patientReg.PR_Email != null ? patientReg.PR_Email : "",
        //                PR_PassportNo = patientReg.PR_PassportNo,
        //                PR_RegistrationDateTime = DateTime.Now,
        //                PR_Photo = Filename,
        //                PR_UserId_FK = patientReg.PR_UserId_FK,
        //                created_by = "",
        //                created_date = DateTime.Now,
        //                delete_flag = false,
        //                status = 1
        //            };
        //            var PatientResult = await globalContext.Patient.AddAsync(obj);
        //            await globalContext.SaveChangesAsync();

        //            if (result.Succeeded)
        //            {
        //                string RequestBody = "{"
        //                                  + "\"Header\": \"" + "RgMDTL" + "\","
        //                                  + "\"Target\": \"" + patientReg.PR_MobileNumber + "\","
        //                                  + "\"Is_Unicode\": \"" + "0" + "\","
        //                                  + "\"Is_Flash\": \"" + "0" + "\","
        //                                  + "\"Message_Type\": \"" + _configuration["SMSSettings:Message_Type"] + "\","
        //                                  + "\"Entity_Id\": \"" + _configuration["SMSSettings:Entity_Id"] + "\","
        //                                  + "\"Content_Template_Id\": \"" + "1407167937909688733" + "\","
        //                                  + "\"Consent_Template_Id\": \"" + "" + "\","
        //                                  + "\"Template_Keys_and_Values\": " + "[{"
        //                                        + "\"Key\": \"" + "" + "\","
        //                                        + "\"Value\": \"" + "" + "\""
        //                                  + "}]"
        //                                  + "}";
        //                bool SendMessage = this.objSMSService.SendMessage(RequestBody);
        //                return new
        //                {
        //                    Message = "User created successfully!",
        //                    IsSuccess = true,
        //                    userid = objAuthUser.Id
        //                };
        //            }

        //            return new
        //            {
        //                Message = "User did not create",
        //                IsSuccess = false,
        //                Errors = result.Errors.Select(e => e.Description)
        //            };

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //Online Portals
        //public async Task<dynamic> PatientRegister_Online(PatientReg_Online patientReg)
        //{
        //    try
        //    {
        //        using (GlobalContext globalContext = new GlobalContext())
        //        {
        //            /*
        //            var objUserVerfiy = await this.auth.UserVerfication.FirstOrDefaultAsync(x => x.Id == patientReg.Id && ((DateTime.Now.Second - x.OTPCreatedDate.Value.Second) + ((DateTime.Now.Minute - x.OTPCreatedDate.Value.Minute) * 60) <= ((x.ExpiryTime * 60) - 10)) && x.OTP == patientReg.OTP);
        //            if (objUserVerfiy == null)
        //            {
        //                return new
        //                {
        //                    Message = "Invalid OTP Expired",
        //                    IsSuccess = false,
        //                };
        //            }
        //            */
        //            var objTokenVerfiy = await this.auth.UserVerfication_Online.FirstOrDefaultAsync(x => x.verify_id == patientReg.verify_id && x.otp == patientReg.otp);

        //            DateTime otpExpirationDate = Convert.ToDateTime(objTokenVerfiy.created_date);
        //            DateTime otpExpirationTime = otpExpirationDate.AddMinutes(Convert.ToInt32(objTokenVerfiy.expiry_time));                   
        //            DateTime currentDateTime = DateTime.Now;                   
        //            TimeSpan timeDifference = currentDateTime - otpExpirationDate;                   
        //            if (otpExpirationTime.Minute < timeDifference.Minutes)
        //            {
        //                return new UserCustomResponse
        //                {
        //                    message = "Invalid OTP expired.",
        //                    is_success = false
        //                };
        //            }

        //            int id = await primarykeyvalue.primary_key("Patient_Online");
        //            string Filename = "default_user.png";

        //            AuthUser objAuthUser = new AuthUser()
        //            {
        //                UserName = Convert.ToString(patientReg.pr_mobile_no),
        //                UserId = globalContext.Users.Max(x => x.UserId) + 1,
        //                FirstName = patientReg.pr_first_name,
        //                LastName = patientReg.pr_last_name,
        //                PhoneNumber = Convert.ToString(patientReg.pr_mobile_no),
        //                Role_Id_FK = patientReg.Role_ID,
        //                Email = patientReg.pr_email,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                IsEnabled = true,
        //                Inactive = "N",
        //                Imagename = Filename,
        //                Id = Guid.NewGuid().ToString(),
        //                PhoneNumberConfirmed = true,
        //            };
        //            var result = await userManager.CreateAsync(objAuthUser, patientReg.Password);

        //            Patient_Online obj = new Patient_Online()
        //            {
        //                pr_id = id,
        //                pr_user_id = objAuthUser.Id,
        //                pr_code = "P-" + Convert.ToString(id),
        //                pr_first_name = patientReg.pr_first_name,
        //                pr_last_name = patientReg.pr_last_name,
        //                pr_gender = patientReg.pr_gender,
        //                pr_dob = patientReg.pr_dob,
        //                pr_age = patientReg.pr_age,
        //                pr_mobile_no = patientReg.pr_mobile_no,
        //                pr_alternative_no = patientReg.pr_alternative_no,
        //                pr_marital_status = patientReg.pr_marital_status,
        //                pr_blood_group = patientReg.pr_blood_group,
        //                pr_mother_tongue_fk = patientReg.pr_mother_tongue_fk,
        //                pr_have_insurance = patientReg.pr_have_insurance,
        //                pr_insurance_no = patientReg.pr_insurance_no,
        //                pr_nal_id_fk = patientReg.pr_nal_id_fk,
        //                pr_idn_id_fk = patientReg.pr_idn_id_fk,
        //                pr_identity_no = patientReg.pr_identity_no,
        //                national_health_id = patientReg.national_health_id,
        //                pr_address = patientReg.pr_address,
        //                pr_country_id_fk = patientReg.pr_country_id_fk != null ? patientReg.pr_country_id_fk : 0,
        //                pr_st_id_fk = patientReg.pr_st_id_fk != null ? patientReg.pr_st_id_fk : 0,
        //                pr_di_id_fk = patientReg.pr_di_id_fk != null ? patientReg.pr_di_id_fk : 0,
        //                pr_postal_code = patientReg.pr_postal_code,
        //                pr_email = patientReg.pr_email != null ? patientReg.pr_email : "",
        //                pr_reg_date = DateTime.Now,
        //                pr_photo = Filename,
        //                created_by = objAuthUser.Id,
        //                created_date = DateTime.Now,
        //                delete_flag = false,
        //                record_status = 1,
        //                update_lock_count = 0
        //            };
        //            var PatientResult = await globalContext.Patient_Online.AddAsync(obj);
        //            await globalContext.SaveChangesAsync();

        //            if (result.Succeeded)
        //            {
        //                string RequestBody = "{"
        //                                  + "\"Header\": \"" + "RgMDTL" + "\","
        //                                  + "\"Target\": \"" + patientReg.pr_mobile_no + "\","
        //                                  + "\"Is_Unicode\": \"" + "0" + "\","
        //                                  + "\"Is_Flash\": \"" + "0" + "\","
        //                                  + "\"Message_Type\": \"" + _configuration["SMSSettings:Message_Type"] + "\","
        //                                  + "\"Entity_Id\": \"" + _configuration["SMSSettings:Entity_Id"] + "\","
        //                                  + "\"Content_Template_Id\": \"" + "1407167937909688733" + "\","
        //                                  + "\"Consent_Template_Id\": \"" + "" + "\","
        //                                  + "\"Template_Keys_and_Values\": " + "[{"
        //                                        + "\"Key\": \"" + "" + "\","
        //                                        + "\"Value\": \"" + "" + "\""
        //                                  + "}]"
        //                                  + "}";
        //                bool SendMessage = this.objSMSService.SendMessage(RequestBody);
        //                return new UserCustomResponse
        //                {
        //                    message = "User created successfully.",
        //                    is_success = true,
        //                    user_id = objAuthUser.Id
        //                };
        //            }

        //            return new UserCustomResponse
        //            {
        //                message = "User creation failed.",
        //                is_success = false,
        //                errors = result.Errors.Select(e => e.Description)
        //            };

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public async Task<dynamic> DoctorRegister(DoctorReg doctorReg)
        //{
        //    try
        //    {
        //        using (GlobalContext globalContext = new GlobalContext())
        //        {
        //            var objUserVerfiy = await this.auth.UserVerfication.FirstOrDefaultAsync(x => x.Id == doctorReg.Id && ((DateTime.Now.Second - x.OTPCreatedDate.Value.Second) + ((DateTime.Now.Minute - x.OTPCreatedDate.Value.Minute) * 60) <= ((x.ExpiryTime * 60) - 10)) && x.OTP == doctorReg.OTP);
        //            if (objUserVerfiy == null)
        //            {
        //                return new
        //                {
        //                    Message = "Invalid OTP Expired",
        //                    IsSuccess = false,
        //                };
        //            }

        //            int id = await primarykeyvalue.primary_key("Doctor");
        //            string Filename = "default_user.png";

        //            AuthUser objAuthUser = new AuthUser()
        //            {
        //                UserName = Convert.ToString(doctorReg.DO_MobileNumber),
        //                UserId = globalContext.Users.Max(x => x.UserId) + 1,
        //                FirstName = doctorReg.DO_FirstName,
        //                LastName = doctorReg.DO_LastName,
        //                PhoneNumber = Convert.ToString(doctorReg.DO_MobileNumber),
        //                Role_Id_FK = doctorReg.Role_ID,
        //                Email = doctorReg.DO_Email,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                IsEnabled = true,
        //                Inactive = "N",
        //                Imagename = Filename,
        //                Id = Guid.NewGuid().ToString(),
        //                PhoneNumberConfirmed = true,
        //            };
        //            var result = await userManager.CreateAsync(objAuthUser, doctorReg.Password);

        //            Doctor obj = new Doctor()
        //            {
        //                DO_Id = id,
        //                DO_UserId = objAuthUser.Id,
        //                DO_RegNo = "",
        //                DO_Code = "D-" + doctorReg.DO_Code,
        //                DO_FirstName = doctorReg.DO_FirstName,
        //                DO_LastName = doctorReg.DO_LastName,
        //                DO_DOB = doctorReg.DO_DOB,
        //                DO_Gender = doctorReg.DO_Gender,
        //                DO_MotherTongue = doctorReg.DO_MotherTongue,
        //                DO_Address = doctorReg.DO_Address,
        //                DO_Country_Id_FK = doctorReg.DO_Country_Id_FK,
        //                DO_ST_Id_FK = doctorReg.DO_ST_Id_FK,
        //                DO_DI_Id_FK = doctorReg.DO_DI_Id_FK,
        //                DO_Taluk_Id = doctorReg.DO_Taluk_Id,
        //                DO_Gram_Id = doctorReg.DO_Gram_Id,
        //                DO_PostalCode = doctorReg.DO_PostalCode,
        //                DO_MobileNumber = doctorReg.DO_MobileNumber,
        //                DO_OfficialNumber = doctorReg.DO_OfficialNumber,
        //                DO_Email = doctorReg.DO_Email,
        //                DO_HO_Id_FK = doctorReg.DO_HO_Id_FK,
        //                DO_QU_Id_FK = doctorReg.DO_QU_Id_FK,
        //                DO_DE_Id_FK = doctorReg.DO_DE_Id_FK,
        //                DO_CD_Id_FK = doctorReg.DO_CD_Id_FK,
        //                DO_SP_Id_FK = doctorReg.DO_SP_Id_FK,
        //                DO_Photo = Filename,
        //                DO_UserId_FK = doctorReg.DO_UserId_FK,
        //                DO_Village = doctorReg.DO_Village,
        //                DO_Alernative_Numb = doctorReg.DO_Alernative_Numb,
        //                PANno = doctorReg.PANno,
        //                GSTno = doctorReg.GSTno,
        //                Regno = doctorReg.Regno,
        //                DO_Type = doctorReg.DO_Type,
        //                created_by = 1,
        //                created_date = DateTime.Now,
        //                delete_flag = false,
        //                status = 1,
        //                DO_Choose_Document = "",
        //                ClinicName = doctorReg.ClinicName
        //            };
        //            var DoctorResult = await globalContext.Doctor.AddAsync(obj);
        //            await globalContext.SaveChangesAsync();

        //            if (result.Succeeded)
        //            {
        //                string RequestBody = "{"
        //                                  + "\"Header\": \"" + "RgMDTL" + "\","
        //                                  + "\"Target\": \"" + doctorReg.DO_MobileNumber + "\","
        //                                  + "\"Is_Unicode\": \"" + "0" + "\","
        //                                  + "\"Is_Flash\": \"" + "0" + "\","
        //                                  + "\"Message_Type\": \"" + _configuration["SMSSettings:Message_Type"] + "\","
        //                                  + "\"Entity_Id\": \"" + _configuration["SMSSettings:Entity_Id"] + "\","
        //                                  + "\"Content_Template_Id\": \"" + "1407167937909688733" + "\","
        //                                  + "\"Consent_Template_Id\": \"" + "" + "\","
        //                                  + "\"Template_Keys_and_Values\": " + "[{"
        //                                        + "\"Key\": \"" + "" + "\","
        //                                        + "\"Value\": \"" + "" + "\""
        //                                  + "}]"
        //                                  + "}";
        //                bool SendMessage = this.objSMSService.SendMessage(RequestBody);
        //                return new
        //                {
        //                    Message = "User created successfully!",
        //                    IsSuccess = true,
        //                    userid = objAuthUser.Id
        //                };
        //            }

        //            return new
        //            {
        //                Message = "User did not create",
        //                IsSuccess = false,
        //                Errors = result.Errors.Select(e => e.Description)
        //            };

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //sujata
        //public async Task<dynamic> DoctorRegister_Online(DoctorReg_Online doctorReg)
        //{
        //    try
        //    {
        //        using (GlobalContext globalContext = new GlobalContext())
        //        {
        //            /*
        //            var objUserVerfiy = await this.auth.UserVerfication.FirstOrDefaultAsync(x => x.Id == doctorReg.Id && ((DateTime.Now.Second - x.OTPCreatedDate.Value.Second) + ((DateTime.Now.Minute - x.OTPCreatedDate.Value.Minute) * 60) <= ((x.ExpiryTime * 60) - 10)) && x.OTP == doctorReg.OTP);
        //            if (objUserVerfiy == null)
        //            {
        //                return new
        //                {
        //                    Message = "Invalid OTP expired.",
        //                    IsSuccess = false,
        //                };
        //            }
        //            */
        //            var objTokenVerfiy = await this.auth.UserVerfication_Online.FirstOrDefaultAsync(x => x.verify_id == doctorReg.verify_id && x.otp == doctorReg.otp);

        //            // Calculate the expiration time based on the creation time and the defined expiration period.
        //            DateTime otpExpirationDate = Convert.ToDateTime(objTokenVerfiy.created_date);
        //            DateTime otpExpirationTime = otpExpirationDate.AddMinutes(Convert.ToInt32(objTokenVerfiy.expiry_time));
        //            // Get the current date and time.
        //            DateTime currentDateTime = DateTime.Now;

        //            // Calculate the time difference between the current time and OTP creation time.
        //            TimeSpan timeDifference = currentDateTime - otpExpirationDate;

        //            // Check if the current date and time is before the OTP expiration time.
        //            if (otpExpirationTime.Minute < timeDifference.Minutes)
        //            {
        //                return new UserCustomResponse
        //                {
        //                    message = "Invalid OTP expired.",
        //                    is_success = false
        //                };
        //            }

        //            int id = await primarykeyvalue.primary_key("Doctor_Online");
        //            string Filename = "default_user.png";

        //            AuthUser objAuthUser = new AuthUser()
        //            {
        //                UserName = Convert.ToString(doctorReg.do_mobile_no),
        //                UserId = globalContext.Users.Max(x => x.UserId) + 1,
        //                FirstName = doctorReg.do_first_name,
        //                LastName = doctorReg.do_last_name,
        //                PhoneNumber = Convert.ToString(doctorReg.do_mobile_no),
        //                Role_Id_FK = doctorReg.Role_ID,
        //                Email = doctorReg.do_email,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                IsEnabled = true,
        //                Inactive = "N",
        //                Imagename = Filename,
        //                Id = Guid.NewGuid().ToString(),
        //                PhoneNumberConfirmed = true,
        //            };
        //            var result = await userManager.CreateAsync(objAuthUser, doctorReg.Password);

        //            Doctor_Online obj = new Doctor_Online()
        //            {
        //                do_id = id,
        //                do_user_id = objAuthUser.Id,
        //                do_reg_no = doctorReg.do_reg_no,
        //                do_first_name = doctorReg.do_first_name,
        //                do_last_name = doctorReg.do_last_name,
        //                do_dob = doctorReg.do_dob,
        //                do_gender = doctorReg.do_gender,
        //                mother_tongue_fk = doctorReg.mother_tongue_fk,
        //                do_address = doctorReg.do_address,
        //                do_country_id_fk = doctorReg.do_country_id_fk,
        //                do_st_id_fk = doctorReg.do_st_id_fk,
        //                do_di_id_fk = doctorReg.do_di_id_fk,
        //                do_postal_code = doctorReg.do_postal_code,
        //                do_mobile_no = doctorReg.do_mobile_no,
        //                do_alernative_no = doctorReg.do_alernative_no,
        //                do_email = doctorReg.do_email,
        //                do_ho_id_fk = doctorReg.do_ho_id_fk,
        //                others_hospital_name = doctorReg.others_hospital_name,
        //                do_qu_id_fk = doctorReg.do_qu_id_fk,
        //                do_de_id_fk = doctorReg.do_de_id_fk,
        //                do_cd_id_fk = doctorReg.do_cd_id_fk,
        //                do_sp_id_fk = doctorReg.do_sp_id_fk,
        //                do_exp_yr = doctorReg.do_exp_yr,
        //                do_photo = Filename,
        //                pan_no = doctorReg.pan_no,
        //                gst_no = doctorReg.gst_no,
        //                remarks = doctorReg.remarks,
        //                other_language_known_fk = doctorReg.other_language_known_fk,
        //                do_type = doctorReg.do_type,
        //                do_exp_document = "",
        //                do_qualification_document = "",
        //                clinic_name = doctorReg.clinic_name,
        //                consulation_fee = doctorReg.consulation_fee,
        //                created_date = DateTime.Now,
        //                delete_flag = false,
        //                record_status = doctorReg.record_status,
        //                update_lock_count=0
        //            };
        //            var DoctorResult = await globalContext.Doctor_Online.AddAsync(obj);
        //            await globalContext.SaveChangesAsync();

        //            if (result.Succeeded)
        //            {
        //                string RequestBody = "{"
        //                                  + "\"Header\": \"" + "RgMDTL" + "\","
        //                                  + "\"Target\": \"" + doctorReg.do_mobile_no + "\","
        //                                  + "\"Is_Unicode\": \"" + "0" + "\","
        //                                  + "\"Is_Flash\": \"" + "0" + "\","
        //                                  + "\"Message_Type\": \"" + _configuration["SMSSettings:Message_Type"] + "\","
        //                                  + "\"Entity_Id\": \"" + _configuration["SMSSettings:Entity_Id"] + "\","
        //                                  + "\"Content_Template_Id\": \"" + "1407167937909688733" + "\","
        //                                  + "\"Consent_Template_Id\": \"" + "" + "\","
        //                                  + "\"Template_Keys_and_Values\": " + "[{"
        //                                        + "\"Key\": \"" + "" + "\","
        //                                        + "\"Value\": \"" + "" + "\""
        //                                  + "}]"
        //                                  + "}";
        //                bool SendMessage = this.objSMSService.SendMessage(RequestBody);
        //                return new UserCustomResponse
        //                {
        //                    message = "User created successfully.",
        //                    is_success = true,
        //                    user_id = objAuthUser.Id
        //                };
        //            }

        //            return new UserCustomResponse
        //            {
        //                message = "User creation failed.",
        //                is_success = false,
        //                errors = result.Errors.Select(e => e.Description)
        //            };

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public async Task<dynamic> HospitalRegister(HospitalReg hospitalReg)
        //{
        //    try
        //    {
        //        using (GlobalContext globalContext = new GlobalContext())
        //        {
        //            var objUserVerfiy = await this.auth.UserVerfication.FirstOrDefaultAsync(x => x.Id == hospitalReg.Id && ((DateTime.Now.Second - x.OTPCreatedDate.Value.Second) + ((DateTime.Now.Minute - x.OTPCreatedDate.Value.Minute) * 60) <= ((x.ExpiryTime * 60) - 10)) && x.OTP == hospitalReg.OTP);
        //            if (objUserVerfiy == null)
        //            {
        //                return new
        //                {
        //                    Message = "Invalid OTP Expired",
        //                    IsSuccess = false,
        //                };
        //            }

        //            int HospitalId = await primarykeyvalue.primary_key("Hospital");
        //            string Filename = "default_user.png";

        //            AuthUser objAuthUser = new AuthUser()
        //            {
        //                UserName = hospitalReg.Hos_HospitalPhoneNo,
        //                UserId = globalContext.Users.Max(x => x.UserId) + 1,
        //                FirstName = "",
        //                LastName = "",
        //                PhoneNumber = hospitalReg.Hos_HospitalPhoneNo,
        //                Role_Id_FK = hospitalReg.Role_ID,
        //                Email = hospitalReg.Hos_HospitalEmail,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                IsEnabled = true,
        //                Inactive = "N",
        //                Imagename = Filename,
        //                Id = Guid.NewGuid().ToString(),
        //                PhoneNumberConfirmed = true
        //            };
        //            //var result = await userManager.CreateAsync(objAuthUser, hospitalReg.Password);
        //            var result = await auth.Users.AddAsync(objAuthUser);
        //            await globalContext.SaveChangesAsync();

        //            Hospital obj = new Hospital()
        //            {
        //                Hos_Id = HospitalId,
        //                Hos_UserID = objAuthUser.Id,
        //                //Hos_HospitalCode = "HO_" + Convert.ToString(id),
        //                Hos_HospitalCode = hospitalReg.Hos_HospitalCode,
        //                Hos_HospitalName = hospitalReg.Hos_HospitalName,
        //                Hos_Type_Id = hospitalReg.Hos_Type_Id,
        //                Hos_cat_Id = hospitalReg.Hos_cat_Id,
        //                Hos_Branch = hospitalReg.Hos_Branch != null ? hospitalReg.Hos_Branch : 0,
        //                Hos_HospitalEmail = hospitalReg.Hos_HospitalEmail,
        //                Hos_HospitalPhoneNo = hospitalReg.Hos_HospitalPhoneNo,
        //                Hos_HospitalAddress = hospitalReg.Hos_HospitalAddress,
        //                PrimaryorBranch = hospitalReg.PrimaryorBranch,
        //                Hos_Country_Id_FK = hospitalReg.Hos_Country_Id_FK,
        //                Hos_ST_Id_FK = hospitalReg.Hos_ST_Id_FK,
        //                Hos_DI_Id_FK = hospitalReg.Hos_DI_Id_FK,
        //                Hos_Taluk_Id = hospitalReg.Hos_Taluk_Id,
        //                Hos_Gram_Id = hospitalReg.Hos_Gram_Id,
        //                Hos_Village = hospitalReg.Hos_Village,
        //                Hos_PostalCode = hospitalReg.Hos_PostalCode,
        //                Hos_NE_Id_FK = hospitalReg.Hos_NE_Id_FK,
        //                Hos_Alterno = hospitalReg.Hos_Alterno,
        //                Hos_Landline = hospitalReg.Hos_Landline,
        //                Hos_HospitalLogo = Filename,
        //                created_by = 1,
        //                created_date = DateTime.Now,
        //                delete_flag = false,
        //                status = 1,
        //                OwnerName = hospitalReg.OwnerName,
        //                GSTno = hospitalReg.GSTno,
        //                PANno = hospitalReg.PANno,
        //                RegNo = hospitalReg.RegNo
        //            };
        //            var HospitalResult = await globalContext.Hospital.AddAsync(obj);
        //            await globalContext.SaveChangesAsync();

        //            if (result.Entity != null)
        //            {
        //                string RequestBody = "{"
        //                                  + "\"Header\": \"" + "RgMDTL" + "\","
        //                                  + "\"Target\": \"" + hospitalReg.Hos_HospitalPhoneNo + "\","
        //                                  + "\"Is_Unicode\": \"" + "0" + "\","
        //                                  + "\"Is_Flash\": \"" + "0" + "\","
        //                                  + "\"Message_Type\": \"" + _configuration["SMSSettings:Message_Type"] + "\","
        //                                  + "\"Entity_Id\": \"" + _configuration["SMSSettings:Entity_Id"] + "\","
        //                                  + "\"Content_Template_Id\": \"" + "1407167937909688733" + "\","
        //                                  + "\"Consent_Template_Id\": \"" + "" + "\","
        //                                  + "\"Template_Keys_and_Values\": " + "[{"
        //                                        + "\"Key\": \"" + "" + "\","
        //                                        + "\"Value\": \"" + "" + "\""
        //                                  + "}]"
        //                                  + "}";
        //                bool SendMessage = this.objSMSService.SendMessage(RequestBody);
        //                return new
        //                {
        //                    Message = "User created successfully!",
        //                    IsSuccess = true,
        //                    userid = objAuthUser.Id
        //                };
        //            }

        //            return new
        //            {
        //                Message = "User did not create",
        //                IsSuccess = false,
        //                Errors = ""
        //            };

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public async Task<dynamic> PharmacyRegister(PharmacyReg pharmacyReg)
        //{
        //    try
        //    {
        //        using (GlobalContext globalContext = new GlobalContext())
        //        {
        //            var objUserVerfiy = await this.auth.UserVerfication.FirstOrDefaultAsync(x => x.Id == pharmacyReg.Id && ((DateTime.Now.Second - x.OTPCreatedDate.Value.Second) + ((DateTime.Now.Minute - x.OTPCreatedDate.Value.Minute) * 60) <= ((x.ExpiryTime * 60) - 10)) && x.OTP == pharmacyReg.OTP);
        //            if (objUserVerfiy != null)
        //            {
        //                return new
        //                {
        //                    Message = "Invalid OTP Expired",
        //                    IsSuccess = false,
        //                };
        //            }

        //            int id = await primarykeyvalue.primary_key("Pharmacy");
        //            string Filename = "default_user.png";

        //            AuthUser objAuthUser = new AuthUser()
        //            {
        //                UserName = Convert.ToString(pharmacyReg.Ph_MobileNumber),
        //                UserId = globalContext.Users.Max(x => x.UserId) + 1,
        //                FirstName = "",
        //                LastName = "",
        //                PhoneNumber = Convert.ToString(pharmacyReg.Ph_MobileNumber),
        //                Role_Id_FK = pharmacyReg.Role_ID,
        //                Email = pharmacyReg.Ph_Email,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                IsEnabled = true,
        //                Inactive = "N",
        //                Imagename = Filename,
        //                Id = Guid.NewGuid().ToString(),
        //                PhoneNumberConfirmed = true
        //            };
        //            //var result = await userManager.CreateAsync(objAuthUser, pharmacyReg.Password);
        //            var result = await auth.Users.AddAsync(objAuthUser);
        //            await globalContext.SaveChangesAsync();

        //            Pharmacy obj = new Pharmacy()
        //            {
        //                Ph_Id = id,
        //                Ph_UserID = objAuthUser.Id,
        //                Ph_Code = pharmacyReg.Ph_Code,
        //                Ph_Name = pharmacyReg.Ph_Name,
        //                Ph_Address = pharmacyReg.Ph_Address,
        //                PrimaryOrBranch = pharmacyReg.PrimaryOrBranch,
        //                Ph_Branch = pharmacyReg.Ph_Branch != null ? pharmacyReg.Ph_Branch : 0,
        //                cat_id = pharmacyReg.cat_id,
        //                T_Id = pharmacyReg.T_Id,
        //                Ph_NE_Id = pharmacyReg.Ph_NE_Id,
        //                Ph_HO_Id_FK = pharmacyReg.Ph_HO_Id_FK,
        //                Ph_COUN_Id = pharmacyReg.Ph_COUN_Id,
        //                Ph_ST_Id_FK = pharmacyReg.Ph_ST_Id_FK,
        //                Ph_DI_Id_FK = pharmacyReg.Ph_DI_Id_FK,
        //                Ph_tl_Id = pharmacyReg.Ph_tl_Id,
        //                Ph_GR_Id = pharmacyReg.Ph_GR_Id,
        //                Ph_PostalCode = pharmacyReg.Ph_PostalCode,
        //                Ph_MobileNumber = pharmacyReg.Ph_MobileNumber,
        //                Ph_AlterNumber = pharmacyReg.Ph_AlterNumber,
        //                Ph_LandLineNo = pharmacyReg.Ph_LandLineNo,
        //                Ph_Email = pharmacyReg.Ph_Email,
        //                GSTno = pharmacyReg.GSTno,
        //                PANno = pharmacyReg.PANno,
        //                RegNo = pharmacyReg.RegNo,
        //                Ph_Logo = Filename,
        //                created_by = 1,
        //                created_date = DateTime.Now,
        //                delete_flag = false,
        //                status = 1,
        //                OwnerName = pharmacyReg.OwnerName
        //            };
        //            var PharmacyResult = await globalContext.Pharmacy.AddAsync(obj);
        //            await globalContext.SaveChangesAsync();

        //            if (result.Entity != null)
        //            {
        //                string RequestBody = "{"
        //                                  + "\"Header\": \"" + "RgMDTL" + "\","
        //                                  + "\"Target\": \"" + pharmacyReg.Ph_MobileNumber + "\","
        //                                  + "\"Is_Unicode\": \"" + "0" + "\","
        //                                  + "\"Is_Flash\": \"" + "0" + "\","
        //                                  + "\"Message_Type\": \"" + _configuration["SMSSettings:Message_Type"] + "\","
        //                                  + "\"Entity_Id\": \"" + _configuration["SMSSettings:Entity_Id"] + "\","
        //                                  + "\"Content_Template_Id\": \"" + "1407167937909688733" + "\","
        //                                  + "\"Consent_Template_Id\": \"" + "" + "\","
        //                                  + "\"Template_Keys_and_Values\": " + "[{"
        //                                        + "\"Key\": \"" + "" + "\","
        //                                        + "\"Value\": \"" + "" + "\""
        //                                  + "}]"
        //                                  + "}";
        //                bool SendMessage = this.objSMSService.SendMessage(RequestBody);

        //                return new
        //                {
        //                    Message = "User created successfully!",
        //                    IsSuccess = true,
        //                    userid = objAuthUser.Id
        //                };
        //            }

        //            return new
        //            {
        //                Message = "User did not create",
        //                IsSuccess = false,
        //                Errors = ""
        //            };

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public async Task<dynamic> DiagCenterRegister(DiagnosticReg diagnosticReg)
        //{
        //    try
        //    {
        //        using (GlobalContext globalContext = new GlobalContext())
        //        {
        //            var objUserVerfiy = await this.auth.UserVerfication.FirstOrDefaultAsync(x => x.Id == diagnosticReg.Id && ((DateTime.Now.Second - x.OTPCreatedDate.Value.Second) + ((DateTime.Now.Minute - x.OTPCreatedDate.Value.Minute) * 60) <= ((x.ExpiryTime * 60) - 10)) && x.OTP == diagnosticReg.OTP);
        //            if (objUserVerfiy == null)
        //            {
        //                return new
        //                {
        //                    Message = "Invalid OTP Expired",
        //                    IsSuccess = false,
        //                };
        //            }

        //            int id = await primarykeyvalue.primary_key("DiagnosticCenter");
        //            string Filename = "default_user.png";

        //            AuthUser objAuthUser = new AuthUser()
        //            {
        //                UserName = Convert.ToString(diagnosticReg.DGSTC_MobileNumber),
        //                UserId = globalContext.Users.Max(x => x.UserId) + 1,
        //                FirstName = "",
        //                LastName = "",
        //                PhoneNumber = Convert.ToString(diagnosticReg.DGSTC_MobileNumber),
        //                Role_Id_FK = diagnosticReg.Role_ID,
        //                Email = diagnosticReg.DGSTC_Email,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                IsEnabled = true,
        //                Inactive = "N",
        //                Imagename = Filename,
        //                Id = Guid.NewGuid().ToString(),
        //                PhoneNumberConfirmed = true
        //            };
        //            //var result = await userManager.CreateAsync(objAuthUser, diagnosticReg.Password);
        //            var result = await auth.Users.AddAsync(objAuthUser);
        //            await globalContext.SaveChangesAsync();

        //            DiagnosticCenters obj = new DiagnosticCenters()
        //            {
        //                DGSTC_Id = id,
        //                DGSTC_UserID = objAuthUser.Id,
        //                DGSTC_Code = diagnosticReg.DGSTC_Code,
        //                DGSTC_Name = diagnosticReg.DGSTC_Name,
        //                PrimaryOrBranch = diagnosticReg.PrimaryOrBranch,
        //                DGSTC_Branch = diagnosticReg.DGSTC_Branch,
        //                DGSTC_Type_Id = diagnosticReg.DGSTC_Type_Id,
        //                cat_id = diagnosticReg.cat_id,
        //                DGSTC_NE_Id = diagnosticReg.DGSTC_NE_Id,
        //                DGSTC_Address = diagnosticReg.DGSTC_Address,
        //                DGSTC_HO_Id_FK = diagnosticReg.DGSTC_HO_Id_FK,
        //                DGSTC_COUN_Id_FK = diagnosticReg.DGSTC_COUN_Id_FK,
        //                DGSTC_ST_Id_FK = diagnosticReg.DGSTC_ST_Id_FK,
        //                DGSTC_DI_Id_FK = diagnosticReg.DGSTC_DI_Id_FK,
        //                DGSTC_TL_Id_FK = diagnosticReg.DGSTC_TL_Id_FK,
        //                DGSTC_GR_Id_FK = diagnosticReg.DGSTC_GR_Id_FK,
        //                DGSTC_PostalCode = diagnosticReg.DGSTC_PostalCode,
        //                DGSTC_MobileNumber = diagnosticReg.DGSTC_MobileNumber,
        //                DGSTC_AlterNumber = diagnosticReg.DGSTC_AlterNumber,
        //                DGSTC_LandLineNo = diagnosticReg.DGSTC_LandLineNo,
        //                DGSTC_Email = diagnosticReg.DGSTC_Email,
        //                //GSTNoOrPANno = lead.GSTNoOrPANno,
        //                RegNo = diagnosticReg.RegNo,
        //                DGSTC_Logo = Filename,
        //                created_by = 1,
        //                created_date = DateTime.Now,
        //                delete_flag = false,
        //                status = 1
        //            };
        //            var DiagnosticCentersResult = await globalContext.DiagnosticCenters.AddAsync(obj);
        //            await globalContext.SaveChangesAsync();

        //            if (result.Entity != null)
        //            {
        //                string RequestBody = "{"
        //                                  + "\"Header\": \"" + "RgMDTL" + "\","
        //                                  + "\"Target\": \"" + diagnosticReg.DGSTC_MobileNumber + "\","
        //                                  + "\"Is_Unicode\": \"" + "0" + "\","
        //                                  + "\"Is_Flash\": \"" + "0" + "\","
        //                                  + "\"Message_Type\": \"" + _configuration["SMSSettings:Message_Type"] + "\","
        //                                  + "\"Entity_Id\": \"" + _configuration["SMSSettings:Entity_Id"] + "\","
        //                                  + "\"Content_Template_Id\": \"" + "1407167937909688733" + "\","
        //                                  + "\"Consent_Template_Id\": \"" + "" + "\","
        //                                  + "\"Template_Keys_and_Values\": " + "[{"
        //                                        + "\"Key\": \"" + "" + "\","
        //                                        + "\"Value\": \"" + "" + "\""
        //                                  + "}]"
        //                                  + "}";
        //                bool SendMessage = this.objSMSService.SendMessage(RequestBody);
        //                return new
        //                {
        //                    Message = "User created successfully!",
        //                    IsSuccess = true,
        //                    userid = objAuthUser.Id
        //                };
        //            }

        //            return new
        //            {
        //                Message = "User did not create",
        //                IsSuccess = false,
        //                Errors = ""
        //            };

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        private string UploadedFile(IFormFile Image)
        {
            string? uniqueFileName = null;


            if (Image != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "User not found"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Email did not confirm",
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        public async Task<UserManagerResponse> ForgetPasswordAsync(string Username)
        {
            var user = await userManager.FindByEmailAsync(Username);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with email",
                };

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["AppUrl"]}/ResetPassword?username={Username}&token={validToken}";

            await _EMailService.SendEmailAsync(user.UserName, user.Email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password <a href='{url}'>Click here</a></p>");

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Reset password URL has been sent to the email successfully!"
            };
        }


        //saheb online
        public async Task<UserManagerResponse> ResetPasswordAsync_Online(ResetPasswordViewModel_Online model)
        {


            if (model.user_type == "Institution")
            {
                var objInsId = await obj_FindUserId.FindInstitutionIdFromUserId(model.PhoneNumber);
                if (objInsId == 0)
                {
                    return new UserManagerResponse
                    {
                        Message = "Phonenumber not registered with Institution",
                        IsSuccess = false
                    };

                }

            }

            if (model.user_type == "Corporate")
            {
                var objCoId = await obj_FindUserId.FindCorporateIdFromUserId(model.PhoneNumber);
                if (objCoId == 0)
                {
                    return new UserManagerResponse
                    {
                        Message = "Phonenumber not registered with Corporate",
                        IsSuccess = false
                    };
                }
            }
            if (model.user_type == "Individual")
            {
                var objIndId = await obj_FindUserId.FindIndividualIdFromUserId(model.PhoneNumber);
                if (objIndId == 0)
                {
                    return new UserManagerResponse
                    {
                        Message = "Phonenumber not registered with Individual",
                        IsSuccess = false
                    };
                }
            }
            if (model.user_type == "Student")
            {
                var objStuId = await obj_FindUserId.FindStudentIdFromUserId(model.PhoneNumber);
                if (objStuId == 0)
                {
                    return new UserManagerResponse
                    {
                        Message = "Phonenumber not registered with Student",
                        IsSuccess = false
                    };
                }
            }
            var objUserVerfiy = await this.auth.UserVerfication.FirstOrDefaultAsync(x => x.Id == model.Id && ((DateTime.Now.Second - x.OTPCreatedDate.Value.Second) + ((DateTime.Now.Minute - x.OTPCreatedDate.Value.Minute) * 60) <= ((x.ExpiryTime * 60) - 10)) && x.OTP == model.OTP);
            if (objUserVerfiy == null)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid OTP Expired",
                    IsSuccess = false,
                };
            }

            var user = userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "No user associated with this Phone Number: " + model.PhoneNumber,
                    IsSuccess = false
                };
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Password doesn't match with confirmation password",
                    IsSuccess = false,
                };
            }
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var result = await userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "Something went wrong",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }


        //saheb get role id based on user name used in mobile app

        public async Task<UserRoleCustomResponse> GetRoleId_MobileApp(string Phonenumber)
        {
            //get user role id fk based on username
            var user = userManager.Users.FirstOrDefault(x => x.PhoneNumber == Phonenumber);
            if (user == null)
            {
                return new UserRoleCustomResponse
                {
                    username = "",
                    role_id = "",
                    role_name = "",
                    is_success = false,
                    message = "User Name does not exist."

                };
            }

            var role = roleManager.Roles.FirstOrDefault(x => x.Id == user.Role_Id_FK);
            if (role == null)
            {
                return new UserRoleCustomResponse
                {
                    username = user.UserName,
                    role_id = role.Id,
                    role_name = role.Name,
                    is_success = false,
                    message = "Role Name does not exist."

                };
            }

            //pass user role id fk to get role name           
            if (user != null && role != null)
            {
                return new UserRoleCustomResponse
                {
                    username = user.UserName,
                    role_id = role.Id,
                    role_name = role.Name,
                    is_success = true,
                    message = "Role name exist."

                };
            }


            return new UserRoleCustomResponse
            {
                is_success = false,
                message = "No user associated with this Phone Number: " + Phonenumber
            };


        }


        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string tooken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _goolgeSettings.GetSection("clientId").Value }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(tooken, settings);
                return payload;
            }
            catch (Exception ex)
            {
                //log an exception
                throw new Exception(ex.Message);
            }
        }
        public async Task<FacebookTookenvalidationResult> VerifyFacebookToken(string accesstoken)
        {
            var formattedUrl = string.Format(TokenvalidationUrl, accesstoken, _facebookAuthSetting.AppId, _facebookAuthSetting.AppSecret);
            var result = await _httpClientfactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsstring = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookTookenvalidationResult>(responseAsstring);
        }
        public async Task<FacebookUserInfoResult> GetUserInfoAsync(string accesstoken)
        {
            var formattedUrl = string.Format(UserInfo, accesstoken);
            var result = await _httpClientfactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsstring = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsstring);
        }
        public async Task<UserManagerResponse> ForGoogle(string Token)
        {
            var payload = await VerifyGoogleToken(Token);
            if (payload == null)
                return new UserManagerResponse
                {
                    Message = "Invalid External Authentication.",
                    IsSuccess = false,
                };

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == payload.Email || x.Email == payload.Email);
                if (user == null)
                {
                    user = new AuthUser { Email = payload.Email, UserName = payload.Email };
                    await userManager.CreateAsync(user);
                    await userManager.AddLoginAsync(user, info);
                }
                else

                    await userManager.AddLoginAsync(user, info);
            }
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "Invalid External Authentication.",
                    IsSuccess = false,
                };
            var token = await userManager.CreateSecurityTokenAsync(user);
            //TokenHandler._configuration = _configuration;
            return new UserManagerResponse
            {
                Message = "Google Login successfully!",
                IsSuccess = true,
                ExpireDate = DateTime.Now.AddHours(5),
                token = await CreateAccessToken(payload.Email)
            };
        }
        public async Task<UserManagerResponse> ForFacebook(string accesstoken)
        {
            var validateresult = await VerifyFacebookToken(accesstoken);
            var userInfo = await GetUserInfoAsync(accesstoken);
            var info = new UserLoginInfo("FACEBOOK", _facebookAuthSetting.AppId, "FACEBOOK");
            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == userInfo.email || x.Email == userInfo.email);
                if (user == null)
                {
                    user = new AuthUser { Email = userInfo.email, UserName = userInfo.email };
                    var createdResult = await userManager.CreateAsync(user);
                    await userManager.AddLoginAsync(user, info);
                    if (!createdResult.Succeeded)
                    {
                        return new UserManagerResponse
                        {
                            Message = "Something went wrong",
                            IsSuccess = false,
                        };

                    }
                }
                else
                    await userManager.AddLoginAsync(user, info);
            }
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "Invalid External Authentication.",
                    IsSuccess = false,
                };
            var token = await userManager.CreateSecurityTokenAsync(user);
            //TokenHandler._configuration = _configuration;
            return new UserManagerResponse
            {
                Message = "Facebook Login successfully!",
                IsSuccess = true,
                ExpireDate = DateTime.Now.AddHours(5),
                token = await CreateAccessToken(userInfo.email)
            };

        }
        public async Task<UserManagerResponse> ChangePasswordAsync(ChangePassword model, string userName)
        {
            if (model != null)
            {
                //var user = await userManager.FindByNameAsync(model.Username);
                var user = userManager.Users.FirstOrDefault(x => x.UserName == userName || x.PhoneNumber == userName);

                if (user == null)
                {
                    return new UserManagerResponse
                    {
                        IsSuccess = false,
                        Message = "No user find with " + userName,
                    };
                }
                // ChangePasswordAsync changes the user password
                var result = await userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    return new UserManagerResponse
                    {
                        IsSuccess = false,
                        Message = "This " + model.CurrentPassword + " password is not valid",
                    };
                }

                // Upon successfully changing the password refresh sign-in cookie
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Your ChangePasswordConfirmation successfuly",
                };

            }
            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Your sending data are not valid",
            };
        }
        public async Task<string> CreateAccessToken(string username)
        {
            var user = await userManager.FindByEmailAsync(username);
            var userRoles = await userManager.GetRolesAsync(user);
            var authClims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                };
            foreach (var userrole in userRoles)
            {
                authClims.Add(new Claim(ClaimTypes.Role, userrole));
            }
            var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            DateTime now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:iss"],
                audience: _configuration["JWT:aud"],
                claims: authClims,
                notBefore: now,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256));
            var validtoken = new JwtSecurityTokenHandler().WriteToken(token);
            return validtoken;
        }
        public async Task<bool> UpdateUserAsync(RegisterBindingModel model, string userid)
        {

            string roleName = await obj_FindUserId.FindRoleNameFromUserId(userid);

            if (roleName != "")
            {
                AuthUser user = new AuthUser();
                UserStore<AuthUser> store = new UserStore<AuthUser>(auth);
                user = await userManager.FindByIdAsync(userid);
                String hashedNewPassword = userManager.PasswordHasher.HashPassword(user, model.Password);
                AuthUser cUser = await store.FindByIdAsync(user.Id);
                await store.SetPasswordHashAsync(cUser, hashedNewPassword);
                await store.UpdateAsync(cUser);
                return true;
            }
            else
                return false;
        }
        public async Task<bool> DeleteUserAsync(string userId)
        {

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return true;
                }

                return false;
            }
        }
        public async Task<bool> ActivateInactivate(string userid)
        {
            AuthUser user = new AuthUser();
            UserStore<AuthUser> store = new UserStore<AuthUser>(auth);

            var result = userManager.Users.FirstOrDefault(x => x.Id == userid);
            if (result.Inactive == "N" || result.Inactive == null)
            {
                if (result != null)
                {
                    result.Inactive = "Y";
                    await store.UpdateAsync(result);
                    return false;
                }
                return false;
            }
            else
            {
                if (result != null)
                {
                    result.Inactive = "N";
                    await store.UpdateAsync(result);

                }
                return true;
            }
        }

        public bool Userverification(string data)
        {
            var result = userManager.Users.FirstOrDefault(x => x.PhoneNumber == data || x.Email == data);
            if (result != null)
            {
                return true;
            }
            else
                return false;
        }

        public async Task<bool> ApproveUser(string userid, string? Remarks)
        {
            var result = userManager.Users.FirstOrDefault(x => x.Id == userid);
            AuthUser user = new AuthUser();
            UserStore<AuthUser> store = new UserStore<AuthUser>(auth);
            if (result.IsEnabled == false)
            {
                result.IsEnabled = true;
                if (Remarks == null)
                {
                    result.Remarks = "OK";
                }
                else
                    result.Remarks = Remarks;
                await store.UpdateAsync(result);
                return true;
            }
            else
                await store.UpdateAsync(result);
            return false;

        }

        public async Task<Dictionary<string, object>> SendOTP(string Phonenumber, string TemplateId)
        {

            var objUser = await this.auth.Users.FirstOrDefaultAsync(x => x.PhoneNumber == Phonenumber || x.UserName == Phonenumber);
            Dictionary<string, object> objOTP = new Dictionary<string, object>();
            if (objUser != null)
            {
                objOTP.Add("Id", 0);
                objOTP.Add("IsSuccess", false);
                objOTP.Add("Message", "Phonenumber Already Verified");
                return objOTP;
            }
            int Id = await primarykeyvalue.primary_key("UserVerfication");

            Random r = new Random();
            string OTP = r.Next(1000, 9999).ToString();
            int ExpiryTime = DateTime.Now.Minute + 2;
            UserVerfication objUserVerfiy = new UserVerfication()
            {
                Id = Id,
                OTP = OTP,
                Phonenumber = Convert.ToInt64(Phonenumber),
                Email = "",
                ExpiryTime = 2,
                OTPCreatedDate = DateTime.Now
            };
            var result = await this.auth.UserVerfication.AddAsync(objUserVerfiy);
            await this.auth.SaveChangesAsync();
            string RequestBody = "{"
                  + "\"Header\": \"" + "RgMDTL" + "\","
                  + "\"Target\": \"" + Phonenumber + "\","
                  + "\"Is_Unicode\": \"" + "0" + "\","
                  + "\"Is_Flash\": \"" + "0" + "\","
                  + "\"Message_Type\": \"" + _configuration["SMSSettings:Message_Type"] + "\","
                  + "\"Entity_Id\": \"" + _configuration["SMSSettings:Entity_Id"] + "\","
                  + "\"Content_Template_Id\": \"" + TemplateId + "\","
                  + "\"Consent_Template_Id\": \"" + "" + "\","
                  + "\"Template_Keys_and_Values\": " + "[{"
                        + "\"Key\": \"" + "var" + "\","
                        + "\"Value\": \"" + OTP + "\""
                  + "}]"
                  + "}";
            bool SendMessage = this.objSMSService.SendMessage(RequestBody);
            if (SendMessage)
            {
                objOTP.Add("Id", Id);
                objOTP.Add("IsSuccess", true);
                objOTP.Add("Message", $"OTP Successfully Sent to {Phonenumber}");
                return objOTP;
            }
            else
            {
                objOTP.Add("Id", 0);
                objOTP.Add("IsSuccess", false);
                objOTP.Add("Message", "Try After Some Time!");
                return objOTP;
            }

        }



        //online saheb reg
        public async Task<Dictionary<string, object>> SendOTP_Reg(string Phonenumber, string TemplateId)
        {

            var objUser = await this.auth.Users.FirstOrDefaultAsync(x => x.PhoneNumber == Phonenumber || x.UserName == Phonenumber);
            Dictionary<string, object> objOTP = new Dictionary<string, object>();
            if (objUser != null)
            {
                objOTP.Add("verify_id", 0);
                objOTP.Add("otp", string.Empty);
                objOTP.Add("is_success", false);
                objOTP.Add("message", "Phonenumber already verified");
                return objOTP;
            }
            int Id = await primarykeyvalue.primary_key("UserVerfication_Online");

            Random r = new Random();
            string OTP = r.Next(1000, 9999).ToString();
            int ExpiryTime = DateTime.Now.Minute + 2;
            UserVerfication_Online objUserVerfiy = new UserVerfication_Online()
            {
                verify_id = Id,
                otp = OTP,
                phone_number = Phonenumber,
                email = "",
                expiry_time = 2,
                created_date = DateTime.Now
            };
            var result = await this.auth.UserVerfication_Online.AddAsync(objUserVerfiy);
            await this.auth.SaveChangesAsync();
            //string TemplateId = "1407167957139097720";
            string RequestBody = "{"
                  + "\"Header\": \"" + "RgMDTL" + "\","
                  + "\"Target\": \"" + Phonenumber + "\","
                  + "\"Is_Unicode\": \"" + "0" + "\","
                  + "\"Is_Flash\": \"" + "0" + "\","
                  + "\"Message_Type\": \"" + _configuration["SMSSettings:Message_Type"] + "\","
                  + "\"Entity_Id\": \"" + _configuration["SMSSettings:Entity_Id"] + "\","
                  + "\"Content_Template_Id\": \"" + TemplateId + "\","
                  + "\"Consent_Template_Id\": \"" + "" + "\","
                  + "\"Template_Keys_and_Values\": " + "[{"
                        + "\"Key\": \"" + "var" + "\","
                        + "\"Value\": \"" + OTP + "\""
                  + "}]"
                  + "}";
            bool SendMessage = this.objSMSService.SendMessage(RequestBody);
            if (SendMessage)
            {
                objOTP.Add("verify_id", Id);
                objOTP.Add("otp", OTP);
                objOTP.Add("is_success", true);
                objOTP.Add("message", $"OTP Successfully Sent to {Phonenumber}");
                return objOTP;
            }
            else
            {
                objOTP.Add("verify_id", 0);
                objOTP.Add("otp", string.Empty);
                objOTP.Add("is_success", false);
                objOTP.Add("message", "Try After Some Time!");
                return objOTP;
            }

        }

        //online saheb for both patient and doctor        
        public async Task<Dictionary<string, object>> SendOTP_ForgotPassword(string Phonenumber, string TemplateId)
        {

            var objUser = await this.auth.Users.FirstOrDefaultAsync(x => x.PhoneNumber == Phonenumber || x.UserName == Phonenumber);
            Dictionary<string, object> objOTP = new Dictionary<string, object>();
            if (objUser == null)
            {
                objOTP.Add("Id", 0);
                objOTP.Add("IsSuccess", false);
                objOTP.Add("Message", "Phonenumber not registered");
                return objOTP;
            }

            //cross check with child table
            if (TemplateId == "1407167973883559921")
            {
                var objInsId = await obj_FindUserId.FindInstitutionIdFromUserId(objUser.UserName);
                if (objInsId == 0)
                {
                    objOTP.Add("Id", 0);
                    objOTP.Add("IsSuccess", false);
                    objOTP.Add("Message", "Phonenumber not registered with Institution");
                    return objOTP;
                }

            }

            if (TemplateId == "1407167973883559921")
            {
                var objCoId = await obj_FindUserId.FindCorporateIdFromUserId(objUser.UserName);
                if (objCoId == 0)
                {
                    objOTP.Add("Id", 0);
                    objOTP.Add("IsSuccess", false);
                    objOTP.Add("Message", "Phonenumber not registered with Corporate");
                    return objOTP;
                }

            }
            if (TemplateId == "1407167957139097720")
            {
                var objIndId = await obj_FindUserId.FindIndividualIdFromUserId(objUser.UserName);
                if (objIndId == 0)
                {
                    objOTP.Add("Id", 0);
                    objOTP.Add("IsSuccess", false);
                    objOTP.Add("Message", "Phonenumber not registered with Corporate");
                    return objOTP;
                }

            }
            if (TemplateId == "1407167973883559921")
            {
                var objIndId = await obj_FindUserId.FindStudentIdFromUserId(objUser.UserName);
                if (objIndId == 0)
                {
                    objOTP.Add("Id", 0);
                    objOTP.Add("IsSuccess", false);
                    objOTP.Add("Message", "Phonenumber not registered with Corporate");
                    return objOTP;
                }

            }



            int Id = await primarykeyvalue.primary_key("UserVerfication");

            Random r = new Random();
            string OTP = r.Next(1000, 9999).ToString();
            int ExpiryTime = DateTime.Now.Minute + 2;
            UserVerfication objUserVerfiy = new UserVerfication()
            {
                Id = Id,
                OTP = OTP,
                Phonenumber = Convert.ToInt64(Phonenumber),
                Email = "",
                ExpiryTime = 2,
                OTPCreatedDate = DateTime.Now
            };
            var result = await this.auth.UserVerfication.AddAsync(objUserVerfiy);
            await this.auth.SaveChangesAsync();
            string RequestBody = "{"
                  + "\"Header\": \"" + "RgMDTL" + "\","
                  + "\"Target\": \"" + Phonenumber + "\","
                  + "\"Is_Unicode\": \"" + "0" + "\","
                  + "\"Is_Flash\": \"" + "0" + "\","
                  + "\"Message_Type\": \"" + _configuration["SMSSettings:Message_Type"] + "\","
                  + "\"Entity_Id\": \"" + _configuration["SMSSettings:Entity_Id"] + "\","
                  + "\"Content_Template_Id\": \"" + TemplateId + "\","
                  + "\"Consent_Template_Id\": \"" + "" + "\","
                  + "\"Template_Keys_and_Values\": " + "[{"
                        + "\"Key\": \"" + "var" + "\","
                        + "\"Value\": \"" + OTP + "\""
                  + "}]"
                  + "}";
            bool SendMessage = this.objSMSService.SendMessage(RequestBody);
            if (SendMessage)
            {
                objOTP.Add("Id", Id);
                objOTP.Add("IsSuccess", true);
                objOTP.Add("Message", $"OTP Successfully Sent to {Phonenumber}");
                return objOTP;
            }
            else
            {
                objOTP.Add("Id", 0);
                objOTP.Add("IsSuccess", false);
                objOTP.Add("Message", "Try After Some Time!");
                return objOTP;
            }

        }


        public async Task<string> UpdateUserProfile(AuthUser_Details model, string? rolename)
        {
            try
            {
                string Filename = string.Empty;
                var user = await auth.Users.FirstOrDefaultAsync(x => x.Id == model.Id);
                //var doctor = await auth.Doctor.FirstOrDefaultAsync(y => y.DO_UserId == model.Id);
                //var patient = await auth.Patient.FirstOrDefaultAsync(p => p.PR_UserId == model.Id);
                // var assistant = await auth.Assistant.FirstOrDefaultAsync(a => a.Asssi_UserID == model.Id);
                var objuserPhonenumber = await auth.Users.Where(x => x.UserName == model.PhoneNumber || x.PhoneNumber == model.PhoneNumber).FirstOrDefaultAsync();
                var objuserEmail = await auth.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

                if (objuserPhonenumber != null)
                {
                    if (objuserPhonenumber.PhoneNumber != user.PhoneNumber)
                    {
                        return "MobileNumber Already Exists";
                    }
                }
                if (objuserEmail != null)
                {
                    if (objuserEmail.Email != user.Email)
                    {
                        return "Email Already Exists";
                    }
                }

                if (model.Image != null) //profile photo is for all the roles
                {
                    if (user.Imagename != null && user.Imagename != "default_user.png")
                    {
                        string filepath = Path.Combine("wwwroot/Images", user.Imagename);
                        System.IO.File.Delete(filepath);
                    }
                    Filename = this.fileUpload.ProcessUploadedFile("wwwroot/Images", model.Image);
                }
                else
                {
                    Filename = user.Imagename;
                }

                if (user != null)
                {
                    //updateing aspnet user for the all the roles
                    user.UserName = model.PhoneNumber;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.Gender = model.Gender;
                    user.DOB = model.DOB;
                    user.Imagename = Filename;
                    await auth.SaveChangesAsync();
                    //if (rolename == "Doctor")
                    //{
                    //    //update doctor table
                    //    doctor.DO_FirstName = model.FirstName;
                    //    doctor.DO_LastName = model.LastName;
                    //    doctor.DO_MobileNumber = (long)Convert.ToDecimal(model.PhoneNumber);
                    //    doctor.DO_Email = model.Email;
                    //    doctor.DO_Gender = model.Gender;
                    //    doctor.DO_DOB = model.DOB;
                    //    doctor.DO_Photo = Filename;
                    //    await auth.SaveChangesAsync();

                    // }
                    //if (rolename == "patient")
                    //{
                    //    //update patient table
                    //    patient.PR_FirstName = model.FirstName;
                    //    patient.PR_LastName = model.LastName;
                    //    patient.PR_MobileNumber = model.PhoneNumber;
                    //    patient.PR_Email = model.Email;
                    //    patient.PR_Gender = model.Gender;
                    //    patient.PR_DOB = model.DOB;
                    //    patient.PR_Photo = Filename;
                    //    await auth.SaveChangesAsync();
                    //}
                    //if (rolename == "Medical Assistant")
                    //{
                    //    assistant.Assi_FirstName = model.FirstName;
                    //    assistant.Assi_LastName = model.LastName;
                    //    assistant.Assi_MobileNumber = (long)Convert.ToDecimal(model.PhoneNumber);
                    //    assistant.Assi_Email = model.Email;
                    //    assistant.Assi_Gender = model.Gender;
                    //    assistant.Assi_DOB = model.DOB;
                    //    assistant.Assi_Photo = Filename;
                    //    await auth.SaveChangesAsync();
                    //}
                    return "User Updated successfully";
                }
                return "User Deatils not updated!";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AuthUser_Details> GetUsersByNumber(string username)
        {
            try
            {
                var objUsers = await auth.Users.FirstOrDefaultAsync(b => b.UserName == username);
                var objRoles = await auth.Roles.FirstOrDefaultAsync(c => c.Id == objUsers.Role_Id_FK);
                AuthUser_Details obj = new AuthUser_Details();
                obj.Id = objUsers.Id;
                obj.UserName = objUsers.UserName;
                obj.FirstName = objUsers.FirstName;
                // obj.LastName = objUsers.LastName;
                obj.Email = objUsers.Email;
                obj.Gender = objUsers.Gender;
                obj.PhoneNumber = objUsers.PhoneNumber;
                obj.DOB = objUsers.DOB;
                obj.Rolename = objRoles.Name.ToUpper();
                obj.Role_Id_FK = objUsers.Role_Id_FK;
                //  obj.ProfilePicture = objUsers.Imagename;
                // obj.Imagename = _configuration["AppUrl"] + "Images/" + objUsers.Imagename;
                return obj;

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
                var objUsers = await auth.Users.FirstOrDefaultAsync(b => b.UserName == username);
                var objRoles = await auth.Roles.FirstOrDefaultAsync(c => c.Id == objUsers.Role_Id_FK);
                AuthUser_Details obj = new AuthUser_Details();
                obj.Id = objUsers.Id;
                obj.UserName = objUsers.UserName;
                obj.FirstName = objUsers.FirstName;
                // obj.LastName = objUsers.LastName;
                obj.Email = objUsers.Email;
                obj.Gender = objUsers.Gender;
                obj.PhoneNumber = objUsers.PhoneNumber;
                obj.DOB = objUsers.DOB;
                obj.Rolename = objRoles.Name.ToUpper();
                obj.Role_Id_FK = objUsers.Role_Id_FK;
                //  obj.ProfilePicture = objUsers.Imagename;
                // obj.Imagename = _configuration["AppUrl"] + "Images/" + objUsers.Imagename;
                return obj;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }


        public async Task<List<GetAllCoursebyname>> GetUserCourseByname(string username)
        {
            try
            {
                // Find the user by username
                var objUsers = await auth.Users.FirstOrDefaultAsync(b => b.UserName == username);
                if (objUsers == null)
                    throw new Exception("User not found");

                // Find the role of the user
                var objRoles = await auth.Roles.FirstOrDefaultAsync(c => c.Id == objUsers.Role_Id_FK);
                if (objRoles == null)
                    throw new Exception("Role not found for the user");

                // Create an instance of AuthUser_Details to populate later
                //AuthUser_Details obj = new AuthUser_Details
                //{
                //    Id = objUsers.Id,
                //    FirstName = objUsers.FirstName,
                //    Email = objUsers.Email,
                //    Gender = objUsers.Gender,
                //    Rolename = objRoles.Name.ToUpper(),
                //    Role_Id_FK = objUsers.Role_Id_FK
                //};

                // Handling different roles
                if (objRoles.Name == "Institition")
                {
                    var getIns = await auth.Institution.FirstOrDefaultAsync(i => i.Ins_InstitutionPhoneNo == username);
                    if (getIns == null)
                        throw new Exception("Institution not found");

                    var result = (from s in auth.Institution
                                  join i in auth.student_courses on s.Ins_Id equals i.Ins_id_fk
                                  join p in auth.Course_Package on i.cp_id equals p.cp_id
                                  join y in auth.Course_Master on p.cu_name equals y.cu_name into ylist
                                  from y in ylist.DefaultIfEmpty()
                                  join b in auth.Course_Section on y.cu_sc_id_fk equals b.sc_id into blist
                                  from b in blist.DefaultIfEmpty()
                                  join c in auth.Course_Chapters on y.cu_sc_id_fk equals c.ch_id into clist
                                  from c in clist.DefaultIfEmpty()
                                  join d in auth.Upload_Videos on c.ch_vi_FK equals d.vi_id into dlist
                                  from d in dlist.DefaultIfEmpty()
                                  join e in auth.Status on y.status equals e.sts_id into elist
                                  from e in elist.DefaultIfEmpty()
                                  where y.cu_id != 0 && s.Ins_InstitutionPhoneNo == username
                                  select new GetAllCoursebyname
                                  {
                                      cu_id = y.cu_id,
                                      cu_name = y.cu_name,
                                      ch_id = c.ch_id,
                                      ch_name = c.ch_name,
                                      sc_id = b.sc_id,
                                      sc_name = b.sc_name,
                                      cu_author = y.cu_author,
                                      vi_id = d.vi_id,
                                      vi_name = d.vi_name,
                                      vi_amount = d.vi_amount,
                                      ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
                                      vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
                                      vi_time = d.vi_time,
                                      status = y.status,
                                      sts_name = e.sts_name,
                                  }).ToList();

                    // Add result to obj if needed
                    return result;
                }


                else if (objRoles.Name == "Corporate")
                {
                    var getIns = await auth.Corporate.FirstOrDefaultAsync(i => i.CO_MobileNumber == username);
                    if (getIns == null)
                        throw new Exception("Corporate entity not found");

                    var result = (from s in auth.Corporate
                                  join i in auth.student_courses on s.CO_Id equals i.Co_id_fk
                                  join p in auth.Course_Package on i.cp_id equals p.cp_id
                                  join y in auth.Course_Master on p.cu_name equals y.cu_name into ylist
                                  from y in ylist.DefaultIfEmpty()
                                  join b in auth.Course_Section on y.cu_sc_id_fk equals b.sc_id into blist
                                  from b in blist.DefaultIfEmpty()
                                  join c in auth.Course_Chapters on y.cu_sc_id_fk equals c.ch_id into clist
                                  from c in clist.DefaultIfEmpty()
                                  join d in auth.Upload_Videos on c.ch_vi_FK equals d.vi_id into dlist
                                  from d in dlist.DefaultIfEmpty()
                                  join e in auth.Status on y.status equals e.sts_id into elist
                                  from e in elist.DefaultIfEmpty()
                                  where y.cu_id != 0 && s.CO_MobileNumber == username
                                  select new GetAllCoursebyname
                                  {
                                      cu_id = y.cu_id,
                                      cu_name = y.cu_name,
                                      ch_id = c.ch_id,
                                      ch_name = c.ch_name,
                                      sc_id = b.sc_id,
                                      sc_name = b.sc_name,
                                      cu_author = y.cu_author,
                                      vi_id = d.vi_id,
                                      vi_name = d.vi_name,
                                      vi_amount = d.vi_amount,
                                      ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
                                      vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
                                      vi_time = d.vi_time,
                                      status = y.status,
                                      sts_name = e.sts_name,
                                  }).ToList();

                    return result;
                }
                else if (objRoles.Name == "Individual")
                {
                    var getIns = await auth.Individual.FirstOrDefaultAsync(i => i.Ind_MobileNumber == username);
                    if (getIns == null)
                        throw new Exception("Individual not found");

                    var query = (from i in auth.Individual
                                 join g in auth.student_courses on i.Ind_Id equals g.Stu_id_fk
                                 join n in auth.Course_Package on g.cp_id equals n.cp_id
                                 join a in auth.Course_Master on n.cu_name equals a.cu_name
                                 join b in auth.Course_Section on a.cu_sc_id_fk equals b.sc_id
                                 join c in auth.Course_Chapters on b.sc_ch_Fk equals c.ch_id
                                 join d in auth.Upload_Videos on c.ch_vi_FK equals d.vi_id
                                 join e in auth.Status on a.status equals e.sts_id
                                 where a.cu_id != 0 && i.Ind_MobileNumber == username
                                 select new GetAllCoursebyname
                                 {
                                     cu_id = a.cu_id,
                                     cu_name = a.cu_name,
                                     ch_id = c.ch_id,
                                     ch_name = c.ch_name,
                                     sc_id = b.sc_id,
                                     ind_name = i.Ind_Name,
                                     sc_name = b.sc_name,
                                     cu_author = a.cu_author,
                                     vi_id = d.vi_id,
                                     vi_name = d.vi_name,
                                     vi_amount = d.vi_amount,
                                     ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
                                     vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
                                     vi_time = d.vi_time,
                                     status = a.status,
                                     sts_name = e.sts_name,

                                 }).ToList();

                    return query;
                }
                else if (objRoles.Name == "Admin")
                {
                    var query = (from a in auth.Course_Master
                                 join b in auth.Course_Section on a.cu_sc_id_fk equals b.sc_id
                                 join c in auth.Course_Chapters on b.sc_ch_Fk equals c.ch_id
                                 join d in auth.Upload_Videos on c.ch_vi_FK equals d.vi_id
                                 join e in auth.Status on a.status equals e.sts_id
                                 where a.cu_id != 0
                                 select new GetAllCoursebyname
                                 {
                                     cu_id = a.cu_id,
                                     cu_name = a.cu_name,
                                     ch_id = c.ch_id,
                                     co_name = "Admin",
                                     ch_name = c.ch_name,
                                     sc_id = b.sc_id,
                                     sc_name = b.sc_name,
                                     cu_author = a.cu_author,
                                     vi_id = d.vi_id,
                                     vi_name = d.vi_name,
                                     vi_amount = d.vi_amount,
                                     ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
                                     vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
                                     vi_time = d.vi_time,
                                     status = a.status,
                                     sts_name = e.sts_name,

                                 }).ToList();

                    return query;

                }

                else if (objRoles.Name == "Student")
                {

                    var getIns = await auth.Student.FirstOrDefaultAsync(i => i.Stu_MobileNumber == username);
                    if (getIns == null)
                        throw new Exception("Student not found");


                    if (getIns != null && (getIns.Stu_Inscode != ""))
                    {
                        var query = (from s in auth.Student
                                     join i in auth.Institution on s.Stu_Inscode equals i.Ins_Reg_No
                                     join g in auth.student_courses on i.Ins_Id equals g.Ins_id_fk
                                     join n in auth.Course_Package on g.cp_id equals n.cp_id
                                     join a in auth.Course_Master on n.cu_name equals a.cu_name
                                     join b in auth.Course_Section on a.cu_sc_id_fk equals b.sc_id
                                     join c in auth.Course_Chapters on b.sc_ch_Fk equals c.ch_id
                                     join d in auth.Upload_Videos on c.ch_vi_FK equals d.vi_id
                                     join e in auth.Status on a.status equals e.sts_id
                                     where a.cu_id != 0 && s.Stu_MobileNumber == username
                                     select new GetAllCoursebyname
                                     {
                                         cu_id = a.cu_id,
                                         cu_name = a.cu_name,
                                         ch_id = c.ch_id,
                                         ch_name = c.ch_name,
                                         sc_id = b.sc_id,
                                         sc_name = b.sc_name,
                                         ins_name = i.Ins_InstitutionName,
                                         stu_name = s.Stu_Name,
                                         cu_author = a.cu_author,
                                         vi_id = d.vi_id,
                                         vi_name = d.vi_name,
                                         vi_amount = d.vi_amount,
                                         ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
                                         vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
                                         vi_time = d.vi_time,
                                         status = a.status,
                                         sts_name = e.sts_name,

                                     }).ToList();

                        return query;

                    }
                    else
                    {
                        var result = (from s in auth.Student
                                      join i in auth.Corporate on s.Stu_Copcode equals i.Co_Reg_No
                                      join g in auth.student_courses on i.CO_Id equals g.Co_id_fk
                                      join n in auth.Course_Package on g.cp_id equals n.cp_id
                                      join a in auth.Course_Master on n.cu_name equals a.cu_name
                                      join b in auth.Course_Section on a.cu_sc_id_fk equals b.sc_id
                                      join c in auth.Course_Chapters on b.sc_ch_Fk equals c.ch_id
                                      join d in auth.Upload_Videos on c.ch_vi_FK equals d.vi_id
                                      join e in auth.Status on a.status equals e.sts_id
                                      where a.cu_id != 0 && s.Stu_MobileNumber == username
                                      select new GetAllCoursebyname
                                      {
                                          cu_id = a.cu_id,
                                          cu_name = a.cu_name,
                                          ch_id = c.ch_id,
                                          ch_name = c.ch_name,
                                          sc_id = b.sc_id,
                                          sc_name = b.sc_name,
                                          co_name = i.CO_Name,
                                          stu_name = s.Stu_Name,
                                          cu_author = a.cu_author,
                                          vi_id = d.vi_id,
                                          vi_name = d.vi_name,
                                          vi_amount = d.vi_amount,
                                          ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
                                          vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
                                          vi_time = d.vi_time,
                                          status = a.status,
                                          sts_name = e.sts_name,


                                      }).ToList();

                        return result;
                    }

                }

                return new List<GetAllCoursebyname>();
            }
            catch (Exception ex)
            {
                // Log the exception and rethrow if necessary
                throw new Exception($"Error in GetUserByname: {ex.Message}", ex);
            }
        }


        public async Task<List<getprofile>> GetProfile(string username)
        {
            try
            {
                var objStu = await auth.Student.FirstOrDefaultAsync(b => b.Stu_MobileNumber == username);

                if (objStu != null && (objStu.Stu_Inscode != "" || objStu.Stu_Copcode != ""))
                {
                    var resultAll = await (
                        from a in auth.Student
                        join b in auth.Institution on a.Stu_Inscode equals b.Ins_Reg_No into InsMasterJoin
                        from b in InsMasterJoin.DefaultIfEmpty()
                        join h in auth.Corporate on a.Stu_Copcode equals h.Co_Reg_No into CopMasterJoin
                        from h in CopMasterJoin.DefaultIfEmpty()
                        join c in auth.student_courses on b.Ins_Id equals c.Ins_id_fk into StInsMasterJoin
                        from c in StInsMasterJoin.DefaultIfEmpty()
                        join s in auth.student_courses on h.CO_Id equals s.Co_id_fk into StCoMasterJoin
                        from s in StCoMasterJoin.DefaultIfEmpty()
                        join d in auth.Course_Package on c.cp_id equals d.cp_id into CoursePackageJoin
                        from d in CoursePackageJoin.DefaultIfEmpty()
                        where (objStu.Stu_Inscode == "" && objStu.Stu_Copcode == "") ||
                              (a.Stu_Inscode == objStu.Stu_Inscode || a.Stu_Copcode == objStu.Stu_Copcode)
                        orderby a.Stu_Id descending
                        from e in auth.Upload_Videos
                        join f in auth.Course_Chapters on e.vi_id equals f.ch_vi_FK
                        orderby e.vi_id descending
                        select new getprofile
                        {
                            Stu_Name = a.Stu_Name,
                            ch_name = f.ch_name,
                            vi_name = e.vi_name,
                            vi_time = e.vi_time,
                            cu_name = d.cu_name // Use null conditional operator to avoid null reference exceptions
                        }).Distinct().ToListAsync();

                    return resultAll;
                }
                else
                {
                    return new List<getprofile>(); // Return an empty list when objStu is null or neither condition matches
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw; // Re-throwing the exception might not be ideal; handle it based on your application's error handling strategy
            }
        }

        //authentication ctrl
        public async Task<List<AuthUser_Details>> GetUser(string? roleaction, string? rolename, int? OfficeId)
        {
            try
            {
                if (roleaction == "All")
                {
                    var resultAll = (from d in auth.Users
                                     join e in auth.Roles on d.Role_Id_FK equals e.Id
                                     //join f in db.OfficeRoles on d.Id equals f.UserId
                                     where roleaction == "All" && e.Name != "Patient"
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
                var result = (from d in auth.Users
                              join e in auth.Roles on d.Role_Id_FK equals e.Id
                              join f in auth.OfficeRoles on d.Id equals f.UserId
                              where roleaction == "All" ? f.Id != 0 : f.OfficeId == OfficeId
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


                return await result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        //public async Task<UserRegisterResult> UserRegister(UserRegister userRegister)
        //{
        //    try
        //    {
        //        using (GlobalContext globalContext = new GlobalContext())
        //        {
        //            // Validate duplicate Phone Number
        //            var existingUserPhone = await this.auth.Users
        //                .FirstOrDefaultAsync(x => x.UserName == Convert.ToString(userRegister.Stu_MobileNumber));
        //            if (existingUserPhone != null)
        //            {
        //                return new UserRegisterResult { IsSuccess = false, Message = "PhoneNumber Already Exists" };
        //            }

        //            // Validate duplicate Email
        //            var existingUserEmail = await this.auth.Users
        //                .FirstOrDefaultAsync(x => x.Email == userRegister.Stu_Email);
        //            if (existingUserEmail != null)
        //            {
        //                return new UserRegisterResult { IsSuccess = false, Message = "Email Already Exists" };
        //            }

        //            // Generate Primary Key
        //            int id = await primarykeyvalue.primary_key("Student");
        //            string fileName = "default_user.png";

        //            // Create Auth User
        //            AuthUser objAuthUser = new AuthUser()
        //            {
        //                UserName = Convert.ToString(userRegister.Stu_MobileNumber),
        //                UserId = 0,
        //                FirstName = userRegister.Stu_Name,
        //                //DOB = userRegister.Stu_DOB,
        //                //Gender = userRegister.Stu_Gender,
        //                PhoneNumber = Convert.ToString(userRegister.Stu_MobileNumber),
        //                Role_Id_FK = "40ea3dcb-e728-4e1b-a42f-934977114b1a", // Student role
        //                Email = userRegister.Stu_Email,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                IsEnabled = false,
        //                Inactive = "N",
        //                Imagename = fileName,
        //                Id = Guid.NewGuid().ToString(),
        //                PhoneNumberConfirmed = false
        //            };

        //            var result = await userManager.CreateAsync(objAuthUser, userRegister.Password);

        //            // Create Student
        //            Student obj = new Student()
        //            {
        //                Stu_Id = id,
        //                Stu_UserID = objAuthUser.Id,
        //                //Stu_Inscode = userRegister.Stu_Inscode, // no validation
        //                Stu_Inscode = "",
        //                Stu_Copcode = "", // always empty
        //                Stu_Name = userRegister.Stu_Name,
        //                //Stu_DOB = userRegister.Stu_DOB,
        //                //Stu_Gender = userRegister.Stu_Gender,
        //                Stu_MobileNumber = userRegister.Stu_MobileNumber,
        //                Stu_Email = userRegister.Stu_Email,
        //                created_by = 1,
        //                created_date = DateTime.Now,
        //                delete_flag = false,
        //                Stu_status = 1,
        //                FK_StateId = userRegister.FK_StateId,
        //                FK_ZoneId = userRegister.FK_ZoneId,
        //                FK_DistrictId = userRegister.FK_DistrictId,
        //                FK_TestId = userRegister.FK_TestId,
        //            };

        //            await globalContext.Student.AddAsync(obj);
        //            await globalContext.SaveChangesAsync();

        //            return new UserRegisterResult { IsSuccess = true, Message = "User created successfully." };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new UserRegisterResult { IsSuccess = false, Message = $"User creation failed: {ex.Message}" };
        //    }
        //}

        //MCQ
        //profile picture
        // Working code 
        //public async Task<UserRegisterResult> UserRegister(UserRegister userRegister)
        //{
        //    try
        //    {
        //        using (GlobalContext globalContext = new GlobalContext())
        //        {
        //            // 1. Validate duplicate Phone Number
        //            var existingUserPhone = await this.auth.Users
        //            .FirstOrDefaultAsync(x => x.UserName == userRegister.Stu_MobileNumber);
        //            if (existingUserPhone != null)
        //                return new UserRegisterResult { IsSuccess = false, Message = "Phone Number Already Exists" };

        //            // 2. Validate duplicate Email
        //            var existingUserEmail = await this.auth.Users
        //            .FirstOrDefaultAsync(x => x.Email == userRegister.Stu_Email);
        //            if (existingUserEmail != null)
        //                return new UserRegisterResult { IsSuccess = false, Message = "Email Already Exists" };

        //            // 3. Generate Primary Key
        //            int id = await primarykeyvalue.primary_key("Student");

        //            // 4. Handle Profile Picture Upload
        //            string fileName = "default_user.png";
        //            if (userRegister.ProfilePicture != null && userRegister.ProfilePicture.Length > 0)
        //            {
        //                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", "ProfilePictures");
        //                if (!Directory.Exists(uploadsFolder))
        //                    Directory.CreateDirectory(uploadsFolder);

        //                fileName = $"{Guid.NewGuid()}{Path.GetExtension(userRegister.ProfilePicture.FileName)}";
        //                string filePath = Path.Combine(uploadsFolder, fileName);

        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await userRegister.ProfilePicture.CopyToAsync(stream);
        //                }
        //            }

        //            // 5. Create Auth User
        //            AuthUser objAuthUser = new AuthUser()
        //            {
        //                UserName = userRegister.Stu_MobileNumber,
        //                FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userRegister.Stu_Name.ToLower()),
        //                PhoneNumber = userRegister.Stu_MobileNumber,
        //                Role_Id_FK = "40ea3dcb-e728-4e1b-a42f-934977114b1a",
        //                Email = userRegister.Stu_Email,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                IsEnabled = false,
        //                Imagename = fileName,
        //                Id = Guid.NewGuid().ToString(),
        //                PhoneNumberConfirmed = false
        //            };

        //            var result = await userManager.CreateAsync(objAuthUser, userRegister.Password);
        //            if (!result.Succeeded)
        //            {
        //                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
        //                return new UserRegisterResult { IsSuccess = false, Message = $"User creation failed: {errors}" };
        //            }

        //            // 6. Create Student Entry
        //            Student obj = new Student()
        //            {
        //                Stu_Id = id,
        //                Stu_UserID = objAuthUser.Id,
        //                //Stu_Name = userRegister.Stu_Name,
        //                //Stu_Name = char.ToUpper(userRegister.Stu_Name[0]) + userRegister.Stu_Name.Substring(1).ToLower(),


        //                Stu_Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userRegister.Stu_Name.ToLower()),

        //                Stu_MobileNumber = userRegister.Stu_MobileNumber,
        //                Stu_Email = userRegister.Stu_Email,
        //                Stu_Photo = fileName, //  Save the uploaded filename
        //                created_by = 1,
        //                created_date = DateTime.Now,
        //                delete_flag = false,
        //                Stu_status = 1,
        //                FK_StateId = userRegister.FK_StateId,
        //                FK_ZoneId = userRegister.FK_ZoneId,
        //                FK_DistrictId = userRegister.FK_DistrictId,
        //                FK_TestId = userRegister.FK_TestId
        //            };

        //            await globalContext.Student.AddAsync(obj);
        //            await globalContext.SaveChangesAsync();

        //            string emailBody = $@"
        //        <html>
        //        <body style='font-family: Arial, sans-serif; color: #333;'>
        //        <h3 style='color: #2a8cff;'>TestNest - Student Registration</h3>
        //        <table border='1' cellspacing='0' cellpadding='8' style='border-collapse: collapse; width: 100%; border-color: #ddd;'>
        //        <tr>
        //        <th style='background-color:#f2f2f2; text-align:left;'>Field</th>
        //        <th style='background-color:#f2f2f2; text-align:left;'>Details</th>
        //        </tr>
        //        <tr>
        //        <td style='width:30%;'><b>Dear</b></td>
        //        <td>{userRegister.Stu_Name}</td>
        //        </tr>
        //        <tr>
        //        <td colspan='2' style='padding-top:10px;'>
        //        Thank you for registering with <b>TestNest</b>!<br/><br/>
        //        We’re excited to have you join our learning community.
        //        You can log in to your account and start exploring tests and resources by clicking the link below:<br/><br/>
        //        <a href='https://testnest.co.in/#/Login' style='color:#2a8cff; text-decoration:none; font-weight:bold;'>Go to Your Account</a><br/><br/>
        //        <b>Empowering Skills.</b><br/><br/>
        //        Best Regards,<br/>
        //        <b>The TestNest Team</b>
        //        </td>
        //        </tr>
        //        </table>
        //        </body>
        //        </html>";

        //            //live ALDA
        //            try
        //            {
        //                await _EMailService.SendEmailAsync_support(userRegister.Stu_Email, "enquiry@neurospineoptica.com", "TestNest - Student Registration", emailBody);
        //            }
        //            catch (Exception er)
        //            {
        //            }

        //            return new UserRegisterResult { IsSuccess = true, Message = "User created successfully." };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new UserRegisterResult { IsSuccess = false, Message = $"User creation failed: {ex.Message}" };
        //    }
        //}




        //public async Task<string> UserLogin(UserLogin login)
        //{

        //    //var UserReg = userManager.Users.FirstOrDefault(x => x.Email == login.User_Email || x.PhoneNumber == login.User_PhoneNumber);
        //    //if (UserReg == null)
        //    //{
        //    //    return "User not registered. Please register to log in.";
        //    //}

        //    var userLogin = userManager.Users.FirstOrDefault(x => x.Email == login.User_Email || x.PhoneNumber == login.User_PhoneNumber);

        //    if (userLogin == null)
        //    {
        //        return "Invalid Username or Email Address";
        //    }

        //    var result = await signInManager.PasswordSignInAsync(userLogin, login.Password, isPersistent: true, lockoutOnFailure: false);

        //    var isValidCredential = await signInManager.CheckPasswordSignInAsync(userLogin, login.Password, lockoutOnFailure: false);

        //    if (result.Succeeded)
        //    {
        //        // Authentication successful
        //        return "Login successful";
        //    }
        //    else
        //    {
        //        if (result.IsLockedOut)
        //        {
        //            return "Your Account is Locked,Contact System Admin";
        //        }
        //        if (result.IsNotAllowed)
        //        {
        //            return "Please Verify Email Address";
        //        }

        //        if (isValidCredential != null)
        //        {
        //            return "Invalid Password";
        //        }
        //        else
        //        {
        //            return "Login Failed";
        //        }

        //    }
        //}


        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {

            var user = userManager.Users.FirstOrDefault(x => x.Email == model.Username || x.PhoneNumber == model.Username);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with" + model.Username,
                };

            if (model.NewPassword != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Password doesn't match its confirmation",
                };
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            //var _dec = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //var decodedToken = WebEncoders.Base64UrlDecode(token);
            //string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                Message = "Password reset failed",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }
        public async Task<dynamic> VerifyOTP_ForgotPwd(VerifyOTP_ForgotPwd model)
        {


            if (model.user_type == "Institution")
            {
                var objInsId = await obj_FindUserId.FindInstitutionIdFromUserId(model.PhoneNumber);
                if (objInsId == 0)
                {
                    return new
                    {
                        Message = "Phonenumber not registered with Institution",
                        IsSuccess = false
                    };

                }

            }

            if (model.user_type == "Corporate")
            {
                var objCoId = await obj_FindUserId.FindCorporateIdFromUserId(model.PhoneNumber);
                if (objCoId == 0)
                {
                    return new
                    {
                        Message = "Phonenumber not registered with doctor",
                        IsSuccess = false
                    };
                }
            }
            if (model.user_type == "Individual")
            {
                var objIndId = await obj_FindUserId.FindIndividualIdFromUserId(model.PhoneNumber);
                if (objIndId == 0)
                {
                    return new
                    {
                        Message = "Phonenumber not registered with Individual",
                        IsSuccess = false
                    };
                }
            }

            if (model.user_type == "Student")
            {
                var objStuId = await obj_FindUserId.FindStudentIdFromUserId(model.PhoneNumber);
                if (objStuId == 0)
                {
                    return new
                    {
                        Message = "Phonenumber not registered with Student",
                        IsSuccess = false
                    };
                }
            }

            var objUserVerfiy = await this.auth.UserVerfication.FirstOrDefaultAsync(x => x.Id == model.Id && ((DateTime.Now.Second - x.OTPCreatedDate.Value.Second) + ((DateTime.Now.Minute - x.OTPCreatedDate.Value.Minute) * 60) <= ((x.ExpiryTime * 60) - 10)) && x.OTP == model.OTP);
            var user = userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
            if (objUserVerfiy == null)
            {
                return new
                {
                    Message = "Invalid OTP Expired",
                    IsSuccess = false,
                };
            }

            else if (user == null)
            {
                return new
                {
                    Message = "No user associated with this Mobile Number : " + model.PhoneNumber,
                    IsSuccess = false
                };
            }
            else
            {
                return new
                {
                    Message = "Valid OTP and Registered User",
                    IsSuccess = true
                };

            }
        }












        // This Code Is For The Courier Project









        //public async Task<UserRegisterResult> UserRegister(UserRegister userRegister)
        //{
        //    try
        //    {
        //        using (GlobalContext globalContext = new GlobalContext())
        //        {
        //            // ✅ Check duplicate Phone
        //            var existingUserPhone = await this.auth.Users
        //                .FirstOrDefaultAsync(x => x.UserName == Convert.ToString(userRegister.Cust_Mobile_Number));
        //            if (existingUserPhone != null)
        //            {
        //                return new UserRegisterResult { IsSuccess = false, Message = "Phone number already exists." };
        //            }

        //            // ✅ Check duplicate Email
        //            var existingUserEmail = await this.auth.Users
        //                .FirstOrDefaultAsync(x => x.Email == userRegister.Cust_Email_Id);
        //            if (existingUserEmail != null)
        //            {
        //                return new UserRegisterResult { IsSuccess = false, Message = "Email already exists." };
        //            }

        //            // ✅ Generate new PK
        //            int id = await primarykeyvalue.primary_key("UserCustomer");
        //            string fileName = "default_user.png";

        //            // ✅ Create Auth User
        //            AuthUser objAuthUser = new AuthUser()
        //            {
        //                UserName = Convert.ToString(userRegister.Cust_Mobile_Number),
        //                UserId = 0,
        //                FirstName = userRegister.Cust_Name,
        //                PhoneNumber = Convert.ToString(userRegister.Cust_Mobile_Number),
        //                Role_Id_FK = "40ea3dcb-e728-4e1b-a42f-934977114b1a",
        //                Email = userRegister.Cust_Email_Id,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                IsEnabled = false,
        //                Inactive = "N",
        //                Imagename = fileName,
        //                Id = Guid.NewGuid().ToString(),
        //                PhoneNumberConfirmed = false
        //            };

        //            var result = await userManager.CreateAsync(objAuthUser, userRegister.Password);

        //            if (!result.Succeeded)
        //            {
        //                return new UserRegisterResult
        //                {
        //                    IsSuccess = false,
        //                    Message = "User creation in Identity failed: " + string.Join(", ", result.Errors.Select(e => e.Description))
        //                };
        //            }

        //            // ✅ Create Customer record
        //            CustomerModel obj = new CustomerModel()
        //            {
        //                Cust_Id = id,
        //                Cust_Name = userRegister.Cust_Name,
        //                Cust_Email_Id = userRegister.Cust_Email_Id,
        //                Cust_Mobile_Number = userRegister.Cust_Mobile_Number,
        //                Cust_Photo = fileName,
        //                Cust_UserId = objAuthUser.Id,
        //                Cust_status = "A",
        //                create_by = "Admin",
        //                created_date = DateTime.Now,
        //                delete_flag = false
        //            };

        //            await globalContext.UserCustomer.AddAsync(obj);
        //            await globalContext.SaveChangesAsync();

        //            return new UserRegisterResult { IsSuccess = true, Message = "Customer registered successfully." };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var inner = ex.InnerException?.Message ?? ex.Message;
        //        return new UserRegisterResult { IsSuccess = false, Message = $"Customer registration failed: {inner}" };
        //    }
        //}



        public async Task<UserRegisterResult> UserRegister(UserRegister userRegister)
        {
            AuthUser objAuthUser = null;

            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {
                    // ✅ Check duplicate Phone
                    var existingUserPhone = await this.auth.Users
                        .FirstOrDefaultAsync(x => x.UserName == Convert.ToString(userRegister.Cust_Mobile_Number));
                    if (existingUserPhone != null)
                        return new UserRegisterResult { IsSuccess = false, Message = "Phone number already exists." };

                    // ✅ Check duplicate Email
                    var existingUserEmail = await this.auth.Users
                        .FirstOrDefaultAsync(x => x.Email == userRegister.Cust_Email_Id);
                    if (existingUserEmail != null)
                        return new UserRegisterResult { IsSuccess = false, Message = "Email already exists." };

                    // ✅ Generate new PK
                    int id = await primarykeyvalue.primary_key("UserCustomer");
                    string fileName = "default_user.png";

                    // ✅ Create Auth User
                    objAuthUser = new AuthUser()
                    {
                        UserName = Convert.ToString(userRegister.Cust_Mobile_Number),
                        FirstName = userRegister.Cust_Name,
                        PhoneNumber = Convert.ToString(userRegister.Cust_Mobile_Number),
                        Role_Id_FK = "40ea3dcb-e728-4e1b-a42f-934977114b1a",//Customer Role id
                        Email = userRegister.Cust_Email_Id,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsEnabled = false,
                        Inactive = "N",
                        Imagename = fileName,
                        Id = Guid.NewGuid().ToString(),
                        PhoneNumberConfirmed = false
                    };

                    var result = await userManager.CreateAsync(objAuthUser, userRegister.Password);

                    if (!result.Succeeded)
                    {
                        return new UserRegisterResult
                        {
                            IsSuccess = false,
                            Message = "User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description))
                        };
                    }

                    // ✅ Create Customer record
                    CustomerModel obj = new CustomerModel()
                    {
                        Cust_Id = id,
                        Cust_Name = userRegister.Cust_Name,
                        Cust_Email_Id = userRegister.Cust_Email_Id,
                        Cust_Mobile_Number = userRegister.Cust_Mobile_Number,
                        Cust_Photo = fileName,
                        Cust_UserId = objAuthUser.Id,
                        Cust_status = "A",
                        create_by = "Admin",
                        created_date = DateTime.Now,
                        delete_flag = false
                    };

                    await globalContext.UserCustomer.AddAsync(obj);
                    await globalContext.SaveChangesAsync();

                    return new UserRegisterResult { IsSuccess = true, Message = "Customer registered successfully." };
                }
            }
            catch (Exception ex)
            {
                // ✅ Rollback Auth User if customer creation failed
                if (objAuthUser != null)
                {
                    var userInDb = await userManager.FindByIdAsync(objAuthUser.Id);
                    if (userInDb != null)
                    {
                        await userManager.DeleteAsync(userInDb);
                    }
                }

                var inner = ex.InnerException?.Message ?? ex.Message;
                return new UserRegisterResult { IsSuccess = false, Message = $"Customer registration failed: {inner}" };
            }
        }


        public async Task<string> UserLogin(UserLogin login)
        {

            //var UserReg = userManager.Users.FirstOrDefault(x => x.Email == login.User_Email || x.PhoneNumber == login.User_PhoneNumber);
            //if (UserReg == null)
            //{
            //    return "User not registered. Please register to log in.";
            //}

            var userLogin = userManager.Users.FirstOrDefault(x => x.Email == login.User_Email || x.PhoneNumber == login.User_PhoneNumber);

            if (userLogin == null)
            {
                return "Invalid Username or Email Address";
            }

            var result = await signInManager.PasswordSignInAsync(userLogin, login.Password, isPersistent: true, lockoutOnFailure: false);

            var isValidCredential = await signInManager.CheckPasswordSignInAsync(userLogin, login.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Authentication successful
                return "Login successful";
            }
            else
            {
                if (result.IsLockedOut)
                {
                    return "Your Account is Locked,Contact System Admin";
                }
                if (result.IsNotAllowed)
                {
                    return "Please Verify Email Address";
                }

                if (isValidCredential != null)
                {
                    return "Invalid Password";
                }
                else
                {
                    return "Login Failed";
                }

            }
        }

        // PartnerRegister
        public async Task<UserRegisterResult> PartnerRegister(PartnerRegister partnerRegister)
        {
            AuthUser objAuthUser = null;

            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {
                    // ✅ Check duplicate Phone
                    var existingUserPhone = await this.auth.Users
                        .FirstOrDefaultAsync(x => x.UserName == Convert.ToString(partnerRegister.PartMobileNumber));
                    if (existingUserPhone != null)
                        return new UserRegisterResult { IsSuccess = false, Message = "Phone number already exists." };

                    // ✅ Check duplicate Email
                    var existingUserEmail = await this.auth.Users
                        .FirstOrDefaultAsync(x => x.Email == partnerRegister.PartEmail);
                    if (existingUserEmail != null)
                        return new UserRegisterResult { IsSuccess = false, Message = "Email already exists." };

                    // ✅ Generate new PK
                    int id = await primarykeyvalue.primary_key("Partner");
                    string fileName = "default_user.png";

                    // ✅ Create Auth User
                    objAuthUser = new AuthUser()
                    {
                        UserName = Convert.ToString(partnerRegister.PartMobileNumber),
                        FirstName = partnerRegister.PartnerName,
                        PhoneNumber = Convert.ToString(partnerRegister.PartMobileNumber),
                        Role_Id_FK = "46c1e21c-e75d-401d-8285-c65fb6a49185", // Partner Role Id (Change this as per your DB)
                        Email = partnerRegister.PartEmail,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsEnabled = false,
                        Inactive = "N",
                        Imagename = fileName,
                        Id = Guid.NewGuid().ToString(),
                        PhoneNumberConfirmed = false
                    };

                    var result = await userManager.CreateAsync(objAuthUser, partnerRegister.Password);

                    if (!result.Succeeded)
                    {
                        return new UserRegisterResult
                        {
                            IsSuccess = false,
                            Message = "User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description))
                        };
                    }

                    // ✅ Create Partner record
                    Partner obj = new Partner()
                    {
                       // PartnerCode = "PRT" + id.ToString("D5"), // Example: PRT00001
                        PartnerName = partnerRegister.PartnerName,                       
                        PartMobileNumber = partnerRegister.PartMobileNumber,
                        PartEmail = partnerRegister.PartEmail,
                        //PartGender = partnerRegister.PartGender,
                        //PartAddress = partnerRegister.PartAddress,
                        //PartCity = partnerRegister.PartCity,
                        //PartState = partnerRegister.PartState,
                        //PartZipcode = partnerRegister.PartZipcode,
                        //PartLicenseNumber = partnerRegister.PartLicenseNumber,
                        //PartLicenseExpiryDate = partnerRegister.PartLicenseExpiryDate,
                        //PartIDNumber = partnerRegister.PartIDNumber,
                        //PartVehicleType = partnerRegister.PartVehicleType,
                        //PartVehicleNumber = partnerRegister.PartVehicleNumber,
                        Part_UserId = objAuthUser.Id,
                        Inactive = 'N',
                        CreatedBy = "Admin",
                        CreatedDate = DateTime.Now
                    };

                    await globalContext.Partner.AddAsync(obj);
                    await globalContext.SaveChangesAsync();

                    
                    return new UserRegisterResult { IsSuccess = true, Message = "Partner registered successfully." };
                }
            }
            catch (Exception ex)
            {
                // ✅ Rollback Auth User if partner creation failed
                if (objAuthUser != null)
                {
                    var userInDb = await userManager.FindByIdAsync(objAuthUser.Id);
                    if (userInDb != null)
                        await userManager.DeleteAsync(userInDb);
                }

                var inner = ex.InnerException?.Message ?? ex.Message;
                return new UserRegisterResult { IsSuccess = false, Message = $"Partner registration failed: {inner}" };
            }
        }


        public async Task<List<UserWithRoleViewModel>> GetAllUsersWithRoles()
        {
            var userList = new List<UserWithRoleViewModel>();

            using (var connection = auth.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "usp_GetAllUsersWithRoles";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userList.Add(new UserWithRoleViewModel
                            {
                                Id = reader["Id"]?.ToString(),
                                FirstName = reader["FirstName"]?.ToString(),
                                Email = reader["Email"]?.ToString(),
                                UserName = reader["UserName"]?.ToString(),
                                PhoneNumber = reader["PhoneNumber"]?.ToString(),
                                UserId = reader["UserId"]?.ToString(),
                                Role_Id_FK = reader["Role_Id_FK"]?.ToString(),              
                                Inactive = reader["Inactive"]?.ToString(),
                                RoleName = reader["RoleName"]?.ToString()
                            });
                        }
                    }
                }
            }

            return userList;
        }

    }
}
