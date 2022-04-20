using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietPlanController : ControllerBase
    {
        public readonly IDietPlan _repository;
        public DietPlanController()
        {
            this._repository = new DietPlanRepository();
        }

        [HttpPost, Route("InsertDietPlan")]
        public async Task<ActionResult<DietPlan>> Post([FromBody] DietPlan lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDietPlan(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateDietPlan")]
        public async Task<ActionResult<DietPlan>> Put([FromBody] DietPlan lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDietPlan(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllDietPlan")]
        public async Task<ActionResult<IEnumerable<GetAllDietPlan>>> GetAllDietPlan()
        {
            try
            {
                var result = await this._repository.GetAllDietPlan();
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
        [HttpDelete, Route("DeleteDietPlan")]
        public async Task<ActionResult> DeleteDietPlan(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDietPlan(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetDietPlanById")]
        public async Task<ActionResult<IEnumerable<GetById>>> GetDietPlanById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDietPlanById(Id);
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

    }
}
