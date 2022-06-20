using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Drug_UnitRepository : IDrug_UnitRepository
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Drug_UnitRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<Drug_Units> InsertDrug_Unit(Drug_Units lead)
        {
            try
            {
                var duplicate = await db.Drug_Units.FirstOrDefaultAsync(x => x.Drg_Unit == lead.Drg_Unit);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Drug_Unit");
                    Drug_Units obj = new Drug_Units()
                    {
                        Drg_unit_id = id,
                        Drg_Type_Id_FK = lead.Drg_Type_Id_FK,
                        Drg_Unit = lead.Drg_Unit,
                        Drg_unit_created_by = "1",
                        Drg_unit_created_date = DateTime.Now,
                        Drg_unit_delete_flag = false,
                        Status = 1
                    };
                    var result = await db.Drug_Units.AddAsync(obj);
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
        public async Task<Drug_Units> UpdateDrug_Unit(Drug_Units lead)
        {
            try
            {
                var result = await db.Drug_Units.FirstOrDefaultAsync(x => x.Drg_unit_id == lead.Drg_unit_id);
                if (result != null)
                {
                    result.Drg_unit_id = lead.Drg_unit_id;
                    result.Drg_Type_Id_FK = lead.Drg_Type_Id_FK;
                    result.Drg_Unit = lead.Drg_Unit;
                    result.Drg_unit_modified_by = "1";
                    result.Drg_unit_modified_date = DateTime.Now;
                    result.Drg_unit_delete_flag = false;
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
        public async Task<List<Drug_UnitsAll>> GetAllDrug_Unit()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Drug_Units
                                 join b in db.Drug_Type on a.Drg_Type_Id_FK equals b.Drug_type_Id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.Status on a.Status equals c.sts_id
                                 orderby a.Drg_unit_id descending
                                 where a.Status != 6 && a.Drg_unit_delete_flag == false
                                 select new Drug_UnitsAll
                                 {
                                     Drg_unit_id = a.Drg_unit_id,
                                     Drg_Type_Id_FK = a.Drg_Type_Id_FK,
                                     Drg_Type_Name = b.Drg_type_name,
                                     Drg_Unit = a.Drg_Unit,
                                     Drg_unit_delete_flag = a.Drg_unit_delete_flag,
                                     Status = a.Status,
                                     status_name = c.sts_name,
                                     Remarks = a.Remarks
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
        public async Task<Drug_Units> DeleteDrug_Unit(int Id)
        {
            try
            {
                var result = await db.Drug_Units.FirstOrDefaultAsync(x => x.Drg_unit_id == Id);
                if (result != null)
                {
                    result.Drg_unit_id = Id;
                    result.Drg_unit_delete_flag = true;
                    result.Status = 6;
                    result.Drg_unit_deleted_by = "1";
                    result.Drg_unit_deleted_date = DateTime.Now;
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

        public async Task<List<Drug_UnitDD>> GetDD_Drug_Unit()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Drug_Units
                                 join b in db.Drug_Type on a.Drg_Type_Id_FK equals b.Drug_type_Id
                                 where a.Status != 6 && a.Status == 3 && a.Drg_unit_delete_flag == false
                                 orderby a.Drg_unit_id descending
                                 select new Drug_UnitDD
                                 {
                                     Drg_unit_id = a.Drg_unit_id,
                                     Drg_Type_Id_FK = a.Drg_Type_Id_FK,
                                     Drug_type_name = b.Drg_type_name,
                                     Drg_Unit = a.Drg_Unit
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
        public async Task<string> ApproveDrug_Unit(ApproveDrgunit lead)
        {
            try
            {
                if (lead.Drg_unit_id != 0)
                {
                    var result = await db.Drug_Units.Where(x => x.Drg_unit_id == lead.Drg_unit_id).FirstOrDefaultAsync();
                    if (result.Status != 3)
                    {
                        result.Status = 3;
                        if (lead.Remarks == null)
                        {
                            result.Remarks = "OK";
                        }
                        else
                            result.Remarks = lead.Remarks;
                        await db.SaveChangesAsync();
                        return "Discipline is Approved";
                    }
                    else
                        return "Already Active";
                }
                else
                    return "Cannot Approve Default Discipline";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
