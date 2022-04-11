using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class ImgTestRepository : IImgTest
    {
        GlobalContext db;
        ImgTestDetailsRepository ImgTestDetailsRepository;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public ImgTestRepository(GlobalContext _db)
        {
            db = _db;
            this.ImgTestDetailsRepository = new ImgTestDetailsRepository(_db);
            primarykeyvalue = new Primarykeyvalue(_db);
        }
        public async Task<ImgTest> InsertImgTest(ImgTest_Details lead)
        {
            try
            {
                var duplicate = await db.ImgTest.FirstOrDefaultAsync(x => x.Img_CON_Id_FK == lead.Img_CON_Id_FK);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("ImgTest");
                    ImgTest obj = new ImgTest()
                    {
                        Id = id,
                        ImgRefDate = lead.ImgRefDate,
                        Img_CON_Id_FK = lead.Img_CON_Id_FK,
                        AcceptImgTest = false,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.ImgTest.AddAsync(obj);
                    await db.SaveChangesAsync();
                    var ITD = await ImgTestDetailsRepository.InsertImgTestDetails(lead.ImgTestDetails, id);
                    return result.Entity;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //public async Task<PatientDxImgDetail> InsertPatientDxImgDetail(ImgTest lead)
        //{
        //    int _id = await primarykeyvalue.primary_key("PatientDxImgDetail");
        //    PatientDxImgDetail obj = new PatientDxImgDetail()
        //    {
        //        Id = _id,
        //        Img_Id_FK = lead.Id,
        //        CON_Id_FK = lead.Img_CON_Id_FK,
        //        created_by = 1,
        //        created_date = DateTime.Now,
        //        delete_flag = false,
        //        status = 1,
        //    };
        //    var result = await db.PatientDxImgDetail.AddAsync(obj);
        //    await db.SaveChangesAsync();
        //    return result.Entity;
        //}

        public async Task<bool> AcceptImgTest(int Id, int Img_CON_Id_FK, bool AcceptImgTest)
        {
            try
            {
                var result = await db.ImgTest.FirstOrDefaultAsync(x => x.Id == Id && x.Img_CON_Id_FK == Img_CON_Id_FK);
                if (result != null)
                {
                    result.Id = Id;
                    result.Img_CON_Id_FK = Img_CON_Id_FK;
                    result.AcceptImgTest = AcceptImgTest;
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
        public async Task<ImgTest> UpdateImgTest(ImgTest lead)
        {
            try
            {
                var result = await db.ImgTest.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (result != null)
                {
                    result.Id = lead.Id;
                    result.ImgRefDate = lead.ImgRefDate;
                    result.Img_CON_Id_FK = lead.Img_CON_Id_FK;
                    result.Delivery_status = lead.Delivery_status;
                    result.AcceptImgTest = lead.AcceptImgTest;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 1;
                    await db.SaveChangesAsync();
                    //await UpdatePatientDxImgDetails(lead);
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //public async Task<PatientDxImgDetail> UpdatePatientDxImgDetail(ImgTest lead)
        //{
        //    var result = await db.PatientDxImgDetail.FirstOrDefaultAsync(x => x.Id == lead.Id || x.Img_Id_FK == lead.Id);
        //    if (result != null)
        //    {
        //        result.Id = lead.Id;
        //        result.Img_Id_FK = lead.Id;
        //        result.CON_Id_FK = lead.Img_CON_Id_FK;
        //        result.modified_by = 2;
        //        result.modified_date = DateTime.Now;
        //        result.delete_flag = false;
        //        result.status = 1;
        //        await db.SaveChangesAsync();
        //        return result;

        //    }
        //    return null;

        //}

        public async Task<List<GetAllImgTest>> GetAllImgTest()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.ImgTest
                                 join b in db.Consultation on a.Img_CON_Id_FK equals b.CON_Id
                                 join e in db.Doctor on b.CON_DO_Id_FK equals e.DO_Id
                                 join f in db.Patient on b.CON_PR_Id_FK equals f.PR_Id
                                 orderby a.Id descending
                                 select new GetAllImgTest
                                 {
                                     Id = a.Id,
                                     ImgRefDate = a.ImgRefDate,
                                     Img_CON_Id_FK = a.Img_CON_Id_FK,
                                     Tst_CON_DO_Id = b.CON_DO_Id_FK,
                                     Tst_DO_Name = string.Concat(e.DO_FirstName, e.DO_LastName),
                                     Tst_DO_MobNum = e.DO_MobileNumber,
                                     Tst_CON_PR_Id = b.CON_PR_Id_FK,
                                     Tst_PR_Name = string.Concat(f.PR_FirstName, f.PR_LastName),
                                     Tst_PR_Gender = f.PR_Gender,
                                     Tst_PR_Age = f.PR_Age,
                                     Tst_PR_BloodGroup = f.PR_BloodGroup,
                                     Delivery_status = a.Delivery_status,
                                     AcceptImgTest = a.AcceptImgTest,
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
        public async Task<ImgTest> DeleteImgTest(int Id)
        {
            try
            {
                var result = await db.ImgTest.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
                    result.delete_flag = true;
                    result.status = 0;
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
        public async Task<ImgTestById> GetImgTestById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.ImgTest
                             join b in db.Consultation on a.Img_CON_Id_FK equals b.CON_Id
                             join e in db.Doctor on b.CON_DO_Id_FK equals e.DO_Id
                             join f in db.Patient on b.CON_PR_Id_FK equals f.PR_Id
                             where a.Id == Id
                             select new ImgTestById
                             {
                                 Id = a.Id,
                                 ImgRefDate = a.ImgRefDate,
                                 Img_CON_Id_FK = a.Img_CON_Id_FK,
                                 Tst_CON_DO_Id = b.CON_DO_Id_FK,
                                 Tst_DO_Name = string.Concat(e.DO_FirstName, e.DO_LastName),
                                 Tst_DO_MobNum = e.DO_MobileNumber,
                                 Tst_CON_PR_Id = b.CON_PR_Id_FK,
                                 Tst_PR_Name = string.Concat(f.PR_FirstName, f.PR_LastName),
                                 Tst_PR_Gender = f.PR_Gender,
                                 Tst_PR_Age = f.PR_Age,
                                 Tst_PR_BloodGroup = f.PR_BloodGroup,
                                 Delivery_status = a.Delivery_status,
                                 AcceptImgTest = a.AcceptImgTest,
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
