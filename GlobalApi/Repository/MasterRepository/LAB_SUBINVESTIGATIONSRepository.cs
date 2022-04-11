using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class LAB_SUBINVESTIGATIONSRepository : ILAB_SUBINVESTIGATIONS
    {
        GlobalContext  db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public LAB_SUBINVESTIGATIONSRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }
        public async Task<LAB_SUBINVESTIGATIONS> InsertLAB_SUBINVESTIGATIONS(LAB_SUBINVESTIGATIONS lead)
        {
            try
            {
                var duplicate = await db.LAB_SUBINVESTIGATIONS.FirstOrDefaultAsync(x => x.Sub_Category == lead.Sub_Category);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("LAB_SUBINVESTIGATIONS");
                    LAB_SUBINVESTIGATIONS obj = new LAB_SUBINVESTIGATIONS()
                    {
                        Id = id,
                        Lab_Invt_Id_FK = lead.Lab_Invt_Id_FK,
                        Sub_Category = lead.Sub_Category,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.LAB_SUBINVESTIGATIONS.AddAsync(obj);
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
        public async Task<LAB_SUBINVESTIGATIONS> UpdateLAB_SUBINVESTIGATIONS(LAB_SUBINVESTIGATIONS lead)
        {
            try
            {
                var result = await db.LAB_SUBINVESTIGATIONS.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.Lab_Invt_Id_FK = lead.Lab_Invt_Id_FK;
                    result.Sub_Category = lead.Sub_Category;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 1;
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
        public async Task<List<GetLabSubInsv>> GetLAB_SUBINVESTIGATIONS()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.LAB_SUBINVESTIGATIONS
                                 join b in db.LAB_INVESTIGATIONS on a.Lab_Invt_Id_FK equals b.Id
                                 orderby a.Id descending
                                 select new GetLabSubInsv
                                 {
                                     Id = a.Id,
                                     Lab_Invt_Id_FK = a.Lab_Invt_Id_FK,
                                     Category = b.Category,
                                     Sub_Category = a.Sub_Category,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
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
        public async Task<List<LabSubInsv_DD>> GetLabSubInsv_DD()
        {
            if (db != null)
            {
                var query = (from a in db.LAB_SUBINVESTIGATIONS
                             where a.delete_flag == false && a.status == 1
                             select new LabSubInsv_DD
                             {
                                 Id = a.Id,
                                 Sub_Category = a.Sub_Category,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<LAB_SUBINVESTIGATIONS> DeleteLAB_SUBINVESTIGATIONS(int Id)
        {
            try
            {
                var result = await db.LAB_SUBINVESTIGATIONS.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
                    result.delete_flag = true;
                    result.status = 0;
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
        public async Task<LabSubInsvBy_Id> GetLabSubInsvBy_Id(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.LAB_SUBINVESTIGATIONS
                             join b in db.LAB_INVESTIGATIONS on a.Lab_Invt_Id_FK equals b.Id
                             where a.Id == Id
                             select new LabSubInsvBy_Id
                             {
                                 Id = a.Id,
                                 Lab_Invt_Id_FK = a.Lab_Invt_Id_FK,
                                 Category = b.Category,
                                 Sub_Category = a.Sub_Category,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
