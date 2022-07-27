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
    public class ManualAppointmentController : ControllerBase
    {
        public readonly IManualAppointment _repository;
        public readonly FindUserId findUserId;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ManualAppointmentController()
        {
            this._repository = new ManualAppointmentRepository();
            this.findUserId = new FindUserId();
        }

        //[AllowAnonymous]
        [HttpPost, Route("InsertManualAppointment")]
        public async Task<ActionResult<AppointmentModel>> Post([FromBody] InsertDetails lead)
        {
            //var username = User.Identity.Name;
            //var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            //IfClaimExists = claims.Any(x => x.ClaimType == "AppointmentAdd" && x.ClaimValue == "Y");
            //if (IfClaimExists)
            //{
            //    var change = await _repository.InsertAppointment(lead, lead.Appt_PatientId_FK, "");

            //    if (change != null)
            //        return Ok();
            //    else
            //        return BadRequest("Not successfull");
            //}
            //return Unauthorized();
            if (lead == null)
            {
                return BadRequest();
            }
            if (lead.CD_Id == 0 || lead.Appt_DO_Id_FK == 0 || lead.Select_day == null || lead.Select_day == "" || lead.Select_FrmTime == null || lead.Select_FrmTime == "" || lead.Select_toTime == null || lead.Select_toTime == "")
            {
                return BadRequest();
            }

            var change = await _repository.InsertAppointment(lead, lead.Appt_PatientId_FK, "");

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateManualAppointment")]
        public async Task<ActionResult<AppointmentModel>> Put([FromBody] InsertDetails lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateAppointment(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllManualAppointment")]
        public async Task<IActionResult> GetAllManualAppointment()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var rolename = await this.findUserId.FindRoleNameFromUserName(userName);
                var DoctorId = await this.findUserId.FindDoctorIdFromUsername(userName);
                var HospitalId = await this.findUserId.FindHospitalIdFromUsername(userName);
                var result = await this._repository.GetAllAppointment(HospitalId, DoctorId, roleaction, rolename);
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

        [HttpDelete, Route("DeleteManualAppointment")]
        public async Task<ActionResult> DeleteManualAppointment(int Appt_Id)
        {
            if (Appt_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteAppointment(Appt_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetManualAppointmentById")]
        public async Task<ActionResult<IEnumerable<AppointmentModelById>>> GetManualAppointmentById(int Appt_Id)
        {
            if (Appt_Id == 0)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetAppointmentById(Appt_Id);
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

        [HttpGet, Route("GetDoctorDD")]
        public async Task<ActionResult<IEnumerable<GetDocDD>>> GetDoctorDD(string Select_day, string Select_FrmTime, string Select_toTime)
        {
            try
            {
                var result = await this._repository.GetDoctorDD(Select_day, Select_FrmTime, Select_toTime);
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

        [HttpPut, Route("ApproveManualAppointment")]
        public async Task<ActionResult> ApproveAppointment([FromBody] ApproveAppointment lead)
        {
            if (lead.Appt_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.ApproveAppointment(lead);

            if (change == "Appoinment Approved Successfully")
                return Ok();
            else
                return BadRequest("Not Successfully");
        }

        [HttpDelete, Route("RejectManualAppointment")]
        public async Task<ActionResult> RejectManualAppointment(int Appt_Id)
        {
            if (Appt_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.RejectAppointment(Appt_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }


    }
}
