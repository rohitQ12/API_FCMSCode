using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GlobalApi.GlobalClasses;


namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        public readonly ICountry _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public CountryController()
        {
            this._repository = new CountryRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertCountry")]
        public async Task<IActionResult> Post([FromBody] Countries lead)
        {
            string username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "CountryAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertCountry(lead);

                if (change == "Country Added Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }


        [HttpPut, Route("UpdateCountry")]
        public async Task<IActionResult> Put([FromBody] Countries lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "CountryEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateCountry(lead);

                if (change == "Country Updated Successfully")
                {
                    //var result = await this._repository.GetCountryById(lead.cntry_id);
                    var result = await this._repository.GetAllCountry();
                    return Ok(result);
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }


        [HttpGet, Route("GetAllCountry")]
        public async Task<IActionResult> GetAllCountry()
        {
            var result = await this._repository.GetAllCountry();
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound("Country not found");

        }


        [HttpGet, Route("GetCountry_DD")]
        public async Task<IActionResult> GetCountry_DD()
        {
            var result = await this._repository.GetCountry_DD();
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound("Country not found");
        }


        [HttpDelete, Route("DeleteCountry")]
        public async Task<IActionResult> DeleteCountry(int Country_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "CountryDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteCountry(Country_id);
                if (change == "Country Deleted Successfully")
                {
                    var result = await this._repository.GetCountryById(Country_id);
                    return Ok(result);
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }


        [HttpGet, Route("GetCountryById")]
        public async Task<IActionResult> GetCountryById(int Country_id)
        {

            var result = await this._repository.GetCountryById(Country_id);
            if (result!=null)
            {
                return Ok(result);
            }
            return NotFound("Country not found");
        }

        [HttpPut, Route("ApproveCountry")]
        public async Task<IActionResult> ApproveCountry([FromBody] ApproveCountry lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "CountryApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveCountry(lead);

                if (change == "Country Approved Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }

    }
}
