using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        public readonly INotificationRepository _repository;
        private FindUserId obj_FindUserId = null!;
        private string userName = "";
        public NotificationController(INotificationRepository repository,FindUserId obj_FindUserId)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.obj_FindUserId = obj_FindUserId ?? throw new ArgumentNullException(nameof(obj_FindUserId));
        }

        [HttpPost, Route("InsertNotification")]
        public async Task<ActionResult<Notification>> Post([FromBody] Notification notification)
        {
            if (notification == null)
            {
                return BadRequest();
            }
            userName = User.Identity.Name.ToString();
            string userID = await obj_FindUserId.FindUserIdFromUserName(userName);
            var change = await _repository.InsertNotification(notification, userID);

            if (change != null)
                return Ok(change);
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateNotification")]
        public async Task<ActionResult<Notification>> Put([FromBody] Notification notification)
        {
            if (notification == null)
            {
                return BadRequest();
            }
            userName = User.Identity.Name.ToString();
            string userID = await obj_FindUserId.FindUserIdFromUserName(userName);
            var change = await _repository.UpdateNotification(userID,notification.EventId);

            if (change != null)
                return Ok(change);
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetNotificationByUserId")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetByID()
        {
            try
            {
                userName = User.Identity.Name.ToString();
                string userID = await obj_FindUserId.FindUserIdFromUserName(userName);
                var result = await this._repository.GetNotificationByUserId(userID);
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
        [HttpPut, Route("DeleteNotification")]
        public async Task<ActionResult> Delete(int EventId)
        {

            var change = await _repository.DeleteNotification(EventId);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpGet, Route("GetNotification")]
        public async Task<ActionResult<IEnumerable<Notification>>> Get()
        {
            try
            {
                var result = await this._repository.GetNotification();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet, Route("GetNotificationcount")]
        public async Task<ActionResult<IEnumerable<Notification>>> Getcount()
        {
            try
            {
                userName = User.Identity.Name.ToString();
                string userID = await obj_FindUserId.FindUserIdFromUserName(userName);
                var result = await this._repository.GetNotificationcount(userID);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
