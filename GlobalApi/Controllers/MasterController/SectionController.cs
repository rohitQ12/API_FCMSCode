using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        public readonly ISection _repository;
        public SectionController()
        {
            this._repository = new SectionRepository();
        }

        [HttpPost, Route("InsertSection")]
        public async Task<ActionResult<Section>> Post([FromBody] Section lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertSection(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateSection")]
        public async Task<ActionResult<Section>> Put([FromBody] Section lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateSection(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAllSection")]
        public async Task<ActionResult<IEnumerable<GetAllSection>>> GetAllSection()
        {
            try
            {
                var result = await this._repository.GetAllSection();
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
        
        [HttpGet, Route("GetSection_DD")]
        public async Task<ActionResult<IEnumerable<Section_DD>>> GetSection_DD()
        {
            try
            {
                var result = await this._repository.GetSection_DD();
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
        
        [HttpDelete, Route("DeleteSection")]
        public async Task<ActionResult> DeleteSection(int Section_Id)
        {
            if (Section_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteSection(Section_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetSectionById")]
        public async Task<ActionResult<IEnumerable<SectionById>>> GetSectionById(int Section_Id)
        {
            if (Section_Id == 0)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetSectionById(Section_Id);
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
