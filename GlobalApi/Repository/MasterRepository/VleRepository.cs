using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class VleRepository : IVle
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public VleRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Vle> InsertVle(VleModel_Image lead)
        {
            try
            {
                var duplicate = await db.Vle.FirstOrDefaultAsync(x => x.VLE_Code == lead.VLE_Code || x.VLE_Center == lead.VLE_Center);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Vle");
                    string uniqueFilename = ProcessUploadedFile(lead);
                    Vle obj = new Vle()
                    {
                        VL_Id = id,
                        VLE_Center = lead.VLE_Center,
                        VLE_Code = lead.VLE_Code,
                        VL_ContactPerson = lead.VL_ContactPerson,
                        VL_DOB = lead.VL_DOB,
                        VL_Gender = lead.VL_Gender,
                        VL_Address = lead.VL_Address,
                        VL_Country_Id_FK = lead.VL_Country_Id_FK,
                        VL_ST_Id_FK = lead.VL_ST_Id_FK,
                        VL_DI_Id_FK = lead.VL_DI_Id_FK,
                        Taluk_id = lead.Taluk_id,
                        Gram_id = lead.Gram_id,
                        VL_MobileNumber = lead.VL_MobileNumber,
                        VL_AlterNumber = lead.VL_AlterNumber,
                        VL_Email = lead.VL_Email,
                        VL_QU_Id_FK = lead.VL_QU_Id_FK,
                        VL_PostalCode = lead.VL_PostalCode,
                        VL_Photo = uniqueFilename,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Vle.AddAsync(obj);
                    await InsertUsers(obj);
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
        public async Task<UsersLists> InsertUsers(Vle lead)
        {
            int _id = await primarykeyvalue.primary_key("UsersLists");
            UsersLists insert = new UsersLists()
            {
                Id = _id,
                User_cat = "Vle",
                User_ref_id = lead.VL_Id,
                created_by = 1,
                created_date = DateTime.Now,
                delete_flag = false,
                status = 1,

            };
            var _new = await db.UsersLists.AddAsync(insert);
            await db.SaveChangesAsync();
            return _new.Entity;

        }

        private string ProcessUploadedFile(VleModel_Image model)
        {
            string uniqueFileName = null;


            if (model.VL_Photo != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/Vle");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.VL_Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.VL_Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        public async Task<Vle> UpdateVle(VleModel_Image lead)
        {
            try
            {
                var result = await db.Vle.FirstOrDefaultAsync(x => x.VL_Id == lead.VL_Id);
                var _query = from a in db.Vle
                             where a.VL_Id == lead.VL_Id
                             select a.VL_Photo;
                if (lead.VL_Photo != null)
                {
                    foreach (var item in _query)
                    {
                        if (item != null)
                        {
                            string filepath = Path.Combine("wwwroot/Vle", item);
                            System.IO.File.Delete(filepath);
                        }
                    }
                }
                //Update File 
                string uniqueFilename = ProcessUploadedFile(lead);
                if (result != null)
                {
                    result.VL_Id = lead.VL_Id;
                    result.VLE_Center = lead.VLE_Center;
                    result.VLE_Code = lead.VLE_Code;
                    result.VL_ContactPerson = lead.VL_ContactPerson;
                    result.VL_DOB = lead.VL_DOB;
                    result.VL_Gender = lead.VL_Gender;
                    result.VL_Address = lead.VL_Address;
                    result.VL_Country_Id_FK = lead.VL_Country_Id_FK;
                    result.VL_ST_Id_FK = lead.VL_ST_Id_FK;
                    result.VL_DI_Id_FK = lead.VL_DI_Id_FK;
                    result.Taluk_id = lead.Taluk_id;
                    result.Gram_id = lead.Gram_id;
                    result.VL_MobileNumber = lead.VL_MobileNumber;
                    result.VL_AlterNumber = lead.VL_AlterNumber;
                    result.VL_Email = lead.VL_Email;
                    result.VL_QU_Id_FK = lead.VL_QU_Id_FK;
                    result.VL_PostalCode = lead.VL_PostalCode;
                    result.VL_Photo = uniqueFilename;
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
        public async Task<List<GetAllVle>> GetAllVle()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Vle
                                 join b in db.States on a.VL_ST_Id_FK equals b.stat_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.Districts on a.VL_DI_Id_FK equals c.district_id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Qualification on a.VL_QU_Id_FK equals d.qualification_id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Countries on a.VL_Country_Id_FK equals e.cntry_id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Taluk on a.Taluk_id equals f.Taluk_id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.Gram on a.Gram_id equals g.Gram_id into glist
                                 from g in glist.DefaultIfEmpty()
                                 join h in db.Status on a.status equals h.sts_id
                                 where a.VL_Id != 0
                                 orderby a.VL_Id descending
                                 select new GetAllVle
                                 {
                                     VL_Id = a.VL_Id,
                                     VLE_Center = a.VLE_Center,
                                     VLE_Code = a.VLE_Code,
                                     VL_ContactPerson = a.VL_ContactPerson,
                                     VL_DOB = a.VL_DOB,
                                     VL_Gender = a.VL_Gender,
                                     VL_Address = a.VL_Address,
                                     VL_Country_Id_FK = a.VL_Country_Id_FK,
                                     VL_Country = e.country_name,
                                     VL_ST_Id_FK = a.VL_ST_Id_FK,
                                     VL_state_name = b.state_name,
                                     VL_DI_Id_FK = a.VL_DI_Id_FK,
                                     VL_district_name = c.district_name,
                                     Taluk_id = a.Taluk_id,
                                     Taluk_name = f.Taluk_name,
                                     Gram_id = a.Gram_id,
                                     Gram_name = g.Gram_name,
                                     VL_MobileNumber = a.VL_MobileNumber,
                                     VL_AlterNumber = a.VL_AlterNumber,
                                     VL_Email = a.VL_Email,
                                     VL_QU_Id_FK = a.VL_QU_Id_FK,
                                     VL_qualification = d.qualification_Name,
                                     VL_PostalCode = a.VL_PostalCode,
                                     VL_Photo = a.VL_Photo,
                                     Imagebyte = System.IO.File.ReadAllBytes("wwwroot/Vle/" + a.VL_Photo),
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = h.sts_name,
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
        public async Task<List<Vle_DD>> GetVle_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Vle
                             where a.VL_Id != 0 && a.status == 3
                             select new Vle_DD
                             {
                                 VL_Id = a.VL_Id,
                                 VLE_Code = a.VLE_Code,
                                 VLE_Center = a.VLE_Center
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Vle> DeleteVle(int VL_Id)
        {
            try
            {
                var result = await db.Vle.FirstOrDefaultAsync(x => x.VL_Id == VL_Id);
                if (result != null)
                {
                    result.VL_Id = VL_Id;
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
        public async Task<VleBy_Id> GetVleById(int VL_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Vle
                             join b in db.States on a.VL_ST_Id_FK equals b.stat_id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.Districts on a.VL_DI_Id_FK equals c.district_id into clist
                             from c in clist.DefaultIfEmpty()
                             join d in db.Qualification on a.VL_QU_Id_FK equals d.qualification_id into dlist
                             from d in dlist.DefaultIfEmpty()
                             join e in db.Countries on a.VL_Country_Id_FK equals e.cntry_id into elist
                             from e in elist.DefaultIfEmpty()
                             join f in db.Taluk on a.Taluk_id equals f.Taluk_id into flist
                             from f in flist.DefaultIfEmpty()
                             join g in db.Gram on a.Gram_id equals g.Gram_id into glist
                             from g in glist.DefaultIfEmpty()
                             join h in db.Status on a.status equals h.sts_id
                             where a.VL_Id == VL_Id && a.VL_Id != 0
                             select new VleBy_Id
                             {
                                 VL_Id = a.VL_Id,
                                 VLE_Center = a.VLE_Center,
                                 VLE_Code = a.VLE_Code,
                                 VL_ContactPerson = a.VL_ContactPerson,
                                 VL_DOB = a.VL_DOB,
                                 VL_Gender = a.VL_Gender,
                                 VL_Address = a.VL_Address,
                                 VL_Country_Id_FK = a.VL_Country_Id_FK,
                                 VL_Country = e.country_name,
                                 VL_ST_Id_FK = a.VL_ST_Id_FK,
                                 VL_state_name = b.state_name,
                                 VL_DI_Id_FK = a.VL_DI_Id_FK,
                                 VL_district_name = c.district_name,
                                 Taluk_id = a.Taluk_id,
                                 Taluk_name = f.Taluk_name,
                                 Gram_id = a.Gram_id,
                                 Gram_name = g.Gram_name,
                                 VL_MobileNumber = a.VL_MobileNumber,
                                 VL_AlterNumber = a.VL_AlterNumber,
                                 VL_Email = a.VL_Email,
                                 VL_QU_Id_FK = a.VL_QU_Id_FK,
                                 VL_qualification = d.qualification_Name,
                                 VL_PostalCode = a.VL_PostalCode,
                                 VL_Photo = a.VL_Photo,
                                 Imagebyte = System.IO.File.ReadAllBytes("wwwroot/Vle/" + a.VL_Photo),
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = h.sts_name,
                                 Remarks = a.Remarks,
                             }).FirstOrDefaultAsync();

                return await query;
            }
            return null;
        }

        public async Task<string> ApproveVle(int VL_Id, string? Remarks)
        {
            try
            {
                if(VL_Id != 0)
                {
                    var result = await db.Vle.Where(x => x.VL_Id == VL_Id).FirstOrDefaultAsync();
                    if (result.status != 3)
                    {
                        //result.VL_Id = VL_Id;
                        result.status = 3;
                        if (Remarks == null)
                        {
                            result.Remarks = "OK";
                        }
                        else
                            result.Remarks = Remarks;
                        await db.SaveChangesAsync();
                        return "Vle is Approved";
                    }
                    else
                        return "Already Active";
                }
                else
                    return "Cannot Approve Default Vle";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
