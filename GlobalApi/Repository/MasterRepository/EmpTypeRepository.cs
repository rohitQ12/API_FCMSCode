using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class EmpTypeRepository : IEmpType
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public EmpTypeRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Emp_Type> InsertEmpType(Emp_Type lead)
        {
            try
            {
                var duplicate = await db.Emp_Type.FirstOrDefaultAsync(x => x.emptype_name == lead.emptype_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Emp_Type");
                    Emp_Type obj = new Emp_Type()
                    {
                        emptype_id = id,
                        emptype_name = lead.emptype_name,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Emp_Type.AddAsync(obj);
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
        public async Task<Emp_Type> UpdateEmpType(Emp_Type lead)
        {
            try
            {
                var result = await db.Emp_Type.FirstOrDefaultAsync(x => x.emptype_id == lead.emptype_id);
                if (result != null)
                {
                    result.emptype_id = lead.emptype_id;
                    result.emptype_name = lead.emptype_name;
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
        public async Task<List<Emp_Type>> GetAllEmpType()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Emp_Type
                                 orderby a.emptype_id descending
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
        public async Task<List<Emp_Type_DD>> GetEmpType_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Emp_Type
                             where a.delete_flag == false && a.status == 1
                             select new Emp_Type_DD
                             {
                                 emptype_id = a.emptype_id,
                                 emptype_name = a.emptype_name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Emp_Type> DeleteEmpType(int emptype_id)
        {
            try
            {
                var result = await db.Emp_Type.FirstOrDefaultAsync(x => x.emptype_id == emptype_id);
                if (result != null)
                {
                    result.emptype_id = emptype_id;
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
        public async Task<Emp_TypeById> GetEmpTypeById(int emptype_id)
        {
            if (db != null)
            {
                var query = (from a in db.Emp_Type
                             where a.emptype_id == emptype_id
                             select new Emp_TypeById
                             {
                                 emptype_id = a.emptype_id,
                                 emptype_name = a.emptype_name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
