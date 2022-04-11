using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ILabTesting
    {
        Task<LabTesting> InsertLabTesting(LabTesting_Details lead);
        Task<bool> AcceptLabTesting(int Id, int Tst_CON_Id_FK, bool AcceptLabTest);
        Task<LabTesting> UpdateLabTesting(LabTesting lead);
        Task<List<GetLabTestings>> GetAllLabTesting();
        Task<LabTestingsById> GetLabTestingById(int Id);
        Task<LabTesting> DeleteLabTesting(int Id);

    }
}
