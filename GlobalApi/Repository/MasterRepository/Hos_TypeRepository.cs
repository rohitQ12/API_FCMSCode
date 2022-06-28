 using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Hos_TypeRepository : IHos_Type
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Hos_TypeRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Hos_Type> InsertHos_Type(Hos_Type lead)
        {
            try
            {
                var duplicate = await db.Hos_Type.FirstOrDefaultAsync(x => x.Type == lead.Type);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Hos_Type");
                    Hos_Type obj = new Hos_Type()
                    {
                        Id = id,
                        Type = lead.Type,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Hos_Type.AddAsync(obj);
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
        public async Task<Hos_Type> UpdateHos_Type(Hos_Type lead)
        {
            try
            {
                var result = await db.Hos_Type.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.Type = lead.Type;
                    result.modified_by = 2;
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
        public async Task<List<Hos_Type>> GetAllHos_Type()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Hos_Type
                                 orderby a.Id descending
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
        public async Task<List<HosType_DD>> GetHos_Type_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Hos_Type
                             where a.delete_flag == false && a.status != 6 && a.Id != 0
                             select new HosType_DD
                             {
                                 Id = a.Id,
                                 Type = a.Type,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Hos_Type> DeleteHos_Type(int Id)
        {
            try
            {
                var result = await db.Hos_Type.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 3;
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
        //public async Task<Hos_TypeBy_Id> GetHos_TypeById(int Id)
        //{
        //    if (db != null)
        //    {
        //        var query = (from a in db.Hos_Type
        //                     where a.Id == Id
        //                     select new Hos_TypeBy_Id
        //                     {
        //                         Id = a.Id,
        //                         Hos_Type_Code = a.Hos_Type_Code,
        //                         Hos_Type_Name = a.Hos_Type_Name,
        //                         Acronyms = a.Acronyms,
        //                         Dis_SP_Id_FK = a.Dis_SP_Id_FK,
        //                         delete_flag = a.delete_flag,
        //                         status = a.status
        //                     }).FirstOrDefaultAsync();
        //        return await query;
        //    }
        //    return null;
        //}

    }
}
