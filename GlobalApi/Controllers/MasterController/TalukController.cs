using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalukController : ControllerBase
    {
        public readonly ITaluk _repository;
        public TalukController()
        {
            this._repository = new TalukRepository();
        }

        [HttpPost, Route("InsertTaluk")]
        public async Task<ActionResult<Taluk>> Post([FromBody] Taluk lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertTaluk(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateTaluk")]
        public async Task<ActionResult<Taluk>> Put([FromBody] Taluk lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateTaluk(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetTaluk_DD")]
        public async Task<ActionResult<IEnumerable<Taluk_DD>>> GetTaluk_DD(int district_id)
        {
            try
            {
                var result = await this._repository.GetTaluk_DD(district_id);
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

        [HttpDelete, Route("DeleteTaluk")]
        public async Task<ActionResult> DeleteTaluk(int Taluk_id)
        {
            if (Taluk_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteTaluk(Taluk_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        //[HttpGet, Route("GetTalukById")]
        //public async Task<ActionResult<IEnumerable<TalukById>>> GetTalukById(int Taluk_id)
        //{
        //    if (Taluk_id == null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var result = await this._repository.GetTalukById(Taluk_id);
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

        [HttpGet, Route("GetAllTaluk")]
        public async Task<ActionResult<IEnumerable<GetTalukDistricts>>> GetAllTaluk()
        {
            try
            {
                var result = await this._repository.GetAllTaluk();
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
