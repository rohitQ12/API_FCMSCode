using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class RelationRepository : IRelation
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public RelationRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }

        public async Task<Relation> InsertRelation(Relation lead)
        {
            try
            {
                var duplicate = await db.Relation.FirstOrDefaultAsync(x => x.relation_name == lead.relation_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Relation");
                    Relation obj = new Relation()
                    {
                        relation_id = id,
                        relation_name = lead.relation_name,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Relation.AddAsync(obj);
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
        public async Task<Relation> UpdateRelation(Relation lead)
        {
            try
            {
                var result = await db.Relation.FirstOrDefaultAsync(x => x.relation_id == lead.relation_id);
                if (result != null)
                {
                    result.relation_id = lead.relation_id;
                    result.relation_name = lead.relation_name;
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
        public async Task<List<Relation>> GetAllRelation()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Relation
                                 orderby a.relation_id descending
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
        public async Task<List<Relation_DD>> GetRelation_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Relation
                             where a.delete_flag == false && a.status == 1
                             select new Relation_DD
                             {
                                 relation_id = a.relation_id,
                                 relation_name = a.relation_name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

        public async Task<Relation> DeleteRelation(int relation_id)
        {
            try
            {
                var result = await db.Relation.FirstOrDefaultAsync(x => x.relation_id == relation_id);

                if (result != null)
                {
                    result.relation_id = relation_id;
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

        public async Task<RelationById> GetRelationById(int relation_id)
        {
            if (db != null)
            {
                var query = (from a in db.Relation
                             where a.relation_id == relation_id
                             select new RelationById
                             {
                                 relation_id = a.relation_id,
                                 relation_name = a.relation_name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
