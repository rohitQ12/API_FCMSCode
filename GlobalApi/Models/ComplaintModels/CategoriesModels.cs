namespace GlobalApi.Models.ComplaintModels
{
    public class CategoriesModels
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class SubCategoriesModels
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId_fk { get; set; }
        public bool IsActive { get; set; }
    }

}
