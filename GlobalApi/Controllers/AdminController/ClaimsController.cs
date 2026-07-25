using GlobalApi.Models.Authentication;
using GlobalApi.Repository.AuthRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using GlobalApi.GlobalClasses;
using IdentityServer4.AccessTokenValidation;
//using System.Web.Http;

namespace GlobalApi.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClaimsController : ControllerBase
    {
        public readonly ClaimsHandle claimsHandle;   
        private FindUserId obj_FindUserId = null;
        public ClaimsController(ClaimsHandle claimsHandle)
        {
            this.claimsHandle = claimsHandle ?? throw new ArgumentNullException(nameof(claimsHandle));
            this.obj_FindUserId = new FindUserId();
        }

        [HttpGet, Route("{roleId}")]
        public async Task<IActionResult> GetAllClaimsForTheRole(string roleId)
        {            
            if (roleId != null)
            {
                List<Menus_List> claimsListOfTheRole = await this.claimsHandle.GetAllClaimsAllocatedToRole(roleId);
                var test = await this.claimsHandle.Gettest();
                return Ok(claimsListOfTheRole);
            }
            else
                return Unauthorized();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClaimsForTheRole()
        {
            var userName =Convert.ToString(User.Identity.Name);
            string userID = await obj_FindUserId.FindUserIdFromUserName(userName);
            string roleId = await obj_FindUserId.FindRole_Id_FKFromUserName(userName);           
            if (roleId != null)
            {
                List<Menus_List> claimsListOfTheRole = await this.claimsHandle.GetAllClaimsAllocatedToRole(roleId);
                var test = await this.claimsHandle.Gettest();
                return Ok(claimsListOfTheRole);
            }
            else
                return Unauthorized();
        }
        [HttpPost, Route("{roleId}/assignclaims")]
        public async Task<IActionResult> AssignClaimsToRoles(string roleId, [FromBody] List<Menus_List> ClaimsToAssign)
        {            

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (roleId == "" || roleId == null)
            {
                return BadRequest("Role not Found");
            }
            bool result = await this.claimsHandle.Create_RoleClaim(roleId, ClaimsToAssign);
            bool result1 = await this.claimsHandle.CreateClaimsForASP_NetUsersBasedOnRole(roleId, ClaimsToAssign);
                       
            if (result1)
                return Ok();
            else
                return BadRequest();            

        }
        
    }
}
