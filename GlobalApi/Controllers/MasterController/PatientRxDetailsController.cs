using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRxDetailsController : ControllerBase
    {
        public readonly IPatientRxDetails _repository;
        public PatientRxDetailsController()
        {
            this._repository = new PatientRxDetailsRepository();
        }

        [HttpPost, Route("InsertPatientRxDetails")]
        public async Task<ActionResult<PatientRxDetails>> Post([FromBody] Prescription_Details lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertPatientRxDetails(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPost, Route("AcceptPatientRxDetails")]
        public async Task<ActionResult> Post(int Rx_Id, int Rx_CON_Id_FK, int AcceptPrescription)
        {
            if (Rx_Id <= 0)
            {
                return BadRequest();
            }
            if (await _repository.AcceptPatientRxDetails(Rx_Id, Rx_CON_Id_FK, AcceptPrescription))
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdatePatientRxDetails")]
        public async Task<ActionResult<PatientRxDetails>> Put([FromBody] PatientRxDetails lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdatePatientRxDetails(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpDelete, Route("DeletePatientRxDetails")]
        public async Task<ActionResult> DeletePatientRxDetails(int Rx_Id)
        {
            if (Rx_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeletePatientRxDetails(Rx_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllPatientRxDetails")]
        public async Task<ActionResult<IEnumerable<PatientRxDetails>>> GetAllPatientRxDetails()
        {
            try
            {
                var result = await this._repository.GetAllPatientRxDetails();
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

        [HttpGet, Route("PatientRxDetailsById")]
        public async Task<ActionResult<IEnumerable<PatientRxDetailsById>>> GetPatientRxDetailsById(int Rx_Id)
        {
            if (Rx_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetPatientRxDetailsById(Rx_Id);
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

        [HttpGet, Route("GetDrugForSpeedSearch")]
        public async Task<ActionResult<IEnumerable<GetDrugForSpeedSearch>>> GetDrugForSpeedSearch(string EnteredText)
        {
            try
            {
                if (EnteredText == null)
                {
                    return BadRequest();
                }
                var result = await this._repository.GetDrugForSpeedSearch(EnteredText);
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

    }
}
