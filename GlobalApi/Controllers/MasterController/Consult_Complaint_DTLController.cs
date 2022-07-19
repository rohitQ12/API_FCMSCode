using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Consult_Complaint_DTLController : ControllerBase
    {
        public readonly IConsult_Complaint_DTL _repository;
        public Consult_Complaint_DTLController()
        {
            this._repository = new Consult_Complaint_DTLRepository();
        }


        //[HttpPut, Route("UpdateConsult_Complaint_DTL")]
        //public async Task<ActionResult<Consult_Complaint_DTL>> Put([FromBody] List<Consult_Complaint_DTL> lead, int CON_Id)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }

        //    var change = await _repository.UpdateConsult_Complaint_DTL(lead, CON_Id);

        //    if (change == true)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        [HttpGet, Route("GetAllConsult_Complaint_DTL")]
        public async Task<ActionResult<IEnumerable<GetAllCCdtl>>> GetAllConsult_Complaint_DTL()
        {
            try
            {
                var result = await this._repository.GetAllConsult_Complaint_DTL();
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
        
        [HttpGet, Route("GetAllCons_Complaints")]
        public async Task<ActionResult<IEnumerable<GetAllCons_Complaints>>> GetAllCons_Complaints()
        {
            try
            {
                var result = await this._repository.GetAllCons_Complaints();
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


        [HttpDelete, Route("DeleteConsult_Complaint_DTL")]
        public async Task<ActionResult> DeleteConsult_Complaint_DTL(int CPT_Id)
        {
            if (CPT_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteConsult_Complaint_DTL(CPT_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetConsult_Complaint_DTLById")]
        public async Task<ActionResult<IEnumerable<CCdtlBy_Id>>> GetConsult_Complaint_DTLById(int CON_Id)
        {
            if (CON_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetConsult_Complaint_DTLById(CON_Id);
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
