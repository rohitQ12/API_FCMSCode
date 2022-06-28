using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Language_MSTController : ControllerBase
    {
        public readonly ILanguage_MSTRepository _repository;
        public Language_MSTController()
        {
            this._repository = new Language_MSTRepository();
        }
        [HttpGet, Route("GetAllLanguage")]
        public async Task<ActionResult<IEnumerable<Language_MST>>> Get()
        {
            try
            {
                var result = await this._repository.GetAllLanguage();
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
