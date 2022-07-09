using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Consult_LabTestController : ControllerBase
    {
        public readonly IConsult_LabTest _repository;
        public Consult_LabTestController()
        {
            this._repository = new Consult_LabTestRepository();
        }

        [HttpPost, Route("InsertConsult_LabTest")]
        public async Task<ActionResult<Consult_LabTest>> Post([FromBody] Consult_LabTest lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertConsult_LabTest(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateConsult_LabTest")]
        public async Task<ActionResult<Consult_LabTest>> Put([FromBody] Consult_LabTest lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateConsult_LabTest(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllConsult_LabTest")]
        public async Task<ActionResult<IEnumerable<GetConsult_LabTest>>> GetAllConsult_LabTest()
        {
            try
            {
                var result = await this._repository.GetAllConsult_LabTest();
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

        [HttpDelete, Route("DeleteConsult_LabTest")]
        public async Task<ActionResult> DeleteConsult_LabTest(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteConsult_LabTest(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetConsult_LabTestById")]
        public async Task<ActionResult<IEnumerable<GetConsult_LabTest>>> GetConsult_LabTestById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetConsult_LabTestById(Id);
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
        
        [HttpGet, Route("GetConsult_LabTestByCON_Id")]
        public async Task<ActionResult<IEnumerable<GetConsult_LabTest>>> GetConsult_LabTestByCON_Id(int CON_Id)
        {
            if (CON_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetConsult_LabTestByCON_Id(CON_Id);
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
