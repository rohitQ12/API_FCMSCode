//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using GlobalApi.IRepository.MasterIRepository;
//using GlobalApi.Models.Master;

//namespace GlobalApi.Controllers.MasterController
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SHReferralsController : ControllerBase
//    {
//        public readonly ISHReferrals _repository;
//        public SHReferralsController(ISHReferrals repository)
//        {
//            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
//        }

//        [HttpPost, Route("InsertSHReferrals")]
//        public async Task<ActionResult<SHReferrals>> Post([FromBody] SHReferrals lead, string? Time, string? date)
//        {
//            if (lead == null)
//            {
//                return BadRequest();
//            }
//            var change = await _repository.InsertSHReferrals(lead, Time, date);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }
//        [HttpPut, Route("UpdateSHReferrals")]
//        public async Task<ActionResult<SHReferrals>> Put([FromBody] SHReferrals lead)
//        {
//            if (lead == null)
//            {
//                return BadRequest();
//            }

//            var change = await _repository.UpdateSHReferrals(lead);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }
//        [HttpGet, Route("GetAllSHReferrals")]
//        public async Task<ActionResult<IEnumerable<SHReferrals>>> GetAllSHReferrals()
//        {
//            try
//            {
//                var result = await this._repository.GetAllSHReferrals();
//                if (result.Any())
//                {
//                    return Ok(result);
//                }

//                return NotFound();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }
//        //[HttpGet, Route("GetSHReferrals_DD")]
//        //public async Task<ActionResult<IEnumerable<SHReferrals_DD>>> GetSHReferrals_DD()
//        //{
//        //    try
//        //    {
//        //        var result = await this._repository.GetSHReferrals_DD();
//        //        if (result.Any())
//        //        {
//        //            return Ok(result);
//        //        }

//        //        return NotFound();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//        //    }
//        //}
//        [HttpDelete, Route("DeleteSHReferrals")]
//        public async Task<ActionResult> DeleteSHReferrals(int SHR_Id)
//        {
//            if (SHR_Id <= 0)
//            {
//                return BadRequest();
//            }
//            var change = await _repository.DeleteSHReferrals(SHR_Id);

//            if (change != null)
//                return Ok();
//            else
//                return BadRequest("Not successfull");
//        }
//        [HttpGet, Route("GetSHReferralsById")]
//        public async Task<ActionResult<IEnumerable<SHReferralsBy_Id>>> GetSHReferralsById(int SHR_Id)
//        {
//            if (SHR_Id == null)
//            {
//                return BadRequest();
//            }
//            try
//            {
//                var result = await this._repository.GetSHReferralsById(SHR_Id);
//                if (result == null)
//                {
//                    return NotFound();
//                }
//                return Ok(result);

//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }

//    }
//}
