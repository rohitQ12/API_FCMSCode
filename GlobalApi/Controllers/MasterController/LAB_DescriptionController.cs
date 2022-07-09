using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LAB_DescriptionController : ControllerBase
    {
        public readonly ILAB_Description _repository;
        public LAB_DescriptionController()
        {
            this._repository = new LAB_DescriptionRepository();
        }

        [HttpPost, Route("InsertLab_Description")]
        public async Task<ActionResult<LAB_Description>> Post([FromBody] LAB_Description lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertLab_Description(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateLab_Description")]
        public async Task<ActionResult<LAB_Description>> Put([FromBody] LAB_Description lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateLab_Description(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllLab_Description")]
        public async Task<ActionResult<IEnumerable<GetAllLAB_Desc>>> GetAllLab_Description()
        {
            try
            {
                var result = await this._repository.GetAllLab_Description();
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

        [HttpGet, Route("GetLabDesc_DD")]
        public async Task<ActionResult<IEnumerable<LabDesc_DD>>> GetLabDesc_DD()
        {
            try
            {
                var result = await this._repository.GetLabDesc_DD();
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

        [HttpDelete, Route("DeleteLab_Description")]
        public async Task<ActionResult> DeleteLab_Description(int Lab_DescId)
        {
            if (Lab_DescId <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteLab_Description(Lab_DescId);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetLabDescBy_Id")]
        public async Task<ActionResult<IEnumerable<LabSubInsvBy_Id>>> GetLabDesc_ById(int Lab_DescId)
        {
            if (Lab_DescId == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetLabDesc_ById(Lab_DescId);
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

        [HttpGet, Route("LabDesc_DD_ByCat_Id")]
        public async Task<ActionResult<IEnumerable<LabDesc_DD>>> LabDesc_DD_ByCat_Id(int Cat_Id)
        {
            try
            {
                var result = await this._repository.LabDesc_DD_ByCat_Id(Cat_Id);
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
        
        [HttpPut, Route("ApproveLAB_Description")]
        public async Task<ActionResult> ApproveVle([FromBody] ApproveLab_Desc lead)
        {
            if (lead.Lab_DescId <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.ApproveLAB_Description(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

    }
}
