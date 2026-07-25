using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public readonly IDepartment _repository;
        public DepartmentController()
        {
            this._repository = new DepartmentRepository();
        }

        //[HttpPost, Route("InsertDepartment")]
        //public async Task<ActionResult<Department>> Post([FromBody] Department lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertDepartment(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}
        
        
        //[HttpPut, Route("UpdateDepartment")]
        //public async Task<ActionResult<Department>> Put([FromBody] Department lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }

        //    var change = await _repository.UpdateDepartment(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}
        
        
        //[HttpGet, Route("GetAllDepartment")]
        //public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartment()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetAllDepartment();
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        
        
        //[HttpGet, Route("GetDepartment_DD")]
        //public async Task<ActionResult<IEnumerable<Department_DD>>> GetDepartment_DD()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetDepartment_DD();
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        
        
        //[HttpDelete, Route("DeleteDepartment")]
        //public async Task<ActionResult> DeleteDepartment(int Dept_Id)
        //{
        //    if (Dept_Id <= 0)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.DeleteDepartment(Dept_Id);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}
        
        
        //[HttpGet, Route("GetDepartmentById")]
        //public async Task<ActionResult<IEnumerable<DepartmentById>>> GetDepartmentById(int Dept_Id)
        //{
        //    if (Dept_Id == null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var result = await this._repository.GetDepartmentById(Dept_Id);
        //        if (result == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(result);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
    }
}
