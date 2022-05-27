using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Models.Authentication;

namespace GlobalApi.Repository.MasterRepository
{
    public class DiagnosticCentersRepository : IDiagnosticCenters
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DiagnosticCentersRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<DiagnosticCenters> InsertDiagnosticCenters(Diagnostic_Images lead)
        {
            try
            {
                //var duplicate = await db.DiagnosticCenters.FirstOrDefaultAsync(x => x.DGSTC_Code == lead.DGSTC_Code || x.DGSTC_Name == lead.DGSTC_Name);
                //if (duplicate == null)
                //{
                    int id = await primarykeyvalue.primary_key("DiagnosticCenter");
                    string uniqueFilename = ProcessUploadedFile(lead);
                    DiagnosticCenters obj = new DiagnosticCenters()
                    {
                        DGSTC_Id = id,
                        //DGSTC_Code = "DGSTC-" + Convert.ToString(id),                        DGSTC_Code = "DGSTC-" + Convert.ToString(id),
                        DGSTC_Code = lead.DGSTC_Code,
                        DGSTC_Name = lead.DGSTC_Name,
                        PrimaryOrBranch = lead.PrimaryOrBranch, 
                        DGSTC_Branch = lead.DGSTC_Branch,
                        DGSTC_Type_Id = lead.DGSTC_Type_Id,
                        cat_id = lead.cat_id,
                        DGSTC_NE_Id = lead.DGSTC_NE_Id,
                        DGSTC_Address = lead.DGSTC_Address,
                        DGSTC_HO_Id_FK = lead.DGSTC_HO_Id_FK,
                        DGSTC_COUN_Id_FK = lead.DGSTC_COUN_Id_FK,
                        DGSTC_ST_Id_FK = lead.DGSTC_ST_Id_FK,
                        DGSTC_DI_Id_FK = lead.DGSTC_DI_Id_FK,
                        DGSTC_TL_Id_FK = lead.DGSTC_TL_Id_FK,
                        DGSTC_GR_Id_FK = lead.DGSTC_GR_Id_FK,
                        DGSTC_PostalCode = lead.DGSTC_PostalCode,
                        DGSTC_MobileNumber = lead.DGSTC_MobileNumber,
                        DGSTC_AlterNumber = lead.DGSTC_AlterNumber,
                        DGSTC_LandLineNo = lead.DGSTC_LandLineNo,
                        DGSTC_Email = lead.DGSTC_Email,
                        //GSTNoOrPANno = lead.GSTNoOrPANno,
                        RegNo = lead.RegNo,
                        DGSTC_Logo = uniqueFilename,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.DiagnosticCenters.AddAsync(obj);
                    await InsertUsers(obj);
                    await db.SaveChangesAsync();
                    return result.Entity;

                //}
                //return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<UsersLists> InsertUsers(DiagnosticCenters lead)
        {
            int _id = await primarykeyvalue.primary_key("UsersLists");
            UsersLists obj = new UsersLists()
            {
                Id = _id,
                User_cat = "DiagnosticCenters",
                User_ref_id = lead.DGSTC_Id,
            };
            var result = await db.UsersLists.AddAsync(obj);
            await db.SaveChangesAsync();
            return result.Entity;

        }
        private string ProcessUploadedFile(Diagnostic_Images model)
        {
            string uniqueFileName = null;


            if (model.DGSTC_Logo != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/DiagnosticCenters");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.DGSTC_Logo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.DGSTC_Logo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public async Task<DiagnosticCenters> UpdateDiagnosticCenters(Diagnostic_Images lead)
        {
            try
            {
                var result = await db.DiagnosticCenters.FirstOrDefaultAsync(x => x.DGSTC_Id == lead.DGSTC_Id);
                var _query = from a in db.DiagnosticCenters
                             where a.DGSTC_Id == lead.DGSTC_Id
                             select a.DGSTC_Logo;

                if (lead.DGSTC_Logo != null)
                {
                    foreach (var item in _query)
                    {
                        if (item != null)
                        {
                            string filepath = Path.Combine("wwwroot/DiagnosticCenters", item);
                            System.IO.File.Delete(filepath);
                        }
                    }
                }
                //Insert hospital logo
                string uniqueFilename = ProcessUploadedFile(lead);


                if (result != null)
                {
                    result.DGSTC_Id = lead.DGSTC_Id;
                    result.DGSTC_Code = lead.DGSTC_Code;
                    result.DGSTC_Name = lead.DGSTC_Name;
                    result.PrimaryOrBranch = lead.PrimaryOrBranch;
                    result.DGSTC_Branch = lead.DGSTC_Branch;
                    result.DGSTC_Type_Id = lead.DGSTC_Type_Id;
                    result.cat_id = lead.cat_id;
                    result.DGSTC_NE_Id = lead.DGSTC_NE_Id;
                    result.DGSTC_Address = lead.DGSTC_Address;
                    result.DGSTC_HO_Id_FK = lead.DGSTC_HO_Id_FK;
                    result.DGSTC_COUN_Id_FK = lead.DGSTC_COUN_Id_FK;
                    result.DGSTC_ST_Id_FK = lead.DGSTC_ST_Id_FK;
                    result.DGSTC_DI_Id_FK = lead.DGSTC_DI_Id_FK;
                    result.DGSTC_TL_Id_FK = lead.DGSTC_TL_Id_FK;
                    result.DGSTC_GR_Id_FK = lead.DGSTC_GR_Id_FK;
                    result.DGSTC_PostalCode = lead.DGSTC_PostalCode;
                    result.DGSTC_MobileNumber = lead.DGSTC_MobileNumber;
                    result.DGSTC_AlterNumber = lead.DGSTC_AlterNumber;
                    result.DGSTC_LandLineNo = lead.DGSTC_LandLineNo;
                    result.DGSTC_Email = lead.DGSTC_Email;
                    //result.GSTNoOrPANno = lead.GSTNoOrPANno;
                    result.RegNo = lead.RegNo;
                    result.DGSTC_Logo = uniqueFilename;
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
        public async Task<List<GetAllDiagnosticCenters>> GetAllDiagnosticCenters(int? DGSTC_Id, string roleaction)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DiagnosticCenters
                                 join b in db.States on a.DGSTC_Id equals b.stat_id
                                 join c in db.Districts on a.DGSTC_DI_Id_FK equals c.district_id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Countries on a.DGSTC_COUN_Id_FK equals d.cntry_id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Taluk on a.DGSTC_TL_Id_FK equals e.Taluk_id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Gram on a.DGSTC_GR_Id_FK equals f.Gram_id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.DiagnosticType on a.DGSTC_Type_Id equals g.Id into glist
                                 from g in glist.DefaultIfEmpty()
                                 join i in db.Network on a.DGSTC_NE_Id equals i.NE_Id into ilist
                                 from i in ilist.DefaultIfEmpty()
                                 join j in db.Hospital on a.DGSTC_HO_Id_FK equals j.Hos_Id into jlist
                                 from j in jlist.DefaultIfEmpty()
                                 join k in db.DiagnosticType on a.DGSTC_Type_Id equals k.Id into klist
                                 from k in klist.DefaultIfEmpty()
                                 join l in db.DiagnoCategory on a.cat_id equals l.id into llist
                                 from l in llist.DefaultIfEmpty()
                                 join m in db.DiagnosticCenters on a.DGSTC_Branch equals m.DGSTC_Id into mlist
                                 from m in mlist.DefaultIfEmpty()
                                 where
                                 roleaction == "Diag.Center" ? a.DGSTC_Id == DGSTC_Id : a.DGSTC_Id > 0
                                 orderby a.DGSTC_Id descending

                                 select new GetAllDiagnosticCenters
                                 {
                                     DGSTC_Id = a.DGSTC_Id,
                                     DGSTC_Code = a.DGSTC_Code,
                                     DGSTC_Name = a.DGSTC_Name,
                                     PrimaryOrBranch = a.PrimaryOrBranch,
                                     DGSTC_Branch = a.DGSTC_Branch,
                                     branch_name = m.DGSTC_Name,
                                     DGSTC_Type_Id = a.DGSTC_Type_Id,
                                     Type = k.Type,
                                     cat_id = a.cat_id,
                                     name = l.name,
                                     DGSTC_NE_Id = a.DGSTC_NE_Id,
                                     NE_Description = i.NE_Description,
                                     DGSTC_Address = a.DGSTC_Address,
                                     DGSTC_COUN_Id_FK = a.DGSTC_COUN_Id_FK,
                                     country_name = d.country_name,
                                     DGSTC_ST_Id_FK = a.DGSTC_ST_Id_FK,
                                     state_name = b.state_name,
                                     DGSTC_DI_Id_FK = a.DGSTC_DI_Id_FK,
                                     district_name = c.district_name,
                                     DGSTC_TL_Id_FK = a.DGSTC_TL_Id_FK,
                                     Taluk_name = e.Taluk_name,
                                     DGSTC_GR_Id_FK = a.DGSTC_GR_Id_FK,
                                     Gram_name = f.Gram_name,
                                     DGSTC_PostalCode = a.DGSTC_PostalCode,
                                     DGSTC_MobileNumber = a.DGSTC_MobileNumber,
                                     DGSTC_AlterNumber = a.DGSTC_AlterNumber,
                                     DGSTC_LandLineNo = a.DGSTC_LandLineNo,
                                     DGSTC_Email = a.DGSTC_Email,
                                     //GSTNoOrPANno = a.GSTNoOrPANno,
                                     RegNo = a.RegNo,
                                     DGSTC_Logo = a.DGSTC_Logo,
                                     //Logobyte = System.IO.File.ReadAllBytes("wwwroot/DiagnosticCenters/" + a.DGSTC_Logo),
                                     delete_flag = a.delete_flag,
                                     status = a.status

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
        public async Task<List<DiagnosticCenters_DD>> GetDiagnosticCenters_DD(int? DGSTC_Id, string roleaction)
        {
            if (db != null)
            {
                var query = (from a in db.DiagnosticCenters
                             join b in db.Network on a.DGSTC_NE_Id equals b.NE_Id into blist
                             from b in blist.DefaultIfEmpty()
                             where a.delete_flag == false && a.status != 6 && roleaction == "Diag.Center" ? a.DGSTC_Id == DGSTC_Id : a.DGSTC_Id > 0
                             select new DiagnosticCenters_DD
                             {
                                 DGSTC_Id = a.DGSTC_Id,
                                 DGSTC_Code = a.DGSTC_Code,
                                 DGSTC_Name = a.DGSTC_Name,
                                 DGSTC_NE_Id = a.DGSTC_NE_Id,
                                 NE_Description = b.NE_Description,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<Usercategory_DD>> GetDiagnosticCategory_DD()
        {
            if (db != null)
            {
                var query = (from a in db.DiagnosticCenters
                             where a.delete_flag == false && a.status == 1
                             select new Usercategory_DD
                             {
                                 Cat_Id = a.DGSTC_Id,
                                 Code = a.DGSTC_Code,
                                 Name = a.DGSTC_Name,

                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<DiagnosticCenters> DeleteDiagnosticCenters(int DGSTC_Id)
        {
            try
            {
                var result = await db.DiagnosticCenters.FirstOrDefaultAsync(x => x.DGSTC_Id == DGSTC_Id);
                if (result != null)
                {
                    result.DGSTC_Id = DGSTC_Id;
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
        public async Task<DiagnosticCentersById> GetDiagnosticCentersById(int DGSTC_Id, string roleaction)
        {
            if (db != null)
            {
                var query = (from a in db.DiagnosticCenters
                             join b in db.States on a.DGSTC_Id equals b.stat_id
                             join c in db.Districts on a.DGSTC_DI_Id_FK equals c.district_id into clist
                             from c in clist.DefaultIfEmpty()
                             join d in db.Countries on a.DGSTC_COUN_Id_FK equals d.cntry_id into dlist
                             from d in dlist.DefaultIfEmpty()
                             join e in db.Taluk on a.DGSTC_TL_Id_FK equals e.Taluk_id into elist
                             from e in elist.DefaultIfEmpty()
                             join f in db.Gram on a.DGSTC_GR_Id_FK equals f.Gram_id into flist
                             from f in flist.DefaultIfEmpty()
                             join g in db.DiagnosticType on a.DGSTC_Type_Id equals g.Id into glist
                             from g in glist.DefaultIfEmpty()
                             join i in db.Network on a.DGSTC_NE_Id equals i.NE_Id into ilist
                             from i in ilist.DefaultIfEmpty()
                             join j in db.Hospital on a.DGSTC_HO_Id_FK equals j.Hos_Id into jlist
                             from j in jlist.DefaultIfEmpty()
                             join k in db.DiagnosticType on a.DGSTC_Type_Id equals k.Id into klist
                             from k in klist.DefaultIfEmpty()
                             join l in db.DiagnoCategory on a.cat_id equals l.id into llist
                             from l in llist.DefaultIfEmpty()
                             join m in db.DiagnosticCenters on a.DGSTC_Branch equals m.DGSTC_Id into mlist
                             from m in mlist.DefaultIfEmpty()
                             where a.DGSTC_Id == DGSTC_Id || roleaction == "Diag.Center" ? a.DGSTC_Id == DGSTC_Id : a.DGSTC_Id > 0
                             select new DiagnosticCentersById
                             {
                                 DGSTC_Id = a.DGSTC_Id,
                                 DGSTC_Code = a.DGSTC_Code,
                                 DGSTC_Name = a.DGSTC_Name,
                                 PrimaryOrBranch = a.PrimaryOrBranch,
                                 DGSTC_Branch = a.DGSTC_Branch,
                                 branch_name = m.DGSTC_Name,
                                 DGSTC_Type_Id = a.DGSTC_Type_Id,
                                 Type = k.Type,
                                 cat_id = a.cat_id,
                                 name = l.name,
                                 DGSTC_NE_Id = a.DGSTC_NE_Id,
                                 NE_Description = i.NE_Description,
                                 DGSTC_Address = a.DGSTC_Address,
                                 DGSTC_ST_Id_FK = a.DGSTC_ST_Id_FK,
                                 state_name = b.state_name,
                                 DGSTC_DI_Id_FK = a.DGSTC_DI_Id_FK,
                                 district_name = c.district_name,
                                 DGSTC_TL_Id_FK = a.DGSTC_TL_Id_FK,
                                 Taluk_name = e.Taluk_name,
                                 DGSTC_GR_Id_FK = a.DGSTC_GR_Id_FK,
                                 Gram_name = f.Gram_name,
                                 DGSTC_PostalCode = a.DGSTC_PostalCode,
                                 DGSTC_MobileNumber = a.DGSTC_MobileNumber,
                                 DGSTC_AlterNumber = a.DGSTC_AlterNumber,
                                 DGSTC_LandLineNo = a.DGSTC_LandLineNo,
                                 DGSTC_Email = a.DGSTC_Email,
                                 //GSTNoOrPANno = a.GSTNoOrPANno,
                                 RegNo = a.RegNo,
                                 DGSTC_Logo = a.DGSTC_Logo,
                                 /*Logobyte = System.IO.File.ReadAllBytes("wwwroot/DiagnosticCenters/" + a.DGSTC_Logo),*/
                                 delete_flag = a.delete_flag,
                                 status = a.status

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
