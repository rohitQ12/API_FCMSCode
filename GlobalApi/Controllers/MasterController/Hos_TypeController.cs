using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Hos_TypeController : ControllerBase
    {
        public readonly IHos_Type _repository;
        public Hos_TypeController()
        {
            this._repository = new Hos_TypeRepository();
        }

        [HttpPost, Route("InsertHos_Type")]
        public async Task<ActionResult<Hos_Type>> Post([FromBody] Hos_Type lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertHos_Type(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateHos_Type")]
        public async Task<ActionResult<Hos_Type>> Put([FromBody] Hos_Type lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateHos_Type(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllHos_Type")]
        public async Task<ActionResult<IEnumerable<Hos_Type>>> GetAllHos_Type()
        {
            try
            {
                var result = await this._repository.GetAllHos_Type();
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

        [HttpGet, Route("GetHos_Type_DD")]
        public async Task<ActionResult<IEnumerable<HosType_DD>>> GetHos_Type_DD()
        {
            try
            {
                var result = await this._repository.GetHos_Type_DD();
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

        [HttpDelete, Route("DeleteHos_Type")]
        public async Task<ActionResult> DeleteHos_Type(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteHos_Type(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        //[HttpGet, Route("GetHos_TypeById")]
        //public async Task<ActionResult<IEnumerable<Hos_TypeBy_Id>>> GetHos_TypeById(int Id)
        //{
        //    if (Id == null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var result = await this._repository.GetHos_TypeById(Id);
        //        if (result == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(result);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

    }
}
