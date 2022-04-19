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
        private readonly UserManager<AuthUser> userManager;
        private readonly RoleManager<AspNetRole> roleManager;
        private readonly GlobalContext auth = null!;
        public PatientController(IPatient repository, IAuthenticationRepository authrepository, GlobalContext auth,
            UserManager<AuthUser> userManager,
            RoleManager<AspNetRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.auth = auth;
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.authrepository = authrepository;
            this.findUserId = new FindUserId(userManager, roleManager, auth);
        }

        [HttpPost, Route("Admin/InsertPatient")]
        public async Task<ActionResult<Patient>> AdminPost([FromForm] PatientReg model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var result = await this.authrepository.ExtRegisterUserAsync(model.PR_FirstName, model.PR_LastName, model.PR_MobileNumber, model.PR_Email, model.Password, "ff613dc4-042a-4167-bc9b-22cdf3fffabc");

            if (result.IsSuccess)
            {
                var UserId = await findUserId.FindPatientIdFromUserEmaiOrNumber(model.PR_Email, model.PR_MobileNumber);
                var patient = await this._repository.InsertPatient(model, UserId);
                if (patient != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return BadRequest("Not successfull");
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
        public async Task<ActionResult<Patient>> AdminPut([FromForm] Patient_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdatePatient(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("Self/UpdatePatient")]
        public async Task<ActionResult<Patient>> SelfPut([FromForm] Patient_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdatePatient(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllPatient")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatient()
        {
            try
            {
                var result = await this._repository.GetAllPatient();
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
        
        [HttpDelete, Route("DeletePatient")]
        public async Task<ActionResult> DeletePatient(int PR_Id)
        {
            if (PR_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeletePatient(PR_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("Admin/GetPatientById")]
        public async Task<ActionResult<IEnumerable<PatientById>>> AdminGetPatientById(int PR_Id)
        {
            if (PR_Id == null)
            {
                return BadRequest();
            }
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

        [HttpGet, Route("Self/GetPatientById")]
        public async Task<ActionResult<IEnumerable<PatientById>>> SelfGetPatientById(int PR_Id)
        {
            if (PR_Id == null)
            {
                return BadRequest();
            }
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
        
        [HttpGet, Route("GetPatient_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Patient/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }

    }
}
