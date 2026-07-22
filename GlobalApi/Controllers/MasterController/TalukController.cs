using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalukController : ControllerBase
    {
        public readonly ITaluk _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public TalukController()
        {
            this._repository = new TalukRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertTaluk")]
        public async Task<IActionResult> Post([FromBody] Taluk Taluk)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "TalukAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertTaluk(Taluk);

                if (change == "Taluk Added Successfully")
                    return Ok();
                else
                    return BadRequest(change);
            }
            return Unauthorized();

        }

        [HttpPut, Route("UpdateTaluk")]
        public async Task<IActionResult> Put([FromBody] Taluk Taluk)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "TalukEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateTaluk(Taluk);

                if (change == "Taluk Updated Successfully")
                    return Ok();
                else
                    return BadRequest(change);
            }
            return Unauthorized();

        }

        [HttpGet, Route("GetTaluk_DD")]
        public async Task<IActionResult> GetTaluk_DD(int district_id)
        {
            var result = await this._repository.GetTaluk_DD(district_id);
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound("Taluk not found");
        }

        [HttpGet, Route("GetTaluk_DD_Mobile")]
        public async Task<IActionResult> GetTaluk_DD_Mobile(int district_id)
        {
            List<NoTalukFound> noTalukList = new List<NoTalukFound>();
            if (district_id == 0)
            {
                noTalukList.Add(new NoTalukFound { taluk_id = 0, taluk_code = "T00", taluk_name = "Taluk not found" });
                return Ok(noTalukList);
            }
            var result = await this._repository.GetTaluk_DD(district_id);

            if (result.Count > 0)
            {
                List<NoTalukFound> DefTalukList = result.Select(taluk => new NoTalukFound
                {
                    taluk_id = taluk.Taluk_id,
                    taluk_code = taluk.Taluk_code,
                    taluk_name = taluk.Taluk_name
                }).ToList();

                NoTalukFound defaultTaluk = new NoTalukFound { taluk_id = 0, taluk_code = "T00", taluk_name = "Select taluk" };

                DefTalukList.Insert(0, defaultTaluk);
                return Ok(DefTalukList);
            }

            noTalukList.Add(new NoTalukFound { taluk_id = 0, taluk_code = "T00", taluk_name = "Taluk not found" });
            return Ok(noTalukList);
        }

        [HttpDelete, Route("DeleteTaluk")]
        public async Task<IActionResult> DeleteTaluk(int Taluk_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "TalukDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteTaluk(Taluk_id);

                if (change == "Taluk Deleted Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }

        [HttpGet, Route("GetAllTaluk")]
        public async Task<IActionResult> GetAllTaluk()
        {
            var result = await this._repository.GetAllTaluk();
            return Ok(result);
        }

        [HttpPut, Route("ApproveTaluk")]
        public async Task<IActionResult> ApproveTaluk([FromBody] ApproveTaluk ApproveTaluk)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "TalukApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveTaluk(ApproveTaluk);

                if (change == "Taluk Approved Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();

        }

    }
}
