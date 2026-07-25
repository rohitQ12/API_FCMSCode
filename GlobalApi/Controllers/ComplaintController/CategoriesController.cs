using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.IRepository.ComplaintIRepository;
using GlobalApi.Models.ComplaintModels;
using GlobalApi.Repository.ComplaintRepository;

namespace GlobalApi.Controllers.ComplaintController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        CategoriesRepository obj = new CategoriesRepository();

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await obj.GetAllCategories());
        }

        [HttpGet]
        [Route("GetSubCategoriesByCategoryId/{CategoryId}")]
        public async Task<IActionResult> GetSubCategoriesByCategoryId(int CategoryId)
        {
            return Ok(await obj.GetSubCategoriesByCategoryId(CategoryId));
        }
    }
}