using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Consult_diagController : ControllerBase
    {
        public readonly IConsult_diag _repository;
        public Consult_diagController()
        {
            this._repository = new Consult_diagRepository();
        }

        [HttpPost, Route("Insert_Consult_diag")]
        public async Task<ActionResult<Consulr_diag>> Post([FromBody] Consulr_diag diagData)
        {
            if (diagData == null)
            {
                return BadRequest();
            }
            var change = await _repository.Insert_Consult_diag(diagData);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("Update_Consult_diag")]
        public async Task<ActionResult<Consulr_diag>> Put([FromBody] Consulr_diag UpdConDiag)
        {
            if (UpdConDiag == null)
            {
                return BadRequest();
            }

            var change = await _repository.Update_Consult_diag(UpdConDiag);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }


        [HttpGet, Route("GetAll_Consult_diag")]
        public async Task<ActionResult<IEnumerable<Consulr_diag_GetAll>>> GetAll()
        {
            try
            {

                var result = await this._repository.GetAll_Consult_diag();
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

        [HttpDelete, Route("Delete_Consult_diag")]
        public async Task<ActionResult> Delete(int Dlt_Id)
        {
            if (Dlt_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.Delete_Consult_diag(Dlt_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetById_Consult_diag")]
        public async Task<ActionResult<IEnumerable<CurrencyById>>> GetById(int Conslt_id)
        {
            if (Conslt_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetById_Consult_diag(Conslt_id);
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
