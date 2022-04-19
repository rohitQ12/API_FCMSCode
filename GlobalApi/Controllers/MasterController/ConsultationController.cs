using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        public readonly IConsultation _repository;
        public ConsultationController(IConsultation repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //[HttpPost, Route("InsertConsultation")]
        //public async Task<ActionResult<Consultation>> Post([FromBody] Consultation lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertConsultation(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}
        
        
        [HttpPut, Route("UpdateConsultation")]
        public async Task<ActionResult<Consultation>> Put([FromBody] Consultation lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateConsultation(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpGet, Route("GetAllConsultation")]
        public async Task<ActionResult<IEnumerable<GetAllConsultation>>> GetAllConsultation()
        {
            try
            {
                var result = await this._repository.GetAllConsultation();
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

        
        [HttpDelete, Route("DeleteConsultation")]
        public async Task<ActionResult> DeleteConsultation(int CON_Id)
        {
            if (CON_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteConsultation(CON_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetConsultationById")]
        public async Task<ActionResult<IEnumerable<ConsultationBy_Id>>> GetConsultationById(int CON_PR_Id_FK)
        {
            if (CON_PR_Id_FK == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetConsultationById(CON_PR_Id_FK);
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
