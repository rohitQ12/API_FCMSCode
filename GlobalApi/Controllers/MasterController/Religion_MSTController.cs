using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;


namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Religion_MSTController : ControllerBase
    {
        public readonly IReligion_MSTRepository _repository;
        public Religion_MSTController()
        {
            this._repository = new Religion_MSTRepository();
        }
        [HttpGet, Route("GetAllReligion")]
        public async Task<ActionResult<IEnumerable<Religion_MST>>> Get()
        {
            try
            {
                var result = await this._repository.GetAllReligion();
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
