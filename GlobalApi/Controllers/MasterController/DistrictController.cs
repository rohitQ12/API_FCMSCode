using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.GlobalClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DistrictController : ControllerBase
    {
        public readonly IDistrict _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public DistrictController()
        {
            this._repository = new DistrictRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        //[HttpPost, Route("InsertDistrict")]
        //public async Task<IActionResult> Post([FromBody] Districts Districts)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DistrictAdd" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.InsertDistrict(Districts);

        //        if (change== "District Added Successfully")
        //        {
        //            return Ok();
        //        }

        //        return BadRequest(change);
        //    }
        //    return Unauthorized();
        //}

        //[HttpPut, Route("UpdateDistrict")]
        //public async Task<IActionResult> Put([FromBody] Districts Districts)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DistrictEdit" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.UpdateDistrict(Districts);
        //        if (change == "District Updated Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();
        //}

        //[HttpGet, Route("GetDistrict_DD")]
        //public async Task<IActionResult> GetDistrict_DD(int stat_id)
        //{
        //    var result = await this._repository.GetDistrict_DD(stat_id);
        //    if (result.Any())
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("District not found");
        //}

        //[HttpGet, Route("GetDistrict_DD_Mobile")]
        //public async Task<IActionResult> GetDistrict_DD_Mobile(int stat_id)
        //{
        //    List<NoDistFound> noDistList = new List<NoDistFound>();
        //    if (stat_id == 0)
        //    {
        //        noDistList.Add(new NoDistFound { district_id = 0, district_code = "D00", district_name = "District not found" });
        //        return Ok(noDistList);
        //    }

        //    var result = await this._repository.GetDistrict_DD(stat_id);
        //    if (result.Count > 0)
        //    {
        //        List<NoDistFound> DefDistList = result.Select(dist => new NoDistFound
        //        {
        //            district_id = dist.district_id,
        //            district_code = dist.district_code,
        //            district_name = dist.district_name
        //        }).ToList();

        //        NoDistFound defaultDist = new NoDistFound { district_id = 0, district_code = "D00", district_name = "Select district" };

        //        DefDistList.Insert(0, defaultDist);
        //        return Ok(DefDistList);
        //    }

        //    noDistList.Add(new NoDistFound { district_id = 0, district_code = "D00", district_name = "District not found" });
        //    return Ok(noDistList);
        //}


        //[HttpDelete, Route("DeleteDistrict")]
        //public async Task<IActionResult> DeleteDistrict(int district_id)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DistrictDelete" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.DeleteDistrict(district_id);

        //        if (change == "District Deleted Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();
        //}

        //[HttpGet, Route("GetDistrictById")]
        //public async Task<IActionResult> GetDistrictById(int district_id)
        //{
        //    var result = await this._repository.GetDistrictById(district_id);
        //    if (result != null)
        //    {
        //        return Ok(result);
               
        //    }
        //    return NotFound("District not found");
        //}

        //[HttpGet, Route("GetAllDistrict")]
        //public async Task<IActionResult> GetAllDistrict()
        //{
        //    var result = await this._repository.GetAllDistrict();
        //    return Ok(result);
        //}

        //[HttpPut, Route("ApproveDistrict")]
        //public async Task<IActionResult> ApproveDistrict([FromBody] ApproveDistrict ApproveDistrict)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DistrictApprove" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.ApproveDistrict(ApproveDistrict);
        //        if (change == "District Approved Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();
        //}
    }
}
