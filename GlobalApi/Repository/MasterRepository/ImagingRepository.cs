//using Microsoft.EntityFrameworkCore;
//using GlobalApi.Data;
//using GlobalApi.GlobalClasses;
//using GlobalApi.IRepository.MasterIRepository;
//using GlobalApi.Models.Master;

//namespace GlobalApi.Repository.MasterRepository
//{
//    public class ImagingRepository : IImaging
//    {
//        GlobalContext  db;
//        //public readonly string _connectionString;
//        private IPrimarykeyvalue primarykeyvalue;
//        public ImagingRepository(GlobalContext _db)
//        {
//            db = _db;
//            primarykeyvalue = new Primarykeyvalue(_db);
//        }
//        public async Task<Imaging> InsertImaging(Imaging lead)
//        {
//            try
//            {
//                var duplicate = await db.Imaging.FirstOrDefaultAsync(x => x.Img_CON_Id_FK == lead.Img_CON_Id_FK 
//                && x.Img_Invst_Id_FK == lead.Img_Invst_Id_FK && x.Img_SubInvst_Id_FK == lead.Img_SubInvst_Id_FK);
//                if (duplicate == null)
//                {
//                    int id = await primarykeyvalue.primary_key("Imaging");
//                    Imaging obj = new Imaging()
//                    {
//                        Id = id,
//                        Img_CON_Id_FK = lead.Img_CON_Id_FK,
//                        Img_Invst_Id_FK = lead.Img_Invst_Id_FK,
//                        Img_SubInvst_Id_FK = lead.Img_SubInvst_Id_FK,
//                        created_by = 1,
//                        created_date = DateTime.Now,
//                        delete_flag = false,
//                        status = 1
//                    };
//                    var result = await db.Imaging.AddAsync(obj);
//                    await db.SaveChangesAsync();
//                    await InsertPatientDxImgDetails(obj);
//                    return result.Entity;
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task<PatientDxImgDetails> InsertPatientDxImgDetails(Imaging lead)
//        {
//            int _id = await primarykeyvalue.primary_key("PatientDxImgDetails");
//            PatientDxImgDetails obj = new PatientDxImgDetails()
//            {
//                Id = _id,
//                Img_Id_FK = lead.Id,
//                CON_Id_FK = lead.Img_CON_Id_FK,
//                Img_Invst_Id_FK = lead.Img_Invst_Id_FK,
//                Img_SubInvst_Id_FK = lead.Img_SubInvst_Id_FK,
//                created_by = 1,
//                created_date = DateTime.Now,
//                delete_flag = false,
//                status = 1,
//            };
//            var result = await db.PatientDxImgDetails.AddAsync(obj);
//            await db.SaveChangesAsync();
//            return result.Entity;
//        }

//        public async Task<Imaging> UpdateImaging(Imaging lead)
//        {
//            try
//            {
//                var result = await db.Imaging.FirstOrDefaultAsync(x => x.Id == lead.Id);
//                if (result != null)
//                {
//                    result.Id = lead.Id;
//                    result.Img_CON_Id_FK = lead.Img_CON_Id_FK;
//                    result.Img_Invst_Id_FK = lead.Img_Invst_Id_FK;
//                    result.Img_SubInvst_Id_FK = lead.Img_SubInvst_Id_FK;
//                    result.modified_by = 2;
//                    result.modified_date = DateTime.Now;
//                    result.delete_flag = false;
//                    result.status = 1;
//                    await db.SaveChangesAsync();
//                    await UpdatePatientDxImgDetails(lead);
//                    return result;
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task<PatientDxImgDetails> UpdatePatientDxImgDetails(Imaging lead)
//        {
//            var result = await db.PatientDxImgDetails.FirstOrDefaultAsync(x => x.Id == lead.Id || x.Img_Id_FK == lead.Id);
//            if (result != null)
//            {
//                result.Id = lead.Id;
//                result.Img_Id_FK = lead.Id;
//                result.CON_Id_FK = lead.Img_CON_Id_FK;
//                result.Img_Invst_Id_FK = lead.Img_Invst_Id_FK;
//                result.Img_SubInvst_Id_FK = lead.Img_SubInvst_Id_FK;
//                result.modified_by = 2;
//                result.modified_date = DateTime.Now;
//                result.delete_flag = false;
//                result.status = 1;
//                await db.SaveChangesAsync();
//                return result;

//            }
//            return null;

//        }

//        public async Task<List<GetImaging>> GetAllImaging()
//        {
//            try
//            {
//                if (db != null)
//                {
//                    var query = (from a in db.Imaging
//                                 join b in db.Consultation on a.Img_CON_Id_FK equals b.CON_Id
//                                 join c in db.IMG_INVESTIGATIONS on a.Img_Invst_Id_FK equals c.Id
//                                 join d in db.IMG_SUBINVESTIGATIONS on a.Img_SubInvst_Id_FK equals d.Id
//                                 orderby a.Id descending
//                                 select new GetImaging
//                                 {
//                                     Id = a.Id,
//                                     Img_CON_Id_FK = a.Img_CON_Id_FK,
//                                     //Img_CON_Weight = b.CON_Weight,
//                                     Img_Invst_Id_FK = a.Img_Invst_Id_FK,
//                                     Img_Invst_Category = c.Category,
//                                     Img_SubInvst_Id_FK = a.Img_SubInvst_Id_FK,
//                                     Img_SubInvst_Category = d.Sub_Category,
//                                     ImgTestReport = a.ImgTestReport,
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
//        public async Task<Imaging> DeleteImaging(int Id)
//        {
//            try
//            {
//                var result = await db.Imaging.FirstOrDefaultAsync(x => x.Id == Id);
//                if (result != null)
//                {
//                    result.Id = Id;
//                    result.delete_flag = true;
//                    result.status = 0;
//                    result.deleted_by = 1;
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
//        public async Task<ImagingBy_Id> GetImagingById(int Id)
//        {
//            if (db != null)
//            {
//                var query = (from a in db.Imaging
//                             join b in db.Consultation on a.Img_CON_Id_FK equals b.CON_Id
//                             join c in db.IMG_INVESTIGATIONS on a.Img_Invst_Id_FK equals c.Id
//                             join d in db.IMG_SUBINVESTIGATIONS on a.Img_SubInvst_Id_FK equals d.Id
//                             where a.Id == Id
//                             select new ImagingBy_Id
//                             {
//                                 Id = a.Id,
//                                 Img_CON_Id_FK = a.Img_CON_Id_FK,
//                                 //Img_CON_Weight = b.CON_Weight,
//                                 Img_Invst_Id_FK = a.Img_Invst_Id_FK,
//                                 Img_Invst_Category = c.Category,
//                                 Img_SubInvst_Id_FK = a.Img_SubInvst_Id_FK,
//                                 Img_SubInvst_Category = d.Sub_Category,
//                                 ImgTestReport = a.ImgTestReport,
//                                 delete_flag = a.delete_flag,
//                                 status = a.status
//                             }).FirstOrDefaultAsync();
//                return await query;
//            }
//            return null;
//        }

//    }
//}
