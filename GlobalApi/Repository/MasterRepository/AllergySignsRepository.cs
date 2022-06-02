using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class AllergySignsRepository : IAllergySigns
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public AllergySignsRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<AllergySigns> InsertAllergySigns(AllergySigns lead)
        {
            try
            {
                var duplicate = await db.AllergySigns.FirstOrDefaultAsync(x => x.Al_Name == lead.Al_Name || x.Acronyms == lead.Acronyms);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("AllergySigns");
                    AllergySigns obj = new AllergySigns()
                    {
                        Al_Id = id,
                        Al_Code = lead.Al_Code,
                        Al_Name = lead.Al_Name,
                        Acronyms = lead.Acronyms,
                        //Dis_SP_Id_FK = lead.Dis_SP_Id_FK,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.AllergySigns.AddAsync(obj);
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
        public async Task<AllergySigns> UpdateAllergySigns(AllergySigns lead)
        {
            try
            {
                var result = await db.AllergySigns.FirstOrDefaultAsync(x => x.Al_Id == lead.Al_Id);
                if (result != null)
                {
                    result.Al_Id = lead.Al_Id;
                    result.Al_Name = lead.Al_Name;
                    result.Al_Code = lead.Al_Code;
                    result.Acronyms = lead.Acronyms;
                    //result.Dis_SP_Id_FK = lead.Dis_SP_Id_FK;
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
        public async Task<List<GetAllAllergySigns>> GetAllAllergySigns()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.AllergySigns
                                 join b in db.Status on a.status equals b.sts_id
                                 orderby a.Al_Id descending
                                 select new GetAllAllergySigns
                                 {
                                     Al_Id = a.Al_Id,
                                     Al_Code = a.Al_Code,
                                     Al_Name = a.Al_Name,
                                     Acronyms = a.Acronyms,
                                     status = a.status,
                                     sts_name = b.sts_name,
                                     delete_flag = a.delete_flag,
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
        public async Task<List<AllergySigns_DD>> GetAllergySigns_DD()
        {
            if (db != null)
            {
                var query = (from a in db.AllergySigns
                             where a.delete_flag == false && a.status != 6 && a.Al_Id != 0
                             select new AllergySigns_DD
                             {
                                 Al_Id = a.Al_Id,
                                 Al_Code = a.Al_Code,
                                 Al_Name = a.Al_Name,
                                 //Dis_SP_Id_FK = a.Dis_SP_Id_FK,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<AllergySigns> DeleteAllergySigns(int Al_Id)
        {
            try
            {
                var result = await db.AllergySigns.FirstOrDefaultAsync(x => x.Al_Id == Al_Id);
                if (result != null)
                {
                    result.Al_Id = Al_Id;
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
        public async Task<AllergySignsBy_Id> GetAllergySignsById(int Al_Id)
        {
            if (db != null)
            {
                var query = (from a in db.AllergySigns
                             join b in db.Status on a.status equals b.sts_id
                             where a.Al_Id == Al_Id
                             select new AllergySignsBy_Id
                             {
                                 Al_Id = a.Al_Id,
                                 Al_Code = a.Al_Code,
                                 Al_Name = a.Al_Name,
                                 Acronyms = a.Acronyms,
                                 //Dis_SP_Id_FK = a.Dis_SP_Id_FK,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = b.sts_name,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
