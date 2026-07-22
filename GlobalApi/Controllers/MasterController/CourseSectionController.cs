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
    public class CourseSectionController : ControllerBase
    
    {

        public readonly Course_SectionIRepository _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public CourseSectionController()
        {
            this.findUserId = new FindUserId();
            this._repository = new CourseSectionRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }





        //[HttpPost, Route("InsertCourseSection")]
        //public async Task<IActionResult> InsertCourseSection([FromBody] InsertCourse_Section insertCourse_Section)
        //{
           
        //        var change = await _repository.InsertCourse_Section(insertCourse_Section);

        //        if (change == "Section name Added Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
           
        //}



        //[HttpPut, Route("ApproveCourse_SectionById")]
        //public async Task<IActionResult> ApproveCourse_SectionById(ApproveCourse_Section approveCourse_Section)
        //{
           
        //        var change = await _repository.ApproveCourse_SectionById(approveCourse_Section);

        //        if (change == "Section Approved Successfully")
        //        {
        //            return Ok();
        //        }
                
        //    return BadRequest(change);
        //}


        //[HttpPut, Route("UpdateCourse_Section")]
        //public async Task<IActionResult> UpdateCourse_Section([FromBody] UpdateCourse_Section updateCourse_Section)
        //{
            
        //        var change = await _repository.UpdateCourse_Section(updateCourse_Section);

        //        if (change == "Section Updated Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);

        //}


        //[HttpDelete, Route("DeleteCourse_SectionById")]
        //public async Task<IActionResult> DeleteCourse_SectionById(int sc_Id)
        //{
           
        //        var change = await _repository.DeleteCourse_SectionById(sc_Id);

        //        if (change == "Section Deleted Successfully")
        //        {
        //            return Ok();
        //        }
        //        return BadRequest(change);
           
        //}

        //[HttpGet, Route("GetCourse_Section_DD")]
        //public async Task<IActionResult> GetCourse_Section_DD()
        //{
        //    var result = await this._repository.GetCourse_Section_DD();
        //    return Ok(result);
        //}


        //[HttpGet, Route("GetCourseSectionByName")]
        //public async Task<IActionResult> GetCourseSectionByName(string sc_name)
        //{
        //    var result = await this._repository.GetCourseSectionByName(sc_name);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("Section not found");
        //}


        //[HttpGet, Route("GetCourseSectionById")]
        //public async Task<IActionResult> GetCourseSectionById(int sc_Id)
        //{
        //    var result = await this._repository.GetCourseSectionById(sc_Id);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("Section not found");
        //}


        //[HttpGet, Route("GetAllCourse_Section")]
        //public async Task<IActionResult> GetAllCourse_Section()
        //{
        //    var result = await this._repository.GetAllCourse_Sections();
        //    return Ok(result);

        //}

        //[HttpGet, Route("GetCranialSectionDD")]
        //public async Task<IActionResult> GetCranialSectionDD()
        //{
        //    var result = await this._repository.GetCranialSectionDD();
        //    if (result.Any())
        //    {
        //        return Ok(result);
        //    }

        //    return NotFound("Data not found");
        //}

        //[HttpGet , Route("GetSpinalSectionDD")]
        //public async Task<IActionResult> GetSpinalSectionDD()
        //{
        //    var result = await _repository.GetSpinalSectionDD();
        //    if (result.Any())
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("Data not found");
        //}

    }
}
