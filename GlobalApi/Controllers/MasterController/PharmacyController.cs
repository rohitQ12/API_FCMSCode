using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using GlobalApi.GlobalClasses;
using GlobalApi.Models.Authentication;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        public readonly IPharmacy _repository;
        public readonly FindUserId findUserId;
        public PharmacyController()
        {
            this._repository = new PharmacyRepository();
            this.findUserId = new FindUserId();
        }

        [HttpPost, Route("Admin/InsertPharmacy")]
        public async Task<ActionResult<Pharmacy>> AdminPost([FromForm] Pharmacy_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertPharmacy(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPost, Route("Self/InsertPharmacy")]
        public async Task<ActionResult<Pharmacy>> SelfPost([FromForm] Pharmacy_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertPharmacy(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("Admin/UpdatePharmacy")]
        public async Task<ActionResult<Pharmacy>> AdminPut([FromForm] Pharmacy_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdatePharmacy(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("Self/UpdatePharmacy")]
        public async Task<ActionResult<Pharmacy>> SelfPut([FromForm] Pharmacy_Images lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdatePharmacy(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAllPharmacy")]
        public async Task<ActionResult<IEnumerable<Pharmacy>>> GetAllPharmacy()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var PharmacyId = await this.findUserId.FindPharmacyIdFromPharmacyOfficeUsername(userName);
                var result = await this._repository.GetAllPharmacy(PharmacyId, roleaction);
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
        
        [HttpGet, Route("Admin/GetPharmacy_DD")]
        public async Task<ActionResult<IEnumerable<Pharmacy_DD>>> AdminGetPharmacy_DD()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var PharmacyId = await this.findUserId.FindPharmacyIdFromPharmacyOfficeUsername(userName);
                var result = await this._repository.GetPharmacy_DD(PharmacyId, roleaction);
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
        [HttpGet, Route("Admin/GetPharmacyCategory_DD")]
        public async Task<ActionResult<IEnumerable<Usercategory_DD>>> AdminGetPharmacyCategory_DD()
        {
            try
            {
                var result = await this._repository.GetPharmacyCategory_DD();
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

        [HttpGet, Route("Self/GetPharmacy_DD")]
        public async Task<ActionResult<IEnumerable<Pharmacy_DD>>> SelfGetPharmacy_DD()
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var PharmacyId = await this.findUserId.FindPharmacyIdFromPharmacyOfficeUsername(userName);
                var result = await this._repository.GetPharmacy_DD(PharmacyId, roleaction);
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

        [HttpDelete, Route("DeletePharmacy")]
        public async Task<ActionResult> DeletePharmacy(int Ph_Id)
        {
            if (Ph_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeletePharmacy(Ph_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("Admin/GetPharmacyById")]
        public async Task<ActionResult<IEnumerable<PharmacyById>>> AdminGetPharmacyById(int Ph_Id)
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var PharmacyId = await this.findUserId.FindPharmacyIdFromPharmacyOfficeUsername(userName);
                var result = await this._repository.GetPharmacyById(Ph_Id, roleaction);
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

        [HttpGet, Route("Self/GetPharmacyById")]
        public async Task<ActionResult<IEnumerable<PharmacyById>>> SelfGetPharmacyById(int Ph_Id)
        {
            try
            {
                var userName = User.Identity.Name.ToString();
                var roleaction = await this.findUserId.FindRolecategoryFromUserName(userName);
                var PharmacyId = await this.findUserId.FindPharmacyIdFromPharmacyOfficeUsername(userName);
                var result = await this._repository.GetPharmacyById(Ph_Id, roleaction);
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

    }
}
