using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.Repository.AdminRepository;
using GlobalApi.Models.Authentication;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        public readonly IOfficesRepository _repository;
        public OfficesController()
        {
            this._repository = new OfficesRepository();
        }

        [HttpPost, Route("InsertOffices")]
        public async Task<ActionResult<Offices>> Post([FromBody] Offices subPages)
        {
            var change = await _repository.InsertOffice(subPages);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        [HttpPut, Route("UpdateOffices")]
        public async Task<ActionResult<Offices>> Put([FromBody] Offices subPages)
        {
            var change = await _repository.UpdateOffice(subPages);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        //[HttpGet, Route("GetAllAppPage")]
        //public async Task<ActionResult<IEnumerable<SubMenuPage>>> GetAllAppPage()
        //{
        //    try
        //    {
        //        var result = await this._repository.GetAllAppPage();
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
        
        [HttpPut, Route("DeleteOffices")]
        public async Task<ActionResult> DeleteAppPage(int Id)
        {
            var change = await _repository.DeleteOffice(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetOfficesById")]
        public async Task<ActionResult<IEnumerable<Offices>>> GetAppPageById(int Id)
        {
            try
            {
                var result = await this._repository.GetOfficeById(Id);
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
        
        [HttpGet, Route("GetOffices")]
        public async Task<ActionResult<IEnumerable<Offices>>> GetAppPage()
        {
            try
            {
                var result = await this._repository.GetOffice();
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
        [HttpGet, Route("Admin/GetOfficeCategory_DD")]
        public async Task<ActionResult<IEnumerable<Usercategory_DD>>> GetOfficeCategory_DD()
        {
            try
            {
                var result = await this._repository.GetOfficeCategory_DD();
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
    }
}
