using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class SectionRepository : ISection
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public SectionRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<Section> InsertSection(Section lead)
        {
            try
            {

                int id = await primarykeyvalue.primary_key("Section");
                Section obj = new Section()
                {
                    Section_Id = id,
                    Section_Name = lead.Section_Name,
                    Dept_Id = lead.Dept_Id,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Section.AddAsync(obj);
                await db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Section> UpdateSection(Section lead)
        {
            try
            {
                var result = await db.Section.FirstOrDefaultAsync(x => x.Section_Id == lead.Section_Id);
                if (result != null)
                {
                    result.Section_Id = lead.Section_Id;
                    result.Section_Name = lead.Section_Name;
                    result.Dept_Id = lead.Dept_Id;
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
        public async Task<List<GetAllSection>> GetAllSection()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Department
                                 join b in db.Section on a.Dept_Id equals b.Dept_Id
                                 orderby b.Section_Id descending
                                 select new GetAllSection
                                 {
                                     Section_Id = b.Section_Id,
                                     Section_Name = b.Section_Name,
                                     Dept_Id = a.Dept_Id,
                                     Dept_name = a.Dept_name,
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
        public async Task<List<Section_DD>> GetSection_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Section
                             where a.delete_flag == false && a.status == 1
                             select new Section_DD
                             {
                                 Section_Id = a.Section_Id,
                                 Section_Name = a.Section_Name
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

        public async Task<Section> DeleteSection(int Section_Id)
        {
            try
            {
                var result = await db.Section.FirstOrDefaultAsync(x => x.Section_Id == Section_Id);

                if (result != null)
                {
                    result.Section_Id = Section_Id;
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

        public async Task<SectionById> GetSectionById(int Section_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Section
                             where a.Section_Id == Section_Id
                             select new SectionById
                             {
                                 Section_Id = a.Section_Id,
                                 Section_Name = a.Section_Name,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
