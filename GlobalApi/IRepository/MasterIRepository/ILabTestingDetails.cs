using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ILabTestingDetails
    {
        Task<string> InsertLabTestingDetails(List<LabTestingDetails> lead, int LT_Id_FK);
        Task<LabTestingDetails> UpdateLabTestingDetails(TestReport lead);
        Task<List<GetLabTestingDetails>> GetAllLabTestingDetails();
        Task<LabTestingDetailsById> GetLabTestingDetailsById(int Id);
        Task<LabTestingDetails> DeleteLabTestingDetails(int Id);

    }
}
