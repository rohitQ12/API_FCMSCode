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
        public readonly PatientRepository patient;
        private IEMailService _EMailService;
        private IHttpContextAccessor _accessor;
        public readonly FindUserId findUserId;
        private readonly UserManager<AuthUser> userManager;
        private readonly RoleManager<AspNetRole> roleManager;
        private readonly GlobalContext auth = null!;
        private SignInManager<AuthUser> signInManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public AuthenticationController(IHttpContextAccessor accessor,IConfiguration configuration, 
            IAuthenticationRepository repository, 
            IEMailService EMailService, 
            UserManager<AuthUser> userManager,
            RoleManager<AspNetRole> roleManager, SignInManager<AuthUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.auth = new GlobalContext();
            this._configuration = configuration;
            this._EMailService = EMailService;
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._accessor = accessor;
            this.patient = new PatientRepository();
            this.findUserId = new FindUserId();
            this.signInManager = signInManager;
        }
        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.RegisterUserAsync(model.Firstname, model.Lastname, 
                    model.Phonenumber, model.Email, model.Password, model.RoleId,model.OfficeId,null);

                if (result.IsSuccess)
                {
                    if(result.Message =="User created successfully!")
                        return Ok(result); // Status Code: 200 
                    else
                       return BadRequest(result.Message);
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
                    model.Phonenumber, model.Email, model.Password, "f8bfd5b9-0d17-4617-98c6-2fdd7f85ef3a");

                if (result.IsSuccess)
                    return Ok(result.Message); // Status Code: 200 

                return BadRequest(result.Message);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [AllowAnonymous]
        [HttpPost, Route("PatientRegister")]
        public async Task<IActionResult> Register([FromBody] PatientReg model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.ExtRegisterUserAsync(model.PR_FirstName, model.PR_LastName, model.PR_MobileNumber, model.PR_Email, model.Password, "ff613dc4-042a-4167-bc9b-22cdf3fffabc");

                if (result.IsSuccess)
                {
                    string Username = User.Identity.Name;
                    string Create_by = await this.findUserId.FindIdFromUserName(Username);
                    var UserId = await findUserId.FindPatientIdFromUserEmaiOrNumber(model.PR_Email, model.PR_MobileNumber);
                    var patient = await this.patient.InsertPatient(model, UserId, Create_by);
                    return Ok(result); // Status Code: 200 
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }


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
                return Redirect($"{_configuration["AppUrl"]}/ConfirmEmail.html");
            }
            return BadRequest(result);
        }
        
        [AllowAnonymous]
        [HttpPost, Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (model.Username==null)
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

            if (result==true)
                return Ok(result); // 200

            return BadRequest(result); // 400
        }


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
                var result = await this._repository.ChangePasswordAsync(model);

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
        public async Task<ActionResult> ApproveUser(string userid , string? Remarks)
        {
            if (userid != null)
            {
                var result = await this._repository.ApproveUser(userid,Remarks);
                if (result)
                    return Ok(result);

                return BadRequest("The Role you have entered already exists");
            }
            return BadRequest("Some properties are not valid");
        }
    }
}
