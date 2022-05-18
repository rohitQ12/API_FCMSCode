using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Models.Authentication;

namespace GlobalApi.Repository.MasterRepository
{
    public class PharmacyRepository : IPharmacy
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public PharmacyRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Pharmacy> InsertPharmacy(Pharmacy_Images lead)
        {
            try
            {
                //var duplicate = await db.Pharmacy.FirstOrDefaultAsync(x => x.Ph_Code == lead.Ph_Code || x.Ph_Name == lead.Ph_Name);
                //if (duplicate == null)
                //{
                int id = await primarykeyvalue.primary_key("Pharmacy");
                string uniqueFilename = ProcessUploadedFile(lead);

                Pharmacy obj = new Pharmacy()
                {
                    Ph_Id = id,
                    Ph_Code = lead.Ph_Code,
                    Ph_Name = lead.Ph_Name,
                    Ph_Address = lead.Ph_Address,
                    PrimaryOrBranch = lead.PrimaryOrBranch,
                    Ph_Branch = lead.Ph_Branch,
                    cat_id = lead.id,
                    T_Id = lead.T_Id,
                    Ph_NE_Id = lead.Ph_NE_Id,
                    Ph_HO_Id_FK = lead.Ph_HO_Id_FK,
                    Ph_COUN_Id = lead.Ph_COUN_Id,
                    Ph_ST_Id_FK = lead.Ph_ST_Id_FK,
                    Ph_DI_Id_FK = lead.Ph_DI_Id_FK,
                    Ph_tl_Id = lead.Ph_tl_Id,
                    Ph_GR_Id = lead.Ph_GR_Id,
                    Ph_PostalCode = lead.Ph_PostalCode,
                    Ph_MobileNumber = lead.Ph_MobileNumber,
                    Ph_AlterNumber = lead.Ph_AlterNumber,
                    Ph_LandLineNo = lead.Ph_LandLineNo,
                    Ph_Email = lead.Ph_Email,
                    GSTnoOrPANno = lead.GSTnoOrPANno,
                    RegNo = lead.RegNo,
                    Ph_Logo = uniqueFilename,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Pharmacy.AddAsync(obj);
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
        public async Task<UsersLists> InsertUsers(Pharmacy lead)
        {
            int _id = await primarykeyvalue.primary_key("UsersLists");
            UsersLists insert = new UsersLists()
            {
                Id = _id,
                User_cat = "Pharmacy",
                User_ref_id = lead.Ph_Id,
            };
            var _new = await db.UsersLists.AddAsync(insert);
            await db.SaveChangesAsync();
            return _new.Entity;

        }
        private string ProcessUploadedFile(Pharmacy_Images model)
        {
            string uniqueFileName = null;


            if (model.Ph_Logo != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/Pharmacy");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Ph_Logo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Ph_Logo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public async Task<Pharmacy> UpdatePharmacy(Pharmacy_Images lead)
        {
            try
            {
                var result = await db.Pharmacy.FirstOrDefaultAsync(x => x.Ph_Id == lead.Ph_Id);
                var _query = from a in db.Pharmacy
                             where a.Ph_Id == lead.Ph_Id
                             select a.Ph_Logo;

                if (lead.Ph_Logo != null)
                {
                    foreach (var item in _query)
                    {
                        if (item != null)
                        {
                            string filepath = Path.Combine("wwwroot/Pharmacy", item);
                            System.IO.File.Delete(filepath);
                        }
                    }
                }
                //Insert hospital logo
                string uniqueFilename = ProcessUploadedFile(lead);

                if (result != null)
                {
                    result.Ph_Id = lead.Ph_Id;
                    result.Ph_Code = lead.Ph_Code;
                    result.Ph_Name = lead.Ph_Name;
                    result.Ph_Address = lead.Ph_Address;
                    result.PrimaryOrBranch = lead.PrimaryOrBranch;
                    result.Ph_Branch = lead.Ph_Branch;
                    result.T_Id = lead.T_Id;
                    result.Ph_NE_Id = lead.Ph_NE_Id;
                    result.Ph_COUN_Id = lead.Ph_COUN_Id;
                    result.Ph_HO_Id_FK = lead.Ph_HO_Id_FK;
                    result.Ph_ST_Id_FK = lead.Ph_ST_Id_FK;
                    result.Ph_DI_Id_FK = lead.Ph_DI_Id_FK;
                    result.Ph_tl_Id = lead.Ph_tl_Id;
                    result.Ph_GR_Id = lead.Ph_GR_Id;
                    result.Ph_PostalCode = lead.Ph_PostalCode;
                    result.Ph_MobileNumber = lead.Ph_MobileNumber;
                    result.Ph_AlterNumber = lead.Ph_AlterNumber;
                    result.Ph_LandLineNo = lead.Ph_LandLineNo;
                    result.Ph_Email = lead.Ph_Email;
                    result.GSTnoOrPANno = lead.GSTnoOrPANno;
                    result.RegNo = lead.RegNo;
                    result.Ph_Logo = uniqueFilename;
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
        public async Task<List<GetAllPharmacy>> GetAllPharmacy()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Pharmacy
                                 join b in db.States on a.Ph_ST_Id_FK equals b.stat_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.Districts on a.Ph_DI_Id_FK equals c.district_id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Countries on a.Ph_COUN_Id equals d.cntry_id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Taluk on a.Ph_tl_Id equals e.Taluk_id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Gram on a.Ph_GR_Id equals f.Gram_id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join k in db.PharmacyType on a.T_Id equals k.Id into klist
                                 from k in klist.DefaultIfEmpty()
                                 join l in db.PharmacyCategory on a.cat_id equals l.id into llist
                                 from l in llist.DefaultIfEmpty()
                                 join m in db.Pharmacy on a.Ph_Branch equals m.Ph_Id into mlist
                                 from m in mlist.DefaultIfEmpty()
                                 join g in db.Network on a.Ph_NE_Id equals g.NE_Id into glist
                                 from g in glist.DefaultIfEmpty()
                                 orderby a.Ph_Id descending
                                 select new GetAllPharmacy
                                 {
                                     Ph_Id = a.Ph_Id,
                                     Ph_Code = a.Ph_Code,
                                     Ph_Name = a.Ph_Name,
                                     Ph_Address = a.Ph_Address,
                                     PrimaryOrBranch = a.PrimaryOrBranch,
                                     Ph_Branch = a.Ph_Branch,
                                     Branch_Name = m.Ph_Name,
                                     T_Id = a.T_Id,
                                     Type = k.Type,
                                     cat_id = a.cat_id,
                                     name = l.name,
                                     Ph_NE_Id = a.Ph_NE_Id,
                                     NE_Description = g.NE_Description,
                                     Ph_HO_Id_FK = a.Ph_HO_Id_FK,
                                     Ph_COUN_Id_FK = a.Ph_COUN_Id,
                                     Countries_name = d.country_name,
                                     Ph_ST_Id_FK = a.Ph_ST_Id_FK,
                                     Ph_state_name = b.state_name,
                                     Ph_DI_Id_FK = a.Ph_DI_Id_FK,
                                     Ph_tl_Id = a.Ph_tl_Id,
                                     Taluk_Name = e.Taluk_name,
                                     Ph_GR_Id = a.Ph_GR_Id,
                                     gram_Name = f.Gram_name,
                                     Ph_district_name = c.district_name,
                                     Ph_PostalCode = a.Ph_PostalCode,
                                     Ph_MobileNumber = a.Ph_MobileNumber,
                                     Ph_AlterNumber = a.Ph_AlterNumber,
                                     Ph_LandLineNo = a.Ph_LandLineNo,
                                     Ph_Email = a.Ph_Email,
                                     Ph_Logo = a.Ph_Logo,
                                    /* Logobyte = System.IO.File.ReadAllBytes("wwwroot/Pharmacy/" + a.Ph_Logo),*/
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
        public async Task<List<Pharmacy_DD>> GetPharmacy_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Pharmacy
                             join b in db.Network on a.Ph_NE_Id equals b.NE_Id into blist
                             from b in blist.DefaultIfEmpty()
                             where a.delete_flag == false && a.status != 6
                             select new Pharmacy_DD
                             {
                                 Ph_Id = a.Ph_Id,
                                 Ph_Code = a.Ph_Code,
                                 Ph_Name = a.Ph_Name,
                                 Ph_NE_Id = a.Ph_NE_Id,
                                 NE_Description = b.NE_Description,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<Usercategory_DD>> GetPharmacyCategory_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Pharmacy
                             where a.delete_flag == false && a.status == 1
                             select new Usercategory_DD
                             {
                                 Cat_Id = a.Ph_Id,
                                 Code = a.Ph_Code,
                                 Name = a.Ph_Name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Pharmacy> DeletePharmacy(int Ph_Id)
        {
            try
            {
                var result = await db.Pharmacy.FirstOrDefaultAsync(x => x.Ph_Id == Ph_Id);
                if (result != null)
                {
                    result.Ph_Id = Ph_Id;
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
        public async Task<PharmacyById> GetPharmacyById(int Ph_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Pharmacy
                             join b in db.States on a.Ph_Id equals b.stat_id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.Districts on a.Ph_DI_Id_FK equals c.district_id into clist
                             from c in clist.DefaultIfEmpty()
                             join d in db.Countries on a.Ph_COUN_Id equals d.cntry_id into dlist
                             from d in dlist.DefaultIfEmpty()
                             join e in db.Taluk on a.Ph_tl_Id equals e.Taluk_id into elist
                             from e in elist.DefaultIfEmpty()
                             join f in db.Gram on a.Ph_GR_Id equals f.Gram_id into flist
                             from f in flist.DefaultIfEmpty()
                             join k in db.PharmacyType on a.T_Id equals k.Id into klist
                             from k in klist.DefaultIfEmpty()
                             join l in db.PharmacyCategory on a.cat_id equals l.id into llist
                             from l in llist.DefaultIfEmpty()
                             join m in db.Pharmacy on a.Ph_Branch equals m.Ph_Id into mlist
                             from m in mlist.DefaultIfEmpty()
                             join g in db.Network on a.Ph_NE_Id equals g.NE_Id into glist
                             from g in glist.DefaultIfEmpty()
                             where a.Ph_Id == Ph_Id
                             select new PharmacyById
                             {
                                 Ph_Id = a.Ph_Id,
                                 Ph_Code = a.Ph_Code,
                                 Ph_Name = a.Ph_Name,
                                 Ph_Address = a.Ph_Address,
                                 PrimaryOrBranch = a.PrimaryOrBranch,
                                 Ph_Branch = a.Ph_Branch,
                                 Branch_Name = m.Ph_Name,
                                 T_Id = a.T_Id,
                                 Type = k.Type,
                                 id = a.cat_id,
                                 name = l.name,
                                 Ph_NE_Id = a.Ph_NE_Id,
                                 NE_Description = g.NE_Description,
                                 Ph_HO_Id_FK = a.Ph_HO_Id_FK,
                                 Ph_COUN_Id_FK = a.Ph_COUN_Id,
                                 Countries_name = d.country_name,
                                 Ph_ST_Id_FK = a.Ph_ST_Id_FK,
                                 Ph_state_name = b.state_name,
                                 Ph_DI_Id_FK = a.Ph_DI_Id_FK,
                                 Ph_tl_Id = a.Ph_tl_Id,
                                 Taluk_Name = e.Taluk_name,
                                 Ph_GR_Id = a.Ph_GR_Id,
                                 gram_Name = f.Gram_name,
                                 Ph_district_name = c.district_name,
                                 Ph_PostalCode = a.Ph_PostalCode,
                                 Ph_MobileNumber = a.Ph_MobileNumber,
                                 Ph_AlterNumber = a.Ph_AlterNumber,
                                 Ph_LandLineNo = a.Ph_LandLineNo,
                                 Ph_Email = a.Ph_Email,
                                 Ph_Logo = a.Ph_Logo,
                                 /*Logobyte = System.IO.File.ReadAllBytes("wwwroot/Pharmacy/" + a.Ph_Logo),*/
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
