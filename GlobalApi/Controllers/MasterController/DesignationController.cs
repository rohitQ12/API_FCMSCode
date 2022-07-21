using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        public readonly IDesignation _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public DesignationController()
        {
            this._repository = new DesignationRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertDesignation")]
        public async Task<IActionResult> Post([FromBody] Designation lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DesignationAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertDesignation(lead);

                if (change)
                    return Ok();
                else
                    return BadRequest("Designation name and code must be unique");
            }
            return Unauthorized();

        }


        [HttpPut, Route("UpdateDesignation")]
        public async Task<IActionResult> Put([FromBody] Designation lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DesignationEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateDesignation(lead);

                if (change)
                    return Ok();
                else
                    return BadRequest("Designation name and code must be unique");
            }
            return Unauthorized();

        }


        [HttpGet, Route("GetAllDesignation")]
        public async Task<IActionResult> GetAllDesignation()
        {
            var result = await this._repository.GetAllDesignation();
            if (result.Any())
            {
                return Ok(result);
            }

            return NotFound("Designation data not found");
        }


        [HttpGet, Route("GetDesignation_DD")]
        public async Task<IActionResult> GetDesignation_DD()
        {

            var result = await this._repository.GetDesignation_DD();
            if (result.Any())
            {
                return Ok(result);
            }

            return NotFound("Designation data not found");

        }


        [HttpDelete, Route("DeleteDesignation")]
        public async Task<IActionResult> DeleteDesignation(int designation_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DesignationDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteDesignation(designation_id);

                if (change)
                    return Ok();
                else
                    return BadRequest("Something went wrong. Please retry after sometime !");
            }
            return Unauthorized();

        }


        [HttpGet, Route("GetDesignationById")]
        public async Task<IActionResult> GetDesignationById(int designation_id)
        {

            var result = await this._repository.GetDesignationById(designation_id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Designation data not found");


        }

        [HttpPut, Route("ApproveDesignation")]
        public async Task<IActionResult> ApproveDesignation([FromBody] ApproveDesignation lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DesignationApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveDesignation(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Something went wrong. Please retry after sometime !");
            }
            return Unauthorized();

        }
    }
}
