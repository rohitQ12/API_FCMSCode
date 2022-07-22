using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IAssistant
    {
        Task<string> InsertAssistant(Assistant_Images lead, string UserId);
        Task<string> UpdateAssistant(Assistant_Images lead);
        Task<List<GetAllAssistant>> GetAllAssistant(int? Assi_Hos_Id_FK, string roleaction);
        Task<List<Assistant_DD>> GetAssistant_DD(int? Assi_Hos_Id_FK, string roleaction);
        Task<AssistantById> GetAssistantById(int Assi_Id, string roleaction);
        Task<string> DeleteAssistant(int Assistant_id);
        Task<string> ApproveAssistant(ApproveAssistant approveAssistant);

    }
}
