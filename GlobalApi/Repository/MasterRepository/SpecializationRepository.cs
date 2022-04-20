using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class SpecializationRepository : ISpecialization
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public SpecializationRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Specialization> InsertSpecialization(Specialization lead)
        {
            try
            {
                var duplicate = await db.Specialization.FirstOrDefaultAsync(x => x.SP_Code == lead.SP_Code || x.SP_Specialization == lead.SP_Specialization);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Specialization");
                    Specialization obj = new Specialization()
                    {
                        SP_Id = id,
                        //SP_Code = "TM" + Convert.ToString(id),
                        SP_Code = lead.SP_Code,
                        SP_CD_Id = lead.SP_CD_Id,
                        SP_Specialization = lead.SP_Specialization,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Specialization.AddAsync(obj);
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
        public async Task<Specialization> UpdateSpecialization(Specialization lead)
        {
            try
            {
                var result = await db.Specialization.FirstOrDefaultAsync(x => x.SP_Id == lead.SP_Id);
                if (result != null)
                {
                    result.SP_Id = lead.SP_Id;
                    result.SP_Code = lead.SP_Code;
                    result.SP_CD_Id = lead.SP_CD_Id;
                    result.SP_Specialization = lead.SP_Specialization;
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
        public async Task<List<GetAllSpecialization>> GetAllSpecialization()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Specialization
                                 join b in db.Discipline on a.SP_CD_Id equals b.CD_Id
                                 orderby a.SP_Id descending
                                 select new GetAllSpecialization
                                 {
                                     SP_Id = a.SP_Id,
                                     SP_Code = a.SP_Code,
                                     SP_CD_Id = a.SP_CD_Id,
                                     SP_CD_ClinicalDiscipline = b.CD_ClinicalDiscipline,
                                     SP_Specialization = a.SP_Specialization,
                                     delete_flag = a.delete_flag,
                                     status = a.status
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
        public async Task<List<Specialization_DD>> GetSpecialization_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Specialization
                             where a.delete_flag == false && a.status == 1
                             select new Specialization_DD
                             {
                                 SP_Id = a.SP_Id,
                                 SP_Code = a.SP_Code,
                                 SP_Specialization = a.SP_Specialization
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Specialization> DeleteSpecialization(int SP_Id)
        {
            try
            {
                var result = await db.Specialization.FirstOrDefaultAsync(x => x.SP_Id == SP_Id);
                if (result != null)
                {
                    result.SP_Id = SP_Id;
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
        public async Task<SpecializationById> GetSpecializationById(int SP_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Specialization
                             join b in db.Discipline on a.SP_CD_Id equals b.CD_Id
                             where a.SP_Id == SP_Id
                             select new SpecializationById
                             {
                                 SP_Id = a.SP_Id,
                                 SP_Code = a.SP_Code,
                                 SP_CD_ClinicalDiscipline = b.CD_ClinicalDiscipline,
                                 SP_Specialization = a.SP_Specialization,
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
