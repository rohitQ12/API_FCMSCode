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
    public class ContactUsController : ControllerBase
    {

        public readonly ContactUsIRepository _repository;

        public ContactUsController()
        {
            this._repository = new ContactUsRepository();

        }


        //[HttpPost, Route("InsertContactUs")]
        //public async Task<IActionResult> InsertContactUs([FromForm] ContactUs lead)
        //{
        //    //var username = User.Identity.Name;
        //    var change = await _repository.InsertContactUs(lead);
        //    if (change == "Message sent Successfully")
        //    {
        //        return Ok();
        //    }
        //    return Ok(change);

        //}


        //[HttpGet, Route("GetAllCustomer_ContactUs")]
        //public async Task<IActionResult> GetAllCustomer_ContactUs()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetAllCustomer_ContactUs();
        //        //return Ok(result);
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }
        //        return NotFound("Data not found");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpGet, Route("GetCustomer_ContactUsById")]
        //public async Task<IActionResult> GetCustomer_ContactUsById(string cust_phone_no)
        //{
        //    try
        //    {
        //        var result = await this._repository.GetCustomer_ContactUsById(cust_phone_no);
        //        //return Ok(result);
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }
        //        return NotFound("Data not found");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}



    }
}
