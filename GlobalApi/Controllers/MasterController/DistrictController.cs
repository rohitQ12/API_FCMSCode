using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.GlobalClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DistrictController : ControllerBase
    {
        public readonly IDistrict _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public DistrictController()
        {
            this._repository = new DistrictRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertDistrict")]
        public async Task<IActionResult> Post([FromBody] Districts lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DistrictAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertDistrict(lead);

                if (change)
                {
                    return Ok();
                }

                return BadRequest();
            }
            return Unauthorized();

        }

        [HttpPut, Route("UpdateDistrict")]
        public async Task<IActionResult> Put([FromBody] Districts lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DistrictEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateDistrict(lead);

                if (change)
                {
                    return Ok();
                }

                return BadRequest();
            }
            return Unauthorized();

        }

        [HttpGet, Route("GetDistrict_DD")]
        public async Task<IActionResult> GetDistrict_DD(int stat_id)
        {
            var result = await this._repository.GetDistrict_DD(stat_id);
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound();

        }

        [HttpDelete, Route("DeleteDistrict")]
        public async Task<IActionResult> DeleteDistrict(int district_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DistrictDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteDistrict(district_id);

                if (change)
                {
                    return Ok();
                }
                return BadRequest();
            }
            return Unauthorized();

        }

        [HttpGet, Route("GetDistrictById")]
        public async Task<IActionResult> GetDistrictById(int district_id)
        {

            var result = await this._repository.GetDistrictById(district_id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }

        [HttpGet, Route("GetAllDistrict")]
        public async Task<IActionResult> GetAllDistrict()
        {

            var result = await this._repository.GetAllDistrict();
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound();

        }

        [HttpPut, Route("ApproveDistrict")]
        public async Task<IActionResult> ApproveDistrict([FromBody] ApproveDistrict lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DistrictApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveDistrict(lead);

                if (change)
                {
                    return Ok();
                }
                return BadRequest();
            }
            return Unauthorized();

        }
    }
}
