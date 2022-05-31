using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.GlobalClasses;
using GlobalApi.Models.Authentication;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticCentersController : ControllerBase
    {
        public readonly IDiagnosticCenters _repository;
        public readonly FindUserId findUserId;
        public DiagnosticCentersController()
        {
            this._repository = new DiagnosticCentersRepository();
            this.findUserId = new FindUserId();
        }

        [HttpPost, Route("Admin/InsertDiagnosticCenters")]
        public async Task<ActionResult<DiagnosticCenters>> AdminPost([FromForm] Diagnostic_Images lead)
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
        public async Task<ActionResult<DiagnosticCenters>> SelfPost([FromForm] Diagnostic_Images lead)
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
        public async Task<ActionResult<DiagnosticCenters>> AdminPut([FromForm] Diagnostic_Images lead)
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
        public async Task<ActionResult<DiagnosticCenters>> SelfPut([FromForm] Diagnostic_Images lead)
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
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var DCId = await this.findUserId.FindDCIdFromDCOfficeUsername(userName);
                var result = await this._repository.GetAllDiagnosticCenters(DCId, roleaction);
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
        [HttpGet, Route("Admin/GetDiagnosticCategory_DD")]
        public async Task<ActionResult<IEnumerable<Usercategory_DD>>> GetDiagnosticCategory_DD()
        {
            try
            {
                var result = await this._repository.GetDiagnosticCategory_DD();
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
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var DCId = await this.findUserId.FindDCIdFromDCOfficeUsername(userName);
                var result = await this._repository.GetDiagnosticCenters_DD(DCId, roleaction);
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
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var DCId = await this.findUserId.FindDCIdFromDCOfficeUsername(userName);
                var result = await this._repository.GetDiagnosticCenters_DD(DCId, roleaction);
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
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var DCId = await this.findUserId.FindDCIdFromDCOfficeUsername(userName);
                var result = await this._repository.GetDiagnosticCentersById(DGSTC_Id, roleaction);
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
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var DCId = await this.findUserId.FindDCIdFromDCOfficeUsername(userName);
                var result = await this._repository.GetDiagnosticCentersById(DGSTC_Id, roleaction);
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

        [HttpPut, Route("ApproveDiagnosticCenter")]
        public async Task<ActionResult> ApproveDiagnosticCenter(int DGSTC_Id, string? Remarks)
        {
            if (DGSTC_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.ApproveDiagnosticCenter(DGSTC_Id, Remarks);

            if (change != null)
                return Ok(change);
            else
                return BadRequest("Not successfull");
        }

    }
}
