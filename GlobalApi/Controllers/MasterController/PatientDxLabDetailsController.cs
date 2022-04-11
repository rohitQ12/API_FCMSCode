//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using GlobalApi.IRepository.MasterIRepository;
//using GlobalApi.Models.Master;

//namespace GlobalApi.Controllers.MasterController
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PatientDxLabDetailsController : ControllerBase
//    {
//        public readonly IPatientDxLabDetails _repository;
//        public PatientDxLabDetailsController(IPatientDxLabDetails repository)
//        {
//            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
//        }

//        [HttpGet, Route("GetAllPatientDxLabDetails")]
//        public async Task<ActionResult<IEnumerable<GetPatientDxLabDetails>>> GetAllPatientDxLabDetails()
//        {
//            try
//            {
//                var result = await this._repository.GetAllPatientDxLabDetails();
//                if (result.Any())
//                {
//                    return Ok(result);
//                }

//                return NotFound();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }
//        [HttpGet, Route("GetPatientDxLabDetailsById")]
//        public async Task<ActionResult<IEnumerable<PatientDxLabDetailsBy_Id>>> GetPatientDxLabDetailsById(int Id)
//        {
//            if (Id == null)
//            {
//                return BadRequest();
//            }
//            try
//            {
//                var result = await this._repository.GetPatientDxLabDetailsById(Id);
//                if (result == null)
//                {
//                    return NotFound();
//                }
//                return Ok(result);

//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }

//        [HttpPost, Route("AcceptPatientDxLabDetails")]
//        public async Task<ActionResult> Post(int Id, int LT_Id_FK, int AcceptTest)
//        {
//            if (Id <= 0)
//            {
//                return BadRequest();
//            }
//            if (await _repository.AcceptPatientDxLabDetails(Id, LT_Id_FK, AcceptTest))
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }

//        [HttpPut, Route("UpdatePatientDxLabDetails")]
//        public async Task<ActionResult<PatientDxLabDetails>> Put([FromForm] TestReport lead)
//        {
//            if (lead == null)
//            {
//                return BadRequest();
//            }

//            var change = await _repository.UpdatePatientDxLabDetails(lead);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }

//        [HttpDelete, Route("DeletePatientDxLabDetails")]
//        public async Task<ActionResult> DeletePatientDxLabDetails(int Id)
//        {
//            if (Id <= 0)
//            {
//                return BadRequest();
//            }
//            var change = await _repository.DeletePatientDxLabDetails(Id);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }
//        [HttpGet, Route("GetLabReports")]
//        public IActionResult Get_files(string filename)
//        {
//            string _filepath = Path.GetFullPath("wwwroot/LabReports/");
//            var filepath = _filepath + filename;
//            return PhysicalFile(@filepath, "file/pdf");
//        }

//    }
//}
