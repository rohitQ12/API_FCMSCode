using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IConsult_Diseases_DTL
    {
        Task<bool> UpdateConsult_Diseases_DTL(List<Consult_Diseases_DTL> lead, int CON_Id);
        Task<List<GetAllCDDtl>> GetAllConsult_Diseases_DTL();
        Task<List<GetAllCons_Diseases>> GetAllCons_Diseases();
        Task<List<Consult_Diseases_DTL>> GetExistsConsult_Diseases_DTL(int CON_Id);
        Task<Consult_Diseases_DTL> DeleteConsult_Diseases_DTL(int Ddtl_Id);
        Task<List<GetCDDtlById>> GetConsult_Diseases_DTLById(int CON_Id);
    }
}
