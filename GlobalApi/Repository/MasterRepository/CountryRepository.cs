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

        public async Task<string> InsertCountry(Countries lead)
        {
            try
            {
                var country_name= await db.Countries.FirstOrDefaultAsync(x => x.country_name == lead.country_name);
                var country_code = await db.Countries.FirstOrDefaultAsync(x => x.country_code == lead.country_code);
                if (country_code == null)
                {
                    if (country_name  == null)
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
                        return "Country Added Successfully";
                    }
                    return "Country Name Already Exists";
                }
                return "Country Code Already Exists";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateCountry(Countries lead)
        {
            try
            {
                var Country = await db.Countries.FirstOrDefaultAsync(x => x.cntry_id == lead.cntry_id);
                var country_name = await db.Countries.FirstOrDefaultAsync(x => x.country_name == lead.country_name);
                var country_code = await db.Countries.FirstOrDefaultAsync(x => x.country_code == lead.country_code);
                if (country_code == null)
                {
                    if (country_name == null)
                    {
                        if (Country != null)
                        {
                            Country.cntry_id = lead.cntry_id;
                            Country.country_code = lead.country_code;
                            Country.country_name = lead.country_name;
                            Country.modified_by = 1;
                            Country.modified_date = DateTime.Now;
                            Country.delete_flag = false;
                            Country.status = 2;
                            await db.SaveChangesAsync();
                            return "Country Updated Successfully";
                        }
                    }
                    return "Country Name Already Exists";
                }
                return "Country Code Already Exists";
                
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
        public async Task<string> DeleteCountry(int cntry_id)
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
                    return "Country Deleted Successfully";
                }
                return "Country Details Does Not Exists";
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

        public async Task<string> ApproveCountry(ApproveCountry lead)
        {
            try
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
                    return "Country Approved Successfully";
                }
                return "Country Details Does Not Exists";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
