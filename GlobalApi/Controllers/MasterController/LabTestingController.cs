using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestingController : ControllerBase
    {
        public readonly ILabTesting _repository;
        public LabTestingController()
        {
            this._repository = new LabTestingRepository();
        }

        [HttpPost, Route("InsertLabTesting")]
        public async Task<ActionResult<LabTesting>> Post([FromBody] LabTesting_Details lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertLabTesting(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPost, Route("AcceptLabTesting")]
        public async Task<ActionResult> Post(int Id, int Tst_CON_Id_FK, bool AcceptLabTesting)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            if (await _repository.AcceptLabTesting(Id, Tst_CON_Id_FK, AcceptLabTesting))
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateLabTesting")]
        public async Task<ActionResult<LabTesting>> Put([FromBody] LabTesting lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateLabTesting(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllLabTesting")]
        public async Task<ActionResult<IEnumerable<GetLabTestings>>> GetAllLabTesting()
        {
            try
            {
                var result = await this._repository.GetAllLabTesting();
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
        [HttpDelete, Route("DeleteLabTesting")]
        public async Task<ActionResult> DeleteLabTesting(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteLabTesting(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetLabTestingById")]
        public async Task<ActionResult<IEnumerable<LabTestingsById>>> GetLabTestingById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetLabTestingById(Id);
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
