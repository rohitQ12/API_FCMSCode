using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientHealthRecordsController : ControllerBase
    {
        public readonly IPatientHealthRecords _repository;
        //public readonly FindUserId findUserId;
        public PatientHealthRecordsController()
        {
            this._repository = new PatientHealthRecordsRepository();
            //this.findUserId = new FindUserId();
        }

        [HttpPost, Route("InsertPatientHealthRecords")]
        public async Task<ActionResult<PatientHealthRecords>> Post([FromForm] PHR_Doc lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            //var userName = User.Identity.Name.ToString();
            //var patientid = await findUserId.FindPatientIdFromUserId(userName);
            var change = await _repository.InsertPatientHealthRecords(lead);

            if (change != null)
                return Ok(change);
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdatePatientHealthRecords")]
        public async Task<ActionResult<PatientHealthRecords>> Put([FromForm] PHR_DocUP lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdatePatientHealthRecords(lead);

            if (change != null)
                return Ok(change);
            else
                return BadRequest("Not successfull");
        }


        [HttpGet, Route("GetAllPatientHealthRecords")]
        public async Task<ActionResult<IEnumerable<GetAllPHR>>> GetAllPatientHealthRecords()
        {
            try
            {
                var result = await this._repository.GetAllPatientHealthRecords();
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

        [HttpDelete, Route("DeletePatientHealthRecords")]
        public async Task<ActionResult> DeletePatientHealthRecords(int PHR_Id)
        {
            if (PHR_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeletePatientHealthRecords(PHR_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetPatientHealthRecordsById")]
        public async Task<ActionResult<IEnumerable<PHRById>>> GetPatientHealthRecordsById(int PHR_Id)
        {
            if (PHR_Id == 0)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetPatientHealthRecordsById(PHR_Id);
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
