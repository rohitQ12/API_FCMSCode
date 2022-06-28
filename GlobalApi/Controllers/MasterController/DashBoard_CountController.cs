using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoard_CountController : ControllerBase
    {
        public readonly IDashboard_Count _repository;
        public DashBoard_CountController()
        {
            this._repository = new Dashboard_CountRepository();
        }


        [HttpGet, Route("GetPatient_Count")]
        public  ActionResult GetPatient_Count()
        {
            try
            {
                var result =  this._repository.GetPatient_Count();
                if (result==0)
                {
                    return Ok(result);
                }
                else if(result != 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet, Route("GetNetworkHospital_Count")]
        public ActionResult GetNetworkHospital_Count()
        {
            try
            {
                var result = this._repository.GetNetworkHospital_Count();
                if (result == 0)
                {
                    return Ok(result);
                }
                else if (result != 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet, Route("GetHospital_Count")]
        public ActionResult GetHospital_Count()
        {
            try
            {
                var result = this._repository.GetHospital_Count();
                if (result == 0)
                {
                    return Ok(result);
                }
                else if (result != 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet, Route("GetPharmacy_Count")]
        public ActionResult GetPharmacy_Count()
        {
            try
            {
                var result = this._repository.GetPharmacy_Count();
                if (result == 0)
                {
                    return Ok(result);
                }
                else if (result != 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet, Route("GetDiagnostic_Count")]
        public ActionResult GetDiagnostic_Count()
        {
            try
            {
                var result = this._repository.GetDiagnostic_Count();
                if (result == 0)
                {
                    return Ok(result);
                }
                else if (result != 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet, Route("GetTotalAppointment_Count")]
        public ActionResult GetTotalAppointment_Count()
        {
            try
            {
                var result = this._repository.GetTotalAppointment_Count();
                if (result == 0)
                {
                    return Ok(result);
                }
                else if (result != 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet, Route("GetTodayAppointment_Count")]
        public ActionResult GetTodayAppointment_Count()
        {
            try
            {
                var result = this._repository.GetTodayAppointment_Count();
                if (result == 0)
                {
                    return Ok(result);
                }
                else if (result != 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet, Route("GetTodayConsultation_Count")]
        public ActionResult GetTodayConsultation_Count()
        {
            try
            {
                var result = this._repository.GetTodayConsultation_Count();
                if (result == 0)
                {
                    return Ok(result);
                }
                else if (result != 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet, Route("GetTotalConsultation_Count")]
        public ActionResult GetTotalConsultation_Count()
        {
            try
            {
                var result = this._repository.GetTotalConsultation_Count();
                if (result == 0)
                {
                    return Ok(result);
                }
                else if (result != 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet, Route("Getreferal_Count")]
        public ActionResult Getreferal_Count()
        {
            try
            {
                var result = this._repository.Getreferal_Count();
                if (result == 0)
                {
                    return Ok(result);
                }
                else if (result != 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
