using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GlobalApi.GlobalClasses;
using System.Text;
//using System.Web.Mvc;


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

        //[HttpPost, Route("InsertCountry")]
        //public async Task<IActionResult> Post([FromBody] Countries countries)
        //{
        //    string username = "8073043647";
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "CountryAdd" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.InsertCountry(countries);

        //        if (change == "Country Added Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();

        //}


        //[HttpPut, Route("UpdateCountry")]
        //public async Task<IActionResult> Put([FromBody] Countries countries)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "CountryEdit" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.UpdateCountry(countries);
        //        if (change == "Country Updated Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();
        //}

        //[HttpGet, Route("GetAllCountry")]
        //public async Task<IActionResult> GetAllCountry()
        //{
        //    var result = await this._repository.GetAllCountry();
        //    return Ok(result);
        //}

        //[HttpGet, Route("GetAllCountry_test/{itemsPerPage}/{pageNo}")]
        //public async Task<IActionResult> GetAllCountry_test(int itemsPerPage, int pageNo)
        //{
        //    var result = await this._repository.GetAllCountry(itemsPerPage, pageNo);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("Country not found");

        //}

        //[HttpGet, Route("GetCountry_DD")]
        //public async Task<IActionResult> GetCountry_DD()
        //{
        //    var result = await this._repository.GetCountry_DD();
        //    return Ok(result);
        //}

        //[HttpGet, Route("GetCountry_DD_Mobile")]
        //public async Task<IActionResult> GetCountry_DD_Mobile()
        //{

        //    var result = await this._repository.GetCountry_DD_Mobile();
        //    // Assuming result is a list of Country_DD, you can convert it to a list of NoCountryFound
        //    List<SelDefaultCountry> noCountryList = result.Select(country => new SelDefaultCountry
        //    {
        //        cntry_id = country.cntry_id,
        //        country_code = country.country_code,
        //        country_name = country.country_name
        //    }).ToList();

        //    // Create the default "Select Country" item
        //    SelDefaultCountry defaultCountry = new SelDefaultCountry { cntry_id = 0, country_code = "C00", country_name = "Select Country" };

        //    // Add the default country item to the list
        //    noCountryList.Insert(0, defaultCountry);

        //    // Return the modified list
        //    return Ok(noCountryList);

        //}


        //[HttpDelete, Route("DeleteCountry")]
        //public async Task<IActionResult> DeleteCountry(int Country_id)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "CountryDelete" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.DeleteCountry(Country_id);
        //        if (change == "Country Deleted Successfully")
        //        {
        //            var result = await this._repository.GetCountryById(Country_id);
        //            return Ok(result);
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();

        //}

        //[HttpGet, Route("GetCountryById")]
        //public async Task<IActionResult> Getcountrybyid(int Country_id)
        //{
        //    var result = await this._repository.GetCountryById(Country_id);
        //    return Ok(result);
        //}

        //[HttpPut, Route("ApproveCountry")]
        //public async Task<IActionResult> ApproveCountry([FromBody] ApproveCountry approvecountry)
        //{

        //    var username = "8073043647"; // Convert.ToString(User.Identity.Name);
        //    if(username==null)
        //    {
        //        return Unauthorized();
        //    }
            
        //    //var username = "8073043647";
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "CountryApprove" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.ApproveCountry(approvecountry);

        //        if (change == "Country Approved Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();

        //}

    }

}
