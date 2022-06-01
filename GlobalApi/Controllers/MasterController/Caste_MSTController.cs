using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Caste_MSTController : ControllerBase
    {
        public readonly ICaste_MSTRepository _repository;
        public Caste_MSTController()
        {
            this._repository = new Caste_MSTRepository();
        }
        [HttpGet, Route("GetAllCaste")]
        public async Task<ActionResult<IEnumerable<Caste_MST>>> Get()
        {
            try
            {
                var result = await this._repository.GetAllCaste();
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

        [HttpGet, Route("GetCaste_DD")]
        public async Task<IActionResult> GetCaste_DD(int Religion_id)
        {
            try
            {
                var result = await this._repository.GetCaste_DD(Religion_id);
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
