using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Authorization;
using GlobalApi.Repository.MasterRepository;
using System.Net.Http.Headers;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.AdminIRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public readonly IDoctor _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        public readonly IAuthenticationRepository authrepository;
        private bool IfClaimExists = false;
        public readonly IUserRepository userRepository; 
        public DoctorController(IAuthenticationRepository authrepository, IUserRepository userRepository)
        {
            this._repository = new DoctorRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
            this.authrepository = authrepository;
            this.userRepository = userRepository;

        }
        [AllowAnonymous]
        [HttpPost, Route("Admin/InsertDoctor")]
        public async Task<IActionResult> AdminPost([FromForm] Doctor_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                string phonenumber = lead.DO_MobileNumber.ToString();
                string password = lead.DO_FirstName.Substring(0,1).ToUpper()+ lead.DO_FirstName.Substring(1,2).ToLower() + "/" + phonenumber.Substring(0,3);
                var result = await this.authrepository.RegisterUserAsync(lead.DO_FirstName, 
                lead.DO_LastName, phonenumber, lead.DO_Email, password, "5ed4578c-0915-4874-9aae-1b0f5e62f6dd", lead.DO_HO_Id_FK,lead.DO_Photo);

                if (result.IsSuccess)
                {
                    var change = await _repository.InsertDoctor(lead, result.userid);
                    if (change == "Doctor Added Successfully")
                    {
                        return Ok();
                    }
                    var delete = await this.findUserId.Deleteuser(result.userid);
                    return BadRequest(change);
                }
                return BadRequest(result.Message);
            }
            return Unauthorized();
            
        }
        
        
        [AllowAnonymous]
        [HttpPost, Route("Self/InsertDoctor")]
        public async Task<IActionResult> SelfPost([FromForm] Doctor_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertDoctor(lead,"");

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        
        [HttpPut, Route("Admin/UpdateDoctor")]
        public async Task<IActionResult> AdminPut([FromForm] Doctor_ImagesUP lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateDoctor(lead);
                if (change == "Doctor Updated Successfully")
                {
                    var DO_UserId = await this.findUserId.FindDoctorUserIdFromDoctorId(lead.DO_Id);
                    var profile = await userRepository.UpdateUserProfile(DO_UserId, lead.DO_Photo, lead.DO_Email,
                     lead.DO_MobileNumber.ToString(), lead.DO_FirstName, lead.DO_LastName, lead.DO_Gender, lead.DO_DOB);
                    if (profile == "User Updated successfully")
                    {
                        return Ok();
                    }
                    return BadRequest(profile);
                }
                return BadRequest(change);
            }
            return Unauthorized();
            
        }


        [HttpPut, Route("Self/UpdateDoctor/{DO_Photo}")]
        public async Task<IActionResult> SelfPut([FromBody] Doctor_ImagesUP lead,[FromForm] IFormFile DO_Photo)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {

                var change = await _repository.UpdateDoctor(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        [HttpGet, Route("GetAllDoctor")]
        public async Task<IActionResult> GetAllDoctor()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "DoctorView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {

                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var DO_HO_Id_FK = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
                    var result = await this._repository.GetAllDoctor(DO_HO_Id_FK, roleaction);
                    if (result.Any())
                    {
                        return Ok(result);
                    }

                    return NotFound("Doctor data not found");
                }
                return Unauthorized();
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctor(int DO_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteDoctor(DO_Id);

                if (change == "Doctor Deleted Successfully") { 
                    return Ok();
                }
                    return BadRequest(change);
            }
            return Unauthorized();
            
        }


        [HttpGet, Route("Admin/GetDoctorById")]
        public async Task<IActionResult> AdminGetDoctorById(int DO_Id)
        {

                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "DoctorView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {

                    var result = await this._repository.GetDoctorById(DO_Id);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    return NotFound("Doctor data not found");
                }
                return Unauthorized();
               
        }

        [HttpGet, Route("Self/GetDoctorById")]
        public async Task<IActionResult> SelfGetDoctorById(int DO_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {

                var result = await this._repository.GetDoctorById(DO_Id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Doctor data not found");
            }
            return Unauthorized();
           
        }

        [HttpGet, Route("Doctor_DD")]
        public async Task<IActionResult> Doctor_DD(int SP_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {

                var result = await this._repository.Doctor_DD(SP_Id);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound("Doctor data not found");
            }
            return Unauthorized();
        }

        [HttpGet, Route("GetDoctor_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Doctor/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }

        [HttpPut, Route("ApproveDoctor")]
        public async Task<IActionResult> ApproveDoctor([FromBody] ApproveDoctor lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveDoctor(lead);
                if (change == "Doctor Approved Successfully") { 
                    return Ok();
                }
            }
            return Unauthorized();
        }
    }
}
