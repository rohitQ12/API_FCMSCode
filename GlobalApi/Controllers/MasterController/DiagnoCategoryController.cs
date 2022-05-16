using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnoCategoryController : ControllerBase
    {
        public readonly IDiagnoCategory _repository;
        public DiagnoCategoryController()
        {
            this._repository = new DiagnoCategoryRepository();
        }

        [HttpPost, Route("InsertDiagnoCategory")]
        public async Task<ActionResult<DiagnoCategory>> Post([FromBody] DiagnoCategory lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDiagnoCategory(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateDiagnoCategory")]
        public async Task<ActionResult<DiagnoCategory>> Put([FromBody] DiagnoCategory lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDiagnoCategory(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllDiagnoCategory")]
        public async Task<ActionResult<IEnumerable<DiagnoCategory>>> GetAllDiagnoCategory()
        {
            try
            {
                var result = await this._repository.GetAllDiagnoCategory();
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

        [HttpGet, Route("GetDiagnoCategory_DD")]
        public async Task<ActionResult<IEnumerable<Diagno_DD>>> GetDiagnoCategory_DD()
        {
            try
            {
                var result = await this._repository.GetDiagnoCategory_DD();
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

        [HttpDelete, Route("DeleteDiagnoCategory")]
        public async Task<ActionResult> DeleteDiagnoCategory(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDiagnoCategory(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

    }
}
