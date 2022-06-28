using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class CurrencyRepository : ICurrency
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public CurrencyRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Currency> InsertCurrency(Currency lead)
        {
            try
            {
                //var country = await db.Countries.FirstOrDefaultAsync(x => x.cntry_id == lead.cntry_id && x.delete_flag == false);
                var duplicate = await db.Currency.FirstOrDefaultAsync(x => x.currency_code == lead.currency_code || x.currency_name == lead.currency_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Currency");
                    Currency obj = new Currency()
                    {
                        currency_id = id,
                        currency_code = lead.currency_code,
                        currency_name = lead.currency_name,
                        cntry_id = lead.cntry_id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Currency.AddAsync(obj);
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
        public async Task<Currency> UpdateCurrency(Currency lead)
        {
            try
            {
                var result = await db.Currency.FirstOrDefaultAsync(x => x.currency_id == lead.currency_id /*&& x.cntry_id == lead.cntry_id*/);
                if (result != null)
                {
                    result.currency_id = lead.currency_id;
                    result.currency_code = lead.currency_code;
                    result.currency_name = lead.currency_name;
                    result.cntry_id = lead.cntry_id;
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
        public async Task<List<GetCountryCurrency>> GetAllCurrency()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Countries
                                 join b in db.Currency on a.cntry_id equals b.cntry_id
                                 orderby b.currency_id descending
                                 select new GetCountryCurrency
                                 {
                                     currency_id = b.currency_id,
                                     currency_code = b.currency_code,
                                     currency_name = b.currency_name,
                                     cntry_id = a.cntry_id,
                                     country_name = a.country_name,
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
        public async Task<List<Currency_DD>> GetCurrency_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Currency
                             where a.delete_flag == false && a.status != 6 && a.currency_id != 0
                             select new Currency_DD
                             {
                                 currency_id = a.currency_id,
                                 currency_name = a.currency_name,
                                 //cntry_id = a.cntry_id
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Currency> DeleteCurrency(int currency_id)
        {
            try
            {
                var result = await db.Currency.FirstOrDefaultAsync(x => x.currency_id == currency_id);
                if (result != null)
                {
                    result.currency_id = currency_id;
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
        public async Task<CurrencyById> GetCurrencyById(int currency_id)
        {
            if (db != null)
            {
                var query = (from a in db.Currency
                             where a.currency_id == currency_id
                             select new CurrencyById
                             {
                                 currency_id = a.currency_id,
                                 currency_code = a.currency_code,
                                 currency_name = a.currency_name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
