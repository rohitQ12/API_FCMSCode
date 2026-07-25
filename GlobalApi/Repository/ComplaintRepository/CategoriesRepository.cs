using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.ComplaintIRepository;
using GlobalApi.Models.ComplaintModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GlobalApi.Repository.ComplaintRepository
{
    public class CategoriesRepository : CategoriesIRepository
    {
        SqlConnection con;

       
        private ADO_Configrations ado_Configurations = new ADO_Configrations();

        public async Task<List<CategoriesModels>> GetAllCategories()
        {
            con = ado_Configurations.connection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetAllCategories";

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();

            await con.OpenAsync();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            con.Close();

            List<CategoriesModels> categoryList = new List<CategoriesModels>();

            categoryList = (from DataRow drr in dt.Rows
                            select new CategoriesModels()
                            {
                                CategoryId = (int)drr["CategoryId"],
                                CategoryName = (string)drr["CategoryName"],
                                IsActive = (bool)drr["IsActive"],
                                DisplayOrder = (int)drr["DisplayOrder"]
                            }).ToList();

            return categoryList;
        }

        public async Task<List<SubCategoriesModels>> GetSubCategoriesByCategoryId(int CategoryId)
        {
            con = ado_Configurations.connection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetSubCategoriesByCategoryId";

            // ✅ Pass Parameter
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();

            await con.OpenAsync();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            con.Close();

            List<SubCategoriesModels> subCategoryList = new List<SubCategoriesModels>();

            subCategoryList = (from DataRow drr in dt.Rows
                               select new SubCategoriesModels()
                               {
                                   SubCategoryId = (int)drr["SubCategoryId"],
                                   SubCategoryName = (string)drr["SubCategoryName"],
                                   CategoryId_fk = (int)drr["CategoryId_fk"],
                                   IsActive = (bool)drr["IsActive"]
                               }).ToList();

            return subCategoryList;
        }

    }
}