using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.AdminIRepository;
using static GlobalApi.Models.Master.Individual;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualController : ControllerBase
    {
        public readonly IIndividual _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public readonly IUserRepository userRepository;
        public readonly IAuthenticationRepository authrepository;
        public IndividualController(IAuthenticationRepository authrepository, IUserRepository userRepository)
        {
            this._repository = new IndividualRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
            this.authrepository = authrepository;
            this.userRepository = userRepository;
        }

        [HttpPost, Route("InsertIndividual")]
        public async Task<IActionResult> InsertIndividual([FromBody] Individual_Images lead)
        {
            // var username = "8073043647";
            //var username = User.Identity.Name;
            // var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            // IfClaimExists = claims.Any(x => x.ClaimType == "IndividualAdd" && x.ClaimValue == "Y");
            // if (IfClaimExists)
            // {
            var result = await _repository.InsertIndividual(lead, "");
            if (result.IsSuccess)
            {
                return Ok(new { message = result.Message });
            }
            else
            {
                return BadRequest(new { message = result.Message });
            }
        }
        //  return Unauthorized();




        [HttpPut, Route("UpdateIndividual")]
        public async Task<IActionResult> Put([FromBody] Individual_Images lead)
        {
            //var userName = "8073043647";
            var userName = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
            IfClaimExists = claims.Any(x => x.ClaimType == "IndividualEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var result = await _repository.UpdateIndividual(lead);
                if (result.IsSuccess)
                {
                    return Ok(new { message = result.Message });
                }
                else
                {
                    return BadRequest(new { message = result.Message });
                }
            }
            return Unauthorized();

        }


        [HttpGet, Route("GetAllIndividual")]
        public async Task<IActionResult> GetAllIndividual()
        {
            //var userName = "8073043647";
            var userName = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
            IfClaimExists = claims.Any(x => x.ClaimType == "IndividualView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {

                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var result = await this._repository.GetAllIndividual();
                //return Ok(result);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound("Individual data not found");

            }
            return Unauthorized();

        }


        //[HttpGet, Route("GetAssistant_DD")]
        //public async Task<IActionResult> GetAssistant_DD()
        //{

        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "AssistantView" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var userName = User.Identity.Name.ToString();
        //        var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
        //        var Assi_Hos_Id_FK = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
        //        var result = await this._repository.GetAssistant_DD(Assi_Hos_Id_FK, roleaction);
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound("Assistant data not found");
        //    }
        //    return Unauthorized();


        //}


        [HttpDelete, Route("DeleteIndividual")]
        public async Task<IActionResult> DeleteAssistant(int Individual_id)
        {
            //var userName = "8073043647";
            var userName = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
            IfClaimExists = claims.Any(x => x.ClaimType == "IndividualDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var result = await _repository.DeleteIndividual(Individual_id);
                if (result.IsSuccess)
                {
                    return Ok(new { message = result.Message });
                }
                else
                {
                    return BadRequest(new { message = result.Message });
                }
            }
            return Unauthorized();

        }


        [HttpGet, Route("GetIndividualById")]
        public async Task<IActionResult> GetIndividualById(int Individual_id)
        {
            //var userName = "8073043647";
            var userName = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
            IfClaimExists = claims.Any(x => x.ClaimType == "IndividualView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {

                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var result = await this._repository.GetIndividualById(Individual_id, roleaction);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Individual data not found");
            }
            return Unauthorized();
        }

        [HttpGet, Route("GetIndividualDetails")]
        public async Task<IActionResult> GetAssistantDetails()
        {
            //var userName = "8073043647";
            var userName = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
            IfClaimExists = claims.Any(x => x.ClaimType == "IndividualView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var result = await this._repository.GetIndividualDetails();
                return Ok(result);

            }
            return Unauthorized();
        }

        [HttpPut, Route("ApproveIndividual")]
        public async Task<IActionResult> ApproveAssistant(ApproveIndividual approveIndividual)
        {
            var userName = User.Identity.Name;
            //var userName = "8073043647";
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
            IfClaimExists = claims.Any(x => x.ClaimType == "IndividualApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var result = await _repository.ApproveIndividual(approveIndividual);
                if (result.IsSuccess)
                {
                    return Ok(new { message = result.Message });
                }
                else
                {
                    return BadRequest(new { message = result.Message });
                }
            }
            return Unauthorized();

        }

    }
}
