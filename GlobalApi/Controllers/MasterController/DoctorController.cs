using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Authorization;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public readonly IDoctor _repository;
        public DoctorController(IDoctor repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        [AllowAnonymous]
        [HttpPost, Route("Admin/InsertDoctor")]
        public async Task<ActionResult<Doctor>> AdminPost([FromForm] Doctor_Images lead)
        {
            if (lead == null)
            {

                return BadRequest();
            }
            var change = await _repository.InsertDoctor(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [AllowAnonymous]
        [HttpPost, Route("Self/InsertDoctor")]
        public async Task<ActionResult<Doctor>> SelfPost([FromForm] Doctor_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDoctor(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpPut, Route("Admin/UpdateDoctor")]
        public async Task<ActionResult<Doctor>> AdminPut([FromForm] Doctor_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDoctor(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("Self/UpdateDoctor/{DO_Photo}")]
        public async Task<ActionResult<Doctor>> SelfPut([FromBody] Doctor_Images lead,[FromForm] IFormFile DO_Photo)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDoctor(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("testing/UpdateDoctor")]
        public ActionResult<Doctor> testing([FromBody] Doctor_Imagestesting lead)
        {

            return Ok(lead);
        }

        [HttpPut, Route("lang/{DO_Photo}/UpdateDoctor")]
        public ActionResult testing([FromBody]List<DoctorLanguage> DO_Photo, [FromForm] Doctor_Imagestesting lead)
        {

            return Ok(lead);
        }
        
        [HttpGet, Route("GetAllDoctor")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctor()
        {
            try
            {
                var result = await this._repository.GetAllDoctor();
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

        [HttpDelete, Route("DeleteDoctor")]
        public async Task<ActionResult> DeleteDoctor(int DO_Id)
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

        [HttpGet, Route("Admin/GetDoctorById")]
        public async Task<ActionResult<IEnumerable<DoctorById>>> AdminGetDoctorById(int DO_Id)
        {
            if (DO_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDoctorById(DO_Id);
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

        [HttpGet, Route("Self/GetDoctorById")]
        public async Task<ActionResult<IEnumerable<DoctorById>>> SelfGetDoctorById(int DO_Id)
        {
            if (DO_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDoctorById(DO_Id);
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
        
        [HttpGet, Route("GetDoctor_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Doctor/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }

    }
}
