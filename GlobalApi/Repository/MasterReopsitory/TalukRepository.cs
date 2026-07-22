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
        public async Task<string> InsertTaluk(Taluk Taluk)
        {
            try
            {
                var Taluk_code = await db.Taluk.FirstOrDefaultAsync(x => x.Taluk_code == Taluk.Taluk_code);
                var Taluk_name = await db.Taluk.FirstOrDefaultAsync(x => x.Taluk_name == Taluk.Taluk_name);

                if (Taluk_code != null)
                {
                    return "Taluk Code Already Exists";
                }

                if (Taluk_name != null)
                {
                    return "Taluk Name Already Exists";
                }

                int id = await primarykeyvalue.primary_key("Taluk");
                Taluk obj = new Taluk()
                {
                    Taluk_id = id,
                    Taluk_code = Taluk.Taluk_code,
                    Taluk_name = Taluk.Taluk_name,
                    cntry_id = Taluk.cntry_id,
                    state_id = Taluk.state_id,
                    district_id = Taluk.district_id,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Taluk.AddAsync(obj);
                await db.SaveChangesAsync();
                return "Taluk Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateTaluk(Taluk taluk)
        {
            try
            {
                var Taluk = await db.Taluk.FirstOrDefaultAsync(x => x.Taluk_id == taluk.Taluk_id);
                var Taluk_code = await db.Taluk.FirstOrDefaultAsync(x => x.Taluk_code == Taluk.Taluk_code);
                var Taluk_name = await db.Taluk.FirstOrDefaultAsync(x => x.Taluk_name == Taluk.Taluk_name);
                if (Taluk_code != null)
                {
                    if (Taluk_code.Taluk_code != Taluk.Taluk_code)
                    {
                        return "Taluk Code Already Exists";
                    }
                }
                if (Taluk_name != null)
                {
                    if (Taluk_name.Taluk_name != Taluk.Taluk_name)
                    {
                        return "Taluk Name Already Exists";
                    }
                }

                if (Taluk != null)
                {
                    Taluk.Taluk_id = taluk.Taluk_id;
                    Taluk.Taluk_code = taluk.Taluk_code;
                    Taluk.Taluk_name = taluk.Taluk_name;
                    Taluk.cntry_id = taluk.cntry_id;
                    Taluk.state_id = taluk.state_id;
                    Taluk.district_id = taluk.district_id;
                    Taluk.modified_by = 2;
                    Taluk.modified_date = DateTime.Now;
                    Taluk.delete_flag = false;
                    Taluk.status = 2;
                    await db.SaveChangesAsync();
                    return "Taluk Updated Successfully";
                }
                return "Taluk Didn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<Taluk_DD>> GetTaluk_DD(int district_id)
        {
            var query = (from a in db.Taluk
                         where a.district_id == district_id && a.delete_flag == false
                         && a.status == 3
                         orderby a.Taluk_name
                         select new Taluk_DD
                         {
                             Taluk_id = a.Taluk_id,
                             Taluk_code = a.Taluk_code,
                             Taluk_name = a.Taluk_name
                         }).ToListAsync();
            return await query;
        }

        public async Task<string> DeleteTaluk(int Taluk_id)
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
                    return "Taluk Deleted Successfully";
                }
                return "Taluk Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetTalukDistricts>> GetAllTaluk()
        {
            try
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> ApproveTaluk(ApproveTaluk ApproveTaluk)
        {
            try
            {

                var result = await db.Taluk.Where(x => x.Taluk_id == ApproveTaluk.Taluk_id).FirstOrDefaultAsync();
                if (result.status != 3)
                {
                    result.status = 3;
                    if (ApproveTaluk.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = ApproveTaluk.Remarks;
                    await db.SaveChangesAsync();
                    return "Taluk Approved Successfully";
                }
                return "Taluk Details Does Not Exists";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
