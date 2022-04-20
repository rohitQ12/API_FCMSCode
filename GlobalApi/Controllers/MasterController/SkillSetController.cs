using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillSetController : ControllerBase
    {
        public readonly ISkillSet _repository;
        public SkillSetController()
        {
            this._repository = new SkillSetRepository();
        }

        [HttpPost, Route("InsertSkillSet")]
        public async Task<ActionResult<SkillSets>> Post([FromBody] SkillSets lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertSkillSet(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateSkillSet")]
        public async Task<ActionResult<SkillSets>> Put([FromBody] SkillSets lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateSkillSet(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllSkillSet")]
        public async Task<ActionResult<IEnumerable<Qual_SkillSet>>> GetAllSkillSet()
        {
            try
            {
                var result = await this._repository.GetAllSkillSet();
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
        [HttpGet, Route("GetSkillSet_DD")]
        public async Task<ActionResult<IEnumerable<SkillSet_DD>>> GetSkillSet_DD()
        {
            try
            {
                var result = await this._repository.GetSkillSet_DD();
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
        [HttpDelete, Route("DeleteSkillSet")]
        public async Task<ActionResult> DeleteSkillSet(int Skillset_id)
        {
            if (Skillset_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteSkillSet(Skillset_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetSkillSetById")]
        public async Task<ActionResult<IEnumerable<SkillSetById>>> GetSkillSetById(int Skillset_id)
        {
            if (Skillset_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetSkillSetById(Skillset_id);
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
