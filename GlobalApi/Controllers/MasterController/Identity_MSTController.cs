using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Identity_MSTController : ControllerBase
    {
        public readonly IIdentity_MST _repository;
        public Identity_MSTController()
        {
            this._repository = new Identity_MSTRepository();
        }

        //[HttpPost, Route("InsertIdentity_MST")]
        //public async Task<ActionResult<Identity_DOC_MST>> Post([FromBody] Identity_DOC_MST lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.InsertIdentity_MST(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}


        //[HttpPut, Route("UpdateIdentity_MST")]
        //public async Task<ActionResult<Identity_DOC_MST>> Put([FromBody] Identity_DOC_MST lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }

        //    var change = await _repository.UpdateIdentity_MST(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}


        //[HttpGet, Route("GetAllIdentity_MST")]
        //public async Task<ActionResult<IEnumerable<Identity_DOC_MST>>> GetAllIdentity_MST()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetAllIdentity_MST();
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


        [HttpGet, Route("GetIdentity_MST_DD")]
        public async Task<ActionResult<IEnumerable<IdentityDD>>> GetIdentity_MST_DD()
        {
            try
            {
                var result = await this._repository.GetIdentity_MST_DD();
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


        //[HttpDelete, Route("DeleteIdentity_MST")]
        //public async Task<ActionResult> DeleteIdentity_MST(int Id)
        //{
        //    if (Id <= 0)
        //    {
        //        return BadRequest();
        //    }
        //    var change = await _repository.DeleteIdentity_MST(Id);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

    }
}
