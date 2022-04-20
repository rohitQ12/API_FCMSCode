using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpCategoryController : ControllerBase
    {
        public readonly IEmpCategory _repository;
        public EmpCategoryController(IEmpCategory repository)
        {
            this._repository = new EmpCategoryRepository();
        }

        [HttpPost, Route("InsertEmpCategory")]
        public async Task<ActionResult<Emp_Category>> Post([FromBody] Emp_Category lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertEmpCategory(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateEmpCategory")]
        public async Task<ActionResult<Emp_Category>> Put([FromBody] Emp_Category lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateEmpCategory(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAllEmpCategory")]
        public async Task<ActionResult<IEnumerable<Emp_Category>>> GetAllEmpCategory()
        {
            try
            {
                var result = await this._repository.GetAllEmpCategory();
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
        
        [HttpGet, Route("GetEmpCategory_DD")]
        public async Task<ActionResult<IEnumerable<Emp_Category_DD>>> GetEmpCategory_DD()
        {
            try
            {
                var result = await this._repository.GetEmpCategory_DD();
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
        
        [HttpDelete, Route("DeleteEmpCategory")]
        public async Task<ActionResult> DeleteEmpCategory(int emp_cat_id)
        {
            if (emp_cat_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteEmpCategory(emp_cat_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetEmpCategoryById")]
        public async Task<ActionResult<IEnumerable<Emp_CategoryById>>> GetEmpCategoryById(int emp_cat_id)
        {
            if (emp_cat_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetEmpCategoryById(emp_cat_id);
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
