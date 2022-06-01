using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class SkillSetRepository : ISkillSet
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public SkillSetRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<SkillSets> InsertSkillSet(SkillSets lead)
        {
            try
            {
                var duplicate = await db.SkillSets.FirstOrDefaultAsync(x => x.Skillset_name == lead.Skillset_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("SkillSets");
                    SkillSets obj = new SkillSets()
                    {
                        Skillset_id = id,
                        Skillset_name = lead.Skillset_name,
                        qualification_id = lead.qualification_id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.SkillSets.AddAsync(obj);
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
        public async Task<SkillSets> UpdateSkillSet(SkillSets lead)
        {
            try
            {
                var result = await db.SkillSets.FirstOrDefaultAsync(x => x.Skillset_id == lead.Skillset_id /*&& x.qualification_id == lead.qualification_id*/);
                if (result != null)
                {
                    result.Skillset_id = lead.Skillset_id;
                    result.Skillset_name = lead.Skillset_name;
                    result.qualification_id = lead.qualification_id;
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
        public async Task<List<Qual_SkillSet>> GetAllSkillSet()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.SkillSets
                                 join b in db.Qualification on a.qualification_id equals b.qualification_id
                                 where a.Skillset_id != 0
                                 orderby a.Skillset_id descending
                                 select new Qual_SkillSet
                                 {
                                     Skillset_id = a.Skillset_id,
                                     Skillset_name = a.Skillset_name,
                                     qualification_id = a.qualification_id,
                                     qualification_Name = b.qualification_Name,
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
        public async Task<List<SkillSet_DD>> GetSkillSet_DD(int qualification_Id)
        {
            if (db != null)
            {
                var query = (from a in db.SkillSets
                             where a.qualification_id == qualification_Id && a.delete_flag == false && a.status == 3
                             && a.Skillset_id != 0
                             select new SkillSet_DD
                             {
                                 Skillset_id = a.Skillset_id,
                                 Skillset_name = a.Skillset_name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<SkillSets> DeleteSkillSet(int Skillset_id)
        {
            try
            {
                var result = await db.SkillSets.FirstOrDefaultAsync(x => x.Skillset_id == Skillset_id);
                if (result != null)
                {
                    result.Skillset_id = Skillset_id;
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
        public async Task<SkillSetById> GetSkillSetById(int Skillset_id)
        {
            if (db != null)
            {
                var query = (from a in db.SkillSets
                             where a.Skillset_id == Skillset_id && a.Skillset_id != 0
                             select new SkillSetById
                             {
                                 Skillset_id = a.Skillset_id,
                                 Skillset_name = a.Skillset_name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> ApproveSkillSet(int Skillset_id, string? Remarks)
        {
            try
            {
                if(Skillset_id != 0)
                {
                    var result = await db.SkillSets.Where(x => x.Skillset_id == Skillset_id).FirstOrDefaultAsync();
                    if (result.status != 3)
                    {
                        //result.cntry_id = cntry_id;
                        result.status = 3;
                        if (Remarks == null)
                        {
                            result.Remarks = "OK";
                        }
                        else
                            result.Remarks = Remarks;
                        await db.SaveChangesAsync();
                        return "SkillSet is Approved";
                    }
                    else
                        return "Already Active";
                }
                else
                    return "Cannot Approve Default SkillSet";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
