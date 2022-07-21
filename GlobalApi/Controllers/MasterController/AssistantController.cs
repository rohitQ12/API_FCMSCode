using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.AdminIRepository;

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
        public readonly IUserRepository userRepository;
        public readonly IAuthenticationRepository authrepository;
        public AssistantController(IAuthenticationRepository authrepository, IUserRepository userRepository)
        {
            this._repository = new AssistantRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
            this.authrepository = authrepository;
            this.userRepository = userRepository;
        }

        [HttpPost, Route("InsertAssistant")]
        public async Task<IActionResult> Post([FromForm] Assistant_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "AssistantAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                string phonenumber = lead.Assi_MobileNumber.ToString();
                string password = (lead.Assi_FirstName.Substring(0, 1)).ToUpper() + lead.Assi_FirstName.Substring(1, 2).ToLower() + "/" + phonenumber.Substring(0, 3);
                var result = await this.authrepository.RegisterUserAsync(lead.Assi_FirstName,
                lead.Assi_LastName, phonenumber, lead.Assi_Email, password, "40ea3dcb-e728-4e1b-a42f-934977114b1a", lead.Assi_Hos_Id_FK, lead.Assi_Photo);

                if (result.IsSuccess)
                {
                    var change = await _repository.InsertAssistant(lead, result.userid);
                    if (change != null)
                    {
                        return Ok();
                    }
                    var delete = await this.findUserId.Deleteuser(result.userid);
                    return BadRequest("Assistant Details Already Exist");
                }
                return BadRequest(result.Message);
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
                {
                    var profile = await userRepository.UpdateUserProfile(change.Asssi_UserID, lead.Assi_Photo, lead.Assi_Email,
                    lead.Assi_MobileNumber.ToString(), lead.Assi_FirstName, lead.Assi_LastName, lead.Assi_Gender, lead.Assi_DOB);
                    if (profile != null)
                    {
                        return Ok();
                    }
                    return BadRequest("Assistant User details not Updated successfull, Please retry after sometime!");
                }
                return BadRequest("Assistant details not Updated successfull, Please retry after sometime!");
            }
            return Unauthorized();

        }


        [HttpGet, Route("GetAllAssistant")]
        public async Task<IActionResult> GetAllAssistant()
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
                return NotFound("Assistant data not found");
            }
            return Unauthorized();

        }


        [HttpGet, Route("GetAssistant_DD")]
        public async Task<IActionResult> GetAssistant_DD()
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

                return NotFound("Assistant data not found");
            }
            return Unauthorized();


        }


        [HttpDelete, Route("DeleteAssistant")]
        public async Task<IActionResult> DeleteAssistant(int Assistant_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "AssistantDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteAssistant(Assistant_id);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Something went wrong.Please retry after sometime!");
            }
            return Unauthorized();

        }


        [HttpGet, Route("GetAssistantById")]
        public async Task<IActionResult> GetAssistantById(int Assistant_id)
        {

            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "AssistantView" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var result = await this._repository.GetAssistantById(Assistant_id, roleaction);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Assistant data not found");
            }
            return Unauthorized();
        }


        [HttpGet, Route("GetAssistant_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Assistant/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }

        [HttpPut, Route("ApproveAssistant")]
        public async Task<IActionResult> ApproveAssistant([FromBody] ApproveAssistant approveAssistant)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "AssistantApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveAssistant(approveAssistant);

                if (change)
                    return Ok();
                else
                    return BadRequest("Something went wrong.Please retry after sometime!");
            }
            return Unauthorized();

        }

    }
}
