using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.AdminRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AllowedMenusController : ControllerBase
    {
        public readonly IAllowedMenusRepository repository;
        private FindUserId obj_FindUserId = null;
        private string userName = "";
        public AllowedMenusController(IAllowedMenusRepository repository, FindUserId obj_FindUserId)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.obj_FindUserId = obj_FindUserId ?? throw new ArgumentNullException(nameof(obj_FindUserId));
        }
        
        [HttpGet, Route("GetAllowedMenus")]
        public async Task<ActionResult<IEnumerable<Menus_List>>> GetAllowedMenus()
        {
            try
            {
                userName = User.Identity.Name.ToString();
                string userID = await obj_FindUserId.FindUserIdFromUserName(userName);
                string roleId = await obj_FindUserId.FindRole_Id_FKFromUserName(userName);
                bool roleinactive = await obj_FindUserId.CheckRoles(roleId);
                if (roleinactive)
                {
                    var result = await this.repository.Get(roleId);
                    if (result.Any())
                    {
                        return Ok(result);
                    }
                    else
                        return NotFound("Data not found in DB");
                }
                else
                    return NotFound("Role inacive");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet, Route("GetAllowedFunction")]
        public async Task<ActionResult<IEnumerable<Menus_List>>> GetAllowedFunction(int submenuid)
        {
            try
            {
                userName = User.Identity.Name.ToString();
                string userID = await obj_FindUserId.FindUserIdFromUserName(userName);
                string roleId = await obj_FindUserId.FindRole_Id_FKFromUserName(userName);
                bool roleinactive = await obj_FindUserId.CheckRoles(roleId);
                if (roleinactive)
                {
                    var result = await this.repository.GetClims(submenuid, roleId);
                    if (result.Any())
                    {
                        return Ok(result);
                    }
                    else
                        return NotFound("Data not found in DB");
                }
                else
                    return NotFound("Role inacive");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
