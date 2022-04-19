using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestingDetailsController : ControllerBase
    {
        public readonly ILabTestingDetails _repository;
        public LabTestingDetailsController(ILabTestingDetails repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //[HttpPost, Route("InsertLabTestingDetails")]
        //public async Task<ActionResult<LabTestingDetails>> Post([FromBody] List<LabTestingDetails> lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();

        //    }
        //    var change = await _repository.InsertLabTestingDetails(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        [HttpPut, Route("UpdateLabTestingDetails")]
        public async Task<ActionResult<LabTestingDetails>> Put([FromForm] TestReport lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateLabTestingDetails(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpDelete, Route("DeleteLabTestingDetails")]
        public async Task<ActionResult> DeleteLabTestingDetails(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteLabTestingDetails(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllLabTestingDetails")]
        public async Task<ActionResult<IEnumerable<GetLabTestingDetails>>> GetAllLabTestingDetails()
        {
            try
            {
                var result = await this._repository.GetAllLabTestingDetails();
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
        
        [HttpGet, Route("GetLabTestingDetailsById")]
        public async Task<ActionResult<IEnumerable<LabTestingDetailsById>>> GetLabTestingDetailsById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetLabTestingDetailsById(Id);
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

        [HttpGet, Route("GetLabReports")]
        public IActionResult Get_files(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/LabReports/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "file/pdf");
        }
    }
}
