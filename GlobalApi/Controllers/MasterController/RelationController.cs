using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationController : ControllerBase
    {
        public readonly IRelation _repository;
        public RelationController(IRelation repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertRelation")]
        public async Task<ActionResult<Relation>> Post([FromBody] Relation lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertRelation(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateRelation")]
        public async Task<ActionResult<Relation>> Put([FromBody] Relation lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateRelation(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAllRelation")]
        public async Task<ActionResult<IEnumerable<Relation>>> GetAllRelation()
        {
            try
            {
                var result = await this._repository.GetAllRelation();
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
        
        [HttpGet, Route("GetRelation_DD")]
        public async Task<ActionResult<IEnumerable<Relation_DD>>> GetRelation_DD()
        {
            try
            {
                var result = await this._repository.GetRelation_DD();
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
        
        [HttpDelete, Route("DeleteRelation")]
        public async Task<ActionResult> DeleteRelation(int relation_id)
        {
            if (relation_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteRelation(relation_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetRelationById")]
        public async Task<ActionResult<IEnumerable<RelationById>>> GetRelationById(int relation_id)
        {
            if (relation_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetRelationById(relation_id);
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
