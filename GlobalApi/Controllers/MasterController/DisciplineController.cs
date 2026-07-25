using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplineController : ControllerBase
    {
        public readonly IDiscipline _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public DisciplineController()
        {
            this._repository = new DisciplineRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

      //  [HttpPost, Route("InsertDiscipline")]
      //  public async Task<IActionResult> Post([FromBody] Discipline Discipline)
      //  {
      //      var username = User.Identity.Name;
      //      var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
      //      IfClaimExists = claims.Any(x => x.ClaimType == "DisciplineAdd" && x.ClaimValue == "Y");
      //      if (IfClaimExists)
      //      {
      //          var change = await _repository.InsertDiscipline(Discipline);

      //          if (change == "Discipline Added Successfully")
      //          {
      //              return Ok();
      //          }
      //          return BadRequest(change);
      //      }
      //      return Unauthorized();
      //  }
      //  //online portals
      //  [HttpPost, Route("InsertDiscipline_Onilne")]
      //  public async Task<IActionResult> Post([FromBody] Discipline_Online Discipline_Online)
      //  {
      //      var username = User.Identity.Name;
      //      var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
      //      IfClaimExists = claims.Any(x => x.ClaimType == "DisciplineAdd" && x.ClaimValue == "Y");
      //      if (IfClaimExists)
      //      {
      //          var change = await _repository.InsertDiscipline_Online(Discipline_Online);

      //          if (change == "Discipline Added Successfully")
      //          {
      //              return Ok();
      //          }
      //          return BadRequest(change);
      //      }
      //      return Unauthorized();
      //  }

      //  [HttpPut, Route("UpdateDiscipline")]
      //  public async Task<IActionResult> Put([FromBody] Discipline Discipline)
      //  {
      //      var username = User.Identity.Name;
      //      var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
      //      IfClaimExists = claims.Any(x => x.ClaimType == "DisciplineEdit" && x.ClaimValue == "Y");
      //      if (IfClaimExists)
      //      {
      //          var change = await _repository.UpdateDiscipline(Discipline);

      //          if (change == "Discipline Updated Successfully")
      //          {
      //              return Ok();
      //          }
      //          return BadRequest(change);
      //      }
      //      return Unauthorized();

      //  }
      //// Onilne Portals

      //  [HttpPut, Route("UpdateDiscipline_Online")]
      //  public async Task<IActionResult> Put([FromBody] Discipline_Online Discipline_Online)
      //  {
      //      var username = User.Identity.Name;
      //      var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
      //      IfClaimExists = claims.Any(x => x.ClaimType == "DisciplineEdit" && x.ClaimValue == "Y");
      //      if (IfClaimExists)
      //      {
      //          var change = await _repository.UpdateDiscipline_Online(Discipline_Online);

      //          if (change == "Discipline Updated Successfully")
      //          {
      //              return Ok();
      //          }
      //          return BadRequest(change);
      //      }
      //      return Unauthorized();

      //  }

      //  [HttpGet, Route("GetAllDiscipline")]
      //  public async Task<IActionResult> GetAllDiscipline()
      //  {
      //      var result = await this._repository.GetAllDiscipline();
      //      return Ok(result);

      //  }

      //  //  Onilne Portals

      //  [HttpGet, Route("GetAllDiscipline_Online")]
      //  public async Task<IActionResult> GetAllDiscipline_Online()
      //  {
      //      var result = await this._repository.GetAllDiscipline_Online();
      //      return Ok(result);

      //  } 

      //  [HttpGet, Route("GetDiscipline_DD")]
      //  public async Task<IActionResult> GetDiscipline_DD()
      //  {
      //      var result = await this._repository.GetDiscipline_DD();
      //      return Ok(result);
      //  }

      //  [HttpGet, Route("GetDiscipline_DD_Mobile")]
      //  public async Task<IActionResult> GetDiscipline_DD_Mobile()
      //  {
      //      List<NoDiscFound> noDiscList = new List<NoDiscFound>();
      //      var result = await this._repository.GetDiscipline_DD();
      //      if (result.Count > 0)
      //      {
      //          List<NoDiscFound> DefDiscList = result.Select(disc => new NoDiscFound
      //          {
      //              CD_Id = disc.CD_Id,
      //              CD_ClinicalDiscipline = disc.CD_ClinicalDiscipline

      //          }).ToList();

      //          NoDiscFound defaultDisc = new NoDiscFound { CD_Id = 0, CD_ClinicalDiscipline = "Select discipline" };

      //          DefDiscList.Insert(0, defaultDisc);
      //          return Ok(DefDiscList);
      //      }

      //      noDiscList.Add(new NoDiscFound { CD_Id = 0, CD_ClinicalDiscipline = "Discipline not found" });
      //      return Ok(noDiscList);
      //  }

      //  //  Onilne Portals
      //  [HttpGet, Route("GetDiscipline_DD_Online")]
      //  public async Task<IActionResult> GetDiscipline_DD_Online()
      //  {
      //      var result = await this._repository.GetDiscipline_DD_Online();
      //      return Ok(result);
      //  }

      //  [HttpDelete, Route("DeleteDiscipline")]
      //  public async Task<IActionResult> DeleteDiscipline(int CD_Id)
      //  {
      //      var username = User.Identity.Name;
      //      var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
      //      IfClaimExists = claims.Any(x => x.ClaimType == "DisciplineDelete" && x.ClaimValue == "Y");
      //      if (IfClaimExists)
      //      {
      //          var change = await _repository.DeleteDiscipline(CD_Id);

      //          if (change == "Discipline Deleted Successfully")
      //          {
      //              return Ok();
      //          }
      //          return BadRequest(change);
      //      }
      //      return Unauthorized();
      //  }

      //  //  Onilne Portals
      //  [HttpDelete, Route("DeleteDiscipline_Online")]
      //  public async Task<IActionResult> DeleteDiscipline_Online(int cd_id)
      //  {
      //      var username = User.Identity.Name;
      //      //var username = "8073043647";
      //      var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
      //      IfClaimExists = claims.Any(x => x.ClaimType == "DisciplineDelete" && x.ClaimValue == "Y");
      //      if (IfClaimExists)
      //      {
      //          var change = await _repository.DeleteDiscipline_Online(cd_id);

      //          if (change == "Discipline Deleted Successfully")
      //          {
      //              return Ok();
      //          }
      //          return BadRequest(change);
      //      }
      //      return Unauthorized();
      //  }

      //  [HttpGet, Route("GetDisciplineById")]
      //  public async Task<IActionResult> GetDisciplineById(int CD_Id)
      //  {
      //      var result = await this._repository.GetDisciplineById(CD_Id);
      //      if (result != null)
      //      {
      //          return Ok(result);
      //      }
      //      return NotFound("Discipline not found");
      //  }
      //  //  Onilne Portals
      //  [HttpGet, Route("GetDisciplineById_Online")]
      //  public async Task<IActionResult> GetDisciplineById_Online(int cd_id)
      //  {
      //      var result = await this._repository.GetDisciplineById_Online(cd_id);
      //      if (result != null)
      //      {
      //          return Ok(result);
      //      }
      //      return NotFound("Discipline not found");
      //  }




      //  [HttpPut, Route("ApproveDiscipline")]
      //  public async Task<IActionResult> ApproveDiscipline([FromBody] ApproveDiscipline ApproveDiscipline)
      //  {
      //      var username = User.Identity.Name;
      //      var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
      //      IfClaimExists = claims.Any(x => x.ClaimType == "DisciplineApprove" && x.ClaimValue == "Y");
      //      if (IfClaimExists)
      //      {
      //          var change = await _repository.ApproveDiscipline(ApproveDiscipline);

      //          if (change == "Discipline Approved Successfully")
      //          {
      //              return Ok();
      //          }
      //          return BadRequest(change);
      //      }
      //      return Unauthorized();
      //  }

      //  //  Onilne Portals
      //  [HttpPut, Route("ApproveDiscipline_Online")]
      //  public async Task<IActionResult> ApproveDiscipline_Online([FromBody] ApproveDiscipline_Online ApproveDiscipline_Online)
      //  {
      //      var username = User.Identity.Name;
      //      var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
      //      IfClaimExists = claims.Any(x => x.ClaimType == "DisciplineApprove" && x.ClaimValue == "Y");
      //      if (IfClaimExists)
      //      {
      //          var change = await _repository.ApproveDiscipline_Online(ApproveDiscipline_Online);

      //          if (change == "Discipline Approved Successfully")
      //          {
      //              return Ok();
      //          }
      //          return BadRequest(change);
      //      }
      //      return Unauthorized();
      //  }
    }
}
