using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.StatesAndCitiesIRepository;
using GlobalApi.Models.ComplaintModels;
using GlobalApi.Repository.StatesAndCitiesRepository;

namespace GlobalApi.Controllers.StatesAndCititesController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesAndCitiesController : ControllerBase
    {
        StatesAndCitiesRepository obj = new StatesAndCitiesRepository();


        [HttpGet]
        [Route("GetAllStates")]
        public async Task<IActionResult> GetAllStates()
        {
            return Ok(await obj.GetAllStates());
        }

        [HttpGet]
        [Route("GetCitiesbystate_id/{state_id}")]
        public async Task<IActionResult> GetCitiesbByState_id(int state_id)
        {
            return Ok(await obj.GetCitiesbByState_id(state_id));
        }


    }
}