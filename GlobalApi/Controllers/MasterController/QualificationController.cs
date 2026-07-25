using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.GlobalClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalApi.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
        public readonly IQualification _repository;
        private readonly ClaimsAuthorization claimsAuthorization;
        private bool IfClaimExists = false;
        public QualificationController()
        {
            this._repository = new QualificationRepository();
            this.claimsAuthorization = new ClaimsAuthorization();
        }

        [HttpPost, Route("InsertQualification")]
        public async Task<IActionResult> Post([FromBody] Qualification Qualification)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertQualification(Qualification);

                if (change == "Qualification Added Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();
        }
        //Online Portals
        [HttpPost, Route("InsertQualification_Online")]
        public async Task<IActionResult> Post([FromBody] Qualification_Online Qualification_Online)
        {

            var username = User.Identity.Name;
            //var username = "8073043647";

            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationAdd" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.InsertQualification_Online(Qualification_Online);

                if (change == "Qualification Added Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();
        }

        [HttpPut, Route("UpdateQualification")]
        public async Task<IActionResult> Put([FromBody] Qualification Qualification)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateQualification(Qualification);

                if (change == "Qualification Updated Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();
        }
        //Online Portals
        [HttpPut, Route("UpdateQualification_Online")]
        public async Task<IActionResult> Put([FromBody] Qualification_Online Qualification_Online)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationEdit" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.UpdateQualification_Online(Qualification_Online);

                if (change == "Qualification Updated Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();
        }



        [HttpGet, Route("GetAllQualification")]
        public async Task<IActionResult> GetAllQualification()
        {
            var result = await this._repository.GetAllQualification();
            return Ok(result);
        }

        [HttpGet, Route("GetAllQualification_Skillset_DD")]
        public async Task<IActionResult> GetAllQualification_Skillset_DD()
        {
            var result = await this._repository.GetAllQualification_Skillset_DD();
            return Ok(result);
        }


        //Online Potrals
        [HttpGet, Route("GetAllQualification_Online")]
        public async Task<IActionResult> GetAllQualification_Online()
        {
            var result = await this._repository.GetAllQualification_Online();
            return Ok(result);
        }



        [HttpGet, Route("GetQualification_DD")]
        public async Task<IActionResult> GetQualification_DD()
        {
            var result = await this._repository.GetQualification_DD();
            return Ok(result);
        }

        [HttpGet, Route("GetQualification_DD_Mobile")]
        public async Task<IActionResult> GetQualification_DD_Mobile()
        {
            List<NoQualFound> noQualList = new List<NoQualFound>();
            var result = await this._repository.GetQualification_DD();
            if (result.Count > 0)
            {
                List<NoQualFound> DefQualList = result.Select(qual => new NoQualFound
                {
                    qualification_id = qual.qualification_id,
                    qualification_Name = qual.qualification_Name
                }).ToList();

                NoQualFound defaultQual = new NoQualFound { qualification_id = 0, qualification_Name = "Select qualification" };

                DefQualList.Insert(0, defaultQual);
                return Ok(DefQualList);
            }

            noQualList.Add(new NoQualFound { qualification_id = 0, qualification_Name = "Qualification not found" });
            return Ok(noQualList);

        }
        //Online Potrals 
        [HttpGet, Route("GetQualification_DD_Online")]
        public async Task<IActionResult> GetQualification_DD_Online()
        {
            var result = await this._repository.GetQualification_DD_Online();
            return Ok(result);
        }




        [HttpDelete, Route("DeleteQualification")]
        public async Task<IActionResult> DeleteQualification(int qualification_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteQualification(qualification_id);
                if (change == "Qualification Deleted Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();
        }
        //Online Potrals
        [HttpDelete, Route("DeleteQualification_Online")]
        public async Task<IActionResult> DeleteQualification_Online(int qualification_id)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationDelete" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.DeleteQualification_Online(qualification_id);
                if (change == "Qualification Deleted Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();
        }



        [HttpGet, Route("GetQualificationById")]
        public async Task<IActionResult> GetQualificationById(int qualification_id)
        {
            var result = await this._repository.GetQualificationById(qualification_id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Qualification not found");

        }
        //Online Portals
        [HttpGet, Route("GetQualificationById_Online")]
        public async Task<IActionResult> GetQualificationById_Online(int qualification_id)
        {
            var result = await this._repository.GetQualificationById_Online(qualification_id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Qualification not found");

        }



        [HttpPut, Route("ApproveQualification")]
        public async Task<IActionResult> ApproveQualification([FromBody] ApproveQualification ApproveQualification)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationApprove" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveQualification(ApproveQualification);

                if (change == "Qualification Approved Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();
        }
        //Online Portals
        [HttpPut, Route("ApproveQualification_Online")]
        public async Task<IActionResult> ApproveQualification_Online([FromBody] ApproveQualification_Online ApproveQualification_Online)
        {
            var username = User.Identity.Name;
            var claims = await claimsAuthorization.GetClaimsListForUserAsync(username);
            IfClaimExists = claims.Any(x => x.ClaimType == "QualificationApprove_Online" && x.ClaimValue == "Y");
            if (IfClaimExists)
            {
                var change = await _repository.ApproveQualification_Online(ApproveQualification_Online);

                if (change == "Qualification Approved Successfully")
                {
                    return Ok();
                }
                return BadRequest(change);
            }
            return Unauthorized();
        }

    }
}
