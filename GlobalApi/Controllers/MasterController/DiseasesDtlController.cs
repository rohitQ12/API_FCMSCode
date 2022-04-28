using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesDtlController : ControllerBase
    {
        public readonly IDiseasesDtl _repository;
        public DiseasesDtlController()
        {
            this._repository = new DiseasesDtlRepository();
        }

        //[HttpPost, Route("InsertDiseasesDtl")]
        //public async Task<ActionResult<DiseasesDtl>> Post([FromBody] DiseasesDtl lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertDiseasesDtl(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        [HttpPut, Route("UpdateDiseasesDtl")]
        public async Task<ActionResult<DiseasesDtl>> Put([FromBody] List<DiseasesDtl> lead, int App_id)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDiseasesDtltest(lead, App_id);

            if (change == true)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllDiseasesDtl")]
        public async Task<ActionResult<IEnumerable<DiseasesDtl>>> GetAllDiseasesDtl()
        {
            try
            {
                var result = await this._repository.GetAllDiseasesDtl();
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

        [HttpDelete, Route("DeleteDiseasesDtl")]
        public async Task<ActionResult> DeleteDiseasesDtl(int Ddtl_Id)
        {
            if (Ddtl_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDiseasesDtl(Ddtl_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetDiseasesDtlById")]
        public async Task<ActionResult<IEnumerable<GetDiseaseDtlById>>> GetDiseasesDtlById(int Ddtl_PR_Id_FK)
        {
            if (Ddtl_PR_Id_FK == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDiseasesDtlById(Ddtl_PR_Id_FK);
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
