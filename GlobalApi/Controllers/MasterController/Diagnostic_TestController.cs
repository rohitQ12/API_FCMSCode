using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Diagnostic_TestController : ControllerBase
    {
        public readonly IDiagnostic_Test _repository;
        public Diagnostic_TestController()
        {
            this._repository = new Diagnostic_TestRepository();
        }

        [HttpPost, Route("InsertDiagnostic_Test")]
        public async Task<ActionResult<Diagnostic_Test>> Post([FromBody] Diagnostic_Test lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDiagnostic_Test(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }


        [HttpPut, Route("UpdateDiagnostic_Test")]
        public async Task<ActionResult<Diagnostic_Test>> Put([FromBody] Diagnostic_Test lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDiagnostic_Test(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }


        [HttpGet, Route("GetAllDiagnostic_Test")]
        public async Task<ActionResult<IEnumerable<GetAllDiagno_Test>>> GetAllDiagnostic_Test()
        {
            try
            {
                var result = await this._repository.GetAllDiagnostic_Test();
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


        [HttpGet, Route("GetDiagnostic_Test_DD")]
        public async Task<ActionResult<IEnumerable<Diagno_TestDD>>> GetDiagnostic_Test_DD(int Cat_Id)
        {
            try
            {
                var result = await this._repository.GetDiagnostic_Test_DD(Cat_Id);
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


        [HttpDelete, Route("DeleteDiagnostic_Test")]
        public async Task<ActionResult> DeleteDiagnostic_Test(int DT_Id)
        {
            if (DT_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDiagnostic_Test(DT_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }


        [HttpGet, Route("GetDiagnostic_TestById")]
        public async Task<ActionResult<IEnumerable<GetDiagno_TestById>>> GetDiagnostic_TestById(int DT_Id)
        {
            if (DT_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDiagnostic_TestById(DT_Id);
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

        //[HttpPut, Route("ApproveDiagnosticTest")]
        //public async Task<IActionResult> ApproveDiagnostic_Test([FromBody] Diagnostic_Test lead)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "CountryApprove" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.ApproveDiagnostic_Test(lead);

        //        if (change)
        //        {
        //            return Ok();
        //        }
        //        return BadRequest("Not successfull");
        //    }
        //    return Unauthorized();

        //}

        [HttpPut, Route("ApproveDiagnostic_Test")]
        public async Task<ActionResult> ApproveDiagnostic_Test([FromBody] ApproveDiagno_Test lead)
        {
            if (lead.DT_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.ApproveDiagnostic_Test(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

    }
}
