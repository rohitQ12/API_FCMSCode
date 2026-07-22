using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIReopsitory;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterReopsitory;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentQueController : ControllerBase
    {

        private readonly IStudentQuestion _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private readonly IEMailService _EMailService;
        private bool IfClaimExists = false;


        public StudentQueController(IEMailService EMailService, IStudentQuestion repository)
        {
            this._EMailService = EMailService;
            this._repository = repository;
            this.claimsAuthorization = new ClaimsAuthorization();


        }

        [HttpPost, Route("InsertStuQue")]
        public async Task<IActionResult> InsertStuQue([FromBody] StuqueInsert lead)
        {
            //var username = User.Identity.Name;
            var change = await _repository.InsertStuQue(lead);
            if (change == "Question Added Successfully")
            {
                return Ok();
            }
            return Ok(change);

        }
        [HttpPost, Route("SendStudentQA")]
        public async Task<IActionResult> SendStuQue([FromBody] StuQueries lead)
        {
            //var username = User.Identity.Name;
            var change = await _repository.SendStuQue(lead);
            if (change == "Mail sent successfully")
            {
                return Ok(change);
            }
            return BadRequest(change);

        }


        [HttpGet, Route("GetAllStuQue")]
        public async Task<IActionResult> GetAllStuQue()
        {
            var result = await this._repository.GetAll_StuQue();
            return Ok(result);
        }


        [HttpPut, Route("UpdateStuQue")]
        public async Task<IActionResult> UpdateStuQue([FromBody] StuqueUpdate lead)
        {
            //var username = User.Identity.Name;
            //var username = "8778650328";
            //var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            //IfClaimExists = claims.Any(x => x.ClaimType == "StudentsQuestionsEdit" && x.ClaimValue == "Y");
            //if (IfClaimExists)
            //{
            var change = await _repository.UpdateStuQue(lead);
            if (change == "Queries Updated Successfully")
            {
                return Ok();
            }
            return BadRequest(change);
            //}
            //return Unauthorized();
        }

        [HttpDelete, Route("DeleteStuQue")]
        public async Task<IActionResult> DeleteStuQue(int id)
        {
            // var username = User.Identity.Name;
            //  var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            // IfClaimExists = claims.Any(x => x.ClaimType == "CountryDelete" && x.ClaimValue == "Y");
            //// if (IfClaimExists)
            {
                var change = await _repository.DeleteStuQue(id);
                if (change == "Queries Deleted Successfully")
                {

                    return Ok();
                }
                return BadRequest(change);
                // }
                // return Unauthorized();

            }


        }
    }
}
