using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssistantController : ControllerBase
    {
        public readonly IAssistant _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public AssistantController()
        {
            this._repository = new AssistantRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertAssistant")]
        public async Task<IActionResult> Post([FromForm] Assistant_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "AssistantAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertAssistant(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        
        [HttpPut, Route("UpdateAssistant")]
        public async Task<IActionResult> Put([FromForm] Assistant_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "AssistantEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateAssistant(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        
        [HttpGet, Route("GetAllAssistant")]
        public async Task<IActionResult> GetAllAssistant()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "AssistantView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var Assi_Hos_Id_FK = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
                    var result = await this._repository.GetAllAssistant(Assi_Hos_Id_FK, roleaction);
                    if (result.Any())
                    {
                        return Ok(result);
                    }

                    return NotFound();
                }
                return Unauthorized();
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        
        [HttpGet, Route("GetAssistant_DD")]
        public async Task<IActionResult> GetAssistant_DD()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "AssistantView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var Assi_Hos_Id_FK = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
                    var result = await this._repository.GetAssistant_DD(Assi_Hos_Id_FK, roleaction);
                    if (result.Any())
                    {
                        return Ok(result);
                    }

                    return NotFound();
                }
                return Unauthorized();
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        
        [HttpDelete, Route("DeleteAssistant")]
        public async Task<IActionResult> DeleteAssistant(int Assistant_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "AssistantDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                if (Assistant_id == 0)
                {
                    return BadRequest();
                }
                var change = await _repository.DeleteAssistant(Assistant_id);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        
        [HttpGet, Route("GetAssistantById")]
        public async Task<IActionResult> GetAssistantById(int Assistant_id)
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "AssistantView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var result = await this._repository.GetAssistantById(Assistant_id, roleaction);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return Ok(result);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        
        [HttpGet, Route("GetAssistant_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Assistant/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }

        [HttpPut, Route("ApproveAssistant")]
        public async Task<IActionResult> ApproveAssistant(int Assi_Id, string? Remarks)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "AssistantApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveAssistant(Assi_Id, Remarks);

                if (change != null)
                    return Ok(change);
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

    }
}
