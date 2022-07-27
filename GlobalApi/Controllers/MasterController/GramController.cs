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
    public class GramController : ControllerBase
    {
        public readonly IGram _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public GramController()
        {
            this._repository = new GramRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertGram")]
        public async Task<IActionResult> Post([FromBody] Gram lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "GramAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertGram(lead);

                if (change == "Gram Added Successfully")
                    return Ok();
                else
                    return BadRequest(change);
            }
            return Unauthorized();

        }

        [HttpPut, Route("UpdateGram")]
        public async Task<IActionResult> Put([FromBody] Gram lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "GramEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateGram(lead);

                if (change == "Gram Updated Successfully")
                    return Ok();
                else
                    return BadRequest(change);
            }
            return Unauthorized();

        }

        [HttpGet, Route("GetGram_DD")]
        public async Task<IActionResult> GetGram_DD(int Taluk_id)
        {

            var result = await this._repository.GetGram_DD(Taluk_id);
            if (result.Any())
            {
                return Ok(result);
            }

            return NotFound("Gram not found");

        }

        [HttpDelete, Route("DeleteGram")]
        public async Task<IActionResult> DeleteGram(int Gram_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "GramDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteGram(Gram_id);

                if (change == "Gram Deleted Successfully")
                {
                    return Ok();
                }
                else
                    return BadRequest(change);
            }
            return Unauthorized();

        }

        //[HttpGet, Route("GetGramById")]
        //public async Task<ActionResult<IEnumerable<GramById>>> GetGramById(int Gram_id)
        //{
        //    if (Gram_id == null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var result = await this._repository.GetGramById(Gram_id);
        //        if (result == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(result);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpGet, Route("GetAllGram")]
        public async Task<IActionResult> GetAllGram()
        {

            var result = await this._repository.GetAllGram();
            if (result.Any())
            {
                return Ok(result);
            }

            return NotFound("Gram not found");

        }

        [HttpPut, Route("ApproveGram")]
        public async Task<IActionResult> ApproveGram([FromBody] ApproveGram lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "GramApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveGram(lead);

                if (change == "Gram Approved Successfully")
                    return Ok();
                else
                    return BadRequest(change);
            }
            return Unauthorized();

        }
    }
}
