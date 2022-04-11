using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
        public readonly IQualification _repository;
        public QualificationController(IQualification repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertQualification")]
        public async Task<ActionResult<Qualification>> Post([FromBody] Qualification lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertQualification(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateQualification")]
        public async Task<ActionResult<Qualification>> Put([FromBody] Qualification lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateQualification(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllQualification")]
        public async Task<ActionResult<IEnumerable<Qualification>>> GetAllQualification()
        {
            try
            {
                var result = await this._repository.GetAllQualification();
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
        [HttpGet, Route("GetQualification_DD")]
        public async Task<ActionResult<IEnumerable<Qualification_DD>>> GetQualification_DD()
        {
            try
            {
                var result = await this._repository.GetQualification_DD();
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
        [HttpDelete, Route("DeleteQualification")]
        public async Task<ActionResult> DeleteQualification(int qualification_id)
        {
            if (qualification_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteQualification(qualification_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetQualificationById")]
        public async Task<ActionResult<IEnumerable<QualificationById>>> GetQualificationById(int qualification_id)
        {
            if (qualification_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetQualificationById(qualification_id);
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
