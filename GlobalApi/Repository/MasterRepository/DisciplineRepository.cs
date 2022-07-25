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
        public async Task<string> InsertDiscipline(Discipline lead)
        {
            try
            {
                var Disc_Desc = await db.Discipline.FirstOrDefaultAsync(x => x.CD_ClinicalDiscipline == lead.CD_ClinicalDiscipline);
                var Disc_code = await db.Discipline.FirstOrDefaultAsync(x => x.CD_Code == lead.CD_Code);
                if (Disc_code == null)
                {
                    if (Disc_Desc == null)
                    {
                        int id = await primarykeyvalue.primary_key("Discipline");
                        Discipline obj = new Discipline()
                        {
                            CD_Id = id,
                            CD_Code = lead.CD_Code,
                            CD_ClinicalDiscipline = lead.CD_ClinicalDiscipline,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.Discipline.AddAsync(obj);
                        await db.SaveChangesAsync();
                        return "Discipline Added Successfully";
                    }
                    return "Discipline Desc Already Exists";
                }
                return "Discipline Code Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateDiscipline(Discipline lead)
        {
            try
            {
                var result = await db.Discipline.FirstOrDefaultAsync(x => x.CD_Id == lead.CD_Id);
                var Disc_Desc = await db.Discipline.FirstOrDefaultAsync(x => x.CD_ClinicalDiscipline == lead.CD_ClinicalDiscipline);
                var Disc_code = await db.Discipline.FirstOrDefaultAsync(x => x.CD_Code == lead.CD_Code);
                if (Disc_code == null || result.CD_Code == lead.CD_Code)
                {
                    if (Disc_Desc == null || result.CD_ClinicalDiscipline == lead.CD_ClinicalDiscipline)
                    {
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
                            return "Discipline Updated Successfully";
                        }
                        return "Discipline Not Found";
                    }
                    return "Discipline Name Already Exists";
                }
                return "Discipline Code Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<GetAllDiscipline>> GetAllDiscipline()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Discipline
                                 join b in db.Status on a.status equals b.sts_id
                                 where a.CD_Id != 0
                                 orderby a.CD_Id descending
                                 select new GetAllDiscipline
                                 {
                                     CD_Id = a.CD_Id,
                                     CD_Code = a.CD_Code,
                                     CD_ClinicalDiscipline = a.CD_ClinicalDiscipline,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = b.sts_name,
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
        public async Task<List<Discipline_DD>> GetDiscipline_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Discipline
                             where a.CD_Id != 0 && a.status == 3
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
        public async Task<string> DeleteDiscipline(int CD_Id)
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
                    return "Discipline Deleted Successfully";
                }
                return "Discipline Doesn't Exists";
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
                             join b in db.Status on a.status equals b.sts_id
                             where a.CD_Id == CD_Id && a.CD_Id != 0
                             select new DisciplineById
                             {
                                 CD_Id = a.CD_Id,
                                 CD_Code = a.CD_Code,
                                 CD_ClinicalDiscipline = a.CD_ClinicalDiscipline,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = b.sts_name,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> ApproveDiscipline(ApproveDiscipline lead)
        {
            try
            {
                var result = await db.Discipline.Where(x => x.CD_Id == lead.CD_Id).FirstOrDefaultAsync();
                if (result != null)
                {
                    //result.CD_Id = CD_Id;
                    result.status = 3;
                    if (lead.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = lead.Remarks;
                    await db.SaveChangesAsync();
                    return "Discipline Approved Successfully";
                }
                return "Discipline Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
