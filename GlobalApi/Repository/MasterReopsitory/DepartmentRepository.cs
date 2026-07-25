using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DepartmentRepository : IDepartment
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DepartmentRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<Department> InsertDepartment(Department lead)
        {
            try
            {
                var duplicate = await db.Department.FirstOrDefaultAsync(x => x.Dept_name == lead.Dept_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Department");
                    Department obj = new Department()
                    {
                        Dept_Id = id,
                        Dept_name = lead.Dept_name,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Department.AddAsync(obj);
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
        public async Task<Department> UpdateDepartment(Department lead)
        {
            try
            {
                var result = await db.Department.FirstOrDefaultAsync(x => x.Dept_Id == lead.Dept_Id);
                if (result != null)
                {
                    result.Dept_Id = lead.Dept_Id;
                    result.Dept_name = lead.Dept_name;
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
        public async Task<List<GetAllDepartment>> GetAllDepartment()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Department
                                 join b in db.Status on a.status equals b.sts_id
                                 orderby a.Dept_Id descending
                                 select new GetAllDepartment
                                 {
                                     Dept_Id = a.Dept_Id,
                                     Dept_name = a.Dept_name,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = b.sts_name,
                                 });
                    return await query.ToListAsync();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<List<Department_DD>> GetDepartment_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Department
                             where a.delete_flag == false && a.status == 3 && a.Dept_Id != 0
                             orderby a.Dept_name
                             select new Department_DD
                             {
                                 Dept_Id = a.Dept_Id,
                                 Dept_name = a.Dept_name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

        public async Task<Department> DeleteDepartment(int Dept_Id)
        {
            try
            {
                var result = await db.Department.FirstOrDefaultAsync(x => x.Dept_Id == Dept_Id);

                if (result != null)
                {
                    result.Dept_Id = Dept_Id;
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

        public async Task<DepartmentById> GetDepartmentById(int Dept_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Department
                             join b in db.Status on a.status equals b.sts_id
                             where a.Dept_Id == Dept_Id
                             select new DepartmentById
                             {
                                 Dept_Id = a.Dept_Id,
                                 Dept_name = a.Dept_name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = b.sts_name,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
