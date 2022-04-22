using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Doctor_Schedule_historyController : ControllerBase
    {
        public readonly IDoctor_Schedulehistory _repository;

        public Doctor_Schedule_historyController()
        {
            this._repository = new Doctor_SchedulehistoryRepository();
        }


        /*Get*/
        [HttpGet, Route("GetDoctor_Schedulehistory")]
        public async Task<ActionResult<IEnumerable<Schedule_historyModel>>> GetAll()
        {
            try
            {
                var result = await this._repository.GetDoctor_Schedulehistory();
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

        /*Get scd by id*/
        [HttpGet, Route("GetDoctor_SchedulehistoryById")]
        public async Task<ActionResult<IEnumerable<Schedule_historyModel>>> Select(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();
                }
                var result = await this._repository.GetDoctor_Schedulehistory(Id);
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
