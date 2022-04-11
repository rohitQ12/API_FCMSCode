//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using GlobalApi.IRepository.MasterIRepository;
//using GlobalApi.Models.Master;

//namespace GlobalApi.Controllers.MasterController
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LabTestController : ControllerBase
//    {
//        public readonly ILabTest _repository;
//        public LabTestController(ILabTest repository)
//        {
//            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
//        }

//        [HttpPost, Route("InsertLabTest")]
//        public async Task<ActionResult<LabTest>> Post([FromBody] List<LabTest> lead)
//        {
//            if (lead == null)
//            {
//                return BadRequest();
//            }
//            var change = await _repository.InsertLabTest(lead);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }
//        [HttpPut, Route("UpdateLabTest")]
//        public async Task<ActionResult<LabTest>> Put([FromBody] LabTest lead)
//        {
//            if (lead == null)
//            {
//                return BadRequest();
//            }

//            var change = await _repository.UpdateLabTest(lead);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }
//        [HttpGet, Route("GetAllLabTest")]
//        public async Task<ActionResult<IEnumerable<GetLabTest>>> GetAllLabTest()
//        {
//            try
//            {
//                var result = await this._repository.GetAllLabTest();
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
//        [HttpDelete, Route("DeleteLabTest")]
//        public async Task<ActionResult> DeleteLabTest(int Id)
//        {
//            if (Id <= 0)
//            {
//                return BadRequest();
//            }
//            var change = await _repository.DeleteLabTest(Id);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }
//        [HttpGet, Route("GetLabTestById")]
//        public async Task<ActionResult<IEnumerable<LabTestBy_Id>>> GetLabTestById(int Id)
//        {
//            if (Id == null)
//            {
//                return BadRequest();
//            }
//            try
//            {
//                var result = await this._repository.GetLabTestById(Id);
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

//    }
//}
