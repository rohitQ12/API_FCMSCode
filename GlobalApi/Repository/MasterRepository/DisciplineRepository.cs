using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class DisciplineRepository : IDiscipline
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DisciplineRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Discipline> InsertDiscipline(Discipline lead)
        {
            try
            {
                var duplicate = await db.Discipline.FirstOrDefaultAsync(x => x.CD_Code == lead.CD_Code || x.CD_ClinicalDiscipline == lead.CD_ClinicalDiscipline);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Discipline");
                    Discipline obj = new Discipline()
                    {
                        CD_Id = id,
                        //CD_Code = '0' + Convert.ToString(id),                        CD_Code = '0' + Convert.ToString(id),
                        CD_Code = lead.CD_Code,
                        CD_ClinicalDiscipline = lead.CD_ClinicalDiscipline,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Discipline.AddAsync(obj);
                    await InsertUsers(obj);
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
        public async Task<UsersLists> InsertUsers(Discipline lead)
        {
            int _id = await primarykeyvalue.primary_key("Users");
            UsersLists obj = new UsersLists()
            {
                Id = _id,
                User_cat = "Discipline",
                User_ref_id = lead.CD_Id,
            };
            var result = await db.UsersLists.AddAsync(obj);
            await db.SaveChangesAsync();
            return result.Entity;

        }
        public async Task<Discipline> UpdateDiscipline(Discipline lead)
        {
            try
            {
                var result = await db.Discipline.FirstOrDefaultAsync(x => x.CD_Id == lead.CD_Id);
                if (result != null)
                {
                    result.CD_Id = lead.CD_Id;
                    result.CD_Code = lead.CD_Code;
                    result.CD_ClinicalDiscipline = lead.CD_ClinicalDiscipline;
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
        public async Task<List<Discipline>> GetAllDiscipline()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Discipline
                                 orderby a.CD_Id descending
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
        public async Task<List<Discipline_DD>> GetDiscipline_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Discipline
                             select new Discipline_DD
                             {
                                 CD_Id = a.CD_Id,
                                 CD_Code = a.CD_Code,
                                 CD_ClinicalDiscipline = a.CD_ClinicalDiscipline,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Discipline> DeleteDiscipline(int CD_Id)
        {
            try
            {
                var result = await db.Discipline.FirstOrDefaultAsync(x => x.CD_Id == CD_Id);
                if (result != null)
                {
                    result.CD_Id = CD_Id;
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
        public async Task<DisciplineById> GetDisciplineById(int CD_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Discipline
                             where a.CD_Id == CD_Id
                             select new DisciplineById
                             {
                                 CD_Id = a.CD_Id,
                                 CD_Code = a.CD_Code,
                                 CD_ClinicalDiscipline = a.CD_ClinicalDiscipline,
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
