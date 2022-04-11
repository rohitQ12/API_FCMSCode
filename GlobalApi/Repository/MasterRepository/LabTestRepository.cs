//using Microsoft.EntityFrameworkCore;
//using GlobalApi.Data;
//using GlobalApi.GlobalClasses;
//using GlobalApi.IRepository.MasterIRepository;
//using GlobalApi.Models.Master;

//namespace GlobalApi.Repository.MasterRepository
//{
//    public class LabTestRepository : ILabTest
//    {
//        GlobalContext  db;
//        //public readonly string _connectionString;
//        private IPrimarykeyvalue primarykeyvalue;
//        public LabTestRepository(GlobalContext _db)
//        {
//            db = _db;
//            primarykeyvalue = new Primarykeyvalue(_db);
//        }
//        public async Task<LabTest> InsertLabTest(List<LabTest> lead)
//        {
//            try
//            {
//                foreach(LabTest lab in lead)
//                { 
//                    var duplicate = await db.LabTest.FirstOrDefaultAsync(x => x.Tst_CON_Id_FK == lab.Tst_CON_Id_FK && x.Lab_Invst_Id_FK == lab.Lab_Invst_Id_FK && x.Lab_SubInvst_Id_FK == lab.Lab_SubInvst_Id_FK);
//                    if (duplicate == null)
//                    {

//                            int id = await primarykeyvalue.primary_key("LabTest");
//                            LabTest obj = new LabTest()
//                            {
//                                Id = id,
//                                Tst_CON_Id_FK = lab.Tst_CON_Id_FK,
//                                //Fasting = lab.Fasting,
//                                //Non_Fasting = lab.Non_Fasting,
//                                FastingORNonFasting = lab.FastingORNonFasting,
//                                Lab_Invst_Id_FK = lab.Lab_Invst_Id_FK,
//                                Lab_SubInvst_Id_FK = lab.Lab_SubInvst_Id_FK,
//                                created_by = 1,
//                                created_date = DateTime.Now,
//                                delete_flag = false,
//                                status = 1
//                            };
//                            var result = await db.LabTest.AddAsync(obj);
//                            await db.SaveChangesAsync();
//                            await InsertPatientDxLabDetails(obj);
//                            return result.Entity;
//                    }
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task<PatientDxLabDetails> InsertPatientDxLabDetails(LabTest lead)
//        {
//            try
//            {
//                int _id = await primarykeyvalue.primary_key("PatientDxLabDetails");
//                PatientDxLabDetails obj = new PatientDxLabDetails()
//                {
//                    Id = _id,
//                    LT_Id_FK = lead.Id,
//                    CON_Id_FK = lead.Tst_CON_Id_FK,
//                    //Fasting = lead.Fasting,
//                    //Non_Fasting = lead.Non_Fasting,
//                    FastingORNonFasting = lead.FastingORNonFasting,
//                    Lab_Invst_Id_FK = lead.Lab_Invst_Id_FK,
//                    Lab_SubInvst_Id_FK = lead.Lab_SubInvst_Id_FK,
//                    created_by = 1,
//                    created_date = DateTime.Now,
//                    delete_flag = false,
//                    status = 1

//                };
//                var result = await db.PatientDxLabDetails.AddAsync(obj);
//                await db.SaveChangesAsync();
//                return result.Entity;

//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }


//        }
//        public async Task<LabTest> UpdateLabTest(LabTest lead)
//        {
//            try
//            {
//                var result = await db.LabTest.FirstOrDefaultAsync(x => x.Id == lead.Id);
//                if (result != null)
//                {
//                    result.Id = lead.Id;
//                    result.Tst_CON_Id_FK =lead.Tst_CON_Id_FK;
//                    //result.Fasting = lead.Fasting;
//                    //result.Non_Fasting = lead.Non_Fasting;
//                    result.FastingORNonFasting = lead.FastingORNonFasting;
//                    result.Lab_Invst_Id_FK = lead.Lab_Invst_Id_FK;
//                    result.Lab_SubInvst_Id_FK = lead.Lab_SubInvst_Id_FK;
//                    result.modified_by = 2;
//                    result.modified_date = DateTime.Now;
//                    result.delete_flag = false;
//                    result.status = 1;
//                    await db.SaveChangesAsync();
//                    await UpdatePatientDxLabDetails(lead);
//                    return result;
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }

//        public async Task<PatientDxLabDetails> UpdatePatientDxLabDetails(LabTest lead)
//        {
//            var result = await db.PatientDxLabDetails.FirstOrDefaultAsync(x => x.Id == lead.Id && x.LT_Id_FK == lead.Id);
//            if (result != null)
//            {
//                result.Id = lead.Id;
//                result.LT_Id_FK = lead.Id;
//                result.CON_Id_FK = lead.Tst_CON_Id_FK;
//                //result.Fasting = lead.Fasting;
//                //result.Non_Fasting = lead.Non_Fasting;
//                result.FastingORNonFasting = lead.FastingORNonFasting;
//                result.Lab_Invst_Id_FK = lead.Lab_Invst_Id_FK;
//                result.Lab_SubInvst_Id_FK = lead.Lab_SubInvst_Id_FK;
//                result.modified_by = 2;
//                result.modified_date = DateTime.Now;
//                result.delete_flag = false;
//                result.status = 1;
//                await db.SaveChangesAsync();
//                return result;

