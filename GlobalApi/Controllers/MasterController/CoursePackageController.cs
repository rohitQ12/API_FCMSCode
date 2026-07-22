using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using GlobalApi.GlobalClasses;
using GlobalApi.Models.Authentication;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursePackageController: ControllerBase
    {
        public readonly CoursePackageIRepository _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public CoursePackageController()
        {
            this.findUserId = new FindUserId();
            this._repository = new CoursePackageRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }


        //[HttpPost, Route("InsertCourse_Package")]
        //public async Task<IActionResult> InsertCourse_Pack([FromBody] Course_Pack course_Pack)
        //{

        //    var change = await _repository.InsertCourse_Pack(course_Pack);

        //    if (change == "CoursePack name Added Successfully")
        //    {
        //        return Ok();
        //    }
        //    return BadRequest(change);

        //}

        //[HttpPut, Route("ApproveCourse_PackageById")]
        //public async Task<IActionResult> ApproveCourse_PackById(ApproveCourse_Pack approveCourse_Pack)
        //{

        //    var change = await _repository.ApproveCourse_PackById(approveCourse_Pack);

        //    if (change == "CoursePack Approved Successfully")
        //    {
        //        return Ok();
        //    }

        //    return BadRequest(change);
        //}


        //[HttpPut, Route("UpdateCourse_Package")]
        //public async Task<IActionResult> UpdateCourse_Pack([FromBody] Course_Pack updateCourse_Pack)
        //{

        //    var change = await _repository.UpdateCourse_Pack(updateCourse_Pack);

        //    if (change == "CoursePack Updated Successfully")
        //    {
        //        return Ok();
        //    }
        //    return BadRequest(change);

        //}

        //[HttpDelete, Route("DeleteCourse_PackageById")]
        //public async Task<IActionResult> DeleteCourse_PackById(int cp_id)
        //{

        //    var change = await _repository.DeleteCourse_PackById(cp_id);

        //    if (change == "CoursePack Deleted Successfully")
        //    {
        //        return Ok();
        //    }
        //    return BadRequest(change);

        //}


        //[HttpGet, Route("GetCourse_PackageById")]
        //public async Task<IActionResult> GetCourse_PackById(int cp_id)
        //{
        //    var result = await this._repository.GetCourse_PackById(cp_id);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("not found");
        //}


        //[HttpGet, Route("GetAllCourse_Package")]
        //public async Task<IActionResult> GetAllCourse_Pack()
        //{
        //    var result = await this._repository.GetAllCourse_Pack();
        //    return Ok(result);

        //}

        ////[HttpGet, Route("GetAllCoursePackage")]
        ////public async Task<IActionResult> GetAllCoursePackage()
        ////{
        ////    var result = await this._repository.GetAllCoursePackage();
        ////    return Ok(result);

        ////}

        //[HttpGet, Route("GetSectionDD")]
        //public async Task<IActionResult> GetSectionDD()
        //{
        //    var result = await this._repository.GetSectionDD();
        //    return Ok(result);

        //}
        //[HttpGet ,Route("GetPackageDD")]
        //public async Task<IActionResult> GetPackageDD()
        //{
        //    var result = await _repository.GetPackageDD();
        //    return Ok(result);
        //}

        //[HttpGet, Route("GetHide")]
        //public async Task<IActionResult> GetHide()
        //{
        //    var result = await _repository.GetHide();
        //    return Ok(result);
        //}



        //[HttpGet, Route("GetCardDD")]
        //public async Task<IActionResult> GetCardDD(int id)
        //{
        //    var result = await _repository.GetCardDD(id);
        //    return Ok(result);
        //}

    }
}
