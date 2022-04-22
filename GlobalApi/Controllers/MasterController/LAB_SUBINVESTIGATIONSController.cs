using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LAB_SUBINVESTIGATIONSController : ControllerBase
    {
        public readonly ILAB_SUBINVESTIGATIONS _repository;
        public LAB_SUBINVESTIGATIONSController()
        {
            this._repository = new LAB_SUBINVESTIGATIONSRepository();
        }

        [HttpPost, Route("InsertLAB_SUBINVESTIGATIONS")]
        public async Task<ActionResult<LAB_SUBINVESTIGATIONS>> Post([FromBody] LAB_SUBINVESTIGATIONS lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertLAB_SUBINVESTIGATIONS(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateLAB_SUBINVESTIGATIONS")]
        public async Task<ActionResult<LAB_SUBINVESTIGATIONS>> Put([FromBody] LAB_SUBINVESTIGATIONS lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateLAB_SUBINVESTIGATIONS(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetLAB_SUBINVESTIGATIONS")]
        public async Task<ActionResult<IEnumerable<GetLabSubInsv>>> GetLAB_SUBINVESTIGATIONS()
        {
            try
            {
                var result = await this._repository.GetLAB_SUBINVESTIGATIONS();
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
        
        [HttpGet, Route("GetLabSubInsv_DD")]
        public async Task<ActionResult<IEnumerable<LabSubInsv_DD>>> GetLabSubInsv_DD(int Lab_Invst_Id)
        {
            try
            {
                var result = await this._repository.GetLabSubInsv_DD(Lab_Invst_Id);
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
        
        [HttpDelete, Route("DeleteLAB_SUBINVESTIGATIONS")]
        public async Task<ActionResult> DeleteLAB_SUBINVESTIGATIONS(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteLAB_SUBINVESTIGATIONS(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetLabSubInsvBy_Id")]
        public async Task<ActionResult<IEnumerable<LabSubInsvBy_Id>>> GetLabSubInsvBy_Id(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetLabSubInsvBy_Id(Id);
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
