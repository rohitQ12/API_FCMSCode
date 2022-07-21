using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.GlobalClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
        public readonly IQualification _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public QualificationController()
        {
            this._repository = new QualificationRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertQualification")]
        public async Task<IActionResult> Post([FromBody] Qualification lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertQualification(lead);

                if (change)
                    return Ok();
                else
                    return BadRequest("Qualification name and code must be unique");
            }
            return Unauthorized();
            
        }
        
        [HttpPut, Route("UpdateQualification")]
        public async Task<IActionResult> Put([FromBody] Qualification lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateQualification(lead);

                if (change)
                    return Ok();
                else
                    return BadRequest("Qualification name and code must be unique");
            }
            return Unauthorized();
            
        }
        
        [HttpGet, Route("GetAllQualification")]
        public async Task<IActionResult> GetAllQualification()
        {
                var result = await this._repository.GetAllQualification();
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound("Qualification data not found");
        }
        
        [HttpGet, Route("GetQualification_DD")]
        public async Task<IActionResult> GetQualification_DD()
        {

                var result = await this._repository.GetQualification_DD();
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound("Qualification data not found");
        }
        
        [HttpDelete, Route("DeleteQualification")]
        public async Task<IActionResult> DeleteQualification(int qualification_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteQualification(qualification_id);
                if (change)
                    return Ok();
                else
                    return BadRequest("Something went wrong. Please retry after sometime !");
            }
            return Unauthorized();
            
        }
        
        [HttpGet, Route("GetQualificationById")]
        public async Task<IActionResult> GetQualificationById(int qualification_id)
        {
                var result = await this._repository.GetQualificationById(qualification_id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Qualification data not found");

        }

        [HttpPut, Route("ApproveQualification")]
        public async Task<IActionResult> ApproveQualification([FromBody] ApproveQualification lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveQualification(lead);

                if (change)
                    return Ok();
                else
                    return BadRequest("Something went wrong. Please retry after sometime !");
            }
            return Unauthorized();
            
        }
    }
}
