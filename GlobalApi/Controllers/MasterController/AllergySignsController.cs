using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergySignsController : ControllerBase
    {
        public readonly IAllergySigns _repository;
        public AllergySignsController()
        {
            this._repository = new AllergySignsRepository();
        }

        [HttpPost, Route("InsertAllergySigns")]
        public async Task<ActionResult<AllergySigns>> Post([FromBody] AllergySigns lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertAllergySigns(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateAllergySigns")]
        public async Task<ActionResult<AllergySigns>> Put([FromBody] AllergySigns lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateAllergySigns(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllAllergySigns")]
        public async Task<ActionResult<IEnumerable<AllergySigns>>> GetAllAllergySigns()
        {
            try
            {
                var result = await this._repository.GetAllAllergySigns();
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

        [HttpGet, Route("GetAllergySigns_DD")]
        public async Task<ActionResult<IEnumerable<AllergySigns_DD>>> GetAllergySigns_DD()
        {
            try
            {
                var result = await this._repository.GetAllergySigns_DD();
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

        [HttpDelete, Route("DeleteAllergySigns")]
        public async Task<ActionResult> DeleteAllergySigns(int Al_Id)
        {
            if (Al_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteAllergySigns(Al_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllergySignsById")]
        public async Task<ActionResult<IEnumerable<AllergySignsBy_Id>>> GetAllergySignsById(int Al_Id)
        {
            if (Al_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetAllergySignsById(Al_Id);
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
