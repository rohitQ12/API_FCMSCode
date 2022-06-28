using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergySigns_DTLController : ControllerBase
    {
        public readonly IAllergySigns_DTL _repository;
        public AllergySigns_DTLController()
        {
            this._repository = new AllergySigns_DTLRepository();
        }

        //[HttpPost, Route("InsertAllergySigns_DTL")]
        //public async Task<ActionResult<AllergySigns_DTL>> Post([FromBody] AllergySigns_DTL lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertAllergySigns_DTL(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        //[HttpPut, Route("UpdateAllergySigns_DTL")]
        //public async Task<ActionResult<AllergySigns_DTL>> Put([FromBody] List<AllergySigns_DTL> lead, int App_id)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }

        //    var change = await _repository.UpdateAllergySigns_DTLtest(lead, App_id);

        //    if (change == true)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

        [HttpGet, Route("GetAllAllergySigns_DTL")]
        public async Task<ActionResult<IEnumerable<AllergySigns_DTL>>> GetAllAllergySigns_DTL()
        {
            try
            {
                var result = await this._repository.GetAllAllergySigns_DTL();
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

        [HttpDelete, Route("DeleteAllergySigns_DTL")]
        public async Task<ActionResult> DeleteAllergySigns_DTL(int Ddtl_Id)
        {
            if (Ddtl_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteAllergySigns_DTL(Ddtl_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllergySigns_DTLById")]
        public async Task<ActionResult<IEnumerable<GetDiseaseDtlById>>> GetAllergySigns_DTLById(int Ddtl_PR_Id_FK)
        {
            if (Ddtl_PR_Id_FK == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetAllergySigns_DTLById(Ddtl_PR_Id_FK);
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
