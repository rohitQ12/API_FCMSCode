using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IParameters
    {
        //Task<string> InsertParameters(List<Parameters> lead, int Appt_Id);
        Task<Parameters> UpdateParameters(Parameters lead);
        Task<List<GetAllParameters>> GetAllParameters();
        Task<List<ParametersBy_Id>> GetParametersById(int PA_PR_Id_FK);
        Task<Parameters> DeleteParameters(int PA_Id);
        Task<List<Parameters>> GetExistsParameters(int Appt_Id);
        Task<List<Parameters>> GetExistsPHCParameters(int Appt_Id);

    }
}
