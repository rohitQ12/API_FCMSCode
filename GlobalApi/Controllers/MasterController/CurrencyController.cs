using GlobalApi.IRepository.MasterIRepository;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        public readonly ICurrency _repository;
        public CurrencyController()
        {
            this._repository = new CurrencyRepository();
        }

        [HttpPost, Route("InsertCurrency")]
        public async Task<ActionResult<Currency>> Post([FromBody] Currency lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertCurrency(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateCurrency")]
        public async Task<ActionResult<Currency>> Put([FromBody] Currency lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateCurrency(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllCurrency")]
        public async Task<ActionResult<IEnumerable<GetCountryCurrency>>> GetAllCurrency()
        {
            try
            {
                var result = await this._repository.GetAllCurrency();
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
        [HttpGet, Route("GetCurrency_DD")]
        public async Task<ActionResult<IEnumerable<Currency_DD>>> GetCurrency_DD()
        {
            try
            {
                var result = await this._repository.GetCurrency_DD();
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
        [HttpDelete, Route("DeleteCurrency")]
        public async Task<ActionResult> DeleteCurrency(int currency_id)
        {
            if (currency_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteCurrency(currency_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetCurrencyById")]
        public async Task<ActionResult<IEnumerable<CurrencyById>>> GetCurrencyById(int currency_id)
        {
            if (currency_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetCurrencyById(currency_id);
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
