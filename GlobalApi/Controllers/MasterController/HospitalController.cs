using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using GlobalApi.Models.Authentication;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        public readonly IHospital _repository;
        public HospitalController()
        {
            this._repository = new HospitalRepository();
        }

        [HttpPost, Route("Admin/InsertHospital")]
        public async Task<ActionResult<Hospital>> AdminPost([FromForm] Hospital_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertHospital(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPost, Route("Self/InsertHospital")]
        public async Task<ActionResult<Hospital>> SelfPost([FromForm] Hospital_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertHospital(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("Admin/UpdateHospital")]
        public async Task<ActionResult<Hospital>> AdminPut([FromForm] Hospital_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateHospital(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("Self/UpdateHospital")]
        public async Task<ActionResult<Hospital>> SelfPut([FromForm] Hospital_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateHospital(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllHospital")]
        public async Task<ActionResult<IEnumerable<GetAllHospital>>> GetAllHospital()
        {
            try
            {
                var result = await this._repository.GetAllHospital();
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
        
        [HttpGet, Route("Admin/GetHospital_DD")]
        public async Task<ActionResult<IEnumerable<Hospital_DD>>> AdminGetHospital_DD()
        {
            try
            {
                var result = await this._repository.GetHospital_DD();
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
        [HttpGet, Route("Admin/GetHospitalCategory_DD")]
        public async Task<ActionResult<IEnumerable<Usercategory_DD>>> GetHospitalCategory_DD()
        {
            try
            {
                var result = await this._repository.GetHospitalCategory_DD();
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

        [HttpGet, Route("Self/GetHospital_DD")]
        public async Task<ActionResult<IEnumerable<Hospital_DD>>> SelfGetHospital_DD()
        {
            try
            {
                var result = await this._repository.GetHospital_DD();
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
        
        [HttpDelete, Route("DeleteHospital")]
        public async Task<ActionResult> DeleteHospital(int Hos_Id)
        {
            if (Hos_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteHospital(Hos_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("Admin/GetHospitalById")]
        public async Task<ActionResult<IEnumerable<HospitalById>>> AdminGetHospitalById(int Hos_Id)
        {
            if (Hos_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetHospitalById(Hos_Id);
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

        [HttpGet, Route("Self/GetHospitalById")]
        public async Task<ActionResult<IEnumerable<HospitalById>>> SelfGetHospitalById(int Hos_Id)
        {
            if (Hos_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetHospitalById(Hos_Id);
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
        
        [HttpGet, Route("GetHospital_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Hospital/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }
    }
}
