using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ICategory _repository;
        public CategoryController()
        {
            this._repository = new CategoryRepository();
        }

        [HttpPost, Route("InsertCategory")]
        public async Task<ActionResult<Category>> Post([FromBody] Category lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertCategory(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateCategory")]
        public async Task<ActionResult<Category>> Put([FromBody] Category lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateCategory(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllCategory")]
        public async Task<ActionResult<IEnumerable<GetAllCat>>> GetAllCategory()
        {
            try
            {
                var result = await this._repository.GetAllCategory();
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

        [HttpGet, Route("GetCategory_DD")]
        public async Task<ActionResult<IEnumerable<Cat_DD>>> GetCategory_DD()
        {
            try
            {
                var result = await this._repository.GetCategory_DD();
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

        [HttpDelete, Route("DeleteCategory")]
        public async Task<ActionResult> DeleteCategory(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteCategory(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        //[HttpGet, Route("GetCategoryById")]
        //public async Task<ActionResult<IEnumerable<CategoryBy_Id>>> GetCategoryById(int Id)
        //{
        //    if (Id == null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var result = await this._repository.GetCategoryById(Id);
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
