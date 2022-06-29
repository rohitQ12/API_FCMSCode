using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReVisitController : ControllerBase
    {
        public readonly IReVisit _repository;
        public ReVisitController()
        {
            this._repository = new ReVisitRepository();
        }

        [HttpPost, Route("InsertReVist")]
        public async Task<ActionResult<ReVisit>> Post([FromBody] ReVisit lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertReVisit(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllReVisit")]
        public async Task<ActionResult<IEnumerable<GetAllReVisit>>> GetAllReVisit()
        {
            try
            {
                var result = await this._repository.GetAllReVisit();
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

        [HttpDelete, Route("DeleteReVisit")]
        public async Task<ActionResult> DeleteReVisit(int RV_Id)
        {
            if (RV_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteReVisit(RV_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }


        [HttpGet, Route("GetReVisitByCON_Id")]
        public async Task<ActionResult<IEnumerable<GetAllReVisit>>> GetReVisitByCON_Id(int CON_Id)
        {
            if (CON_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetReVisitByCON_Id(CON_Id);
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

        [HttpGet, Route("GetReVisitById")]
        public async Task<ActionResult<IEnumerable<GetAllReVisit>>> GetReVisitById(int RV_Id)
        {
            if (RV_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetReVisitById(RV_Id);
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


        //[HttpGet, Route("GetReVisit_DD")]
        //public async Task<ActionResult<IEnumerable<ReVisit_DD>>> GetReVisit_DD()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetReVisit_DD();
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


        //[HttpPut, Route("UpdateReVisit")]
        //public async Task<ActionResult<ReVisit>> Put([FromBody] ReVisit lead)
        //{
        //    if (lead == null)
        //    {
        //        return BadRequest();
        //    }

        //    var change = await _repository.UpdateReVisit(lead);

        //    if (change != null)
        //        return Ok();
        //    else
        //        return BadRequest("Not successfull");
        //}

    }
}
