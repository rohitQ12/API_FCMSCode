using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using GlobalApi.GlobalClasses;
using GlobalApi.Models.Authentication;
using static GlobalApi.Models.Master.GetAllCourse_Section;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseChaptersController : ControllerBase
    
    {

        public readonly CourseChaptersIRepository _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public CourseChaptersController()
        {
            this.findUserId = new FindUserId();
            this._repository = new CourseChaptersRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }





        //[HttpPost, Route("InsertCourseChapters")]
        //public async Task<IActionResult> InsertCourseChapters([FromBody] InsertCourse_Chapters insertCourse_Chapters)
        //{
           

        //        var change = await _repository.InsertCourse_Chapters(insertCourse_Chapters);

        //        if (change == "Chapter name Added Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
           
        //}



        //[HttpPut, Route("ApproveCourse_ChaptersById")]
        //public async Task<IActionResult> ApproveCourse_ChaptersById(ApproveCourse_Chapters approveCourse_Chapters)
        //{
           
        //        var change = await _repository.ApproveCourse_ChaptersById(approveCourse_Chapters);

        //        if (change == "Chapter Approved Successfully")
        //        {
        //            return Ok();
        //        }
                
        //    return BadRequest(change);
        //}


        //[HttpPut, Route("UpdateCourse_Chapters")]
        //public async Task<IActionResult> UpdateCourse_Chapters([FromBody] UpdateCourse_Chapters UpdateCourse_Chapters)
        //{
            
        //        var change = await _repository.UpdateCourse_Chapters(UpdateCourse_Chapters);

        //        if (change == "Chapter Updated Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);

        //}


        //[HttpDelete, Route("DeleteCourse_ChaptersById")]
        //public async Task<IActionResult> DeleteCourse_ChaptersById(int ch_id)
        //{
           
        //        var change = await _repository.DeleteCourse_ChaptersById(ch_id);

        //        if (change == "Chapter Deleted Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
           
        //}

        //[HttpGet, Route("GetCourse_Chapters_DD")]
        //public async Task<IActionResult> GetSpecilistDoctor_DD()
        //{
        //    var result = await this._repository.GetCourse_Chapters_DD();
        //    return Ok(result);
        //}



        //[HttpGet, Route("GetCourseChaptersByName")]
        //public async Task<IActionResult> GetCourseChaptersByName(string ch_name)
        //{
        //    var result = await this._repository.GetCourseChaptersByName(ch_name);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("Chapter not found");
        //}





        //[HttpGet, Route("GetCourseChaptersById")]
        //public async Task<IActionResult> GetCourseChaptersById(int ch_id)
        //{
        //    var result = await this._repository.GetCourseChaptersById(ch_id);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("Chapter not found");
        //}


        //[HttpGet, Route("GetAllCourse_Chapters")]
        //public async Task<IActionResult> GetAllCourse_Chapters()
        //{
        //    var result = await this._repository.GetAllCourse_Chapters();
        //    return Ok(result);

        //}

    }
}
