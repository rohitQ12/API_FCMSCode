using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DietPlanRepository : IDietPlan
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public DietPlanRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }
        public async Task<DietPlan> InsertDietPlan(DietPlan lead)
        {
            try
            {
                var duplicate = await db.DietPlan.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("DietPlan");
                    DietPlan obj = new DietPlan()
                    {
                        Id = id,
                        DP_CON_Id_FK = lead.DP_CON_Id_FK,
                        BreakFast = lead.BreakFast,
                        Lunch = lead.Lunch,
                        Dinner = lead.Dinner,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.DietPlan.AddAsync(obj);
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
        public async Task<DietPlan> UpdateDietPlan(DietPlan lead)
        {
            try
            {
                var result = await db.DietPlan.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.DP_CON_Id_FK = lead.DP_CON_Id_FK;
                    result.BreakFast = lead.BreakFast;
                    result.Lunch = lead.Lunch;
                    result.Dinner = lead.Dinner;
                    result.modified_by = 1;
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
        public async Task<List<GetAllDietPlan>> GetAllDietPlan()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DietPlan
                                 join b in db.Consultation on a.DP_CON_Id_FK equals b.CON_Id
                                 join c in db.Patient on b.CON_PR_Id_FK equals c.PR_Id
                                 orderby a.Id descending
                                 select new GetAllDietPlan
                                 {
                                     Id = a.Id,
                                     DP_CON_Id_FK = a.DP_CON_Id_FK,
                                     DP_CON_PR_ID_FK = b.CON_PR_Id_FK,
                                     DP_CON_PR_Name = string.Concat(c.PR_FirstName, c.PR_LastName),
                                     DP_CON_Type = b.CON_Type,
                                     BreakFast = a.BreakFast,
                                     Lunch = a.Lunch,
                                     Dinner = a.Dinner,
                                     delete_flag = a.delete_flag,
                                     status = a.status
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
        public async Task<DietPlan> DeleteDietPlan(int Id)
        {
            try
            {
                var result = await db.DietPlan.FirstOrDefaultAsync(x => x.Id == Id);
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
        public async Task<GetById> GetDietPlanById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.DietPlan
                             join b in db.Consultation on a.DP_CON_Id_FK equals b.CON_Id
                             join c in db.Patient on b.CON_PR_Id_FK equals c.PR_Id
                             where a.Id == Id
                             select new GetById
                             {
                                 Id = a.Id,
                                 DP_CON_Id_FK = a.DP_CON_Id_FK,
                                 DP_CON_PR_ID_FK = b.CON_PR_Id_FK,
                                 DP_CON_PR_Name = string.Concat(c.PR_FirstName, c.PR_LastName),
                                 DP_CON_Type = b.CON_Type,
                                 BreakFast = a.BreakFast,
                                 Lunch = a.Lunch,
                                 Dinner = a.Dinner,
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
