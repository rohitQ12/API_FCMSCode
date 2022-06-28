using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class VleController : ControllerBase
    {
        public readonly IVle _repository;
        public VleController()
        {
            this._repository = new VleRepository();
        }

        [HttpPost, Route("InsertVle")]
        public async Task<ActionResult<Vle>> Post([FromForm] VleModel_Image lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertVle(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateVle")]
        public async Task<ActionResult<Vle>> Put([FromForm] VleModel_Image lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateVle(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAllVle")]
        public async Task<ActionResult<IEnumerable<Vle>>> GetAllVle()
        {
            try
            {
                var result = await this._repository.GetAllVle();
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
        
        //[HttpGet, Route("GetVle_DD")]
        //public async Task<ActionResult<IEnumerable<Vle_DD>>> GetVle_DD()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetVle_DD();
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
        
        [HttpDelete, Route("DeleteVle")]
        public async Task<ActionResult> DeleteVle(int VL_Id)
        {
            if (VL_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteVle(VL_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetVleById")]
        public async Task<ActionResult<IEnumerable<VleBy_Id>>> GetVleById(int VL_Id)
        {
            if (VL_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetVleById(VL_Id);
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
        
        [HttpGet, Route("GetVle_Images")]
        public IActionResult Get_images(string filename)
        {
            string _filepath = Path.GetFullPath("wwwroot/Vle/");
            var filepath = _filepath + filename;
            return PhysicalFile(@filepath, "image/jpeg");
        }

        [HttpGet, Route("Vle_DD")]
        public async Task<ActionResult<IEnumerable<Vle_DD>>> GetVle_DD()
        {
            try
            {
                var result = await this._repository.GetVle_DD();
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


        [HttpPut, Route("ApproveVle")]
        public async Task<ActionResult> ApproveVle([FromBody] ApproveVle lead)
        {
            if (lead.VL_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.ApproveVle(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }


    }
}
