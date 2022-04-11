using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        public readonly ISpecialization _repository;
        public SpecializationController(ISpecialization repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertSpecialization")]
        public async Task<ActionResult<Specialization>> Post([FromBody] Specialization lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertSpecialization(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateSpecialization")]
        public async Task<ActionResult<Specialization>> Put([FromBody] Specialization lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateSpecialization(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllSpecialization")]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetAllSpecialization()
        {
            try
            {
                var result = await this._repository.GetAllSpecialization();
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
        [HttpGet, Route("GetSpecialization_DD")]
        public async Task<ActionResult<IEnumerable<Specialization_DD>>> GetSpecialization_DD()
        {
            try
            {
                var result = await this._repository.GetSpecialization_DD();
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
        [HttpDelete, Route("DeleteSpecialization")]
        public async Task<ActionResult> DeleteSpecialization(int SP_Id)
        {
            if (SP_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteSpecialization(SP_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetSpecializationById")]
        public async Task<ActionResult<IEnumerable<SpecializationById>>> GetSpecializationById(int SP_Id)
        {
            if (SP_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetSpecializationById(SP_Id);
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
