using GlobalApi.IRepository.MasterIReopsitory;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class feedbackController : Controller
    {
        public readonly Ifeedback _repository;
        public feedbackController(Ifeedback repository)
        {
            _repository = repository;
        }
        [HttpGet,Route("GetFeedback")]
        public async Task<IActionResult> GetFeedback()
        {
            var result = await this._repository.GetFeedback();
            return Ok(result);
        }
    }
}
