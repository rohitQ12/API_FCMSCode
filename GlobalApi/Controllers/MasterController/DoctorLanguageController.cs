using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorLanguageController : ControllerBase
    {
        public readonly IDoctorLanguage _repository;
        public DoctorLanguageController(IDoctorLanguage repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //[HttpPost, Route("InsertDoctorLanguage")]
        //public async Task<ActionResult<DoctorLanguage>> Post([FromBody] DoctorLanguage lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertDoctorLanguage(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}
        //[HttpPut, Route("UpdateDoctorLanguage")]
        //public async Task<ActionResult<DoctorLanguage>> Put([FromBody] DoctorLanguage lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }

        //    var change = await _repository.UpdateDoctorLanguage(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}
        [HttpGet, Route("GetAllDoctorLanguage")]
        public async Task<ActionResult<IEnumerable<DoctorLanguage>>> GetAllDoctorLanguage()
        {
            try
            {
                var result = await this._repository.GetAllDoctorLanguage();
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
        [HttpDelete, Route("DeleteDoctorLanguage")]
        public async Task<ActionResult> DeleteDoctorLanguage(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDoctorLanguage(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetDoctorLanguageById")]
        public async Task<ActionResult<IEnumerable<GetDoctorlang>>> GetDoctorLanguageById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDoctorLanguageById(Id);
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
        [HttpGet, Route("GetLanguage_DD")]
        public async Task<ActionResult<IEnumerable<Language_DD>>> GetLanguage_DD()
        {
            try
            {
                var result = await this._repository.GetLanguage_DD();
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

    }
}
