using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class HospitalRepository : IHospital
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public HospitalRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Hospital> InsertHospital(Hospital_Images lead)
        {
            try
            {
                var duplicate = await db.Hospital.FirstOrDefaultAsync(x => x.Hos_HospitalCode == lead.Hos_HospitalCode || x.Hos_HospitalName == lead.Hos_HospitalName);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Hospital");
                    string uniqueFilename = ProcessUploadedFile(lead);
                    Hospital obj = new Hospital()
                    {
                        Hos_Id = id,
                        //Hos_HospitalCode = "HO_" + Convert.ToString(id),
                        Hos_HospitalCode = lead.Hos_HospitalCode,
                        Hos_HospitalName = lead.Hos_HospitalName,
                        Hos_Type_Id = lead.Hos_Type_Id,
                        Hos_cat_Id = lead.Hos_cat_Id,
                        Hos_Branch = lead.Hos_Branch,
                        Hos_HospitalEmail = lead.Hos_HospitalEmail,
                        Hos_HospitalPhoneNo = lead.Hos_HospitalPhoneNo,
                        Hos_HospitalAddress = lead.Hos_HospitalAddress,
                        PrimaryorBranch = lead.PrimaryorBranch,
                        Hos_Country_Id_FK = lead.Hos_Country_Id_FK,
                        Hos_ST_Id_FK = lead.Hos_ST_Id_FK,
                        Hos_DI_Id_FK = lead.Hos_DI_Id_FK,
                        Hos_Taluk = lead.Hos_Taluk,
                        Hos_PostalCode = lead.Hos_PostalCode,
                        Hos_NE_Id_FK = lead.Hos_NE_Id_FK,
                        Hos_village = lead.Hos_village,
                        Hos_Alterno = lead.Hos_Alterno,
                        Hos_Landline = lead.Hos_Landline,
                        Hos_HospitalLogo = uniqueFilename,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Hospital.AddAsync(obj);
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
        public async Task<UsersLists> InsertUsers(Hospital lead)
        {
            try
            {
                int _id = await primarykeyvalue.primary_key("Users");
                UsersLists insert = new UsersLists()
                {
                    Id = _id,
                    User_cat = "Hospital",
                    User_ref_id = lead.Hos_Id,
                };
                var _new = await db.UsersLists.AddAsync(insert);
                await db.SaveChangesAsync();
                return _new.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Inserting Hospital Logo
        private string ProcessUploadedFile(Hospital_Images model)
        {
            string uniqueFileName = null;


            if (model.Hos_HospitalLogo != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/Hospital");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Hos_HospitalLogo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Hos_HospitalLogo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }


        public async Task<Hospital> UpdateHospital(Hospital_Images lead)
        {
            try
            {
                var result = await db.Hospital.FirstOrDefaultAsync(x => x.Hos_Id == lead.Hos_Id);
                var _query = from a in db.Hospital
                             where a.Hos_Id == lead.Hos_Id
                             select a.Hos_HospitalLogo;

                if (lead.Hos_HospitalLogo != null)
                {
                    foreach (var item in _query)
                    {
                        if (item != null)
                        {
                            string filepath = Path.Combine("wwwroot/Hospital", item);
                            System.IO.File.Delete(filepath);
                        }
                    }
                }
                //Insert hospital logo
                string uniqueFilename = ProcessUploadedFile(lead);

                if (result != null)
                {
                    result.Hos_Id = lead.Hos_Id;
                    result.Hos_HospitalCode = lead.Hos_HospitalCode;
                    result.Hos_HospitalName = lead.Hos_HospitalName;
                    result.Hos_Type_Id = lead.Hos_Type_Id;
                    result.Hos_cat_Id = lead.Hos_cat_Id;
                    result.Hos_Branch = lead.Hos_Branch;
                    result.Hos_HospitalEmail = lead.Hos_HospitalEmail;
                    result.Hos_HospitalPhoneNo = lead.Hos_HospitalPhoneNo;
                    result.Hos_HospitalAddress = lead.Hos_HospitalAddress;
                    result.PrimaryorBranch = lead.PrimaryorBranch;
                    result.Hos_Country_Id_FK = lead.Hos_Country_Id_FK;
                    result.Hos_ST_Id_FK = lead.Hos_ST_Id_FK;
                    result.Hos_DI_Id_FK = lead.Hos_DI_Id_FK;
                    result.Hos_Taluk = lead.Hos_Taluk;
                    result.Hos_PostalCode = lead.Hos_PostalCode;
                    result.Hos_NE_Id_FK = lead.Hos_NE_Id_FK;
                    result.Hos_village = lead.Hos_village;
                    result.Hos_Alterno = lead.Hos_Alterno;
                    result.Hos_Landline = lead.Hos_Landline;
                    result.Hos_HospitalLogo = uniqueFilename;
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
        public async Task<List<GetAllHospital>> GetAllHospital()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Hospital
                                 join b in db.States on a.Hos_ST_Id_FK equals b.stat_id
                                 join c in db.Districts on a.Hos_DI_Id_FK equals c.district_id
                                 join d in db.Network on a.Hos_NE_Id_FK equals d.NE_Id
                                 join e in db.Countries on a.Hos_Country_Id_FK equals e.cntry_id
                                 join f in db.Hos_Type on a.Hos_Type_Id equals f.Id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.Category on a.Hos_cat_Id equals g.id into glist
                                 from g in glist.DefaultIfEmpty()
                                 orderby a.Hos_Id descending
                                 select new GetAllHospital
                                 {
                                     Hos_Id = a.Hos_Id,
                                     Hos_HospitalCode = a.Hos_HospitalCode,
                                     Hos_HospitalName = a.Hos_HospitalName,
                                     Hos_Type_Id = a.Hos_Type_Id,
                                     TypeName = f.Type,
                                     Hos_cat_Id = a.Hos_cat_Id,
                                     CatName = g.name,
                                     Hos_Branch = a.Hos_Branch,
                                     Hos_HospitalEmail = a.Hos_HospitalEmail,
                                     Hos_HospitalPhoneNo = a.Hos_HospitalPhoneNo,
                                     Hos_HospitalAddress = a.Hos_HospitalAddress,
                                     PrimaryorBranch = a.PrimaryorBranch,
                                     Hos_Country_Id_FK = a.Hos_Country_Id_FK,
                                     Hos_Country_name = e.country_name,
                                     Hos_ST_Id_FK = a.Hos_ST_Id_FK,
                                     Hos_state_name = b.state_name,
                                     Hos_DI_Id_FK = a.Hos_DI_Id_FK,
                                     Hos_district_name = c.district_name,
                                     Hos_Taluk = a.Hos_Taluk,
                                     Hos_PostalCode = a.Hos_PostalCode,
                                     Hos_NE_Id_FK = a.Hos_NE_Id_FK,
                                     Hos_Description = d.NE_Description,
                                     Hos_village = a.Hos_village,
                                     Hos_Alterno = a.Hos_Alterno,
                                     Hos_Landline = a.Hos_Landline,
                                     Hos_HospitalLogo = a.Hos_HospitalLogo,
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
        public async Task<List<Hospital_DD>> GetHospital_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Hospital
                             where a.delete_flag == false && a.status == 1
                             select new Hospital_DD
                             {
                                 Hos_Id = a.Hos_Id,
                                 Hos_HospitalCode = a.Hos_HospitalCode,
                                 Hos_HospitalName = a.Hos_HospitalName,
                                 Hos_Branch = a.Hos_Branch,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Hospital> DeleteHospital(int Hos_Id)
        {
            try
            {
                var result = await db.Hospital.FirstOrDefaultAsync(x => x.Hos_Id == Hos_Id);
                if (result != null)
                {
                    result.Hos_Id = Hos_Id;
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
        public async Task<HospitalById> GetHospitalById(int Hos_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Hospital
                             join b in db.States on a.Hos_ST_Id_FK equals b.stat_id
                             join c in db.Districts on a.Hos_DI_Id_FK equals c.district_id
                             join d in db.Network on a.Hos_NE_Id_FK equals d.NE_Id
                             join e in db.Countries on a.Hos_Country_Id_FK equals e.cntry_id
                             join f in db.Hos_Type on a.Hos_Type_Id equals f.Id into flist
                             from f in flist.DefaultIfEmpty()
                             join g in db.Category on a.Hos_cat_Id equals g.id into glist
                             from g in glist.DefaultIfEmpty()
                             where a.Hos_Id == Hos_Id
                             select new HospitalById
                             {
                                 Hos_Id = a.Hos_Id,
                                 Hos_HospitalCode = a.Hos_HospitalCode,
                                 Hos_HospitalName = a.Hos_HospitalName,
                                 Hos_Type_Id = a.Hos_Type_Id,
                                 TypeName = f.Type,
                                 Hos_cat_Id = a.Hos_cat_Id,
                                 CatName = g.name,
                                 Hos_Branch = a.Hos_Branch,
                                 Hos_HospitalEmail = a.Hos_HospitalEmail,
                                 Hos_HospitalPhoneNo = a.Hos_HospitalPhoneNo,
                                 Hos_HospitalAddress = a.Hos_HospitalAddress,
                                 PrimaryorBranch = a.PrimaryorBranch,
                                 Hos_Country_Id_FK = a.Hos_Country_Id_FK,
                                 Hos_Country_name = e.country_name,
                                 Hos_ST_Id_FK = a.Hos_ST_Id_FK,
                                 Hos_state_name = b.state_name,
                                 Hos_DI_Id_FK = a.Hos_DI_Id_FK,
                                 Hos_district_name = c.district_name,
                                 Hos_Taluk = a.Hos_Taluk,
                                 Hos_PostalCode = a.Hos_PostalCode,
                                 Hos_NE_Id_FK = a.Hos_NE_Id_FK,
                                 Hos_Description = d.NE_Description,
                                 Hos_village = a.Hos_village,
                                 Hos_Alterno = a.Hos_Alterno,
                                 Hos_Landline = a.Hos_Landline,
                                 Hos_HospitalLogo = a.Hos_HospitalLogo,
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
