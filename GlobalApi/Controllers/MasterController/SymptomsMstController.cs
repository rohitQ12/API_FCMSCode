using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymptomsMstController : ControllerBase
    {
        public readonly ISymptomsMst _repository;
        public SymptomsMstController(ISymptomsMst repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertSymptomsMst")]
        public async Task<ActionResult<SymptomsMst>> Post([FromBody] SymptomsMst lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertSymptomsMst(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateSymptomsMst")]
        public async Task<ActionResult<SymptomsMst>> Put([FromBody] SymptomsMst lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateSymptomsMst(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllSymptomsMst")]
        public async Task<ActionResult<IEnumerable<SymptomsMst>>> GetAllSymptomsMst()
        {
            try
            {
                var result = await this._repository.GetAllSymptomsMst();
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
        [HttpGet, Route("GetSymptomsMst_DD")]
        public async Task<ActionResult<IEnumerable<SymptomsMst_DD>>> GetSymptomsMst_DD()
        {
            try
            {
                var result = await this._repository.GetSymptomsMst_DD();
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
        [HttpDelete, Route("DeleteSymptomsMst")]
        public async Task<ActionResult> DeleteSymptomsMst(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteSymptomsMst(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetSymptomsMstById")]
        public async Task<ActionResult<IEnumerable<SymptomsMst>>> GetSymptomsMstById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetSymptomsMstById(Id);
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
