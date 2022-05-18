using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiagnoCategory
    {
        Task<DiagnoCategory> InsertDiagnoCategory(DiagnoCategory lead);
        Task<DiagnoCategory> UpdateDiagnoCategory(DiagnoCategory lead);
        Task<List<DiagnoCategory>> GetAllDiagnoCategory();
        Task<List<Diagno_DD>> GetDiagnoCategory_DD();
        //Task<DiagnoCategoryBy_Id> GetDiagnoCategoryById(int Id);
        Task<DiagnoCategory> DeleteDiagnoCategory(int Id);

    }
}
