using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplineController : ControllerBase
    {
        public readonly IDiscipline _repository;
        public DisciplineController()
        {
            this._repository = new DisciplineRepository();
        }

        [HttpPost, Route("InsertDiscipline")]
        public async Task<ActionResult<Discipline>> Post([FromBody] Discipline lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDiscipline(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateDiscipline")]
        public async Task<ActionResult<Discipline>> Put([FromBody] Discipline lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDiscipline(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllDiscipline")]
        public async Task<ActionResult<IEnumerable<Discipline>>> GetAllDiscipline()
        {
            try
            {
                var result = await this._repository.GetAllDiscipline();
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
        [HttpGet, Route("GetDiscipline_DD")]
        public async Task<ActionResult<IEnumerable<Discipline_DD>>> GetDiscipline_DD()
        {
            try
            {
                var result = await this._repository.GetDiscipline_DD();
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
        [HttpDelete, Route("DeleteDiscipline")]
        public async Task<ActionResult> DeleteDiscipline(int CD_Id)
        {
            if (CD_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDiscipline(CD_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetDisciplineById")]
        public async Task<ActionResult<IEnumerable<DisciplineById>>> GetDisciplineById(int CD_Id)
        {
            if (CD_Id == 0)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDisciplineById(CD_Id);
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
