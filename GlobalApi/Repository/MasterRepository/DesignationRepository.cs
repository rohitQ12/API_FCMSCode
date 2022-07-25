using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using Microsoft.EntityFrameworkCore;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class DesignationRepository : IDesignation
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DesignationRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertDesignation(Designation lead)
        {
            try
            {
                var designation_desc = await db.Designation.FirstOrDefaultAsync(x => x.designation_desc == lead.designation_desc);
                var designation_code = await db.Designation.FirstOrDefaultAsync(x => x.designation_code == lead.designation_code);
                if (designation_code == null)
                {
                    if (designation_desc == null)
                    {
                        int id = await primarykeyvalue.primary_key("Designation");
                        Designation obj = new Designation()
                        {
                            designation_id = id,
                            //designation_code = "V" + Convert.ToString(id), 
                            designation_code = lead.designation_code,
                            designation_desc = lead.designation_desc,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.Designation.AddAsync(obj);
                        await db.SaveChangesAsync();
                        return "Designation Added Successfully";
                    }
                    return "Designation Desc Already Exists";
                }
                return "Designation Code Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateDesignation(Designation lead)
        {
            try
            {
                var result = await db.Designation.FirstOrDefaultAsync(x => x.designation_id == lead.designation_id);
                var DesigDesc = await db.Designation.FirstOrDefaultAsync(x => x.designation_desc == lead.designation_desc);
                var DesigCode = await db.Designation.FirstOrDefaultAsync(x => x.designation_code == lead.designation_code);
                if (DesigCode == null || result.designation_code == lead.designation_code)
                {
                    if (DesigDesc == null || result.designation_desc == lead.designation_desc)
                    {
                        if (result != null)
                        {
                            result.designation_id = lead.designation_id;
                            result.designation_code = lead.designation_code;
                            result.designation_desc = lead.designation_desc;
                            result.modified_by = 1;
                            result.modified_date = DateTime.Now;
                            result.delete_flag = false;
                            result.status = 2;
                            await db.SaveChangesAsync();
                            return "Designation Updated Successfully";
                        }
                        return "Designation not Found";
                    }
                    return "Designation Desc Already Exists";
                }
                return "Designation Code Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<GetAllDesignation>> GetAllDesignation()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Designation
                                 join b in db.Status on a.status equals b.sts_id
                                 where a.designation_id != 0
                                 orderby a.designation_id descending
                                 select new GetAllDesignation
                                 {
                                     designation_id = a.designation_id,
                                     designation_code = a.designation_code,
                                     designation_desc = a.designation_desc,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = b.sts_name,
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
        public async Task<List<Designation_DD>> GetDesignation_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Designation
                             where a.delete_flag == false && a.status == 3
                             && a.designation_id != 0
                             select new Designation_DD
                             {
                                 designation_id = a.designation_id,
                                 designation_code = a.designation_code,
                                 designation_desc = a.designation_desc
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> DeleteDesignation(int designation_id)
        {
            try
            {
                var result = await db.Designation.FirstOrDefaultAsync(x => x.designation_id == designation_id);
                if (result != null)
                {
                    result.designation_id = designation_id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Designation Deleted Successfully";
                }
                return "Designation Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<DesignationById> GetDesignationById(int designation_id)
        {
            if (db != null)
            {
                var query = (from a in db.Designation
                             join b in db.Status on a.status equals b.sts_id
                             where a.designation_id == designation_id && a.designation_id != 0
                             select new DesignationById
                             {
                                 designation_id = a.designation_id,
                                 designation_code = a.designation_code,
                                 designation_desc = a.designation_desc,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = b.sts_name,
                                 Remarks = a.Remarks,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> ApproveDesignation(ApproveDesignation lead)
        {
            try
            {
                if (lead.designation_id != 0)
                {
                    var result = await db.Designation.Where(x => x.designation_id == lead.designation_id).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        //result.cntry_id = lead.cntry_id;
                        result.status = 3;
                        if (lead.Remarks == null)
                        {
                            result.Remarks = "OK";
                        }
                        else
                            result.Remarks = lead.Remarks;
                        await db.SaveChangesAsync();
                        return "Designation Approved Succesfully";
                    }
                    return "Designation Doesn't Exists";
                }
                return "Designation with Id 0 Cannot be Approved";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
