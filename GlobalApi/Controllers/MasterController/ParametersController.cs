using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametersController : ControllerBase
    {
        public readonly IParameters _repository;
        public ParametersController()
        {
            this._repository = new ParametersRepository();
        }

        //[HttpPost, Route("InsertParameters")]
        //public async Task<ActionResult<Parameters>> Post([FromBody] Parameters lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertParameters(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}
        
        [HttpPut, Route("UpdateParameters")]
        public async Task<ActionResult<Parameters>> Put([FromBody] Parameters lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateParameters(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAllParameters")]
        public async Task<ActionResult<IEnumerable<GetAllParameters>>> GetAllParameters()
        {
            try
            {
                var result = await this._repository.GetAllParameters();
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
        
        //[HttpGet, Route("GetParameters_DD")]
        //public async Task<ActionResult<IEnumerable<Parameters_DD>>> GetParameters_DD()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetParameters_DD();
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        
        [HttpDelete, Route("DeleteParameters")]
        public async Task<ActionResult> DeleteParameters(int PA_Id)
        {
            if (PA_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteParameters(PA_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetParametersById")]
        public async Task<ActionResult<IEnumerable<ParametersBy_Id>>> GetParametersById(int PA_PR_Id_FK)
        {
            if (PA_PR_Id_FK == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetParametersById(PA_PR_Id_FK);
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
