using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PHC_AppointmentController : ControllerBase
    {
        public readonly IPHC_Appointment _repository;
        public readonly FindUserId findUserId;
        //private static Logger logger = LogManager.GetCurrentClassLogger();
        public PHC_AppointmentController()
        {
            this._repository = new PHC_AppoinmentRepository();
            this.findUserId = new FindUserId();
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
        public async Task<IActionResult> Post([FromBody] InsertPHCApptDetails lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            if (lead.Select_day == null || lead.Select_day == "" || lead.Select_FrmTime == null || lead.Select_FrmTime == "" || lead.Select_toTime == null || lead.Select_toTime == "")
            {
                return BadRequest();
            }

            var change = await _repository.InsertPHCAppointment(lead, lead.Appt_PatientId_FK, "");

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdatePHC_Appointment")]
        public async Task<IActionResult> Put([FromBody] InsertPHCApptDetails lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdatePHCAppointment(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllPHC_Appointment")]
        public async Task<IActionResult> GetAllPHCAppointment()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var rolename = await this.findUserId.FindRoleNameFromUserName(userName);
                var DoctorId = await this.findUserId.FindDoctorIdFromUsername(userName);
                var HospitalId = await this.findUserId.FindHospitalIdFromUsername(userName);
                var result = await this._repository.GetAllPHCAppointment(HospitalId, DoctorId, roleaction, rolename);
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

        [HttpDelete, Route("DeletePHC_Appointment")]
        public async Task<ActionResult> DeletePHCAppointment(int Phc_Appt_Id)
        {
            if (Phc_Appt_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeletePHCAppointment(Phc_Appt_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetPHC_AppointmentyId")]
        public async Task<ActionResult<IEnumerable<PHC_AppointmentById>>> GetPHCAppointmentById(int Phc_Appt_Id)
        {
            if (Phc_Appt_Id == 0)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetPHCAppointmentById(Phc_Appt_Id);
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

        [HttpPut, Route("ApprovePHC_Appointment")]
        public async Task<ActionResult> ApprovePHCAppointment([FromBody] ApprovePhcAppointment lead)
        {
            if (lead.Phc_Appt_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.ApprovePHCAppointment(lead);

            if (change == "Appoinment Approved Successfully")
                return Ok();
            else
                return BadRequest("Not Successfully");
        }



        [HttpDelete, Route("RejectPHC_Appointment")]
        public async Task<ActionResult> RejectPHCAppointment(int Appt_Id)
        {
            if (Appt_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.RejectPHCAppointment(Appt_Id);

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
