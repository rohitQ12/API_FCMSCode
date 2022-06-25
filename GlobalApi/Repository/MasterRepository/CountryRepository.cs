using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public sealed class CountryRepository : ICountry
    {
        private readonly GlobalContext db;
        private readonly IPrimarykeyvalue primarykeyvalue;
        public CountryRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<bool> InsertCountry(Countries lead)
        {
            try
            {
                var duplicate = await db.Countries.FirstOrDefaultAsync(x => x.country_code == lead.country_code && x.country_name == lead.country_name);
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
                    return true;

                }
                return false;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> UpdateCountry(Countries lead)
        {
            try
            {
                var result = await db.Countries.FirstOrDefaultAsync(x => x.cntry_id == lead.cntry_id);
                if (result != null)
                {
                    result.cntry_id = lead.cntry_id;
                    result.country_code = lead.country_code;
                    result.country_name = lead.country_name;
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
        public async Task<List<GetAllCountry>> GetAllCountry()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Countries
                                 join b in db.Status on a.status equals b.sts_id
                                 where a.cntry_id != 0
                                 orderby a.cntry_id descending
                                 select new GetAllCountry
                                 {
                                     cntry_id = a.cntry_id,
                                     country_code = a.country_code,
                                     country_name = a.country_name,
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
        public async Task<List<Country_DD>> GetCountry_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Countries
                             where a.delete_flag == false && a.status == 3
                             && a.cntry_id != 0
                             select new Country_DD
                             {
                                 cntry_id = a.cntry_id,
                                 country_code = a.country_code,
                                 country_name = a.country_name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

        public async Task<bool> DeleteCountry(int cntry_id)
        {
            try
            {
                var result = await db.Countries.FirstOrDefaultAsync(x => x.cntry_id == cntry_id);

                if (result != null)
                {
                    result.cntry_id = cntry_id;
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

        public async Task<CountryById> GetCountryById(int Country_id)
        {
            if (db != null)
            {
                var query = (from a in db.Countries
                             join b in db.Status on a.status equals b.sts_id
                             where a.cntry_id == Country_id && a.cntry_id != 0
                             select new CountryById
                             {
                                 cntry_id = a.cntry_id,
                                 country_name = a.country_name,
                                 country_code = a.country_code,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 Remarks = a.Remarks,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

        public async Task<bool> ApproveCountry(ApproveCountry lead)
        {
            try
            {
                if (lead.cntry_id != 0)
                {
                    var result = await db.Countries.FirstOrDefaultAsync(x => x.cntry_id == lead.cntry_id);
                    if (result != null)
                    {
                        //result.cntry_id = lead.cntry_id;
                        result.status = 3;
                        if (lead.Remarks == null)
                        {
                            lead.Remarks = "OK";
                        }
                        else
                            result.Remarks = lead.Remarks;
                        await db.SaveChangesAsync();
                        return true;
                    }
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
