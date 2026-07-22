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
        public async Task<string> InsertNetwork(Network Network)
        {
            try
            {
                var Netwrk_name = await db.Network.FirstOrDefaultAsync(x => x.NE_Description == Network.NE_Description);
                var Netwrk_code = await db.Network.FirstOrDefaultAsync(x => x.NE_Code == Network.NE_Code);
                if (Netwrk_code != null)
                {
                    return "Network Code Already Exists";
                }
                if (Netwrk_name != null)
                {
                    return "Network Name Already Exists";
                }
                int id = await primarykeyvalue.primary_key("Network");
                Network obj = new Network()
                {
                    NE_Id = id,
                    NE_Code = Network.NE_Code,
                    NE_Description = Network.NE_Description,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Network.AddAsync(obj);
                await InsertUsers(obj);
                await db.SaveChangesAsync();
                return "Network Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<UsersLists> InsertUsers(Network Network)
        {
            int _id = await primarykeyvalue.primary_key("UsersLists");
            UsersLists insert = new UsersLists()
            {
                Id = _id,
                User_cat = "Network",
                User_ref_id = Network.NE_Id,
                created_by = 1,
                created_date = DateTime.Now,
                delete_flag = false,
                status = 1,

            };
            var _new = await db.UsersLists.AddAsync(insert);
            await db.SaveChangesAsync();
            return _new.Entity;

        }
        public async Task<string> UpdateNetwork(Network Network)
        {
            try
            {
                var result = await db.Network.FirstOrDefaultAsync(x => x.NE_Id == Network.NE_Id);
                var Netwrk_name = await db.Network.FirstOrDefaultAsync(x => x.NE_Description == Network.NE_Description);
                var Netwrk_code = await db.Network.FirstOrDefaultAsync(x => x.NE_Code == Network.NE_Code);

                if (Netwrk_code != null)
                {
                    if (Netwrk_code.NE_Code != result.NE_Code)
                    {
                        return "Network Code Already Exists";
                    }
                }
                if (Netwrk_name != null)
                {
                    if (Netwrk_name.NE_Description != result.NE_Description)
                    {
                        return "Network Name Already Exists";
                    }
                }
                if (result != null)
                {
                    result.NE_Id = Network.NE_Id;
                    result.NE_Code = Network.NE_Code;
                    result.NE_Description = Network.NE_Description;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    return "Network Updated Successfully";
                }
                return "Network Doesn't Exists";
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
                var query = (from a in db.Network
                             join b in db.Status on a.status equals b.sts_id
                             where a.NE_Id != 0 && a.delete_flag == false && a.status == 3
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<Network_DD>> GetNetwork_DD()
        {
            var query = (from a in db.Network
                         where a.delete_flag == false && a.status == 3
                         && a.NE_Id != 0
                         orderby a.NE_Description
                         select new Network_DD
                         {
                             NE_Id = a.NE_Id,
                             NE_Code = a.NE_Code,
                             NE_Description = a.NE_Description
                         }).ToListAsync();
            return await query;
        }
        public async Task<string> DeleteNetwork(int NE_Id)
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
                    return "Network Deleted Successfully";
                }
                return "Network Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<NetworkById> GetNetworkById(int NE_Id)
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
        public async Task<string> ApproveNetwork(ApproveNetwork ApproveNetwork)
        {
            try
            {
                var result = await db.Network.Where(x => x.NE_Id == ApproveNetwork.NE_Id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.status = 3;
                    if (ApproveNetwork.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = ApproveNetwork.Remarks;
                    await db.SaveChangesAsync();
                    return "Network Approved Successfully";
                }
                return "Network Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
