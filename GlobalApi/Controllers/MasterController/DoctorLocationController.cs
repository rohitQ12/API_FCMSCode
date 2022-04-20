using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorLocationController : ControllerBase
    {
        public readonly IDoctorLocation _repository;
        public DoctorLocationController()
        {
            this._repository = new DoctorLocationRepository();
        }

        //[HttpPost, Route("InsertDoctorLocation")]
        //public async Task<ActionResult<DoctorLocation>> Post([FromBody] DoctorLocation lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertDoctorLocation(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}
        //[HttpPut, Route("UpdateDoctorLocation")]
        //public async Task<ActionResult<DoctorLocation>> Put([FromBody] DoctorLocation lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }

        //    var change = await _repository.UpdateDoctorLocation(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}
        [HttpGet, Route("GetAllDoctorLocation")]
        public async Task<ActionResult<IEnumerable<GetDoctorLoc>>> GetAllDoctorLocation()
        {
            try
            {
                var result = await this._repository.GetAllDoctorLocation();
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
        [HttpDelete, Route("DeleteDoctorLocation")]
        public async Task<ActionResult> DeleteDoctorLocation(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDoctorLocation(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetDoctorLocationById")]
        public async Task<ActionResult<IEnumerable<GetDoctorLoc>>> GetDoctorLocationById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDoctorLocationById(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
