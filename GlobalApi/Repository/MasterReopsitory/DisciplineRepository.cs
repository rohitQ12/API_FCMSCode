using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class DisciplineRepository : IDiscipline
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DisciplineRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertDiscipline(Discipline Discipline)
        {
            try
            {
                var Disc_Desc = await db.Discipline.FirstOrDefaultAsync(x => x.CD_ClinicalDiscipline == Discipline.CD_ClinicalDiscipline);
                var Disc_code = await db.Discipline.FirstOrDefaultAsync(x => x.CD_Code == Discipline.CD_Code);
                if (Disc_code != null)
                {
                    return "Discipline Code Already Exists";
                }
                if (Disc_Desc != null)
                {
                    return "Discipline Desc Already Exists";
                }
                int id = await primarykeyvalue.primary_key("Discipline");
                Discipline obj = new Discipline()
                {
                    CD_Id = id,
                    CD_Code = Discipline.CD_Code,
                    CD_ClinicalDiscipline = Discipline.CD_ClinicalDiscipline,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Discipline.AddAsync(obj);
                await db.SaveChangesAsync();
                return "Discipline Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //online portals
        public async Task<string> InsertDiscipline_Online(Discipline_Online Discipline_Online)
        {
            try
            {
                var Disc_Desc = await db.Discipline_Online.FirstOrDefaultAsync(x => x.cd_clinicaldiscipline == Discipline_Online.cd_clinicaldiscipline);
                var Disc_code = await db.Discipline_Online.FirstOrDefaultAsync(x => x.cd_code == Discipline_Online.cd_code);
                if (Disc_code != null)
                {
                    return "Discipline Code Already Exists";
                }
                if (Disc_Desc != null)
                {
                    return "Discipline Desc Already Exists";
                }
                int id = await primarykeyvalue.primary_key("Discipline_Online");
                Discipline_Online obj = new Discipline_Online()
                {
                    cd_id = id,
                    cd_code = Discipline_Online.cd_code,
                    cd_clinicaldiscipline = Discipline_Online.cd_clinicaldiscipline,
                    created_by = Discipline_Online.created_by,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    record_status = 1
                };
                var result = await db.Discipline_Online.AddAsync(obj);
                await db.SaveChangesAsync();
                return "Discipline Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> UpdateDiscipline(Discipline Discipline)
        {
            try
            {
                var result = await db.Discipline.FirstOrDefaultAsync(x => x.CD_Id == Discipline.CD_Id);
                var Disc_Desc = await db.Discipline.FirstOrDefaultAsync(x => x.CD_ClinicalDiscipline == Discipline.CD_ClinicalDiscipline);
                var Disc_code = await db.Discipline.FirstOrDefaultAsync(x => x.CD_Code == Discipline.CD_Code);
                if (Disc_code != null)
                {
                    if (Disc_code.CD_Code != result.CD_Code)
                    {
                        return "Discipline Code Already Exists";
                    }
                }
                if (Disc_Desc != null)
                {
                    if (Disc_Desc.CD_ClinicalDiscipline != result.CD_ClinicalDiscipline)
                    {
                        return "Discipline Name Already Exists";
                    }
                }

                if (result != null)
                {
                    result.CD_Id = Discipline.CD_Id;
                    result.CD_Code = Discipline.CD_Code;
                    result.CD_ClinicalDiscipline = Discipline.CD_ClinicalDiscipline;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    return "Discipline Updated Successfully";
                }
                return "Discipline Didn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //online portals
        public async Task<string> UpdateDiscipline_Online(Discipline_Online Discipline_Online)
        {
            try
            {
                var result = await db.Discipline_Online.FirstOrDefaultAsync(x => x.cd_id == Discipline_Online.cd_id);
                var Disc_Desc = await db.Discipline_Online.FirstOrDefaultAsync(x => x.cd_clinicaldiscipline == Discipline_Online.cd_clinicaldiscipline);
                var Disc_code = await db.Discipline_Online.FirstOrDefaultAsync(x => x.cd_code == Discipline_Online.cd_code);
                if (Disc_code != null)
                {
                    if (Disc_code.cd_code != result.cd_code)
                    {
                        return "Discipline Code Already Exists";
                    }
                }
                if (Disc_Desc != null)
                {
                    if (Disc_Desc.cd_clinicaldiscipline != result.cd_clinicaldiscipline)
                    {
                        return "Discipline Name Already Exists";
                    }
                }

                if (result != null)
                {
                    result.cd_id = Discipline_Online.cd_id;
                    result.cd_code = Discipline_Online.cd_code;
                    result.cd_clinicaldiscipline= Discipline_Online.cd_clinicaldiscipline;
                    result.modified_by = Discipline_Online.modified_by;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.record_status = 2;
                    await db.SaveChangesAsync();
                    return "Discipline Updated Successfully";
                }
                return "Discipline Didn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllDiscipline>> GetAllDiscipline()
        {
            try
            {
                    var query = (from a in db.Discipline
                                 join b in db.Status on a.status equals b.sts_id
                                 where a.CD_Id != 0
                                 orderby a.CD_Id descending
                                 select new GetAllDiscipline
                                 {
                                     CD_Id = a.CD_Id,
                                     CD_Code = a.CD_Code,
                                     CD_ClinicalDiscipline = a.CD_ClinicalDiscipline,
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

        //online portals
        public async Task<List<GetAllDiscipline_Online>> GetAllDiscipline_Online()
        {
            try
            {
                var query = (from a in db.Discipline_Online
                             join b in db.Status on a.record_status equals b.sts_id
                             where a.cd_id != 0
                             orderby a.cd_id descending
                             select new GetAllDiscipline_Online
                             {
                                 cd_id = a.cd_id,
                                 cd_code = a.cd_code,
                                 cd_clinicaldiscipline = a.cd_clinicaldiscipline,
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


        public async Task<List<Discipline_DD>> GetDiscipline_DD()
        {
            var query = (from a in db.Discipline
                         where a.CD_Id != 0 && a.status == 3
                         orderby a.CD_ClinicalDiscipline
                         select new Discipline_DD
                         {
                             CD_Id = a.CD_Id,
                             CD_Code = a.CD_Code,
                             CD_ClinicalDiscipline = a.CD_ClinicalDiscipline,
                         });
            return await query.ToListAsync();
            // Wait for the query to complete and get the list
            //var clinicalDisciplineList = await query;

            // Now, you can work with the list
            //var noneData = clinicalDisciplineList.FirstOrDefault(d => d.CD_ClinicalDiscipline == "None");
            //clinicalDisciplineList.RemoveAll(d => d.CD_ClinicalDiscipline == "None");
            //clinicalDisciplineList.Add(noneData);
            //return clinicalDisciplineList;
        }
        //Online Portals
        public async Task<List<Discipline_DD_Online>> GetDiscipline_DD_Online()
        {
                var query = (from a in db.Discipline_Online
                             where a.cd_id != 0 && a.record_status == 3
                             orderby a.cd_clinicaldiscipline
                             select new Discipline_DD_Online
                             {
                                 cd_id = a.cd_id,
                                 cd_code = a.cd_code,
                                 cd_clinicaldiscipline = a.cd_clinicaldiscipline,
                             }).ToListAsync();
                return await query;
        }



        public async Task<string> DeleteDiscipline(int CD_Id)
        {
            try
            {
                var result = await db.Discipline.FirstOrDefaultAsync(x => x.CD_Id == CD_Id);
                if (result != null)
                {
                    result.CD_Id = CD_Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Discipline Deleted Successfully";
                }
                return "Discipline Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Online Portals
        public async Task<string> DeleteDiscipline_Online(int cd_id)
        {
            try
            {
                var result = await db.Discipline_Online.FirstOrDefaultAsync(x => x.cd_id == cd_id);
                if (result != null)
                {
                    result.cd_id = cd_id;
                    result.delete_flag = true;
                    result.record_status = 6;
                    result.deleted_by = result.deleted_by;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Discipline Deleted Successfully";
                }
                return "Discipline Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<DisciplineById> GetDisciplineById(int CD_Id)
        {
                var query = (from a in db.Discipline
                             join b in db.Status on a.status equals b.sts_id
                             where a.CD_Id == CD_Id && a.CD_Id != 0
                             select new DisciplineById
                             {
                                 CD_Id = a.CD_Id,
                                 CD_Code = a.CD_Code,
                                 CD_ClinicalDiscipline = a.CD_ClinicalDiscipline,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = b.sts_name,

                             }).FirstOrDefaultAsync();
                return await query;
        }

        //Online Portals
        public async Task<DisciplineById_Online> GetDisciplineById_Online(int cd_id)
        {
            var query = (from a in db.Discipline_Online
                         join b in db.Status on a.record_status equals b.sts_id
                         where a.cd_id == cd_id && a.cd_id != 0
                         select new DisciplineById_Online
                         {
                             cd_id = a.cd_id,
                             cd_code = a.cd_code,
                             cd_clinicaldiscipline = a.cd_clinicaldiscipline,
                             delete_flag = a.delete_flag,
                             record_status = a.record_status,
                             sts_name = b.sts_name,

                         }).FirstOrDefaultAsync();
            return await query;
        }

        public async Task<string> ApproveDiscipline(ApproveDiscipline ApproveDiscipline)
        {
            try
            {
                var result = await db.Discipline.Where(x => x.CD_Id == ApproveDiscipline.CD_Id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.status = 3;
                    if (ApproveDiscipline.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    else
                        result.Remarks = ApproveDiscipline.Remarks;
                    await db.SaveChangesAsync();
                    return "Discipline Approved Successfully";
                }
                return "Discipline Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //Online Portals

        public async Task<string> ApproveDiscipline_Online(ApproveDiscipline_Online ApproveDiscipline_Online)
        {
            try
            {
                var result = await db.Discipline_Online.Where(x => x.cd_id == ApproveDiscipline_Online.cd_id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.record_status = 3;
                    if (ApproveDiscipline_Online.remarks == null)
                    {
                        result.remarks = "OK";
                    }
                    else
                        result.remarks = ApproveDiscipline_Online.remarks;
                    await db.SaveChangesAsync();
                    return "Discipline Approved Successfully";
                }
                return "Discipline Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
