using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugMasterController : ControllerBase
    {
        public readonly IDrugMaster _repository;
        public DrugMasterController(IDrugMaster repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertDrugMaster")]
        public async Task<ActionResult<DrugMaster>> Post([FromBody] DrugMaster lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDrugMaster(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateDrugMaster")]
        public async Task<ActionResult<DrugMaster>> Put([FromBody] DrugMaster lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDrugMaster(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllDrugMaster")]
        public async Task<ActionResult<IEnumerable<GetAllDrugMaster>>> GetAllDrugMaster()
        {
            try
            {
                var result = await this._repository.GetAllDrugMaster();
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

        [HttpDelete, Route("DeleteDrugMaster")]
        public async Task<ActionResult> DeleteDrugMaster(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDrugMaster(Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetDrugMasterById")]
        public async Task<ActionResult<IEnumerable<GetDrugById>>> GetDrugMasterById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDrugMasterById(Id);
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

        [HttpGet, Route("GetDrugTypeDD")]
        public async Task<ActionResult<IEnumerable<DrugTypeDD>>> GetDrugTypeDD()
        {
            try
            {
                var result = await this._repository.GetDrugTypeDD();
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

        [HttpGet, Route("GetUnitDD")]
        public async Task<ActionResult<IEnumerable<UnitDD>>> GetUnitDD(int DT_Id_FK)
        {
            try
            {
                var result = await this._repository.GetUnitDD(DT_Id_FK);
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
