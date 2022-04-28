using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        public readonly IConsultation _repository;
        public readonly FindUserId findUserId;
        public ConsultationController()
        {
            this._repository = new ConsultationRepository();
            this.findUserId = new FindUserId();
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

        [HttpGet, Route("Self/GetConsultationById")]
        public async Task<ActionResult<IEnumerable<ConsultationBy_Id>>> GetConsultationById()
        {
            
            try
            {
                var userName = User.Identity.Name.ToString();
                var patientid = await findUserId.FindPatientIdFromUserId(userName);
                var result = await this._repository.GetConsultationById(patientid);
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

        [HttpGet, Route("Admin/GetConsultationById")]
        public async Task<ActionResult<IEnumerable<AppointmentModelById>>> AdminGetConsultationById(int Appt_Id)
        {
            if (Appt_Id == 0)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetAdminConsultationById(Appt_Id);
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

        [HttpPut, Route("CloseConsultation")]
        public async Task<ActionResult> CloseConsultation(int CON_Id)
        {
            if (CON_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.CloseConsultation(CON_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

    }
}
