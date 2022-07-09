using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class LAB_DescriptionRepository : ILAB_Description
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public LAB_DescriptionRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<LAB_Description> InsertLab_Description(LAB_Description lead)
        {
            try
            {
                var duplicate = await db.LAB_Description.FirstOrDefaultAsync(x => x.Lab_Invt_Id == lead.Lab_Invt_Id && x.Lab_SubInvt_Id == lead.Lab_SubInvt_Id);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Lab_Description");
                    LAB_Description obj = new LAB_Description()
                    {
                        Lab_DescId = id,
                        Lab_Invt_Id = lead.Lab_Invt_Id,
                        Lab_SubInvt_Id = lead.Lab_SubInvt_Id,
                        Lab_Description = lead.Lab_Description,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.LAB_Description.AddAsync(obj);
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
        public async Task<LAB_Description> UpdateLab_Description(LAB_Description lead)
        {
            try
            {
                var result = await db.LAB_Description.FirstOrDefaultAsync(x => x.Lab_DescId == lead.Lab_DescId);
                if (result != null)
                {
                    //result.Lab_DescId = lead.Lab_DescId;
                    result.Lab_Invt_Id = lead.Lab_Invt_Id;
                    result.Lab_SubInvt_Id = lead.Lab_SubInvt_Id;
                    result.Lab_Description = lead.Lab_Description;
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
        public async Task<List<GetAllLAB_Desc>> GetAllLab_Description()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.LAB_Description
                                 join b in db.LAB_INVESTIGATIONS on a.Lab_Invt_Id equals b.Id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.LAB_SUBINVESTIGATIONS on a.Lab_SubInvt_Id equals c.Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 orderby a.Lab_DescId descending
                                 select new GetAllLAB_Desc
                                 {
                                     Lab_DescId = a.Lab_DescId,
                                     Lab_Invt_Id = a.Lab_Invt_Id,
                                     Category = b.Category,
                                     Lab_SubInvt_Id = a.Lab_SubInvt_Id,
                                     Sub_Category = c.Sub_Category,
                                     Lab_Description = a.Lab_Description,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     Remarks = a.Remarks,

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
        public async Task<List<LabDesc_DD>> GetLabDesc_DD()
        {
            if (db != null)
            {
                var query = (from a in db.LAB_Description
                             where a.delete_flag == false && a.status == 3
                             select new LabDesc_DD
                             {
                                 Lab_DescId = a.Lab_DescId,
                                 Lab_SubInvt_Id = a.Lab_SubInvt_Id,
                                 Lab_Description = a.Lab_Description,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<LabDesc_DD>> LabDesc_DD_ByCat_Id(int Cat_Id)
        {
            if (db != null)
            {
                var query = (from a in db.LAB_Description
                             where a.Lab_SubInvt_Id == Cat_Id && a.delete_flag == false 
                             && a.status == 3
                             select new LabDesc_DD
                             {
                                 Lab_DescId = a.Lab_DescId,
                                 Lab_SubInvt_Id = a.Lab_SubInvt_Id,
                                 Lab_Description = a.Lab_Description,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

        public async Task<LAB_Description> DeleteLab_Description(int Lab_DescId)
        {
            try
            {
                var result = await db.LAB_Description.FirstOrDefaultAsync(x => x.Lab_DescId == Lab_DescId);
                if (result != null)
                {
                    result.Lab_DescId = Lab_DescId;
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
        public async Task<GetLabDesc_ById> GetLabDesc_ById(int Lab_DescId)
        {
            if (db != null)
            {
                var query = (from a in db.LAB_Description
                             join b in db.LAB_INVESTIGATIONS on a.Lab_Invt_Id equals b.Id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.LAB_SUBINVESTIGATIONS on a.Lab_SubInvt_Id equals c.Id into clist
                             from c in clist.DefaultIfEmpty()
                             where a.Lab_DescId == Lab_DescId
                             select new GetLabDesc_ById
                             {
                                 Lab_DescId = a.Lab_DescId,
                                 Lab_Invt_Id = a.Lab_Invt_Id,
                                 Category = b.Category,
                                 Lab_SubInvt_Id = a.Lab_SubInvt_Id,
                                 Sub_Category = c.Sub_Category,
                                 Lab_Description = a.Lab_Description,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 Remarks = a.Remarks,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> ApproveLAB_Description(ApproveLab_Desc lead)
        {
            try
            {
                var result = await db.LAB_Description.Where(x => x.Lab_DescId == lead.Lab_DescId).FirstOrDefaultAsync();
                if (result.status != 3)
                {
                    //result.VL_Id = lead.VL_Id;
                    result.status = 3;
                    if (lead.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = lead.Remarks;
                    await db.SaveChangesAsync();
                    return "Vle is Approved";
                }
                else
                    return "Already Active";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
