using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class GramRepository : IGram
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public GramRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertGram(Gram lead)
        {
            try
            {
                var Gram = await db.Gram.FirstOrDefaultAsync(x => x.Gram_code == lead.Gram_code || x.Gram_name == lead.Gram_name);

                if (Gram.Gram_code != lead.Gram_code)
                {
                    if (Gram.Gram_name != lead.Gram_name)
                    {
                        int id = await primarykeyvalue.primary_key("Gram");
                        Gram obj = new Gram()
                        {
                            Gram_id = id,
                            Gram_code = lead.Gram_code,
                            Gram_name = lead.Gram_name,
                            cntry_id = lead.cntry_id,
                            state_id = lead.state_id,
                            dist_id = lead.dist_id,
                            Taluk_id = lead.Taluk_id,
                            Postal_Code = lead.Postal_Code,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.Gram.AddAsync(obj);
                        await db.SaveChangesAsync();
                        return "Gram Added Successfully";
                    }
                    return "Gram Name Already Exists";
                }
                return "Gram Code Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateGram(Gram lead)
        {
            try
            {
                var Gram = await db.Gram.FirstOrDefaultAsync(x => x.Gram_id == lead.Gram_id);
                if (Gram.Gram_code != lead.Gram_code)
                {
                    if (Gram.Gram_name != lead.Gram_name)
                    {
                        if (Gram != null)
                        {
                            Gram.Gram_id = lead.Gram_id;
                            Gram.Gram_code = lead.Gram_code;
                            Gram.Gram_name = lead.Gram_name;
                            Gram.cntry_id = lead.cntry_id;
                            Gram.state_id = lead.state_id;
                            Gram.dist_id = lead.dist_id;
                            Gram.Taluk_id = lead.Taluk_id;
                            Gram.Postal_Code = lead.Postal_Code;
                            Gram.modified_by = 1;
                            Gram.modified_date = DateTime.Now;
                            Gram.delete_flag = false;
                            Gram.status = 2;
                            await db.SaveChangesAsync();
                            return "Gram Updated Successfully";
                        }
                    }
                    return "Gram Name Already Exists";
                }
                return "Gram Code Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<Gram_DD>> GetGram_DD(int Taluk_id)
        {
            if (db != null)
            {
                var query = (from a in db.Gram
                             where a.Taluk_id == Taluk_id && a.delete_flag == false 
                             && a.status == 3 && a.Gram_id != 0
                             select new Gram_DD
                             {
                                 Gram_id = a.Gram_id,
                                 Gram_code = a.Gram_code,
                                 Gram_name = a.Gram_name,
                                 Postal_Code = a.Postal_Code
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> DeleteGram(int Gram_id)
        {
            try
            {
                var result = await db.Gram.FirstOrDefaultAsync(x => x.Gram_id == Gram_id);
                if (result != null)
                {
                    result.Gram_id = Gram_id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Gram Deleted Successfully";
                }
                return "Gram Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<GetGramTaluk>> GetAllGram()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Gram
                                 join b in db.Countries on a.cntry_id equals b.cntry_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.States on a.state_id equals c.stat_id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Districts on a.dist_id equals d.district_id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Taluk on a.Taluk_id equals e.Taluk_id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Status on a.status equals f.sts_id
                                 where a.Gram_id != 0
                                 orderby a.Gram_id descending
                                 select new GetGramTaluk
                                 {
                                     Gram_id = a.Gram_id,
                                     Gram_code = a.Gram_code,
                                     Gram_name = a.Gram_name,
                                     cntry_id = a.cntry_id,
                                     cntry_name = b.country_name,
                                     state_id = a.state_id,
                                     state_name = c.state_name,
                                     dist_id = a.dist_id,
                                     dist_name = d.district_name,
                                     Taluk_id = a.Taluk_id,
                                     Taluk_name = e.Taluk_name,
                                     Postal_Code = a.Postal_Code,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = f.sts_name,
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
        public async Task<string> ApproveGram(ApproveGram lead)
        {
            try
            {

                var result = await db.Gram.Where(x => x.Gram_id == lead.Gram_id).FirstOrDefaultAsync();
                if (result.status != 3)
                {
                    //result.Gram_id = Gram_id;
                    result.status = 3;
                    if (lead.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = lead.Remarks;
                    await db.SaveChangesAsync();
                    return "Gram Approved Successfully";
                }
                return "Gram Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
