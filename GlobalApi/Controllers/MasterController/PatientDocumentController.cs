using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDocumentController : ControllerBase
    {
        public readonly IPatientDocument _repository;
        public PatientDocumentController()
        {
            this._repository = new PatientDocumentRepository();
        }

        [HttpPost, Route("InsertPatientDocument")]
        public async Task<ActionResult<PatientDocument>> Post([FromForm] List<Patient_Documents> lead, int PR_Id_FK)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertPatientDocument(lead , PR_Id_FK);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdatePatientDocument")]
        public async Task<ActionResult<PatientDocument>> Put([FromBody] PatientDocument lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdatePatientDocument(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAllPatientDocument")]
        public async Task<ActionResult<IEnumerable<PatientDocument>>> GetAllPatientDocument()
        {
            try
            {
                var result = await this._repository.GetAllPatientDocument();
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

        [HttpDelete, Route("DeletePatientDocument")]
        public async Task<ActionResult> DeletePatientDocument(int Doc_Id)
        {
            if (Doc_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeletePatientDocument(Doc_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetPatientDocumentById")]
        public async Task<ActionResult<IEnumerable<PatientDocumentById>>> GetPatientDocumentById(int Doc_Id)
        {
            if (Doc_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetPatientDocumentById(Doc_Id);
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

        [HttpGet, Route("GetPatient_Documents")]
        public IActionResult Get_documents(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/PatientDocuments/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "file/pdf");
        }

    }
}
