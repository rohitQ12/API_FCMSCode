using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Authorization;
using GlobalApi.GlobalClasses;
using Microsoft.AspNetCore.Identity;
using GlobalApi.Models.Authentication;
using GlobalApi.Data;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        public readonly IAppointment _repository;
        public readonly FindUserId findUserId;
        private readonly UserManager<AuthUser> userManager;
        private readonly RoleManager<AspNetRole> roleManager;
        private readonly GlobalContext auth = null!;
        public AppointmentController(IAppointment repository,GlobalContext auth,
            UserManager<AuthUser> userManager,
            RoleManager<AspNetRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.auth = auth;
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.findUserId = new FindUserId(userManager, roleManager, auth);
        }

        //[AllowAnonymous]
        [HttpPost, Route("Self/InsertAppointment")]
        public async Task<ActionResult<AppointmentModel>> SelfPost([FromBody] InsertDetails lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            if (lead.CD_Id == 0 || lead.Appt_DO_Id_FK == 0 || lead.Select_day == null || lead.Select_day == "" || lead.Select_FrmTime == null || lead.Select_FrmTime == "" || lead.Select_toTime == null || lead.Select_toTime == "")
            {
                return BadRequest();
            }
            var userName = User.Identity.Name.ToString();
            var patientid = await findUserId.FindPatientIdFromUserId(userName);
            var change = await _repository.InsertAppointment(lead, patientid);

            if (change != null)
                return Ok("Successfull");
            else
                return BadRequest("Not successfull");
        }

        [HttpPost, Route("Admin/InsertAppointment")]
        public async Task<ActionResult<AppointmentModel>> AdminPost([FromBody] InsertDetails lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            if (lead.CD_Id == 0 || lead.Appt_DO_Id_FK == 0 || lead.Select_day == null || lead.Select_day == "" || lead.Select_FrmTime == null || lead.Select_FrmTime == "" || lead.Select_toTime == null || lead.Select_toTime == "")
            {
                return BadRequest();
            }

            var change = await _repository.InsertAppointment(lead,lead.Appt_PatientId_FK);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("Self/UpdateAppointment")]
        public async Task<ActionResult<AppointmentModel>> SelfPut([FromBody] AppointmentModel lead)
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

        [HttpPut, Route("Admin/UpdateAppointment")]
        public async Task<ActionResult<AppointmentModel>> AdminPut([FromBody] AppointmentModel lead)
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

        [HttpGet, Route("Self/GetAllAppointment")]
        public async Task<ActionResult<IEnumerable<AppointmentModel>>> SelfGetAllAppointment()
        {
            try
            {
                var result = await this._repository.GetAllAppointment();
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

        [HttpGet, Route("Admin/GetAllAppointment")]
        public async Task<ActionResult<IEnumerable<AppointmentModel>>> AdminGetAllAppointment()
        {
            try
            {
                var result = await this._repository.GetAllAppointment();
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

        [HttpDelete, Route("Self/DeleteAppointment")]
        public async Task<ActionResult> SelfDeleteAppointment(int Appt_Id)
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

        [HttpDelete, Route("Admin/DeleteAppointment")]
        public async Task<ActionResult> AdminDeleteAppointment(int Appt_Id)
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

        [HttpGet, Route("Self/GetAppointmentById")]
        public async Task<ActionResult<IEnumerable<AppointmentModelById>>> SelfGetAppointmentById()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var patientid = await findUserId.FindPatientIdFromUserId(userName);
                var result = await this._repository.GetAppointmentById(patientid);
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

        [HttpGet, Route("Admin/GetAppointmentById")]
        public async Task<ActionResult<IEnumerable<AppointmentModelById>>> AdminGetAppointmentById(int Appt_PatientId_FK)
        {
            if (Appt_PatientId_FK == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetAppointmentById(Appt_PatientId_FK);
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
        public async Task<ActionResult<IEnumerable<GetDocDD>>> GetDoctorDD(string Select_day , string Select_FrmTime , string Select_toTime)
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

        [HttpPost, Route("ApproveAppointment")]
        public async Task<ActionResult> ApproveAppointment(int Appt_Id)
        {
            if (Appt_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.ApproveAppointment(Appt_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        //[AllowAnonymous]
        [HttpPost, Route("Self/InsertApptBasedOnSymptoms")]
        public async Task<ActionResult<AppointmentModel>> SymptPost([FromBody] ApptonDiffCategory lead , int SYM_MST_Id_FK)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            if (lead.CD_Id == 0 || lead.Appt_DO_Id_FK == 0 || lead.Select_day == null || lead.Select_day == "" || lead.Select_FrmTime == null || lead.Select_FrmTime == "" || lead.Select_toTime == null || lead.Select_toTime == "")
            {
                return BadRequest();
            }
            var userName = User.Identity.Name.ToString();
            var patientid = await findUserId.FindPatientIdFromUserId(userName);
            var change = await _repository.InsertApptBasedOnSymptoms(lead, patientid, SYM_MST_Id_FK);
            //var change = await _repository.InsertApptBasedOnSymptoms(lead, 3, SYM_MST_Id_FK);

            if (change != null)
                return Ok("Successfull");
            else
                return BadRequest("Not successfull");
        }

        [HttpPost, Route("Self/InsertApptBasedOnDisease")]
        public async Task<ActionResult<AppointmentModel>> DisPost([FromBody] ApptonDiffCategory lead, int Dis_Id_FK)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            if (lead.CD_Id == 0 || lead.Appt_DO_Id_FK == 0 || lead.Select_day == null || lead.Select_day == "" || lead.Select_FrmTime == null || lead.Select_FrmTime == "" || lead.Select_toTime == null || lead.Select_toTime == "")
            {
                return BadRequest();
            }
            //var userName = User.Identity.Name.ToString();
            //var patientid = await findUserId.FindPatientIdFromUserId(userName);
            //var change = await _repository.InsertApptBasedOnSymptoms(lead, patientid, Dis_Id_FK);
            var change = await _repository.InsertApptBasedOnSymptoms(lead, 4, Dis_Id_FK);

            if (change != null)
                return Ok("Successfull");
            else
                return BadRequest("Not successfull");
        }

        [HttpPost, Route("Self/InsertApptBasedOnDoctor")]
        public async Task<ActionResult<AppointmentModel>> DocPost([FromBody] ApptonDiffCategory lead, int DO_Id)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            if (lead.CD_Id == 0 || lead.Appt_DO_Id_FK == 0 || lead.Select_day == null || lead.Select_day == "" || lead.Select_FrmTime == null || lead.Select_FrmTime == "" || lead.Select_toTime == null || lead.Select_toTime == "")
            {
                return BadRequest();
            }
            //var userName = User.Identity.Name.ToString();
            //var patientid = await findUserId.FindPatientIdFromUserId(userName);
            //var change = await _repository.InsertApptBasedOnSymptoms(lead, patientid, DO_Id);
            var change = await _repository.InsertApptBasedOnSymptoms(lead, 5, DO_Id);

            if (change != null)
                return Ok("Successfull");
            else
                return BadRequest("Not successfull");
        }

        [HttpPost, Route("Self/InsertApptBasedOnSpecalization")]
        public async Task<ActionResult<AppointmentModel>> SpecPost([FromBody] ApptonDiffCategory lead, int SP_Id)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            if (lead.CD_Id == 0 || lead.Appt_DO_Id_FK == 0 || lead.Select_day == null || lead.Select_day == "" || lead.Select_FrmTime == null || lead.Select_FrmTime == "" || lead.Select_toTime == null || lead.Select_toTime == "")
            {
                return BadRequest();
            }
            //var userName = User.Identity.Name.ToString();
            //var patientid = await findUserId.FindPatientIdFromUserId(userName);
            //var change = await _repository.InsertApptBasedOnSymptoms(lead, patientid, SP_Id);
            var change = await _repository.InsertApptBasedOnSymptoms(lead, 6, SP_Id);

            if (change != null)
                return Ok("Successfull");
            else
                return BadRequest("Not successfull");
        }


    }

}
