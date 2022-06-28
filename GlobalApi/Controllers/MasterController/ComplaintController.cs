using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        public readonly IComplaint _repository;
        public ComplaintController()
        {
            this._repository = new ComplaintRepository();
        }

        //[HttpPost, Route("InsertComplaint")]
        //public async Task<ActionResult<Complaint>> Post([FromBody] Complaint lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertComplaint(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        [HttpPut, Route("UpdateComplaint")]
        public async Task<ActionResult<Complaint>> Put([FromBody] List<Complaint> lead, int Appt_id)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateComplainttest(lead, Appt_id);

            if (change == true)
                return Ok();
            else
                return BadRequest("Not successfull");
        }


        [HttpGet, Route("GetAllComplaint")]
        public async Task<ActionResult<IEnumerable<GetAllComplaint>>> GetAllComplaint()
        {
            try
            {
                var result = await this._repository.GetAllComplaint();
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
        
        
        //[HttpGet, Route("GetComplaint_DD")]
        //public async Task<ActionResult<IEnumerable<Complaint_DD>>> GetComplaint_DD()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetComplaint_DD();
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
        
        
        
        [HttpDelete, Route("DeleteComplaint")]
        public async Task<ActionResult> DeleteComplaint(int CPT_Id)
        {
            if (CPT_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteComplaint(CPT_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpGet, Route("GetComplaintById")]
        public async Task<ActionResult<IEnumerable<ComplaintBy_Id>>> GetComplaintById(int CPT_PR_Id_FK)
        {
            if (CPT_PR_Id_FK == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetComplaintById(CPT_PR_Id_FK);
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
