using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralsController : ControllerBase
    {
        public readonly IReferrals _repository;
        public readonly FindUserId findUserId;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ReferralsController()
        {
            this._repository = new ReferralsRepository();
            this.findUserId = new FindUserId();

        }

        [HttpPost, Route("InsertReferrals")]
        public async Task<ActionResult<Referrals>> Post([FromBody] Referrals lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertReferrals(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllReferrals")]
        public async Task<ActionResult<IEnumerable<GetReferrals>>> GetAllReferrals()
        {
            try
            {
                var result = await this._repository.GetAllReferrals();
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

        [HttpDelete, Route("DeleteReferrals")]
        public async Task<ActionResult> DeleteReferrals(int RV_Id)
        {
            if (RV_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteReferrals(RV_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }


        [HttpGet, Route("GetReferralsByCON_Id")]
        public async Task<ActionResult<IEnumerable<GetReferrals>>> GetReferralsByCON_Id(int CON_Id)
        {
            if (CON_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetReferralsByCON_Id(CON_Id);
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

        [HttpGet, Route("GetReferralsById")]
        public async Task<ActionResult<IEnumerable<GetReferrals>>> GetReferralsById(int Ref_Id)
        {
            if (Ref_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetReferralsById(Ref_Id);
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

        [HttpPut, Route("ApproveReferrals")]
        public async Task<ActionResult> ApproveReferrals([FromBody] ApprvReferrals lead)
        {
            //var userName = User.Identity.Name.ToString();
            //var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
            //var AssistantId = await this.findUserId.FindAssistantIdFromHospitalOfficeUsername(userName);
            if (lead.Ref_Id <= 0)
            {
                return BadRequest();
            }
            //var change = await _repository.ApproveReferrals(AssistantId, roleaction,lead);
            var change = await _repository.ApproveReferrals(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

    }
}
