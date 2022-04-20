using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpTypeController : ControllerBase
    {
        public readonly IEmpType _repository;
        public EmpTypeController()
        {
            this._repository = new EmpTypeRepository();
        }

        [HttpPost, Route("InsertEmpType")]
        public async Task<ActionResult<Emp_Type>> Post([FromBody] Emp_Type lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertEmpType(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateEmpType")]
        public async Task<ActionResult<Emp_Type>> Put([FromBody] Emp_Type lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateEmpType(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetAllEmpType")]
        public async Task<ActionResult<IEnumerable<Emp_Type>>> GetAllEmpType()
        {
            try
            {
                var result = await this._repository.GetAllEmpType();
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
        [HttpGet, Route("GetEmpType_DD")]
        public async Task<ActionResult<IEnumerable<Emp_Type_DD>>> GetEmpType_DD()
        {
            try
            {
                var result = await this._repository.GetEmpType_DD();
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
        [HttpDelete, Route("DeleteEmpType")]
        public async Task<ActionResult> DeleteEmpType(int emptype_id)
        {
            if (emptype_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteEmpType(emptype_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetEmpTypeById")]
        public async Task<ActionResult<IEnumerable<Emp_TypeById>>> GetEmpTypeById(int emptype_id)
        {
            if (emptype_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetEmpTypeById(emptype_id);
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
