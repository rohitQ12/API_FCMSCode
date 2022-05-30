using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class EmpCategoryRepository : IEmpCategory
    {

        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public EmpCategoryRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Emp_Category> InsertEmpCategory(Emp_Category lead)
        {
            try
            {
                var duplicate = await db.Emp_Category.FirstOrDefaultAsync(x => x.emp_cat_name == lead.emp_cat_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Emp_Category");
                    Emp_Category obj = new Emp_Category()
                    {
                        emp_cat_id = id,
                        emp_cat_name = lead.emp_cat_name,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Emp_Category.AddAsync(obj);
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
        public async Task<Emp_Category> UpdateEmpCategory(Emp_Category lead)
        {
            try
            {
                var result = await db.Emp_Category.FirstOrDefaultAsync(x => x.emp_cat_id == lead.emp_cat_id);
                if (result != null)
                {
                    result.emp_cat_id = lead.emp_cat_id;
                    result.emp_cat_name = lead.emp_cat_name;
                    result.modified_by = 1;
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
        public async Task<List<Emp_Category>> GetAllEmpCategory()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Emp_Category
                                 orderby a.emp_cat_id descending
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
        public async Task<List<Emp_Category_DD>> GetEmpCategory_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Emp_Category
                             where a.delete_flag == false && a.status != 6 && a.emp_cat_id != 0
                             select new Emp_Category_DD
                             {
                                 emp_cat_id = a.emp_cat_id,
                                 emp_cat_name = a.emp_cat_name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Emp_Category> DeleteEmpCategory(int emp_cat_id)
        {
            try
            {
                var result = await db.Emp_Category.FirstOrDefaultAsync(x => x.emp_cat_id == emp_cat_id);
                if (result != null)
                {
                    result.emp_cat_id = emp_cat_id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
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
        public async Task<Emp_CategoryById> GetEmpCategoryById(int emp_cat_id)
        {
            if (db != null)
            {
                var query = (from a in db.Emp_Category
                             where a.emp_cat_id == emp_cat_id
                             select new Emp_CategoryById
                             {
                                 emp_cat_id = a.emp_cat_id,
                                 emp_cat_name = a.emp_cat_name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
