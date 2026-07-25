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
        private IPrimarykeyvalue primarykeyvalue;
        public StateRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertState(States states)
        {
            try
            {
                var statecode= await db.States.FirstOrDefaultAsync(x => x.state_code == states.state_code);
                var statename= await db.States.FirstOrDefaultAsync(x => x.state_name == states.state_name);

                if (statecode != null)
                {
                    return "State Code Already Exists";
                }

                if (statename != null)
                {
                    return "State Name Already Exists";
                }

                int id = await primarykeyvalue.primary_key("States");
                States obj = new States()
                {
                    stat_id = id,
                    state_code = states.state_code,
                    state_name = states.state_name,
                    cntry_id = states.cntry_id,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.States.AddAsync(obj);
                await db.SaveChangesAsync();
                return "State Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateState(States states)
        {
            try
            {
                var State = await db.States.FirstOrDefaultAsync(x => x.stat_id == states.stat_id);
                var statecode = await db.States.FirstOrDefaultAsync(x => x.state_code == states.state_code);
                var statename = await db.States.FirstOrDefaultAsync(x => x.state_name == states.state_name);


                if (statecode != null)
                {
                    if (statecode.state_code != State.state_code)
                    {
                        return "State Code Already Exists";
                    }
                }
                if (statename != null)
                {
                    if (statename.state_name != State.state_name)
                    {
                        return "State Code Already Exists";
                    }
                }

                if (State != null)
                {
                    State.stat_id = states.stat_id;
                    State.state_name = states.state_name;
                    State.state_code = states.state_code;
                    State.cntry_id = states.cntry_id;
                    State.modified_by = 1;
                    State.modified_date = DateTime.Now;
                    State.delete_flag = false;
                    State.status = 2;
                    await db.SaveChangesAsync();
                    return "State Updated Successfully";
                }
                return "State Didn't Exists";
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
                
                    var query = (from a in db.States
                                 join b in db.Countries on a.cntry_id equals b.cntry_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.Status on a.status equals c.sts_id
                                 where a.stat_id != 0 && a.delete_flag == false
                                 orderby a.stat_id descending
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<State_DD>> GetState_DD(int cntry_id)
        {

                var query = (from a in db.States
                             where a.cntry_id == cntry_id && a.delete_flag == false
                             && a.status == 3
                             orderby a.state_name
                             select new State_DD
                             {
                                 stat_id = a.stat_id,
                                 state_code = a.state_code,
                                 state_name = a.state_name,
                             }).ToListAsync();
                return await query;
            
        }
        public async Task<string> DeleteState(int stat_id)
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
                    return "State Deleted Successfully";
                }
                return "State Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<StateById> GetStateById(int stat_id)
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
        public async Task<string> ApproveState(ApproveState approvestate)
        {
            try
            {
                var result = await db.States.Where(x => x.stat_id == approvestate.stat_id).FirstOrDefaultAsync();
                if (result.status != 3)
                {
                    result.status = 3;
                    if (approvestate.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = approvestate.Remarks;
                    await db.SaveChangesAsync();
                    return "State Approved Successfully";
                }
                else
                    return "State Details Does Not Exists";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}