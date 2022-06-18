using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Drug_TypeRepository : IDrug_TypeRepository
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Drug_TypeRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Drug_Type> InsertDrug_Type(Drug_Type lead)
        {
            try
            {
                var duplicate = await db.Drug_Type.FirstOrDefaultAsync(x => x.Drug_type_Id == lead.Drug_type_Id);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Drug_Type");
                    Drug_Type obj = new Drug_Type()
                    {
                        Drug_type_Id = id,
                        Drg_type_name = lead.Drg_type_name,
                        Drg_type_created_by = "1",
                        Drg_type_created_date = DateTime.Now,
                        Drg_type_delete_flag = false,
                        Status  = 1
                    };
                    var result = await db.Drug_Type.AddAsync(obj);      
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
        public async Task<Drug_Type> UpdateDrug_Type(Drug_Type lead)
        {
            try
            {
                var result = await db.Drug_Type.FirstOrDefaultAsync(x => x.Drug_type_Id == lead.Drug_type_Id);
                if (result != null)
                {
                    result.Drug_type_Id = lead.Drug_type_Id;
                    result.Drg_type_name = lead.Drg_type_name;
                    result.Drg_type_modified_by = "1";
                    result.Drg_type_modified_date = DateTime.Now;
                    result.Drg_type_delete_flag = false;
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
        public async Task<List<Drug_TypeAll>> GetAllDrug_Type()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Drug_Type
                                 join b in db.Status on a.Status equals b.sts_id
                                 orderby a.Drug_type_Id descending
                                 where a.Status != 6 && a.Drg_type_delete_flag == false
                                 select new Drug_TypeAll
                                 {
                                     Drug_type_Id = a.Drug_type_Id,
                                     Drg_type_name = a.Drg_type_name,
                                     Drg_type_delete_flag = a.Drg_type_delete_flag,
                                     Status = a.Status,
                                     status_name = b.sts_name,
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
        public async Task<Drug_Type> DeleteDrug_Type(int Id)
        {
            try
            {
                var result = await db.Drug_Type.FirstOrDefaultAsync(x => x.Drug_type_Id == Id);
                if (result != null)
                {
                    result.Drug_type_Id = Id;
                    result.Drg_type_delete_flag = true;
                    result.Status = 6;
                    result.Drg_type_deleted_by = "1";
                    result.Drg_type_deleted_date = DateTime.Now;
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
        public async Task<List<Drug_TypeDD>> GetDrug_Type_DD()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Drug_Type
                                 orderby a.Drug_type_Id descending
                                 where a.Status != 6 && a.Status == 3 && a.Drg_type_delete_flag == false
                                 select new Drug_TypeDD
                                 {
                                     Drug_type_Id = a.Drug_type_Id,
                                     Drg_type_name = a.Drg_type_name
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
        public async Task<string> ApproveDrug_Type(DrugTypeapprove lead)
        {
            try
            {
                if (lead.Drug_type_Id != 0)
                {
                    var result = await db.Drug_Type.Where(x => x.Drug_type_Id == lead.Drug_type_Id).FirstOrDefaultAsync();
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
