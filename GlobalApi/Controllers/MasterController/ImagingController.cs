//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using GlobalApi.IRepository.MasterIRepository;
//using GlobalApi.Models.Master;

//namespace GlobalApi.Controllers.MasterController
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ImagingController : ControllerBase
//    {
//        public readonly IImaging _repository;
//        public ImagingController(IImaging repository)
//        {
//            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
//        }

//        [HttpPost, Route("InsertImaging")]
//        public async Task<ActionResult<Imaging>> Post([FromBody] Imaging lead)
//        {
//            if (lead == null)
//            {
//                return BadRequest();
//            }
//            var change = await _repository.InsertImaging(lead);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }
//        [HttpPut, Route("UpdateImaging")]
//        public async Task<ActionResult<Imaging>> Put([FromBody] Imaging lead)
//        {
//            if (lead == null)
//            {
//                return BadRequest();
//            }

//            var change = await _repository.UpdateImaging(lead);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }
//        [HttpGet, Route("GetAllImaging")]
//        public async Task<ActionResult<IEnumerable<GetImaging>>> GetAllImaging()
//        {
//            try
//            {
//                var result = await this._repository.GetAllImaging();
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
//        [HttpDelete, Route("DeleteImaging")]
//        public async Task<ActionResult> DeleteImaging(int Id)
//        {
//            if (Id <= 0)
//            {
//                return BadRequest();
//            }
//            var change = await _repository.DeleteImaging(Id);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }
//        [HttpGet, Route("GetImagingById")]
//        public async Task<ActionResult<IEnumerable<ImagingBy_Id>>> GetImagingById(int Id)
//        {
//            if (Id == null)
//            {
//                return BadRequest();
//            }
//            try
//            {
//                var result = await this._repository.GetImagingById(Id);
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
