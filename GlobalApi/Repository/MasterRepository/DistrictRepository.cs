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
        public async Task<Districts> InsertDistrict(Districts lead)
        {
            try
            {
                var duplicate = await db.Districts.FirstOrDefaultAsync(x => x.district_code == lead.district_code || x.district_name == lead.district_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Districts");
                    Districts obj = new Districts()
                    {
                        district_id = id,
                        //district_code = "DI-" + Convert.ToString(id),
                        district_code = lead.district_code,
                        district_name = lead.district_name,
                        stat_id = lead.stat_id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Districts.AddAsync(obj);
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
        public async Task<Districts> UpdateDistrict(Districts lead)
        {
            try
            {
                var result = await db.Districts.FirstOrDefaultAsync(x => x.district_id == lead.district_id);
                if (result != null)
                {
                    result.stat_id = lead.stat_id;
                    result.district_id = lead.district_id;
                    result.district_name = lead.district_name;
                    result.district_code = lead.district_code;
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
        public async Task<List<District_DD>> GetDistrict_DD(int stat_id)
        {
            if (db != null)
            {
                var query = (from a in db.Districts
                             where a.stat_id == stat_id && a.delete_flag == false && a.status == 1
                             select new District_DD
                             {
                                 district_id = a.district_id,
                                 district_name = a.district_name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Districts> DeleteDistrict(int district_id)
        {
            try
            {
                var result = await db.Districts.FirstOrDefaultAsync(x => x.district_id == district_id);
                if (result != null)
                {
                    result.district_id = district_id;
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
        public async Task<DistrictById> GetDistrictById(int district_id)
        {
            if (db != null)
            {
                var query = (from a in db.Districts
                             where a.district_id == district_id
                             select new DistrictById
                             {
                                 district_id = a.district_id,
                                 district_name = a.district_name,
                                 district_code = a.district_code,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<GetStateDistrict>> GetAllDistrict()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.States
                                 join b in db.Districts on a.stat_id equals b.stat_id
                                 orderby b.district_id descending
                                 select new GetStateDistrict
                                 {
                                     district_id = b.district_id,
                                     district_code = b.district_code,
                                     district_name = b.district_name,
                                     stat_id = a.stat_id,
                                     state_name = a.state_name,
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


    }
}
