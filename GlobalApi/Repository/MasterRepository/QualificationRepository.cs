using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class QualificationRepository : IQualification
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public QualificationRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<Qualification> InsertQualification(Qualification lead)
        {
            try
            {
                var duplicate = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_code == lead.qualification_code || x.qualification_Name == lead.qualification_Name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Qualification");
                    Qualification obj = new Qualification()
                    {
                        qualification_id = id,
                        //qualification_code = "Q" + Convert.ToString(id),
                        qualification_code = lead.qualification_code,
                        qualification_Name = lead.qualification_Name,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Qualification.AddAsync(obj);
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
        public async Task<Qualification> UpdateQualification(Qualification lead)
        {
            try
            {
                var result = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_id == lead.qualification_id);
                if (result != null)
                {
                    result.qualification_id = lead.qualification_id;
                    result.qualification_code = lead.qualification_code;
                    result.qualification_Name = lead.qualification_Name;
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
        public async Task<List<Qualification>> GetAllQualification()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Qualification
                                 orderby a.qualification_id descending
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
        public async Task<List<Qualification_DD>> GetQualification_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Qualification
                             where a.delete_flag == false && a.status == 1
                             select new Qualification_DD
                             {
                                 qualification_id = a.qualification_id,
                                 qualification_Name = a.qualification_Name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Qualification> DeleteQualification(int qualification_id)
        {
            try
            {
                var result = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_id == qualification_id);
                if (result != null)
                {
                    result.qualification_id = qualification_id;
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
        public async Task<QualificationById> GetQualificationById(int qualification_id)
        {
            if (db != null)
            {
                var query = (from a in db.Qualification
                             where a.qualification_id == qualification_id
                             select new QualificationById
                             {
                                 qualification_id = a.qualification_id,
                                 qualification_code = a.qualification_code,
                                 qualification_Name = a.qualification_Name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
