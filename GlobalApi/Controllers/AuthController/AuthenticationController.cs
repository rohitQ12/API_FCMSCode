using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.Repository.AuthRepository;
using GlobalApi.GlobalClasses;
using Microsoft.AspNetCore.Authorization;
using GlobalApi.Models.Authentication;
using System.Net;
using GlobalApi.IRepository.AuthIRepository;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Linq;
using MaxMind.GeoIP2;
using GlobalApi.Models.Master;
using GlobalApi.IRepository.MasterIRepository;
using Microsoft.AspNetCore.Identity;
using GlobalApi.Data;
using GlobalApi.Repository.MasterRepository;
using NLog;

namespace GlobalApi.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public readonly IAuthenticationRepository _repository;
        // public readonly PatientRepository patient;
        //public readonly DoctorRepository doctor;
        //public readonly HospitalRepository hospital;
        //  public readonly PharmacyRepository pharmacy;
        //  public readonly DiagnosticCentersRepository diagnosticCenters;
        private IEMailService _EMailService;
        private IHttpContextAccessor _accessor;
        public readonly FindUserId findUserId;
        private readonly UserManager<AuthUser> userManager;
        private readonly RoleManager<AspNetRole> roleManager;
        private readonly GlobalContext auth = null!;
        private SignInManager<AuthUser> signInManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AuthenticationController(IHttpContextAccessor accessor, IConfiguration configuration,
            IAuthenticationRepository repository,
            IEMailService EMailService,
            UserManager<AuthUser> userManager,
            RoleManager<AspNetRole> roleManager, SignInManager<AuthUser> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.auth = new GlobalContext();
            this._configuration = configuration;
            this._EMailService = EMailService;
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._accessor = accessor;
            // this.patient = new PatientRepository();
            // this.doctor = new DoctorRepository(_webHostEnvironment = webHostEnvironment);
            this.findUserId = new FindUserId();
            this.signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.RegisterUserAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                    //if (result.Message == "User created successfully!")
                    //    return Ok(result); // Status Code: 200 
                    //else
                    //    return BadRequest(result.Message);
                }
                return BadRequest(result.Message);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [AllowAnonymous]
        [HttpPost, Route("ExternalRegister")]
        public async Task<IActionResult> Register([FromBody] SelfRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.ExtRegisterUserAsync(model.Firstname, model.Lastname,
                    model.Phonenumber, model.Email, model.Password, "f8bfd5b9-0d17-4617-98c6-2fdd7f85ef3a", 0, "");

                if (result.IsSuccess)
                    return Ok(result.Message); // Status Code: 200 

                return BadRequest(result.Message);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }


        // used for mobile app, because it is required message in response
        //[AllowAnonymous]
        //[HttpPost, Route("PatientRegister_Mobile")]
        //public async Task<IActionResult> Register_Mobile([FromBody] PatientReg model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.Role_ID = "ff613dc4-042a-4167-bc9b-22cdf3fffabc";
        //        var result = await this._repository.PatientRegister(model);

        //        if (result.IsSuccess)
        //        {
        //            return Ok(result);
        //        }
        //        return BadRequest(result);
        //    }
        //    return BadRequest("Some properties are not valid"); // Status code: 400
        //}

        ////Online Portals
        //[AllowAnonymous]
        //[HttpPost, Route("PatientRegister_Online")]
        //public async Task<IActionResult> PatRegister_Online([FromBody] PatientReg_Online model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.Role_ID = "ff613dc4-042a-4167-bc9b-22cdf3fffabc";
        //        var result = await this._repository.PatientRegister_Online(model);

        //        if (result.is_success)
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(result);
        //    }
        //    return BadRequest("Some properties are not valid"); // Status code: 400
        //}


        //[AllowAnonymous]
        //[HttpPost, Route("DoctorRegister")]
        //public async Task<IActionResult> DoctorRegister([FromBody] DoctorReg model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.Role_ID = "7711a4e3-989b-4bb3-af56-c0b48c19996d";
        //        model.DO_Type = "Clinic";
        //        var result = await this._repository.DoctorRegister(model);

        //        if (result.IsSuccess)
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(result);
        //    }

        //    return BadRequest("Some properties are not valid"); // Status code: 400
        //}

        // used for mobile app, because it is required message in response
        //[AllowAnonymous]
        //[HttpPost, Route("DoctorRegister_Mobile")]
        //public async Task<IActionResult> DoctorRegister_Mobile([FromBody] DoctorReg model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.Role_ID = "7711a4e3-989b-4bb3-af56-c0b48c19996d";
        //        model.DO_Type = "Clinic";
        //        var result = await this._repository.DoctorRegister(model);

        //        if (result.IsSuccess)
        //        {
        //            return Ok(result);
        //        }
        //        return BadRequest(result);
        //    }

        //    return BadRequest("Some properties are not valid"); // Status code: 400
        //}

        //sujata
        ////[AllowAnonymous]
        ////[HttpPost, Route("DoctorRegister_Online")]
        ////public async Task<IActionResult> DoctorRegister_Online([FromBody] DoctorReg_Online model)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        model.Role_ID = "7711a4e3-989b-4bb3-af56-c0b48c19996d";
        ////        var result = await this._repository.DoctorRegister_Online(model);

        ////        if (result.is_success)
        ////        {
        ////            return Ok();
        ////        }
        ////        return BadRequest(result);
        ////    }

        ////    return BadRequest("Some properties are not valid"); // Status code: 400
        ////}


        ////clinic
        //[AllowAnonymous]
        //[HttpPost, Route("ClinicDoctorRegister")]
        //public async Task<IActionResult> ClinicDoctorRegister([FromBody] DoctorReg model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.Role_ID = "7711a4e3-989b-4bb3-af56-c0b48c19996d";
        //        model.DO_Type = "Clinic";
        //        var result = await this._repository.DoctorRegister(model);

        //        if (result.IsSuccess)
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(result);
        //    }

        //    return BadRequest("Some properties are not valid"); 
        //}
        ////hospital
        //[AllowAnonymous]
        //[HttpPost, Route("HospitalRegister")]
        //public async Task<IActionResult> HospitalRegister([FromBody] HospitalReg model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.Role_ID = "ff613dc4-042a-4167-bc9b-22cdf3fffabd";
        //        var result = await this._repository.HospitalRegister(model);
        //        if (result.IsSuccess)
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(result);
        //    }
        //    return BadRequest("Some properties are not valid"); 
        //}
        ////speciality hospital
        //[AllowAnonymous]
        //[HttpPost, Route("SpecialityHospitalRegister")]
        //public async Task<IActionResult> SpecialityHospitalRegister([FromBody] HospitalReg model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await this._repository.ExtRegisterUserAsync(model.Hos_HospitalName, model.OwnerName,
        //            Convert.ToString(model.Hos_HospitalPhoneNo), model.Hos_HospitalEmail, model.Password,
        //            "ff613dc4-042a-4167-bc9b-22cdf3fffabd", model.Id, model.OTP);

        //        if (result.IsSuccess)
        //        {
        //            if (model.Hos_Type_Id == 2)
        //            {
        //                var hospital = await this.hospital.InsertHospital(model);
        //                if (hospital == "Hospital Added Successfully")
        //                {
        //                    return Ok(result);
        //                }
        //                return BadRequest(hospital);
        //            }
        //        }
        //        return BadRequest(result);
        //    }
        //    return BadRequest("Some properties are not valid");
        //}
        ////pharmacy
        //[AllowAnonymous]
        //[HttpPost, Route("PharmacyRegister")]
        //public async Task<IActionResult> PharmacyRegister([FromBody] PharmacyReg model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.Role_ID = "ff613dc4-042a-4167-bc9b-22cdf3fffabf";
        //        var result = await this._repository.PharmacyRegister(model);
        //        if (result.IsSuccess)
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(result);
        //    }
        //    return BadRequest("Some properties are not valid");
        //}
        ////Diagnostic Center
        //[AllowAnonymous]
        //[HttpPost, Route("DiagCenterRegister")]
        //public async Task<IActionResult> DiagCenterRegister([FromBody] DiagnosticReg model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.Role_ID = "7711a4e3-989b-4bb3-af56-c0b48c19910d";
        //        var result = await this._repository.DiagCenterRegister(model);
        //        if (result.IsSuccess)
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(result);
        //    }
        //    return BadRequest("Some properties are not valid");
        //}

        [HttpPut, Route("Update")]
        public async Task<IActionResult> Update([FromBody] RegisterBindingModel model)
        {
            //string userName = User.Identity.Name.ToString();
            if (ModelState.IsValid)
            {
                var result = await this._repository.UpdateUserAsync(model, model.UserId);

                if (result != false)
                {
                    return Ok(result);
                }
                return Unauthorized(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpDelete, Route("Delete")]
        public async Task<IActionResult> Delete(string UserId)
        {
            //string userName = User.Identity.Name.ToString();
            if (ModelState.IsValid)
            {
                var result = await this._repository.DeleteUserAsync(UserId);

                if (result != false)
                {
                    return Ok(result);
                }

                return Unauthorized(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpGet, Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await this._repository.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {
                return Redirect($"{_configuration["AppUrl"]}ConfirmEmail.html");
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost, Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (model.Username == null)
                return NotFound();

            var result = await this._repository.ResetPasswordAsync(model);

            if (result.IsSuccess)
                return Ok(result); // 200

            return BadRequest(result); // 400
        }


        [AllowAnonymous]
        [HttpGet, Route("Verification")]
        public IActionResult Get(string data)
        {
            if (string.IsNullOrEmpty(data))
                return NotFound();

            var result = this._repository.Userverification(data);

            if (result == true)
                return Ok(result); // 200

            return BadRequest(result); // 400
        }

        [AllowAnonymous]
        [HttpGet, Route("Google_Login")]
        public async Task<IActionResult> GoogleLogin(string accesstoken)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.ForGoogle(accesstoken);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [AllowAnonymous]
        [HttpPost, Route("Facebook_Login")]
        public async Task<IActionResult> FacebookLogin(string accesstoken)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.ForFacebook(accesstoken);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpPut, Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {

            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name.ToString();
                var result = await this._repository.ChangePasswordAsync(model, userName);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }


        [HttpGet, Route("ActivateInactivate")]
        public async Task<IActionResult> ActivateInactivate(string userid)
        {
            if (userid != null)
            {
                var result = await this._repository.ActivateInactivate(userid);
                //if (result)
                return Ok(result); // Status Code: 200 

                //return BadRequest("The Role you have entered already exists");
            }
            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [HttpPut, Route("Test")]
        public ActionResult get()
        {
            var result = System.IO.File.ReadAllBytes(("wwwroot/Images/" + "08132e2d-8c2f-4417-b6eb-9488ccf0c88a_OIP.jpg"));
            return Ok(result);
        }

        [HttpGet("Logout")]
        public async Task<ActionResult> Logout()
        {
            logger.Info("Username" + User.Identity.Name + "AuthenticationController -- >");
            await this.signInManager.SignOutAsync();
            logger.Debug("GetAllState : " + User.Identity.Name + " StateController:Aprslcyclemap : Start ->");
            return Ok();
        }

        [HttpPut, Route("UserApprove")]
        public async Task<ActionResult> ApproveUser(string userid, string? Remarks)
        {
            if (userid != null)
            {
                var result = await this._repository.ApproveUser(userid, Remarks);
                if (result)
                    return Ok(result);

                return BadRequest("The Role you have entered already exists");
            }
            return BadRequest("Some properties are not valid");
        }
        [HttpGet, Route("SendOTP/{Phonenumber}/{TemplateId}")]
        public async Task<ActionResult> SendOTP(string Phonenumber, string TemplateId)
        {
            var result = await this._repository.SendOTP(Phonenumber, TemplateId);
            return Ok(result);
        }

        [HttpGet, Route("SendOTP_Reg/{Phonenumber}/{TemplateId}")]
        public async Task<ActionResult> SendOTP_Reg(string Phonenumber, string TemplateId)
        {
            try
            {

                if (Phonenumber == null || Phonenumber == "0000000000")
                {
                    return BadRequest("Phone number is required.");
                }
                if (TemplateId == null)
                {
                    return BadRequest("Template id is required");
                }
                var result = await this._repository.SendOTP_Reg(Phonenumber, TemplateId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //saheb online for both online and portal
        [HttpGet, Route("SendOTP_ForgotPwd/{Phonenumber}/{TemplateId}")]
        public async Task<ActionResult> SendOTP_ForgotPwd(string Phonenumber, string TemplateId)
        {
            try
            {

                if (Phonenumber == null || Phonenumber == "")
                {
                    return BadRequest("Phone Number is required");
                }
                if (TemplateId == null || TemplateId == "")
                {
                    return BadRequest("Template Id is required");
                }
                var result = await this._repository.SendOTP_ForgotPassword(Phonenumber, TemplateId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //saheb online for verify otp for changing password both for online and portal
        [AllowAnonymous]
        [HttpPost, Route("VerifyOTP_ForgotPwd")]
        public async Task<IActionResult> VerifyOTP_ForgotPwd([FromBody] VerifyOTP_ForgotPwd model)
        {
            try
            {
                if (model.PhoneNumber == null || model.PhoneNumber == "")
                {
                    return BadRequest("Phone number is required");
                }
                if (model.Id == 0)
                {
                    return BadRequest("Verify Id is required");
                }
                if (model.OTP == null || model.OTP == "")
                {
                    return BadRequest("OTP is required");
                }

                var result = await this._repository.VerifyOTP_ForgotPwd(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //saheb online reset password for both online and portal
        [AllowAnonymous]
        [HttpPost, Route("ResetPassword_Online")]
        public async Task<IActionResult> ForgetPassword_Online([FromBody] ResetPasswordViewModel_Online model)
        {
            try
            {
                if (model.PhoneNumber == null || model.PhoneNumber == "")
                {
                    return BadRequest("Phone number is required");
                }
                if (model.Id == 0)
                {
                    return BadRequest("Verify Id is required");
                }
                if (model.OTP == null || model.OTP == "")
                {
                    return BadRequest("OTP is required");
                }
                if (model.NewPassword == null || model.NewPassword == "")
                {
                    return BadRequest("Password is required");
                }
                if (model.ConfirmPassword == null || model.ConfirmPassword == "")
                {
                    return BadRequest("Confirm Password is required");
                }

                var result = await this._repository.ResetPasswordAsync_Online(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        //saheb get role id based on user name/mobile number
        [AllowAnonymous]
        [HttpGet, Route("GetRoleId_Mobile/{Phonenumber}")]
        public async Task<IActionResult> GetRoleId_MobileApp(string Phonenumber)
        {
            try
            {
                string roleId = string.Empty;
                string rolename = string.Empty;

                if (Phonenumber == null || Phonenumber == "")
                {
                    return BadRequest("Phone number is required");
                }
                var result = await this._repository.GetRoleId_MobileApp(Phonenumber);
                if (result.is_success)
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet, Route("GetUsersByNumber")]
        public async Task<ActionResult> GetUsersByNumber(string userName)
        {
            //var userName = "8778650328";
            //var userName = Convert.ToString(User.Identity.Name);
            if (userName == null)
            {
                return Unauthorized();
            }
            var result = await this._repository.GetUsersByNumber(userName);
            return Ok(result);
        }



        [HttpGet, Route("GetUserByname")]
        public async Task<ActionResult> GetUserByname()
        {
            // var userName = "8778650328";
            var userName = Convert.ToString(User.Identity.Name);
            if (userName == null)
            {
                return Unauthorized();
            }
            var result = await this._repository.GetUserByname(userName);
            return Ok(result);
        }

        [HttpGet, Route("GetUserCourseByname")]
        public async Task<ActionResult> GetUserCourseByname(string userName)
        {
            // var userName = "6363731313";
            // var userName = Convert.ToString(User.Identity.Name);
            if (userName == null)
            {
                return Unauthorized();
            }
            //var userName = User.Identity.Name.ToString();

            var result = await this._repository.GetUserCourseByname(userName);
            return Ok(result);
        }


        [HttpGet, Route("GetProfile")]
        public async Task<ActionResult> GetProfile()
        {
            var userName = User.Identity.Name.ToString();
            //var userName = "8454521212";
            var result = await this._repository.GetProfile(userName);
            return Ok(result);
        }
        [HttpPut, Route("UpdateUserProfile")]
        public async Task<ActionResult> UpdateUserProfile([FromForm] AuthUser_Details userProfile)
        {
            var userName = User.Identity.Name.ToString();
            var rolename = await this.findUserId.FindRoleNameFromUserName(userName);
            var change = await _repository.UpdateUserProfile(userProfile, rolename);

            if (change == "User Updated successfully")
            {
                return Ok();
            }
            return BadRequest(change);
        }

        [HttpGet, Route("GetAllUser")]
        public async Task<ActionResult> GetAllUser()
        {
            var userName = "8778650328"; // admin
                                         //  var userName = Convert.ToString(User.Identity.Name);
            if (userName == null)
            {
                return Unauthorized();
            }


            var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
            var rolename = await this.findUserId.FindRoleNameFromUserName(userName);
            var OfficeId = await this.findUserId.FindOfficeIdFromUserNames(userName);
            var result = await this._repository.GetUser(roleaction, rolename, OfficeId);
            return Ok(result);
        }

        //[AllowAnonymous]
        //[HttpPost, Route("UserRegister")]
        //public async Task<IActionResult> UserRegister([FromBody] UserRegister userRegister)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _repository.UserRegister(userRegister);

        //        if (result.IsSuccess)
        //        {
        //            return Ok(new { message = result.Message });
        //        }
        //        else
        //        {
        //            return BadRequest(new { message = result.Message });
        //        }
        //    }

        //    return BadRequest("Some properties are not valid"); // Status code: 400
        //}



        //profile picture
        //[AllowAnonymous]
        //[HttpPost, Route("UserRegister")]
        //public async Task<IActionResult> UserRegister([FromForm] UserRegister userRegister)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _repository.UserRegister(userRegister);

        //        if (result.IsSuccess)
        //        {
        //            return Ok(new { message = result.Message });
        //        }
        //        else
        //        {
        //            return BadRequest(new { message = result.Message });
        //        }
        //    }

        //    return BadRequest("Some properties are not valid"); // Status code: 400
        //}

        //public async Task<IActionResult> UserRegister([FromBody] UserRegister userRegister)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // studentReg.Role_ID = "ff613dc4-042a-4167-bc9b-22cdf3fffabc";
        //        var result = await this._repository.UserRegister(userRegister);

        //        if (result == "Registration Successful")
        //        {
        //            return Ok(result);
        //        }
        //        return BadRequest(result);
        //    }
        //    return BadRequest("Some properties are not valid"); // Status code: 400
        //}



        //[AllowAnonymous]
        //[HttpPost, Route("UserLogin")]
        //public async Task<IActionResult> UserLogin([FromBody] UserLogin model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // model.Role_ID = "ff613dc4-042a-4167-bc9b-22cdf3fffabc";
        //        var result = await this._repository.UserLogin(model);




        //        if (result== "Login successful")
        //        {
        //            return Ok(result);
        //        }
        //        return BadRequest(result);
        //    }
        //    return BadRequest("Some properties are not valid"); // Status code: 400
        //}

        [HttpPost, Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        //saheb
        [HttpGet("api/test-user")]
        public async Task<IActionResult> GetTestUser()
        {
            try
            {
                var user = await _repository.GetUsersByNumber("8778650328");

                if (user == null)
                    return NotFound("User not found");

                return Ok(new
                {
                    user.UserName,
                    user.Email
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }







        // This Code Is For The Courier Project







        // Register For Customer 
        [AllowAnonymous]
        [HttpPost, Route("UserRegister")]
        public async Task<IActionResult> UserRegister([FromForm] UserRegister userRegister)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.UserRegister(userRegister);

                if (result.IsSuccess)
                {
                    return Ok(new { message = result.Message });
                }
                else
                {
                    return BadRequest(new { message = result.Message });
                }
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }



        [AllowAnonymous]
        [HttpPost, Route("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] UserLogin model)
        {
            if (ModelState.IsValid)
            {
                // model.Role_ID = "ff613dc4-042a-4167-bc9b-22cdf3fffabc";
                var result = await this._repository.UserLogin(model);




                if (result == "Login successful")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        // Register For Partner 
        [AllowAnonymous]
        [HttpPost, Route("PartnerRegister")]
        public async Task<IActionResult> PartnerRegister([FromForm] PartnerRegister partnerRegister)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.PartnerRegister(partnerRegister);

                if (result.IsSuccess)
                    return Ok(new { message = result.Message });
                else
                    return BadRequest(new { message = result.Message });
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [HttpGet("GetAllUsersWithRoles")]
        public async Task<IActionResult> GetAllUsersWithRoles()
        {
            var result = await _repository.GetAllUsersWithRoles();

            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No users found." });
            }

            return Ok(result);
        }


    }
}
