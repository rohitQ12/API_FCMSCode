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

            if (change == "Prescription inserted successfully")
                return Ok();
            else
                return BadRequest(change);
        }

        [HttpPut, Route("UpdateDrug_Prescription")]
        public async Task<ActionResult<Drug_Prescription>> Put([FromBody] Drug_Prescription lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDrug_Prescription(lead);

            if (change == "Presciption updated successfully")
                return Ok();
            else
                return BadRequest(change);
        }

        [HttpGet, Route("GetAllDrug_Prescription")]
        public async Task<ActionResult<IEnumerable<Drug_PrescriptionAll>>> GetAll()
        {
           
                var result = await this._repository.GetAllDrug_Prescription();
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound("Drugs not found");
            
        }
        [HttpDelete, Route("DeleteDrug_Prescription")]
        public async Task<ActionResult> Delete(int Dtl_Id)
        {
           
            var change = await _repository.DeleteDrug_Prescription(Dtl_Id);

            if (change == "Prescription deleted successfully")
                return Ok();
            else
                return BadRequest(change);
        }
            [HttpGet, Route("GetById_Drug_Prescription")]
            public async Task<ActionResult<IEnumerable<Drug_PrescriptionAll>>> GetById(int Cons_Id)
            {
                var result = await this._repository.GetById_Drug_Prescription(Cons_Id);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound("Drug not found");
            }
        
    }
}
