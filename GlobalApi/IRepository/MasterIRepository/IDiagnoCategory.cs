using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDiagnoCategory
    {
        Task<DiagnoCategory> InsertDiagnoCategory(DiagnoCategory lead);
        Task<DiagnoCategory> UpdateDiagnoCategory(DiagnoCategory lead);
        Task<List<GetAllDiagnoCat>> GetAllDiagnoCategory();
        Task<List<Diagno_DD>> GetDiagnoCategory_DD(int Type_Id);
        //Task<DiagnoCategoryBy_Id> GetDiagnoCategoryById(int Id);
        Task<DiagnoCategory> DeleteDiagnoCategory(int Id);

    }
}
