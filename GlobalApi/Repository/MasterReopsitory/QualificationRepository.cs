using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class QualificationRepository : IQualification
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public QualificationRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<string> InsertQualification(Qualification Qualification)
        {
            try
            {
                var qualification_code = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_code == Qualification.qualification_code);
                var qualification_Name = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_Name == Qualification.qualification_Name);
                if (qualification_code != null)
                {
                    return "Qualification Code Already Exists";
                }
                if (qualification_Name != null)
                {
                    return "Qualification Name Already Exists";
                }
                int id = await primarykeyvalue.primary_key("Qualification");
                Qualification obj = new Qualification()
                {
                    qualification_id = id,
                    qualification_code = Qualification.qualification_code,
                    qualification_Name = Qualification.qualification_Name,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Qualification.AddAsync(obj);
                await db.SaveChangesAsync();
                return "Qualification Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //Online Portals
        public async Task<string> InsertQualification_Online(Qualification_Online Qualification_Online)
        {
            try
            {
                var qualification_code = await db.Qualification_Online.FirstOrDefaultAsync(x => x.qualification_code == Qualification_Online.qualification_code);
                var qualification_name = await db.Qualification_Online.FirstOrDefaultAsync(x => x.qualification_name == Qualification_Online.qualification_name);
                if (qualification_code != null)
                {
                    return "Qualification Code Already Exists";
                }
                if (qualification_name != null)
                {
                    return "Qualification Name Already Exists";
                }
                int id = await primarykeyvalue.primary_key("Qualification_Online");
                Qualification_Online obj = new Qualification_Online()
                {
                    qualification_id = id,
                    qualification_code = Qualification_Online.qualification_code,
                    qualification_name = Qualification_Online.qualification_name,
                    created_by = Qualification_Online.created_by,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    record_status = 1
                };
                var result = await db.Qualification_Online.AddAsync(obj);
                await db.SaveChangesAsync();
                return "Qualification Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public async Task<string> UpdateQualification(Qualification Qualification)
        {
            try
            {
                var qualification = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_id == Qualification.qualification_id);
                var qualification_code = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_code == Qualification.qualification_code);
                var qualification_Name = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_Name == Qualification.qualification_Name);

                if (qualification_code != null)
                {
                    if (qualification_code.qualification_code != qualification.qualification_code)
                    {
                        return "Qualification Code Already Exists";
                    }
                }
                if (qualification_Name != null)
                {
                    if (qualification_Name.qualification_Name != qualification.qualification_Name)
                    {
                        return "Qualification Name Already Exists";
                    }
                }
                if (qualification != null)
                {
                    qualification.qualification_id = Qualification.qualification_id;
                    qualification.qualification_code = Qualification.qualification_code;
                    qualification.qualification_Name = Qualification.qualification_Name;
                    qualification.modified_by = 1;
                    qualification.modified_date = DateTime.Now;
                    qualification.delete_flag = false;
                    qualification.status = 2;
                    await db.SaveChangesAsync();
                    return "Qualification Updated Successfully";
                }
                return "Qualification Didn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //Online Portals
        public async Task<string> UpdateQualification_Online(Qualification_Online Qualification_Online)
        {
            try
            {
                var qualification = await db.Qualification_Online.FirstOrDefaultAsync(x => x.qualification_id == Qualification_Online.qualification_id);
                var qualification_code = await db.Qualification_Online.FirstOrDefaultAsync(x => x.qualification_code == Qualification_Online.qualification_code);
                var qualification_name = await db.Qualification_Online.FirstOrDefaultAsync(x => x.qualification_name == Qualification_Online.qualification_name);

                if (qualification_code != null)
                {
                    if (qualification_code.qualification_code != qualification.qualification_code)
                    {
                        return "Qualification Code Already Exists";
                    }
                }
                if (qualification_name != null)
                {
                    if (qualification_name.qualification_name != qualification.qualification_name)
                    {
                        return "Qualification Name Already Exists";
                    }
                }
                if (qualification != null)
                {
                    qualification.qualification_id = Qualification_Online.qualification_id;
                    qualification.qualification_code = Qualification_Online.qualification_code;
                    qualification.qualification_name = Qualification_Online.qualification_name;
                    qualification.modified_by = Qualification_Online.modified_by;
                    qualification.modified_date = DateTime.Now;
                    qualification.delete_flag = false;
                    qualification.record_status = 2;
                    await db.SaveChangesAsync();
                    return "Qualification Updated Successfully";
                }
                return "Qualification Didn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public async Task<List<GetAllQualification>> GetAllQualification()
        {
            try
            {
                var query = (from a in db.Qualification
                             join b in db.Status on a.status equals b.sts_id
                             //where a.delete_flag == false && a.status == 3
                             //&& a.qualification_id != 0 && a.qualification_Name != "None"
                             where a.qualification_id != 0 && a.qualification_Name != "None"
                             orderby a.qualification_id descending
                             select new GetAllQualification
                             {
                                 qualification_id = a.qualification_id,
                                 qualification_code = a.qualification_code,
                                 qualification_Name = a.qualification_Name,
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

        public async Task<List<GetAllQualification>> GetAllQualification_Skillset_DD()
        {
            try
            {
                var query = (from a in db.Qualification
                             join b in db.Status on a.status equals b.sts_id
                             where a.delete_flag == false && a.status == 3
                             && a.qualification_id != 0 && a.qualification_Name != "None"
                             orderby a.qualification_id descending
                             select new GetAllQualification
                             {
                                 qualification_id = a.qualification_id,
                                 qualification_code = a.qualification_code,
                                 qualification_Name = a.qualification_Name,
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
        public async Task<List<GetAllQualification_Online>> GetAllQualification_Online()
        {
            try
            {
                var query = (from a in db.Qualification_Online
                             join b in db.Status on a.record_status equals b.sts_id
                             where a.qualification_id != 0 && a.delete_flag == false && a.qualification_name != "None"
                             orderby a.qualification_id descending
                             select new GetAllQualification_Online
                             {
                                 qualification_id = a.qualification_id,
                                 qualification_code = a.qualification_code,
                                 qualification_name = a.qualification_name,
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



        public async Task<List<Qualification_DD>> GetQualification_DD()
        {
            var query = (from a in db.Qualification
                         where a.delete_flag == false && a.status == 3
                         && a.qualification_id != 0
                         orderby a.qualification_Name
                         select new Qualification_DD
                         {
                             qualification_id = a.qualification_id,
                             qualification_Name = a.qualification_Name
                         }).ToListAsync();
            var qualificationList = await query;
            var noneData = qualificationList.FirstOrDefault(d => d.qualification_Name == "None");
            qualificationList.RemoveAll(d => d.qualification_Name == "None");
            qualificationList.Add(noneData);
            return qualificationList;
        }
        //Online Portals
        public async Task<List<Qualification_DD_Online>> GetQualification_DD_Online()
        {
            var query = (from a in db.Qualification_Online
                         where a.delete_flag == false && a.record_status == 3
                         && a.qualification_id != 0
                         orderby a.qualification_name
                         select new Qualification_DD_Online
                         {
                             qualification_id = a.qualification_id,
                             qualification_name = a.qualification_name
                         }).ToListAsync();
            return await query;
        }


        public async Task<string> DeleteQualification(int qualification_id)
        {
            try
            {
                var result = await db.Qualification.FirstOrDefaultAsync(x => x.qualification_id == qualification_id);
                if (result != null)
                {
                    result.qualification_id = qualification_id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Qualification Deleted Successfully";
                }
                return "Qualification Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //Online Portals
        public async Task<string> DeleteQualification_Online(int qualification_id)
        {
            try
            {
                var result = await db.Qualification_Online.FirstOrDefaultAsync(x => x.qualification_id == qualification_id);
                if (result != null)
                {
                    result.qualification_id = qualification_id;
                    result.delete_flag = true;
                    result.record_status = 6;
                    result.deleted_by = result.deleted_by;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Qualification Deleted Successfully";
                }
                return "Qualification Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<QualificationById> GetQualificationById(int qualification_id)
        {
            var query = (from a in db.Qualification
                         join b in db.Status on a.status equals b.sts_id
                         where a.qualification_id == qualification_id && a.qualification_id != 0
                         select new QualificationById
                         {
                             qualification_id = a.qualification_id,
                             qualification_code = a.qualification_code,
                             qualification_Name = a.qualification_Name,
                             delete_flag = a.delete_flag,
                             status = a.status,
                             sts_name = b.sts_name,
                             Remarks = a.Remarks,
                         }).FirstOrDefaultAsync();
            return await query;
        }
        //Online Portals
        public async Task<QualificationById_Online> GetQualificationById_Online(int qualification_id)
        {
            var query = (from a in db.Qualification_Online
                         join b in db.Status on a.record_status equals b.sts_id
                         where a.qualification_id == qualification_id && a.qualification_id != 0
                         select new QualificationById_Online
                         {
                             qualification_id = a.qualification_id,
                             qualification_code = a.qualification_code,
                             qualification_name = a.qualification_name,
                             delete_flag = a.delete_flag,
                             record_status = a.record_status,
                             sts_name = b.sts_name,
                             remarks = a.remarks,
                         }).FirstOrDefaultAsync();
            return await query;
        }




        public async Task<string> ApproveQualification(ApproveQualification ApproveQualification)
        {
            try
            {
                var result = await db.Qualification.Where(x => x.qualification_id == ApproveQualification.qualification_id).FirstOrDefaultAsync();
                if (result.status != 3)
                {
                    //result.qualification_id = lead.qualification_id;
                    result.status = 3;
                    if (ApproveQualification.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = ApproveQualification.Remarks;
                    await db.SaveChangesAsync();
                    return "Qualification Approved Successfully";
                }
                return "Qualification Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        //Online Portals
        public async Task<string> ApproveQualification_Online(ApproveQualification_Online ApproveQualification_Online)
        {
            try
            {
                var result = await db.Qualification_Online.Where(x => x.qualification_id == ApproveQualification_Online.qualification_id).FirstOrDefaultAsync();
                if (result.record_status != 3)
                {
                    //result.qualification_id = lead.qualification_id;
                    result.record_status = 3;
                    if (ApproveQualification_Online.remarks == null)
                    {
                        result.remarks = "OK";
                    }
                    else
                        result.remarks = ApproveQualification_Online.remarks;
                    await db.SaveChangesAsync();
                    return "Qualification Approved Successfully";
                }
                return "Qualification Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


    }
}
