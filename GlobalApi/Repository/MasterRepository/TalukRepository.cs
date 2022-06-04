using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class TalukRepository : ITaluk
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public TalukRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Taluk> InsertTaluk(Taluk lead)
        {
            try
            {
                var duplicate = await db.Taluk.FirstOrDefaultAsync(x => x.Taluk_code == lead.Taluk_code 
                && x.Taluk_name == lead.Taluk_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Taluk");
                    Taluk obj = new Taluk()
                    {
                        Taluk_id = id,
                        //Taluk_code = "DI-" + Convert.ToString(id),
                        Taluk_code = lead.Taluk_code,
                        Taluk_name = lead.Taluk_name,
                        cntry_id = lead.cntry_id,
                        state_id = lead.state_id,
                        district_id = lead.district_id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Taluk.AddAsync(obj);
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
        public async Task<Taluk> UpdateTaluk(Taluk lead)
        {
            try
            {
                var result = await db.Taluk.FirstOrDefaultAsync(x => x.Taluk_id == lead.Taluk_id);
                if (result != null)
                {
                    result.Taluk_id = lead.Taluk_id;
                    result.Taluk_code = lead.Taluk_code;
                    result.Taluk_name = lead.Taluk_name;
                    result.cntry_id = lead.cntry_id;
                    result.state_id = lead.state_id;
                    result.district_id = lead.district_id;
                    result.modified_by = 2;
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
        public async Task<List<Taluk_DD>> GetTaluk_DD(int district_id)
        {
            if (db != null)
            {
                var query = (from a in db.Taluk
                             where a.district_id == district_id && a.delete_flag == false 
                             && a.status == 3 && a.Taluk_id != 0
                             select new Taluk_DD
                             {
                                 Taluk_id = a.Taluk_id,
                                 Taluk_code = a.Taluk_code,
                                 Taluk_name = a.Taluk_name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Taluk> DeleteTaluk(int Taluk_id)
        {
            try
            {
                var result = await db.Taluk.FirstOrDefaultAsync(x => x.Taluk_id == Taluk_id);
                if (result != null)
                {
                    result.Taluk_id = Taluk_id;
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
        //public async Task<TalukById> GetTalukById(int Taluk_id)
        //{
        //    if (db != null)
        //    {
        //        var query = (from a in db.Taluk
        //                     where a.Taluk_id == Taluk_id
        //                     select new TalukById
        //                     {
        //                         Taluk_id = a.Taluk_id,
        //                         Taluk_name = a.Taluk_name,
        //                         Taluk_code = a.Taluk_code,
        //                         delete_flag = a.delete_flag,
        //                         status = a.status,

        //                     }).FirstOrDefaultAsync();
        //        return await query;
        //    }
        //    return null;
        //}
        public async Task<List<GetTalukDistricts>> GetAllTaluk()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Taluk
                                 join b in db.Countries on a.cntry_id equals b.cntry_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.States on a.state_id equals c.stat_id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Districts on a.district_id equals d.district_id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Status on a.status equals e.sts_id
                                 where a.Taluk_id != 0
                                 orderby a.Taluk_id descending
                                 select new GetTalukDistricts
                                 {
                                     Taluk_id = a.Taluk_id,
                                     Taluk_code = a.Taluk_code,
                                     Taluk_name = a.Taluk_name,
                                     cntry_id = a.cntry_id,
                                     cntry_name = b.country_name,
                                     state_id = a.state_id,
                                     state_name = c.state_name,
                                     district_id = a.district_id,
                                     district_name = d.district_name,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = e.sts_name,
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
        public async Task<string> ApproveTaluk(ApproveTaluk lead)
        {
            try
            {
                if (lead.Taluk_id != 0)
                {
                    var result = await db.Taluk.Where(x => x.Taluk_id == lead.Taluk_id).FirstOrDefaultAsync();
                    if (result.status != 3)
                    {
                        //result.Taluk_id = lead.Taluk_id;
                        result.status = 3;
                        if (lead.Remarks == null)
                        {
                            result.Remarks = "OK";
                        }
                        else
                            result.Remarks = lead.Remarks;
                        await db.SaveChangesAsync();
                        return "Taluk is Approved";
                    }
                    else
                        return "Already Active";
                }
                else
                    return "Cannot Approve Default Taluk";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
