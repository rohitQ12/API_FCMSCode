using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Consult_Diseases_DTLController : ControllerBase
    {
        public readonly IConsult_Diseases_DTL _repository;
        public Consult_Diseases_DTLController()
        {
            this._repository = new Consult_Diseases_DTLRepository();
        }


        //[HttpPut, Route("UpdateConsult_Diseases_DTL")]
        //public async Task<ActionResult<Consult_Diseases_DTL>> Put([FromBody] List<Consult_Diseases_DTL> lead, int CON_Id)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }

        //    var change = await _repository.UpdateConsult_Diseases_DTL(lead, CON_Id);

        //    if (change == true)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        [HttpGet, Route("GetAllConsult_Diseases_DTL")]
        public async Task<ActionResult<IEnumerable<GetAllCDDtl>>> GetAllConsult_Diseases_DTL()
        {
            try
            {
                var result = await this._repository.GetAllConsult_Diseases_DTL();
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
        
        [HttpGet, Route("GetAllCons_Diseases")]
        public async Task<ActionResult<IEnumerable<GetAllCons_Diseases>>> GetAllCons_Diseases()
        {
            try
            {
                var result = await this._repository.GetAllCons_Diseases();
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


        [HttpDelete, Route("DeleteConsult_Diseases_DTL")]
        public async Task<ActionResult> DeleteConsult_Diseases_DTL(int Ddtl_Id)
        {
            if (Ddtl_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteConsult_Diseases_DTL(Ddtl_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetConsult_Diseases_DTLById")]
        public async Task<ActionResult<IEnumerable<GetCDDtlById>>> GetConsult_Diseases_DTLById(int CON_Id)
        {
            if (CON_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetConsult_Diseases_DTLById(CON_Id);
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
