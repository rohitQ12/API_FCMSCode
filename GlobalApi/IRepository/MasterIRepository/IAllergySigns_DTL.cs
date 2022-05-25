using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IAllergySigns_DTL
    {
        Task<string> InsertAllergySigns_DTL(List<AllergySigns_DTL> lead, int Appt_Id);
        Task<string> InsertManualAllergySigns_DTL(List<AllergySigns_DTL> lead, int MAppt_Id);
        Task<bool> UpdateAllergySigns_DTLtest(List<AllergySigns_DTL> lead, int Appt_Id);
        Task<bool> UpdateManualAllergySigns_DTL(List<AllergySigns_DTL> lead, int MAppt_Id);
        Task<List<GetAllAllergySigns_DTL>> GetAllAllergySigns_DTL();
        Task<List<GetAllAllergySigns_DTL>> GetAllManualAllergySigns_DTL();
        Task<List<GetAllergySigns_DTLById>> GetAllergySigns_DTLById(int Ddtl_PR_Id_FK);
        Task<AllergySigns_DTL> DeleteAllergySigns_DTL(int Ddtl_Id);

    }
}
