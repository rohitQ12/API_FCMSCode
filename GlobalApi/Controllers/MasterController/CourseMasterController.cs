using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using GlobalApi.GlobalClasses;
using GlobalApi.Models.Authentication;
using GlobalApi.IRepository.MasterIReopsitory;
using GlobalApi.Repository.MasterReopsitory;
using static GlobalApi.Models.Master.GetAllCourse_Section;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseMasterController : ControllerBase
    
    {

        public readonly Course_MasterIRepository _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public CourseMasterController()
        {
            this.findUserId = new FindUserId();
            this._repository = new CourseMasterRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }





        //[HttpPost, Route("InsertCourseMaster")]
        //public async Task<IActionResult> InsertCourseMaster([FromBody] InsertCourse_Master insertCourse_Master)
        //{
           
        //        var change = await _repository.InsertCourse_Master(insertCourse_Master);

        //        if (change == "Course name Added Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
           
        //}



        //[HttpPut, Route("ApproveCourse_MasterById")]
        //public async Task<IActionResult> ApproveCourse_MasterById(ApproveCourse_Master approveCourse_Master)
        //{
           
        //        var change = await _repository.ApproveCourse_MasterById(approveCourse_Master);

        //        if (change == "Course Approved Successfully")
        //        {
        //            return Ok();
        //        }
                
        //    return BadRequest(change);
        //}


        //[HttpPut, Route("UpdateCourse_Master")]
        //public async Task<IActionResult> UpdateCourse_Master([FromBody] UpdateCourse_Master updateCourse_Master)
        //{
            
        //        var change = await _repository.UpdateCourse_Master(updateCourse_Master);

        //        if (change == "Course Updated Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);

        //}

        //[HttpDelete, Route("DeleteCourse_MasterById")]
        //public async Task<IActionResult> DeleteCourse_MasterById(int cu_Id)
        //{
           
        //        var change = await _repository.DeleteCourse_MasterById(cu_Id);

        //        if (change == "Course Deleted Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
           
        //}

        //[HttpGet, Route("GetCourse_Master_DD")]
        //public async Task<IActionResult> GetCourse_Master_DD()
        //{
        //    var result = await this._repository.GetCourse_Master_DD();
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("not found");
        //}

        //[HttpGet, Route("GetCourseMasterById")]
        //public async Task<IActionResult> GetCourseMasterById(string cu_name)
        //{
        //    var result = await this._repository.GetCourse_MasterById(cu_name);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("not found"); 
        //}


        //[HttpGet, Route("GetAllCourse_Master")]
        //public async Task<IActionResult> GetAllCourse_Master()
        //{
        //    var result = await this._repository.GetAllCourse_Master();
        //    return Ok(result);

        //}


        //[HttpGet, Route("GetCoursesDD")]
        //public async Task<ActionResult<IEnumerable<GetCourDD>>> GetCoursesDD()
        //{
        //    try
        //     {
        //        var result = await this._repository.GetCoursesDD();
                
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }
        //        return NotFound("not found");

               

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpGet, Route("GetSecDD")]
        //public async Task<IActionResult> GetSecDD()
        //{
        //    var result = await _repository.GetSecDD();
        //    return Ok(result);
        //}

        //[HttpGet ,Route("GetSecById")]
        //public async Task<IActionResult> GetSecBId(int sec_Id)
        //{
        //    var result = await _repository.GetSecById(sec_Id);
        //    return Ok(result);
        //}


        //[HttpGet, Route("GetAllPackDD")]
        //public async Task<ActionResult<IEnumerable<GetCourDD>>> GetAllPackDD()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetAllPackDD();

        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }
        //        return NotFound("not found");



        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

    }
}
