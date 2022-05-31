using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorDocumentController : ControllerBase
    {
        public readonly IDoctorDocument _repository;
        //public readonly FindUserId findUserId;
        public DoctorDocumentController()
        {
            this._repository = new DoctorDocumentRepository();
            //this.findUserId = new FindUserId();
        }

        //[HttpPost, Route("InsertDoctorDocument")]
        //public async Task<ActionResult<DoctorDocument>> Post([FromForm] Doctor_Documents lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    //var userName = User.Identity.Name.ToString();
        //    //var doctorid = await findUserId.FindDoctorIdFromUserId(userName);
        //    var change = await _repository.InsertDoctorDocument(lead, 1);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        [HttpPost, Route("InsertDoctorDocument")]
        public async Task<ActionResult<DoctorDocument>> Post([FromForm] Doctor_Documents lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            //var userName = User.Identity.Name.ToString();
            //var doctorid = await findUserId.FindDoctorIdFromUserId(userName);
            var change = await _repository.InsertDoctorDocument(lead, 2);

            if (change != null)
                return Ok(change);
            else
                return BadRequest("Not successfull");
        }


        [HttpPut, Route("UpdateDoctorDocument")]
        public async Task<ActionResult<DoctorDocument>> Put([FromForm] Doctor_Documents lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDoctorDocument(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllDoctorDocument")]
        public async Task<ActionResult<IEnumerable<DoctorDocument>>> GetAllDoctorDocument()
        {
            try
            {
                var result = await this._repository.GetAllDoctorDocument();
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

        [HttpDelete, Route("DeleteDoctorDocument")]
        public async Task<ActionResult> DeleteDoctorDocument(int Doc_Id)
        {
            if (Doc_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDoctorDocument(Doc_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetDoctorDocumentById")]
        public async Task<ActionResult<IEnumerable<DoctorDocumentById>>> GetDoctorDocumentById(int Doc_Id)
        {
            if (Doc_Id == 0)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDoctorDocumentById(Doc_Id);
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

        [HttpGet, Route("GetDoctor_Documents")]
        public IActionResult Get_documents(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/DoctorDocuments/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "file/pdf");
        }

    }
}
