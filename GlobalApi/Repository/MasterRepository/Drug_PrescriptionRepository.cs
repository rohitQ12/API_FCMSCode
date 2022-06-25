using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Drug_PrescriptionRepository : IDrug_Prescription
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Drug_PrescriptionRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Drug_Prescription> InserDrug_Prescription(Drug_Prescription lead)
        {
            try
            {
                var checkval = await db.Drug_Prescription.FirstOrDefaultAsync(x => x.Prc_id == lead.Prc_id);
                if (checkval == null)
                {
                    int id = await primarykeyvalue.primary_key("DrugPrescription");
                    Drug_Prescription obj = new Drug_Prescription()
                    {
                        Prc_id = id,
                        Prc_CONS_id_FK = lead.Prc_CONS_id_FK,
                        Prc_drug_id_FK = lead.Prc_drug_id_FK,
                        Prc_dosage_qty = lead.Prc_dosage_qty,
                        Prc_drg_frequency_id_FK = lead.Prc_drg_frequency_id_FK,
                        Prc_custom_freuency = lead.Prc_custom_freuency,
                        Prc_intake = lead.Prc_intake,
                        Prc_intake_instaruction = lead.Prc_intake_instaruction,
                        Prc_drug_duration = lead.Prc_drug_duration,
                        Prc_duration_intermof = lead.Prc_duration_intermof,
                        Prc_created_by = "1",
                        Prc_created_date = DateTime.Now,
                        Prc_delete_flag = false,
                        Status = 1
                    };
                    var result = await db.Drug_Prescription.AddAsync(obj);
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

        public async Task<Drug_Prescription> UpdateDrug_Prescription(Drug_Prescription lead)
        {
            try
            {
                var result = await db.Drug_Prescription.FirstOrDefaultAsync(x => x.Prc_id == lead.Prc_id);
                if (result != null)
                {
                    result.Prc_id = lead.Prc_id;
                    result.Prc_CONS_id_FK = lead.Prc_CONS_id_FK;
                    result.Prc_drug_id_FK = lead.Prc_drug_id_FK;
                    result.Prc_dosage_qty = lead.Prc_dosage_qty;
                    result.Prc_drg_frequency_id_FK = lead.Prc_drg_frequency_id_FK;
                    result.Prc_custom_freuency = lead.Prc_custom_freuency;
                    result.Prc_intake = lead.Prc_intake;
                    result.Prc_intake_instaruction = lead.Prc_intake_instaruction;
                    result.Prc_drug_duration = lead.Prc_drug_duration;
                    result.Prc_duration_intermof = lead.Prc_duration_intermof;
                    result.Prc_modified_by = "1";
                    result.Prc_modified_date = DateTime.Now;
                    result.Status = 2;
                    result.Prc_delete_flag = false;
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

        public async Task<List<Drug_PrescriptionAll>> GetAllDrug_Prescription()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Drug_Prescription
                                 join b in db.Drug_Master on a.Prc_drug_id_FK equals b.Drg_mst_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.Drug_Units on b.Drg_unit_id_FK equals c.Drg_unit_id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Drug_Frequency on a.Prc_drg_frequency_id_FK equals d.Drg_freq_Id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Drug_Type on b.Drg_type_id_FK equals e.Drug_type_Id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Status on a.Status equals f.sts_id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.Drug_Manufacturers on b.Drg_manufacturer_id_FK equals g.Drg_manuf_id into glist
                                 from g in glist.DefaultIfEmpty()
                                 where a.Prc_id != 0
                                 orderby a.Prc_id descending
                                 select new Drug_PrescriptionAll
                                 {
                                     Prc_id = a.Prc_id,
                                     Prc_CONS_id_FK = a.Prc_CONS_id_FK,
                                     Prc_drug_id_FK = a.Prc_drug_id_FK,
                                     Prc_Drg_name = b.Drg_name,
                                     Prc_dosage_qty = a.Prc_dosage_qty,
                                     Prc_drug_type_id_FK = e.Drug_type_Id,
                                     Prc_drug_type_name = e.Drg_type_name,
                                     Prc_Unit_id_FK = c.Drg_unit_id,
                                     Drg_Unit = c.Drg_Unit,
                                     Prc_drg_frequency_id_FK = a.Prc_drg_frequency_id_FK,
                                     Drg_frq_name = d.Drg_frq_name,
                                     Drg_frq_order = d.Drg_frq_order,
                                     Prc_custom_freuency = a.Prc_custom_freuency,
                                     Prc_intake = a.Prc_intake,
                                     Prc_intake_instaruction = a.Prc_intake_instaruction,
                                     Prc_drug_duration = a.Prc_drug_duration,
                                     Prc_duration_intermof = a.Prc_duration_intermof,
                                     Status = a.Status,
                                     status_name = f.sts_name,
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

        public async Task<Drug_Prescription> DeleteDrug_Prescription(int Dtl_Id)
        {
            try
            {
                var result = await db.Drug_Prescription.FirstOrDefaultAsync(x => x.Prc_id == Dtl_Id);
                if (result != null)
                {
                    result.Prc_id = Dtl_Id;
                    result.Prc_delete_flag = true;
                    result.Prc_deleted_by = "1";
                    result.Prc_deleted_date = DateTime.Now;
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
        public async Task<Drug_PrescriptionAll> GetById_Drug_Prescription(int Prsc_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Drug_Prescription
                             join b in db.Drug_Master on a.Prc_drug_id_FK equals b.Drg_mst_id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.Drug_Units on b.Drg_unit_id_FK equals c.Drg_unit_id into clist
                             from c in clist.DefaultIfEmpty()
                             join d in db.Drug_Frequency on a.Prc_drg_frequency_id_FK equals d.Drg_freq_Id into dlist
                             from d in dlist.DefaultIfEmpty()
                             join e in db.Drug_Type on b.Drg_type_id_FK equals e.Drug_type_Id into elist
                             from e in elist.DefaultIfEmpty()
                             join f in db.Status on a.Status equals f.sts_id into flist
                             from f in flist.DefaultIfEmpty()
                             where a.Prc_id == Prsc_Id
                             select new Drug_PrescriptionAll
                             {
                                 Prc_id = a.Prc_id,
                                 Prc_CONS_id_FK = a.Prc_CONS_id_FK,
                                 Prc_drug_id_FK = a.Prc_drug_id_FK,
                                 Prc_Drg_name = b.Drg_name,
                                 Prc_dosage_qty = a.Prc_dosage_qty,
                                 Prc_drug_type_id_FK = e.Drug_type_Id,
                                 Prc_drug_type_name = e.Drg_type_name,
                                 Prc_Unit_id_FK = c.Drg_unit_id,
                                 Drg_Unit = c.Drg_Unit,
                                 Prc_drg_frequency_id_FK = a.Prc_drg_frequency_id_FK,
                                 Drg_frq_name = d.Drg_frq_name,
                                 Drg_frq_order = d.Drg_frq_order,
                                 Prc_custom_freuency = a.Prc_custom_freuency,
                                 Prc_intake = a.Prc_intake,
                                 Prc_intake_instaruction = a.Prc_intake_instaruction,
                                 Prc_drug_duration = a.Prc_drug_duration,
                                 Prc_duration_intermof = a.Prc_duration_intermof,
                                 Status = a.Status,
                                 status_name = f.sts_name,
                                 Remarks = a.Remarks
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
    }
}
