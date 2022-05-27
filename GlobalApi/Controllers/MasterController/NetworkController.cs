using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        public readonly INetwork _repository;
        public NetworkController()
        {
            this._repository = new NetworkRepository();
        }

        [HttpPost, Route("InsertNetwork")]
        public async Task<ActionResult<Network>> Post([FromBody] Network lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertNetwork(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateNetwork")]
        public async Task<ActionResult<Network>> Put([FromBody] Network lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateNetwork(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAllNetwork")]
        public async Task<ActionResult<IEnumerable<Network>>> GetAllNetwork()
        {
            try
            {
                var result = await this._repository.GetAllNetwork();
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
        
        [HttpGet, Route("GetNetwork_DD")]
        public async Task<ActionResult<IEnumerable<Network_DD>>> GetNetwork_DD()
        {
            try
            {
                var result = await this._repository.GetNetwork_DD();
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
        
        [HttpDelete, Route("DeleteNetwork")]
        public async Task<ActionResult> DeleteNetwork(int NE_Id)
        {
            if (NE_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteNetwork(NE_Id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetNetworkById")]
        public async Task<ActionResult<IEnumerable<NetworkById>>> GetNetworkById(int NE_Id)
        {
            if (NE_Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetNetworkById(NE_Id);
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
        
        [HttpPut, Route("ApproveNetwork")]
        public async Task<ActionResult> ApproveNetwork(int NE_Id, string? Remarks)
        {
            if (NE_Id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.ApproveNetwork(NE_Id, Remarks);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
    }
}
