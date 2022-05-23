using GlobalApi.IRepository.MasterIRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Identity_DOC_MSTController : ControllerBase
    {
        public readonly IIdentity_DOC_MSTRepository _repository;
        public Identity_DOC_MSTController()
        {
            this._repository = new Identity_DOC_MSTRepository();
        }
        [HttpGet, Route("GetAllIdentity")]
        public async Task<ActionResult<IEnumerable<Identity_DOC_MST>>> Get()
        {
            try
            {
                var result = await this._repository.GetAllIdentity();
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
