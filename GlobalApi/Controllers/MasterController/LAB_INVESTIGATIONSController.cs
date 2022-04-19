using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LAB_INVESTIGATIONSController : ControllerBase
    {
        public readonly ILAB_INVESTIGATIONS _repository;
        public LAB_INVESTIGATIONSController(ILAB_INVESTIGATIONS repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertLAB_INVESTIGATIONS")]
        public async Task<ActionResult<LAB_INVESTIGATIONS>> Post([FromBody] LAB_INVESTIGATIONS lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertLAB_INVESTIGATIONS(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateLAB_INVESTIGATIONS")]
        public async Task<ActionResult<LAB_INVESTIGATIONS>> Put([FromBody] LAB_INVESTIGATIONS lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateLAB_INVESTIGATIONS(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetLAB_INVESTIGATIONS")]
        public async Task<ActionResult<IEnumerable<LAB_INVESTIGATIONS>>> GetLAB_INVESTIGATIONS()
        {
            try
            {
                var result = await this._repository.GetLAB_INVESTIGATIONS();
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
        
        [HttpGet, Route("GetLabInsv_DD")]
        public async Task<ActionResult<IEnumerable<LabInsv_DD>>> GetLabInsv_DD()
        {
            try
            {
                var result = await this._repository.GetLabInsv_DD();
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
        
        [HttpDelete, Route("DeleteLAB_INVESTIGATIONS")]
        public async Task<ActionResult> DeleteLAB_INVESTIGATIONS(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteLAB_INVESTIGATIONS(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetLabInsvBy_Id")]
        public async Task<ActionResult<IEnumerable<LabInsvBy_Id>>> GetLabInsvBy_Id(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetLabInsvBy_Id(Id);
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
