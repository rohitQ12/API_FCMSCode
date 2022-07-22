using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;
//using log4net;
using NLog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        public readonly Istate _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public StateController()
        {
            this._repository = StateRepository.Getinstance;
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertState")]
        public async Task<IActionResult> Post([FromBody] States lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "StateAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertState(lead);

                if (change == "State Added Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }

        [HttpPut, Route("UpdateState")]
        public async Task<IActionResult> Put([FromBody] States lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "StateEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateState(lead);

                if (change == "State Updated Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }

        [HttpGet, Route("GetAllState")]
        public async Task<IActionResult> GetAllState()
        {
            var result = await this._repository.GetAllState();
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound("State data not found");
        }

        [HttpGet, Route("GetState_DD")]
        public async Task<IActionResult> GetState_DD(int cntry_id)
        {

            var result = await this._repository.GetState_DD(cntry_id);
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound("State data not found");
        }

        [HttpDelete, Route("DeleteState")]
        public async Task<IActionResult> DeleteState(int stat_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "StateDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteState(stat_id);

                if (change == "State Deleted Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }

        [HttpGet, Route("GetStateById")]
        public async Task<IActionResult> GetStateById(int stat_id)
        {
            var result = await this._repository.GetStateById(stat_id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("State data not found");
        }

        [HttpPut, Route("ApproveState")]
        public async Task<IActionResult> ApproveState([FromBody] ApproveState lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "StateApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveState(lead);
                if (change == "State Approved Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();
        }
    }
}
