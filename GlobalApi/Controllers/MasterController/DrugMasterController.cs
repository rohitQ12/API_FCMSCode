using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugMasterController : ControllerBase
    {
        public readonly IDrugMaster _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public DrugMasterController()
        {
            this._repository = new DrugMasterRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertDrugMaster")]
        public async Task<ActionResult<DrugMaster>> Post([FromBody] DrugMaster lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDrugMaster(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateDrugMaster")]
        public async Task<IActionResult> Put([FromBody] DrugMaster lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDrugMaster(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllDrugMaster")]
        public async Task<ActionResult<IEnumerable<GetAllDrugMaster>>> GetAllDrugMaster()
        {
            try
            {
                var result = await this._repository.GetAllDrugMaster();
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

        [HttpDelete, Route("DeleteDrugMaster")]
        public async Task<ActionResult> DeleteDrugMaster(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDrugMaster(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetDrugMasterById")]
        public async Task<ActionResult<IEnumerable<GetAllDrugMaster>>> GetDrugMasterById(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDrugMasterById(Id);
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
        [HttpGet, Route("GetDrugMaster_DD")]
        public async Task<ActionResult<IEnumerable<DrugMasterDD>>> GetDD()
        {
            try
            {
                var result = await this._repository.GetDrugMaster_DD();
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
        [HttpPut, Route("ApproveDrugMaster")]
        public async Task<IActionResult> ApproveDiscipline([FromBody] ApproveDrgMst lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DrugsApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveDrugMaster(lead);

                if (change)
                    return Ok();
                else
                    return BadRequest();
            }
            return Unauthorized();

        }

    }
}
