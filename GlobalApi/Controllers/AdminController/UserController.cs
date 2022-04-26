using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.Data;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.IRepository.MasterIRepository;

namespace GlobalApi.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserRepository _repository;
        public readonly IPatient patient;

        public UserController(IUserRepository repository)
        {
            this._repository = repository;
            this.patient = new PatientRepository();
        }
        [AllowAnonymous]
        [HttpGet, Route("GetAllUser")]
        public async Task<ActionResult<IEnumerable<AuthUser_Details>>> GetAllUser()
        {
            try
            {
                var result = await this._repository.GetUser();
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet, Route("GetProfile_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Images/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }
        //[Authorize]
        [HttpGet, Route("GetUserByname")]
        public async Task<ActionResult<IEnumerable<AuthUser_Details>>> GetUserByname()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var result = await this._repository.GetUserByname(userName);
                if (result!=null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut, Route("UpdateUserProfile")]
        public async Task<ActionResult<AuthUser_Details>> UpdateUserProfile([FromForm] AuthUser_Details userProfile)
        {
            if (userProfile == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateUserProfile(userProfile.Id, userProfile.Image,
            userProfile.Email, userProfile.PhoneNumber, userProfile.FirstName, userProfile.LastName, userProfile.Gender, userProfile.DOB);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdatePatientProfile")]
        public async Task<ActionResult<AuthUser_Details>> UpdatePatientProfile([FromForm] Patient_Images PatientProfile)
        {
            if (PatientProfile == null)
            {
                return BadRequest();
            }

            var UserProfile = await _repository.UpdateUserProfile(PatientProfile.UserID, PatientProfile.PR_Photo,
            PatientProfile.PR_Email, PatientProfile.PR_MobileNumber, PatientProfile.PR_FirstName, PatientProfile.PR_LastName, PatientProfile.PR_Gender, PatientProfile.PR_DOB);

            if (UserProfile != null)
            {
                var Patient = await patient.UpdatePatient(PatientProfile);
                if (Patient != null)
                {
                    return Ok();
                }
                return BadRequest("Not successfull");
            }
                
            else
                return BadRequest("Not successfull");
        }


    }
}
