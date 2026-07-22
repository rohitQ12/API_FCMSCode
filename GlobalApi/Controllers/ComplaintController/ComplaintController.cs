//namespace GlobalApi.Controllers.ComplaintController
//{
//    public class ComplaintController
//    {
//    }
//}



using GlobalApi.IRepository.ComplaintIRepository;
using GlobalApi.IRepository.StatesAndCitiesIRepository;
using GlobalApi.Models.ComplaintModels;
using GlobalApi.Repository.ComplaintRepository;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Repository.StatesAndCitiesRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalApi.Controllers.ComplaintController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        ComplaintRepository obj = new ComplaintRepository();


        // Customer, User
        [HttpPost]
        [Route("InsertComplaint")]
        public async Task<IActionResult> InsertComplaint([FromBody] ComplaintInsertModel model)
        {
            if (model == null)
                return BadRequest("Invalid data.");

            string refNo = await obj.InsertComplaint(model);

            if (!string.IsNullOrEmpty(refNo))
                return Ok(new { success = true, refNo = refNo, message = "Complaint submitted successfully." });
            else
                return StatusCode(500, new { success = false, message = "Failed to submit complaint." });
        }

        //Admin 
        [HttpGet]
        [Route("GetAllComplaintDetails")]
        public async Task<IActionResult> GetAllComplaintDetails()
        {
            return Ok(await obj.GetAllComplaintDetails());
        }

        //Admin 
        [HttpGet]
        [Route("GetComplaintDetailsById/{CP_RefNo}")]
        public async Task<IActionResult> GetComplaintDetailsById(string CP_RefNo)
        {
            if (string.IsNullOrEmpty(CP_RefNo))
                return BadRequest("Reference number is required.");

            var result = await obj.GetComplaintDetailsById(CP_RefNo);

            if (result == null)
                return NotFound(new { message = "Complaint not found." });

            return Ok(result);
        }


        //Admin
        [HttpGet("CheckUserTypeAdmin")]
        public async Task<IActionResult> CheckUserTypeAdmin(string userName)
        {
            var user = await obj.CheckUserTypeAdmin(userName);

            if (user == null)
            {
                return Ok(new CustomMsg
                {
                    message = "fail",
                    message_desc = "User not found"
                });
            }

            if (user.IsAdmin)
            {
                return Ok(new CustomMsg
                {
                    message = "success",
                    message_desc = "Admin logged in successfully"
                });
            }
            else
            {
                return Ok(new CustomMsg
                {
                    message = "fail",
                    message_desc = "Only users with the 'Admin' role can log in"
                });
            }
        }

        // Customer, User
        [HttpGet("CheckUserTypeCustomer")]
        public async Task<IActionResult> CheckUserTypeCustomer(string userName)
        {
            var user = await obj.CheckUserTypeCustomer(userName);

            if (user == null)
            {
                return Ok(new CustomMsg
                {
                    message = "fail",
                    message_desc = "User not found"
                });
            }

            if (user.IsAdmin)
            {
                return Ok(new CustomMsg
                {
                    message = "success",
                    message_desc = "Customer logged in successfully"
                });
            }
            else
            {
                return Ok(new CustomMsg
                {
                    message = "fail",
                    message_desc = "Only users with the 'Customer' role can log in"
                });
            }
        }


        // Customer, User
        [HttpGet]
        [Route("GetComplaintDetailsBy/{UserName}")]
        public async Task<IActionResult> GetComplaintDetailsBy(string UserName)
        {
            var data = await obj.GetComplaintDetailsBy(UserName);

            if (data == null || data.Count == 0)
            {
                return Ok(new
                {
                    message = "fail",
                    description = "No complaints found"
                });
            }

            return Ok(new
            {
                message = "success",
                data = data
            });
        }


        [HttpGet]
        [Route("GetAllStatus")]
        public async Task<IActionResult> GetAllStatus()
        {
            return Ok(await obj.GetAllStatus());
        }



        [HttpGet]
        [Route("GetAllPriority")]
        public async Task<IActionResult> GetAllPriority()
        {
            return Ok(await obj.GetAllPriority());
        }



        //[HttpPost]
        //[Route("UpdateStatusAndPriority")]
        //public async Task<IActionResult> UpdateStatusAndPriority([FromBody] UpdateComplaintStatusPriorityModel model)
        //{
        //    if (model == null || string.IsNullOrEmpty(model.CP_RefNo))
        //        return BadRequest(new { success = false, message = "Invalid request." });

        //    try
        //    {
        //        int affectedRows = await obj.UpdateComplaintStatusPriority(model);

        //        if (affectedRows > 0)
        //            return Ok(new { success = true, message = "Complaint updated successfully." });
        //        else
        //            return NotFound(new { success = false, message = "Complaint not found." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { success = false, message = ex.Message });
        //    }
        //}

        [HttpPost]
        [Route("UpdateStatusAndPriority")]
        public async Task<IActionResult> UpdateStatusAndPriority([FromBody] UpdateComplaintStatusPriorityModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.CP_RefNo))
                return BadRequest(new { success = false, message = "Invalid request." });

            try
            {
                int affectedRows = await obj.UpdateComplaintStatusPriority(model);

                if (affectedRows > 0)
                    return Ok(new { success = true, message = "Complaint updated successfully." });
                else
                    return NotFound(new { success = false, message = "Complaint not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpGet]
        [Route("GetAllDepartment")]
        public async Task<IActionResult> GetAllDepartment()
        {
            return Ok(await obj.GetAllDepartment());
        }


        [HttpGet]
        [Route("GetAllUserDetails")]
        public async Task<IActionResult> GetAllUserDetails()
        {
            var result = await obj.GetAllUserDetails();
            return Ok(result);
        }

        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentModel model)
        {
            var result = await obj.AddDepartment(model);

            return Ok(new
            {
                Success = true,
                Message = result
            });
        }
    }
}
