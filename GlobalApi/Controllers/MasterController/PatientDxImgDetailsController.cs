//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using GlobalApi.IRepository.MasterIRepository;
//using GlobalApi.Models.Master;

//namespace GlobalApi.Controllers.MasterController
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PatientDxImgDetailsController : ControllerBase
//    {
//        public readonly IPatientDxImgDetails _repository;
//        public PatientDxImgDetailsController(IPatientDxImgDetails repository)
//        {
//            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
//        }

//        [HttpGet, Route("GetAllPatientDxImgDetails")]
//        public async Task<ActionResult<IEnumerable<GetPatientDxImgDetails>>> GetAllPatientDxImgDetails()
//        {
//            try
//            {
//                var result = await this._repository.GetAllPatientDxImgDetails();
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
//        [HttpGet, Route("GetPatientDxImgDetailsById")]
//        public async Task<ActionResult<IEnumerable<PatientDxImgDetailsBy_Id>>> GetPatientDxImgDetailsById(int Id)
//        {
//            if (Id == null)
//            {
//                return BadRequest();
//            }
//            try
//            {
//                var result = await this._repository.GetPatientDxImgDetailsById(Id);
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

//        [HttpPost, Route("AcceptPatientDxImgDetails")]
//        public async Task<ActionResult> Post(int Id, int Img_Id_FK, int AcceptTest)
//        {
//            if (Id <= 0)
//            {
//                return BadRequest();
//            }
//            if (await _repository.AcceptPatientDxImgDetails(Id, Img_Id_FK, AcceptTest))
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }

//        [HttpPut, Route("UpdatePatientDxImgDetails")]
//        public async Task<ActionResult<PatientDxImgDetails>> Put([FromForm] ImgReport lead)
//        {
//            if (lead == null)
//            {
//                return BadRequest();
//            }

//            var change = await _repository.UpdatePatientDxImgDetails(lead);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }

//        [HttpDelete, Route("DeletePatientDxImgDetails")]
//        public async Task<ActionResult> DeletePatientDxImgDetails(int Id)
//        {
//            if (Id <= 0)
//            {
//                return BadRequest();
//            }
//            var change = await _repository.DeletePatientDxImgDetails(Id);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }

//        [HttpGet, Route("GetImgReports")]
//        public IActionResult Get_files(string filename)
//        {
//            string _filepath = Path.GetFullPath("wwwroot/ImgReports/");
//            var filepath = _filepath + filename;
//            return PhysicalFile(@filepath, "file/pdf");
//        }

//    }
//}
