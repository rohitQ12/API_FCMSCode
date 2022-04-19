using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymptomsController : ControllerBase
    {
        public readonly ISymptoms _repository;
        public SymptomsController(ISymptoms repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //[HttpPost, Route("InsertSymptoms")]
        //public async Task<ActionResult<Symptoms>> Post([FromBody] Symptoms lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertSymptoms(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}
        
        [HttpPut, Route("UpdateSymptoms")]
        public async Task<ActionResult<Symptoms>> Put([FromBody] Symptoms lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateSymptoms(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAllSymptoms")]
        public async Task<ActionResult<IEnumerable<GetAllSymptoms>>> GetAllSymptoms()
        {
            try
            {
                var result = await this._repository.GetAllSymptoms();
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
        
        //[HttpGet, Route("GetSymptoms_DD")]
        //public async Task<ActionResult<IEnumerable<Symptoms_DD>>> GetSymptoms_DD()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetSymptoms_DD();
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        
        [HttpDelete, Route("DeleteSymptoms")]
        public async Task<ActionResult> DeleteSymptoms(int SYM_Id)
        {
            if (SYM_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteSymptoms(SYM_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetSymptomsById")]
        public async Task<ActionResult<IEnumerable<SymptomsBy_Id>>> GetSymptomsById(int SYM_APPT_PR_Id_FK)
        {
            if (SYM_APPT_PR_Id_FK == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetSymptomsById(SYM_APPT_PR_Id_FK);
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
