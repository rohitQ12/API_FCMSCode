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
        public async Task<Pharmacy> InsertPharmacy(Pharmacy lead)
        {
            try
            {
                var duplicate = await db.Pharmacy.FirstOrDefaultAsync(x => x.Ph_Code == lead.Ph_Code || x.Ph_Name == lead.Ph_Name);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Pharmacy");
                    Pharmacy obj = new Pharmacy()
                    {
                        Ph_Id = id,
                        Ph_Code = lead.Ph_Code,
                        Ph_Name = lead.Ph_Name,
                        Ph_Address = lead.Ph_Address,
                        Ph_ST_Id_FK = lead.Ph_ST_Id_FK,
                        Ph_DI_Id_FK = lead.Ph_DI_Id_FK,
                        Ph_Village = lead.Ph_Village,
                        Ph_PostalCode = lead.Ph_PostalCode,
                        Ph_MobileNumber = lead.Ph_MobileNumber,
                        Ph_AlterNumber = lead.Ph_AlterNumber,
                        Ph_LandLineNo = lead.Ph_LandLineNo,
                        Ph_Email = lead.Ph_Email,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Pharmacy.AddAsync(obj);
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
        public async Task<UsersLists> InsertUsers(Pharmacy lead)
        {
            int _id = await primarykeyvalue.primary_key("Users");
            UsersLists insert = new UsersLists()
            {
                Id = _id,
                User_cat = "Hospital",
                User_ref_id = lead.Ph_Id,
            };
            var _new = await db.UsersLists.AddAsync(insert);
            await db.SaveChangesAsync();
            return _new.Entity;

        }

        public async Task<Pharmacy> UpdatePharmacy(Pharmacy lead)
        {
            try
            {
                var result = await db.Pharmacy.FirstOrDefaultAsync(x => x.Ph_Id == lead.Ph_Id);
                if (result != null)
                {
                    result.Ph_Id = lead.Ph_Id;
                    result.Ph_Code = lead.Ph_Code;
                    result.Ph_Name = lead.Ph_Name;
                    result.Ph_Address = lead.Ph_Address;
                    result.Ph_ST_Id_FK = lead.Ph_ST_Id_FK;
                    result.Ph_DI_Id_FK = lead.Ph_DI_Id_FK;
                    result.Ph_Village = lead.Ph_Village;
                    result.Ph_PostalCode = lead.Ph_PostalCode;
                    result.Ph_MobileNumber = lead.Ph_MobileNumber;
                    result.Ph_AlterNumber = lead.Ph_AlterNumber;
                    result.Ph_LandLineNo = lead.Ph_LandLineNo;
                    result.Ph_Email = lead.Ph_Email;
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
                                 join b in db.States on a.Ph_Id equals b.stat_id
                                 join c in db.Districts on a.Ph_DI_Id_FK equals c.district_id
                                 orderby a.Ph_Id descending
                                 select new GetAllPharmacy
                                 {
                                     Ph_Id = a.Ph_Id,
                                     Ph_Code = a.Ph_Code,
                                     Ph_Name = a.Ph_Name,
                                     Ph_Address = a.Ph_Address,
                                     Ph_ST_Id_FK = a.Ph_ST_Id_FK,
                                     Ph_state_name = b.state_name,
                                     Ph_DI_Id_FK = a.Ph_DI_Id_FK,
                                     Ph_district_name = c.district_name,
                                     Ph_Village = a.Ph_Village,
                                     Ph_PostalCode = a.Ph_PostalCode,
                                     Ph_MobileNumber = a.Ph_MobileNumber,
                                     Ph_AlterNumber = a.Ph_AlterNumber,
                                     Ph_LandLineNo = a.Ph_LandLineNo,
                                     Ph_Email = a.Ph_Email,
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
                             where a.delete_flag == false && a.status == 1
                             select new Pharmacy_DD
                             {
                                 Ph_Id = a.Ph_Id,
                                 Ph_Code = a.Ph_Code,
                                 Ph_Name = a.Ph_Name,
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
                             join b in db.States on a.Ph_Id equals b.stat_id
                             join c in db.Districts on a.Ph_DI_Id_FK equals c.district_id
                             where a.Ph_Id == Ph_Id
                             select new PharmacyById
                             {
                                 Ph_Id = a.Ph_Id,
                                 Ph_Code = a.Ph_Code,
                                 Ph_Name = a.Ph_Name,
                                 Ph_Address = a.Ph_Address,
                                 Ph_ST_Id_FK = a.Ph_ST_Id_FK,
                                 Ph_state_name = b.state_name,
                                 Ph_DI_Id_FK = a.Ph_DI_Id_FK,
                                 Ph_district_name = c.district_name,
                                 Ph_Village = a.Ph_Village,
                                 Ph_PostalCode = a.Ph_PostalCode,
                                 Ph_MobileNumber = a.Ph_MobileNumber,
                                 Ph_AlterNumber = a.Ph_AlterNumber,
                                 Ph_LandLineNo = a.Ph_LandLineNo,
                                 Ph_Email = a.Ph_Email,
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
