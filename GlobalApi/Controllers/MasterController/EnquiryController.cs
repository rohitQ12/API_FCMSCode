using GlobalApi.IRepository.MasterIRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Authorization;
using GlobalApi.Repository.MasterRepository;
using System.Net.Http.Headers;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {

        private readonly EnquiryIRepository _repository;
        private readonly IEMailService _EMailService;

        public EnquiryController(IEMailService EMailService,EnquiryIRepository repository)
        {
            this._EMailService = EMailService;
            this._repository = repository;

        }

        [HttpPost, Route("InsertEnquiry")]
        public async Task<IActionResult> InsertEnquiry([FromBody] Enquiry_details lead)
        {
            //var username = User.Identity.Name;
            var change = await _repository.InsertEnquiry(lead);
            if (change == "Enquiry sended Successfully")
            {
                return Ok();
            }
            return Ok(change);

        }


        [HttpGet, Route("GetAllEnquiry_details")]
        public async Task<IActionResult> GetAllEnquiry_details()
        {
            try
            {
                var result = await this._repository.GetAllEnquiry_details();
                //return Ok(result);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Data not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpGet, Route("GetAllEnquiry_detailsById")]
        public async Task<IActionResult> GetAllEnquiry_detailsById(string cus_eq_phoneNo)
        {
            try
            {
                var result = await this._repository.GetAllEnquiry_detailsById(cus_eq_phoneNo);
                //return Ok(result);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Data not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpGet, Route("GetAllEnquiryInformation")]
        public async Task<IActionResult> GetAllEnquiryInformation()
        {
            try
            {
                var result = await this._repository.GetAllEnquiryInformation();
                //return Ok(result);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Data not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        [HttpGet, Route("Enquire_TypeDD")]
        public async Task<IActionResult> GetAllEnquire_Type()
        {
            var result = await this._repository.GetAllEnquire_Type();
            if (result.Any())
            {
                return Ok(result);
            }

            return NotFound("Data not found");
        }


        [HttpDelete, Route("DelelteEnquire")]
        public async Task<IActionResult> DeleteEnquire(int id)
        {

            var change = await _repository.DeleteEnquiry(id);
            if (change == "Successfully Deleted")
            {
                return Ok();
            }
            return BadRequest(change);
        }
       
    }


}
