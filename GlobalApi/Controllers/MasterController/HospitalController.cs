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
    public class HospitalController : ControllerBase
    {
        public readonly IHospital _repository;
        public readonly FindUserId findUserId;
        private bool IfClaimExists = false;
        private IEnumerable<Claim> claims = null;
        private readonly ClaimsAuthorization claimsAuthorization;
        public HospitalController()
        {
            this._repository = new HospitalRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("Admin/InsertHospital")]
        public async Task<IActionResult> AdminPost([FromForm] Hospital_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "HospitalAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertHospital(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpPost, Route("Self/InsertHospital")]
        public async Task<IActionResult> SelfPost([FromForm] Hospital_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertHospital(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("Admin/UpdateHospital")]
        public async Task<IActionResult> AdminPut([FromForm] Hospital_Images lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "HospitalEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateHospital(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();

            
        }

        [HttpPut, Route("Self/UpdateHospital")]
        public async Task<IActionResult> SelfPut([FromForm] Hospital_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateHospital(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllHospital")]
        public async Task<IActionResult> GetAllHospital()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "HospitalView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var HospitalId = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
                    var result = await this._repository.GetAllHospitaltest(HospitalId, roleaction);
                    if (result != null)
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

        [HttpGet, Route("GetHosReg_DD")]
        public async Task<ActionResult<IEnumerable<Hospital_DD>>> GetHosReg_DD(string PrimaryorBranch)
        {
            try
            {
                var result = await this._repository.GetHosReg_DD(PrimaryorBranch);
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


        [HttpGet, Route("Admin/GetHospital_DD")]
        public async Task<IActionResult> AdminGetHospital_DD()
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "HospitalView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var HospitalId = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
                    var result = await this._repository.GetHospital_DD(HospitalId, roleaction);
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

        [HttpGet, Route("Admin/GetNetworkHospital_DD")]
        public async Task<IActionResult> AdminGetNetworkHospital_DD(int NE_Id)
        {
            try
            {
                var result = await this._repository.GetNetworkHospital_DD(NE_Id);
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

        [HttpGet, Route("Admin/GetHospitalCategory_DD")]
        public async Task<IActionResult> AdminGetHospitalCategory_DD(int HosCat_Id)
        {
            try
            {
                var result = await this._repository.GetHospitalCategory_DD(HosCat_Id);
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

        [HttpGet, Route("Self/GetHospital_DD")]
        public async Task<IActionResult> SelfGetHospital_DD()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var HospitalId = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
                var result = await this._repository.GetHospital_DD(HospitalId, roleaction);
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
        
        [HttpDelete, Route("DeleteHospital")]
        public async Task<IActionResult> DeleteHospital(int Hos_Id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "HospitalDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteHospital(Hos_Id);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }

        [HttpGet, Route("Admin/GetHospitalById")]
        public async Task<IActionResult> AdminGetHospitalById(int Hos_Id)
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "HospitalView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    //var HospitalId = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
                    var result = await this._repository.GetHospitalById(Hos_Id, roleaction);
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

        [HttpGet, Route("Self/GetHospitalById")]
        public async Task<IActionResult> SelfGetHospitalById(int Hos_Id)
        {
            try
            {
                var username = User.Identity.Name;
                var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
                IfClaimExists = claims.Any(x => x.ClaimType == "HospitalView" && x.ClaimValue == "Y");
                if (IfClaimExists)
                {
                    var userName = User.Identity.Name.ToString();
                    var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                    var result = await this._repository.GetHospitalById(Hos_Id, roleaction);
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
        
        [HttpGet, Route("GetHospital_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Hospital/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }

        [HttpPut, Route("ApproveHospital")]
        public async Task<IActionResult> ApproveHospital([FromBody] ApproveHos lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "HospitalApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveHospital(lead);

                if (change != null)
                    return Ok(change);
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();
            
        }
    }
}
