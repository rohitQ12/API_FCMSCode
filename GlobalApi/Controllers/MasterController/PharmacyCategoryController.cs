using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyCategoryController : ControllerBase
    {
        public readonly IPharmacyCategory _repository;
        public PharmacyCategoryController()
        {
            this._repository = new PharmacyCategoryRepository();
        }

        [HttpPost, Route("InsertPharmacyCategory")]
        public async Task<ActionResult<PharmacyCategory>> Post([FromBody] PharmacyCategory lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertPharmacyCategory(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdatePharmacyCategory")]
        public async Task<ActionResult<PharmacyCategory>> Put([FromBody] PharmacyCategory lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdatePharmacyCategory(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllPharmacyCategory")]
        public async Task<ActionResult<IEnumerable<PharmacyCategory>>> GetAllPharmacyCategory()
        {
            try
            {
                var result = await this._repository.GetAllPharmacyCategory();
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

        [HttpGet, Route("GetPharmacyCategory_DD")]
        public async Task<ActionResult<IEnumerable<Pharma_DD>>> GetPharmacyCategory_DD()
        {
            try
            {
                var result = await this._repository.GetPharmacyCategory_DD();
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

        [HttpDelete, Route("DeletePharmacyCategory")]
        public async Task<ActionResult> DeletePharmacyCategory(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeletePharmacyCategory(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

    }
}
