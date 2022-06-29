using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IConsult_AllergySigns_DTL
    {
        Task<bool> UpdateConsult_AllergySigns_DTL(List<Consult_AllergySigns_DTL> lead, int CON_Id);
        Task<List<GetAllCASdtl>> GetAllConsult_AllergySigns_DTL();
        Task<List<GetAllCons_Allergys>> GetAllCons_Allergys();
        Task<List<Consult_AllergySigns_DTL>> GetExistsAllergySigns(int CON_Id);
        Task<Consult_AllergySigns_DTL> DeleteConsult_AllergySigns_DTL(int Ddtl_Id);
        Task<List<GetCASdtlById>> GetConsult_AllergySigns_DTLById(int CON_Id);
    }
}
