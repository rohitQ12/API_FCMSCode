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
using GlobalApi.GlobalClasses;
using Newtonsoft.Json;

namespace GlobalApi.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserRepository _repository;
      //  public readonly IPatient patient;
        //public readonly IDoctor doctor;
        public readonly FindUserId findUserId;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUserRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            this._repository = repository;
            //this.patient = new PatientRepository();
            this.findUserId = new FindUserId();
           // this.doctor = new DoctorRepository(_webHostEnvironment = webHostEnvironment);
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]
        [HttpGet, Route("GetAllUser")]
        public async Task<ActionResult<IEnumerable<AuthUser_Details>>> GetAllUser()
        {
            try
            {
                
                var userName =Convert.ToString(User.Identity.Name);
                if(userName == null)
                {
                    return Unauthorized();
                }
                
                //var userName = "8095118991"; //admin
                //var userName = "9986630000"; // hospital admin
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var rolename = await this.findUserId.FindRoleNameFromUserName(userName);
                var OfficeId = await this.findUserId.FindOfficeIdFromUserNames(userName);
                var result = await this._repository.GetUser(roleaction, rolename, OfficeId);
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
                if (result != null)
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

        [HttpGet, Route("GetUserByname_New")]
        public async Task<ActionResult<IEnumerable<AuthUser_Details_New>>> GetUserByname_New()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                if (userName == null || userName == "")
                {
                    return Unauthorized();
                }
                var result = await this._repository.GetUserByname_New(userName);
                if (result != null)
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
            var files = Request.Form.Files;
            var change = await _repository.UpdateUserProfile(userProfile.Id, userProfile.Image,
            userProfile.Email, userProfile.PhoneNumber, userProfile.FirstName, userProfile.LastName, userProfile.Gender, userProfile.DOB);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }



        //[HttpGet, Route("GetPatientProfile")]
        //public async Task<IActionResult> GetPatientProfile()
        //{
        //    try
        //    {
        //        var userName = User.Identity.Name.ToString();

        //        var result = await this._repository.GetPatientProfile(userName);
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpGet, Route("GetDoctorProfile")]
        //public async Task<IActionResult> GetDoctorProfile()
        //{
        //    try
        //    {
        //        var userName = User.Identity.Name.ToString();
        //        var result = await this._repository.GetDoctorProfile(userName);
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        //[HttpGet, Route("GetDoctorProfile_Online")]
        //public async Task<IActionResult> GetDoctorProfile_Online()
        //{
        //    try
        //    {
        //        var userName = User.Identity.Name.ToString();
        //        //var userName = "6301712311";
        //        if (userName == null || userName == "")
        //        {
        //            return Unauthorized();
        //        }
        //        var result = await this._repository.GetDoctorProfile_Online(userName);
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        //[HttpPut, Route("UpdatePatientProfile")]
        //public async Task<IActionResult> UpdatePatientProfile([FromForm] Patient_Images Profile)
        //{
        //    var PatientProfile = await this.patient.UpdatePatient(Profile);
        //    if (PatientProfile == "Patient Updated Successfully")
        //    {
        //        return Ok();
        //    }
        //    return BadRequest("Not successfull.");
        //}

        //[HttpPut, Route("UpdatePatientProfile_Mobile")]
        //public async Task<IActionResult> UpdatePatientProfile_Mobile([FromForm] Patient_Images Profile)
        //{
        //    var PatientProfile = await this.patient.UpdatePatient(Profile);            

        //    if (PatientProfile == "Patient Updated Successfully")
        //    {
        //        var ok_response = new
        //        {
        //            ok = "Patient Updated Successfully"
        //        };
        //        string okResponse = JsonConvert.SerializeObject(ok_response);

        //        return Ok(okResponse);
        //    }

        //    var err_response = new
        //    {
        //        error = "Not successfull."
        //    };
        //    string errorResponse = JsonConvert.SerializeObject(err_response);

        //    return BadRequest(errorResponse);
        //}

        //[HttpPut, Route("UpdateDoctorProfile")]
        //public async Task<IActionResult> UpdateDoctorProfile([FromForm] Doctor_ImagesUP Profile)
        //{
        //    var DoctorProfile = await this.doctor.UpdateDoctor(Profile);
        //    if (DoctorProfile == "Doctor updated successfully.")
        //    {
        //        return Ok();
        //    }
        //    return BadRequest("Not successfull.");
        //}

        //[HttpPut, Route("UpdateDoctorProfile_Mobile")]
        //public async Task<IActionResult> UpdateDoctorProfile_Mobile([FromForm] Doctor_ImagesUP Profile)
        //{
        //    var DoctorProfile = await this.doctor.UpdateDoctor(Profile);
        //    if (DoctorProfile == "Doctor updated successfully.")
        //    {
        //        var ok_response = new
        //        {
        //            ok = "Doctor updated successfully."
        //        };
        //        string okResponse = JsonConvert.SerializeObject(ok_response);

        //        return Ok(okResponse);
        //    }
        //    var err_response = new
        //    {
        //        error = "Not successfull."
        //    };
        //    string errorResponse = JsonConvert.SerializeObject(err_response);

        //    return BadRequest(errorResponse);
        //}
        ////online
        //[HttpPut, Route("UpdateDoctorProfile_Online")]
        //public async Task<IActionResult> UpdateDoctorProfile_Online([FromBody] Doctor_ImagesUP_Online Profile)
        //{
        //    if (Profile == null)
        //    {
        //        return BadRequest();
        //    }
        //    var DoctorProfile = await this.doctor.UpdateDoctor_Online(Profile);
        //    if (DoctorProfile == "Doctor updated successfully.")
        //    {
        //        return Ok();
        //    }
        //    return BadRequest("Not successfull.");
        //}
        ////rohit
        //[HttpGet, Route("GetPatientProfile_Online")]
        //public async Task<IActionResult> GetPatientProfile_Online()
        //{
        //    try
        //    {
        //        var userName = User.Identity.Name.ToString();

        //        //var userName = "7775939380";

        //        if (userName == null || userName == "")
        //        {
        //            return Unauthorized();
        //        }

        //        var result = await this._repository.GetPatientProfile_Online(userName);
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        //[HttpPut, Route("UpdatePatientProfile_Online")]
        //public async Task<IActionResult> UpdatePatientProfile_Online([FromForm] Patient_Images_Online Profile)
        //{
        //    if (Profile == null)
        //    {
        //        return BadRequest();
        //    }
        //    var PatientProfile = await this.patient.UpdatePatient_Online(Profile);
        //    if (PatientProfile == "Patient Updated Successfully")
        //    {
        //        return Ok();
        //    }
        //    return BadRequest("Not successfull.");
        //}

    }
}
