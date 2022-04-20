using GlobalApi.Models.Authentication;
using GlobalApi.Repository.AuthRepository;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        public readonly IMenu _repository;
        public MenusController()
        {
            this._repository = new MenuRepository();
        }

        [HttpPost, Route("InsertAppMenu")]
        public async Task<ActionResult<Menus>> Post([FromBody] Menus lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertAppMenu(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateAppMenu")]
        public async Task<ActionResult<Menus>> Put([FromBody] Menus lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateAppMenu(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("DeleteAppMenu")]
        public async Task<ActionResult> DeleteAppMenu(int app_menu_id)
        {
            if (app_menu_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteAppMenu(app_menu_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAppMenuById")]
        public async Task<ActionResult<IEnumerable<Menus>>> GetAppMenuById(int app_menu_id)
        {
            if (app_menu_id !=0)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetAppMenuById(app_menu_id);
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
        
        [HttpGet, Route("GetAppMenu")]
        public async Task<ActionResult<IEnumerable<Menus>>> GetAppMenu()
        {
            try
            {
                var result = await this._repository.GetAppMenu();
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
