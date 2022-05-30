using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class CategoryRepository : ICategory
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public CategoryRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Category> InsertCategory(Category lead)
        {
            try
            {
                var duplicate = await db.Category.FirstOrDefaultAsync(x => x.name == lead.name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Category");
                    Category obj = new Category()
                    {
                        id = id,
                        name = lead.name,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Category.AddAsync(obj);
                    await db.SaveChangesAsync();
                    return result.Entity;

                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Category> UpdateCategory(Category lead)
        {
            try
            {
                var result = await db.Category.FirstOrDefaultAsync(x => x.id == lead.id);
                if (result != null)
                {
                    result.id = lead.id;
                    result.name = lead.name;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<Category>> GetAllCategory()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Category
                                 orderby a.id descending
                                 select a);
                    return await query.ToListAsync();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<Cat_DD>> GetCategory_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Category
                             where a.delete_flag == false && a.status != 1 && a.id != 0
                             select new Cat_DD
                             {
                                 id = a.id,
                                 name = a.name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Category> DeleteCategory(int Id)
        {
            try
            {
                var result = await db.Category.FirstOrDefaultAsync(x => x.id == Id);
                if (result != null)
                {
                    result.id = Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 3;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //public async Task<CategoryBy_Id> GetCategoryById(int Id)
        //{
        //    if (db != null)
        //    {
        //        var query = (from a in db.Category
        //                     where a.Id == Id
        //                     select new CategoryBy_Id
        //                     {
        //                         Id = a.Id,
        //                         Category_Code = a.Category_Code,
        //                         Category_Name = a.Category_Name,
        //                         Acronyms = a.Acronyms,
        //                         Dis_SP_Id_FK = a.Dis_SP_Id_FK,
        //                         delete_flag = a.delete_flag,
        //                         status = a.status
        //                     }).FirstOrDefaultAsync();
        //        return await query;
        //    }
        //    return null;
        //}

    }
}
