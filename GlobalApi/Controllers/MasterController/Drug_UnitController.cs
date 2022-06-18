using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Drug_UnitController : ControllerBase
    {
        public readonly IDrug_UnitRepository _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public Drug_UnitController()
        {
            this._repository = new Drug_UnitRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertDrug_Unit")]
        public async Task<ActionResult<Drug_Units>> Post([FromBody] Drug_Units lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDrug_Unit(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateDrug_Unit")]
        public async Task<ActionResult<Drug_Units>> Put([FromBody] Drug_Units lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDrug_Unit(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllDrug_Unit")]
        public async Task<ActionResult<IEnumerable<Drug_UnitsAll>>> GetAllDrug_Unit()
        {
            try
            {
                var result = await this._repository.GetAllDrug_Unit();
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

        [HttpDelete, Route("DeleteDrug_Unit")]
        public async Task<ActionResult> DeleteDrug_Unit(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDrug_Unit(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetDD_Drug_Unit")]
        public async Task<ActionResult<IEnumerable<Drug_UnitDD>>> GetDrugTypeDD()
        {
            try
            {
                var result = await this._repository.GetDD_Drug_Unit();
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
        [HttpPut, Route("ApproveDrug_Unit")]
        public async Task<IActionResult> ApproveDrug_Unit([FromBody] ApproveDrgunit lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DrugUnitApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveDrug_Unit(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();

        }
    }
}
