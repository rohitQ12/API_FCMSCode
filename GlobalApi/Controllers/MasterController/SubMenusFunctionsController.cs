using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.Repository.AdminRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubMenusFunctionsController : ControllerBase
    {
        public readonly ISubMenusFunctionsRepository _repository;
        public SubMenusFunctionsController()
        {
            this._repository = new SubMenuFunctionsRepository();
        }

        [HttpPost, Route("InsertAppSubMenuFunctions")]
        public async Task<ActionResult<SubMenusFunctions>> Post([FromBody] SubMenusFunctions lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertAppSubMenuFunctions(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateAppSubMenuFunctions")]
        public async Task<ActionResult<SubMenusFunctions>> Put([FromBody] SubMenusFunctions lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateAppSubMenuFunctions(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllAppSubMenuFunctions")]
        public async Task<ActionResult<IEnumerable<SubMenusFunctions>>> GetAllAppSubMenuFunctions()
        {
            try
            {
                var result = await this._repository.GetAllAppSubMenuFunctions();
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

        [HttpPut, Route("DeleteAppSubMenuFunctions")]
        public async Task<ActionResult> DeleteAppSubMenuFunctions(int SMF_Id)
        {

            var change = await _repository.DeleteAppSubMenuFunctions(SMF_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAppSubMenuFunctionsById")]
        public async Task<ActionResult<IEnumerable<SubMenu>>> GetAppSubMenuFunctionsById(int SM_Id)
        {
            try
            {
                var result = await this._repository.GetAppSubMenuFunctionsById(SM_Id);
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

        [HttpGet, Route("GetAppSubMenuFunctions")]
        public async Task<ActionResult<IEnumerable<SubMenu>>> GetAppSubMenuFunctions()
        {
            try
            {
                var result = await this._repository.GetAppSubMenuFunctions();
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
