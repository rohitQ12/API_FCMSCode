using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Patient_Prescription_DTLController : ControllerBase
    {
        public readonly IPatient_Prescription_DTL _repository;
        public Patient_Prescription_DTLController()
        {
            this._repository = new Patient_Prescription_DTLRepository();
        }

        //[HttpPost, Route("InsertPatient_Prescription_DTL")]
        //public async Task<ActionResult<Patient_Prescription_DTL>> Post([FromBody] List<Patient_Prescription_DTL> lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertPatient_Prescription_DTL(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}
        [HttpPut, Route("UpdatePatient_Prescription_DTL")]
        public async Task<ActionResult<Patient_Prescription_DTL>> Put([FromBody] Patient_Prescription_DTL lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdatePatient_Prescription_DTL(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllPatient_Prescription_DTL")]
        public async Task<ActionResult<IEnumerable<GetAllPPD>>> GetAllPatient_Prescription_DTL()
        {
            try
            {
                var result = await this._repository.GetAllPatient_Prescription_DTL();
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

        [HttpDelete, Route("DeletePatient_Prescription_DTL")]
        public async Task<ActionResult> DeletePatient_Prescription_DTL(int Dtl_Id)
        {
            if (Dtl_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeletePatient_Prescription_DTL(Dtl_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetPatient_Prescription_DTLById")]
        public async Task<ActionResult<IEnumerable<PPD_By_Id>>> GetPatient_Prescription_DTLById(int Dtl_Id)
        {
            if (Dtl_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetPatient_Prescription_DTLById(Dtl_Id);
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
