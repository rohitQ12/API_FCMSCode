using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        public readonly IDesignation _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public DesignationController()
        {
            this._repository = new DesignationRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        //[HttpPost, Route("InsertDesignation")]
        //public async Task<IActionResult> Post([FromBody] Designation Designation)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DesignationAdd" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.InsertDesignation(Designation);

        //        if (change == "Designation Added Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();
        //}
        ////Online Portals
        //[HttpPost, Route("InsertDesignation_Online")]
        //public async Task<IActionResult> Post([FromBody] Designation_Online Designation_Online)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DesignationAdd" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.InsertDesignation_Online(Designation_Online);

        //        if (change == "Designation Added Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();
        //}




        //[HttpPut, Route("UpdateDesignation")]
        //public async Task<IActionResult> Put([FromBody] Designation Designation)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DesignationEdit" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.UpdateDesignation(Designation);

        //        if (change == "Designation Updated Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();

        //}
        ////Online Portals
        //[HttpPut, Route("UpdateDesignation_Online")]
        //public async Task<IActionResult> Put([FromBody] Designation_Online Designation_Online)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DesignationEdit" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.UpdateDesignation_Online(Designation_Online);

        //        if (change == "Designation Updated Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();

        //}



        //[HttpGet, Route("GetAllDesignation")]
        //public async Task<IActionResult> GetAllDesignation()
        //{
        //    var result = await this._repository.GetAllDesignation();
        //    return Ok(result);
        //}
        ////Online Portals
        //[HttpGet, Route("GetAllDesignation_Online")]
        //public async Task<IActionResult> GetAllDesignation_Online()
        //{
        //    var result = await this._repository.GetAllDesignation_Online();
        //    return Ok(result);
        //}



        //[HttpGet, Route("GetDesignation_DD")]
        //public async Task<IActionResult> GetDesignation_DD()
        //{
        //    var result = await this._repository.GetDesignation_DD();
        //    return Ok(result);
        //}

        //[HttpGet, Route("GetDesignation_DD_Mobile")]
        //public async Task<IActionResult> GetDesignation_DD_Mobile()
        //{
        //    List<NoDesigFound> noDesigList = new List<NoDesigFound>();
        //    var result = await this._repository.GetDesignation_DD();
        //    if (result.Count > 0)
        //    {
        //        List<NoDesigFound> DefDesigList = result.Select(desig => new NoDesigFound
        //        {
        //            designation_id = desig.designation_id,
        //            designation_desc = desig.designation_desc
        //        }).ToList();

        //        NoDesigFound defaultDesig = new NoDesigFound { designation_id = 0, designation_desc = "Select designation" };

        //        DefDesigList.Insert(0, defaultDesig);
        //        return Ok(DefDesigList);
        //    }

        //    noDesigList.Add(new NoDesigFound { designation_id = 0, designation_desc = "Designation not found" });
        //    return Ok(noDesigList);

        //}

        ////Online Portals
        //[HttpGet, Route("GetDesignation_DD_Online")]
        //public async Task<IActionResult> GetDesignation_DD_Online()
        //{
        //    var result = await this._repository.GetDesignation_DD_Online();
        //    return Ok(result);
        //}



        //[HttpDelete, Route("DeleteDesignation")]
        //public async Task<IActionResult> DeleteDesignation(int designation_id)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DesignationDelete" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.DeleteDesignation(designation_id);
        //        if (change == "Designation Deleted Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();

        //}
        ////Online Portals 
        //[HttpDelete, Route("DeleteDesignation_Online")]
        //public async Task<IActionResult> DeleteDesignation_Online(int designation_id)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DesignationDelete" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.DeleteDesignation_Online(designation_id);
        //        if (change == "Designation Deleted Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();

        //}





        //[HttpGet, Route("GetDesignationById")]
        //public async Task<IActionResult> GetDesignationById(int designation_id)
        //{

        //    var result = await this._repository.GetDesignationById(designation_id);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("Designation not found");
        //}
        ////Online Portals 
        //[HttpGet, Route("GetDesignationById_Online")]
        //public async Task<IActionResult> GetDesignationById_Online(int designation_id)
        //{

        //    var result = await this._repository.GetDesignationById_Online(designation_id);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("Designation not found");
        //}




        ////[HttpPut, Route("ApproveDesignation")]
        ////public async Task<IActionResult> ApproveDesignation([FromBody] ApproveDesignation ApproveDesignation)
        ////{
        ////    var username = User.Identity.Name;
        ////    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        ////    IfClaimExists = claims.Any(x => x.ClaimType == "DesignationApprove" && x.ClaimValue == "Y");
        ////    if (IfClaimExists)
        ////    {
        ////        var change = await _repository.ApproveDesignation(ApproveDesignation);
        ////        if (change == "Designation Approved Succesfully")
        ////        {
        ////            return Ok();
        ////        }
        ////        return BadRequest(change);
        ////    }
        ////    return Unauthorized();
        ////}
        //[HttpPut, Route("ApproveDesignation")]
        //public async Task<IActionResult> ApproveDesignation([FromBody] ApproveDesignation ApproveDesignation)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DesignationApprove" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.ApproveDesignation(ApproveDesignation);

        //        if (change == "Designation Approved Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();

        //}
        ////ONline Portals 
        //[HttpPut, Route("ApproveDesignation_Online")]
        //public async Task<IActionResult> ApproveDesignation_Online([FromBody] ApproveDesignation_Online ApproveDesignation_Online)
        //{
        //    var username = User.Identity.Name;
        //    var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
        //    IfClaimExists = claims.Any(x => x.ClaimType == "DesignationApprove" && x.ClaimValue == "Y");
        //    if (IfClaimExists)
        //    {
        //        var change = await _repository.ApproveDesignation_Online(ApproveDesignation_Online);

        //        if (change == "Designation Approved Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
        //    }
        //    return Unauthorized();

        //}

    }
}
