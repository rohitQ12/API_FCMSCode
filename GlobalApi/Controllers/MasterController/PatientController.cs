using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.GlobalClasses;
using Microsoft.AspNetCore.Identity;
using GlobalApi.Models.Authentication;
using GlobalApi.Data;


namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        public readonly IPatient _repository;
        public readonly IAuthenticationRepository authrepository;
        public readonly FindUserId findUserId;
        private readonly GlobalContext auth = null!;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public PatientController(IAuthenticationRepository authrepository)
        {
            this.auth =new GlobalContext();
            this._repository = new PatientRepository();
            this.authrepository = authrepository;
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        //[HttpPost, Route("Admin/InsertPatient")]
        //public async Task<ActionResult<Patient>> AdminPost([FromForm] PatientReg model)
        //{
        //    if (model == null)
        //    {
        //        return BadRequest();
        //    }
        //    var result = await this.authrepository.ExtRegisterUserAsync(model.PR_FirstName, model.PR_LastName, model.PR_MobileNumber, model.PR_Email, model.Password, "ff613dc4-042a-4167-bc9b-22cdf3fffabc");

        //    if (result.IsSuccess)
        //    {
        //        var UserId = await findUserId.FindPatientIdFromUserEmaiOrNumber(model.PR_Email, model.PR_MobileNumber);
        //        var patient = await this._repository.InsertPatient(model, UserId);
        //        if (patient != null)
        //            return Ok();
        //        else
        //            return BadRequest("Not successfull");
        //    }
        //    return BadRequest("Not successfull");
        //}
        [HttpPost, Route("Admin/InsertPatient")]
        public async Task<IActionResult> AdminPost([FromForm] Patient_Images model)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PatientRegistrationAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var patient = await this._repository.InsertPatient(model, "");
                if (patient != null)
                    return Ok(patient);
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        //[HttpPost, Route("Self/InsertPatient")]
        //public async Task<ActionResult<Patient>> SelfPost([FromForm] Patient_Images lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertPatient(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        [HttpPut, Route("Admin/UpdatePatient")]
        public async Task<IActionResult> AdminPut([FromForm] Patient_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PatientRegistrationEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdatePatient(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpPut, Route("Self/UpdatePatient")]
        public async Task<IActionResult> SelfPut([FromForm] Patient_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PatientRegistrationEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdatePatient(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpGet, Route("GetAllPatient")]
        public async Task<IActionResult> GetAllPatient()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "PatientRegistrationView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var result = await this._repository.GetAllPatient();
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
        
        [HttpDelete, Route("DeletePatient")]
        public async Task<IActionResult> DeletePatient(int PR_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PatientRegistrationDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeletePatient(PR_Id);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        [HttpGet, Route("Admin/GetPatientById")]
        public async Task<IActionResult> AdminGetPatientById(int PR_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PatientRegistrationView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                try
                {
                    var result = await this._repository.GetPatientById(PR_Id);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return Unauthorized();
            
        }

        [HttpGet, Route("Self/GetPatientById")]
        public async Task<IActionResult> SelfGetPatientById()
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PatientRegistrationView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                try
                {
                    var userName = User.Identity.Name.ToString();
                    var PR_Id = await findUserId.FindPatientIdFromUserId(userName);
                    var result = await this._repository.GetPatientById(PR_Id);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return Unauthorized();
            
        }
        
        [HttpGet, Route("GetPatient_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Patient/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }

        [HttpGet, Route("GetPatient_DD")]
        public async Task<IActionResult> GetPatient_DD()
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PatientRegistrationView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                try
                {
                    var result = await this._repository.GetPatient_DD();
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
            return Unauthorized();
            
        }
        
        [HttpGet, Route("Admin/GetPatientByCode")]
        public async Task<IActionResult> AdminGetPatientByCode(string PR_PatientCode)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PatientRegistrationView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                try
                {
                    var result = await this._repository.GetPatientByCode(PR_PatientCode);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return Unauthorized();
            
        }

        /*[HttpGet, Route("GetPatient_Count")]
        public async Task<ActionResult<IEnumerable<Patient_Count>>> GetPatient_Count()
        {
            try
            {
                var result = await this._repository.GetPatient_Count();
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
        }*/

    }
}
