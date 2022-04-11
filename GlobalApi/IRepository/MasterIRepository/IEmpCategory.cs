using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IEmpCategory
    {
        Task<Emp_Category> InsertEmpCategory(Emp_Category lead);
        Task<Emp_Category> UpdateEmpCategory(Emp_Category lead);
        Task<List<Emp_Category>> GetAllEmpCategory();
        Task<List<Emp_Category_DD>> GetEmpCategory_DD();
        Task<Emp_CategoryById> GetEmpCategoryById(int emp_cat_id);
        Task<Emp_Category> DeleteEmpCategory(int emp_cat_id);

    }
}
