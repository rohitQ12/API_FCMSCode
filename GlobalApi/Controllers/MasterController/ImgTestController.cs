using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImgTestController : ControllerBase
    {
        public readonly IImgTest _repository;
        public ImgTestController(IImgTest repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertImgTest")]
        public async Task<ActionResult<ImgTest>> Post([FromBody] ImgTest_Details lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertImgTest(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPost, Route("AcceptImgTest")]
        public async Task<ActionResult> Post(int Id, int Img_CON_Id_FK, bool AcceptImgTest)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            if (await _repository.AcceptImgTest(Id, Img_CON_Id_FK, AcceptImgTest))
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateImgTest")]
        public async Task<ActionResult<ImgTest>> Put([FromBody] ImgTest lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateImgTest(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllImgTest")]
        public async Task<ActionResult<IEnumerable<GetAllImgTest>>> GetAllImgTest()
        {
            try
            {
                var result = await this._repository.GetAllImgTest();
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
        [HttpDelete, Route("DeleteImgTest")]
        public async Task<ActionResult> DeleteImgTest(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteImgTest(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetImgTestById")]
        public async Task<ActionResult<IEnumerable<ImgTestById>>> GetImgTestById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetImgTestById(Id);
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
