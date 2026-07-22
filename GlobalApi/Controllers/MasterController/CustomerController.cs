using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.MasterIReopsitory;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterReopsitory;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public readonly ICustomer _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public readonly IUserRepository userRepository;
        public readonly IAuthenticationRepository authrepository;
        public CustomerController(IAuthenticationRepository authrepository, IUserRepository userRepository)
        {
            this._repository = new CustomerRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
            this.authrepository = authrepository;
            this.userRepository = userRepository;
        }

        //[HttpGet("GetAllCustomerList")]
        //public async Task<IActionResult> GetAllCustomerList()
        //{
        //    var result = await _repository.GetAllCustomerList();

        //    if (result == null || !result.Any())
        //        return NotFound("No customers found");

        //    return Ok(result);
        //}


        //[HttpGet("GetCustomerByID/{Cust_Id}")]
        //public async Task<IActionResult> GetCustomerByID(int Cust_Id)
        //{
        //    var result = await _repository.GetCustomerByID(Cust_Id);

        //    if (result == null)
        //        return NotFound($"No customer found with Cust_Id = {Cust_Id}");

        //    return Ok(result);
        //}


    }
}
