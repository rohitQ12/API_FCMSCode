using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImgTestDetailsController : ControllerBase
    {
        public readonly IImgTestDetails _repository;
        public ImgTestDetailsController(IImgTestDetails repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //[HttpPost, Route("InsertImgTestDetails")]
        //public async Task<ActionResult<ImgTestDetails>> Post([FromBody] List<ImgTestDetails> lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertImgTestDetails(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        [HttpPut, Route("UpdateImgTestDetails")]
        public async Task<ActionResult<ImgTestDetails>> Put([FromForm] ImgReport lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateImgTestDetails(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpDelete, Route("DeleteImgTestDetails")]
        public async Task<ActionResult> DeleteImgTestDetails(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteImgTestDetails(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllImgTestDetails")]
        public async Task<ActionResult<IEnumerable<GetAllImgTestDetails>>> GetAllImgTestDetails()
        {
            try
            {
                var result = await this._repository.GetAllImgTestDetails();
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
        [HttpGet, Route("GetImgTestDetailsById")]
        public async Task<ActionResult<IEnumerable<ImgTestDetailsById>>> GetImgTestDetailsById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetImgTestDetailsById(Id);
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

        [HttpGet, Route("GetImgReports")]
        public IActionResult Get_files(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/ImgReports/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "file/pdf");
        }

    }
}
