using GlobalApi.Models.ComplaintModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalApi.IRepository.ComplaintIRepository
{
    public interface CategoriesIRepository
    {
        Task<List<CategoriesModels>> GetAllCategories();
        Task<List<SubCategoriesModels>> GetSubCategoriesByCategoryId(int CategoryId);
    }
}
