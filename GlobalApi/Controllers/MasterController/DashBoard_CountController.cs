using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoard_CountController : ControllerBase
    {
        public readonly IDashboard_Count _repository;
        public readonly FindUserId findUserId;
        public DashBoard_CountController()
        {
            this._repository = new Dashboard_CountRepository();
            this.findUserId = new FindUserId();
        }        

        //[Route("GetDashboardData")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<ActionResult> getDashboardData()
        //{

        //    try
        //    {
        //        //var userName = Convert.ToString(User.Identity.Name);
        //        //if (userName == null)
        //        //{
        //        //    return Unauthorized();
        //        //}
        //         var userName = "8778650328";
        //        var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
        //        var rolename = await this.findUserId.FindRoleNameFromUserName(userName);

        //            var result = await this._repository.GetDashboardData();
        //            if (result.Any())
        //            {
        //                return Ok(result);
        //            }

        //        return NotFound();

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //}
       

    }
}
