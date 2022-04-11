using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class SkillSetRepository : ISkillSet
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public SkillSetRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
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
        public async Task<List<Qual_SkillSet>> GetAllSkillSet()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.SkillSets
                                 join b in db.Qualification on a.qualification_id equals b.qualification_id
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
        public async Task<List<SkillSet_DD>> GetSkillSet_DD()
        {
            if (db != null)
            {
                var query = (from a in db.SkillSets
                             where a.delete_flag == false && a.status == 1
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
        public async Task<SkillSetById> GetSkillSetById(int Skillset_id)
        {
            if (db != null)
            {
                var query = (from a in db.SkillSets
                             where a.Skillset_id == Skillset_id
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
    }
}
