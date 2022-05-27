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
        public async Task<Designation> InsertDesignation(Designation lead)
        {
            try
            {
                //var duplicate = await db.Designation.FirstOrDefaultAsync(x => x.designation_code == lead.designation_code || x.designation_desc == lead.designation_desc);
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
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Designation> UpdateDesignation(Designation lead)
        {
            try
            {
                var result = await db.Designation.FirstOrDefaultAsync(x => x.designation_id == lead.designation_id);
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
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<Designation>> GetAllDesignation()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Designation
                                 where a.designation_id != 0
                                 orderby a.designation_id descending
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
        public async Task<List<Designation_DD>> GetDesignation_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Designation
                             where a.delete_flag == false && a.status != 6 
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
        public async Task<Designation> DeleteDesignation(int designation_id)
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
                    return result;
                }
                return null;
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
                             where a.designation_id == designation_id && a.designation_id != 0
                             select new DesignationById
                             {
                                 designation_id = a.designation_id,
                                 designation_code = a.designation_code,
                                 designation_desc = a.designation_desc,
                                 delete_flag = a.delete_flag,
                                 status = a.status,

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> ApproveDesignation(int designation_id, string? Remarks)
        {
            try
            {
                if(designation_id != 0)
                {
                    var result = await db.Designation.Where(x => x.designation_id == designation_id).FirstOrDefaultAsync();
                    if (result.status != 3)
                    {
                        //result.cntry_id = cntry_id;
                        result.status = 3;
                        if (Remarks == null)
                        {
                            result.Remarks = "OK";
                        }
                        else
                            result.Remarks = Remarks;
                        await db.SaveChangesAsync();
                        return "Designation is Approved";
                    }
                    else
                        return "Already Active";
                }
                else
                    return "Cannot Approve Default Designation";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
