using GlobalApi.IRepository.MasterIRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Insurer_MSTController : ControllerBase
    {
        public readonly IInsurer_MSTRepository _repository;
        public Insurer_MSTController()
        {
            this._repository = new Insurer_MSTRepository();
        }
        [HttpGet, Route("GetAllInsurer")]
        public async Task<ActionResult<IEnumerable<Insurer_MST>>> Get()
        {
            try
            {
                var result = await this._repository.GetAllInsurer();
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
