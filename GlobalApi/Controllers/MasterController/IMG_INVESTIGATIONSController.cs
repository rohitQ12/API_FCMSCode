using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class IMG_INVESTIGATIONSController : ControllerBase
    {
        public readonly IIMG_INVESTIGATIONS _repository;
        public IMG_INVESTIGATIONSController(IIMG_INVESTIGATIONS repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertIMG_INVESTIGATIONS")]
        public async Task<ActionResult<IMG_INVESTIGATIONS>> Post([FromBody] IMG_INVESTIGATIONS lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertIMG_INVESTIGATIONS(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateIMG_INVESTIGATIONS")]
        public async Task<ActionResult<IMG_INVESTIGATIONS>> Put([FromBody] IMG_INVESTIGATIONS lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateIMG_INVESTIGATIONS(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetIMG_INVESTIGATIONS")]
        public async Task<ActionResult<IEnumerable<IMG_INVESTIGATIONS>>> GetIMG_INVESTIGATIONS()
        {
            try
            {
                var result = await this._repository.GetIMG_INVESTIGATIONS();
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
        [HttpGet, Route("GetImgInsv_DD")]
        public async Task<ActionResult<IEnumerable<ImgInsv_DD>>> GetImgInsv_DD()
        {
            try
            {
                var result = await this._repository.GetImgInsv_DD();
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
        [HttpDelete, Route("DeleteIMG_INVESTIGATIONS")]
        public async Task<ActionResult> DeleteIMG_INVESTIGATIONS(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteIMG_INVESTIGATIONS(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetImgInsvBy_Id")]
        public async Task<ActionResult<IEnumerable<ImgInsvBy_Id>>> GetImgInsvBy_Id(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetImgInsvBy_Id(Id);
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
