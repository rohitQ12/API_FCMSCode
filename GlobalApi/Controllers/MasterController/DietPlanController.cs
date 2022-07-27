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

            if (change == "Dietplan inserted successfully")
                return Ok();
            else
                return BadRequest(change);
        }
        
        [HttpPut, Route("UpdateDietPlan")]
        public async Task<ActionResult<DietPlan>> Put([FromBody] DietPlan lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDietPlan(lead);

            if (change == "DietPlan updated successfully")
                return Ok();
            else
                return BadRequest(change);
        }
        
        [HttpGet, Route("GetAllDietPlan")]
        public async Task<ActionResult<IEnumerable<GetAllDietPlan>>> GetAllDietPlan()
        {
            
                var result = await this._repository.GetAllDietPlan();
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound("Dietplans not found");
        }

        [HttpDelete, Route("DeleteDietPlan")]
        public async Task<ActionResult> DeleteDietPlan(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDietPlan(Id);

            if (change == "DietPlan deleted successfully")
                return Ok();
            else
                return BadRequest(change);
        }
        
        [HttpGet, Route("GetDietPlanById")]
        public async Task<ActionResult<IEnumerable<GetAllDietPlan>>> GetDietPlanById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
           
                var result = await this._repository.GetDietPlanById(Id);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound("Dietplan not found");
        }

    }
}
