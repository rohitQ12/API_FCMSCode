using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class IMG_SUBINVESTIGATIONSController : ControllerBase
    {
        public readonly IIMG_SUBINVESTIGATIONS _repository;
        public IMG_SUBINVESTIGATIONSController()
        {
            this._repository = new IMG_SUBINVESTIGATIONSRepository();
        }

        [HttpPost, Route("InsertIMG_SUBINVESTIGATIONS")]
        public async Task<ActionResult<IMG_SUBINVESTIGATIONS>> Post([FromBody] IMG_SUBINVESTIGATIONS lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertIMG_SUBINVESTIGATIONS(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateIMG_SUBINVESTIGATIONS")]
        public async Task<ActionResult<IMG_SUBINVESTIGATIONS>> Put([FromBody] IMG_SUBINVESTIGATIONS lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateIMG_SUBINVESTIGATIONS(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetIMG_SUBINVESTIGATIONS")]
        public async Task<ActionResult<IEnumerable<IMG_SUBINVESTIGATIONS>>> GetIMG_SUBINVESTIGATIONS()
        {
            try
            {
                var result = await this._repository.GetIMG_SUBINVESTIGATIONS();
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
        
        [HttpGet, Route("GetImgSubInsv_DD")]
        public async Task<ActionResult<IEnumerable<ImgSubInsv_DD>>> GetImgSubInsv_DD(int Img_Invt_Id)
        {
            try
            {
                var result = await this._repository.GetImgSubInsv_DD(Img_Invt_Id);
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
        
        [HttpDelete, Route("DeleteIMG_SUBINVESTIGATIONS")]
        public async Task<ActionResult> DeleteIMG_SUBINVESTIGATIONS(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteIMG_SUBINVESTIGATIONS(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetImgSubInsvBy_Id")]
        public async Task<ActionResult<IEnumerable<ImgSubInsvBy_Id>>> GetImgSubInsvBy_Id(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetImgSubInsvBy_Id(Id);
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
