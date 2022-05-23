using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Occupation_MSTController : ControllerBase
    {
        public readonly IOccupation_MSTRepository _repository;
        public Occupation_MSTController()
        {
            this._repository = new Occupation_MSTRepository();
        }
        [HttpGet, Route("GetAllOccupation")]
        public async Task<ActionResult<IEnumerable<Occupation_MST>>> Get()
        {
            try
            {
                var result = await this._repository.GetAllOccupation();
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
