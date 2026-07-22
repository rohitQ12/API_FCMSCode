using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.AdminRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using GlobalApi.Repository.MasterRepository;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowedMenusController : ControllerBase
    {
        public readonly IAllowedMenusRepository repository;
        private FindUserId obj_FindUserId = null;
        private string userName = "";
        public AllowedMenusController(FindUserId obj_FindUserId)
        {
            this.repository = new AllowedMenusRepository();
            this.obj_FindUserId = obj_FindUserId ?? throw new ArgumentNullException(nameof(obj_FindUserId));
        }

        [HttpGet, Route("GetAllowedMenus")]
        public async Task<IActionResult> GetAllowedMenus()
        {
            try
            {
                //userName = Convert.ToString(User.Identity.Name);
                 userName = "7775939380";
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
        public async Task<IActionResult> GetAllowedFunction(int submenuid)
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
        [HttpGet, Route("test1")]
        public ActionResult get()
        {
            //using (var ms= new MemoryStream(data))
            //{
            //    using(var fs=new FileStream("F:\GlobalApi\GlobalApi\GlobalApi\wwwroot\Images\", FileMode.Create))
            //    {
            //        ms.WriteTo(fs);
            //        return Ok();
            //    }
            //}
            var user = System.IO.File.ReadAllBytes(("wwwroot/Images/" + "default_user.png"));
            return Ok(user);
        }

        [HttpPost, Route("test")]
        public ActionResult get([FromBody] Test test)
        {
            //using (var ms= new MemoryStream(data))
            //{
            //    using(var fs=new FileStream("F:\GlobalApi\GlobalApi\GlobalApi\wwwroot\Images\", FileMode.Create))
            //    {
            //        ms.WriteTo(fs);
            //        return Ok();
            //    }
            //}
            byte[] bytes = Encoding.ASCII.GetBytes(test.image);
            string _filepath = Path.GetFullPath("F:/GlobalApi/GlobalApi/GlobalApi/wwwroot/Images/08132e2d-8c2f-4417-b6eb-9488ccf0c88a_OIP.jpg");
            System.IO.File.WriteAllBytes(_filepath, bytes);
            string dir = "wwwroot/Images/";
            if (!Directory.Exists(_filepath))
            {
                //Your Code
                System.IO.File.WriteAllBytes(_filepath, bytes);
                return Ok();
            }
            return BadRequest();

        }
    }
    public class Test
    {
        public string image { get; set; }
    }

    public class Questions
    {
        public string image { get; set; }
    }
}
