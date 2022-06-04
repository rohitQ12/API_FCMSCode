using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class NetworkRepository : INetwork
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public NetworkRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Network> InsertNetwork(Network lead)
        {
            try
            {
                var duplicate = await db.Network.FirstOrDefaultAsync(x => x.NE_Code == lead.NE_Code && x.NE_Description == lead.NE_Description);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Network");
                    Network obj = new Network()
                    {
                        NE_Id = id,
                        NE_Code = lead.NE_Code,
                        NE_Description = lead.NE_Description,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Network.AddAsync(obj);
                    await InsertUsers(obj);
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
        public async Task<UsersLists> InsertUsers(Network lead)
        {
            int _id = await primarykeyvalue.primary_key("UsersLists");
            UsersLists insert = new UsersLists()
            {
                Id = _id,
                User_cat = "Network",
                User_ref_id = lead.NE_Id,
                created_by = 1,
                created_date = DateTime.Now,
                delete_flag = false,
                status = 1,

            };
            var _new = await db.UsersLists.AddAsync(insert);
            await db.SaveChangesAsync();
            return _new.Entity;

        }
        public async Task<Network> UpdateNetwork(Network lead)
        {
            try
            {
                var result = await db.Network.FirstOrDefaultAsync(x => x.NE_Id == lead.NE_Id);
                if (result != null)
                {
                    result.NE_Id = lead.NE_Id;
                    result.NE_Code = lead.NE_Code;
                    result.NE_Description = lead.NE_Description;
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
        public async Task<List<GetAllNetwork>> GetAllNetwork()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Network
                                 join b in db.Status on a.status equals b.sts_id
                                 where a.NE_Id != 0
                                 orderby a.NE_Id descending
                                 select new GetAllNetwork
                                 {
                                     NE_Id = a.NE_Id,
                                     NE_Code = a.NE_Code,
                                     NE_Description = a.NE_Description,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = b.sts_name,
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
        public async Task<List<Network_DD>> GetNetwork_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Network
                             where a.delete_flag == false && a.status == 3
                             && a.NE_Id != 0
                             select new Network_DD
                             {
                                 NE_Id = a.NE_Id,
                                 NE_Code = a.NE_Code,
                                 NE_Description = a.NE_Description
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Network> DeleteNetwork(int NE_Id)
        {
            try
            {
                var result = await db.Network.FirstOrDefaultAsync(x => x.NE_Id == NE_Id);
                if (result != null)
                {
                    result.NE_Id = NE_Id;
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
        public async Task<NetworkById> GetNetworkById(int NE_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Network
                             join b in db.Status on a.status equals b.sts_id
                             where a.NE_Id == NE_Id && a.NE_Id != 0
                             select new NetworkById
                             {
                                 NE_Id = a.NE_Id,
                                 NE_Code = a.NE_Code,
                                 NE_Description = a.NE_Description,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = b.sts_name,
                                 Remarks = a.Remarks,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> ApproveNetwork(ApproveNetwork lead)
        {
            try
            {
                if(lead.NE_Id != 0)
                {
                    var result = await db.Network.Where(x => x.NE_Id == lead.NE_Id).FirstOrDefaultAsync();
                    if (result.status != 3)
                    {
                        //result.NE_Id = NE_Id;
                        result.status = 3;
                        if (lead.Remarks == null)
                        {
                            result.Remarks = "OK";
                        }
                        else
                            result.Remarks = lead.Remarks;
                        await db.SaveChangesAsync();
                        return "Network is Approved";
                    }
                    else
                        return "Already Active";
                }
                else
                    return "Cannot Approve Default Network";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
