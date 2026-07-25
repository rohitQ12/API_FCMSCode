using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using static GlobalApi.Models.Master.vedio_Documents;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadVideosController : ControllerBase
    {
        private readonly string connectingString;
        public readonly UploadVideosIRepository _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        public readonly IAuthenticationRepository authrepository;
        private bool IfClaimExists = false;
        public readonly IUserRepository userRepository;

        public UploadVideosController(IAuthenticationRepository authrepository, IUserRepository userRepository)
        {
            this._repository = new UploadVideosRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
            this.authrepository = authrepository;
            this.userRepository = userRepository;

        }


        [HttpPost, Route("InsertCourseVideos")]
        [RequestSizeLimit(91474836480)] // 90GB
        public async Task<ActionResult<vedio_Documents>> InsertCourseVideos([FromForm] vedio_Documents fileupload)
        {
            try
            {
                  var userName = Convert.ToString(User.Identity.Name);
                //var userName = "8073043647";
                if (userName == null)
                {
                    return Unauthorized();
                }
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                if (roleaction == null)
                {
                    return BadRequest("User not found");
                }

                if (fileupload == null || fileupload.vi_vedio == null)
                {
                    return BadRequest("File upload is null or invalid");
                }
                var change = await _repository.InsertCourseVideos(fileupload);
                if (change == "File uploaded successfully")
                {
                    return Ok(new CustomMessage_Consult { code = 200, message = change });
                }

                return BadRequest(new CustomMessage_Consult { code = 400, message = change });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet, Route("GetAllVideo_Documents")]
        public async Task<ActionResult<IEnumerable<GetAllVedio_Documents>>> GetAllVideo_Documents()
        {
            try
            {
                var result = await this._repository.GetAllVideo_Documents();
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


        [HttpGet, Route("GetAllVideo_DocumentsById")]
        public async Task<ActionResult> GetAllVideo_DocumentsById(int vi_id)
        {

            try
            {
                var result = await this._repository.GetAllVideo_DocumentsById(vi_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete, Route("DeleteVideo_DocumentsById")]
        public async Task<ActionResult> DeleteVideo_DocumentsById(int vi_id)
        {

            var change = await _repository.DeleteVideo_DocumentsById(vi_id);

            if (change == "Video Deleted Successfully")
            {
                return Ok();
            }
            return BadRequest("Not successfull");
        }


        [HttpPut, Route("UpdateVideo_DocumentsById")]
        public async Task<IActionResult> UpdateVideo_DocumentsById([FromForm] UpdateVedio_DocumentsById lead)
        {

            var change = await _repository.UpdateVideo_DocumentsById(lead);
            if (change == "Video file successfully Updated")
            {
                return Ok();
            }

            return BadRequest("Updated Not successfull");

        }

        [HttpPut, Route("ApproveVideoUpload")]
        public async Task<IActionResult> ApproveVideo(Approvevedio_Documents approvevedio_Documents)
        {

            var change = await _repository.ApproveVideo(approvevedio_Documents);

            if (change == "VideoUpload Approved Successfully")
            {
                return Ok();
            }

            return BadRequest(change);
        }


    }
}
