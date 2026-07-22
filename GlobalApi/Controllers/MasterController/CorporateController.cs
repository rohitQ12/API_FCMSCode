using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Authorization;
using GlobalApi.Repository.MasterRepository;
using System.Net.Http.Headers;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.AdminIRepository;
using System.Diagnostics.Eventing.Reader;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorporateController : ControllerBase
    {
        public readonly ICorporate _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        public readonly IAuthenticationRepository authrepository;
        private bool IfClaimExists = false;
        public readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CorporateController(IAuthenticationRepository authrepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            this._repository = new CorporateRepository(_webHostEnvironment = webHostEnvironment);
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
            this.authrepository = authrepository;
            this.userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;

        }
        //[AllowAnonymous]
        //[HttpPost, Route("Admin/InsertCorporate")]
        //public async Task<IActionResult> AdminPost([FromBody] Corporate_Images lead)
        //{
        //    var userName = User.Identity.Name;
        //    //var userName = "8778650328";
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
        //    //IfClaimExists = claims.Any(x => x.ClaimType == "CorporateAdd" && x.ClaimValue == "Y");
        //    //if (IfClaimExists)
        //    //{

        //        lead.CO_Type = "Corporate";
        //        var result = await _repository.InsertCorporate(lead, "");
        //    if (result.IsSuccess)
        //    {
        //        return Ok(new { message = result.Message });
        //    }
        //    else
        //    {
        //        return BadRequest(new { message = result.Message });
        //    }
        //    // }
        //    //  return Unauthorized();

        //}


        ////[AllowAnonymous]
        ////[HttpPost, Route("Self/InsertCorporate")]
        ////public async Task<IActionResult> SelfPost([FromForm] Corporate_Images lead)
        ////{
        ////    var username = User.Identity.Name;
        ////    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        ////    IfClaimExists = claims.Any(x => x.ClaimType == "CorporateAdd" && x.ClaimValue == "Y");
        ////    if (IfClaimExists)
        ////    {
        ////        lead.CO_Type = "Clinic";
        ////        var change = await _repository.InsertCorporate(lead, "");

        ////        if (change != null)
        ////            return Ok();
        ////        else
        ////            return BadRequest("Not successfull");
        ////    }
        ////    return Unauthorized();

        ////}


        //[HttpPut, Route("Admin/UpdateCorporate")]
        //public async Task<IActionResult> AdminPut([FromBody] Corporate_ImagesUP lead)
        //{
        //    var userName = User.Identity.Name;
        //    //var userName = "8073043647";
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "CorporateEdit" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var result = await _repository.UpdateCorporate(lead);
        //        if (result.IsSuccess)
        //        {
        //            return Ok(new { message = result.Message });
        //        }
        //        else
        //        {
        //            return BadRequest(new { message = result.Message });
        //        }
        //    }
        //    return Unauthorized();

        //}


        ////[HttpPut, Route("Self/UpdateCorporate/{DO_Photo}")]
        ////public async Task<IActionResult> SelfPut([FromBody] Corporate_ImagesUP lead, [FromForm] IFormFile DO_Photo)
        ////{
        ////    var username = User.Identity.Name;
        ////    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        ////    IfClaimExists = claims.Any(x => x.ClaimType == "CorporateEdit" && x.ClaimValue == "Y");
        ////    if (IfClaimExists)
        ////    {

        ////        var change = await _repository.UpdateCorporate(lead);

        ////        if (change != null)
        ////            return Ok();
        ////        else
        ////            return BadRequest("Not successfull");
        ////    }
        ////    return Unauthorized();

        ////}

        //[HttpGet, Route("GetAllCorporate")]
        //public async Task<IActionResult> GetAllCorporate()
        //{
        //    try
        //    {
        //        var userName = User.Identity.Name;
        //      //  var userName = "8073043647";
        //        var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
        //        IfClaimExists = claims.Any(x => x.ClaimType == "CorporateView" && x.ClaimValue == "Y");
        //        if (IfClaimExists)
        //        { 
        //            var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
        //            var result = await this._repository.GetAllCorporate();
        //            //return Ok(result);
        //            if (result.Any())
        //            {
        //                return Ok(result);
        //            }
        //            return NotFound("Corporate data not found");
        //        }
        //        return Unauthorized();

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpDelete, Route("DeleteCorporate")]
        //public async Task<IActionResult> DeleteCorporate(int CO_Id)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "CorporateDelete" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var result = await _repository.DeleteCorporate(CO_Id);

        //        if (result.IsSuccess)
        //        {
        //            return Ok(new { message = result.Message });
        //        }
        //        else
        //        {
        //            return BadRequest(new { message = result.Message });
        //        }
        //    }
        //    return Unauthorized();

        //}


        //[HttpGet, Route("Admin/GetCorporateById")]
        //public async Task<IActionResult> GetCorporateById(int CO_Id)
        //{

        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "CorporateView" && x.ClaimValue == "Y");
           
        //    if (IfClaimExists)
        //    {     
        //        var result = await this._repository.GetCorporateById(CO_Id);
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }
                
        //        return NotFound("Corporate data not found");
        //    }
        //    return Unauthorized();

        //}

        ////[HttpGet, Route("Self/GetCorporateById")]
        ////public async Task<IActionResult> SelfGetCorporateById(int DO_Id)
        ////{
        ////    var username = User.Identity.Name;
        ////    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        ////    IfClaimExists = claims.Any(x => x.ClaimType == "CorporateView" && x.ClaimValue == "Y");
        ////    if (IfClaimExists)
        ////    {

        ////        var result = await this._repository.GetCorporateById(DO_Id);
        ////        if (result != null)
        ////        {
        ////            return Ok(result);
        ////        }
        ////        return NotFound("Corporate data not found");
        ////    }
        ////    return Unauthorized();

        ////}

        ////[HttpGet, Route("Doctor_DD")]
        ////public async Task<IActionResult> Doctor_DD(int SP_Id)
        ////{
        ////    var username = User.Identity.Name;
        ////    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        ////    IfClaimExists = claims.Any(x => x.ClaimType == "DoctorView" && x.ClaimValue == "Y");
        ////    if (IfClaimExists)
        ////    {

        ////        var result = await this._repository.Corporate_DD(SP_Id);
        ////        if (result.Any())
        ////        {
        ////            return Ok(result);
        ////        }

        ////        return NotFound("Doctor data not found");
        ////    }
        ////    return Unauthorized();
        ////}


        ////[HttpGet, Route("GeneralDoctor")]
        ////public async Task<IActionResult> GeneralDoctor()
        ////{
        ////    var result = await this._repository.GetGeneralDoctorDetails();
        ////    return Ok(result);
        ////}


        ////[HttpGet, Route("SpecialityDoctor")]
        ////public async Task<IActionResult> SpecialityDoctor()
        ////{
        ////    var result = await this._repository.GetSpecialityDoctorDetails();
        ////    return Ok(result);
        ////}
        ////[HttpGet, Route("MultiSpecialityDoctor")]
        ////public async Task<IActionResult> MultiSpecialityDoctor()
        ////{
        ////    var result = await this._repository.GetMultiSpecialityDoctorDetails();
        ////    return Ok(result);
        ////}
        ////[HttpGet, Route("ClinicDoctor")]
        ////public async Task<IActionResult> ClinicDoctor()
        ////{
        ////    var result = await this._repository.GetClinicDoctorDetails();
        ////    return Ok(result);
        ////}
        ////[HttpGet, Route("HospitalDoctor")]
        ////public async Task<IActionResult> HospitalDoctor()
        ////{
        ////    var result = await this._repository.GetHospitalDoctorDetails();
        ////    return Ok(result);
        ////}


        ////[HttpGet, Route("DoctorDD")]
        ////public async Task<IActionResult> DoctorDD()
        ////{
        ////    var username = User.Identity.Name;
        ////    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        ////    IfClaimExists = claims.Any(x => x.ClaimType == "DoctorView" && x.ClaimValue == "Y");
        ////    if (IfClaimExists)
        ////    {
        ////        var userName = User.Identity.Name.ToString();
        ////        var Rolecategory = await this.findUserId.FindRolecategoryFromUserName(userName);
        ////        var RoleName = await this.findUserId.FindRoleNameFromUserName(userName);
        ////        var DO_HO_Id_FK = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
        ////        var DO_Id_Fk = await this.findUserId.FindDoctorIdFromUsername(userName);
        ////        var result = await this._repository.DoctorDD(DO_HO_Id_FK, DO_Id_Fk, Rolecategory, RoleName);
        ////        if (result.Any())
        ////        {
        ////            return Ok(result);
        ////        }

        ////        return NotFound("Doctor data not found");
        ////    }
        ////    return Unauthorized();
        ////}

        //[HttpGet, Route("GetCorporateDetails")]
        //public async Task<IActionResult> GetCorporateDetails(int corporateId)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "CorporateView" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {

        //        var result = await this._repository.GetCorporateDetails(corporateId);
        //        if (result.Count > 0)
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound("Corporate data not found");
        //    }
        //    return Unauthorized();
        //}

        //[HttpPut, Route("ApproveCorporate")]
        //public async Task<IActionResult> ApproveCorporate([FromBody] ApproveCorporate lead)
        //{
        //    //var userName = "8073043647";
        //    var userName = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(userName);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "CorporateApprove" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var result = await _repository.ApproveCorporate(lead);

        //        if (result.IsSuccess)
        //        {
        //            return Ok(new { message = result.Message });
        //        }
        //        else
        //        {
        //            return BadRequest(new { message = result.Message });
        //        }
        //    }
        //    return Unauthorized();
        //}


        //[HttpGet, Route("GetAllCorporateIds")]
        //public async Task<IActionResult> GetAllCorporateIds()
        //{
        //    try
        //    {
        //        var userName = User.Identity.Name;
        //           var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
        //            var result = await this._repository.GetAllCorporateIds();
        //            //return Ok(result);
        //            if (result.Any())
        //            {
        //                return Ok(result);
        //            }
        //            return NotFound("Corporate data not found");
               

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

    }
}
