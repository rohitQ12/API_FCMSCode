using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Consult_Symptoms_DTLController : ControllerBase
    {
        public readonly IConsult_Symptoms_DTL _repository;
        public Consult_Symptoms_DTLController()
        {
            this._repository = new Consult_Symptoms_DTLRepository();
        }

        //[HttpPut, Route("UpdateConsult_Symptoms_DTL")]
        //public async Task<ActionResult<Consult_Symptoms_DTL>> Put([FromBody] List<Consult_Symptoms_DTL> lead, int CON_Id)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }

        //    var change = await _repository.UpdateConsult_Symptoms_DTL(lead, CON_Id);

        //    if (change == true)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        [HttpGet, Route("GetAllConsult_Symptoms_DTL")]
        public async Task<ActionResult<IEnumerable<GetAllCSdtl>>> GetAllConsult_Symptoms_DTL()
        {
            try
            {
                var result = await this._repository.GetAllConsult_Symptoms_DTL();
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

        [HttpDelete, Route("DeleteConsult_Symptoms_DTL")]
        public async Task<ActionResult> DeleteConsult_Symptoms_DTL(int SYM_Id)
        {
            if (SYM_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteConsult_Symptoms_DTL(SYM_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetConsult_Symptoms_DTLById")]
        public async Task<ActionResult<IEnumerable<CSdtlBy_Id>>> GetConsult_Symptoms_DTLById(int CON_Id)
        {
            if (CON_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetConsult_Symptoms_DTLById(CON_Id);
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
