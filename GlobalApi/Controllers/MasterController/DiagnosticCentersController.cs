using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.GlobalClasses;
using GlobalApi.Models.Authentication;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.AdminIRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticCentersController : ControllerBase
    {
        public readonly IDiagnosticCenters _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;


        public DiagnosticCentersController()
        {
            this._repository = new DiagnosticCentersRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();

        }

        [HttpPost, Route("Admin/InsertDiagnosticCenters")]
        public async Task<IActionResult> AdminPost([FromForm] Diagnostic_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "Diag.CenterAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                
                var change = await _repository.InsertDiagnosticCenters(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpPost, Route("Self/InsertDiagnosticCenters")]
        public async Task<IActionResult> SelfPost([FromForm] Diagnostic_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "Diag.CenterAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertDiagnosticCenters(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        
        [HttpPut, Route("Admin/UpdateDiagnosticCenters")]
        public async Task<IActionResult> AdminPut([FromForm] Diagnostic_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "Diag.CenterEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateDiagnosticCenters(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpPut, Route("Self/UpdateDiagnosticCenters")]
        public async Task<IActionResult> SelfPut([FromForm] Diagnostic_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "Diag.CenterEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateDiagnosticCenters(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpGet, Route("GetAllDiagnosticCenters")]
        public async Task<IActionResult> GetAllDiagnosticCenters()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "Diag.CenterView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var DCId = await this.findUserId.FindDCIdFromDCOfficeUsername(userName);
                    var result = await this._repository.GetAllDiagnosticCenters(DCId, roleaction);
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
        [HttpGet, Route("Admin/GetDiagnosticCategory_DD")]
        public async Task<IActionResult> GetDiagnosticCategory_DD()
        {
            try
            {
                var result = await this._repository.GetDiagnosticCategory_DD();
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
        [HttpGet, Route("Admin/GetDiagnosticCenters_DD")]
        public async Task<IActionResult> AdminGetDiagnosticCenters_DD()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "Diag.CenterView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var DCId = await this.findUserId.FindDCIdFromDCOfficeUsername(userName);
                    var result = await this._repository.GetDiagnosticCenters_DD(DCId, roleaction);
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

        [HttpGet, Route("Self/GetDiagnosticCenters_DD")]
        public async Task<IActionResult> SelfGetDiagnosticCenters_DD()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "Diag.CenterView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var DCId = await this.findUserId.FindDCIdFromDCOfficeUsername(userName);
                    var result = await this._repository.GetDiagnosticCenters_DD(DCId, roleaction);
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
        
        
        [HttpDelete, Route("DeleteDiagnosticCenters")]
        public async Task<IActionResult> DeleteDiagnosticCenters(int DGSTC_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "Diag.CenterDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteDiagnosticCenters(DGSTC_Id);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpGet, Route("Admin/GetDiagnosticCentersById")]
        public async Task<IActionResult> AdminGetDiagnosticCentersById(int DGSTC_Id)
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "Diag.CenterView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var DCId = await this.findUserId.FindDCIdFromDCOfficeUsername(userName);
                    var result = await this._repository.GetDiagnosticCentersById(DGSTC_Id, roleaction);
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

        [HttpGet, Route("Self/GetDiagnosticCentersById")]
        public async Task<IActionResult> SelfGetDiagnosticCentersById(int DGSTC_Id)
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "Diag.CenterView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var DCId = await this.findUserId.FindDCIdFromDCOfficeUsername(userName);
                    var result = await this._repository.GetDiagnosticCentersById(DGSTC_Id, roleaction);
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

        [HttpPut, Route("ApproveDiagnosticCenter")]
        public async Task<IActionResult> ApproveDiagnosticCenter([FromBody] ApproveDiagnosticCenter lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "Diag.CenterApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveDiagnosticCenter(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

    }
}
