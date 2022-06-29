using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IConsult_Symptoms_DTL
    {
        Task<List<Consult_Symptoms_DTL>> GetExistsConsult_Symptoms_DTL(int CON_Id);
        Task<bool> UpdateConsult_Symptoms_DTL(List<Consult_Symptoms_DTL> lead, int CON_Id);
        Task<List<GetAllCSdtl>> GetAllConsult_Symptoms_DTL();
        Task<List<GetAllCons_Symptoms>> GetAllCons_Symptoms();
        Task<Consult_Symptoms_DTL> DeleteConsult_Symptoms_DTL(int SYM_Id);
        Task<List<CSdtlBy_Id>> GetConsult_Symptoms_DTLById(int CON_Id);
    }
}
