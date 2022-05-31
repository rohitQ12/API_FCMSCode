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
        public async Task<IActionResult> Post([FromBody] Network lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "NetworkAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertNetwork(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        [HttpPut, Route("UpdateNetwork")]
        public async Task<IActionResult> Put([FromBody] Network lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "NetworkEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateNetwork(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        [HttpGet, Route("GetAllNetwork")]
        public async Task<IActionResult> GetAllNetwork()
        {
            try
            {
                var result = await this._repository.GetAllNetwork();
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
        
        [HttpGet, Route("GetNetwork_DD")]
        public async Task<IActionResult> GetNetwork_DD()
        {
            try
            {
                var result = await this._repository.GetNetwork_DD();
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
        
        [HttpDelete, Route("DeleteNetwork")]
        public async Task<IActionResult> DeleteNetwork(int NE_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "NetworkDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteNetwork(NE_Id);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        [HttpGet, Route("GetNetworkById")]
        public async Task<IActionResult> GetNetworkById(int NE_Id)
        {

            try
            {
                var result = await this._repository.GetNetworkById(NE_Id);
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
        
        [HttpPut, Route("ApproveNetwork")]
        public async Task<IActionResult> ApproveNetwork(int NE_Id, string? Remarks)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "NetworkApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveNetwork(NE_Id, Remarks);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
    }
}
