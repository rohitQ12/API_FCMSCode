using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DietPlanRepository : IDietPlan
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DietPlanRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertDietPlan(DietPlan lead)
        {
            try
            {
                var duplicate = await db.DietPlan.FirstOrDefaultAsync(x => x.Dp_Id == lead.Dp_Id);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("DietPlan");
                    DietPlan obj = new DietPlan()
                    {
                        Dp_Id = id,
                        DP_CON_Id_FK = lead.DP_CON_Id_FK,
                        Dp_intake = lead.Dp_intake,
                        Dp_duration = lead.Dp_duration,
                        Dp_dura_interof = lead.Dp_dura_interof,
                        Dp_instruction = lead.Dp_instruction,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        Status = 1
                    };
                    var result = await db.DietPlan.AddAsync(obj);
                    await db.SaveChangesAsync();
                    return "Dietplan inserted successfully";
                }
                return "Dietplan alredy exits";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateDietPlan(DietPlan lead)
        {
            try
            {
                var result = await db.DietPlan.FirstOrDefaultAsync(x => x.Dp_Id == lead.Dp_Id);
                if (result != null)
                {
                    result.Dp_Id = lead.Dp_Id;
                    result.DP_CON_Id_FK = lead.DP_CON_Id_FK;
                    result.Dp_intake = lead.Dp_intake;
                    result.Dp_duration = lead.Dp_duration;
                    result.Dp_dura_interof = lead.Dp_dura_interof;
                    result.Dp_instruction = lead.Dp_instruction;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.Status = 2;
                    await db.SaveChangesAsync();
                    return "DietPlan updated successfully";
                }
                return "Dietplan does not exits";
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
                                 join b in db.Status on a.Status equals b.sts_id
                                 orderby a.Dp_Id descending
                                 select new GetAllDietPlan
                                 {
                                     Dp_Id = a.Dp_Id,
                                     DP_CON_Id_FK = a.DP_CON_Id_FK,
                                     Dp_intake = a.Dp_intake,
                                     Dp_duration = a.Dp_duration,
                                     Dp_dura_interof = a.Dp_dura_interof,
                                     Dp_instruction = a.Dp_instruction,
                                     Status_name = b.sts_name,
                                     delete_flag = a.delete_flag,
                                     Status = a.Status
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
        public async Task<string> DeleteDietPlan(int Id)
        {
            try
            {
                var result = await db.DietPlan.FirstOrDefaultAsync(x => x.Dp_Id == Id);
                if (result != null)
                {
                    result.Dp_Id = Id;
                    result.delete_flag = true;
                    result.Status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "DietPlan deleted successfully";
                }
                return "Dietplan does not exits";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<GetAllDietPlan>> GetDietPlanById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.DietPlan
                             join b in db.Status on a.Status equals b.sts_id
                             where a.DP_CON_Id_FK == Id
                             orderby a.Dp_Id descending
                             select new GetAllDietPlan
                             {
                                 Dp_Id = a.Dp_Id,
                                 DP_CON_Id_FK = a.DP_CON_Id_FK,
                                 Dp_intake = a.Dp_intake,
                                 Dp_duration = a.Dp_duration,
                                 Dp_dura_interof = a.Dp_dura_interof,
                                 Dp_instruction = a.Dp_instruction,
                                 Status_name = b.sts_name,
                                 delete_flag = a.delete_flag,
                                 Status = a.Status
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
