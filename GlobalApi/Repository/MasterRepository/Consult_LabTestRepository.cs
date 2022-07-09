using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Consult_LabTestRepository : IConsult_LabTest
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Consult_LabTestRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Consult_LabTest> InsertConsult_LabTest(Consult_LabTest lead)
        {
            try
            {
                var duplicate = await db.Consult_LabTest.FirstOrDefaultAsync(x => x.CON_Id == lead.CON_Id || x.Category_Id == lead.Category_Id
                || x.Description_Id == lead.Description_Id);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Consult_LabTest");
                    Consult_LabTest obj = new Consult_LabTest()
                    {
                        Id = id,
                        CON_Id = lead.CON_Id,
                        Category_Id = lead.Category_Id,
                        Description_Id = lead.Description_Id,
                        Created_by = 1,
                        Created_date = DateTime.Now,
                        Delete_flag = false,
                        Status = 1
                    };
                    var result = await db.Consult_LabTest.AddAsync(obj);
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
        public async Task<Consult_LabTest> UpdateConsult_LabTest(Consult_LabTest lead)
        {
            try
            {
                var result = await db.Consult_LabTest.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.CON_Id = lead.CON_Id;
                    result.Category_Id = lead.Category_Id;
                    result.Description_Id = lead.Description_Id;
                    result.Modified_by = 2;
                    result.Modified_date = DateTime.Now;
                    result.Delete_flag = false;
                    result.Status = 2;
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
        public async Task<List<GetConsult_LabTest>> GetAllConsult_LabTest()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consult_LabTest
                                 join b in db.Status on a.Status equals b.sts_id
                                 join c in db.LAB_SUBINVESTIGATIONS on a.Category_Id equals c.Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.LAB_Description on a.Description_Id equals d.Lab_DescId into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 orderby a.Id descending
                                 select new GetConsult_LabTest
                                 {
                                     Id = a.Id,
                                     CON_Id = a.CON_Id,
                                     Category_Id = a.Category_Id,
                                     Cat_Name = c.Sub_Category,
                                     Description_Id = a.Description_Id,
                                     Description = d.Lab_Description,
                                     Delete_flag = a.Delete_flag,
                                     Status = a.Status,
                                     sts_name = b.sts_name
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
        public async Task<Consult_LabTest> DeleteConsult_LabTest(int Id)
        {
            try
            {
                var result = await db.Consult_LabTest.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
                    result.Deleted_by = 3;
                    result.Deleted_date = DateTime.Now;
                    result.Delete_flag = true;
                    result.Status = 6;
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
        public async Task<GetConsult_LabTest> GetConsult_LabTestById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.Consult_LabTest
                             join b in db.Status on a.Status equals b.sts_id
                             join c in db.LAB_SUBINVESTIGATIONS on a.Category_Id equals c.Id into clist
                             from c in clist.DefaultIfEmpty()
                             join d in db.LAB_Description on a.Description_Id equals d.Lab_DescId into dlist
                             from d in dlist.DefaultIfEmpty()
                             where a.Id == Id
                             select new GetConsult_LabTest
                             {
                                 Id = a.Id,
                                 CON_Id = a.CON_Id,
                                 Category_Id = a.Category_Id,
                                 Cat_Name = c.Sub_Category,
                                 Description_Id = a.Description_Id,
                                 Description = d.Lab_Description,
                                 Delete_flag = a.Delete_flag,
                                 Status = a.Status,
                                 sts_name = b.sts_name
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<GetConsult_LabTest>> GetConsult_LabTestByCON_Id(int CON_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Consult_LabTest
                             join b in db.Status on a.Status equals b.sts_id
                             join c in db.LAB_SUBINVESTIGATIONS on a.Category_Id equals c.Id into clist
                             from c in clist.DefaultIfEmpty()
                             join d in db.LAB_Description on a.Description_Id equals d.Lab_DescId into dlist
                             from d in dlist.DefaultIfEmpty()
                             where a.CON_Id == CON_Id
                             select new GetConsult_LabTest
                             {
                                 Id = a.Id,
                                 CON_Id = a.CON_Id,
                                 Category_Id = a.Category_Id,
                                 Cat_Name = c.Sub_Category,
                                 Description_Id = a.Description_Id,
                                 Description = d.Lab_Description,
                                 Delete_flag = a.Delete_flag,
                                 Status = a.Status,
                                 sts_name = b.sts_name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }


    }
}
