using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticTypeController : ControllerBase
    {
        public readonly IDiagnosticType _repository;
        public DiagnosticTypeController()
        {
            this._repository = new DiagnosticTypeRepository();
        }

        [HttpPost, Route("InsertDiagnosticType")]
        public async Task<ActionResult<DiagnosticType>> Post([FromBody] DiagnosticType lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDiagnosticType(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateDiagnosticType")]
        public async Task<ActionResult<DiagnosticType>> Put([FromBody] DiagnosticType lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDiagnosticType(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllDiagnosticType")]
        public async Task<ActionResult<IEnumerable<DiagnosticType>>> GetAllDiagnosticType()
        {
            try
            {
                var result = await this._repository.GetAllDiagnosticType();
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

        [HttpGet, Route("GetDiagnosticType_DD")]
        public async Task<ActionResult<IEnumerable<HosType_DD>>> GetDiagnosticType_DD()
        {
            try
            {
                var result = await this._repository.GetDiagnosticType_DD();
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

        [HttpDelete, Route("DeleteDiagnosticType")]
        public async Task<ActionResult> DeleteDiagnosticType(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDiagnosticType(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

    }
}
