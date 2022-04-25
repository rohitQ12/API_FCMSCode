using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.Data;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;

namespace GlobalApi.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
                this._repository = repository;
        }
        [AllowAnonymous]
        [HttpGet, Route("GetAllUser")]
        public async Task<ActionResult<IEnumerable<AuthUser_Details>>> GetAllUser()
        {
            try
            {
                var result = await this._repository.GetUser();
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
        [AllowAnonymous]
        [HttpGet, Route("GetProfile_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Images/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }
        //[Authorize]
        [HttpGet, Route("GetUserByname")]
        public async Task<ActionResult<IEnumerable<AuthUser_Details>>> GetUserByname()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var result = await this._repository.GetUserByname(userName);
                if (result!=null)
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
        [HttpPut, Route("UpdateUserProfile")]
        public async Task<ActionResult<AuthUser_Details>> UpdateUserProfile([FromForm] AuthUser_Details userProfile)
        {
            if (userProfile == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateUserProfile(userProfile);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

    }
}
