using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Drug_PrescriptionController : ControllerBase
    {
        public readonly IDrug_Prescription _repository;
        public Drug_PrescriptionController()
        {
            this._repository = new Drug_PrescriptionRepository();
           
        }

        [HttpPost, Route("InserDrug_Prescription")]
        public async Task<ActionResult<Drug_Prescription>> Post([FromBody] Drug_Prescription lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InserDrug_Prescription(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpPut, Route("UpdateDrug_Prescription")]
        public async Task<ActionResult<Drug_Prescription>> Put([FromBody] Drug_Prescription lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDrug_Prescription(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }

        [HttpGet, Route("GetAllDrug_Prescription")]
        public async Task<ActionResult<IEnumerable<Drug_PrescriptionAll>>> GetAll()
        {
            try
            {
                var result = await this._repository.GetAllDrug_Prescription();
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
        [HttpDelete, Route("DeleteDrug_Prescription")]
        public async Task<ActionResult> Delete(int Dtl_Id)
        {
            if (Dtl_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDrug_Prescription(Dtl_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
            [HttpGet, Route("GetById_Drug_Prescription")]
            public async Task<ActionResult<IEnumerable<Drug_PrescriptionAll>>> GetById(int Cons_Id)
            {
                if (Cons_Id == null)
                {
                    return BadRequest();
                }
                try
                {
                    var result = await this._repository.GetById_Drug_Prescription(Cons_Id);
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
