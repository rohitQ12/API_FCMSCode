using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;
using log4net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        public readonly Istate _repository;
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(StateController));
        public StateController()
        {
            this._repository = StateRepository.Getinstance;
        }

        [HttpPost, Route("InsertState")]
        public async Task<ActionResult<States>> Post([FromBody] States lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }
            var change = await _repository.InsertState(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpPut, Route("UpdateState")]
        public async Task<ActionResult<States>> Put([FromBody] States lead)
        {
            if (lead == null)
            {
                return BadRequest();
            }

            var change = await _repository.UpdateState(lead);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetAllState")]
        public async Task<ActionResult<IEnumerable<GetStateCountry>>> GetAllState()
        {
            log.Info("Username" + User.Identity.Name + "StateController -- >");
            try
            {
                var result = await this._repository.GetAllState();
                log.Debug("GetAllState : " + User.Identity.Name + " StateController:Aprslcyclemap : Start ->");
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                log.Error("Username : " + User.Identity.Name + " - StateController : Error - " + ex.Message + " ->");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpGet, Route("GetState_DD")]
        public async Task<ActionResult<IEnumerable<State_DD>>> GetState_DD(int cntry_id)
        {
            try
            {
                var result = await this._repository.GetState_DD(cntry_id);
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
        
        [HttpDelete, Route("DeleteState")]
        public async Task<ActionResult> DeleteState(int stat_id)
        {
            if (stat_id <= 0)
            {
                return BadRequest();
            }
            var change = await _repository.DeleteState(stat_id);

            if (change != null)
                return Ok();
            else
                return BadRequest("Not successfull");
        }
        
        [HttpGet, Route("GetStateById")]
        public async Task<ActionResult<IEnumerable<StateById>>> GetStateById(int stat_id)
        {
            if (stat_id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await this._repository.GetStateById(stat_id);
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
