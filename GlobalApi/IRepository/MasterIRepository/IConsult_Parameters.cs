using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IConsult_Parameters
    {
        Task<Consult_Parameters> UpdateConsult_Parameters(Consult_Parameters lead);
        Task<List<GetAllCPara>> GetAllConsult_Parameters();
        Task<List<CParaBy_Id>> GetConsult_ParametersById(int CON_Id);
        Task<Consult_Parameters> DeleteConsult_Parameters(int CON_Id);

    }
}
