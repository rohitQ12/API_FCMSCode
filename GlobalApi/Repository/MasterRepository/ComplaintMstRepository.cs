using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class ComplaintMstRepository : IComplaintMst
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public ComplaintMstRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }
        public async Task<ComplaintMst> InsertComplaintMst(ComplaintMst lead)
        {
            try
            {
                var duplicate = await db.ComplaintMst.FirstOrDefaultAsync(x => x.Cmst_Name == lead.Cmst_Name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("ComplaintMst");
                    ComplaintMst obj = new ComplaintMst()
                    {
                        Cmst_Id = id,
                        Cmst_Code = lead.Cmst_Code,
                        Cmst_Name = lead.Cmst_Name,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.ComplaintMst.AddAsync(obj);
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
        public async Task<ComplaintMst> UpdateComplaintMst(ComplaintMst lead)
        {
            try
            {
                var result = await db.ComplaintMst.FirstOrDefaultAsync(x => x.Cmst_Id == lead.Cmst_Id);
                if (result != null)
                {
                    result.Cmst_Id = lead.Cmst_Id;
                    result.Cmst_Code = lead.Cmst_Code;
                    result.Cmst_Name = lead.Cmst_Name;
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
        public async Task<List<ComplaintMst>> GetAllComplaintMst()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.ComplaintMst
                                 orderby a.Cmst_Id descending
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
        public async Task<List<ComplaintMst_DD>> GetComplaintMst_DD()
        {
            if (db != null)
            {
                var query = (from a in db.ComplaintMst
                             where a.delete_flag == false && a.status == 1
                             select new ComplaintMst_DD
                             {
                                 Cmst_Id = a.Cmst_Id,
                                 Cmst_Code = a.Cmst_Code,
                                 Cmst_Name = a.Cmst_Name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<ComplaintMst> DeleteComplaintMst(int Cmst_Id)
        {
            try
            {
                var result = await db.ComplaintMst.FirstOrDefaultAsync(x => x.Cmst_Id == Cmst_Id);
                if (result != null)
                {
                    result.Cmst_Id = Cmst_Id;
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
        public async Task<ComplaintMst> GetComplaintMstById(int Cmst_Id)
        {
            if (db != null)
            {
                var query = (from a in db.ComplaintMst
                             where a.Cmst_Id == Cmst_Id
                             select new ComplaintMst
                             {
                                 Cmst_Id = a.Cmst_Id,
                                 Cmst_Code = a.Cmst_Code,
                                 Cmst_Name = a.Cmst_Name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
