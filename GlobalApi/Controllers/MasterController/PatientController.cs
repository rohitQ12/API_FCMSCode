using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.IRepository.AdminIRepository;
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
        public readonly IUserRepository userRepository;
        private bool IfClaimExists = false;
        public PatientController(IAuthenticationRepository authrepository, IUserRepository userRepository)
        {
            this.auth = new GlobalContext();
            this._repository = new PatientRepository();
            this.authrepository = authrepository;
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
            this.userRepository = userRepository;
        }

        [HttpPost, Route("Admin/InsertPatient")]
        public async Task<IActionResult> AdminPost([FromForm] Patient_Images model)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PatientRegistrationAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                string Username = User.Identity.Name;
                string Create_by = await this.findUserId.FindIdFromUserName(Username);
                string phonenumber = model.PR_MobileNumber.ToString();
                string password = (model.PR_FirstName.Substring(0, 1)).ToUpper() + model.PR_FirstName.Substring(1, 2).ToLower() + "/" + phonenumber.Substring(0, 3);
                var result = await this.authrepository.RegisterUserAsync(model.PR_FirstName,
                model.PR_LastName, phonenumber, model.PR_Email, password, "ff613dc4-042a-4167-bc9b-22cdf3fffabc", 0, model.PR_Photo);

                if (result.IsSuccess)
                {
                    var change = await this._repository.InsertPatient(model, result.userid, Create_by);
                    if (change == "Patient Added Successfully")
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

                if (change == "Patient Updated Successfully")
                {
                    var PR_UserId = await this.findUserId.FindPatientUserIdFromPatientId(lead.PR_Id);
                    var profile = await userRepository.UpdateUserProfile(PR_UserId, lead.PR_Photo, lead.PR_Email,
                        lead.PR_MobileNumber.ToString(), lead.PR_FirstName, lead.PR_LastName, lead.PR_Gender, lead.PR_DOB);
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
                    string Create_by = await this.findUserId.FindIdFromUserName(username);
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(username);
                    int OfficeRoleId = await this.findUserId.FindOfficeRoleIdFromUserNames(username);
                    var result = await this._repository.GetAllPatient(OfficeRoleId, roleaction);
                    if (result.Any())
                    {
                        return Ok(result);
                    }
                    return NotFound("Patient data not found");
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

                if (change == "Patient Deleted Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
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

                var result = await this._repository.GetPatientById(PR_Id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Patient data not found");


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

                var userName = User.Identity.Name.ToString();
                var PR_Id = await findUserId.FindPatientIdFromUserId(userName);
                var result = await this._repository.GetPatientById(PR_Id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Patient data not found");

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

                var result = await this._repository.GetPatient_DD();
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound("Patient data not found");

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

                var result = await this._repository.GetPatientByCode(PR_PatientCode);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Patient data not found");


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
