using GlobalApi.JsonFile;
using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIReopsitory
{
    public interface Ifeedback
    {
        Task<List<GetAllfeedback>> GetFeedback();
        Task<feedback> updateFeedback(int lead);
    }
}
