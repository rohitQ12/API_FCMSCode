using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Drug_FrequencyController : ControllerBase
    {
        public readonly IDrug_FrequencyRepository _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public Drug_FrequencyController()
        {
            this._repository = new Drug_FrequencyRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertDrug_Frequency")]
        public async Task<ActionResult<Drug_Frequency>> Post([FromBody] Drug_Frequency lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDrug_Frequency(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateDrug_Frequency")]
        public async Task<ActionResult<Drug_Frequency>> Put([FromBody] Drug_Frequency lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDrug_Frequency(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllDrug_Frequency")]
        public async Task<ActionResult<IEnumerable<Drug_FrequencyAll>>> GetAllDrug_Frequency()
        {
            try
            {
                var result = await this._repository.GetAllDrug_Frequency();
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

        [HttpDelete, Route("DeleteDrug_Frequency")]
        public async Task<ActionResult> DeleteDrug_Frequency(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDrug_Frequency(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetADrug_Frequency_DD")]
        public async Task<ActionResult<IEnumerable<Drug_FrequencyDD>>> GetDD()
        {
            try
            {
                var result = await this._repository.GetADrug_Frequency_DD();
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

        [HttpPut, Route("ApproveDrug_Frequency")]
        public async Task<IActionResult> ApproveDrug_Frequency([FromBody] DrugFrequencyapprove lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DrugFrequencyApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveDrug_Frequency(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();

        }
    }
}
