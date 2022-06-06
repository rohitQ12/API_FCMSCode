using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Authorization;
using GlobalApi.Repository.MasterRepository;
using System.Net.Http.Headers;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public readonly IDoctor _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public DoctorController()
        {
            this._repository = new DoctorRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
        }
        [AllowAnonymous]
        [HttpPost, Route("Admin/InsertDoctor")]
        public async Task<ActionResult<Doctor>> AdminPost([FromForm] Doctor_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorRegistrationAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertDoctor(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        
        [AllowAnonymous]
        [HttpPost, Route("Self/InsertDoctor")]
        public async Task<ActionResult<Doctor>> SelfPost([FromForm] Doctor_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorRegistrationAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertDoctor(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        
        [HttpPut, Route("Admin/UpdateDoctor")]
        public async Task<ActionResult<Doctor>> AdminPut([FromForm] Doctor_ImagesUP lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorRegistrationEdit" && x.ClaimValue == "Y");
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


        [HttpPut, Route("Self/UpdateDoctor/{DO_Photo}")]
        public async Task<ActionResult<Doctor>> SelfPut([FromBody] Doctor_ImagesUP lead,[FromForm] IFormFile DO_Photo)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorRegistrationEdit" && x.ClaimValue == "Y");
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
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctor()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "DoctorRegistrationView" && x.ClaimValue == "Y");
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

                    return NotFound();
                }
                return Unauthorized();
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteDoctor")]
        public async Task<ActionResult> DeleteDoctor(int DO_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorRegistrationDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {

                if (DO_Id <= 0)
                {
                    return BadRequest();
                }
                var change = await _repository.DeleteDoctor(DO_Id);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }


        [HttpGet, Route("Admin/GetDoctorById")]
        public async Task<ActionResult<IEnumerable<DoctorById>>> AdminGetDoctorById(int DO_Id)
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "DoctorRegistrationView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {

                    var result = await this._repository.GetDoctorById(DO_Id);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return Ok(result);
                }
                return Unauthorized();
                

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet, Route("Self/GetDoctorById")]
        public async Task<ActionResult<IEnumerable<DoctorById>>> SelfGetDoctorById(int DO_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorRegistrationView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {

                var result = await this._repository.GetDoctorById(DO_Id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            return Unauthorized();
           
        }

        [HttpGet, Route("Doctor_DD")]
        public async Task<ActionResult<IEnumerable<Doctor_DD>>> Doctor_DD(int SP_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorRegistrationView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {

                var result = await this._repository.Doctor_DD(SP_Id);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
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
        public async Task<ActionResult> ApproveDoctor([FromBody] ApproveDoctor lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DoctorRegistrationApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveDoctor(lead);
                if (change != null)
                    return Ok(change);
                else
                    return BadRequest("Not successfull");
                
            }
            return Unauthorized();
        }
    }
}
