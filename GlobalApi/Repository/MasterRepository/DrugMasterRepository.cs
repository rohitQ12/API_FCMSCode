using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DrugMasterRepository : IDrugMaster
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DrugMasterRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<DrugMaster> InsertDrugMaster(DrugMaster lead)
        {
            try
            {
                var duplicate = await db.DrugMaster.FirstOrDefaultAsync(x => x.DrugName == lead.DrugName && x.DT_Id_FK == lead.DT_Id_FK 
                   && x.Strength == lead.Strength && x.UT_Id_FK == lead.UT_Id_FK);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("DrugMaster");
                    DrugMaster obj = new DrugMaster()
                    {
                        Id = id,
                        DrugName = lead.DrugName,
                        DT_Id_FK = lead.DT_Id_FK,
                        Strength = lead.Strength,
                        UT_Id_FK = lead.UT_Id_FK,
                        Unit = lead.Unit,
                        Description = lead.Description,
                        Instruction = lead.Instruction,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.DrugMaster.AddAsync(obj);
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
        public async Task<DrugMaster> UpdateDrugMaster(DrugMaster lead)
        {
            try
            {
                var result = await db.DrugMaster.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.DrugName = lead.DrugName;
                    result.DT_Id_FK = lead.DT_Id_FK;
                    result.Strength = lead.Strength;
                    result.UT_Id_FK = lead.UT_Id_FK;
                    result.Unit = lead.Unit;
                    result.Description = lead.Description;
                    result.Instruction = lead.Instruction;
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
        public async Task<List<GetAllDrugMaster>> GetAllDrugMaster()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DrugMaster
                                 join b in db.DrugType on a.DT_Id_FK equals b.Id
                                 join c in db.Unit on a.UT_Id_FK equals c.Id
                                 orderby a.Id descending
                                 select new GetAllDrugMaster
                                 {
                                     Id = a.Id,
                                     DrugName = a.DrugName,
                                     DT_Id_FK = a.DT_Id_FK,
                                     Drugtype = b.Type,
                                     Strength = a.Strength,
                                     UT_Id_FK = a.UT_Id_FK,
                                     Drugunit = c.DrugUnit,
                                     Description = a.Description,
                                     Instruction = a.Instruction,
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
        public async Task<DrugMaster> DeleteDrugMaster(int Id)
        {
            try
            {
                var result = await db.DrugMaster.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
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
        public async Task<GetDrugById> GetDrugMasterById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.DrugMaster
                             join b in db.DrugType on a.DT_Id_FK equals b.Id
                             join c in db.Unit on a.UT_Id_FK equals c.Id
                             where a.Id == Id
                             select new GetDrugById
                             {
                                 Id = a.Id,
                                 DrugName = a.DrugName,
                                 DT_Id_FK = a.DT_Id_FK,
                                 Drugtype = b.Type,
                                 Strength = a.Strength,
                                 UT_Id_FK = a.UT_Id_FK,
                                 Drugunit = c.DrugUnit,
                                 Description = a.Description,
                                 Instruction = a.Instruction,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<DrugTypeDD>> GetDrugTypeDD()
        {
            if (db != null)
            {
                var query = (from a in db.DrugType
                             where a.delete_flag == false && a.status == 1
                             select new DrugTypeDD
                             {
                                 Id = a.Id,
                                 Type = a.Type,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<UnitDD>> GetUnitDD(int DT_Id_FK)
        {
            if (db != null)
            {
                var query = (from a in db.Unit
                             where a.DType_Id_FK == DT_Id_FK && a.delete_flag == false && a.status == 1
                             select new UnitDD
                             {
                                 Id= a.Id,
                                 DType_Id_FK = DT_Id_FK,
                                 DrugUnit = a.DrugUnit,

                             }).ToListAsync();
                return await query;
            }
            return null;
        }
    }
}
