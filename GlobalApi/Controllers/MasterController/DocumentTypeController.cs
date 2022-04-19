using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        public readonly IDocumentType _repository;
        public DocumentTypeController(IDocumentType repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost, Route("InsertDocumentType")]
        public async Task<ActionResult<DocumentType>> Post([FromBody] DocumentType lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertDocumentType(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateDocumentType")]
        public async Task<ActionResult<DocumentType>> Put([FromBody] DocumentType lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateDocumentType(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAllDocumentType")]
        public async Task<ActionResult<IEnumerable<DocumentType>>> GetAllDocumentType()
        {
            try
            {
                var result = await this._repository.GetAllDocumentType();
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
        
        [HttpGet, Route("GetDocumentType_DD")]
        public async Task<ActionResult<IEnumerable<DocumentType_DD>>> GetDocumentType_DD()
        {
            try
            {
                var result = await this._repository.GetDocumentType_DD();
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
        
        [HttpDelete, Route("DeleteDocumentType")]
        public async Task<ActionResult> DeleteDocumentType(int doctype_id)
        {
            if (doctype_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteDocumentType(doctype_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetDocumentTypeById")]
        public async Task<ActionResult<IEnumerable<DocumentTypeById>>> GetDocumentTypeById(int doctype_id)
        {
            if (doctype_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetDocumentTypeById(doctype_id);
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
