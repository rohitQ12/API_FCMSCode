using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        public readonly IDesignation _repository;
        public DesignationController(IDesignation repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertDesignation")]
        public async Task<ActionResult<Designation>> Post([FromBody] Designation lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDesignation(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpPut, Route("UpdateDesignation")]
        public async Task<ActionResult<Designation>> Put([FromBody] Designation lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDesignation(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpGet, Route("GetAllDesignation")]
        public async Task<ActionResult<IEnumerable<Designation>>> GetAllDesignation()
        {
            try
            {
                var result = await this._repository.GetAllDesignation();
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
        
        
        [HttpGet, Route("GetDesignation_DD")]
        public async Task<ActionResult<IEnumerable<Designation_DD>>> GetDesignation_DD()
        {
            try
            {
                var result = await this._repository.GetDesignation_DD();
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
        
        
        [HttpDelete, Route("DeleteDesignation")]
        public async Task<ActionResult> DeleteDesignation(int designation_id)
        {
            if (designation_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDesignation(designation_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpGet, Route("GetDesignationById")]
        public async Task<ActionResult<IEnumerable<DesignationById>>> GetDesignationById(int designation_id)
        {
            if (designation_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDesignationById(designation_id);
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
    }
}
