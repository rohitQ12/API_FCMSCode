using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Doctor_ScheduleController : ControllerBase
    {
        public readonly IDoctor_ScheduleInterface _repository;

        public Doctor_ScheduleController()
        {
            this._repository = new Doctor_ScheduleRepository();
        }


        /*Get*/
        [HttpGet, Route("GetDoctor_Schedule")]
        public async Task<ActionResult<IEnumerable<Doctor_ScheduleModule>>> GetAll()
        {
            try
            {
                var result = await this._repository.GetDoctor_Schedule();
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
        [HttpGet, Route("GetDoctor_ScheduleById")]
        public async Task<ActionResult<IEnumerable<Doctor_ScheduleModule>>> Select(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();
                }
                var result = await this._repository.GetDoctor_ScheduleById(Id);
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

        //insert Schedule
        [HttpPost, Route("InsertDoctor_Schedule")]
        public async Task<ActionResult<Doctor_ScheduleModule>> Post([FromBody] Doctor_ScheduleModule Sc)
        {
            if (Sc == null)
            {

                return BadRequest();
            }
            var change = await _repository.Insert_DoctorSchedule(Sc);

            if (change != null)
                return Ok(change);
            else
                return BadRequest("Not successfull");
        }

        /*update */
        [HttpPut, Route("UpdateDoctor_Schedule")]
        public async Task<ActionResult<Doctor_ScheduleModule>> Put([FromBody] Doctor_ScheduleModule Su)
        {

            if (Su == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDoctor_Schedule(Su);

            if (change != null)
                return Ok(change);
            else
                return BadRequest("Not successfull");

        }

        /*Delete schedule by id*/

        [HttpDelete, Route("DeleteDoctor_Schedule")]
        public async Task<ActionResult> Delete(int Id)
        {
            if (Id <= 0)

            {
                return BadRequest();
            }
            var change = await _repository.DeleteDoctor_Schedule(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
    }
}
