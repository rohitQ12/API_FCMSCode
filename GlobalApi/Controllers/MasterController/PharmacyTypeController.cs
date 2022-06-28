using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyTypeController : ControllerBase
    {
        public readonly IPharmacyType _repository;
        public PharmacyTypeController()
        {
            this._repository = new PharmacyTypeRepository();
        }

        [HttpPost, Route("InsertPharmacyType")]
        public async Task<ActionResult<PharmacyType>> Post([FromBody] PharmacyType lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertPharmacyType(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdatePharmacyType")]
        public async Task<ActionResult<PharmacyType>> Put([FromBody] PharmacyType lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdatePharmacyType(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllPharmacyType")]
        public async Task<ActionResult<IEnumerable<PharmacyType>>> GetAllPharmacyType()
        {
            try
            {
                var result = await this._repository.GetAllPharmacyType();
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

        [HttpGet, Route("GetPharmacyType_DD")]
        public async Task<ActionResult<IEnumerable<HosType_DD>>> GetPharmacyType_DD()
        {
            try
            {
                var result = await this._repository.GetPharmacyType_DD();
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

        [HttpDelete, Route("DeletePharmacyType")]
        public async Task<ActionResult> DeletePharmacyType(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeletePharmacyType(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

    }
}
