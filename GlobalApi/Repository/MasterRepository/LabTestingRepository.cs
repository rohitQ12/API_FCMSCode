using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class LabTestingRepository : ILabTesting
    {
        GlobalContext db;
        LabTestingDetailsRepository labTestingDetailsRepository;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public LabTestingRepository(GlobalContext _db)
        {
            db = _db;
            this.labTestingDetailsRepository = new LabTestingDetailsRepository(_db);
            primarykeyvalue = new Primarykeyvalue(_db);
        }
        public async Task<LabTesting> InsertLabTesting(LabTesting_Details lead)
        {
            try
            {
                var duplicate = await db.LabTesting.FirstOrDefaultAsync(x => x.TstRefDate == lead.TstRefDate && x.Tst_CON_Id_FK == lead.Tst_CON_Id_FK);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("LabTesting");
                    LabTesting obj = new LabTesting()
                    {
                        Id = id,
                        TstRefDate = lead.TstRefDate,
                        Tst_CON_Id_FK = lead.Tst_CON_Id_FK,
                        AcceptLabTest = false,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.LabTesting.AddAsync(obj);
                    await db.SaveChangesAsync();
                    var LTD = await labTestingDetailsRepository.InsertLabTestingDetails(lead.LabTestingDetails, id);
                    return result.Entity;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> AcceptLabTesting(int Id, int Tst_CON_Id_FK, bool AcceptLabTest)
        {
            try
            {
                var result = await db.LabTesting.FirstOrDefaultAsync(x => x.Id == Id && x.Tst_CON_Id_FK == Tst_CON_Id_FK);
                if (result != null)
                {

                    result.Id = Id;
                    result.Tst_CON_Id_FK = Tst_CON_Id_FK;
                    result.AcceptLabTest = AcceptLabTest;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 1;
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<LabTesting> UpdateLabTesting(LabTesting lead)
        {
            try
            {
                var result = await db.LabTesting.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.TstRefDate = lead.TstRefDate;
                    result.Tst_CON_Id_FK = lead.Tst_CON_Id_FK;
                    result.AcceptLabTest = lead.AcceptLabTest;
                    result.SampleTaken = lead.SampleTaken;
                    result.Delivery_status = lead.Delivery_status;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 1;
                    await db.SaveChangesAsync();
                    //await UpdatePatientDxLabDetails(lead);
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<GetLabTestings>> GetAllLabTesting()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.LabTesting
                                 join b in db.Consultation on a.Tst_CON_Id_FK equals b.CON_Id
                                 join e in db.Doctor on b.CON_DO_Id_FK equals e.DO_Id
                                 join f in db.Patient on b.CON_PR_Id_FK equals f.PR_Id
                                 orderby a.Id descending
                                 select new GetLabTestings
                                 {
                                     Id = a.Id,
                                     TstRefDate = a.TstRefDate,
                                     Tst_CON_Id_FK = a.Tst_CON_Id_FK,
                                     //Tst_CON_DO_Id = b.CON_DO_Id_FK,
                                     Tst_DO_Name = string.Concat(e.DO_FirstName, e.DO_LastName),
                                     Tst_DO_MobNum = e.DO_MobileNumber,
                                     //Tst_CON_PR_Id = b.CON_PR_Id_FK,
                                     Tst_PR_Name = string.Concat(f.PR_FirstName, f.PR_LastName),
                                     Tst_PR_Gender = f.PR_Gender,
                                     Tst_PR_Age = f.PR_Age,
                                     Tst_PR_BloodGroup = f.PR_BloodGroup,
                                     //Tst_PR_MobNum = f.PR_MobileNumber,
                                     //Tst_PR_Email = f.PR_Email,
                                     //Tst_PR_Address = f.PR_Address,
                                     AcceptLabTest = a.AcceptLabTest,
                                     SampleTaken = a.SampleTaken,
                                     Delivery_status = a.Delivery_status,
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

        public async Task<LabTesting> DeleteLabTesting(int Id)
        {
            try
            {
                var result = await db.LabTesting.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
                    result.delete_flag = true;
                    result.status = 0;
                    result.deleted_by = 3;
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
        public async Task<LabTestingsById> GetLabTestingById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.LabTesting
                             join b in db.Consultation on a.Tst_CON_Id_FK equals b.CON_Id
                             join e in db.Doctor on b.CON_DO_Id_FK equals e.DO_Id
                             join f in db.Patient on b.CON_PR_Id_FK equals f.PR_Id
                             where a.Id == Id
                             select new LabTestingsById
                             {
                                 Id = a.Id,
                                 TstRefDate = a.TstRefDate,
                                 Tst_CON_Id_FK = a.Tst_CON_Id_FK,
                                 //Tst_CON_DO_Id = b.CON_DO_Id_FK,
                                 Tst_DO_Name = string.Concat(e.DO_FirstName, e.DO_LastName),
                                 Tst_DO_MobNum = e.DO_MobileNumber,
                                 //Tst_CON_PR_Id = b.CON_PR_Id_FK,
                                 Tst_PR_Name = string.Concat(f.PR_FirstName, f.PR_LastName),
                                 Tst_PR_Gender = f.PR_Gender,
                                 Tst_PR_Age = f.PR_Age,
                                 Tst_PR_BloodGroup = f.PR_BloodGroup,
                                 //Tst_PR_MobNum = f.PR_MobileNumber,
                                 //Tst_PR_Email = f.PR_Email,
                                 //Tst_PR_Address = f.PR_Address,
                                 AcceptLabTest = a.AcceptLabTest,
                                 SampleTaken = a.SampleTaken,
                                 Delivery_status = a.Delivery_status,
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
