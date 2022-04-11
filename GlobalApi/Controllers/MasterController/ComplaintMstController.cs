using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintMstController : ControllerBase
    {
        public readonly IComplaintMst _repository;
        public ComplaintMstController(IComplaintMst repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertComplaintMst")]
        public async Task<ActionResult<ComplaintMst>> Post([FromBody] ComplaintMst lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertComplaintMst(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateComplaintMst")]
        public async Task<ActionResult<ComplaintMst>> Put([FromBody] ComplaintMst lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateComplaintMst(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllComplaintMst")]
        public async Task<ActionResult<IEnumerable<ComplaintMst>>> GetAllComplaintMst()
        {
            try
            {
                var result = await this._repository.GetAllComplaintMst();
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
        [HttpGet, Route("GetComplaintMst_DD")]
        public async Task<ActionResult<IEnumerable<ComplaintMst_DD>>> GetComplaintMst_DD()
        {
            try
            {
                var result = await this._repository.GetComplaintMst_DD();
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
        [HttpDelete, Route("DeleteComplaintMst")]
        public async Task<ActionResult> DeleteComplaintMst(int Cmst_Id)
        {
            if (Cmst_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteComplaintMst(Cmst_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetComplaintMstBy_Id")]
        public async Task<ActionResult<IEnumerable<ComplaintMst>>> GetComplaintMstBy_Id(int Cmst_Id)
        {
            if (Cmst_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetComplaintMstById(Cmst_Id);
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
