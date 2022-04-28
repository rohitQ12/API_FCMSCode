using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        public readonly IPharmacy _repository;
        public PharmacyController()
        {
            this._repository = new PharmacyRepository();
        }

        [HttpPost, Route("Admin/InsertPharmacy")]
        public async Task<ActionResult<Pharmacy>> AdminPost([FromBody] Pharmacy lead)
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
        public async Task<ActionResult<Pharmacy>> SelfPost([FromBody] Pharmacy lead)
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
        public async Task<ActionResult<Pharmacy>> AdminPut([FromBody] Pharmacy lead)
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
        public async Task<ActionResult<Pharmacy>> SelfPut([FromBody] Pharmacy lead)
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
                var result = await this._repository.GetAllPharmacy();
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
                var result = await this._repository.GetPharmacy_DD();
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
                var result = await this._repository.GetPharmacy_DD();
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
            if (Ph_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetPharmacyById(Ph_Id);
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
            if (Ph_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetPharmacyById(Ph_Id);
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
