using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Authorization;
using GlobalApi.Repository.MasterRepository;
using System.Net.Http.Headers;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public readonly IDoctor _repository;
        public DoctorController()
        {
            this._repository = new DoctorRepository();
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

        [HttpPost, Route("post-images")]
        public ActionResult<Doctor> testing([FromForm] Doctor_Imagestesting request)
        {

            return Ok(request);
        }
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot/Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
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

        [HttpGet, Route("Doctor_DD")]
        public async Task<ActionResult<IEnumerable<Doctor_DD>>> Doctor_DD(int SP_Id)
        {
            try
            {
                var result = await this._repository.Doctor_DD(SP_Id);
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

        [HttpGet, Route("GetDoctor_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Doctor/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }

    }
}
