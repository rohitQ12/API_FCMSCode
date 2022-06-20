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
    public class Drug_TypeController : ControllerBase
    {
        public readonly IDrug_TypeRepository _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public Drug_TypeController()
        {
            this._repository = new Drug_TypeRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertDrug_Type")]
        public async Task<ActionResult<Drug_Type>> Post([FromBody] Drug_Type lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDrug_Type(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateDrug_Type")]
        public async Task<ActionResult<Drug_Type>> Put([FromBody] Drug_Type lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDrug_Type(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllDrug_Type")]
        public async Task<ActionResult<IEnumerable<Drug_TypeAll>>> GetAllDrug_Type()
        {
            try
            {
                var result = await this._repository.GetAllDrug_Type();
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

        [HttpDelete, Route("DeleteDrug_Type")]
        public async Task<ActionResult> DeleteDrug_Type(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDrug_Type(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetDrug_Type_DD")]
        public async Task<ActionResult<IEnumerable<Drug_TypeDD>>> GetDrugTypeDD()
        {
            try
            {
                var result = await this._repository.GetDrug_Type_DD();
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


        [HttpPut, Route("ApproveDrug_Type")]
        public async Task<IActionResult> ApproveDrug_Type([FromBody] DrugTypeapprove lead)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "DrugTypeApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveDrug_Type(lead);

                if (change != null)
                    return Ok();
                else
                    return BadRequest("Not successfull");
            }
            return Unauthorized();

        }
    }
}
