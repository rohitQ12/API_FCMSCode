using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Consult_ParametersController : ControllerBase
    {
        public readonly IConsult_Parameters _repository;
        public Consult_ParametersController()
        {
            this._repository = new Consult_ParametersRepository();
        }

        [HttpPut, Route("UpdateConsult_Parameters")]
        public async Task<ActionResult<Consult_Parameters>> Put([FromBody] Consult_Parameters lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateConsult_Parameters(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllConsult_Parameters")]
        public async Task<ActionResult<IEnumerable<GetAllCPara>>> GetAllConsult_Parameters()
        {
            try
            {
                var result = await this._repository.GetAllConsult_Parameters();
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

        [HttpDelete, Route("DeleteConsult_Parameters")]
        public async Task<ActionResult> DeleteConsult_Parameters(int CON_Id)
        {
            if (CON_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteConsult_Parameters(CON_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetConsult_ParametersById")]
        public async Task<ActionResult<IEnumerable<CParaBy_Id>>> GetConsult_ParametersById(int CON_Id)
        {
            if (CON_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetConsult_ParametersById(CON_Id);
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

    }
}
