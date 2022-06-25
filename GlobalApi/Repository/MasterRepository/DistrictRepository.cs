using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DistrictRepository : IDistrict
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DistrictRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<bool> InsertDistrict(Districts lead)
        {
            try
            {
                var duplicate = await db.Districts.FirstOrDefaultAsync(x => x.district_code == lead.district_code && x.district_name == lead.district_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Districts");
                    Districts obj = new Districts()
                    {
                        district_id = id,
                        //district_code = "DI-" + Convert.ToString(id),
                        district_code = lead.district_code,
                        district_name = lead.district_name,
                        cntry_id = lead.cntry_id,
                        stat_id = lead.stat_id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Districts.AddAsync(obj);
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
        public async Task<bool> UpdateDistrict(Districts lead)
        {
            try
            {
                var result = await db.Districts.FirstOrDefaultAsync(x => x.district_id == lead.district_id);
                if (result != null)
                {
                    result.district_id = lead.district_id;
                    result.district_name = lead.district_name;
                    result.district_code = lead.district_code;
                    result.cntry_id = lead.cntry_id;
                    result.stat_id = lead.stat_id;
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
        public async Task<List<District_DD>> GetDistrict_DD(int stat_id)
        {
            if (db != null)
            {
                var query = (from a in db.Districts
                             where a.stat_id == stat_id && a.delete_flag == false
                             && a.status == 3 && a.district_id != 0 
                             select new District_DD
                             {
                                 district_id = a.district_id,
                                 district_code = a.district_code,
                                 district_name = a.district_name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<bool> DeleteDistrict(int district_id)
        {
            try
            {
                var result = await db.Districts.FirstOrDefaultAsync(x => x.district_id == district_id);
                if (result != null)
                {
                    result.district_id = district_id;
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
        public async Task<DistrictById> GetDistrictById(int district_id)
        {
            if (db != null)
            {
                var query = (from a in db.Districts
                             join b in db.Status on a.status equals b.sts_id
                             where a.district_id == district_id && a.district_id != 0
                             select new DistrictById
                             {
                                 district_id = a.district_id,
                                 district_name = a.district_name,
                                 district_code = a.district_code,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = b.sts_name,
                                 Remarks = a.Remarks,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<GetDistrictState>> GetAllDistrict()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Districts
                                 join ab in db.Countries on a.cntry_id equals ab.cntry_id into ablist
                                 from ab in ablist.DefaultIfEmpty()
                                 join b in db.States on a.stat_id equals b.stat_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.Status on a.status equals c.sts_id
                                 where a.district_id != 0
                                 orderby a.district_id descending
                                 select new GetDistrictState
                                 {
                                     district_id = a.district_id,
                                     district_code = a.district_code,
                                     district_name = a.district_name,
                                     cntry_id = a.cntry_id,
                                     cntry_name = ab.country_name,
                                     stat_id = a.stat_id,
                                     state_name = b.state_name,
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
        public async Task<bool> ApproveDistrict(ApproveDistrict lead)
        {
            //try
            //{
                if (lead.district_id != 0)
                {
                    var result = await db.Districts.Where(x => x.district_id == lead.district_id).FirstOrDefaultAsync();
                    if (result.status != 3)
                    {
                        //result.district_id = lead.district_id;
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
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message);
            //}

        }
    }
}
