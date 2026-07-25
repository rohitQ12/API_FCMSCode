using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        public readonly INetwork _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public NetworkController()
        {
            this._repository = new NetworkRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertNetwork")]
        public async Task<IActionResult> Post([FromBody] Network Network)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "NetworkAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertNetwork(Network);

                if (change == "Network Added Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }

        [HttpPut, Route("UpdateNetwork")]
        public async Task<IActionResult> Put([FromBody] Network Network)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "NetworkEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateNetwork(Network);

                if (change == "Network Updated Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();
        }

        [HttpGet, Route("GetAllNetwork")]
        public async Task<IActionResult> GetAllNetwork()
        {
            var result = await this._repository.GetAllNetwork();
            return Ok(result);
        }

        [HttpGet, Route("GetNetwork_DD")]
        public async Task<IActionResult> GetNetwork_DD()
        {
            var result = await this._repository.GetNetwork_DD();
            return Ok(result);
        }

        [HttpDelete, Route("DeleteNetwork")]
        public async Task<IActionResult> DeleteNetwork(int NE_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "NetworkDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteNetwork(NE_Id);

                if (change == "Network Deleted Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();
        }

        [HttpGet, Route("GetNetworkById")]
        public async Task<IActionResult> GetNetworkById(int NE_Id)
        {
            var result = await this._repository.GetNetworkById(NE_Id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Network not found");
        }

        [HttpPut, Route("ApproveNetwork")]
        public async Task<IActionResult> ApproveNetwork(ApproveNetwork ApproveNetwork)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "NetworkApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveNetwork(ApproveNetwork);

                if (change == "Network Approved Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }
    }
}
