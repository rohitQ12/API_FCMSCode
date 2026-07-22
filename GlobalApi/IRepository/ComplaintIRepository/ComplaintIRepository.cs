//namespace GlobalApi.IRepository.ComplaintIRepository
//{
//    public class ComplaintIRepository
//    {
//    }
//}


using GlobalApi.Models.ComplaintModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalApi.IRepository.ComplaintIRepository
{
    public interface ComplaintIRepository
    {
        Task<string> InsertComplaint(ComplaintInsertModel model);
        Task<List<ComplaintDetailsModels>> GetAllComplaintDetails();
        Task<ComplaintDetailsModel> GetComplaintDetailsById(string CP_RefNo); 
        Task<UserRoleDto> CheckUserTypeAdmin(string userName);
        Task<UserRoleDto> CheckUserTypeCustomer(string userName);
        Task<List<ComplaintDetailsModels>> GetComplaintDetailsBy(string UserName);
        Task<List<StatusModels>> GetAllStatus();
        Task<List<PriorityModels>> GetAllPriority();
        //Task<int> UpdateComplaintStatusPriority(UpdateComplaintStatusPriorityModel model);
        Task<int> UpdateComplaintStatusPriority(UpdateComplaintStatusPriorityModel model);
        Task<List<DepartmentModels>> GetAllDepartment();
        Task<List<UserDetailsModel>> GetAllUserDetails();
        Task<string> AddDepartment(DepartmentModel model);
    }
}