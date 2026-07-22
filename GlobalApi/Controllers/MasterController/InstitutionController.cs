using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using GlobalApi.Models.Authentication;
using GlobalApi.GlobalClasses;
using System.Security.Claims;


namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        public readonly IInstitution _repository;
        public readonly FindUserId findUserId;
        private bool IfClaimExists = false;
        private IEnumerable<Claim> claims = null;
        private readonly ClaimsAuthorization claimsAuthorization;
        public InstitutionController()
        {
            this._repository = new InstitutionRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        //saheb
        [HttpPost, Route("Admin/InsertInstitution")]
        public async Task<IActionResult> InsertInstitution([FromBody] Institution_Images lead)
        {

            var username = User.Identity.Name;
            //var username = "8778650328";
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                          IfClaimExists = claims.Any(x => x.ClaimType == "InstitutionAdd" && x.ClaimValue == "Y");
            //if (IfClaimExists)
            //{
               // var test = HttpContext.Request.Form.Files;
                var result = await _repository.InsertInstitution(lead);
            if (result.IsSuccess)
            {
                return Ok(new { message = result.Message });
            }
            else
            {
                return BadRequest(new { message = result.Message });
            }

        }


        [HttpPut, Route("Admin/UpdateInstitution")]
        public async Task<IActionResult> AdminPut([FromBody] Institution_Images lead)
        {
           var username = User.Identity.Name;
            // var username = "8778650328";
             var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "InstitutionEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var result = await _repository.UpdateInstitution(lead);
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


        [HttpGet, Route("GetAllInstitution")]
        public async Task<IActionResult> GetAllInstitution()
        {
            try
            {
                //var userName = "8778650328";
               var userName = Convert.ToString(User.Identity.Name);
                // if (userName == null)
                // {
                //     return Unauthorized();
                // }


                var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
                IfClaimExists = claims.Any(x => x.ClaimType == "InstitutionView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    //var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    //var HospitalId = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
                    var result = await this._repository.GetAllInstitution();
                    //return Ok(result);
                    if (result.Any())
                    {
                        return Ok(result);
                    }
                    return NotFound("Institution data not found");
                }
                return Unauthorized();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet, Route("GetAllInstitutionIds")]
        public async Task<IActionResult> GetAllInstitutionIds()
        {
            try
            {
                //var userName = "8778650328";
                var userName = Convert.ToString(User.Identity.Name);

                    var result = await this._repository.GetAllInstitutionIds();
                    //return Ok(result);
                    if (result.Any())
                    {
                        return Ok(result);
                    }
                    return NotFound("Institution data not found");
               

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpGet, Route("GetInstitution_DD")]
        public async Task<IActionResult> GetInstitution_DD()
        {
            try
            {
                var username = User.Identity.Name;
                //var username = "8073043647";
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "InstitutionView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    //var userName = User.Identity.Name.ToString();
                    //var roleaction = await this.findUserId.FindRolecategoryFromUserName(username);
                    //var HospitalId = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(username);
                    var result = await this._repository.GetInstitution_DD();
                    if (result.Any())
                    {
                        return Ok(result);
                    }
                    return NotFound("Institution data not found");
                }
                return Unauthorized();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete, Route("DeleteInstitution")]
        public async Task<IActionResult> DeleteInstitution(int Ins_Id)
        {
            var username = User.Identity.Name;
           // var username = "8073043647";
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "InstitutionDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var result = await _repository.DeleteInstitution( Ins_Id);
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



        [HttpPut, Route("ApproveInstitution")]
        public async Task<IActionResult> ApproveHospital([FromBody] ApproveIns lead)
        {
           // var username = "8073043647";
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "InstitutionApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var result = await _repository.ApproveInstitution(lead);
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



        [HttpGet, Route("GetInstitutionById")]
        public async Task<IActionResult> GetInstitutionById(int Ins_Id)
        {
            try
            {
              // var username = "8778650328";
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "InstitutionView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    //var userName = User.Identity.Name.ToString();
                    //var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    //var HospitalId = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
                    var result = await this._repository.GetInstitutionById(Ins_Id);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    return NotFound("Institution data not found");
                }
                return Unauthorized();


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }


    }