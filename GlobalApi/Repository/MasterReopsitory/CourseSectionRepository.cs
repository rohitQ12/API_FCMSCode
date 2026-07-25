using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIReopsitory;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using static GlobalApi.Models.Master.GetAllCourse_Section;

namespace GlobalApi.Repository.MasterReopsitory
{
    public class CourseSectionRepository : Course_SectionIRepository
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        public CourseSectionRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
        }

        public async Task<string> InsertCourse_Section(InsertCourse_Section insertCourse_Section)
        {
            try
            {

                //if (insertCourse_Section.sc_name == null)
                //{
                //    return "Section name Already Exists";
                //}

                int id = await primarykeyvalue.primary_key("Course_Section");
                Course_Section obj = new Course_Section()
                {
                    sc_id = id,
                    sc_name = insertCourse_Section.sc_name,
                    sc_ch_Fk = insertCourse_Section.sc_ch_Fk,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Course_Section.AddAsync(obj);
                await db.SaveChangesAsync();
                return "Section name Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> UpdateCourse_Section(UpdateCourse_Section updateCourse_Section)
        {
            try
            {
                var result = await db.Course_Section.FirstOrDefaultAsync(x => x.sc_id == updateCourse_Section.sc_id);

                if (result != null)
                {
                    result.sc_id = updateCourse_Section.sc_id;
                    result.sc_name = updateCourse_Section.sc_name;
                    result.sc_ch_Fk = updateCourse_Section.sc_ch_Fk;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    return "Section Updated Successfully";
                }
                return "Section Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async  Task<List<GetAllCourse_Section>> GetAllCourse_Sections()
        {
            try
            {
                var query = (from a in db.Course_Section
                             join b in db.Course_Chapters on a.sc_ch_Fk equals b.ch_id
                             join d in db.Upload_Videos on b.ch_vi_FK equals d.vi_id
                             join c in db.Status on a.status equals c.sts_id
                             where a.sc_id != 0 && a.status!=6
                             select new GetAllCourse_Section
                             {
                                 sc_id = a.sc_id,
                                 ch_name =b.ch_name + "(" + d.vi_name + ")",
                                 sc_name= a.sc_name,
                                 vi_name=d.vi_name,
                                 sc_ch_Fk = a.sc_ch_Fk,
                                 vi_amount = d.vi_amount,
                                 ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
                                 vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
                                 status = a.status,
                                 sts_name = c.sts_name,
                             });
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<List<GetCourseSectionById>> GetCourseSectionByName(string sc_name)
        {
            try
            {
                var query = (from a in db.Course_Section
                             join b in db.Course_Chapters on a.sc_ch_Fk equals b.ch_id
                             join d in db.Upload_Videos on b.ch_vi_FK equals d.vi_id
                             join c in db.Status on a.status equals c.sts_id
                             where a.sc_id != 0 && a.sc_name == sc_name
                             select new GetCourseSectionById
                             {
                                 sc_id = a.sc_id,
                                 ch_name = b.ch_name + "(" + d.vi_name + ")",
                                 sc_name = a.sc_name,
                                 vi_name = d.vi_name,
                                 sc_ch_Fk = a.sc_ch_Fk,
                                 vi_amount = d.vi_amount,
                                 ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
                                 vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
                                 status = a.status,
                                 sts_name = c.sts_name,
                             });
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<GetCourseSectionById> GetCourseSectionById(int sc_Id)
            
        {
            try
            {
                var query = (from a in db.Course_Section
                             join b in db.Course_Chapters on a.sc_ch_Fk equals b.ch_id
                             join d in db.Upload_Videos on b.ch_vi_FK equals d.vi_id
                             join c in db.Status on a.status equals c.sts_id
                             where a.sc_id != 0 && a.sc_id == sc_Id
                             select new GetCourseSectionById
                             {
                                 sc_id = a.sc_id,
                                 ch_name = b.ch_name + "(" + d.vi_name + ")",
                                 sc_name = a.sc_name,
                                 vi_name=d.vi_name,
                                 sc_ch_Fk = a.sc_ch_Fk,
                                 vi_amount = d.vi_amount,
                                 ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
                                 vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
                                 status = a.status,
                                 sts_name = c.sts_name,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
      /*  public async Task<List<GetCourse_Section_DD>> GetCourse_Section_DD()
        {
            var nametoCheck = new List<string> { "Basic Techniques", "Cerebrovascular", "Miscellaneous", "Skull Base", "Tumors", };
            var query = (from a in db.Course_Section
                         join b in db.Course_Chapters on a.sc_ch_Fk equals b.ch_id
                         join d in db.Upload_Videos on b.ch_vi_FK equals d.vi_id
                         join c in db.Status on a.status equals c.sts_id
                         where a.delete_flag == false && a.status == 3 && nametoCheck.Contains(a.sc_name)
                         && a.sc_id != 0
                         orderby a.sc_id
                         select new GetCourse_Section_DD
                         {
                             sc_id = a.sc_id,
                             sc_name = a.sc_name + "(" + b.ch_name + "-" + d.vi_name + ")"
                         }).ToListAsync();
            return await query;
        }*/

        public async  Task<List<GetCourse_Section_DD>> GetCourse_Section_DD()
        {
            var query = (from a in db.Course_Section
                         join b in db.Course_Chapters on a.sc_ch_Fk equals b.ch_id
                         join d in db.Upload_Videos on b.ch_vi_FK equals d.vi_id
                         join c in db.Status on a.status equals c.sts_id
                         where a.delete_flag == false && a.status == 3 
                         && a.sc_id != 0
                         orderby a.sc_id
                         select new GetCourse_Section_DD
                         {
                             sc_id = a.sc_id,
                             sc_name = a.sc_name + "(" + b.ch_name + "-" + d.vi_name + ")" 
                         }).ToListAsync();
            return await query;
        }

        public async Task<string> DeleteCourse_SectionById(int sc_Id)
        {
            try
            {
                var result = await db.Course_Section.FirstOrDefaultAsync(x => x.sc_id == sc_Id);
                if (result != null)
                {
                    result.sc_id = sc_Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Section Deleted Successfully";
                }
                return "Section Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async  Task<string> ApproveCourse_SectionById(ApproveCourse_Section approveCourse_Section)
        {
            try
            {
                var result = await db.Course_Section.Where(x => x.sc_id == approveCourse_Section.sc_id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.status = 3;
                    await db.SaveChangesAsync();
                    return "Section Approved Successfully";
                }
                return "Section Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetCranialSectionDD>> GetCranialSectionDD()
        {
            var query = (from n in db.Course_Package 
                                 join a in db.Course_Master on n.cu_name equals a.cu_name
                                 join b in db.Course_Section on a.cu_sc_id_fk equals b.sc_id
                                 join c in db.Course_Chapters on b.sc_ch_Fk equals c.ch_id
                                 join d in db.Upload_Videos on c.ch_vi_FK equals d.vi_id
                                 join e in db.Status on a.status equals e.sts_id
                                 where a.cu_id != 0 && a.cu_name != "Spinal Surgery"
                                 group new { b } by new { b.sc_id,b.sc_name } into grouped
                         select new GetCranialSectionDD
                         {
                             sc_id = grouped.Key.sc_id,
                             sc_name = grouped.Key.sc_name,

                         });
            return await query.ToListAsync();
        }

        public async Task<List<GetCranialSectionDD>> GetSpinalSectionDD()
        {
            var query = (from n in db.Course_Package
                         join a in db.Course_Master on n.cu_name equals a.cu_name
                         join b in db.Course_Section on a.cu_sc_id_fk equals b.sc_id
                         join c in db.Course_Chapters on b.sc_ch_Fk equals c.ch_id
                         join d in db.Upload_Videos on c.ch_vi_FK equals d.vi_id
                         where a.cu_id != 0 && a.cu_name != "Cranial Surgery"
                         select new GetCranialSectionDD
                         {
                             sc_id = b.sc_id,
                             sc_name = b.sc_name,
                         });
            return await query.ToListAsync();
                
        }
    }
}
