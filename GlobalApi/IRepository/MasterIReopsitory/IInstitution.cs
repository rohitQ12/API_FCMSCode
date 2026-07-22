using GlobalApi.JsonFile;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IInstitution
    {
        Task<UserRegisterResult> InsertInstitution(Institution_Images lead);
        Task<UserRegisterResult> UpdateInstitution(Institution_Images lead);
        Task<List<GetAllInstitution>> GetAllInstitution();
        Task<List<Institution_DD>> GetInstitution_DD();
        Task<UserRegisterResult> DeleteInstitution(int Ins_Id);
        Task<InstitutionById> GetInstitutionById(int? Ins_Id);
        Task<UserRegisterResult> ApproveInstitution(ApproveIns lead);
        Task<List<GetAllInstitutionIds>> GetAllInstitutionIds();
    }
}
