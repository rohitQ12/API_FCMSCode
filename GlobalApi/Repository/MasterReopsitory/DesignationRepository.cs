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
        public async Task<string> InsertDesignation(Designation Designation)
        {
            try
            {
                var designation_desc = await db.Designation.FirstOrDefaultAsync(x => x.designation_desc == Designation.designation_desc);
                var designation_code = await db.Designation.FirstOrDefaultAsync(x => x.designation_code == Designation.designation_code);
                if (designation_code != null)
                {
                    return "Designation Code Already Exists";
                }
                if (designation_desc != null)
                {
                    return "Designation Desc Already Exists";
                }
                int id = await primarykeyvalue.primary_key("Designation");
                Designation obj = new Designation()
                {
                    designation_id = id,
                    designation_code = Designation.designation_code,
                    designation_desc = Designation.designation_desc,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Designation.AddAsync(obj);
                await db.SaveChangesAsync();
                return "Designation Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //Online Portals
        public async Task<string> InsertDesignation_Online(Designation_Online Designation_Online)
        {
            try
            {
                var designation_desc = await db.Designation_Online.FirstOrDefaultAsync(x => x.designation_desc == Designation_Online.designation_desc);
                var designation_code = await db.Designation_Online.FirstOrDefaultAsync(x => x.designation_code == Designation_Online.designation_code);
                if (designation_code != null)
                {
                    return "Designation Code Already Exists";
                }
                if (designation_desc != null)
                {
                    return "Designation Desc Already Exists";
                }
                int id = await primarykeyvalue.primary_key("Designation_Online");
                Designation_Online obj = new Designation_Online()
                {
                    designation_id = id,
                    designation_code = Designation_Online.designation_code,
                    designation_desc = Designation_Online.designation_desc,
                    created_by = Designation_Online.created_by,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    record_status = 1
                };
                var result = await db.Designation_Online.AddAsync(obj);
                await db.SaveChangesAsync();
                return "Designation Added Successfully";
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
                if (DesigCode != null)
                {
                    if (DesigCode.designation_code != result.designation_code)
                    {
                        return "Designation Code Already Exists";
                    }
                }
                if (DesigDesc != null)
                {
                    if (DesigDesc.designation_desc != result.designation_desc)
                    {
                        return "Designation Desc Already Exists";
                    }
                }

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
                return "Designation Didn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //Online Portals
        public async Task<string> UpdateDesignation_Online(Designation_Online lead)
        {
            try
            {
                var result = await db.Designation_Online.FirstOrDefaultAsync(x => x.designation_id == lead.designation_id);
                var DesigDesc = await db.Designation_Online.FirstOrDefaultAsync(x => x.designation_desc == lead.designation_desc);
                var DesigCode = await db.Designation_Online.FirstOrDefaultAsync(x => x.designation_code == lead.designation_code);
                if (DesigCode != null)
                {
                    if (DesigCode.designation_code != result.designation_code)
                    {
                        return "Designation Code Already Exists";
                    }
                }
                if (DesigDesc != null)
                {
                    if (DesigDesc.designation_desc != result.designation_desc)
                    {
                        return "Designation Desc Already Exists";
                    }
                }

                if (result != null)
                {
                    result.designation_id = lead.designation_id;
                    result.designation_code = lead.designation_code;
                    result.designation_desc = lead.designation_desc;
                    result.modified_by = lead.modified_by;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.record_status = 2;
                    await db.SaveChangesAsync();
                    return "Designation Updated Successfully";
                }
                return "Designation Didn't Exists";
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
                var query = (from a in db.Designation
                             join b in db.Status on a.status equals b.sts_id
                             where a.designation_id != 0 && a.designation_desc != "None"
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //Online Portals
        public async Task<List<GetAllDesignation_Online>> GetAllDesignation_Online()
        {
            try
            {
                var query = (from a in db.Designation_Online
                             join b in db.Status on a.record_status equals b.sts_id
                             where a.designation_id != 0 && a.designation_desc != "None"
                             orderby a.designation_id descending
                             select new GetAllDesignation_Online
                             {
                                 designation_id = a.designation_id,
                                 designation_code = a.designation_code,
                                 designation_desc = a.designation_desc,
                                 delete_flag = a.delete_flag,
                                 record_status = a.record_status,
                                 sts_name = b.sts_name,
                                 remarks = a.remarks,
                             });
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        public async Task<List<Designation_DD>> GetDesignation_DD()
        {
            var query = (from a in db.Designation
                         where a.delete_flag == false && a.status == 3
                         && a.designation_id != 0
                         orderby a.designation_desc
                         select new Designation_DD
                         {
                             designation_id = a.designation_id,
                             designation_code = a.designation_code,
                             designation_desc = a.designation_desc
                         }).ToListAsync();
            
            var designationList = await query;

            var noneData = designationList.FirstOrDefault(d => d.designation_desc == "None");
            designationList.RemoveAll(d => d.designation_desc == "None");
            designationList.Add(noneData);
            return designationList;
        }
        //Online Protals
        public async Task<List<Designation_DD_Online>> GetDesignation_DD_Online ()
        {
            var query = (from a in db.Designation_Online
                         where a.delete_flag == false && a.record_status == 3
                         && a.designation_id != 0
                         orderby a.designation_desc
                         select new Designation_DD_Online
                         {
                             designation_id = a.designation_id,
                             designation_code = a.designation_code,
                             designation_desc = a.designation_desc
                         }).ToListAsync();
            return await query;
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
        //Online Portals
        public async Task<string> DeleteDesignation_Online(int designation_id)
        {
            try
            {
                var result = await db.Designation_Online.FirstOrDefaultAsync(x => x.designation_id == designation_id);
                if (result != null)
                {
                    result.designation_id = designation_id;
                    result.delete_flag = true;
                    result.record_status = 6;
                    result.deleted_by = result.deleted_by;
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
        //Online Portals
        public async Task<DesignationById_Online> GetDesignationById_Online(int designation_id)
        {
            var query = (from a in db.Designation_Online
                         join b in db.Status on a.record_status equals b.sts_id
                         where a.designation_id == designation_id && a.designation_id != 0
                         select new DesignationById_Online
                         {
                             designation_id = a.designation_id,
                             designation_code = a.designation_code,
                             designation_desc = a.designation_desc,
                             delete_flag = a.delete_flag,
                             record_status = a.record_status,
                             sts_name = b.sts_name,
                             remarks = a.remarks,

                         }).FirstOrDefaultAsync();
            return await query;
        }


        //}
        //public async Task<string> ApproveDesignation(ApproveDesignation ApproveDesignation)
        //{
        //    try
        //    {
        //        var result = await db.Designation.Where(x => x.designation_id == ApproveDesignation.designation_id).FirstOrDefaultAsync();
        //        if (result.status != 3)
        //        {
        //            result.status = 3;
        //            if (ApproveDesignation.Remarks == null)
        //            {
        //                result.Remarks = "OK";
        //            }
        //            else
        //                result.Remarks = ApproveDesignation.Remarks;
        //            await db.SaveChangesAsync();
        //            return "Designation Approved Successfully";
        //        }
        //        else
        //            return "Designation Details Does Not Exists";
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //}
        public async Task<string> ApproveDesignation(ApproveDesignation ApproveDesignation)
        {
            try
            {
                var result = await db.Designation.FirstOrDefaultAsync(x => x.designation_id == ApproveDesignation.designation_id);
                if (result != null)
                {
                    result.status = 3;
                    if (ApproveDesignation.Remarks.Length <= 0)
                    {
                        ApproveDesignation.Remarks = "OK";
                    }
                    else
                        result.Remarks = ApproveDesignation.Remarks;
                    await db.SaveChangesAsync();
                    return "Designation Approved Successfully";
                }
                return "Designation Details Does Not Exists";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        //Online Portals 
        public async Task<string> ApproveDesignation_Online(ApproveDesignation_Online ApproveDesignation_Online)
        {
            try
            {
                var result = await db.Designation_Online.FirstOrDefaultAsync(x => x.designation_id == ApproveDesignation_Online.designation_id);
                if (result != null)
                {
                    result.record_status = 3;
                    if (ApproveDesignation_Online.remarks.Length <= 0)
                    {
                        ApproveDesignation_Online.remarks = "OK";
                    }
                    else
                        result.remarks = ApproveDesignation_Online.remarks;
                    await db.SaveChangesAsync();
                    return "Designation Approved Successfully";
                }
                return "Designation Details Does Not Exists";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
