using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IConsult_LabTest
    {
        Task<Consult_LabTest> InsertConsult_LabTest(Consult_LabTest lead);
        Task<Consult_LabTest> UpdateConsult_LabTest(Consult_LabTest lead);
        Task<List<GetConsult_LabTest>> GetAllConsult_LabTest();
        Task<GetConsult_LabTest> GetConsult_LabTestById(int Id);
        Task<List<GetConsult_LabTest>> GetConsult_LabTestByCON_Id(int CON_Id);
        Task<Consult_LabTest> DeleteConsult_LabTest(int Id);

    }
}
