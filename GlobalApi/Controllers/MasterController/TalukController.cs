using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalukController : ControllerBase
    {
        public readonly ITaluk _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public TalukController()
        {
            this._repository = new TalukRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertTaluk")]
        public async Task<IActionResult> Post([FromBody] Taluk lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "TalukAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertTaluk(lead);

                if (change)
                    return Ok();
                else
                    return BadRequest("Taluk name and code must be unique");
            }
            return Unauthorized();

        }

        [HttpPut, Route("UpdateTaluk")]
        public async Task<IActionResult> Put([FromBody] Taluk lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "TalukEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateTaluk(lead);

                if (change)
                    return Ok();
                else
                    return BadRequest("Taluk name and code must be unique");
            }
            return Unauthorized();

        }

        [HttpGet, Route("GetTaluk_DD")]
        public async Task<IActionResult> GetTaluk_DD(int district_id)
        {
            var result = await this._repository.GetTaluk_DD(district_id);
            if (result.Any())
            {
                return Ok(result);
            }

            return NotFound("Taluk data not found");
        }

        [HttpDelete, Route("DeleteTaluk")]
        public async Task<IActionResult> DeleteTaluk(int Taluk_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "TalukDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteTaluk(Taluk_id);

                if (change)
                    return Ok();
                else
                    return BadRequest("Something went wrong. Please retry after sometime !");
            }
            return Unauthorized();

        }

        [HttpGet, Route("GetAllTaluk")]
        public async Task<IActionResult> GetAllTaluk()
        {

            var result = await this._repository.GetAllTaluk();
            if (result.Any())
            {
                return Ok(result);
            }

            return NotFound("Taluk data not found");
        }

        [HttpPut, Route("ApproveTaluk")]
        public async Task<IActionResult> ApproveTaluk([FromBody] ApproveTaluk lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "TalukApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveTaluk(lead);

                if (change)
                    return Ok();
                else
                    return BadRequest("Something went wrong. Please retry after sometime !");
            }
            return Unauthorized();

        }

    }
}
