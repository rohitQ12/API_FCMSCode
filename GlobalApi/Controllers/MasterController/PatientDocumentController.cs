using GlobalApi.Repository.MasterRepository;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.GlobalClasses;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDocumentController : ControllerBase
    {
        public readonly IPatientDocument _repository;
        public readonly FindUserId findUserId;
        public PatientDocumentController()
        {
            this._repository = new PatientDocumentRepository();
            this.findUserId = new FindUserId();
        }

        [HttpPost, Route("InsertPatientDocument")]
        public async Task<ActionResult<PatientDocument>> Post([FromForm] Patient_Documents lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            //var userName = User.Identity.Name.ToString();
            //var patientid = await findUserId.FindPatientIdFromUserId(userName);
            var change = await _repository.InsertPatientDocument(lead , 258);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        //[HttpPost, Route("Test/certificatestest")]
        //[RequestSizeLimit(long.MaxValue)]
        //public ActionResult<PatientDocument> test([Required] List<IFormFile> certificatesd)
        //{
        //    return Ok();
        //}

        //[HttpPost("fileupload")]
        //public IActionResult FileUpload([FromForm] MyFileUploadClass @class)  // -> property name must be the same used as formdata key
        //{
        //    // do the magic here
        //    return NoContent();
        //}
        
        [HttpPut, Route("UpdatePatientDocument")]
        public async Task<ActionResult<PatientDocument>> Put([FromForm] Patient_DocumentsUP lead)
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
            if (Doc_Id == 0)
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
