using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GramController : ControllerBase
    {
        public readonly IGram _repository;
        public GramController()
        {
            this._repository = new GramRepository();
        }

        [HttpPost, Route("InsertGram")]
        public async Task<ActionResult<Gram>> Post([FromBody] Gram lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertGram(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateGram")]
        public async Task<ActionResult<Gram>> Put([FromBody] Gram lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateGram(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetGram_DD")]
        public async Task<ActionResult<IEnumerable<Gram_DD>>> GetGram_DD(int Taluk_id)
        {
            try
            {
                var result = await this._repository.GetGram_DD(Taluk_id);
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

        [HttpDelete, Route("DeleteGram")]
        public async Task<ActionResult> DeleteGram(int Gram_id)
        {
            if (Gram_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteGram(Gram_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        //[HttpGet, Route("GetGramById")]
        //public async Task<ActionResult<IEnumerable<GramById>>> GetGramById(int Gram_id)
        //{
        //    if (Gram_id == null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var result = await this._repository.GetGramById(Gram_id);
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

        [HttpGet, Route("GetAllGram")]
        public async Task<ActionResult<IEnumerable<GetGramTaluk>>> GetAllGram()
        {
            try
            {
                var result = await this._repository.GetAllGram();
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
