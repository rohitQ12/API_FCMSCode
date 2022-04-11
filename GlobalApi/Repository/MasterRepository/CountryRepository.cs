using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class CountryRepository : ICountry
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public CountryRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }

        public async Task<Countries> InsertCountry(Countries lead)
        {
            try
            {
                var duplicate = await db.Countries.FirstOrDefaultAsync(x => x.country_code == lead.country_code || x.country_name == lead.country_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Countries");
                    Countries obj = new Countries()
                    {
                        cntry_id = id,
                        country_name = lead.country_name,
                        country_code = lead.country_code,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Countries.AddAsync(obj);
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
        public async Task<Countries> UpdateCountry(Countries lead)
        {
            try
            {
                var result = await db.Countries.FirstOrDefaultAsync(x => x.cntry_id == lead.cntry_id);
                if (result != null)
                {
                    result.cntry_id = lead.cntry_id;
                    result.country_name = lead.country_name;
                    result.country_code = lead.country_code;
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
        public async Task<List<Countries>> GetAllCountry()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Countries
                                 orderby a.cntry_id descending
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
        public async Task<List<Country_DD>> GetCountry_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Countries
                             where a.delete_flag == false && a.status == 1
                             select new Country_DD
                             {
                                 cntry_id = a.cntry_id,
                                 country_name = a.country_name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

        public async Task<Countries> DeleteCountry(int cntry_id)
        {
            try
            {
                var result = await db.Countries.FirstOrDefaultAsync(x => x.cntry_id == cntry_id);

                if (result != null)
                {
                    result.cntry_id = cntry_id;
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

        public async Task<CountryById> GetCountryById(int Country_id)
        {
            if (db != null)
            {
                var query = (from a in db.Countries
                             where a.cntry_id == Country_id
                             select new CountryById
                             {
                                 cntry_id = a.cntry_id,
                                 country_name = a.country_name,
                                 country_code = a.country_code,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
