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
    public class PartnerController : ControllerBase
    {
        public readonly IPartnerRepository _repository;
        public readonly FindUserId findUserId;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public readonly IUserRepository userRepository;
        public readonly IAuthenticationRepository authrepository;
        public PartnerController(IAuthenticationRepository authrepository, IUserRepository userRepository)
        {
            this._repository = new PartnerRepository();
            this.findUserId = new FindUserId();
            this.claimsAuthorization = new ClaimsAuthorization();
            this.authrepository = authrepository;
            this.userRepository = userRepository;
        }



        [HttpGet("GetAllPartnersList")]
        public async Task<IActionResult> GetAllPartnersList()
        {
            var result = await _repository.GetAllPartnersList();

            if (result == null || !result.Any())
                return NotFound("No customers found");

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdatePartner")]
        public async Task<IActionResult> UpdatePartner([FromBody] Partner partner)
        {
            if (partner.PartnerId <= 0)
                return BadRequest(new { message = "Invalid PartnerId" });

            var result = await _repository.UpdatePartnerDetails(partner);
            return Ok(new { message = result });
        }


        [HttpGet("GetPartnerDetailsByID/{PartnerId}")]
        public async Task<IActionResult> GetPartnerDetailsByID(int PartnerId)
        {
            var result = await _repository.GetPartnerDetailsByID(PartnerId);

            if (result == null)
                return NotFound($"No partner found with PartnerId = {PartnerId}");

            return Ok(result);
        }



    }
}