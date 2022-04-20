using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticCentersController : ControllerBase
    {
        public readonly IDiagnosticCenters _repository;
        public DiagnosticCentersController(IDiagnosticCenters repository)
        {
            this._repository = new DiagnosticCentersRepository();
        }

        [HttpPost, Route("Admin/InsertDiagnosticCenters")]
        public async Task<ActionResult<DiagnosticCenters>> AdminPost([FromBody] DiagnosticCenters lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDiagnosticCenters(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPost, Route("Self/InsertDiagnosticCenters")]
        public async Task<ActionResult<DiagnosticCenters>> SelfPost([FromBody] DiagnosticCenters lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDiagnosticCenters(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("Admin/UpdateDiagnosticCenters")]
        public async Task<ActionResult<DiagnosticCenters>> AdminPut([FromBody] DiagnosticCenters lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDiagnosticCenters(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("Self/UpdateDiagnosticCenters")]
        public async Task<ActionResult<DiagnosticCenters>> SelfPut([FromBody] DiagnosticCenters lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDiagnosticCenters(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllDiagnosticCenters")]
        public async Task<ActionResult<IEnumerable<DiagnosticCenters>>> GetAllDiagnosticCenters()
        {
            try
            {
                var result = await this._repository.GetAllDiagnosticCenters();
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
        [HttpGet, Route("Admin/GetDiagnosticCenters_DD")]
        public async Task<ActionResult<IEnumerable<DiagnosticCenters_DD>>> AdminGetDiagnosticCenters_DD()
        {
            try
            {
                var result = await this._repository.GetDiagnosticCenters_DD();
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

        [HttpGet, Route("Self/GetDiagnosticCenters_DD")]
        public async Task<ActionResult<IEnumerable<DiagnosticCenters_DD>>> SelfGetDiagnosticCenters_DD()
        {
            try
            {
                var result = await this._repository.GetDiagnosticCenters_DD();
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
        [HttpDelete, Route("DeleteDiagnosticCenters")]
        public async Task<ActionResult> DeleteDiagnosticCenters(int DGSTC_Id)
        {
            if (DGSTC_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDiagnosticCenters(DGSTC_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("Admin/GetDiagnosticCentersById")]
        public async Task<ActionResult<IEnumerable<DiagnosticCentersById>>> AdminGetDiagnosticCentersById(int DGSTC_Id)
        {
            if (DGSTC_Id == 0)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDiagnosticCentersById(DGSTC_Id);
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

        [HttpGet, Route("Self/GetDiagnosticCentersById")]
        public async Task<ActionResult<IEnumerable<DiagnosticCentersById>>> SelfGetDiagnosticCentersById(int DGSTC_Id)
        {
            if (DGSTC_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDiagnosticCentersById(DGSTC_Id);
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
