using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IAssistant
    {
        Task<Assistant> InsertAssistant(Assistant_Images lead);
        Task<Assistant> UpdateAssistant(Assistant_Images lead);
        Task<List<GetAllAssistant>> GetAllAssistant();
        Task<List<Assistant_DD>> GetAssistant_DD();
        Task<AssistantById> GetAssistantById(int Assistant_id);
        Task<Assistant> DeleteAssistant(int Assistant_id);

    }
}
