using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        public readonly ICountry _repository;
        public CountryController()
        {
            this._repository = new CountryRepository();
        }

        [HttpPost, Route("InsertCountry")]
        public async Task<ActionResult<Countries>> Post([FromBody] Countries lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertCountry(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpPut, Route("UpdateCountry")]
        public async Task<ActionResult<Countries>> Put([FromBody] Countries lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateCountry(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpGet, Route("GetAllCountry")]
        public async Task<ActionResult<IEnumerable<Countries>>> GetAllCountry()
        {
            try
            {
                var result = await this._repository.GetAllCountry();
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
        
        
        [HttpGet, Route("GetCountry_DD")]
        public async Task<ActionResult<IEnumerable<Country_DD>>> GetCountry_DD()
        {
            try
            {
                var result = await this._repository.GetCountry_DD();
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
        
        
        [HttpDelete, Route("DeleteCountry")]
        public async Task<ActionResult> DeleteCountry(int Country_id)
        {
            if (Country_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteCountry(Country_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpGet, Route("GetCountryById")]
        public async Task<ActionResult<IEnumerable<CountryById>>> GetCountryById(int Country_id)
        {
            if (Country_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetCountryById(Country_id);
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

        [HttpPut, Route("ApproveCountry")]
        public async Task<ActionResult> ApproveCountry(int cntry_id, string? Remarks)
        {
            if (cntry_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.ApproveCountry(cntry_id, Remarks);

            if (change != null)
                return Ok(change);
            else
                return BadRequest("Not successfull");
        }

    }
}
