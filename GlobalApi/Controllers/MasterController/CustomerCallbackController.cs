using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCallbackController : ControllerBase
    {
        public readonly ICustomerCallback _repository;
        private IEMailService _EMailService;
        public CustomerCallbackController(ICustomerCallback _repository, IEMailService EMailService)
        {
            this._EMailService = EMailService;
            this._repository = _repository;

        }

        //[HttpPost, Route("InsertCustomer_Callback")]
        //public async Task<IActionResult> InsertCustomer_Callback([FromBody] Customer_Callback objCust)
        //{
        //    try
        //    {
        //        if (objCust == null)
        //        {
        //            return BadRequest("Invalid request data.");
        //        }

        //        if (string.IsNullOrWhiteSpace(objCust.cust_name))
        //        {
        //            return BadRequest("Please enter your name.");
        //        }
        //        if (string.IsNullOrWhiteSpace(objCust.cust_email))
        //        {
        //            return BadRequest("Please enter your email.");
        //        }
        //        if (string.IsNullOrWhiteSpace(objCust.cust_phone_no))
        //        {
        //            return BadRequest("Please enter your phone number.");
        //        }
        //        if (string.IsNullOrWhiteSpace(objCust.cust_msg_desc))
        //        {
        //            return BadRequest("Please enter message detail.");
        //        }

        //        var response = await _repository.InsertCustomer_Callback(objCust);
        //        return Ok(response);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        //    }
        //}

    }
}
