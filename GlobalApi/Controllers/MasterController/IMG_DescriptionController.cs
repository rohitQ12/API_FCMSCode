using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class IMG_DescriptionController : ControllerBase
    {
        public readonly IIMG_Description _repository;
        public IMG_DescriptionController()
        {
            this._repository = new IMG_DescriptionRepository();
        }

        [HttpPost, Route("InsertIMG_Description")]
        public async Task<ActionResult<IMG_Description>> Post([FromBody] IMG_Description lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertIMG_Description(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateIMG_Description")]
        public async Task<ActionResult<IMG_Description>> Put([FromBody] IMG_Description lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateIMG_Description(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllIMG_Description")]
        public async Task<ActionResult<IEnumerable<GetAllIMG_Desc>>> GetAllIMG_Description()
        {
            try
            {
                var result = await this._repository.GetAllIMG_Description();
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

        [HttpGet, Route("GetImgDesc_DD")]
        public async Task<ActionResult<IEnumerable<Img_Desc_DD>>> GetImgDesc_DD()
        {
            try
            {
                var result = await this._repository.GetImgDesc_DD();
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

        [HttpDelete, Route("DeleteIMG_Description")]
        public async Task<ActionResult> DeleteIMG_Description(int Img_DescId)
        {
            if (Img_DescId <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteIMG_Description(Img_DescId);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetImgDesc_ById")]
        public async Task<ActionResult<IEnumerable<GetImgDesc_ById>>> GetImgDesc_ById(int Img_DescId)
        {
            if (Img_DescId == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetImgDesc_ById(Img_DescId);
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
