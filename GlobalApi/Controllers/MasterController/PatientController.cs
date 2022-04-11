using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        public readonly IPatient _repository;
        public PatientController(IPatient repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("Admin/InsertPatient")]
        public async Task<ActionResult<Patient>> AdminPost([FromForm] Patient_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertPatient(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPost, Route("Self/InsertPatient")]
        public async Task<ActionResult<Patient>> SelfPost([FromForm] Patient_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertPatient(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

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
