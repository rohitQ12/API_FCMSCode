using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IParameters
    {
        //Task<string> InsertParameters(List<Parameters> lead, int Appt_Id);
        Task<Parameters> UpdateParameters(Parameters lead);
        Task<List<GetAllParameters>> GetAllParameters();
        Task<ParametersBy_Id> GetParametersById(int PA_Id);
        Task<Parameters> DeleteParameters(int PA_Id);

    }
}
