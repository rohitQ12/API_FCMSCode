using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using GlobalApi.GlobalClasses;
using GlobalApi.Models.Authentication;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        public readonly IPharmacy _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public PharmacyController()
        {
            this._repository = new PharmacyRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("Admin/InsertPharmacy")]
        public async Task<IActionResult> AdminPost([FromForm] Pharmacy_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PharmacyAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertPharmacy(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpPost, Route("Self/InsertPharmacy")]
        public async Task<IActionResult> SelfPost([FromForm] Pharmacy_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PharmacyAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertPharmacy(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpPut, Route("Admin/UpdatePharmacy")]
        public async Task<IActionResult> AdminPut([FromForm] Pharmacy_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PharmacyEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdatePharmacy(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpPut, Route("Self/UpdatePharmacy")]
        public async Task<IActionResult> SelfPut([FromForm] Pharmacy_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PharmacyEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdatePharmacy(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
        
        [HttpGet, Route("GetAllPharmacy")]
        public async Task<IActionResult> GetAllPharmacy()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "PharmacyView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var PharmacyId = await this.findUserId.FindPharmacyIdFromPharmacyOfficeUsername(userName);
                    var result = await this._repository.GetAllPharmacy(PharmacyId, roleaction);
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

        [HttpGet, Route("Admin/GetPharmacyCategory_DD")]
        public async Task<IActionResult> AdminGetPharmacyCategory_DD()
        {
            try
            {
                var result = await this._repository.GetPharmacyCategory_DD();
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

        [HttpGet, Route("Admin/GetPharmacy_DD")]
        public async Task<IActionResult> AdminGetPharmacy_DD()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "PharmacyView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var PharmacyId = await this.findUserId.FindPharmacyIdFromPharmacyOfficeUsername(userName);
                    var result = await this._repository.GetPharmacy_DD(PharmacyId, roleaction);
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
        
        //[HttpGet, Route("Admin/GetPharmacyCategory_DD")]
        //public async Task<ActionResult<IEnumerable<Pharma_DD>>> AdminGetPharmacyCategory_DD()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetPharmacyCategory_DD();
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpGet, Route("Self/GetPharmacy_DD")]
        public async Task<IActionResult> SelfGetPharmacy_DD()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "PharmacyView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var PharmacyId = await this.findUserId.FindPharmacyIdFromPharmacyOfficeUsername(userName);
                    var result = await this._repository.GetPharmacy_DD(PharmacyId, roleaction);
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

        [HttpDelete, Route("DeletePharmacy")]
        public async Task<IActionResult> DeletePharmacy(int Ph_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PharmacyDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeletePharmacy(Ph_Id);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpGet, Route("Admin/GetPharmacyById")]
        public async Task<IActionResult> AdminGetPharmacyById(int Ph_Id)
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "PharmacyView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var PharmacyId = await this.findUserId.FindPharmacyIdFromPharmacyOfficeUsername(userName);
                    var result = await this._repository.GetPharmacyById(Ph_Id, roleaction);
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

        [HttpGet, Route("Self/GetPharmacyById")]
        public async Task<IActionResult> SelfGetPharmacyById(int Ph_Id)
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "PharmacyView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var PharmacyId = await this.findUserId.FindPharmacyIdFromPharmacyOfficeUsername(userName);
                    var result = await this._repository.GetPharmacyById(Ph_Id, roleaction);
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

        [HttpPut, Route("ApprovePharmacy")]
        public async Task<IActionResult> ApprovePharmacy([FromBody] ApprovePharmacy lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "PharmacyApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApprovePharmacy(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

    }
}
