using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
using GlobalApi.Repository.AuthRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubMenuController : ControllerBase
    {
        public readonly ISubMenu _repository;
        public SubMenuController()
        {
            this._repository = new SubMenuRepository();
        }

        [HttpPost, Route("InsertAppSubMenu")]
        public async Task<ActionResult<SubMenu>> Post([FromBody] SubMenu lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertAppSubMenu(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateAppSubMenu")]
        public async Task<ActionResult<SubMenu>> Put([FromBody] SubMenu lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateAppSubMenu(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllAppSubMenu")]
        public async Task<ActionResult<IEnumerable<SubMenu>>> GetAllAppSubMenu()
        {
            try
            {
                var result = await this._repository.GetAllAppSubMenu();
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

        [HttpPut, Route("DeleteAppSubMenu")]
        public async Task<ActionResult> DeleteAppSubMenu(int SM_Id)
        {

            var change = await _repository.DeleteAppSubMenu(SM_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAppSubMenuById")]
        public async Task<ActionResult<IEnumerable<SubMenu>>> GetAppSubMenuById(int SM_Id)
        {
            try
            {
                var result = await this._repository.GetAppSubMenuById(SM_Id);
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

        [HttpGet, Route("GetAppSubMenu")]
        public async Task<ActionResult<IEnumerable<SubMenu>>> GetAppSubMenu()
        {
            try
            {
                var result = await this._repository.GetAppSubMenu();
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
