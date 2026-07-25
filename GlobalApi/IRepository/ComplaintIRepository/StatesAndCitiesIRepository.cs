using GlobalApi.Models.ComplaintModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalApi.IRepository.StatesAndCitiesIRepository
{
    public interface StatesAndCitiesIRepository
    {
        Task<List<StatesModels>> GetAllStates();
        Task<List<CitiesModels>> GetCitiesbByState_id(int state_id);
    }
}