//            }
//            return null;

//        }
//        public async Task<List<GetLabTest>> GetAllLabTest()
//        {
//            try
//            {
//                if (db != null)
//                {
//                    var query = (from a in db.LabTest
//                                 join b in db.Consultation on a.Tst_CON_Id_FK equals b.CON_Id
//                                 join c in db.LAB_INVESTIGATIONS on a.Lab_Invst_Id_FK equals c.Id
//                                 join d in db.LAB_SUBINVESTIGATIONS on a.Lab_SubInvst_Id_FK equals d.Id
//                                 //join e in db.Doctor on b.CON_DO_Id_FK equals e.DO_Id
//                                 //join f in db.Patient on b.CON_PR_Id_FK equals f.PR_Id
//                                 orderby a.Id descending
//                                 select new GetLabTest
//                                 {
//                                     Id = a.Id,
//                                     Tst_CON_Id_FK = a.Tst_CON_Id_FK,
//                                     //Tst_CON_DO_Id = b.CON_DO_Id_FK,
//                                     //Tst_DO_Name = string.Concat(e.DO_FirstName, e.DO_LastName),
//                                     //Tst_CON_PR_Id = b.CON_PR_Id_FK,
//                                     //Tst_PR_Name = string.Concat(f.PR_FirstName, f.PR_LastName),
//                                     //Tst_PR_Gender = f.PR_Gender,
//                                     //Tst_PR_Age = f.PR_Age,
//                                     //Tst_PR_MobNum = f.PR_MobileNumber,
//                                     //Tst_PR_Email = f.PR_Email,
//                                     //Tst_PR_Address = f.PR_Address,
//                                     //Tst_PR_BloodGroup = f.PR_BloodGroup,
//                                     FastingORNonFasting = a.FastingORNonFasting,
//                                     //Non_Fasting = a.Non_Fasting,
//                                     Lab_Invst_Id_FK = a.Lab_Invst_Id_FK,
//                                     Lab_Invst_Category = c.Category,
//                                     Lab_SubInvst_Id_FK = a.Lab_SubInvst_Id_FK,
//                                     Lab_SubInvst_Category = d.Sub_Category,
//                                     LabTestReport = a.LabTestReport,
//                                     delete_flag = a.delete_flag,
//                                     status = a.status
//                                 });
//                    return await query.ToListAsync();
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }

//        public async Task<LabTest> DeleteLabTest(int Id)
//        {
//            try
//            {
//                var result = await db.LabTest.FirstOrDefaultAsync(x => x.Id == Id);
//                if (result != null)
//                {
//                    result.Id = Id;
//                    result.delete_flag = true;
//                    result.status = 0;
//                    result.deleted_by = 3;
//                    result.deleted_date = DateTime.Now;
//                    await db.SaveChangesAsync();
//                    return result;
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task<LabTestBy_Id> GetLabTestById(int Id)
//        {
//            if (db != null)
//            {
//                var query = (from a in db.LabTest
//                             join b in db.Consultation on a.Tst_CON_Id_FK equals b.CON_Id
//                             join c in db.LAB_INVESTIGATIONS on a.Lab_Invst_Id_FK equals c.Id
//                             join d in db.LAB_SUBINVESTIGATIONS on a.Lab_SubInvst_Id_FK equals d.Id
//                             //join e in db.Doctor on b.CON_DO_Id_FK equals e.DO_Id
//                             //join f in db.Patient on b.CON_PR_Id_FK equals f.PR_Id
//                             where a.Id == Id
//                             select new LabTestBy_Id
//                             {
//                                 Id = a.Id,
//                                 Tst_CON_Id_FK = a.Tst_CON_Id_FK,
//                                 //Tst_CON_DO_Id = b.CON_DO_Id_FK,
//                                 //Tst_DO_Name = string.Concat(e.DO_FirstName, e.DO_LastName),
//                                 //Tst_CON_PR_Id = b.CON_PR_Id_FK,
//                                 //Tst_PR_Name = string.Concat(f.PR_FirstName, f.PR_LastName),
//                                 //Tst_PR_Gender = f.PR_Gender,
//                                 //Tst_PR_Age = f.PR_Age,
//                                 //Tst_PR_MobNum = f.PR_MobileNumber,
//                                 //Tst_PR_Email = f.PR_Email,
//                                 //Tst_PR_Address = f.PR_Address,
//                                 //Tst_PR_BloodGroup = f.PR_BloodGroup,
//                                 //Fasting = a.Fasting,
//                                 //Non_Fasting = a.Non_Fasting,
//                                 FastingORNonFasting = a.FastingORNonFasting,
//                                 Lab_Invst_Id_FK = a.Lab_Invst_Id_FK,
//                                 Lab_Invst_Category = c.Category,
//                                 Lab_SubInvst_Id_FK = a.Lab_SubInvst_Id_FK,
//                                 Lab_SubInvst_Category = d.Sub_Category,
//                                 LabTestReport = a.LabTestReport,
//                                 delete_flag = a.delete_flag,
//                                 status = a.status
//                             }).FirstOrDefaultAsync();
//                return await query;
//            }
//            return null;
//        }

//    }
//}
