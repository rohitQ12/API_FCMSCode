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

        public async Task<string> InsertCountry(Countries countries)
        {
            try
            {
                var Countryname = await db.Countries.FirstOrDefaultAsync(x => x.country_name == countries.country_name);
                var Countrycode = await db.Countries.FirstOrDefaultAsync(x => x.country_code == countries.country_code);

                if (Countrycode != null)
                {
                    return "Country Code Already Exists";
                }

                if (Countryname != null)
                {
                    return "Country Name Already Exists";
                }

                int id = await primarykeyvalue.primary_key("Countries");
                Countries obj = new Countries()
                {
                    cntry_id = id,
                    country_name = countries.country_name,
                    country_code = countries.country_code,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Countries.AddAsync(obj);
                await db.SaveChangesAsync();
                return "Country Added Successfully";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateCountry(Countries countries)
        {
            try
            {
                var Country = await db.Countries.FirstOrDefaultAsync(x => x.cntry_id == countries.cntry_id);
                var country_name = await db.Countries.FirstOrDefaultAsync(x => x.country_name == countries.country_name);
                var country_code = await db.Countries.FirstOrDefaultAsync(x => x.country_code == countries.country_code);
                if (country_code != null)
                {
                    if(country_code.country_code != Country.country_code)
                    {
                        return "Country Code Already Exists";
                    }
                }
                if (country_name != null)
                {
                    if (country_name.country_name != Country.country_name)
                    {
                        return "Country Code Already Exists";
                    }
                }

                if (Country != null)
                {
                    Country.cntry_id = countries.cntry_id;
                    Country.country_code = countries.country_code;
                    Country.country_name = countries.country_name;
                    Country.modified_by = 1;
                    Country.modified_date = DateTime.Now;
                    Country.delete_flag = false;
                    Country.status = 2;
                    await db.SaveChangesAsync();
                    return "Country Updated Successfully";
                }
                return "Country Didn't Exists";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<dynamic> GetAllCountry(int ItemsPerPage, int pageno)
        {
            try
            {

                var query = (from a in db.Countries
                             join b in db.Status on a.status equals b.sts_id
                             where a.cntry_id != 0
                             orderby a.cntry_id descending
                             select new
                             {
                                 cntry_id = a.cntry_id,
                                 country_code = a.country_code,
                                 country_name = a.country_name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = b.sts_name,
                                 Remarks = a.Remarks,
                             }).Take(ItemsPerPage);
                return await query.ToListAsync();
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

                var query = (from a in db.Countries
                             join b in db.Status on a.status equals b.sts_id
                             where a.cntry_id != 0 && a.delete_flag == false
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<List<Country_DD>> GetCountry_DD()
        {

            var query = (from a in db.Countries
                         where a.delete_flag == false && a.status == 3 && a.cntry_id !=0
                         orderby a.country_name
                         select new Country_DD
                         {
                             cntry_id = a.cntry_id,
                             country_code = a.country_code,
                             country_name = a.country_name
                         }).ToListAsync();
            return await query;

        }

        public async Task<List<Country_DD>> GetCountry_DD_Mobile()
        {

            var query = (from a in db.Countries
                         where a.delete_flag == false && a.status == 3 && a.cntry_id != 0
                         orderby a.country_name
                         select new Country_DD
                         {
                             cntry_id = a.cntry_id,
                             country_code = a.country_code,
                             country_name = a.country_name
                         }).ToListAsync();
            return await query;

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
        public async Task<string> ApproveCountry(ApproveCountry approvecountry)
        {
            try
            {
                var result = await db.Countries.FirstOrDefaultAsync(x => x.cntry_id == approvecountry.cntry_id);
                if (result != null)
                {
                    result.status = 3;
                    if (approvecountry.Remarks== null || approvecountry.Remarks == "")
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = approvecountry.Remarks;
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
