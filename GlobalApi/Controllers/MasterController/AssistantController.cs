using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssistantController : ControllerBase
    {
        public readonly IAssistant _repository;
        public readonly FindUserId findUserId;
        public AssistantController()
        {
            this._repository = new AssistantRepository();
            this.findUserId = new FindUserId();
        }

        [HttpPost, Route("InsertAssistant")]
        public async Task<ActionResult<Assistant>> Post([FromForm] Assistant_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertAssistant(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpPut, Route("UpdateAssistant")]
        public async Task<ActionResult<Assistant>> Put([FromForm] Assistant_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateAssistant(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpGet, Route("GetAllAssistant")]
        public async Task<ActionResult<IEnumerable<Assistant>>> GetAllAssistant()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var Assi_Hos_Id_FK = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
                var result = await this._repository.GetAllAssistant(Assi_Hos_Id_FK, roleaction);
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
        
        
        [HttpGet, Route("GetAssistant_DD")]
        public async Task<ActionResult<IEnumerable<Assistant_DD>>> GetAssistant_DD()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var Assi_Hos_Id_FK = await this.findUserId.FindHospitalIdFromHospitalOfficeUsername(userName);
                var result = await this._repository.GetAssistant_DD(Assi_Hos_Id_FK, roleaction);
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
        
        
        [HttpDelete, Route("DeleteAssistant")]
        public async Task<ActionResult> DeleteAssistant(int Assistant_id)
        {
            if (Assistant_id == 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteAssistant(Assistant_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        
        [HttpGet, Route("GetAssistantById")]
        public async Task<ActionResult<IEnumerable<AssistantById>>> GetAssistantById(int Assistant_id)
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var result = await this._repository.GetAssistantById(Assistant_id, roleaction);
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
        
        
        [HttpGet, Route("GetAssistant_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Assistant/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }

        [HttpPut, Route("ApproveAssistant")]
        public async Task<ActionResult> ApproveAssistant(int Assi_Id, string? Remarks)
        {
            if (Assi_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.ApproveAssistant(Assi_Id, Remarks);

            if (change != null)
                return Ok(change);
            else
                return BadRequest("Not successfull");
        }

    }
}
