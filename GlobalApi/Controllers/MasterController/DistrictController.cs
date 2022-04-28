using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DistrictController : ControllerBase
    {
        public readonly IDistrict _repository;
        public DistrictController()
        {
            this._repository =new DistrictRepository();
        }

        [HttpPost, Route("InsertDistrict")]
        public async Task<ActionResult<Districts>> Post([FromBody] Districts lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDistrict(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateDistrict")]
        public async Task<ActionResult<Districts>> Put([FromBody] Districts lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDistrict(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetDistrict_DD")]
        public async Task<ActionResult<IEnumerable<District_DD>>> GetDistrict_DD(int stat_id)
        {
            try
            {
                var result = await this._repository.GetDistrict_DD(stat_id);
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
        
        [HttpDelete, Route("DeleteDistrict")]
        public async Task<ActionResult> DeleteDistrict(int district_id)
        {
            if (district_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDistrict(district_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetDistrictById")]
        public async Task<ActionResult<IEnumerable<DistrictById>>> GetDistrictById(int district_id)
        {
            if (district_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDistrictById(district_id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpGet, Route("GetAllDistrict")]
        public async Task<ActionResult<IEnumerable<GetStateDistrict>>> GetAllDistrict()
        {
            try
            {
                var result = await this._repository.GetAllDistrict();
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

    }
}
