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
                var duplicate = await db.Drug_Master.FirstOrDefaultAsync(x => x.Drg_name == lead.Drg_name && x.Drg_type_id_FK == lead.Drg_type_id_FK 
                   && x.Drg_strength == lead.Drg_strength && x.Drg_unit_id_FK == lead.Drg_unit_id_FK);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("DrugMaster");
                    DrugMaster obj = new DrugMaster()
                    {
                        Drg_mst_id = id,
                        Drug_code = lead.Drug_code,
                        Drg_name = lead.Drg_name,
                        Drg_type_id_FK = lead.Drg_type_id_FK,
                        Drg_strength = lead.Drg_strength,
                        Drg_unit_id_FK = lead.Drg_unit_id_FK,
                        Discription = lead.Discription,
                        Instruction = lead.Instruction,
                        Drg_mst_created_by = "1",
                        Drg_mst_created_date = DateTime.Now,
                        Drg_mst_delete_flag = false,
                        Status = 1
                    };
                    var result = await db.Drug_Master.AddAsync(obj);
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
                var result = await db.Drug_Master.FirstOrDefaultAsync(x => x.Drg_mst_id == lead.Drg_mst_id);
                if (result != null)
                {
                    result.Drg_mst_id = lead.Drg_mst_id;
                    result.Drug_code = lead.Drug_code;
                    result.Drg_name = lead.Drg_name;
                    result.Drg_type_id_FK = lead.Drg_type_id_FK;
                    result.Drg_strength = lead.Drg_strength;
                    result.Drg_unit_id_FK = lead.Drg_unit_id_FK;
                    result.Discription = lead.Discription;
                    result.Instruction = lead.Instruction;
                    result.Drg_mst_modified_by = "1";
                    result.Drg_mst_modified_date = DateTime.Now;
                    result.Drg_mst_delete_flag = false;
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
        public async Task<List<GetAllDrugMaster>> GetAllDrugMaster()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Drug_Master
                                 join b in db.Drug_Type on a.Drg_type_id_FK equals b.Drug_type_Id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.Drug_Units on a.Drg_unit_id_FK equals c.Drg_unit_id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Status on a.Status equals d.sts_id
                                 where a.Drg_mst_id !=0
                                 orderby a.Drg_mst_id descending
                                 select new GetAllDrugMaster
                                 {
                                     Drg_mst_id = a.Drg_mst_id,
                                     Drug_code = a.Drug_code,
                                     Drg_name = a.Drg_name,
                                     Drg_type_id_FK = a.Drg_type_id_FK,
                                     Drg_type_name = b.Drg_type_name,
                                     Drg_strength = a.Drg_strength,
                                     Drg_Unit = c.Drg_Unit,
                                     Drg_unit_id_FK = c.Drg_unit_id,
                                     Discription = a.Discription,
                                     Instruction = a.Instruction,
                                     Drg_mst_delete_flag = a.Drg_mst_delete_flag,
                                     Status = a.Status,
                                     status_name = d.sts_name,
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
        public async Task<DrugMaster> DeleteDrugMaster(int Id)
        {
            try
            {
                var result = await db.Drug_Master.FirstOrDefaultAsync(x => x.Drg_mst_id == Id);
                if (result != null)
                {
                    result.Drg_mst_id = Id;
                    result.Drg_mst_delete_flag = true;
                    result.Status = 6;
                    result.Drg_mst_deletd_by = "1";
                    result.Drg_mst_deleted_date = DateTime.Now;
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
        public async Task<GetAllDrugMaster> GetDrugMasterById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.Drug_Master
                             join b in db.Drug_Type on a.Drg_type_id_FK equals b.Drug_type_Id
                             join c in db.Drug_Units on a.Drg_unit_id_FK equals c.Drg_unit_id
                             where a.Drg_mst_id == Id
                             select new GetAllDrugMaster
                             {
                                 Drg_mst_id = a.Drg_mst_id,
                                 Drug_code = a.Drug_code,
                                 Drg_name = a.Drg_name,
                                 Drg_type_id_FK = a.Drg_type_id_FK,
                                 Drg_type_name = b.Drg_type_name,
                                 Drg_strength = a.Drg_strength,
                                 Drg_Unit = c.Drg_Unit,
                                 Drg_unit_id_FK = c.Drg_unit_id,
                                 Discription = a.Discription,
                                 Instruction = a.Instruction,
                                 Drg_mst_delete_flag = a.Drg_mst_delete_flag,
                                 Status = a.Status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

        public async Task<List<DrugMasterDD>> GetDrugMaster_DD()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Drug_Master
                                 join b in db.Drug_Type on a.Drg_type_id_FK equals b.Drug_type_Id
                                 join c in db.Drug_Units on a.Drg_unit_id_FK equals c.Drg_unit_id
                                 where a.Status != 6 && a.Status == 3 && a.Drg_mst_delete_flag == false
                                 orderby a.Drg_mst_id descending
                                 select new DrugMasterDD
                                 {
                                     Drg_mst_id = a.Drg_mst_id,
                                     Drug_code = a.Drug_code,
                                     Drg_name = a.Drg_name,
                                     Drg_type_id_FK = a.Drg_type_id_FK,
                                     Drg_type_name = b.Drg_type_name,
                                     Drg_strength = a.Drg_strength,
                                     Drg_Unit = c.Drg_Unit,
                                     Drg_unit_id_FK = c.Drg_unit_id
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
        public async Task<string> ApproveDrugMaster(ApproveDrgMst lead)
        {
            try
            {
                if (lead.Drg_mst_id != 0)
                {
                    var result = await db.Drug_Master.Where(x => x.Drg_mst_id == lead.Drg_mst_id).FirstOrDefaultAsync();
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
