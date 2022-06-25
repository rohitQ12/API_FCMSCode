using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Drug_FrequencyRepository : IDrug_FrequencyRepository
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Drug_FrequencyRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<Drug_Frequency> InsertDrug_Frequency(Drug_Frequency lead)
        {
            try
            {
                var duplicate = await db.Drug_Frequency.FirstOrDefaultAsync(x => x.Drg_frq_name == lead.Drg_frq_name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Drug_Frequency");
                    Drug_Frequency obj = new Drug_Frequency()
                    {
                        Drg_freq_Id = id,
                        Drg_frq_name = lead.Drg_frq_name,
                        Drg_frq_order = lead.Drg_frq_order,
                        Drg_frq_created_by = "1",
                        Drg_frq_created_date = DateTime.Now,
                        Drg_frq_delete_flag = false,
                        status = 1
                    };
                    var result = await db.Drug_Frequency.AddAsync(obj);
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
        public async Task<Drug_Frequency> UpdateDrug_Frequency(Drug_Frequency lead)
        {
            try
            {
                var result = await db.Drug_Frequency.FirstOrDefaultAsync(x => x.Drg_freq_Id == lead.Drg_freq_Id);
                if (result != null)
                {
                    result.Drg_freq_Id = lead.Drg_freq_Id;
                    result.Drg_frq_name = lead.Drg_frq_name;
                    result.Drg_frq_order = lead.Drg_frq_order;
                    result.Drg_frq_modified_by = "1";
                    result.Drg_frq_modified_date = DateTime.Now;
                    result.Drg_frq_delete_flag = false;
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
        public async Task<List<Drug_FrequencyAll>> GetAllDrug_Frequency()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Drug_Frequency
                                 join b in db.Status on a.status equals b.sts_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 where a.Drg_freq_Id !=0
                                 orderby a.Drg_freq_Id descending
                                 select new Drug_FrequencyAll
                                 {
                                     Drg_freq_Id = a.Drg_freq_Id,
                                     Drg_frq_name = a.Drg_frq_name,
                                     Drg_frq_order = a.Drg_frq_order,
                                     Drg_frq_delete_flag = a.Drg_frq_delete_flag,
                                     status = a.status,
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
        public async Task<Drug_Frequency> DeleteDrug_Frequency(int Id)
        {
            try
            {
                var result = await db.Drug_Frequency.FirstOrDefaultAsync(x => x.Drg_freq_Id == Id);
                if (result != null)
                {
                    result.Drg_freq_Id = Id;
                    result.Drg_frq_delete_flag = true;
                    result.status = 6;
                    result.Drg_frq_deleted_by = "1";
                    result.Drg_frq_deleted_date = DateTime.Now;
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

        public async Task<List<Drug_FrequencyDD>> GetADrug_Frequency_DD()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Drug_Frequency
                                 where a.status != 6 && a.status ==3 && a.Drg_frq_delete_flag == false
                                 orderby a.Drg_freq_Id descending
                                 select new Drug_FrequencyDD
                                 {
                                     Drg_freq_Id = a.Drg_freq_Id,
                                     Drg_frq_name = a.Drg_frq_name,
                                     Drg_frq_order = a.Drg_frq_order
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
        public async Task<string> ApproveDrug_Frequency(DrugFrequencyapprove lead)
        {
            try
            {
                if (lead.Drg_freq_Id != 0)
                {
                    var result = await db.Drug_Frequency.Where(x => x.Drg_freq_Id == lead.Drg_freq_Id).FirstOrDefaultAsync();
                    if (result.status != 3)
                    {
                        result.status = 3;
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
