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
            string username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DrugsAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertDrugMaster(lead);

            if (change == "Drug added Successfully")
                return Ok();
            else
                return BadRequest(change);
        }

        [HttpPut, Route("UpdateDrugMaster")]
        public async Task<IActionResult> Put([FromBody] DrugMaster lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DrugsEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {

                var change = await _repository.UpdateDrugMaster(lead);

            if (change == "Drug updated Successfully")
                return Ok();
            else
                return BadRequest(change);
        }

        [HttpGet, Route("GetAllDrugMaster")]
        public async Task<ActionResult<IEnumerable<GetAllDrugMaster>>> GetAllDrugMaster()
        {
           
                var result = await this._repository.GetAllDrugMaster();
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound("Drugs not found");
        }

        [HttpDelete, Route("DeleteDrugMaster")]
        public async Task<ActionResult> DeleteDrugMaster(int Id)
        {
            var change = await _repository.DeleteDrugMaster(Id);

            if (change == "Drug deleted Successfully")
                return Ok();
            else
                return BadRequest(change);
        }

        [HttpGet, Route("GetDrugMasterById")]
        public async Task<ActionResult<IEnumerable<GetAllDrugMaster>>> GetDrugMasterById(int Id)
        {

                var result = await this._repository.GetDrugMasterById(Id);
                if (result == null)
                {
                    return NotFound("Drug not found");
                }
                return Ok(result);

        }
        [HttpGet, Route("GetDrugMaster_DD")]
        public async Task<ActionResult<IEnumerable<DrugMasterDD>>> GetDD()
        {
        
                var result = await this._repository.GetDrugMaster_DD();
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound("Drugs not found");
            
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

                if (change == "Drug Approved Successfully")
                {
                    return Ok();
                }
                    
                else
                    return BadRequest(change);
            }
            return Unauthorized();

        }

    }
}
