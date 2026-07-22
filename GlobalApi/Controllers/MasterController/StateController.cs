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
            this._repository = new StateRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertState")]
        public async Task<IActionResult> Post([FromBody] States states)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "StateAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertState(states);

                if (change == "State Added Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }

        [HttpPut, Route("UpdateState")]
        public async Task<IActionResult> Put([FromBody] States states)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "StateEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateState(states);

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
            return Ok(result);
        }

        [HttpGet, Route("GetState_DD")]
        public async Task<IActionResult> GetState_DD(int cntry_id)
        {
            var result = await this._repository.GetState_DD(cntry_id);
            return Ok(result);
        }

        [HttpGet, Route("GetState_DD_Mobile")]
        public async Task<IActionResult> GetState_DD_Mobile(int cntry_id)
        {
            List<NoStateFound> noStateList = new List<NoStateFound>();
            if (cntry_id == 0)
            {
                noStateList.Add(new NoStateFound { stat_id = 0, state_code = "S00", state_name = "State not found" });
                return Ok(noStateList);
            }

            var result = await this._repository.GetState_DD(cntry_id);
            if (result.Count > 0)
            {
                List<NoStateFound> DefStateList = result.Select(state => new NoStateFound
                {
                    stat_id = state.stat_id,
                    state_code = state.state_code,
                    state_name = state.state_name
                }).ToList();

                NoStateFound defaultState = new NoStateFound { stat_id = 0, state_code = "S00", state_name = "Select state" };

                DefStateList.Insert(0, defaultState);
                return Ok(DefStateList);
            }

            noStateList.Add(new NoStateFound { stat_id = 0, state_code = "S00", state_name = "State not found" });
            return Ok(noStateList);

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
            return Ok(result);

        }

        [HttpPut, Route("ApproveState")]
        public async Task<IActionResult> ApproveState([FromBody] ApproveState approvestate)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "StateApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveState(approvestate);
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
