using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICategory
    {
        Task<Category> InsertCategory(Category lead);
        Task<Category> UpdateCategory(Category lead);
        Task<List<GetAllCat>> GetAllCategory();
        Task<List<Cat_DD>> GetCategory_DD();
        //Task<CategoryBy_Id> GetCategoryById(int Id);
        Task<Category> DeleteCategory(int Id);

    }
}
