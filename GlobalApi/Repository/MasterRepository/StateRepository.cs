                                                                                                                                                                                                                      using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class StateRepository : Istate
    {
        private readonly GlobalContext db;
        private static readonly Lazy<StateRepository> instance=new Lazy<StateRepository>(() =>new StateRepository());
        private IPrimarykeyvalue primarykeyvalue;
        public static StateRepository Getinstance { get { return instance.Value; } }
        public StateRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<bool> InsertState(States lead)
        {
            try
            {
                var duplicate = await db.States.FirstOrDefaultAsync(x => x.state_code == lead.state_code 
                && x.state_name == lead.state_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("States");
                    //bool state_exits = db.States.Any(x => x.state_name == lead.state_name);
                    var state_exits = db.States.FirstOrDefaultAsync(x => x.state_name == lead.state_name);

                    if (state_exits.Result == null)
                    {
                        States obj = new States()
                        {
                            stat_id = id,
                            state_code = lead.state_code,
                            state_name = lead.state_name,
                            cntry_id = lead.cntry_id,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.States.AddAsync(obj);
                        await db.SaveChangesAsync();
                        return true;
                    }
                    return false;

                }
                return false;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> UpdateState(States lead)
        {
            try
            {
                var result = await db.States.FirstOrDefaultAsync(x => x.stat_id == lead.stat_id /*&& x.cntry_id == lead.cntry_id*/);
                if (result != null)
                {
                    result.stat_id = lead.stat_id;
                    result.state_name = lead.state_name;
                    result.state_code = lead.state_code;
                    result.cntry_id = lead.cntry_id;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<GetStateCountry>> GetAllState()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.States
                                 join b in db.Countries on a.cntry_id equals b.cntry_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.Status on a.status equals c.sts_id
                                 where a.stat_id != 0
                                 orderby b.cntry_id descending
                                 select new GetStateCountry
                                 {
                                     stat_id = a.stat_id,
                                     state_name = a.state_name,
                                     state_code = a.state_code,
                                     cntry_id = a.cntry_id,
                                     country_name = b.country_name,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = c.sts_name,
                                     Remarks = a.Remarks,
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
        public async Task<List<State_DD>> GetState_DD(int cntry_id)
        {
            if (db != null)
            {
                var query = (from a in db.States
                             where a.cntry_id== cntry_id && a.delete_flag == false 
                             && a.status != 6 && a.stat_id != 0
                             select new State_DD
                             {
                                 stat_id = a.stat_id,
                                 state_code = a.state_code,
                                 state_name = a.state_name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<bool> DeleteState(int stat_id)
        {
            try
            {
                var result = await db.States.FirstOrDefaultAsync(x => x.stat_id == stat_id);

                if (result != null)
                {
                    result.stat_id = stat_id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<StateById> GetStateById(int stat_id)
        {
            if (db != null)
            {
                var query = (from a in db.States
                             join b in db.Countries on a.cntry_id equals b.cntry_id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.Status on a.status equals c.sts_id
                             where a.stat_id == stat_id && a.stat_id != 0
                             select new StateById
                             {
                                 stat_id = a.stat_id,
                                 state_name = a.state_name,
                                 state_code = a.state_code,
                                 cntry_id = a.cntry_id,
                                 country_name = b.country_name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = c.sts_name,
                                 Remarks = a.Remarks,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<bool> ApproveState(ApproveState lead)
        {
            try
            {
                if (lead.stat_id != 0)
                {
                    var result = await db.States.Where(x => x.stat_id == lead.stat_id).FirstOrDefaultAsync();
                    if (result.status != 3)
                    {
                        //result.stat_id = lead.stat_id;
                        result.status = 3;
                        if (lead.Remarks == null)
                        {
                            result.Remarks = "OK";
                        }
                        else
                            result.Remarks = lead.Remarks;
                        await db.SaveChangesAsync();
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}