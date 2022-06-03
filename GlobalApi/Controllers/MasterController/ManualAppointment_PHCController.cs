using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManualAppointment_PHCController : ControllerBase
    {
        public readonly IManualAppointment _repository;
        //public readonly FindUserId findUserId;
        //private static Logger logger = LogManager.GetCurrentClassLogger();
        public ManualAppointment_PHCController()
        {
            this._repository = new ManualAppoinmentRepository();
            //this.findUserId = new FindUserId();
        }

        ////[AllowAnonymous]
        //[HttpPost, Route("Self/InsertAppointment")]
        //public async Task<ActionResult<AppointmentModel>> SelfPost([FromBody] InsertDetails lead)
        //{
        //    //if (lead == null)
        //    //{
        //    //    logger.Error("Username : " + User.Identity.Name + " - StateController : Error - ");
        //    //    return BadRequest();
        //    //}
        //    if (lead.CD_Id == 0 || lead.Appt_DO_Id_FK == 0 || lead.Select_day == null || lead.Select_day == "" || lead.Select_FrmTime == null || lead.Select_FrmTime == "" || lead.Select_toTime == null || lead.Select_toTime == "")
        //    {
        //        return BadRequest();
        //    }
        //    //logger.Info("Username " + User.Identity.Name + " AppointmentController -- >");
        //    //var userName = User.Identity.Name.ToString();
        //    //var patientid = await findUserId.FindPatientIdFromUserId(userName);
        //    //logger.Debug("Getpatientid : " + patientid + " AppointmentController:Aprslcyclemap : Start ->");
        //    //var UserId = await findUserId.FindUserIdFromPatientId(patientid);
        //    //logger.Debug("Getpatientuserid : " + UserId + " AppointmentController:Aprslcyclemap : Start ->");
        //    ////var change = await _repository.InsertAppointment(lead, patientid, UserId);
        //    var change = await _repository.InsertAppointment(lead, 6, "702");
        //    //logger.Debug("Insert Appointment : " + change + " AppointmentController:Aprslcyclemap : Start ->");

        //    if (change != null)
        //        return Ok("Successfull");
        //    else
        //        return BadRequest("Not successfull");
        //    logger.Error("Username : " + User.Identity.Name + " - AppointmentController : Error - ");
        //}

        [HttpPost, Route("InsertAppointment")]
        public async Task<ActionResult<ManualAppointment>> AdminPost([FromBody] InsertManualApptDetails lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            if (lead.Select_day == null || lead.Select_day == "" || lead.Select_FrmTime == null || lead.Select_FrmTime == "" || lead.Select_toTime == null || lead.Select_toTime == "")
            {
                return BadRequest();
            }

            var change = await _repository.InsertAppointment(lead, lead.Appt_PatientId_FK, "");

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("ManualAppointment_PHC/UpdateAppointment")]
        public async Task<ActionResult<ManualAppointment>> AdminPut([FromBody] InsertManualApptDetails lead)
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

        [HttpGet, Route("ManualAppointment_PHC/GetAllAppointment")]
        public async Task<ActionResult<IEnumerable<ManualAppointment>>> AdminGetAllAppointment()
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

        [HttpDelete, Route("ManualAppointment_PHC/DeleteAppointment")]
        public async Task<ActionResult> AdminDeleteAppointment(int MAppt_Id)
        {
            if (MAppt_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteAppointment(MAppt_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("ManualAppointment_PHC/GetAppointmentById")]
        public async Task<ActionResult<IEnumerable<ManualAppointmentById>>> AdminGetAppointmentById(int MAppt_Id)
        {
            if (MAppt_Id == 0)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetAdminAppointmentById(MAppt_Id);
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

        //[HttpGet, Route("GetDoctorDD")]
        //public async Task<ActionResult<IEnumerable<GetDocDD>>> GetDoctorDD(string Select_day, string Select_FrmTime, string Select_toTime)
        //{
        //    try
        //    {
        //        var result = await this._repository.GetDoctorDD(Select_day, Select_FrmTime, Select_toTime);
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpPut, Route("ApproveAppointment")]
        public async Task<ActionResult> ApproveAppointment(int MAppt_Id , string CON_ConsultedDate, string CON_ConsultedTime ,string Remarks)
        {
            if (MAppt_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.ApproveAppointment(MAppt_Id, CON_ConsultedDate, CON_ConsultedTime , Remarks);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }



        [HttpDelete, Route("RejectAppointment")]
        public async Task<ActionResult> RejectAppointment(int MAppt_Id)
        {
            if (MAppt_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.RejectAppointment(MAppt_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        //[HttpGet, Route("GetHospital_DD")]
        //public async Task<ActionResult<IEnumerable<GetHosDD>>> GetHospital_DD(int PR_Id)
        //{
        //    try
        //    {
        //        var result = await this._repository.GetHospital_DD(PR_Id);
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}


        //[HttpGet, Route("GetDoctorDDBasedOnSpecialization")]
        //public async Task<ActionResult<IEnumerable<GetDocDD>>> GetDoctorDDOnSpec(int Sp_Id, string Select_day, string Select_FrmTime, string Select_toTime)
        //{
        //    try
        //    {
        //        var result = await this._repository.GetDoctorDDOnSpec(Sp_Id, Select_day, Select_FrmTime, Select_toTime);
        //        if (result.Any())
        //        {
        //            return Ok(result);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpGet, Route("GetHospital_DD")]
        public async Task<ActionResult<IEnumerable<GetHosDD>>> GetHospital_DD()
        {
            try
            {
                var result = await this._repository.GetHospital_DD();
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


    }
}
