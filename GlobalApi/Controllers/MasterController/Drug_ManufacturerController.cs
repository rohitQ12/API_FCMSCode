using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Drug_ManufacturerController : ControllerBase
    {
        public readonly IDrug_Manufacturer _repository;
        public Drug_ManufacturerController()
        {
            this._repository = new Drug_ManufacturerRepository();

        }


        [HttpGet, Route("GetAllDrug_Manufacturer")]
        public async Task<ActionResult<IEnumerable<Drug_Manufacturer>>> GetAll()
        {
            try
            {
                var result = await this._repository.GetAllDrug_Manufacturer();
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

        [HttpGet, Route("GetDrug_Manufacturer_DD")]
        public async Task<ActionResult<IEnumerable<Drug_ManufacturerDD>>> GetDD()
        {
            try
            {
                var result = await this._repository.GetDrug_Manufacturer_DD();
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
