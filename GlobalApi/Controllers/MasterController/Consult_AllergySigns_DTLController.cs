using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Consult_AllergySigns_DTLController : ControllerBase
    {
        public readonly IConsult_AllergySigns_DTL _repository;
        public Consult_AllergySigns_DTLController()
        {
            this._repository = new Consult_AllergySigns_DTLRepository();
        }


        //[HttpPut, Route("UpdateConsult_AllergySigns_DTL")]
        //public async Task<ActionResult<Consult_AllergySigns_DTL>> Put([FromBody] List<Consult_AllergySigns_DTL> lead, int CON_Id)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }

        //    var change = await _repository.UpdateConsult_AllergySigns_DTL(lead, CON_Id);

        //    if (change == true)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        [HttpGet, Route("GetAllConsult_AllergySigns_DTL")]
        public async Task<ActionResult<IEnumerable<GetAllCASdtl>>> GetAllConsult_AllergySigns_DTL()
        {
            try
            {
                var result = await this._repository.GetAllConsult_AllergySigns_DTL();
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

        [HttpGet, Route("GetAllCons_Allergys")]
        public async Task<ActionResult<IEnumerable<GetAllCons_Allergys>>> GetAllCons_Allergys()
        {
            try
            {
                var result = await this._repository.GetAllCons_Allergys();
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


        [HttpDelete, Route("DeleteConsult_AllergySigns_DTL")]
        public async Task<ActionResult> DeleteConsult_AllergySigns_DTL(int Ddtl_Id)
        {
            if (Ddtl_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteConsult_AllergySigns_DTL(Ddtl_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetConsult_AllergySigns_DTLById")]
        public async Task<ActionResult<IEnumerable<GetDiseaseDtlById>>> GetConsult_AllergySigns_DTLById(int Ddtl_PR_Id_FK)
        {
            if (Ddtl_PR_Id_FK == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetConsult_AllergySigns_DTLById(Ddtl_PR_Id_FK);
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
