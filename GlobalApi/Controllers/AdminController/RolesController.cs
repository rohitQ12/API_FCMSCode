using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.Repository.AdminRepository;
using GlobalApi.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GlobalApi.Models.AdminClaims;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        public readonly RolesRepository _repository;
        public readonly FindUserId findUserId;
        public RolesController(RolesRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.findUserId = new FindUserId();
        }
        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create([FromBody] RolesModels role)
        {
            if (ModelState.IsValid)
            {

                var result = await this._repository.CreateRoles(role);

                if (result)
                    return Ok(result); // Status Code: 200 

                return BadRequest("The Role you have entered already exists");
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }
        [HttpGet, Route("GetAllRoles")]
        public async Task<ActionResult<IEnumerable<AspNetRole>>> GetAllRoles()
        {
            try
            {
                var result = await this._repository.GetAllRoles();
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
       
        //[HttpPut, Route("UpdateRoles")]
        //public async Task<IActionResult> UpdateRoles([FromBody] RolesModels role)
        //{
        //    if (role != null)
        //    {
        //        var result = await this._repository.UpdateOfficeRole(role);

        //        if (result)
        //        {
        //            return Ok(); // Status Code: 200
        //        }
        //        return BadRequest("The Role you have entered already exists");
        //    }
        //    return BadRequest("Some properties are not valid"); // Status code: 400
        //}

        [HttpPut, Route("UpdateRoles")]
        public async Task<IActionResult> UpdateRoles([FromBody] RolesModels role)
        {
            if (role == null)
                return BadRequest("Invalid role data");

            var result = await _repository.UpdateOfficeRole(role);

            if (result == "Role updated successfully")
                return Ok(result);

            return BadRequest(result);
        }


        [HttpGet, Route("GetRoleId")]
        public async Task<ActionResult<IEnumerable<AspNetRole>>> GetRoleId(string Id)
        {
            try
            {
                var result = await this._repository.GetRoleId(Id);
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
      
        [HttpGet, Route("ActivateInactivate")]
        public async Task<IActionResult> ActivateInactivate(string roleid)
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
            if (roleid != null)
            {
                var result = await this._repository.ActivateInactivate(roleid);
                
                return Ok(result); // Status Code: 200 
            }
            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [HttpGet, Route("GetAllRoles_DD")]
        public async Task<ActionResult<IEnumerable<AspNetRole>>> GetAllRoles_DD()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var rolename = await this.findUserId.FindRoleNameFromUserName(userName);
                var result = await this._repository.GetAllRoles_DD(roleaction, rolename);
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

        [HttpDelete, Route("GetRolesforSelectedOffice")]
        public async Task<ActionResult<IEnumerable<AspNetRole>>> GetRolesforSelectedOffice()
        {
            try
            {
                var result = await this._repository.GetRolesforSelectedOffice();
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


    }
}
