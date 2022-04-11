using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {
        public readonly IDiseases _repository;
        public DiseasesController(IDiseases repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertDiseases")]
        public async Task<ActionResult<Diseases>> Post([FromBody] Diseases lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDiseases(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateDiseases")]
        public async Task<ActionResult<Diseases>> Put([FromBody] Diseases lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDiseases(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllDiseases")]
        public async Task<ActionResult<IEnumerable<Diseases>>> GetAllDiseases()
        {
            try
            {
                var result = await this._repository.GetAllDiseases();
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
        [HttpGet, Route("GetDiseases_DD")]
        public async Task<ActionResult<IEnumerable<Diseases_DD>>> GetDiseases_DD()
        {
            try
            {
                var result = await this._repository.GetDiseases_DD();
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
        [HttpDelete, Route("DeleteDiseases")]
        public async Task<ActionResult> DeleteDiseases(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDiseases(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetDiseasesById")]
        public async Task<ActionResult<IEnumerable<DiseasesBy_Id>>> GetDiseasesById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDiseasesById(Id);
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
